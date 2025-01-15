#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

typedef struct{
	char nombre [20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados [100];
	int num; 
} ListaConectados;

ListaConectados miLista;
int nPartidas = 0;	//Contador partidas

//----------------------------------------------------------------------------- Acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
int numSocket = 0;
int contador;
int sockets[100];

//----------------------------------------------------------------------------- MYSQL
MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;

char consulta [512];
char respuesta [512];
char nick[20]; 
//-----------------------------------------------------------------------------
//----------------------------------------------------------------------------- REGISTRO
int id = 0;
int Registrarse (char p[200], char respuesta[20])
{
	//Peticion
	char nombre_usuario[20], contra[20];
	p = strtok(NULL,"/");
	strcpy(nombre_usuario, p);
	p = strtok(NULL,"/");
	strcpy(contra, p);											
	
	printf("Solicitud de registro recibida: Usuario: %s Contrase�a: %s \n", nombre_usuario, contra);
	
	sprintf(consulta,"SELECT jugadores.nombre_usuario FROM jugadores WHERE jugadores.nombre_usuario = '%s'", nombre_usuario);
	err = mysql_query (conn, consulta);
	if (err != 0)
	{
		printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuesta,"2/ERROR_DB");
		return -1;
	}
	resultado = mysql_store_result (conn);
	row=mysql_fetch_row(resultado);
	
	//Si el usuario no existe, lo anadimos
	if (row == NULL) 
	{
		//Obtenemos el mayor ID actual de los jugadores
		sprintf(consulta, "SELECT MAX(jugadores.id) FROM (jugadores)");
		err = mysql_query (conn, consulta);
		if (err != 0)
		{
			printf ("Error al obtener el ID m�ximo %u %s \n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "2/ERROR_DB");
			return -1;
		}
		resultado = mysql_store_result (conn);
		row=mysql_fetch_row(resultado);
		
		//Convierte ID maximo a un numero entero y le suma 1 para generar un nuevo ID unico para el nuevo jugador
		int id = atoi(row[0])+1;
		
		//Registrar jugador 
		sprintf(consulta, "INSERT INTO jugadores (id, nombre_usuario, password) VALUES (%d,'%s','%s')",id, nombre_usuario, contra);
		err = mysql_query (conn, consulta);
		if (err != 0)
		{
			printf ("Error al a�adir el nuevo jugador: %u %s \n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "2/ERROR_DB");
			return -1;
		}
		printf("Usuario '%s' registrado correctamente con ID %d.\n", nombre_usuario, id);
		sprintf(respuesta, "2/0"); //Indicar que el registro fue exitoso
	}
	else
	{
		printf("El usuario '%s' ya est� registrado.\n", nombre_usuario);
		sprintf(respuesta, "2/-1"); //Indicar que el usuario ya existe
	}
	return 0;
}
void ProcesoRegistro(int sock_conn, char *p) {
    Registrarse(p, respuesta);
    printf("%s\n", respuesta);
    write(sock_conn, respuesta, strlen(respuesta));
}
//----------------------------------------------------------------------------- LOGIN
int LogIn(char p[200], char respuesta[20])
{
	//peticion
	char nombre_usuario[20], contra[20];
	p = strtok( NULL, "/");
	strcpy(nombre_usuario, p);
	p = strtok(NULL, "/");
	strcpy(contra, p);
	
	printf("Solicitud de inicio de sesi�n recibida: Usuario: %s Contrase�a: %s \n", nombre_usuario, contra);

	//Primero miramos ya hay un usuario con el mismo nombre conectado
	int resp = DamePosicionNombre(nombre_usuario);
	if (resp != -1) {
		printf("Jugador ya conectado\n");
		strcpy(respuesta, "1/ALREADY_IN");
		return -1;
	} 
	else 
	{
		//Consultamos si el usuario existe 
		sprintf(consulta, "SELECT nombre_usuario FROM jugadores WHERE nombre_usuario = '%s'", nombre_usuario);
		err = mysql_query(conn, consulta);
		if (err != 0) {
			printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "1/ERROR_DB");
			return -1;
		}
		//Almacenamos y verificamos el resultado
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
	
		if (row == NULL) {
			//No se encontro el usuario
			printf("El usuario '%s' no existe.\n", nombre_usuario);
			strcpy(respuesta, "1/NO_USER");
		}
		else 
		{
			//Si el usuario existe, verificamos la contrasena
			sprintf(consulta, "SELECT nombre_usuario FROM jugadores WHERE nombre_usuario = '%s' AND password = '%s'", nombre_usuario, contra);
			err = mysql_query(conn, consulta);
			if (err != 0) {
				printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "1/ERROR_DB");
				return -1;
			}
			//Verificamos si hay resultados
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado);
		
			if (row == NULL) {
				//El usuario existe, pero la contrasena es incorrecta
				strcpy(respuesta, "1/WRONG_PASSWORD");
			} else {
				//Usuario y contrasena correctos
				pthread_mutex_lock( &mutex );
				int success = PonConectado(nombre_usuario, &sockets[numSocket-1]);
				pthread_mutex_unlock( &mutex );

				if (success == 0) {
					strcpy(respuesta, "1/LOGIN_SUCCESSFUL");
				} else {
					strcpy(respuesta, "1/ERROR_LIST");
				}
			}
		}
	}
	return 0;
}
void ProcesoLogIn(int sock_conn, char *p) {
    LogIn(p, respuesta);
    printf("%s\n", respuesta); // Escribir respuesta del cliente por consola en el servidor "LOGIN_SUCCESSFUL"
    write(sock_conn, respuesta, strlen(respuesta));        
    DameConectados(respuesta);
    sprintf(respuesta, "1/%s", respuesta);
    for (int i = 0; i < numSocket; i++) {
        write(sockets[i], respuesta, strlen(respuesta));
    }
}
//----------------------------------------------------------------------------- DARSE DE BAJA
void DarseBaja(char p[200], char respuesta[200])
{
    // Petición: obtener nombre de usuario y contraseña
    char nombre_usuario[20];
    p = strtok(NULL, "/");
    if (p == NULL) {
        strcpy(respuesta, "5/ERROR_FECHA");
        return;
    }
    strcpy(nombre_usuario, p);

    char contra[20];
    p = strtok(NULL, "/");
    if (p == NULL) {
        strcpy(respuesta, "5/ERROR_FECHA");
        return;
    }
    strcpy(contra, p);

    printf("Solicitud para darse de baja: Usuario: %s Contraseña: %s \n", nombre_usuario, contra);

    // Obtener el ID del usuario
    char consulta[512];
    sprintf(consulta, "SELECT id FROM jugadores WHERE nombre_usuario = '%s' AND password = '%s'", nombre_usuario, contra);

    int err = mysql_query(conn, consulta);
    if (err != 0) {
        printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "5/ERROR_DB");
        return;
    }

    MYSQL_RES *resultado = mysql_store_result(conn);
    if (resultado == NULL) {
        printf("Error al obtener el resultado %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "5/ERROR_DB");
        return;
    }

    MYSQL_ROW row = mysql_fetch_row(resultado);
    if (row == NULL) {
        printf("Usuario o contraseña incorrectos\n");
        strcpy(respuesta, "5/USER_NOT_FOUND");
        mysql_free_result(resultado); // Liberar el resultado de la consulta
        return;
    }

    int user_id = atoi(row[0]);
    mysql_free_result(resultado); // Liberar el resultado de la consulta

    // Iniciar una transacción para asegurar consistencia
    mysql_query(conn, "START TRANSACTION");

    // Eliminar las entradas de info_partida asociadas con el usuario
    sprintf(consulta, "DELETE FROM info_partida WHERE jugador1 = %d OR jugador2 = %d OR jugador3 = %d OR jugador4 = %d", user_id, user_id, user_id, user_id);
    err = mysql_query(conn, consulta);
    if (err != 0) {
        printf("Error al eliminar datos de info_partida %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "5/ERROR_DB");
        mysql_query(conn, "ROLLBACK"); // Revertir la transacción
        return;
    }

    // Eliminar el usuario de la tabla jugadores
    sprintf(consulta, "DELETE FROM jugadores WHERE id = %d", user_id);
    err = mysql_query(conn, consulta);
    if (err != 0) {
        printf("Error al eliminar datos de jugadores %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "5/ERROR_DB");
        mysql_query(conn, "ROLLBACK"); // Revertir la transacción
        return;
    }

    // Confirmar la transacción
    mysql_query(conn, "COMMIT");

    printf("Usuario '%s' eliminado de la base de datos\n", nombre_usuario);
    sprintf(respuesta, "5/DELETED_SUCCESSFUL");
}

int username_exists(const char *username, MYSQL *conn) {
    char consulta[512];
    sprintf(consulta, "SELECT id FROM jugadores WHERE nombre_usuario = '%s'", username);

    int err = mysql_query(conn, consulta);
    if (err != 0) {
        printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
        return 0;
    }

    MYSQL_RES *resultado = mysql_store_result(conn);
    if (resultado == NULL) {
        printf("Error al obtener el resultado %u %s\n", mysql_errno(conn), mysql_error(conn));
        return 0;
    }

    MYSQL_ROW row = mysql_fetch_row(resultado);
    int exists = row != NULL;
    mysql_free_result(resultado); // Liberar el resultado de la consulta
    return exists;
}
//
void ProcesoDarseBaja(int sock_conn, char *p) {
	DarseBaja(p, respuesta);
	printf("%s\n", respuesta); 
	write(sock_conn, respuesta, strlen(respuesta));

	// Eliminar el usuario de la base de datos
	if (EliminarUsuarioDeBaseDeDatos(p)) {
		printf("Usuario eliminado de la base de datos\n");
	} else {
		printf("Error eliminando el usuario de la base de datos\n");
	}

	if (QuitarJugador(&sock_conn)) {
		printf("Usuario eliminado de la lista de usuarios conectados\n");
		for (int i = 0; i < miLista.num; i++) {
			printf("Nombre: %s\nSocket: %d\n\n", miLista.conectados[i].nombre, miLista.conectados[i].socket);
		}
}
DameConectados(respuesta);
sprintf(respuesta, "4/%s", respuesta);
printf("%s", respuesta);
for (int i = 0; i < numSocket; i++) {
	write(sockets[i], respuesta, strlen(respuesta));
}
//----------------------------------------------------------------------------- CHAT
void ProcesoChat(int sock_conn, char *p) {
	char nPartida[10], nombre[20], mesnsaje[512];
				
	p = strtok(NULL, "/");
	strcpy(nPartida, p);
	p = strtok(NULL, "/");
	strcpy(nombre, p);
	char mensaje[512];
	p = strtok(NULL, "/");
	strcpy(mensaje, p);

	sprintf(respuesta, "%d/%s/%s/%s", codigo, nPartida, nombre, menssaje);
	printf("%s\n", respuesta);
	for (int i = 0; i < numSocket; i++) {
		write(sockets[i], respuesta, strlen(respuesta));
	}
}
//----------------------------------------------------------------------------- PROCESOS DE
//----------------------------------------------------------------------------- LISTA DE CONECTADOS

//Anade nuevo conectados, retorna 0 si ok y -1 si la lista ya estaba llena 
int PonConectado (char nombre[20], int *socket)
{
	if (miLista.num == 100)
		return -1;
	else {
		strcpy (miLista.conectados[miLista.num].nombre, nombre);
		miLista.conectados[miLista.num].socket = *socket;
		miLista.num++;
		return 0;
	}
}

// Pone en conectados los nombres de todos los conectados separados por "/". Primero pone el numero de conectados
void DameConectados (char conectado[300])
{
	char conectados[300];
	sprintf(conectados, "%d/", miLista.num);
	for (int i = 0; i < miLista.num; i++){
		sprintf(conectados, "%s%s/", conectados, miLista.conectados[i].nombre);
	}
	printf("Conectados:%s \n", conectados);
	strcpy(conectado, conectados);
}

// Devuelve la posicion en la lista o -1 si no esta en la lista
int DamePosicionNombre (char name[20]) {
	for (int i = 0; i < miLista.num; i++){
		if (strcmp(miLista.conectados[i].nombre, name) == 0);
		return i;
	}
	return -1;
}

int DamePosicionSocket (int *socket) {
	for (int i = 0; i < miLista.num; i++){
		if (miLista.conectados[i].socket == *socket)
		return i;
	}
	return -1;
}

//Busca el socket del jugador para sacarlo de la lista de conectados
int QuitarJugador(int *socket) {
	
	pthread_mutex_lock( &mutex );
	for (int i = 0; i < miLista.num && !found; i++) {
		if (miLista.conectados[i].socket == *socket) {
		
			sockets[i] = sockets[numSocket - 1];
			numSocket--;
			miLista.conectados[i] = miLista.conectados[miLista.num-1];
			miLista.num--;		
		}
	}	
	pthread_mutex_unlock( &mutex );
	return 0;
}	
void ProcesoQuitarJugador(int sock_conn, char *p) {
	if(QuitarJugador(&sock_conn)) {
		printf("Usuario eliminado de la lista,usuarios conectados:\n");
		for (int i = 0; i < miLista.num; i++) {
			printf("Nombre: %s\nSocket: %d\n\n", miLista.conectados[i].nombre, miLista.conectados[i].socket);
		}
	}
	DameConectados (respuesta);
	sprintf (respuesta, "3/%s", `respuesta);
	printf("%s", respuesta);
	for (int i = 0; i < numSocket; i++) {
		write (sockets[i], respuesta, strlen(respuesta));
	}
}
//----------------------------------------------------------------------------- INVITACIONES
void ProcesoInvitar(int sock_conn, char *p) {
	char nombre1[20];   // Personas que son invitadas
	char nombre2[20];
	char nombre3[20];
	char nombre4[20];   // Persona que invita
	int turnos[3];
	int numJugador;

	p = strtok(NULL, "/");
	printf("numJugadores: %s\n", p);
	numJugador = atoi(p);
	p = strtok(NULL, "/");
	strcpy(nombre1, p);

	if (numJugador >= 1 && numJugador <= 3) {
		p = strtok(NULL, "/");
		if (numJugador >= 2) {
			strcpy(nombre2, p);
			p = strtok(NULL, "/");
		}
		if (numJugador == 3) {
			strcpy(nombre3, p);
			p = strtok(NULL, "/");
		}
		strcpy(nombre4, p);

		for (int i = 0; i < numJugador; i++) {
			p = strtok(NULL, "/");
			turnos[i] = atoi(p);
		}

		char *nombres[] = {nombre1, nombre2, nombre3};
		for (int i = 0; i < numJugador; i++) {
			for (int j = 0; j < miLista.num; j++) {
				if (strcmp(nombres[i], miLista.conectados[j].nombre) == 0) {
					sprintf(respuesta, "6/%s/%d", nombre4, turnos[i]);
					write(miLista.conectados[j].socket, respuesta, strlen(respuesta));
					break;
				}
			}
		}
	}
	printf("%s\n", respuesta);
}
void ProcesoAceptarRechazar(int sock_conn, char *p) {
	char res[10], nombre[20], invitado[20];
				
	p = strtok(NULL, "/");
	strcpy(res, p);
	p = strtok(NULL, "/");
	strcpy(nombre, p);
	p = strtok(NULL, "/");
	strcpy(invitado, p);

	int resp = DamePosicionNombre(nom);	//Buscamos el socket de la persona que invita per responderle si han acceptado o no

	if (strcmp(res, "OK") == 0) {
		sprintf(respuesta, "7/OK/%s", invitado);
	}
	else {
		sprintf(respuesta, "7/NO/%s", invitado);
	}
	printf("%s\n", respuesta);
	write(miLista.conectados[resp].socket, respuesta, strlen(respuesta));
}

//----------------------------------------------------------------------------- PROCESOS DE
//----------------------------------------------------------------------------- JUEGO DE LA OCA

void ProcesoEmpezarJugar(int sock_conn, char *p) {
	int nJugadores;
	char J1[20], J2[20], J3[20], J4[20];
	int j = 0;

	p = strtok(NULL, "/");
	nJugadores = atoi(p);
	sprintf(respuesta, "8/%d/%d", nJugadores, nPartidas);

	for (int i = 0; i < nJugadores; i++) {
		p = strtok(NULL, "/");
		sprintf(respuesta, "%s/%s", respuesta, p);
		j++;
		switch (j) {
			case 1:
				strcpy(J1, p);
				break;
			case 2:
				strcpy(J2, p);
				break;
			case 3:
				strcpy(J3, p);
				break;
			case 4:
				strcpy(J4, p);
				break;
		}
	}
	printf("%s\n", respuesta);

	for (int i = 0; i < miLista.num; i++) {
		if (strcmp(miLista.conectados[i].nombre, J1) == 0 || strcmp(miLista.conectados[i].nombre, J2) == 0 ||
			(nJugadores > 2 && strcmp(miLista.conectados[i].nombre, J3) == 0) ||
			(nJugadores > 3 && strcmp(miLista.conectados[i].nombre, J4) == 0)) {
			write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
		}
	}
	nPartidas++;
}

void ProcesoMoverFichas(int sock_conn, char *p) {
	int nPartida, D1, D2;
	char J1[20], J2[20], J3[20], J4[20];

	p = strtok(NULL, "/");
	nPartida = atoi(p);
	p = strtok(NULL, "/");
	D1 = atoi(p);
	p = strtok(NULL, "/");
	D2 = atoi(p);
	p = strtok(NULL, "/");
	strcpy(J1, p);
	p = strtok(NULL, "/");
	strcpy(J2, p);
	p = strtok(NULL, "/");
	strcpy(J3, p);
	p = strtok(NULL, "/");
	strcpy(J4, p);

	sprintf(respuesta, "9/%d/%d/%d/%s/%s/%s/%s", nPartida, D1, D2, J1, J2, J3, J4);
	printf("%s\n", respuesta);

	char *jugadores[] = {J1, J2, J3, J4};

	for (int j = 0; j < 4; j++) {
		if (strcmp("NO", jugadores[j]) != 0) {
			for (int i = 0; i < miLista.num; i++) {
				if (strcmp(miLista.conectados[i].nombre, jugadores[j]) == 0) {
					write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
					break;
				}
			}
		}
	}
}

void ProcesoAbandonarPartida(int sock_conn, char *p) {
	int nPartida;
	char J1[20], J2[20], J3[20], J4[20];

	p = strtok(NULL, "/");
	nPartida = atoi(p);
	p = strtok(NULL, "/");
	strcpy(J1, p);
	p = strtok(NULL, "/");
	strcpy(J2, p);
	p = strtok(NULL, "/");
	strcpy(J3, p);
	p = strtok(NULL, "/");
	strcpy(J4, p);

	sprintf(respuesta, "10/%d/%s/%s/%s/%s", nPartida, J1, J2, J3, J4);
	printf("%s\n", respuesta);

	char *jugadores[] = {J1, J2, J3, J4};

	for (int j = 0; j < 4; j++) {
		if (strcmp("NO", jugadores[j]) != 0) {
			for (int i = 0; i < miLista.num; i++) {
				if (strcmp(miLista.conectados[i].nombre, jugadores[j]) == 0) {
					write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
					break;
				}
			}
		}
	}
}

//----------------------------------------------------------------------------- ACTUALIZAR BBDD
void ProcesoActualizarBBDD(int sock_conn, char *p) {		
    int id, P1, P2, P3, P4, nJugadors, error = 0;
    char fecha[25], hora[20], guanyador[20], J1[20], J2[20], J3[20], J4[20];
    char consulta[512], consulta2[512], consulta_playerID[512];
    float duration;

    p = strtok(NULL, "/");
    strcpy(fecha, p);
    p = strtok(NULL, "/");
    strcpy(hora, p);
    p = strtok(NULL, "/");
    duration = atof(p);
    p = strtok(NULL, "/");
    strcpy(guanyador, p);
    p = strtok(NULL, "/");
    strcpy(J1, p);
    p = strtok(NULL, "/");
    strcpy(J2, p);
    p = strtok(NULL, "/");
    strcpy(J3, p);
    p = strtok(NULL, "/");
    strcpy(J4, p);

    nJugadors = (strcmp(J4, "NO") == 0) ? ((strcmp(J3, "NO") == 0) ? 2 : 3) : 4;

    char *jugadores[] = {J1, J2, J3, J4};
    int *jugadorIDs[] = {&P1, &P2, &P3, &P4};

    for (int i = 0; i < nJugadors; i++) {
        snprintf(consulta_playerID, sizeof(consulta_playerID), "SELECT id FROM jugadores WHERE nombre_usuario = '%s'", jugadores[i]);
        if (mysql_query(conn, consulta_playerID) != 0) {
            printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
            error = 1;
            break;
        }
        resultado = mysql_store_result(conn);
        row = mysql_fetch_row(resultado);
        if (row == NULL) {
            printf("No se encontró al jugador %d\n", i + 1);
            error = 1;
            break;
        }
        *jugadorIDs[i] = atoi(row[0]);
        mysql_free_result(resultado);
    }

    if (error == 0) {
        pthread_mutex_lock(&mutex);

        // Obtener el próximo id de partida
        snprintf(consulta, sizeof(consulta), "SELECT IFNULL(MAX(id), 0) + 1 FROM partidas");
        if (mysql_query(conn, consulta) != 0) {
            printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
            pthread_mutex_unlock(&mutex);
            return;
        }
        resultado = mysql_store_result(conn);
        row = mysql_fetch_row(resultado);
        id = atoi(row[0]);
        mysql_free_result(resultado);

        // Insertar nueva partida
        snprintf(consulta, sizeof(consulta), "INSERT INTO partidas(id, fecha, hora, duracion, ganador) VALUES(%d, '%s', '%s', %f, '%s')", id, fecha, hora, duration, guanyador);
        if (mysql_query(conn, consulta) != 0) {
            printf("Error al insertar datos en la tabla partidas %u %s\n", mysql_errno(conn), mysql_error(conn));
            pthread_mutex_unlock(&mutex);
            return;
        }

        // Insertar info de la partida
		if (nJugadors == 2) {
            snprintf(consulta2, sizeof(consulta2), "INSERT INTO info_partida(jugador1, jugador2, partida) VALUES(%d, %d, %d)", P1, P2, id);
        } else if (nJugadors == 3) {
            snprintf(consulta2, sizeof(consulta2), "INSERT INTO info_partida(jugador1, jugador2, jugador3, partida) VALUES(%d, %d, %d, %d)", P1, P2, P3, id);
        } else {
            snprintf(consulta2, sizeof(consulta2), "INSERT INTO info_partida(jugador1, jugador2, jugador3, jugador4, partida) VALUES(%d, %d, %d, %d, %d)", P1, P2, P3, P4, id);
        }

        if (mysql_query(conn, consulta2) != 0) {
            printf("Error al insertar datos en la tabla info_partida %u %s\n", mysql_errno(conn), mysql_error(conn));
        } else {
            printf("Datos de la partida y jugadores insertados correctamente.\n");
        }

        pthread_mutex_unlock(&mutex);
    } else {
        printf("Error al encontrar algún id(s) de jugador(es)\n");
    }
}
//----------------------------------------------------------------------------- CONSULTAS
void DuracionPartidasJugador(int sock_conn, char *p) // DEVUELVE DURACION TOTAL DE PARTIDAS GANADAS DE UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO
{
	char nombre_usuario[30];
	p = strtok(NULL, "/"); 
	strcpy(nombre_usuario, p);
			
	char consulta[512];
	printf("El nombre del jugador seleccionado es: '%s'\n", nombre_usuario);
			
	sprintf(consulta, "SELECT SUM(partidas.duracion) FROM (partidas) WHERE partidas.ganador = '%s'",nombre_usuario);
			
	err= mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuesta, "12/ERROR_DB");
	}
			
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
			
	if (row[0]==NULL)
		sprintf(respuesta, "12/ERROR_DB");
			
	else
		sprintf(respuesta, "12/%s", row[0]);
			
	printf("%s\n", respuesta);
	write(sock_conn, respuesta, strlen(respuesta));
}

void ResultadoContrincantes(int sock_conn, char *p) // DEVUELVE LISTA DE JUGADORES DE PARTIDAS JUGADAS POR EL USUARIO QUE CONSULTA (uno mismo)
{
	char consulta[512], nombre_usuario[20];

	p = strtok(NULL, "/");
	if (p == NULL) {
		sprintf(respuesta, "13/ERROR_INVALID_INPUT");
		write(sock_conn, respuesta, strlen(respuesta));
		break;
	}
	strcpy(nombre_usuario, p);

	snprintf(consulta, sizeof(consulta),
		"SELECT DISTINCT jugadores.nombre_usuario "
		"FROM jugadores "
		"JOIN info_partida ON jugadores.id IN (info_partida.jugador1, info_partida.jugador2, info_partida.jugador3, info_partida.jugador4) "
		"WHERE info_partida.partida IN ("
		"    SELECT partida "
		"    FROM info_partida "
		"    JOIN jugadores ON jugadores.id IN (info_partida.jugador1, info_partida.jugador2, info_partida.jugador3, info_partida.jugador4) "
		"    WHERE jugadores.nombre_usuario = '%s'"
		") AND jugadores.nombre_usuario != '%s'",
		nombre_usuario, nombre_usuario);

	if (mysql_query(conn, consulta) != 0) {
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(respuesta, "13/ERROR_DB");
		write(sock_conn, respuesta, strlen(respuesta));
		break;
	}

	resultado = mysql_store_result(conn);
	if (resultado == NULL) {
		printf("Error al almacenar el resultado de la consulta %u %s\n", mysql_errno(conn), mysql_error(conn));
		sprintf(respuesta, "13/ERROR_DB");
		write(sock_conn, respuesta, strlen(respuesta));
		break;
	}

	row = mysql_fetch_row(resultado);

	if (row == NULL) {
		sprintf(respuesta, "13/NO_RESULTS_FOUND");
	} else {
		sprintf(respuesta, "13/");
		while (row != NULL) {
			if (strcmp(nombre_usuario, row[0]) != 0) {
				strcat(respuesta, row[0]);
				strcat(respuesta, "/");
			}
			row = mysql_fetch_row(resultado);
		}
		printf("%s\n", respuesta);
	}

	write(sock_conn, respuesta, strlen(respuesta));
	mysql_free_result(resultado);
}

void ListaPartidasPeriodo(int sock_conn, char *p) // LISTA DE PARTIDAS JUGADAS EN UN PERIODIO INTRODUCIDO POR TECLADO 
{
    char fecha[50], consulta[512];
    MYSQL_RES *resultado;
    MYSQL_ROW row;

    // Obtener la fecha
    p = strtok(NULL, "/");
    if (p == NULL) {
        printf("Error: fecha no encontrada\n");
        sprintf(respuesta, "14/ERROR_INVALID_INPUT");
        write(sock_conn, respuesta, strlen(respuesta));
        return;
    }
    strcpy(fecha, p);

    // Preparar la consulta
    snprintf(consulta, sizeof(consulta),
             "SELECT partidas.id, partidas.ganador "
             "FROM partidas "
             "JOIN info_partida ON partidas.id = info_partida.partida "
             "WHERE partidas.fecha = '%s'", fecha);

    printf("Consulta: %s\n", consulta);

    // Ejecutar la consulta
    if (mysql_query(conn, consulta) != 0) {
        printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "14/ERROR_DB");
        write(sock_conn, respuesta, strlen(respuesta));
        return;
    }

    resultado = mysql_store_result(conn);
    if (!resultado) {
        printf("Error al obtener resultados %u %s\n", mysql_errno(conn), mysql_error(conn));
        strcpy(respuesta, "14/ERROR_DB");
        write(sock_conn, respuesta, strlen(respuesta));
        return;
    }

    // Formatear la respuesta
    strcpy(respuesta, "14/");
    while ((row = mysql_fetch_row(resultado)) != NULL) {
        strcat(respuesta, row[0]); // ID de la partida
        strcat(respuesta, "/");
        strcat(respuesta, row[1]); // Nombre del ganador
        strcat(respuesta, "/");
    }
    mysql_free_result(resultado);

    printf("%s\n", respuesta);
    write(sock_conn, respuesta, strlen(respuesta));
}

void ResultadoPartidas(int sock_conn, char *p) // DEVUELVE EL RESULTADO DE PARTIDAS JUGADAS CONTRA UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO 
{
	char consulta[512];
	char J1[20];
	char J2[20];

	p = strtok(NULL, "/");
	strcpy(J1, p);
	p = strtok(NULL, "/");
	strcpy(J2, p);

	sprintf(consulta,
		"SELECT partidas.id, partidas.ganador "
		"FROM partidas "
		"JOIN info_partida ON partidas.id = info_partida.partida "
		"JOIN jugadores J1 ON J1.id = info_partida.jugador1 OR J1.id = info_partida.jugador2 OR J1.id = info_partida.jugador3 OR J1.id = info_partida.jugador4 "
		"JOIN jugadores J2 ON J2.id = info_partida.jugador1 OR J2.id = info_partida.jugador2 OR J2.id = info_partida.jugador3 OR J2.id = info_partida.jugador4 "
		"WHERE J1.nombre_usuario = '%s' AND J2.nombre_usuario = '%s' "
		"ORDER BY partidas.id DESC "
		"LIMIT 5", J1, J2);

	err = mysql_query(conn, consulta);
	if (err != 0) {
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuesta, "15/ERROR_DB");
	} else {
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);

		if (row == NULL) {
			sprintf(respuesta, "15/NO_RESULTS");
		} else {
			sprintf(respuesta, "15");
			while (row != NULL) {
				sprintf(respuesta, "%s/%s/%s", respuesta, row[0], row[1]);
				row = mysql_fetch_row(resultado);
			}
		}
	}
	printf("%s\n", respuesta);
	write(sock_conn, respuesta, strlen(respuesta));
}
//----------------------------------------------------------------------------- PETICIONES CLIENTES
void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	//char nombre [20];
	char conectados[200];
	char peticion[512], respuesta[800];
	//int ret;

	conn = mysql_init(NULL);//Creamos una conexion al servidor MYSQL 
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG8",0, NULL, 0);

	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	// Bucle que atiende las peticiones hasta que el cliente se desconecte
	int terminar = 0;		
	while (terminar == 0)
	{
		// Ahora recibimos la peticion
		int ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que anadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		
switch (codigo) {
			case 0:// ELIMINAR JUGADOR DE LA LISTA
				ProcesoQuitarJugador(sock_conn, p); 8
				break;

			case 1: // LOGIN
				ProcesoLogIn(sock_conn, p);
				break;

			case 2: // REGISTRO 
				ProcesoRegistro(sock_conn, p);
				break;

			case 5: // DARSE DE BAJA
				ProcesoDarseBaja(sock_conn,p);
				break;
				
			case 7: // CHAT
				ProcesoChat(sock_conn, p);
				break;

			case 6: // INVITACION
				ProcesoInvitar(sock_conn,p);
				break;

			case 8: // ACEPTAR/RECHAZAR INVITACION
				ProcesoAceptarRechazar(sock_conn, p);
				break;

			case 9: // EMPEZAR EL JUEGO
				ProcesoEmpezarJugar(sock_conn, p);
				break;
			
			case 20: // MOVER FICHAS
				ProcesoMoverFichar(sock_conn, p);
				break;

			case 21: // ABANDONAR PARTIDA
				ProcesoAbandonarPartida(sock_conn,p);
				break;

			case 22: //ACTUALIZAR BBDD DE PARTIDA
				ProcesoActualizarBBDD(sock_conn, p);
				break;
			
			case 12: //CONSULTA : DEVUELVE DURACION TOTAL DE PARTIDAS GANADAS DE UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO
				 DuracionPartidasJugador(sock_conn, p);
				break;

			case 13: //CONSULTA : DEVUELVE LISTA DE JUGADORES DE PARTIDAS JUGADAS POR EL USUARIO QUE CONSULTA (uno mismo)
				ResultadoContrincantes(sock_conn, p);
				break;

			case 14: //CONSULTA 14 : LISTA DE PARTIDAS JUGADAS EN UN PERIODIO INTRODUCIDO POR TECLADO 
				ListaPartidasPeriodo(sock_conn, p);
				break;

			case 15: //CONSULTA 15 : DEVUELVE EL RESULTADO DE PARTIDAS JUGADAS CONTRA UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO 
				ResultadoPartidas(sock_conn, p);
				break;

			default:
			break;
		}
	}
	close(sock_conn);	// Se acabo el servicio para este cliente
}
//-----------------------------------------------------------------------------
//----------------------------------------------------------------------------- MAIN PROGRAM
int main(int argc, char **argv)
{	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	
	// INICIALIZACIONES socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	
	// Hacemos el bind al puerto
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY); // asociar socket a cualquier IP
	serv_adr.sin_port = htons(50089); // establecemos el puerto de escucha
	
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	
	if (listen(sock_listen, 100) < 0)
		printf("Error en el Listen");
	
	contador =0;
	pthread_t thread; //creo la estuctura de threads y declaro un vector de threads, en creador de threads incluyo el que estamos usando ahora
	
	// bucle infinito
	for(;;){ 
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[numSocket] = sock_conn;//sock_conn es el socket que usaremos para este cliente
		pthread_create (&thread, NULL, AtenderCliente, &sockets[numSocket]);// Crear thead y decirle lo que tiene que hacer
		numSocket++;
	}
}

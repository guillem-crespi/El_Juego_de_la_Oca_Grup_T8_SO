
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
	int num;//si en la lista hay 5 conectados, de 0 a 4 el siguiente es el 5	
} ListaConectados;


// ------------------------------------------------- Acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int contador;
int sockets[100];
ListaConectados miLista;


// ------------------------------------------------- MYSQL
MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;



char consulta [512];
char respuesta [512];
char nick[20]; 


//--------------------------------------------------	
//-------------------------------------------------- CONECTADOS

//Añade nuevo conectados
//retorna 0 si ok y -1 si la lista ya estaba llena 

int PonConectado (ListaConectados *lista, char nombre[20], int socket)
{
	if (lista->num == 100)
		return -1;
	else {
		strcpy (lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
}


// Pone en conectados los nombres de todos los conectados separados por "/". 
// primero pone el numero de conectados

void DameConectados (ListaConectados *lista, char conectado[200])
{
	char conectados[512];
	sprintf(conectados, "%d/", lista->num);
	int i;
	for (i = 0; i < lista->num; i++){
		sprintf(conectados, "%s%s/", conectados, lista->conectados[i].nombre);
	}
	//conectados[strlen(conectados) - 1] = '\0';
	//strcat(conectados, "/");	
	sprintf(conectado, "Conectados:%s \n", conectados);
	printf(conectado);
}

//-----------------------------
int DamePosicion (ListaConectados *lista, int socket){
	
	int i=0;
	int encontrado=0;
	while ((i< lista->num) && !encontrado)
	{
		if (lista->conectados[i].socket==socket)
			encontrado =1;
		if (!encontrado)
			i++;
	}
	if (encontrado)
		return i;
	else
		return -1;
}

//------------------------------------------------------
//------------------------------------------------------ REGISTRO
int id = 0;

int Registrarse (char p[200], char respuesta[20])
{
	//peticion
	char nombre_usuario[20];
	p = strtok(NULL,"/");
	strcpy(nombre_usuario, p);
	
	char contra[20];
	p = strtok(NULL,"/");
	strcpy(contra, p);											
	
	printf("Solicitud de registro recibida: Usuario: %s Contraseña: %s \n", nombre_usuario, contra);
	
	char consulta[512];
	sprintf(consulta,"SELECT jugadores.nombre_usuario FROM jugadores WHERE jugadores.nombre_usuario = '%s'", nombre_usuario);
	
	err = mysql_query (conn, consulta);
	printf("el nombre y contraseÃ±a son: %s, %s\n" ,nombre_usuario, contra);
	if (err != 0)
	{
		printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuesta,"ERROR_DB");
		exit(1);
	}
	resultado = mysql_store_result (conn);
	row=mysql_fetch_row(resultado);
	
	// Si el usuario no existe, lo añadimos
	if (row == NULL) 
	{
		// Obtenemos el mayor ID actual de los jugadores
		sprintf(consulta, "SELECT MAX(jugadores.id) FROM (jugadores)");
		err = mysql_query (conn, consulta);
		if (err != 0)
		{
			printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "ERROR_DB");
			exit(1);
		}
		resultado = mysql_store_result (conn);
		row=mysql_fetch_row(resultado);
		
		//convierte ID máximo a un número entero y le suma 1 para generar un nuevo ID único para el nuevo jugador
		int id = atoi(row[0])+1;
		
		//Registrar jugador 
		sprintf(consulta, "INSERT INTO jugadores (id, nombre_usuario, password) VALUES (%d,'%s','%s')",id, nombre_usuario, contra);
		err = mysql_query (conn, consulta);
		printf("Su nombre y contraseÃ±a son: %s, %s\n" ,nombre_usuario, contra);
		if (err != 0)
		{
			printf ("Error al consultar datos de la base %u %s \n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "ERROR_DB");
			exit(1);
		}
		printf("Usuario '%s' registrado correctamente con ID %d.\n", nombre_usuario, id);
		sprintf(respuesta, "0"); // Indicar que el registro fue exitoso
	}
	else
	{
		printf("El usuario '%s' ya está registrado.\n", nombre_usuario);
		sprintf(respuesta, "-1"); // Indicar que el usuario ya existe
	}
}

//---------------------------------------------------------
//--------------------------------------------------------- LOGIN

int LogIn(char p[200], char respuesta[20])
{
	//peticion
	char nombre_usuario[20];
	p = strtok( NULL, "/");
	strcpy(nombre_usuario, p);
	
	char contra[20];
	p = strtok(NULL, "/");
	strcpy(contra, p);
	
	printf("Solicitud de inicio de sesión recibida: Usuario: %s Contraseña: %s \n", nombre_usuario, contra);
	
	// Primero, consultamos si el usuario existe 
	char consulta_usuario[512];
	sprintf(consulta_usuario, "SELECT nombre_usuario FROM jugadores WHERE nombre_usuario = '%s'", nombre_usuario);
	
	err = mysql_query(conn, consulta_usuario);
	if (err != 0) {
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuesta, "ERROR_DB");
		exit(1);
	}
	
	// Almacenamos y verificamos el resultado
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	
	if (row == NULL) {
		// No se encontró el usuario
		printf("El usuario '%s' no existe.\n", nombre_usuario);
		strcpy(respuesta, "NO_USER");
	}
	else 
	{
		// Si el usuario existe, verificamos la contraseña
		char consulta_contra[512];
		sprintf(consulta_contra, "SELECT nombre_usuario FROM jugadores WHERE nombre_usuario = '%s' AND password = '%s'", nombre_usuario, contra);
		
		err = mysql_query(conn, consulta_contra);
		if (err != 0) {
			printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuesta, "ERROR_DB");
			exit(1);
		}
		
		// Verificamos si hay resultados
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		if (row == NULL) {
			// El usuario existe, pero la contraseña es incorrecta
			strcpy(respuesta, "WRONG_PASSWORD");
		} else {
			// Usuario y contraseña correctos
			strcpy(respuesta, "SI");
		}
	}
}


//--------------------------------------------------------
//-------------------------------------------------------- PETICIONES CLIENTES

void *AtenderCliente (void *socket)
{
	
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	char conectado[200];
	char peticion[512];
	char respuesta[512];
	int ret;
	
	/*//inizializamos conexion con la base de datos
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;// Estructura especial para almacenar resultados de consultas 
	MYSQL_ROW row;
	*/
	
	conn = mysql_init(NULL);//Creamos una conexion al servidor MYSQL 
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BBDD",0, NULL, 0); 

	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	// Bucle que atiende las peticiones hasta que el cliente se desconecte
	int terminar =0;		
	while (terminar ==0)
	{
		// Ahora recibimos la peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que añadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		
		///////////////////////////////////////////////////////////////
		if (codigo ==0) // CONUSLTA 0 : DESCONECTAR
			terminar=1;
		
		
		//////////////////////////////////////////////////////////////
		else if (codigo ==1) // CONSULTA 1 : LOGIN
		{	
			LogIn(p,respuesta);
			
			printf("%s\n", respuesta);
			write(sock_conn, respuesta, strlen(respuesta));
			
			pthread_mutex_lock( &mutex );
			//LogIn(p,respuesta,sock_conn);
			DameConectados(&miLista,conectado);
			pthread_mutex_unlock( &mutex );
			
		}
		///////////////////////////////////////////////////////////////
		else if (codigo== 2) // CONSULTA 2 : REGISTRO 
		{
			
			Registrarse(p, respuesta);
			
			printf("%s\n", respuesta);
			write(sock_conn, respuesta, strlen(respuesta));
			
		}	
		
		/////////////////////////////////////////////////////////////////	
		else if (codigo== 3)// CONSULTA 3 : LISTA CONECTADOS
		{
			
			char conectados [300];
			DameConectados (&miLista, conectados);
			printf("lista de conectados: %s\n", conectados);
			
			for (i=0; i < miLista.num; i++)
			{
				write (miLista.conectados[i].socket, conectados, strlen(conectados));
			}
			
			
			printf ("%s", respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		///////////////////////////////////////////////////////////////
		else if (codigo ==5) // CONSULTA 5 : DEVUELVE JUGADORES QUE JUGARON UN DIA INTROCUDIO POR TECLADO
		{
			char fecha[30];
			p = strtok(NULL, "/"); 
			strcpy(fecha,p);
			char consulta[512];
			char nombres[50];
			
			/*sprintf (consulta, "SELECT jugadores.nombre_usuario "
			"FROM jugadores "
			"JOIN info_partida ON (info_partida.jugador1 = jugadores.id OR "
			"info_partida.jugador2 = jugadores.id OR "
			"info_partida.jugador3 = jugadores.id OR "
			"info_partida.jugador4 = jugadores.id) "
			"JOIN partidas ON partidas.id = info_partida.partida "
			"WHERE partidas.fecha = '%s'",fecha);*/
			
			sprintf (consulta, "SELECT jugadores.nombre_usuario FROM jugadores,partidas,info_partida WHERE partidas.fecha = '%s' AND partidas.id=info_partida.partida AND (info_partida.jugador1 = jugadores.id OR info_partida.jugador2 = jugadores.id OR info_partida.jugador3 = jugadores.id OR info_partida.jugador4 = jugadores.id)",fecha);
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			printf("consulta: %s\n",consulta);
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			
			if (row == NULL)
				sprintf (resultado, "ERROR_DB");
			
			else 
			{
				while (row!=NULL) 
				{
					sprintf (nombres, row[0]);
					row = mysql_fetch_row(resultado);
					sprintf(resultado, "%s%s/ \n", resultado, nombres);
				}
			}	
			printf("Consulta de jugadores que jugaron el %s: %s\n", fecha, resultado);
			write (sock_conn,resultado, strlen(resultado));
		}
		
		//////////////////////////////////////////////////////////
		else if (codigo ==6)// CONSULTA 6: DimeGanadores: que jugadores ganaron ese dia
		{
			char fecha[30];
			p = strtok(NULL, "/"); 
			strcpy(fecha,p);
			char consulta[512];
			char nombres[50];
			
			sprintf(consulta, "SELECT DISTINCT jugadores.nombre_usuario "
					"FROM jugadores, partidas "
					"WHERE partidas.fecha = '%s' AND partidas.ganador = jugadores.nombre_usuario", fecha); //jugadores.id
			
			
			err=mysql_query (conn, consulta);
			if (err!=0) {	
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}		
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL){
				printf("Error al almacenar el resultado de la consulta: %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "ERROR_DB");
			}
			else 
			{
				while (row!=NULL) 
				{
					sprintf (nombres, row[0]);
					row = mysql_fetch_row(resultado);
					sprintf(resultado, "%s%s/ \n", resultado, nombres);
				}
			}
			printf("Consulta de jugadores que ganaron el %s: %s\n", fecha, resultado);
			write (sock_conn,resultado, strlen(resultado));	
		}
		
		
		//////////////////////////////////////////////////////////////	
		//-Consulta Duracion total partidas-
		else if (codigo ==7)
		{
			
			char nombre_usuario[30];
			p = strtok(NULL, "/"); 
			strcpy(nombre_usuario, p);
			
			char consulta[512];
			printf("El nomdre del jugador seleccionado es: '%s'\n", nombre_usuario);
			
			
			sprintf (consulta, "SELECT SUM(partidas.duracion) FROM (partidas) WHERE partidas.ganador = '%s'",nombre_usuario);
			
			err= mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			printf("consulta: %s\n",consulta);
			
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row[0]==NULL)
				sprintf (respuesta, "ERROR_DB");
			
			else 
				sprintf (respuesta, row[0]);
			
			
			printf("2%s", respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		/////////////////////////////////////////////////////////////////
		else if ((codigo ==1) || (codigo==2) || (codigo==3) || (codigo==4) || (codigo==5) || (codigo==6)|| (codigo==7))
		{
			pthread_mutex_lock( &mutex );
			contador=contador+1;
			pthread_mutex_unlock( &mutex );
		}	
		
	}
	close(sock_conn);	// Se acabo el servicio para este cliente
}


//-------------------------------------------------------------------------------------------------
//----------------------------------MAIN PROGRAM---------------------------------------------------
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
	serv_adr.sin_port = htons(9010); // establecemos el puerto de escucha
	
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 100) < 0)
		printf("Error en el Listen");
	
	contador =0;
	int i;
	int sockets[100];
	pthread_t thread; //creo la estuctura de threads y declaro un vector de threads, en creador de threads incluyo el que estamos usando ahora
	i=0;
	
	// bucle infinito
	for(;;){ 
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;//sock_conn es el socket que usaremos para este cliente
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);// Crear thead y decirle lo que tiene que hacer
		i=i+1;
	}
}


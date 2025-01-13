using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Threading;
using System.Security.Cryptography;
using System.Globalization;
using System.Text.RegularExpressions;

// FORM1: CORRESPONDE AL FORMULARIO PRINCIPAL, DESPUES DE INICIAR SESION (FORM3)

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Configuraciones iniciales
        Socket server;                 //Conexion al servidor
        Thread atender;                //Hilo para atender mensajes del servidor
        private string nickname;
        private string password;

        delegate void DelegadoParaEscribir(string mensaje); //Delegado para permitir operaciones en el hilo

        //Lista de formularios abiertos
        List<Form2> Forms = new List<Form2>(); 

        //Lista jugadores invitados
        private List<string> invitados = new List<string>(4);
        private int nJugador = 0;

       
        public Form1(string nickname, string password, Socket server)
        {
            this.nickname = nickname;
            this.password = password;
            this.server = server;
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- CARGAR FORMULARIO PRINCIPAL
        private void Form1_Load(object sender, EventArgs e)
        {
            //Configuracion ListaConectados
            ListaConectados.ColumnCount = 1;
            ListaConectados.Columns[0].Name = "Conectados";

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Seleccionar",
                HeaderText = "Invitar",
            };

            ListaConectados.Columns.Add(checkBoxColumn);
            ListaConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ListaConectados.AllowUserToAddRows = false;
            ListaConectados.Columns[0].ReadOnly = true; //Conectados no editables
            ListaConectados.Columns[1].ReadOnly = false;
            ListaConectados.Columns[1].Width = 50;

            //Hilo para atender al servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- BOTON DESCONECTAR (CONSULTA 0)
        private void button_Desconectar_Click(object sender, EventArgs e) 
        {
            //Mensaje de desconexion
            string mensaje = "0/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            atender.Abort();

            //Nos desconectamos
            this.BackColor = Color.IndianRed;
            MessageBox.Show("Desconectado");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Close();
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- BOTON DARSE DE BAJA (CONSULTA 5)
        private void button_Baja_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + nickname + "/" + password;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            button_Desconectar_Click(sender, e);
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- BOTON ENVIAR CONSULTA
        private void button2_Click(object sender, EventArgs e)
        {
            if (DimeJugadores.Checked) // CONSULTA 10 : JUGADORES QUE JUGARON (EL DIA INTRODUCIDO POR TECLADO)
            {
                string fecha = ConsultaFecha.Text;
                if (!EsFechaValida(fecha))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "Formato de fecha no válido. Use el formato dd-MM-yyyy."));
                    return;
                }

                string mensaje = "10/" + fecha;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (DimeGanadores.Checked)  // CONSULTA 11 : JUGADORES QUE GANARON (EL DIA INTRODUCIDO POR TECLADO)
            {
                string fecha = ConsultaFecha.Text;
                if (!EsFechaValida(fecha))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "Formato de fecha no válido. Use el formato dd-MM-yyyy."));
                    return;
                }

                string mensaje = "11/" + fecha;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (SumaDuracion.Checked) // CONSULTA 12 : DURACION TOTAL DE PARTIDAS GANADAS (DE UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO)
            {
                string nombre = ConsultaNombre.Text;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "El nombre no puede estar vacío."));
                    return;
                }

                string mensaje = "12/" + nombre;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (ListadoJugadores.Checked) // CONSULTA 13 : LISTA DE JUGADORES DE PARTIDAS JUGADAS (CON UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO) (Requisito minimo) 
            {
                string nombre = nickname;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "El nombre no puede estar vacío."));
                    return;
                }

                string mensaje = "13/" + nombre;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (PartidasDia.Checked) // CONSULTA 14 : LISTA DE PARTIDAS JUGADAS (EN UN PERIODO INTRODUCIDO POR TECLADO) (Requisito minimo) 
            {
                string fecha1 = ConsultaFecha.Text;
                string fecha2 = ConsultaPeriodo.Text;

                if (!EsFechaValida(fecha1))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "Formato de fecha1 no válido. Use el formato dd-MM-yyyy."));
                    return;
                }

                if (!EsFechaValida(fecha2))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "Formato de fecha2 no válido. Use el formato dd-MM-yyyy."));
                    return;
                }

                // Construir el mensaje y enviarlo al servidor
                string mensaje = "14/" + fecha1 + "/" + fecha2;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (ResultadoParitdas.Checked) // CONSULTA 15 : RESULTADO DE PARTIDAS JUGADAS CON UN (JUGADOR-NOMBRE INTRODUCIDO POR TECLADO) (Requisito minimo)
            {
                string nombre = ConsultaNombre.Text;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Resposta_Lbl.Invoke(new Action(() => Resposta_Lbl.Text = "El nombre no puede estar vacío."));
                    return;
                }

                string mensaje = "15/" + nickname + "/" + nombre;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        //FUNCIÓN PARA COMPROBAR SI LA FECHA ES VÁLIDA
        private bool EsFechaValida(string fecha) 
        {
            // Expresión regular para validar el formato de la fecha (dd-MM-yyyy)
            string pattern = @"^\d{2}-\d{2}-\d{4}$";

            // Verificar si la fecha coincide con el patrón
            if (!Regex.IsMatch(fecha, pattern))
            {
                return false;
            }

            // Intentar convertir la cadena a una fecha válida
            DateTime dt;
            if (DateTime.TryParseExact(fecha, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return true;
            }

            return false;
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- BOTON EMPEZAR A JUGAR 
        private void button_MatrizJuego_Click(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { EmpezarJuego("NO", "NO", "NO", "NO", 0); };
            Thread J = new Thread(ts);
            J.Start();
        }   
        
        // Metodo para empezar una nueva partida
        private void EmpezarJuego(string J1, string J2, string J3, string J4, int nPartida)
        {
            int nForm = Forms.Count;
            Form2 f2 = new Form2(nickname, nJugador, nForm, server, J1, J2, J3, J4, nPartida);
            Forms.Add(f2);
            f2.ShowDialog();
        }
        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------BOTON INVITAR A JUGAR 
        private void button1_Click(object sender, EventArgs e)
        {
            invitados = new List<string> { "", "", "", "", "" };

            int nInvitados = 0;
            string dest = "";
            string dest2 = "";
            string dest3 = "";
            string reg;
            int res1 = 0;
            int res2 = 0;
            int res3 = 0;
            int res4 = 0;

            foreach (DataGridViewRow row in ListaConectados.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    nInvitados++;
                }
            }

            if (nInvitados > 3)
            {
                MessageBox.Show("Máximo 3 invitaciones");
                return;
            }

            foreach (DataGridViewRow row in ListaConectados.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && Convert.ToBoolean(row.Cells["Seleccionar"].Value))
                {
                    string playerName = Convert.ToString(row.Cells["Conectados"].Value)?.Trim();
                    if (playerName == nickname)
                    {
                        MessageBox.Show("No te puedes invitar a ti mismo");
                        return;
                    }
                    else if (!string.IsNullOrEmpty(playerName))
                    {
                        if (string.IsNullOrEmpty(dest))
                        {
                            dest = playerName;
                        }
                        else if (string.IsNullOrEmpty(dest2))
                        {
                            dest2 = playerName;
                        }
                        else if (string.IsNullOrEmpty(dest3))
                        {
                            dest3 = playerName;
                        }
                        else
                        {
                            MessageBox.Show("Error en el proceso");
                            return;
                        }
                    }
                }
            }
            Random random = new Random();

            if (nInvitados == 1)
            {
                res1 = random.Next(1, 3);
                res2 = random.Next(1, 3);

                while (res2 == res1)    //Comprovació que els jugadors tinguin el mateix número
                    res2 = random.Next(1, 3);

                reg = $"6/{nInvitados}/{dest}/{nickname}/{res2}";

                invitados[res1 - 1] = nickname;
                invitados[res2 - 1] = dest;

                for (int i = 1; i <= 4; i++)
                {
                    if (i != res1 && i != res2)
                    {
                        res3 = i;
                        invitados[i - 1] = "NO";
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    if (i != res1 && i != res2 && i != res3)
                    {
                        res4 = i;
                        invitados[i - 1] = "NO";
                    }
                }
                invitados[4] = "3"; //Amb aquest número sabem quanta gent falta per acceptar la invitació (3 de 4 jugadors perquè falta 1 (el dest) per acceptar)
            }
            else if (nInvitados == 2)
            {
                res1 = random.Next(1, 4);
                res2 = random.Next(1, 4);
                res3 = random.Next(1, 4);

                while (res2 == res1)
                    res2 = random.Next(1, 4);

                while (res3 == res1 || res3 == res2)
                    res3 = random.Next(1, 4);

                reg = $"6/{nInvitados}/{dest}/{dest2}/{nickname}/{res2}/{res3}";

                invitados[res1 - 1] = nickname;
                invitados[res2 - 1] = dest;
                invitados[res3 - 1] = dest2;

                for (int i = 1; i <= 4; i++)
                {
                    if (i != res1 && i != res2 && i != res3)
                    {
                        res4 = i;
                        invitados[i - 1] = "NO";
                    }
                }
                invitados[4] = "2";
            }
            else if (nInvitados == 3)
            {
                res1 = random.Next(1, 5);
                res2 = random.Next(1, 5);
                res3 = random.Next(1, 5);
                res4 = random.Next(1, 5);

                while (res2 == res1)
                    res2 = random.Next(1, 5);

                while (res3 == res1 || res3 == res2)
                    res3 = random.Next(1, 5);

                while (res4 == res1 || res4 == res2 || res4 == res3)
                    res4 = random.Next(1, 5);

                reg = $"6/{nInvitados}/{dest}/{dest2}/{dest3}/{nickname}/{res2}/{res3}/{res4}";

                invitados[res1 - 1] = nickname;
                invitados[res2 - 1] = dest;
                invitados[res3 - 1] = dest2;
                invitados[res4 - 1] = dest3;

                invitados[4] = "1";
            }
            else
            {
                return;
            }
            byte[] msg = Encoding.ASCII.GetBytes(reg);
            server.Send(msg);
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- ATENDER SERVIDOR
        private void AtenderServidor()
        {
            string mensaje;
            DelegadoParaEscribir delegado;

            while (true)
            {       
                    //Recibimos la respuesta del servidor 
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    string[] trozos = mensaje.Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
               
                switch (codigo)
                {
                    case 3: // ACTUALIZA LA LISTA DE USUARIOS CONECTADOS
                        Invoke(new Action(() =>
                        {
                            ListaConectados.Rows.Clear();
                            int num = Convert.ToInt32(trozos[1]);
                            for (int i = 2; i < num + 2; i++)
                            {
                                ListaConectados.Rows.Add(trozos[i], false);
                            }
                            ListaConectados.ClearSelection();
                        }));
                    break;

                    case 5: // RESPUESTA CONSULTA 0 : ELIMINA USUARIO DADO DE BAJA
                        mensaje = trozos[1];

                        if (mensaje == "ERROR_DB")
                            MessageBox.Show("No se puede dar de baja");
                        else if (mensaje == "DELETED_SUCCESSFUL")
                        {
                            MessageBox.Show("Usuario dado de baja correctamente");

                            //Mensaje de desconexion
                            mensaje = "0/";
                            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            atender.Abort();

                            //Nos desconectamos
                            this.BackColor = Color.Gray;
                            MessageBox.Show("Desconectado");
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                            Close();
                        }
                    break;

                    case 6: // GESTION DE INVITACIONES
                        DialogResult result = MessageBox.Show(
                            trozos[1] + " te ha invitado a jugar. Eres el jugador " + trozos[2] + " si todos aceptan la invitación",
                            "OK",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question); //Pregunta del servidor respecto a la invitacion

                        if (result == DialogResult.OK) //Invitacion aceptada 
                        {
                            nJugador = Convert.ToInt32(trozos[2]); //Asigna numero del jugador si acepta
                            mensaje = "8/OK/" + trozos[1] + "/" + nickname;
                            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                        else if (result == DialogResult.Cancel) //Invitacion rechazada
                        {
                            mensaje = "8/NO/" + trozos[1] + "/" + nickname;
                            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                    break;

                    case 7: // ACTUALIZA MENSAJES EN EL CHAT
                        int numPartida = Convert.ToInt16(trozos[1]);
                        int numForm = -1;
                        bool found = false;

                        for (int i = 0; i < Forms.Count && !found; i++)
                        {
                            if (numPartida == Forms[i].GetPartidaNum())
                            {
                                numForm = Forms[i].GetFormNum();
                                found = true;
                            }
                        }

                        if (numForm != -1)
                        {
                            Form2 f2 = Forms[numForm];
                            f2.ActualizarChat(trozos[2], trozos[3]);
                        }
                    break;

                    case 8: // ACTUALIZA ESTADO DE INVITACIONES
                        int nInvitados = Convert.ToInt32(invitados[4]);
                        string players = "";

                        if (trozos[1] == "OK") //Jugador acepta
                        {
                            if ((nInvitados + 1) == 4) 
                            {
                                MessageBox.Show("Todos los invitados han decidido");

                                invitados[4] = "4";
                                mensaje = "9/";
                                int nJugadores = 0;

                                for (int i = 0; i <= 3; i++)
                                {
                                    if (invitados[i] != "NO")
                                    {
                                        players += "/" + invitados[i];
                                        nJugadores++;
                                    }
                                }
                                mensaje += Convert.ToString(nJugadores) + players;
                                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                                server.Send(msg);
                            }
                            else 
                            {
                                invitados[4] = Convert.ToString(nInvitados + 1);
                            }
                        }
                        else //Jugador rechaza
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (trozos[2] == invitados[i])
                                {
                                    invitados[i] = "NO";
                                }
                            }

                            if ((nInvitados + 1) == 4)
                            {
                                MessageBox.Show("Todos los invitados han decidido");

                                invitados[4] = "4";
                                mensaje = "9/";
                                int nJugadores = 0;

                                for (int i = 0; i <= 3; i++)
                                {
                                    if (invitados[i] != "NO")
                                    {
                                        mensaje += invitados[i] + "/";
                                        nJugadores++;
                                    }
                                }

                                if (nJugadores > 1)
                                {
                                    mensaje += Convert.ToString(nJugadores);
                                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                }
                                else
                                    MessageBox.Show("Todos han rechazado tu invitacion :(\n No puedes jugar solo :(((");
                            }
                            else
                            {
                                invitados[4] = Convert.ToString(nInvitados + 1);
                            }
                        }
                    break;

                    case 9: // INICAR PARTIDA DE LOS JUGADORES CONFIRMADOS 
                        int nJugadors = Convert.ToInt32(trozos[1]);
                        int nPartida = Convert.ToInt32(trozos[2]);
                        string Jugador1 = "NO";
                        string Jugador2 = "NO";
                        string Jugador3 = "NO";
                        string Jugador4 = "NO";

                        switch (nJugadors) //Asignar jugadores segun numero de participantes
                        {
                            case 2:
                                Jugador1 = trozos[3];
                                Jugador2 = trozos[4];
                                break;

                            case 3:
                                Jugador1 = trozos[3];
                                Jugador2 = trozos[4];
                                Jugador3 = trozos[5];
                                break;

                            case 4:
                                Jugador1 = trozos[3];
                                Jugador2 = trozos[4];
                                Jugador3 = trozos[5];
                                Jugador4 = trozos[6];
                                break;
                        }

                        if (Jugador1 == nickname)
                        {
                            nJugador = 1;
                        }
                        else if (Jugador2 == nickname)
                        {
                            nJugador = 2;
                        }
                        else if (Jugador3 == nickname)
                        {
                            nJugador = 3;
                        }
                        else if (Jugador4 == nickname)
                        {
                            nJugador = 4;
                        }
                        // Inicia la partida en un nuevo hilo
                        ThreadStart ts = delegate { EmpezarJuego(Jugador1, Jugador2, Jugador3, Jugador4, nPartida); };
                        Thread J = new Thread(ts);
                        J.Start();
                    break;

                    case 10:  // RESPUESTA CONSULTA 10 : JUGADORES QUE JUGARON EL DIA INTRODUCIDO POR TECLADO
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador en ese dia" });
                        else
                            Resposta_Lbl.Invoke(delegado, new object[] { "Los jugadores que jugaron ese día son: " + mensaje });
                    break;

                    case 11: // RESPUESTA CONSULTA 11 : JUGADORES QUE GANARON EL DIA INTRODUCIDO POR TECLADO
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador en ese dia" });
                        else
                            Resposta_Lbl.Invoke(delegado, new object[] { "Los jugadores que ganaron ese día son: " + mensaje });
                    break;

                    case 12: // RESPUESTA CONSULTA 12 : DURACION TOTAL DE PARTIDAS GANADAS DE UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador" });
                        else
                            Resposta_Lbl.Invoke(delegado, new object[] { "La duración total de partidas ganadas es: " + mensaje });
                        break;

                    case 13: // RESPUESTA CONSULTA 13 : LISTA DE JUGADORES DE PARTIDAS JUGADAS CON UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO 
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);
                        string resposta = "";

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador" });
                        else
                        {
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                resposta += trozos[i] + "\n";
                            }
                            Resposta_Lbl.Invoke(delegado, new object[] { resposta });
                        }
                        break;

                    case 14: // RESPUESTA CONSULTA 14 : LISTA DE PARTIDAS JUGADAS EN UN PERIODIO INTRODUCIDO POR TECLADO 
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);
                        resposta = "";

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador" });
                        else
                        {
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                resposta += trozos[i] + "\n";
                            }
                            Resposta_Lbl.Invoke(delegado, new object[] { resposta });
                        }
                        break;

                    case 15: // RESPUESTA CONSULTA 15 : RESULTADO DE PARTIDAS JUGADAS CON UN JUGADOR-NOMBRE INTRODUCIDO POR TECLADO 
                        mensaje = trozos[1];
                        delegado = new DelegadoParaEscribir(Update_Respuesta_Lbl);
                        resposta = "";

                        if (mensaje == "ERROR_DB")
                            Resposta_Lbl.Invoke(delegado, new object[] { "No hay partidas de ese jugador" });
                        else
                        {
                            for (int i = 1; i < trozos.Length; i++)
                            {
                                resposta += trozos[i] + "\n";
                            }
                            Resposta_Lbl.Invoke(delegado, new object[] { resposta });
                        }
                        break;

                    case 20: // GESTION DADOS DE LA PARTIDA
                        numPartida = Convert.ToInt16(trozos[1]);
                        int D1 = Convert.ToInt16(trozos[2]);
                        int D2 = Convert.ToInt16(trozos[3]);
                        numForm = -1;
                        found = false;

                        for (int i = 0; i < Forms.Count && !found; i++)
                        {
                            if (numPartida == Forms[i].GetPartidaNum())
                            {
                                numForm = Forms[i].GetFormNum();
                                found = true;
                            }
                        }

                        if (numForm != -1)
                        {
                            Form2 f2 = Forms[numForm];
                            f2.SetDados(D1, D2); // Actualiza los valores de los dados en el formulario
                        }
                    break;

                    case 21: // GESTION ABANDONOS DE LA PARTIDA
                        numPartida = Convert.ToInt16(trozos[1]);
                        string P1 = trozos[2];
                        string P2 = trozos[3];
                        string P3 = trozos[4];
                        string P4 = trozos[5];

                        numForm = -1;
                        found = false;

                        for (int i = 0; i < Forms.Count && !found; i++)
                        {
                            if (numPartida == Forms[i].GetPartidaNum())
                            {
                                numForm = Forms[i].GetFormNum();
                                found = true;
                            }
                        }

                        if (numForm != -1)
                        {
                            Form2 f2 = Forms[numForm];
                            f2.QuitOut(P1, P2, P3, P4); // Actualiza el formulario con los jugadores que han salido
                        }
                    break;
                }
            }
        }

        // Actualizar la respuesta del servidor recibida
        private void Update_Respuesta_Lbl(string respuesta)
        {
            Resposta_Lbl.Text = respuesta;
        }
        //----------------------------------------------------------------------------- 
        // Cierre del formulario
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string mensaje = "0/";
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                atender.Abort();
            }
            catch { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//FORM2: CORRESPONDE A LA MATRIZ DEL JUEGO

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Socket server;

        //onfiguraciones iniciales
        PictureBox tablero = new PictureBox();                    //Imagen del tablero
        private List<int> casillas = new List<int>();             //Lista casillas del tableto
        private List<int> casillasJugador = new List<int>();      //Lista casillas del jugaador
        private List<int> posiciones = new List<int>(new int[] { 0, 0, 0, 0 });// Indice de la posicion actual
       
        private float resolution = 1.0f;
        private string nickname;
        private int Player;                  //Numero de jugador (1, 2, 3 o 4)
        private int nJugador = 1;            //Numero de jugador que le toca escoger
        private string J1, J2, J3, J4;       //Nombre de los jugadores
        private int nForm;
        private int nPartida;
        private bool fiPartida = false;      //Partida terminada
        private List<string> Jugadors = new List<string>();       //Lista jugadores partida
        private float time = DateTime.Now.Hour * 60 + DateTime.Now.Minute + DateTime.Now.Second / 60; //Hora

        bool pozo1 = false, pozo2 = false, pozo3 = false, pozo4 = false; // Bandera para indicar si un jugador ha caído en un pozo
        int Turnos1 = 0, Turnos2 = 0, Turnos3 = 0, Turnos4 = 0; // Turnos de penalización de cada jugador

    

        public Form2(string nickname, int nJugador, int nForm, Socket server, string J1, string J2, string J3, string J4, int nPartida)
        {
            this.nickname = nickname;
            this.Player = nJugador;
            this.nForm = nForm;
            this.server = server;
            this.J1 = J1;
            this.J2 = J2;
            this.J3 = J3;
            this.J4 = J4;
            this.nPartida = nPartida;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Inicializar tablero
            CrearTablero();


            //Configuracion del chat
            ConfigurarChat();


            //Configuración de los jugadores
            if (J4 == "NO") //Jugador 4 no esta presente
            {
                Turnos4 = -1; 

                if (J3 == "NO") //Jugador 3 no esta presente
                {
                    Turnos3 = -1; 

                    Jugadors.Add(J1);
                    Jugadors.Add(J2);
                    Jugadors.Add("NO");
                    Jugadors.Add("NO");
                }
                else
                {
                    Jugadors.Add(J1);
                    Jugadors.Add(J2);
                    Jugadors.Add(J3);
                    Jugadors.Add("NO");
                }
            }
            else
            {
                Jugadors.Add(J1);
                Jugadors.Add(J2);
                Jugadors.Add(J3);
                Jugadors.Add(J4);
            }

            //Permite tirar los dados solo al jugador correspondiente
            if (Player == 1)
            {
                button_Dados.Enabled = true;
            }
            else
            {
                button_Dados.Enabled = false;
            }

            nJugadorLbl.Text = Convert.ToString(Player); //Muestra el número del jugador en el formulario

           
        }

        //--------------------------------------------------------------------
        //--------------------------------------------------------------  CHAT

        private void ConfigurarChat()  
        {
            ChatTable.ColumnCount = 2;
            ChatTable.Columns[0].Name = "Nombre";
            ChatTable.Columns[1].Name = "Mensaje";
            ChatTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ChatTable.AllowUserToAddRows = false;
            ChatTable.Columns[0].Width = 100;
            ChatTable.ClearSelection();
        }

        public void ActualizarChat(string nom, string text)   //Actualiza el chat 
        {
            Invoke(new Action(() =>
            {
                ChatTable.Rows.Add(nom, text);
                ChatTable.ClearSelection();
            }));
        }

        private void ChatSendBtn_Click(object sender, EventArgs e) //BOTON ENVIAR EN EL CHAT
        {
            if (!string.IsNullOrEmpty(ChatTxtBox.Text) && ChatTxtBox.Text.Length < 50)  //Si el mensaje en el chat es muy largo no se enviará correctamente
            {
                bool found = false;
                string message = ChatTxtBox.Text;
                for (int i = 0; i < ChatTxtBox.Text.Length & !found; i++)
                {
                    if (message[i] == '/')  //Evitar error en el juego por enviar el carácter '/'
                    {
                        found = true;
                        MessageBox.Show("No se puede usar el caracter '/'");
                    }
                }

                if (!found)
                {
                    string mensaje = "7/" + nPartida.ToString() + "/" + nickname + "/" + ChatTxtBox.Text;
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    ChatTxtBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("Texto vacio o demasiado largo");
            }
        }

        private void ChatTxtBox_KeyPress(object sender, KeyPressEventArgs e) //PULSAR ENTER EN EL CHAT
        {
            if (e.KeyChar == '\r')
            {
                if (!string.IsNullOrEmpty(ChatTxtBox.Text) && ChatTxtBox.Text.Length < 50)  //Si el mensaje en el chat es muy largo no se enviará correctamente
                {
                    bool found = false;
                    string message = ChatTxtBox.Text;
                    for (int i = 0; i < ChatTxtBox.Text.Length & !found; i++)
                    {
                        if (message[i] == '/')  //Evitar error en el juego por enviar el carácter '/'
                        {
                            found = true;
                            MessageBox.Show("No se puede usar el caracter '/'");
                        }
                    }

                    if (!found)
                    {
                        string mensaje = "7/" + nPartida.ToString() + "/" + nickname + "/" + ChatTxtBox.Text;
                        byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        ChatTxtBox.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Texto vacio o demasiado largo");
                }
            }
        }

        //--------------------------------------------------------------
   


        // Devuelve el numero de la partida
        public int GetPartidaNum()
        {
            return nPartida;
        }
       
        // Devuelve el numero del formulario
        public int GetFormNum()
        {
            return nForm;
        }
       
        // Metodo para finalizar la partida 
        void FiPartida(int n)
        {
            fiPartida = true;

            if (n == Player)
            {
                string guanyador = ""; //Determina quien ha ganado

                switch (n)
                {
                    case 1:
                        guanyador = J1;
                        break;
                    case 2:
                        guanyador = J2;
                        break;
                    case 3:
                        guanyador = J3;
                        break;
                    case 4:
                        guanyador = J4;
                        break;
                }
                //Guarda la duracion de la partida
                int Year = DateTime.Now.Year;
                int Month = DateTime.Now.Month;
                int Day = DateTime.Now.Day;
                int Hour = DateTime.Now.Hour;
                int Minute = DateTime.Now.Minute;
                int Second = DateTime.Now.Second;
                float duration = (Hour * 60 + Minute + Second / 60) - (time); //En minutos

                string fecha = Day + "-" + Month + "-" + Year;
                string hora = Hour + ":" + Minute;

                string mensaje = "22/" + fecha + "/" + hora + "/" + duration.ToString() + "/" + guanyador + "/"
                                 + Jugadors[0] + "/" + Jugadors[1] + "/" + Jugadors[2] + "/" + Jugadors[3];
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
               
                //Partida finalizada
                mensaje = "23/";
                msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            Close();
        }

        //Metodo para tirar los dados
        private void button_Dados_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            // Genera dos número aleatorio entre 1 y 6
            int resultado = random.Next(1, 7);
            int resultado2 = random.Next(1, 7);
            button_Dados.Enabled = false;

            string mensaje = "20/" + nPartida.ToString() + "/" + resultado.ToString() + "/"
                             + resultado2.ToString() + "/" + J1 + "/" + J2 + "/" + J3 + "/" + J4;
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        //Establece los resultados de los dados
        public void SetDados(int D1, int D2)
        {
            Invoke(new Action(() =>
            {
                MoverFicha(D1, D2);//mueve fichas en el tablero segun el resultado de los dados
            }));
        }

        //Metodo actualiza lista de jugadores en caso de abandono
        public void QuitOut(string J1, string J2, string J3, string J4)
        {
            Invoke(new Action(() =>
            {
                UpdatePlayers(J1, J2, J3, J4);
            }));
        }
        //Metodo actualiza la lista jugadores presentes en la partida
        private void UpdatePlayers(string J1, string J2, string J3, string J4)
        {
            this.J1 = J1;
            this.J2 = J2;
            this.J3 = J3;
            this.J4 = J4;
            int c = 0;

            if (J1 == "NO")
            {
                c++;
                Turnos1 = -1; 
                if (nJugador == 1)
                {
                    SetDados(0, 0);
                }
            }

            if (J2 == "NO")
            {
                c++;
                Turnos2 = -1;
                if (nJugador == 2)
                {
                    SetDados(0, 0);
                }
            }

            if (J3 == "NO")
            {
                c++;
                Turnos3 = -1;
                if (nJugador == 3)
                {
                    SetDados(0, 0);
                }
            }

            if (J4 == "NO")
            {
                c++;
                Turnos4 = -1;
                if (nJugador == 4)
                {
                    SetDados(0, 0);
                }
            }


            if (c == 3) //Jugador actual gana si todos los demas jugadores abandonaron
            {
              
                MessageBox.Show($"Has ganado jugador {Player} porque todos han abandonado");
                FiPartida(Player);
            }
        }

        //--------------------------------------------------------------
        //--------------------------------------------------------------  TABLERO DEL JUEGO
        private void CrearTablero()
        {
            //Configuracion del tablero
            tablero.ClientSize = new Size((int)(resolution * 1000), (int)(resolution * 1000));
            tablero.Location = new Point(0, 0);
            tablero.SizeMode = PictureBoxSizeMode.StretchImage;
            tablero.Image = Image.FromFile("Oca.jpg");
            panel1.Controls.Add(tablero);

            //Definición de las casillas del tablero (coordenadas en un tablero de 1000x1000 píxeles)
            casillas = new List<int>(new int[] {   50, 50, 355, 438, 525, 606, 695, 777, 858,
                                            855, 775, 697, 615, 530, 443, 357, 275, 198, 110,
                                            850, 775, 698, 615, 530, 443, 356, 270, 190, 111,
                                            114, 195, 275, 357, 444, 530, 612, 688,
                                            110, 195, 270, 355, 438, 525, 606, 687,
                                            687, 615, 531, 444, 369, 279,
                                            685, 615, 530, 444, 365, 278,
                                            276, 355, 444, 524,
                                            275, 350, 500 });   

            //Ajustar escala de las casillas de acuerdo con la resolucion 
            for (int i = 0; i < casillas.Count; i++)
            {
                casillas[i] = (int)(casillas[i] * resolution);
            }
            //Coordenadas para las posiciones de los jugadores
            casillasJugador = new List<int>(new int[] { 920, 963, 960, 10, 2, 795, 792, 173, 170, 627 });

            for (int i = 0; i < casillasJugador.Count; i++)
            {
                casillasJugador[i] = (int)(casillasJugador[i] * resolution);
            }

            tablero.Paint += new PaintEventHandler(tablero_Paint);  //Funció per pintar en el tablero
        }

        //Evento para pintar las fichas en el tablero
        private void tablero_Paint(object sender, PaintEventArgs e)
        {
            float res = resolution;
            /*    int[] coord1 = new int[2];
                int[] coord2 = new int[2];
                int[] coord3 = new int[2];
                int[] coord4 = new int[2];

                coord1 = GetCoordinates(casillas, casillasJugador, 1);
                coord2 = GetCoordinates(casillas, casillasJugador, 2);
                coord3 = GetCoordinates(casillas, casillasJugador, 3);
                coord4 = GetCoordinates(casillas, casillasJugador, 4);*/
          
            int[] coord1 = GetCoordinates(casillas, casillasJugador, 1);
            int[] coord2 = GetCoordinates(casillas, casillasJugador, 2);
            int[] coord3 = GetCoordinates(casillas, casillasJugador, 3);
            int[] coord4 = GetCoordinates(casillas, casillasJugador, 4);


            Graphics g = e.Graphics;

            //Crear rectangulos para las fichas
            RectangleF pieza1 = new RectangleF(coord1[0], coord1[1], (int)(35 * res), (int)(35 * res));
            RectangleF pieza2 = new RectangleF(coord2[0], coord2[1], (int)(35 * res), (int)(35 * res));
            RectangleF pieza3 = new RectangleF(coord3[0], coord3[1], (int)(35 * res), (int)(35 * res));
            RectangleF pieza4 = new RectangleF(coord4[0], coord4[1], (int)(35 * res), (int)(35 * res));

            //Crear circulo de colores para las fichas
            SolidBrush myBrush1 = new SolidBrush(Color.Red); 
            SolidBrush myBrush2 = new SolidBrush(Color.Blue);
            SolidBrush myBrush3 = new SolidBrush(Color.Green);
            SolidBrush myBrush4 = new SolidBrush(Color.Yellow);

            g.FillEllipse(myBrush1, pieza1);
            g.FillEllipse(myBrush2, pieza2);
            g.FillEllipse(myBrush3, pieza3);
            g.FillEllipse(myBrush4, pieza4);

            //Crear controno negro para los bordes de las fichas
            Pen myPen = new Pen(Color.Black);   
            g.DrawEllipse(myPen, pieza1);
            g.DrawEllipse(myPen, pieza2);
            g.DrawEllipse(myPen, pieza3);
            g.DrawEllipse(myPen, pieza4);

            myBrush1.Dispose();
            myBrush2.Dispose();
            myBrush3.Dispose();
            myBrush4.Dispose();
            myPen.Dispose();
        }

        //Devuelve las coordenadas de las fichas segun la posicion del jugador
        private int[] GetCoordinates(List<int> cas, List<int> casJugador, int nJugador)    
        {
            int pos = posiciones[nJugador - 1];
            float res = resolution;
            int[] coords = new int[2];   //Coordenadas (x,y) de las fichas
            int separacion = (int)(35 * res * (nJugador - 1));

            if (pos == 0)
            {
                coords[0] = cas[pos] * nJugador;
                coords[1] = casJugador[0];
            }
            else if (pos >= 1 && pos <= 7)
            {
                coords[0] = cas[pos];
                coords[1] = casJugador[1] - separacion;
            }
            else if (pos == 8)
            {
                coords[0] = (int)(cas[pos] - 10 * res * (nJugador - 1));
                coords[1] = casJugador[1] - separacion;
            }
            else if (pos == 9)
            {
                coords[0] = casJugador[2] - separacion;
                coords[1] = (int)(cas[pos] - 10 * res * (nJugador - 1));
            }
            else if (pos >= 10 && pos <= 17)
            {
                coords[0] = casJugador[2] - separacion;
                coords[1] = cas[pos];
            }
            else if (pos == 18)
            {
                coords[0] = casJugador[2] - separacion;
                coords[1] = (int)(cas[pos] + 10 * res * (nJugador - 1));
            }
            else if (pos == 19)
            {
                coords[0] = (int)(cas[pos] - 10 * res * (nJugador - 1));
                coords[1] = casJugador[3] + separacion;
            }
            else if (pos >= 20 && pos <= 27)
            {
                coords[0] = cas[pos];
                coords[1] = casJugador[3] + separacion;
            }
            else if (pos == 28)
            {
                coords[0] = (int)(cas[pos] + 10 * res * (nJugador - 1));
                coords[1] = casJugador[3] + separacion;
            }
            else if (pos == 29)
            {
                coords[0] = casJugador[4] + separacion;
                coords[1] = (int)(cas[pos] + 10 * res * (nJugador - 1));
            }
            else if (pos >= 30 && pos <= 35)
            {
                coords[0] = casJugador[4] + separacion;
                coords[1] = cas[pos];
            }
            else if (pos == 36)
            {
                coords[0] = casJugador[4] + separacion;
                coords[1] = (int)(cas[pos] - 10 * res * (nJugador - 1));
            }
            else if (pos == 37)
            {
                coords[0] = (int)(cas[pos] + 10 * res * (nJugador - 1));
                coords[1] = casJugador[5] - separacion;
            }
            else if (pos >= 38 && pos <= 43)
            {
                coords[0] = cas[pos];
                coords[1] = casJugador[5] - separacion;
            }
            else if (pos == 44)
            {
                coords[0] = (int)(cas[pos] - 10 * res * (nJugador - 1));
                coords[1] = casJugador[5] - separacion;
            }
            else if (pos == 45)
            {
                coords[0] = casJugador[6] - separacion;
                coords[1] = (int)(cas[pos] - 10 * res * (nJugador - 1));
            }
            else if (pos >= 46 && pos <= 49)
            {
                coords[0] = casJugador[6] - separacion;
                coords[1] = cas[pos];
            }
            else if (pos == 50)
            {
                coords[0] = casJugador[6] - separacion;
                coords[1] = (int)(cas[pos] + 10 * res * (nJugador - 1));
            }
            else if (pos == 51)
            {
                coords[0] = (int)(cas[pos] - 10 * res * (nJugador - 1));
                coords[1] = casJugador[7] + separacion;
            }
            else if (pos >= 52 && pos <= 55)
            {
                coords[0] = cas[pos];
                coords[1] = casJugador[7] + separacion;
            }
            else if (pos == 56)
            {
                coords[0] = (int)(cas[pos] + 10 * res * (nJugador - 1));
                coords[1] = casJugador[7] + separacion;
            }
            else if (pos == 57)
            {
                coords[0] = casJugador[8] + separacion;
                coords[1] = (int)(cas[pos] + 10 * res * (nJugador - 1));
            }
            else if (pos >= 58 && pos <= 59)
            {
                coords[0] = casJugador[8] + separacion;
                coords[1] = cas[pos];
            }
            else if (pos == 60)
            {
                coords[0] = casJugador[8] + separacion;
                coords[1] = (int)(cas[pos] - 10 * res * (nJugador - 1));
            }
            else if (pos == 61)
            {
                coords[0] = (int)(cas[pos] + 10 * res * (nJugador - 1));
                coords[1] = casJugador[9] - separacion;
            }
            else if (pos >= 62)
            {
                coords[0] = cas[pos];
                coords[1] = casJugador[9] - separacion;
            }

            return coords;
        }

        //Cambiar la resolucion del tablero segun la pantalla del dispositivo
        private void Resolution_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Resolution_Box.SelectedIndex)
            {
                case 0:     //1000x1000
                    resolution = 1.0f;
                    break;
                    
                case 1:     //900x900
                    resolution = 0.9f;
                    break;

                case 2:     //800x800
                    resolution = 0.8f;
                    break;

                case 3:     //700x700
                    resolution = 0.7f;
                    break;

                case 4:     //600x600
                    resolution = 0.6f;
                    break;

                case 5:     //500x500
                    resolution = 0.5f;
                    break;

                case 6:     //400x400
                    resolution = 0.4f;
                    break;

                case 7:     //300x300
                    resolution = 0.3f;
                    break;
            }

            CrearTablero();
        }
        //----------------------------------------------------------------------------------
        //-------------------------------------------------------------- MOVIMIENTOS JUGADOR
        
        private void MoverFicha(int D1, int D2)
        {
            //Variables iniciales y de estado
            bool online = true;
            bool volverATirar = false;
            int pierdeTurno = 0;
            bool pozo = false;
            bool saltar = true;

            //Mostrar valores dados
            Resultado.Text = D1.ToString();
            Resultado2.Text = D2.ToString();

            //Calculo suma de los dos dados
            int suma = D1 + D2;
            SumaResultado.Text = suma.ToString();

            //Determinar que jugadores estan invitados
            if (radioButton1.Checked) nJugador = 1;
            else if (radioButton2.Checked) nJugador = 2;
            else if (radioButton3.Checked) nJugador = 3;
            else nJugador = 4;

            //Verificar si el jugador esta conectado
           
            /* if (nJugador == 1 && Turnos1 < 0)
                 online = false;
             else if (nJugador == 2 && Turnos2 < 0)
                 online = false;
             else if (nJugador == 3 && Turnos3 < 0)
                 online = false;
             else if (nJugador == 4 && Turnos4 < 0)
                 online = false;*/

            if ((nJugador == 1 && Turnos1 < 0) ||
                 (nJugador == 2 && Turnos2 < 0) ||
                 (nJugador == 3 && Turnos3 < 0) ||
                 (nJugador == 4 && Turnos4 < 0))
            {
                online = false;
            }


            if (online) //Procede si esta conectado
            {
                //Si un jugador pasa la casilla 31 
                if (posiciones[nJugador - 1] < 31 && (posiciones[nJugador - 1] + suma) >= 31)
                {
                    pozo1 = pozo2 = pozo3 = pozo4 = false;
                }
                
                //Avanzar 
                if (posiciones[nJugador - 1] == 0)
                {
                    posiciones[nJugador - 1] += 1;
                }
                posiciones[nJugador - 1] += suma;

                //Retroceder cuando llega y se pasa de la última casilla
                if (posiciones[nJugador - 1] > 63)
                {
                    posiciones[nJugador - 1] = 2 * 63 - posiciones[nJugador - 1];
                }

                //Si un jugador cae en casilla 26 o casilla 53
                while (posiciones[nJugador - 1] == 26 || posiciones[nJugador - 1] == 53)
                {
                    if (posiciones[nJugador - 1] == 26)         //Posición especial: Dados
                    {
                        posiciones[nJugador - 1] = posiciones[nJugador - 1] + 26 + suma;
                    }

                    else if (posiciones[nJugador - 1] == 53)    //Posición especial: Dados
                    {
                        posiciones[nJugador - 1] = posiciones[nJugador - 1] + 53 + suma;
                    }

                    if (posiciones[nJugador - 1] > 63)  //Retroceder cuando llega a la última casilla
                    {
                        posiciones[nJugador - 1] = 2 * 63 - posiciones[nJugador - 1];
                    }
                }

                //Posiciones especiales 
                switch (posiciones[nJugador - 1])
                {
                    case 5:     //De oca a oca
                        posiciones[nJugador - 1] = 9;
                        volverATirar = true;
                        break;
                    case 9:     //De oca a oca
                        posiciones[nJugador - 1] = 14;
                        volverATirar = true;
                        break;
                    case 14:     //De oca a oca
                        posiciones[nJugador - 1] = 18;
                        volverATirar = true;
                        break;
                    case 18:     //De oca a oca
                        posiciones[nJugador - 1] = 23;
                        volverATirar = true;
                        break;
                    case 23:     //De oca a oca
                        posiciones[nJugador - 1] = 27;
                        volverATirar = true;
                        break;
                    case 27:     //De oca a oca
                        posiciones[nJugador - 1] = 32;
                        volverATirar = true;
                        break;
                    case 32:     //De oca a oca
                        posiciones[nJugador - 1] = 36;
                        volverATirar = true;
                        break;
                    case 36:     //De oca a oca
                        posiciones[nJugador - 1] = 41;
                        volverATirar = true;
                        break;
                    case 41:     //De oca a oca
                        posiciones[nJugador - 1] = 45;
                        volverATirar = true;
                        break;
                    case 45:     //De oca a oca
                        posiciones[nJugador - 1] = 50;
                        volverATirar = true;
                        break;
                    case 50:     //De oca a oca
                        posiciones[nJugador - 1] = 54;
                        volverATirar = true;
                        break;
                    case 54:     //De oca a oca
                        posiciones[nJugador - 1] = 59;
                        volverATirar = true;
                        break;
                    case 59:     //De oca a oca
                        posiciones[nJugador - 1] = 63;
                        volverATirar = true;
                        break;
                    case 6:     //Puente a puente
                        posiciones[nJugador - 1] = 19;
                        pierdeTurno = 1;
                        break;
                    case 12:    //Puente a puente
                        posiciones[nJugador - 1] = 19;
                        pierdeTurno = 1;
                        break;
                    case 19:    //Posada
                        pierdeTurno = 1;
                        break;
                    case 31:    //Pozo
                        pozo = true;
                        break;
                    case 42:    //Laberinto
                        posiciones[nJugador - 1] = 30;
                        break;
                    case 52:    //Carcel
                        pierdeTurno = 2;
                        break;
                    case 58:    //Muerte
                        posiciones[nJugador - 1] = 0;
                        break;
                }


                // Perder turno
                if (pierdeTurno > 0)
                {
                    if (nJugador == 1) Turnos1 = pierdeTurno;
                    else if (nJugador == 2) Turnos2 = pierdeTurno;
                    else if (nJugador == 3) Turnos3 = pierdeTurno;
                    else Turnos4 = pierdeTurno;
                    
                }

                // Actualizar el estado del pozo
                if (pozo)
                {
                    if (nJugador == 1) pozo1 = true;
                    else if (nJugador == 2) pozo2 = true;
                    else if (nJugador == 3) pozo3 = true;
                    else pozo4 = true;
                }

                // Redibujar tablero
                tablero.Invalidate();
            }
            //Volver a tirar o pasar el turno
            if (volverATirar)
            {
                if (nJugador == 1)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }

                else if (nJugador == 2)
                {
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                }

                else if (nJugador == 3)
                {
                    radioButton3.Checked = true;
                    radioButton4.Checked = false;
                }

                else
                {
                    radioButton4.Checked = true;
                    radioButton1.Checked = false;
                }
            }
            else
            {
                // Declarar ganador si llega a la meta
                if (posiciones[nJugador - 1] == 63 && online)
                {
                    MessageBox.Show("Ha ganado el jugador " + Convert.ToString(nJugador));
                    FiPartida(nJugador);
                }
                // Pasar el turno a otros jugadores
                while (saltar)
                {
                    switch (nJugador)
                    {
                        case 1:
                            if (pozo2)
                            {
                                nJugador = 2;
                            }
                            else if (Turnos2 != 0)
                            {
                                nJugador = 2;
                                Turnos2--;
                            }
                            else
                            {
                                saltar = false;
                                radioButton2.Checked = true;
                            }
                            break;
                        case 2:
                            if (pozo3)
                            {
                                nJugador = 3;
                            }
                            else if (Turnos3 != 0)
                            {
                                nJugador = 3;
                                Turnos3--;
                            }
                            else
                            {
                                saltar = false;
                                radioButton3.Checked = true;
                            }
                            break;
                        case 3:
                            if (pozo4)
                            {
                                nJugador = 4;
                            }
                            else if (Turnos4 != 0)
                            {
                                nJugador = 4;
                                Turnos4--;
                            }
                            else
                            {
                                saltar = false;
                                radioButton4.Checked = true;
                            }
                            break;
                        case 4:
                            if (pozo1)
                            {
                                nJugador = 1;
                            }
                            else if (Turnos1 != 0)
                            {
                                nJugador = 1;
                                Turnos1--;
                            }
                            else
                            {
                                saltar = false;
                                radioButton1.Checked = true;
                            }
                            break;
                    }
                }
            }
            //Habilitar dados para el jugador actual
            if (radioButton1.Checked && Player == 1)
            {
                button_Dados.Enabled = true;
            }
            else if (radioButton2.Checked && Player == 2)
            {
                button_Dados.Enabled = true;
            }
            else if (radioButton3.Checked && Player == 3)
            {
                button_Dados.Enabled = true;
            }
            else if (radioButton4.Checked && Player == 4)
            {
                button_Dados.Enabled = true;
            }
        }

        //---------------------------------------------------------------------------------------
        //--------------------------------------------------------------  VENTANA DE INSTRUCCIONES
        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            Form ayuda = new Form4();
            ayuda.Show();
        }

        // Cierre del formulario
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!fiPartida)
            {
                switch (Player)
                {
                    case 1: J1 = "NO";
                        break;
                    case 2: J2 = "NO";
                        break;
                    case 3: J3 = "NO";
                        break;
                    case 4: J4 = "NO";
                        break;
                }

                string mensaje = $"21/{nPartida}/{J1}/{J2}/{J3}/{J4}";
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
      
    }
}
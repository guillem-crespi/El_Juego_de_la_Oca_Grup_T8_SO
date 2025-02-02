﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Text;

//FORM3: CORRESPONDE AL FORMULARIO INICIAL, DONDE SE PUEDE INICAR SESIÓN Y REGISTRARSE,
//DESPUES DE INICIAR SESIÓN SE CIERRA ESTE FORM Y SE ABRE FORM1 QUE CORRESPONDE AL FORMULARIO PRINCIPAL

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        //Configuraciones iniciales
        Socket server;                   //Conexion al servidor
        string mensaje;
    
        public Form3()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------
        //----------------------------------------------------------------------------- BOTON INICIAR SESION (CONSULTA 1)
        private void button_LogIn_Click(object sender, EventArgs e) 
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("10.4.119.5");
            IPEndPoint ipep = new IPEndPoint(direc, 50089);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); //Intentamos conectar el socket
                BackColor = Color.PaleGreen; 
            }
            catch (SocketException)
            {
                //Si hay excepcion, imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            // Guarda los datos del usuario en un string 
            mensaje = "1/" + nickname.Text + "/" + password.Text; 
            
            // Enviamos al servidor el mensaje introducido por teclado
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor                    
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            //Respuestas del servidor
            if (mensaje == "1/LOGIN_SUCCESSFUL")
            {
                // El servidor confirma que el inicio de sesion fue exitoso 
                // Cerramos la ventana actual y abrimos FORM1 que corresponde a la ventana principal
                MessageBox.Show("Bienvenido/a " + nickname.Text + ". Has iniciado sesión correctamente.");
                Form f = new Form1(nickname.Text, password.Text, server);
                Hide();
                f.ShowDialog();
                BackColor = Color.Gray;
                Show();
            }
            else
            {
                if (mensaje == "1/NO_USER") 
                {
                    MessageBox.Show("No estás registrado. Para registrarte, rellena los campos y presiona 'Registrar'.");
                }
                else if (mensaje == "1/WRONG_PASSWORD") 
                {
                    MessageBox.Show("Contraseña incorrecta. Por favor, inténtalo de nuevo.");
                }
                else if (mensaje == "1/ALREADY_IN") 
                {
                    MessageBox.Show("Jugador ya conectado. Prueba con otro usuario");
                }
                else
                {
                    MessageBox.Show("Error desconocido. Por favor, intenta de nuevo más tarde.");
                }
                //Mensaje de desconexion
                mensaje = "0/";
                msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
        }
        //-----------------------------------------------------------------------------
        //-----------------------------------------------------------------------------  BOTON REGISTRO (CONSULTA 2)
        private void button_Registro_Click(object sender, EventArgs e)
        {
            byte[] msg;
            ////Creamos un IPEndPoint con el ip del servidor y puerto del servidor al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("10.4.119.5");
            IPEndPoint ipep = new IPEndPoint(direc, 50089);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                BackColor = Color.Green;
            }
            catch (SocketException)
            {
                //Si hay excepcion, imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            if ((password.Text.Length > 3) && (nickname.Text.Length > 3))
            {
                // Verificamos que la confirmación de la contraseña coincida con la contraseña 
                if (password.Text == password_conf.Text)
                {
                    // Guarda los datos del usuario en un string 
                    mensaje = "2/" + nickname.Text + "/" + password.Text;

                    // Enviamos al servidor el mensaje introducido por teclado
                    msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor                    
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    if (mensaje == "2/0")
                    {
                        MessageBox.Show("Bienvenido/a " + nickname.Text + ". Te has registrado correctamente.");
                    }
                    else if (mensaje == "2/-1")
                    {
                        MessageBox.Show("Fallo al registrar, intentelo de nuevo.");
                    }
                    else
                    {
                        MessageBox.Show("Error desconocido. Por favor, inténtelo más tarde.");
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña de confirmación no coincide.");
                }
            }
            else
            {
                MessageBox.Show("El nombre de usuario y la contraseña deben tener más de 3 caracteres.");
            }

            //Mensaje de desconexion
            mensaje = "0/";
            msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}

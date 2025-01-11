namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConsultaFecha = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_MatrizJuego = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ConsultaNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DimeGanadores = new System.Windows.Forms.RadioButton();
            this.ListaCon = new System.Windows.Forms.Button();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password_conf = new System.Windows.Forms.TextBox();
            this.button_Registro = new System.Windows.Forms.Button();
            this.SumaDuracion = new System.Windows.Forms.RadioButton();
            this.button_LogIn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Nick = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.nickname = new System.Windows.Forms.TextBox();
            this.DimeJugadores = new System.Windows.Forms.RadioButton();
            this.button_Conectar = new System.Windows.Forms.Button();
            this.button_Desconectar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // ConsultaFecha
            // 
            this.ConsultaFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsultaFecha.ForeColor = System.Drawing.Color.Black;
            this.ConsultaFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsultaFecha.Location = new System.Drawing.Point(817, 432);
            this.ConsultaFecha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsultaFecha.Name = "ConsultaFecha";
            this.ConsultaFecha.Size = new System.Drawing.Size(246, 40);
            this.ConsultaFecha.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(504, 626);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 60);
            this.button2.TabIndex = 5;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.button_MatrizJuego);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ConsultaNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DimeGanadores);
            this.groupBox1.Controls.Add(this.ListaCon);
            this.groupBox1.Controls.Add(this.ListaConectados);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.password_conf);
            this.groupBox1.Controls.Add(this.button_Registro);
            this.groupBox1.Controls.Add(this.SumaDuracion);
            this.groupBox1.Controls.Add(this.button_LogIn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Nick);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.nickname);
            this.groupBox1.Controls.Add(this.DimeJugadores);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.ConsultaFecha);
            this.groupBox1.Location = new System.Drawing.Point(18, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1886, 778);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // button_MatrizJuego
            // 
            this.button_MatrizJuego.Location = new System.Drawing.Point(585, 178);
            this.button_MatrizJuego.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_MatrizJuego.Name = "button_MatrizJuego";
            this.button_MatrizJuego.Size = new System.Drawing.Size(158, 52);
            this.button_MatrizJuego.TabIndex = 39;
            this.button_MatrizJuego.Text = "Empezar a jugar";
            this.button_MatrizJuego.UseVisualStyleBackColor = true;
            this.button_MatrizJuego.Click += new System.EventHandler(this.button_MatrizJuego_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(857, 585);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 22);
            this.label6.TabIndex = 37;
            this.label6.Text = "Introduce una nombre";
            // 
            // ConsultaNombre
            // 
            this.ConsultaNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsultaNombre.ForeColor = System.Drawing.Color.Black;
            this.ConsultaNombre.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsultaNombre.Location = new System.Drawing.Point(817, 526);
            this.ConsultaNombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsultaNombre.Name = "ConsultaNombre";
            this.ConsultaNombre.Size = new System.Drawing.Size(246, 40);
            this.ConsultaNombre.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(857, 478);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 22);
            this.label2.TabIndex = 35;
            this.label2.Text = "Introduce una fecha";
            // 
            // DimeGanadores
            // 
            this.DimeGanadores.AutoSize = true;
            this.DimeGanadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimeGanadores.Location = new System.Drawing.Point(84, 464);
            this.DimeGanadores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DimeGanadores.Name = "DimeGanadores";
            this.DimeGanadores.Size = new System.Drawing.Size(627, 33);
            this.DimeGanadores.TabIndex = 34;
            this.DimeGanadores.TabStop = true;
            this.DimeGanadores.Text = "Dime que ganadores jugaron este día (introduce fecha)";
            this.DimeGanadores.UseVisualStyleBackColor = true;
            // 
            // ListaCon
            // 
            this.ListaCon.Location = new System.Drawing.Point(1284, 386);
            this.ListaCon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListaCon.Name = "ListaCon";
            this.ListaCon.Size = new System.Drawing.Size(294, 58);
            this.ListaCon.TabIndex = 33;
            this.ListaCon.Text = "Usuarios Conectados";
            this.ListaCon.UseVisualStyleBackColor = true;
            this.ListaCon.Click += new System.EventHandler(this.ListaCon_Click);
            // 
            // ListaConectados
            // 
            this.ListaConectados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaConectados.Location = new System.Drawing.Point(1188, 89);
            this.ListaConectados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.RowHeadersVisible = false;
            this.ListaConectados.RowHeadersWidth = 62;
            this.ListaConectados.Size = new System.Drawing.Size(510, 262);
            this.ListaConectados.TabIndex = 32;
            this.ListaConectados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListaConectados_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(839, 499);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 22);
            this.label5.TabIndex = 31;
            this.label5.Text = "Formato: (DD-MM-AAAA)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "(solo para registrarse)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 142);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "Confirmacion de Contraseña:";
            // 
            // password_conf
            // 
            this.password_conf.Location = new System.Drawing.Point(264, 138);
            this.password_conf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.password_conf.Name = "password_conf";
            this.password_conf.Size = new System.Drawing.Size(188, 26);
            this.password_conf.TabIndex = 28;
            // 
            // button_Registro
            // 
            this.button_Registro.Location = new System.Drawing.Point(585, 115);
            this.button_Registro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Registro.Name = "button_Registro";
            this.button_Registro.Size = new System.Drawing.Size(144, 52);
            this.button_Registro.TabIndex = 20;
            this.button_Registro.Text = "Registrarse";
            this.button_Registro.UseVisualStyleBackColor = true;
            this.button_Registro.Click += new System.EventHandler(this.button_Registro_Click);
            // 
            // SumaDuracion
            // 
            this.SumaDuracion.AutoSize = true;
            this.SumaDuracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SumaDuracion.Location = new System.Drawing.Point(84, 494);
            this.SumaDuracion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SumaDuracion.Name = "SumaDuracion";
            this.SumaDuracion.Size = new System.Drawing.Size(586, 33);
            this.SumaDuracion.TabIndex = 18;
            this.SumaDuracion.TabStop = true;
            this.SumaDuracion.Text = "Duración total partidas ganadas (introduce nombre)";
            this.SumaDuracion.UseVisualStyleBackColor = true;
            // 
            // button_LogIn
            // 
            this.button_LogIn.Location = new System.Drawing.Point(585, 48);
            this.button_LogIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_LogIn.Name = "button_LogIn";
            this.button_LogIn.Size = new System.Drawing.Size(144, 52);
            this.button_LogIn.TabIndex = 17;
            this.button_LogIn.Text = "Log In";
            this.button_LogIn.UseVisualStyleBackColor = true;
            this.button_LogIn.Click += new System.EventHandler(this.button_LogIn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Contraseña:";
            // 
            // Nick
            // 
            this.Nick.AutoSize = true;
            this.Nick.Location = new System.Drawing.Point(80, 42);
            this.Nick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Nick.Name = "Nick";
            this.Nick.Size = new System.Drawing.Size(133, 20);
            this.Nick.TabIndex = 15;
            this.Nick.Text = "Nombre_Usuario:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(266, 89);
            this.password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(187, 26);
            this.password.TabIndex = 14;
            // 
            // nickname
            // 
            this.nickname.Location = new System.Drawing.Point(264, 38);
            this.nickname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nickname.Name = "nickname";
            this.nickname.Size = new System.Drawing.Size(188, 26);
            this.nickname.TabIndex = 13;
            // 
            // DimeJugadores
            // 
            this.DimeJugadores.AutoSize = true;
            this.DimeJugadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimeJugadores.Location = new System.Drawing.Point(84, 432);
            this.DimeJugadores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DimeJugadores.Name = "DimeJugadores";
            this.DimeJugadores.Size = new System.Drawing.Size(620, 33);
            this.DimeJugadores.TabIndex = 8;
            this.DimeJugadores.TabStop = true;
            this.DimeJugadores.Text = "Dime que jugadores jugaron este día (introduce fecha)";
            this.DimeJugadores.UseVisualStyleBackColor = true;
            // 
            // button_Conectar
            // 
            this.button_Conectar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_Conectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Conectar.Location = new System.Drawing.Point(18, 18);
            this.button_Conectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Conectar.Name = "button_Conectar";
            this.button_Conectar.Size = new System.Drawing.Size(240, 58);
            this.button_Conectar.TabIndex = 10;
            this.button_Conectar.Text = "Conectar";
            this.button_Conectar.UseVisualStyleBackColor = false;
            this.button_Conectar.Click += new System.EventHandler(this.button_Conectar_Click);
            // 
            // button_Desconectar
            // 
            this.button_Desconectar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Desconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Desconectar.Location = new System.Drawing.Point(330, 18);
            this.button_Desconectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Desconectar.Name = "button_Desconectar";
            this.button_Desconectar.Size = new System.Drawing.Size(238, 58);
            this.button_Desconectar.TabIndex = 11;
            this.button_Desconectar.Text = "Desconectar";
            this.button_Desconectar.UseVisualStyleBackColor = false;
            this.button_Desconectar.Click += new System.EventHandler(this.button_Desconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1058);
            this.Controls.Add(this.button_Desconectar);
            this.Controls.Add(this.button_Conectar);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox ConsultaFecha;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton DimeJugadores;
        private System.Windows.Forms.Button button_LogIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Nick;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox nickname;
        private System.Windows.Forms.RadioButton SumaDuracion;
        private System.Windows.Forms.Button button_Conectar;
        private System.Windows.Forms.Button button_Desconectar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password_conf;
        private System.Windows.Forms.Button button_Registro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.Button ListaCon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DimeGanadores;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ConsultaNombre;
        private System.Windows.Forms.Button button_MatrizJuego;
    }
}


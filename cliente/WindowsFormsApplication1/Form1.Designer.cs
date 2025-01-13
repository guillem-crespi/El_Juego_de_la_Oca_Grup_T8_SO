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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ConsultaFecha = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ConsultaPeriodo = new System.Windows.Forms.TextBox();
            this.PartidasDia = new System.Windows.Forms.RadioButton();
            this.ResultadoParitdas = new System.Windows.Forms.RadioButton();
            this.ListadoJugadores = new System.Windows.Forms.RadioButton();
            this.Resposta_Lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DimeJugadores = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConsultaNombre = new System.Windows.Forms.TextBox();
            this.DimeGanadores = new System.Windows.Forms.RadioButton();
            this.SumaDuracion = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.button_Baja = new System.Windows.Forms.Button();
            this.button_Desconectar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // ConsultaFecha
            // 
            this.ConsultaFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsultaFecha.ForeColor = System.Drawing.Color.Black;
            this.ConsultaFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsultaFecha.Location = new System.Drawing.Point(20, 167);
            this.ConsultaFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConsultaFecha.Name = "ConsultaFecha";
            this.ConsultaFecha.Size = new System.Drawing.Size(219, 34);
            this.ConsultaFecha.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Thistle;
            this.button2.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(27, 356);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 47);
            this.button2.TabIndex = 5;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.ListaConectados);
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1068, 805);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ConsultaPeriodo);
            this.groupBox2.Controls.Add(this.PartidasDia);
            this.groupBox2.Controls.Add(this.ResultadoParitdas);
            this.groupBox2.Controls.Add(this.ListadoJugadores);
            this.groupBox2.Controls.Add(this.Resposta_Lbl);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ConsultaFecha);
            this.groupBox2.Controls.Add(this.DimeJugadores);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ConsultaNombre);
            this.groupBox2.Controls.Add(this.DimeGanadores);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.SumaDuracion);
            this.groupBox2.Location = new System.Drawing.Point(31, 326);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(993, 473);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Consultas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(543, 266);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 17);
            this.label4.TabIndex = 50;
            this.label4.Text = "Formato: (Dia-Mes-Año)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(543, 207);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 51);
            this.label3.TabIndex = 49;
            this.label3.Text = "Introduce otra fecha\r\npara tener un periodo\r\n   (fecha final)";
            // 
            // ConsultaPeriodo
            // 
            this.ConsultaPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsultaPeriodo.ForeColor = System.Drawing.Color.Black;
            this.ConsultaPeriodo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsultaPeriodo.Location = new System.Drawing.Point(541, 167);
            this.ConsultaPeriodo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConsultaPeriodo.Name = "ConsultaPeriodo";
            this.ConsultaPeriodo.Size = new System.Drawing.Size(219, 34);
            this.ConsultaPeriodo.TabIndex = 48;
            // 
            // PartidasDia
            // 
            this.PartidasDia.AutoSize = true;
            this.PartidasDia.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartidasDia.Location = new System.Drawing.Point(20, 112);
            this.PartidasDia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PartidasDia.Name = "PartidasDia";
            this.PartidasDia.Size = new System.Drawing.Size(823, 27);
            this.PartidasDia.TabIndex = 47;
            this.PartidasDia.TabStop = true;
            this.PartidasDia.Text = "Dime las partidas que se han jugado entre  las fechas introducidas";
            this.PartidasDia.UseVisualStyleBackColor = true;
            // 
            // ResultadoParitdas
            // 
            this.ResultadoParitdas.AutoSize = true;
            this.ResultadoParitdas.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultadoParitdas.Location = new System.Drawing.Point(20, 133);
            this.ResultadoParitdas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ResultadoParitdas.Name = "ResultadoParitdas";
            this.ResultadoParitdas.Size = new System.Drawing.Size(859, 27);
            this.ResultadoParitdas.TabIndex = 46;
            this.ResultadoParitdas.TabStop = true;
            this.ResultadoParitdas.Text = "Dime resultados de partidas que jugué con alguien (introducir nombre)";
            this.ResultadoParitdas.UseVisualStyleBackColor = true;
            // 
            // ListadoJugadores
            // 
            this.ListadoJugadores.AutoSize = true;
            this.ListadoJugadores.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListadoJugadores.Location = new System.Drawing.Point(20, 90);
            this.ListadoJugadores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ListadoJugadores.Name = "ListadoJugadores";
            this.ListadoJugadores.Size = new System.Drawing.Size(523, 27);
            this.ListadoJugadores.TabIndex = 45;
            this.ListadoJugadores.TabStop = true;
            this.ListadoJugadores.Text = "Lista los jugadores con los que he jugado";
            this.ListadoJugadores.UseVisualStyleBackColor = true;
            // 
            // Resposta_Lbl
            // 
            this.Resposta_Lbl.AutoSize = true;
            this.Resposta_Lbl.Location = new System.Drawing.Point(351, 356);
            this.Resposta_Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Resposta_Lbl.Name = "Resposta_Lbl";
            this.Resposta_Lbl.Size = new System.Drawing.Size(111, 16);
            this.Resposta_Lbl.TabIndex = 44;
            this.Resposta_Lbl.Text = "Sin respuesta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Introduce una fecha";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 226);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Formato: (Dia-Mes-Año)";
            // 
            // DimeJugadores
            // 
            this.DimeJugadores.AutoSize = true;
            this.DimeJugadores.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimeJugadores.Location = new System.Drawing.Point(20, 23);
            this.DimeJugadores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DimeJugadores.Name = "DimeJugadores";
            this.DimeJugadores.Size = new System.Drawing.Size(667, 27);
            this.DimeJugadores.TabIndex = 8;
            this.DimeJugadores.TabStop = true;
            this.DimeJugadores.Text = "Dime qué jugadores jugaron este día (introduce fecha)";
            this.DimeJugadores.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 356);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "Respuesta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(260, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 51);
            this.label6.TabIndex = 37;
            this.label6.Text = "    Introduce un nombre o\r\nnombres separados por \'/\'\r\n     (Máximo 3 jugadores)";
            // 
            // ConsultaNombre
            // 
            this.ConsultaNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsultaNombre.ForeColor = System.Drawing.Color.Black;
            this.ConsultaNombre.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsultaNombre.Location = new System.Drawing.Point(284, 167);
            this.ConsultaNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConsultaNombre.Name = "ConsultaNombre";
            this.ConsultaNombre.Size = new System.Drawing.Size(219, 34);
            this.ConsultaNombre.TabIndex = 36;
            // 
            // DimeGanadores
            // 
            this.DimeGanadores.AutoSize = true;
            this.DimeGanadores.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimeGanadores.Location = new System.Drawing.Point(20, 44);
            this.DimeGanadores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DimeGanadores.Name = "DimeGanadores";
            this.DimeGanadores.Size = new System.Drawing.Size(667, 27);
            this.DimeGanadores.TabIndex = 34;
            this.DimeGanadores.TabStop = true;
            this.DimeGanadores.Text = "Dime qué jugadores ganaron este día (introduce fecha)";
            this.DimeGanadores.UseVisualStyleBackColor = true;
            // 
            // SumaDuracion
            // 
            this.SumaDuracion.AutoSize = true;
            this.SumaDuracion.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SumaDuracion.Location = new System.Drawing.Point(20, 66);
            this.SumaDuracion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SumaDuracion.Name = "SumaDuracion";
            this.SumaDuracion.Size = new System.Drawing.Size(631, 27);
            this.SumaDuracion.TabIndex = 18;
            this.SumaDuracion.TabStop = true;
            this.SumaDuracion.Text = "Duración total partidas ganadas (introduce nombre)";
            this.SumaDuracion.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleGreen;
            this.button1.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(619, 126);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 42);
            this.button1.TabIndex = 42;
            this.button1.Text = "Invitar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListaConectados
            // 
            this.ListaConectados.AllowUserToAddRows = false;
            this.ListaConectados.AllowUserToDeleteRows = false;
            this.ListaConectados.AllowUserToResizeColumns = false;
            this.ListaConectados.AllowUserToResizeRows = false;
            this.ListaConectados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaConectados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListaConectados.DefaultCellStyle = dataGridViewCellStyle6;
            this.ListaConectados.EnableHeadersVisualStyles = false;
            this.ListaConectados.Location = new System.Drawing.Point(75, 38);
            this.ListaConectados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ListaConectados.MultiSelect = false;
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.RowHeadersVisible = false;
            this.ListaConectados.RowHeadersWidth = 62;
            this.ListaConectados.Size = new System.Drawing.Size(491, 209);
            this.ListaConectados.TabIndex = 32;
            // 
            // button_Baja
            // 
            this.button_Baja.BackColor = System.Drawing.Color.IndianRed;
            this.button_Baja.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Baja.Location = new System.Drawing.Point(635, 11);
            this.button_Baja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Baja.Name = "button_Baja";
            this.button_Baja.Size = new System.Drawing.Size(212, 47);
            this.button_Baja.TabIndex = 41;
            this.button_Baja.Text = "Darse de Baja";
            this.button_Baja.UseVisualStyleBackColor = false;
            this.button_Baja.Click += new System.EventHandler(this.button_Baja_Click);
            // 
            // button_Desconectar
            // 
            this.button_Desconectar.BackColor = System.Drawing.Color.IndianRed;
            this.button_Desconectar.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Desconectar.Location = new System.Drawing.Point(873, 11);
            this.button_Desconectar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Desconectar.Name = "button_Desconectar";
            this.button_Desconectar.Size = new System.Drawing.Size(212, 47);
            this.button_Desconectar.TabIndex = 11;
            this.button_Desconectar.Text = "Desconectar";
            this.button_Desconectar.UseVisualStyleBackColor = false;
            this.button_Desconectar.Click += new System.EventHandler(this.button_Desconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(1095, 899);
            this.Controls.Add(this.button_Desconectar);
            this.Controls.Add(this.button_Baja);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "El Juego de la Oca";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox ConsultaFecha;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton DimeJugadores;
        private System.Windows.Forms.RadioButton SumaDuracion;
        private System.Windows.Forms.Button button_Desconectar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DimeGanadores;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ConsultaNombre;
        private System.Windows.Forms.Button button_Baja;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Resposta_Lbl;
        private System.Windows.Forms.RadioButton PartidasDia;
        private System.Windows.Forms.RadioButton ResultadoParitdas;
        private System.Windows.Forms.RadioButton ListadoJugadores;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ConsultaPeriodo;
    }
}


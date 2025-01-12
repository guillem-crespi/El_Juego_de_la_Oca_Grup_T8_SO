namespace WindowsFormsApplication1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Resolution_Box = new System.Windows.Forms.ComboBox();
            this.button_Dados = new System.Windows.Forms.Button();
            this.Resultado = new System.Windows.Forms.TextBox();
            this.Resultado2 = new System.Windows.Forms.TextBox();
            this.SumaResultado = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.Resolution_Lbl = new System.Windows.Forms.Label();
            this.Resultado_Lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nJugadorLbl = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Chat = new System.Windows.Forms.GroupBox();
            this.ChatTable = new System.Windows.Forms.DataGridView();
            this.ChatSendBtn = new System.Windows.Forms.Button();
            this.ChatTxtBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.Chat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChatTable)).BeginInit();
            this.SuspendLayout();
            // 
            // Resolution_Box
            // 
            this.Resolution_Box.AccessibleName = "Resolution_Box";
            this.Resolution_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Resolution_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Resolution_Box.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resolution_Box.FormattingEnabled = true;
            this.Resolution_Box.Items.AddRange(new object[] {
            "1000x1000",
            "900x900",
            "800x800",
            "700x700",
            "600x600",
            "500x500",
            "400x400",
            "300x300"});
            this.Resolution_Box.Location = new System.Drawing.Point(80, 332);
            this.Resolution_Box.Margin = new System.Windows.Forms.Padding(4);
            this.Resolution_Box.Name = "Resolution_Box";
            this.Resolution_Box.Size = new System.Drawing.Size(188, 24);
            this.Resolution_Box.TabIndex = 28;
            this.Resolution_Box.Tag = "";
            this.Resolution_Box.SelectedIndexChanged += new System.EventHandler(this.Resolution_Box_SelectedIndexChanged);
            // 
            // button_Dados
            // 
            this.button_Dados.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Dados.Location = new System.Drawing.Point(17, 74);
            this.button_Dados.Margin = new System.Windows.Forms.Padding(4);
            this.button_Dados.Name = "button_Dados";
            this.button_Dados.Size = new System.Drawing.Size(261, 47);
            this.button_Dados.TabIndex = 18;
            this.button_Dados.Text = "Tirar los dados";
            this.button_Dados.UseVisualStyleBackColor = true;
            this.button_Dados.Click += new System.EventHandler(this.button_Dados_Click);
            // 
            // Resultado
            // 
            this.Resultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Resultado.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resultado.Location = new System.Drawing.Point(90, 130);
            this.Resultado.Name = "Resultado";
            this.Resultado.ReadOnly = true;
            this.Resultado.Size = new System.Drawing.Size(38, 23);
            this.Resultado.TabIndex = 20;
            // 
            // Resultado2
            // 
            this.Resultado2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Resultado2.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resultado2.Location = new System.Drawing.Point(156, 130);
            this.Resultado2.Name = "Resultado2";
            this.Resultado2.ReadOnly = true;
            this.Resultado2.Size = new System.Drawing.Size(38, 23);
            this.Resultado2.TabIndex = 21;
            // 
            // SumaResultado
            // 
            this.SumaResultado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SumaResultado.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SumaResultado.Location = new System.Drawing.Point(161, 163);
            this.SumaResultado.Name = "SumaResultado";
            this.SumaResultado.ReadOnly = true;
            this.SumaResultado.Size = new System.Drawing.Size(67, 16);
            this.SumaResultado.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(307, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 1000);
            this.panel1.TabIndex = 23;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(90, 190);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(97, 20);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Jugador 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(90, 221);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(97, 20);
            this.radioButton2.TabIndex = 25;
            this.radioButton2.Text = "Jugador 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(90, 254);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(97, 20);
            this.radioButton3.TabIndex = 26;
            this.radioButton3.Text = "Jugador 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(90, 285);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(97, 20);
            this.radioButton4.TabIndex = 27;
            this.radioButton4.Text = "Jugador 4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // Resolution_Lbl
            // 
            this.Resolution_Lbl.AutoSize = true;
            this.Resolution_Lbl.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resolution_Lbl.Location = new System.Drawing.Point(42, 316);
            this.Resolution_Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Resolution_Lbl.Name = "Resolution_Lbl";
            this.Resolution_Lbl.Size = new System.Drawing.Size(151, 16);
            this.Resolution_Lbl.TabIndex = 29;
            this.Resolution_Lbl.Text = "Resolución tablero";
            // 
            // Resultado_Lbl
            // 
            this.Resultado_Lbl.AutoSize = true;
            this.Resultado_Lbl.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resultado_Lbl.Location = new System.Drawing.Point(35, 163);
            this.Resultado_Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Resultado_Lbl.Name = "Resultado_Lbl";
            this.Resultado_Lbl.Size = new System.Drawing.Size(87, 16);
            this.Resultado_Lbl.TabIndex = 30;
            this.Resultado_Lbl.Text = "Resultado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 31;
            this.label1.Text = "Eres el jugador:";
            // 
            // nJugadorLbl
            // 
            this.nJugadorLbl.AutoSize = true;
            this.nJugadorLbl.Location = new System.Drawing.Point(165, 29);
            this.nJugadorLbl.Name = "nJugadorLbl";
            this.nJugadorLbl.Size = new System.Drawing.Size(28, 14);
            this.nJugadorLbl.TabIndex = 32;
            this.nJugadorLbl.Text = "NaN";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1351, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // Chat
            // 
            this.Chat.BackColor = System.Drawing.Color.White;
            this.Chat.Controls.Add(this.ChatTable);
            this.Chat.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Chat.Location = new System.Drawing.Point(31, 363);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(247, 376);
            this.Chat.TabIndex = 48;
            this.Chat.TabStop = false;
            this.Chat.Text = "Chat";
            // 
            // ChatTable
            // 
            this.ChatTable.AllowUserToAddRows = false;
            this.ChatTable.AllowUserToDeleteRows = false;
            this.ChatTable.AllowUserToResizeColumns = false;
            this.ChatTable.AllowUserToResizeRows = false;
            this.ChatTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ChatTable.BackgroundColor = System.Drawing.Color.White;
            this.ChatTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ChatTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ChatTable.DefaultCellStyle = dataGridViewCellStyle1;
            this.ChatTable.EnableHeadersVisualStyles = false;
            this.ChatTable.GridColor = System.Drawing.Color.White;
            this.ChatTable.Location = new System.Drawing.Point(6, 19);
            this.ChatTable.MultiSelect = false;
            this.ChatTable.Name = "ChatTable";
            this.ChatTable.ReadOnly = true;
            this.ChatTable.RowHeadersVisible = false;
            this.ChatTable.RowHeadersWidth = 62;
            this.ChatTable.Size = new System.Drawing.Size(236, 351);
            this.ChatTable.TabIndex = 48;
            this.ChatTable.TabStop = false;
            // 
            // ChatSendBtn
            // 
            this.ChatSendBtn.BackColor = System.Drawing.Color.Thistle;
            this.ChatSendBtn.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatSendBtn.Location = new System.Drawing.Point(208, 745);
            this.ChatSendBtn.Name = "ChatSendBtn";
            this.ChatSendBtn.Size = new System.Drawing.Size(70, 26);
            this.ChatSendBtn.TabIndex = 50;
            this.ChatSendBtn.Text = "Enviar";
            this.ChatSendBtn.UseVisualStyleBackColor = false;
            this.ChatSendBtn.Click += new System.EventHandler(this.ChatSendBtn_Click);
            // 
            // ChatTxtBox
            // 
            this.ChatTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatTxtBox.ForeColor = System.Drawing.Color.Black;
            this.ChatTxtBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChatTxtBox.Location = new System.Drawing.Point(31, 745);
            this.ChatTxtBox.Name = "ChatTxtBox";
            this.ChatTxtBox.Size = new System.Drawing.Size(173, 29);
            this.ChatTxtBox.TabIndex = 49;
            this.ChatTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChatTxtBox_KeyPress);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(228)))), ((int)(((byte)(4)))));
            this.ClientSize = new System.Drawing.Size(1351, 845);
            this.Controls.Add(this.ChatTxtBox);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.ChatSendBtn);
            this.Controls.Add(this.nJugadorLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Resultado_Lbl);
            this.Controls.Add(this.Resolution_Lbl);
            this.Controls.Add(this.Resolution_Box);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.SumaResultado);
            this.Controls.Add(this.Resultado2);
            this.Controls.Add(this.Resultado);
            this.Controls.Add(this.button_Dados);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Juego de la oca";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Chat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChatTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Dados;
        private System.Windows.Forms.TextBox Resultado;
        private System.Windows.Forms.TextBox Resultado2;
        private System.Windows.Forms.TextBox SumaResultado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.ComboBox Resolution_Box;
        private System.Windows.Forms.Label Resolution_Lbl;
        private System.Windows.Forms.Label Resultado_Lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nJugadorLbl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.GroupBox Chat;
        private System.Windows.Forms.DataGridView ChatTable;
        private System.Windows.Forms.Button ChatSendBtn;
        private System.Windows.Forms.TextBox ChatTxtBox;
    }
}

namespace Sistema
{
    partial class FormConfiguracion
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            txtTarifaCarro = new TextBox();
            txtTarifaMoto = new TextBox();
            btnAplicarTarifas = new Button();
            btnResetParqueo = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 58);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "Carros";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 94);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 1;
            label2.Text = "Motos";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(130, 27);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 2;
            label3.Text = "Tarifas";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(84, 210);
            label4.Name = "label4";
            label4.Size = new Size(125, 15);
            label4.TabIndex = 3;
            label4.Text = "Espacios para parqueo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 255);
            label5.Name = "label5";
            label5.Size = new Size(41, 15);
            label5.TabIndex = 4;
            label5.Text = "Carros";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 295);
            label6.Name = "label6";
            label6.Size = new Size(41, 15);
            label6.TabIndex = 5;
            label6.Text = "Motos";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(84, 247);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(84, 292);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 7;
            // 
            // txtTarifaCarro
            // 
            txtTarifaCarro.Location = new Point(84, 50);
            txtTarifaCarro.Name = "txtTarifaCarro";
            txtTarifaCarro.Size = new Size(100, 23);
            txtTarifaCarro.TabIndex = 8;
            // 
            // txtTarifaMoto
            // 
            txtTarifaMoto.Location = new Point(84, 91);
            txtTarifaMoto.Name = "txtTarifaMoto";
            txtTarifaMoto.Size = new Size(100, 23);
            txtTarifaMoto.TabIndex = 9;
            // 
            // btnAplicarTarifas
            // 
            btnAplicarTarifas.Location = new Point(84, 145);
            btnAplicarTarifas.Name = "btnAplicarTarifas";
            btnAplicarTarifas.Size = new Size(75, 23);
            btnAplicarTarifas.TabIndex = 10;
            btnAplicarTarifas.Text = "Aplicar";
            btnAplicarTarifas.UseVisualStyleBackColor = true;
            btnAplicarTarifas.Click += btnAplicarTarifas_Click;
            // 
            // btnResetParqueo
            // 
            btnResetParqueo.BackColor = Color.FromArgb(192, 0, 0);
            btnResetParqueo.Location = new Point(84, 340);
            btnResetParqueo.Name = "btnResetParqueo";
            btnResetParqueo.Size = new Size(75, 23);
            btnResetParqueo.TabIndex = 11;
            btnResetParqueo.Text = "Reset";
            btnResetParqueo.UseVisualStyleBackColor = false;
            btnResetParqueo.Click += btnResetParqueo_Click;
            // 
            // FormConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(237, 393);
            Controls.Add(btnResetParqueo);
            Controls.Add(btnAplicarTarifas);
            Controls.Add(txtTarifaMoto);
            Controls.Add(txtTarifaCarro);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormConfiguracion";
            Text = "FormConfiguracion";
            Load += FormConfiguracion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox txtTarifaCarro;
        private TextBox txtTarifaMoto;
        private Button btnAplicarTarifas;
        private Button btnResetParqueo;
    }
}
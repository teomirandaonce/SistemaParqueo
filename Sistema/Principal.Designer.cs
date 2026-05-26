namespace Sistema
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label3 = new Label();
            btCobrar = new Button();
            btnAsignarParqueo = new Button();
            label2 = new Label();
            cmbTipoVehiculo = new ComboBox();
            label1 = new Label();
            txtPlaca = new TextBox();
            btnConfiguracion = new Button();
            panelParqueo1 = new FlowLayoutPanel();
            panelParqueo2 = new FlowLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            lblSeleccionado = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btCobrar);
            panel1.Controls.Add(btnAsignarParqueo);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbTipoVehiculo);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtPlaca);
            panel1.Location = new Point(492, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(275, 293);
            panel1.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(87, 85);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 7;
            label3.Text = "Sistema de Parqueo";
            // 
            // btCobrar
            // 
            btCobrar.Location = new Point(119, 252);
            btCobrar.Name = "btCobrar";
            btCobrar.Size = new Size(75, 23);
            btCobrar.TabIndex = 5;
            btCobrar.Text = "Cobrar";
            btCobrar.UseVisualStyleBackColor = true;
            btCobrar.Click += btnCobrar_Click;
            // 
            // btnAsignarParqueo
            // 
            btnAsignarParqueo.Location = new Point(119, 213);
            btnAsignarParqueo.Name = "btnAsignarParqueo";
            btnAsignarParqueo.Size = new Size(75, 23);
            btnAsignarParqueo.TabIndex = 4;
            btnAsignarParqueo.Text = "Asignar";
            btnAsignarParqueo.UseVisualStyleBackColor = true;
            btnAsignarParqueo.Click += btnAsignarParqueo_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 169);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 3;
            label2.Text = "Tipo de Vehiculo";
            // 
            // cmbTipoVehiculo
            // 
            cmbTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoVehiculo.FormattingEnabled = true;
            cmbTipoVehiculo.Items.AddRange(new object[] { "Carro", "Moto" });
            cmbTipoVehiculo.Location = new Point(131, 166);
            cmbTipoVehiculo.Name = "cmbTipoVehiculo";
            cmbTipoVehiculo.Size = new Size(121, 23);
            cmbTipoVehiculo.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 131);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 1;
            label1.Text = "Placa";
            // 
            // txtPlaca
            // 
            txtPlaca.Location = new Point(131, 128);
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(121, 23);
            txtPlaca.TabIndex = 0;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.Location = new Point(673, 415);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Size = new Size(115, 23);
            btnConfiguracion.TabIndex = 6;
            btnConfiguracion.Text = "Configuraciones";
            btnConfiguracion.TextAlign = ContentAlignment.BottomLeft;
            btnConfiguracion.UseVisualStyleBackColor = true;
            btnConfiguracion.Click += btnConfiguracion_Click;
            // 
            // panelParqueo1
            // 
            panelParqueo1.AutoScroll = true;
            panelParqueo1.Location = new Point(33, 53);
            panelParqueo1.Name = "panelParqueo1";
            panelParqueo1.Size = new Size(178, 352);
            panelParqueo1.TabIndex = 3;
            // 
            // panelParqueo2
            // 
            panelParqueo2.AutoScroll = true;
            panelParqueo2.Location = new Point(255, 53);
            panelParqueo2.Name = "panelParqueo2";
            panelParqueo2.Size = new Size(178, 352);
            panelParqueo2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(83, 35);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 5;
            label4.Text = "Parqueo Carros";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(299, 35);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 6;
            label5.Text = "Parqueo Motos";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(50, 419);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 7;
            label6.Text = "Seleccion";
            // 
            // lblSeleccionado
            // 
            lblSeleccionado.AutoSize = true;
            lblSeleccionado.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSeleccionado.Location = new Point(124, 419);
            lblSeleccionado.Name = "lblSeleccionado";
            lblSeleccionado.Size = new Size(165, 15);
            lblSeleccionado.TabIndex = 8;
            lblSeleccionado.Text = "Ningún espacio seleccionado";
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(lblSeleccionado);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnConfiguracion);
            Controls.Add(label4);
            Controls.Add(panelParqueo2);
            Controls.Add(panelParqueo1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Principal";
            Text = "Sistema Parqueo";
            Load += Principal_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private TextBox txtPlaca;
        private Button btnConfiguracion;
        private Button btCobrar;
        private Button btnAsignarParqueo;
        private Label label2;
        private ComboBox cmbTipoVehiculo;
        private Label label3;
        private FlowLayoutPanel panelParqueo1;
        private FlowLayoutPanel panelParqueo2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblSeleccionado;
    }
}

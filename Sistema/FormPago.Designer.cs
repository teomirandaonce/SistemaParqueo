namespace Sistema
{
    partial class FormPago
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
            panel1 = new Panel();
            btnConfirmarPago = new Button();
            lblInformacionPago = new Label();
            btnCancelar = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(81, 32);
            label1.Name = "label1";
            label1.Size = new Size(158, 25);
            label1.TabIndex = 0;
            label1.Text = "Detalle de cobro";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnConfirmarPago);
            panel1.Controls.Add(lblInformacionPago);
            panel1.Location = new Point(7, 60);
            panel1.Name = "panel1";
            panel1.Size = new Size(304, 342);
            panel1.TabIndex = 1;
            // 
            // btnConfirmarPago
            // 
            btnConfirmarPago.Location = new Point(120, 316);
            btnConfirmarPago.Name = "btnConfirmarPago";
            btnConfirmarPago.Size = new Size(75, 23);
            btnConfirmarPago.TabIndex = 2;
            btnConfirmarPago.Text = "Pagar";
            btnConfirmarPago.UseVisualStyleBackColor = true;
            btnConfirmarPago.Click += btnPagar_Click;
            // 
            // lblInformacionPago
            // 
            lblInformacionPago.AutoSize = true;
            lblInformacionPago.Location = new Point(74, 22);
            lblInformacionPago.Name = "lblInformacionPago";
            lblInformacionPago.Size = new Size(162, 15);
            lblInformacionPago.TabIndex = 0;
            lblInformacionPago.Text = "Cargando datos del espacio...";
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(236, 424);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(323, 459);
            Controls.Add(btnCancelar);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "FormPago";
            Text = "FormPago";
            Load += FormPago_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button btnConfirmarPago;
        private Label lblInformacionPago;
        private Button btnCancelar;
    }
}
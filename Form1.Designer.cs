namespace SistemaParqueo
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.parkingPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.placaTextBox = new System.Windows.Forms.TextBox();
            this.plazasNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.entryButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();

            // parkingPanel
            this.parkingPanel.Location = new System.Drawing.Point(12, 12);
            this.parkingPanel.Size = new System.Drawing.Size(760, 400);

            // titleLabel
            this.titleLabel.Text = "Sistema de Parqueo";
            this.titleLabel.Location = new System.Drawing.Point(12, 420);
            this.titleLabel.Size = new System.Drawing.Size(200, 30);

            // statusLabel
            this.statusLabel.Text = "Estado:";
            this.statusLabel.Location = new System.Drawing.Point(12, 450);
            this.statusLabel.Size = new System.Drawing.Size(200, 30);

            // placaTextBox
            this.placaTextBox.Location = new System.Drawing.Point(12, 480);
            this.placaTextBox.Size = new System.Drawing.Size(200, 30);

            // plazasNumericUpDown
            this.plazasNumericUpDown.Location = new System.Drawing.Point(220, 480);
            this.plazasNumericUpDown.Size = new System.Drawing.Size(120, 30);

            // entryButton
            this.entryButton.Text = "Entrada";
            this.entryButton.Location = new System.Drawing.Point(12, 520);
            this.entryButton.Size = new System.Drawing.Size(100, 30);

            // exitButton
            this.exitButton.Text = "Salida";
            this.exitButton.Location = new System.Drawing.Point(120, 520);
            this.exitButton.Size = new System.Drawing.Size(100, 30);

            // Adding controls to the panel
            this.parkingPanel.Controls.Add(this.titleLabel);
            this.parkingPanel.Controls.Add(this.statusLabel);
            this.parkingPanel.Controls.Add(this.placaTextBox);
            this.parkingPanel.Controls.Add(this.plazasNumericUpDown);
            this.parkingPanel.Controls.Add(this.entryButton);
            this.parkingPanel.Controls.Add(this.exitButton);
        }
    }
}
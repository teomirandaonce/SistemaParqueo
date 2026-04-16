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
            this.SuspendLayout();
            this.parkingPanel = new System.Windows.Forms.Panel();
            this.parkingGrid = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.selectedSpaceLabel = new System.Windows.Forms.Label();
            this.placaLabel = new System.Windows.Forms.Label();
            this.placaTextBox = new System.Windows.Forms.TextBox();
            this.entryButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.activeTimeLabel = new System.Windows.Forms.Label();

            // parkingPanel
            this.parkingPanel.BackColor = System.Drawing.Color.White;
            this.parkingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.parkingPanel.Location = new System.Drawing.Point(24, 70);
            this.parkingPanel.Size = new System.Drawing.Size(430, 430);

            // parkingGrid
            this.parkingGrid.ColumnCount = 5;
            this.parkingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parkingGrid.Location = new System.Drawing.Point(0, 0);
            this.parkingGrid.RowCount = 5;
            this.parkingGrid.Margin = new System.Windows.Forms.Padding(0);
            this.parkingGrid.Padding = new System.Windows.Forms.Padding(0);
            this.parkingGrid.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;

            // titleLabel
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.Navy;
            this.titleLabel.Text = "SISTEMA DE PARQUEO INTELIGENTE";
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Size = new System.Drawing.Size(520, 30);

            // statusLabel
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.statusLabel.Text = "Espacios ocupados: 0/25";
            this.statusLabel.Location = new System.Drawing.Point(470, 90);
            this.statusLabel.Size = new System.Drawing.Size(220, 30);

            // selectedSpaceLabel
            this.selectedSpaceLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedSpaceLabel.Text = "Espacio 1: LIBRE";
            this.selectedSpaceLabel.Location = new System.Drawing.Point(24, 520);
            this.selectedSpaceLabel.Size = new System.Drawing.Size(666, 28);

            // placaLabel
            this.placaLabel.Text = "Placa:";
            this.placaLabel.Location = new System.Drawing.Point(24, 565);
            this.placaLabel.Size = new System.Drawing.Size(50, 22);

            // placaTextBox
            this.placaTextBox.Location = new System.Drawing.Point(80, 562);
            this.placaTextBox.Size = new System.Drawing.Size(160, 22);

            // entryButton
            this.entryButton.Text = "Simular";
            this.entryButton.Location = new System.Drawing.Point(260, 558);
            this.entryButton.Size = new System.Drawing.Size(110, 30);

            // exitButton
            this.exitButton.Text = "Simular Salida";
            this.exitButton.Location = new System.Drawing.Point(380, 558);
            this.exitButton.Size = new System.Drawing.Size(120, 30);

            // clearButton
            this.clearButton.Text = "Limpiar";
            this.clearButton.Location = new System.Drawing.Point(510, 558);
            this.clearButton.Size = new System.Drawing.Size(90, 30);

            // activeTimeLabel
            this.activeTimeLabel.Text = "Tiempo activo: 00:00:00";
            this.activeTimeLabel.Location = new System.Drawing.Point(24, 605);
            this.activeTimeLabel.Size = new System.Drawing.Size(220, 22);

            this.parkingPanel.Controls.Add(this.parkingGrid);

            // Form1
            this.ClientSize = new System.Drawing.Size(720, 650);
            this.Controls.Add(this.parkingPanel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.selectedSpaceLabel);
            this.Controls.Add(this.placaLabel);
            this.Controls.Add(this.placaTextBox);
            this.Controls.Add(this.entryButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.activeTimeLabel);
            this.Name = "Form1";
            this.Text = "Parking System";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel parkingPanel;
        private System.Windows.Forms.TableLayoutPanel parkingGrid;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label selectedSpaceLabel;
        private System.Windows.Forms.Label placaLabel;
        private System.Windows.Forms.TextBox placaTextBox;
        private System.Windows.Forms.Button entryButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label activeTimeLabel;
    }
}

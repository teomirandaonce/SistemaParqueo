using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaParqueo
{
    public partial class Form1 : Form
    {
        private const int rows = 5;
        private const int cols = 5;

        private readonly Timer timer;
        private Label[,] spaceLabels;
        private bool[,] occupiedSpaces;
        private int selectedRow = -1;
        private int selectedCol = -1;
        private DateTime startTime;

        public Form1()
        {
            InitializeComponent();
            this.entryButton.Click += EntryButton_Click;
            this.exitButton.Click += ExitButton_Click;
            this.clearButton.Click += ClearButton_Click;

            InitializeParkingGrid();

            startTime = DateTime.Now;
            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeParkingGrid()
        {
            parkingGrid.Controls.Clear();
            parkingGrid.ColumnStyles.Clear();
            parkingGrid.RowStyles.Clear();

            spaceLabels = new Label[rows, cols];
            occupiedSpaces = new bool[rows, cols];

            float columnPercent = 100f / cols;
            float rowPercent = 100f / rows;

            for (int col = 0; col < cols; col++)
            {
                parkingGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
            }

            for (int row = 0; row < rows; row++)
            {
                parkingGrid.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
            }

            int number = 1;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var label = new Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.TopLeft,
                        BackColor = Color.LimeGreen,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Margin = new Padding(0),
                        Padding = new Padding(6, 4, 0, 0),
                        Text = number.ToString()
                    };

                    label.Tag = new Point(row, col);
                    label.Click += SpaceLabel_Click;
                    parkingGrid.Controls.Add(label, col, row);
                    spaceLabels[row, col] = label;
                    number++;
                }
            }

            UpdateOccupiedLabel();
            UpdateSelectedSpaceLabel();
        }

        private void SpaceLabel_Click(object sender, EventArgs e)
        {
            if (sender is Label label && label.Tag is Point point)
            {
                selectedRow = point.X;
                selectedCol = point.Y;
                UpdateSelectedSpaceLabel();
            }
        }

        private void EntryButton_Click(object sender, EventArgs e)
        {
            if (!TryFindFreeSpace(out int row, out int col))
            {
                MessageBox.Show("No hay espacios disponibles.", "Parqueo lleno", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string placa = string.IsNullOrWhiteSpace(placaTextBox.Text) ? "---" : placaTextBox.Text.Trim();
            occupiedSpaces[row, col] = true;

            var label = spaceLabels[row, col];
            label.BackColor = Color.Red;
            label.Text = $"{GetSpaceNumber(row, col)}\n{placa}";

            selectedRow = row;
            selectedCol = col;
            UpdateOccupiedLabel();
            UpdateSelectedSpaceLabel();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (selectedRow < 0 || selectedCol < 0)
            {
                MessageBox.Show("Seleccione un espacio primero.", "Salida", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!occupiedSpaces[selectedRow, selectedCol])
            {
                MessageBox.Show("El espacio seleccionado ya está libre.", "Salida", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            occupiedSpaces[selectedRow, selectedCol] = false;
            ResetSpaceLabel(selectedRow, selectedCol);
            UpdateOccupiedLabel();
            UpdateSelectedSpaceLabel();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    occupiedSpaces[row, col] = false;
                    ResetSpaceLabel(row, col);
                }
            }

            placaTextBox.Clear();
            selectedRow = -1;
            selectedCol = -1;
            UpdateOccupiedLabel();
            UpdateSelectedSpaceLabel();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            activeTimeLabel.Text = $"Tiempo activo: {elapsed:hh\\:mm\\:ss}";
        }

        private bool TryFindFreeSpace(out int row, out int col)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (!occupiedSpaces[r, c])
                    {
                        row = r;
                        col = c;
                        return true;
                    }
                }
            }

            row = -1;
            col = -1;
            return false;
        }

        private void UpdateOccupiedLabel()
        {
            int count = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (occupiedSpaces[r, c])
                    {
                        count++;
                    }
                }
            }

            statusLabel.Text = $"Espacios ocupados: {count}/{rows * cols}";
        }

        private void UpdateSelectedSpaceLabel()
        {
            if (selectedRow < 0 || selectedCol < 0)
            {
                selectedSpaceLabel.Text = "Seleccione un espacio.";
                return;
            }

            string estado = occupiedSpaces[selectedRow, selectedCol] ? "OCUPADO" : "LIBRE";
            selectedSpaceLabel.Text = $"Espacio {GetSpaceNumber(selectedRow, selectedCol)}: {estado}";
        }

        private void ResetSpaceLabel(int row, int col)
        {
            var label = spaceLabels[row, col];
            label.BackColor = Color.LimeGreen;
            label.Text = GetSpaceNumber(row, col).ToString();
        }

        private int GetSpaceNumber(int row, int col)
        {
            return (row * cols) + col + 1;
        }
    }
}

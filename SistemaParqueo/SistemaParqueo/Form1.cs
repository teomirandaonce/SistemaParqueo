using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaParqueo
{
    public partial class Form1 : Form
    {
        private const int rows = 5; // Number of rows in the grid
        private const int cols = 10; // Number of columns in the grid
        
        private Timer timer;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Parking System";
            this.Size = new Size(800, 600);

            // Wire up events to designer-created controls
            this.entryButton.Click += EntryButton_Click;
            this.exitButton.Click += ExitButton_Click;

            // Draw inside the parking panel and handle its mouse clicks
            this.parkingPanel.Paint += ParkingPanel_Paint;
            this.parkingPanel.MouseClick += new MouseEventHandler(this.Form1_MouseClick);

            timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Drawing moved to the parking panel to match designer layout

        private void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= rows; i++)
            {
                g.DrawLine(Pens.Black, 50, 50 + (i * 50), 500, 50 + (i * 50)); // Horizontal
            }
            for (int j = 0; j <= cols; j++)
            {
                g.DrawLine(Pens.Black, 50 + (j * 50), 50, 50 + (j * 50), 50 + (rows * 50)); // Vertical
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int rowIndex = (e.Y - 50) / 50;
            int colIndex = (e.X - 50) / 50;

            if (rowIndex >= 0 && rowIndex < rows && colIndex >= 0 && colIndex < cols)
            {
                Console.WriteLine($"Space selected: Row {rowIndex}, Column {colIndex}");
            }
        }

        private void ParkingPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
        }

        private void EntryButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Vehicle entered.");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Vehicle exited.");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // Redraws the form
        }
    }
}

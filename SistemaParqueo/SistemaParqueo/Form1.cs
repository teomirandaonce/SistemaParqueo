using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaParqueo
{
    public partial class Form1 : Form
    {
        private const int rows = 5; // Number of rows in the grid
        private const int cols = 10; // Number of columns in the grid
        private Button entryButton;
        private Button exitButton;
        private Timer timer;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Parking System";
            this.Size = new Size(800, 600);
            this.MouseClick += new MouseEventHandler(this.Form1_MouseClick);

            entryButton = new Button() { Text = "Entry", Location = new Point(10, 10) };
            exitButton = new Button() { Text = "Exit", Location = new Point(100, 10) };
            entryButton.Click += EntryButton_Click;
            exitButton.Click += ExitButton_Click;

            this.Controls.Add(entryButton);
            this.Controls.Add(exitButton);

            timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawGrid(g);
        }

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

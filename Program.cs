using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaParqueo
{
    public class Form1 : Form
    {
        // Variables globales del sistema de parqueo
        private int filas = 5;
        private int columnas = 5;
        private Rectangle[,] espaciosRect;
        private bool[,] ocupado;
        private string[,] placas;
        private DateTime[,] horasEntrada;
        private int espacioSeleccionadoFila = -1;
        private int espacioSeleccionadoCol = -1;
        private System.Windows.Forms.Timer timerTiempo;
        private System.Windows.Forms.Timer timerTooltip;
        private DateTime horaInicio;
        private int espaciosOcupados = 0;

        // Controles del formulario
        private Label lblTitulo;
        private Panel parkingPanel;
        private Label lblEstado;
        private TextBox txtPlaca;
        private Button btnEntrada;
        private Button btnSalida;
        private Label lblTiempo;
        private Label lblEstadistica;
        private NumericUpDown numHoras;

        public Form1()
        {
            InitializeComponent();
            InicializarDatos();
            InicializarTimer();
        }

        private void InicializarDatos()
        {
            ocupado = new bool[filas, columnas];
            placas = new string[filas, columnas];
            horasEntrada = new DateTime[filas, columnas];
            espaciosRect = new Rectangle[filas, columnas];

            // Ejemplo: algunos espacios ocupados
            ocupado[0, 0] = true;
            placas[0, 0] = "ABC-123";
            horasEntrada[0, 0] = DateTime.Now.AddHours(-1);

            ocupado[1, 2] = true;
            placas[1, 2] = "XYZ-789";
            horasEntrada[1, 2] = DateTime.Now.AddHours(-2);

            espaciosOcupados = 2;
        }

        private void InicializarTimer()
        {
            horaInicio = DateTime.Now;

            // Timer para actualizar tiempo
            timerTiempo = new System.Windows.Forms.Timer();
            timerTiempo.Interval = 1000;
            timerTiempo.Tick += (s, e) =>
            {
                lblTiempo.Text = $"⏱️ Tiempo activo: {(DateTime.Now - horaInicio):hh\\:mm\\:ss}";
            };
            timerTiempo.Start();
        }

        private void InitializeComponent()
        {
            // TÍTULO
            this.lblTitulo = new Label();
            this.lblTitulo.Text = "🅿️ SISTEMA DE PARQUEO INTELIGENTE";
            this.lblTitulo.Font = new Font("Arial", 16, FontStyle.Bold);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.DarkBlue;
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.Size = new Size(900, 35);

            // PANEL DE PARQUEO (Cuadrícula GDI+)
            this.parkingPanel = new Panel();
            this.parkingPanel.Location = new Point(150, 55);
            this.parkingPanel.Size = new Size(400, 400);
            this.parkingPanel.BackColor = Color.White;
            this.parkingPanel.BorderStyle = BorderStyle.FixedSingle;
            this.parkingPanel.Paint += parkingPanel_Paint;
            this.parkingPanel.MouseClick += parkingPanel_MouseClick;
            this.parkingPanel.MouseMove += parkingPanel_MouseMove;

            // LABEL ESTADO
            this.lblEstado = new Label();
            this.lblEstado.Text = "Seleccione un espacio...";
            this.lblEstado.Location = new Point(50, 470);
            this.lblEstado.Size = new Size(800, 35);
            this.lblEstado.BorderStyle = BorderStyle.FixedSingle;
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            this.lblEstado.Padding = new Padding(5, 0, 0, 0);
            this.lblEstado.BackColor = Color.LightYellow;

            // LABEL ESTADÍSTICA
            this.lblEstadistica = new Label();
            this.lblEstadistica.Text = $"Espacios ocupados: 0/{filas * columnas}";
            this.lblEstadistica.Location = new Point(600, 60);
            this.lblEstadistica.Size = new Size(250, 25);
            this.lblEstadistica.TextAlign = ContentAlignment.MiddleLeft;
            this.lblEstadistica.Font = new Font("Arial", 10, FontStyle.Bold);

            // LABEL ENTRADA
            var lblEntrada = new Label();
            lblEntrada.Text = "Placa:";
            lblEntrada.Location = new Point(50, 515);
            lblEntrada.Size = new Size(50, 20);

            // TEXTBOX PLACA
            this.txtPlaca = new TextBox();
            this.txtPlaca.Location = new Point(100, 515);
            this.txtPlaca.Size = new Size(150, 20);
            this.txtPlaca.PlaceholderText = "Ej: ABC-123";

            // NUMERIC UP DOWN (para futuro cobro)
            this.numHoras = new NumericUpDown();
            this.numHoras.Location = new Point(100, 545);
            this.numHoras.Size = new Size(150, 20);
            this.numHoras.Visible = false;
            this.numHoras.Minimum = 0;
            this.numHoras.Maximum = 24;

            // BOTÓN ENTRADA
            this.btnEntrada = new Button();
            this.btnEntrada.Text = "▶ Simular Entrada";
            this.btnEntrada.Location = new Point(270, 515);
            this.btnEntrada.Size = new Size(140, 30);
            this.btnEntrada.BackColor = Color.LimeGreen;
            this.btnEntrada.ForeColor = Color.White;
            this.btnEntrada.FlatStyle = FlatStyle.Flat;
            this.btnEntrada.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnEntrada.Click += btnEntrada_Click;

            // BOTÓN SALIDA
            this.btnSalida = new Button();
            this.btnSalida.Text = "⏹ Simular Salida";
            this.btnSalida.Location = new Point(420, 515);
            this.btnSalida.Size = new Size(140, 30);
            this.btnSalida.BackColor = Color.IndianRed;
            this.btnSalida.ForeColor = Color.White;
            this.btnSalida.FlatStyle = FlatStyle.Flat;
            this.btnSalida.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnSalida.Click += btnSalida_Click;

            // BOTÓN LIMPIAR
            var btnLimpiar = new Button();
            btnLimpiar.Text = "🔄 Limpiar";
            btnLimpiar.Location = new Point(570, 515);
            btnLimpiar.Size = new Size(100, 30);
            btnLimpiar.BackColor = Color.CornflowerBlue;
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Click += (s, e) => { txtPlaca.Clear(); };

            // LABEL TIEMPO
            this.lblTiempo = new Label();
            this.lblTiempo.Text = "⏱️ Tiempo activo: 00:00:00";
            this.lblTiempo.Location = new Point(50, 555);
            this.lblTiempo.Size = new Size(800, 20);
            this.lblTiempo.Font = new Font("Arial", 9);

            // FORMULARIO PRINCIPAL
            this.Text = "Sistema de Parqueo Inteligente - Dashboard";
            this.Size = new Size(920, 610);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.MaximizeBox = false;

            // Agregar controles
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.parkingPanel);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblEstadistica);
            this.Controls.Add(lblEntrada);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.numHoras);
            this.Controls.Add(this.btnEntrada);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(btnLimpiar);
            this.Controls.Add(this.lblTiempo);
        }

        // Evento Paint - Dibuja la cuadrícula con GDI+
        private void parkingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int anchoCelda = parkingPanel.Width / columnas;
            int altoCelda = parkingPanel.Height / filas;

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    int x = j * anchoCelda;
                    int y = i * altoCelda;
                    Rectangle rect = new Rectangle(x, y, anchoCelda, altoCelda);
                    espaciosRect[i, j] = rect;

                    // Color según estado
                    Color colorFondo = ocupado[i, j] ? Color.Red : Color.LimeGreen;

                    // Destacar espacio seleccionado
                    if (espacioSeleccionadoFila == i && espacioSeleccionadoCol == j)
                    {
                        colorFondo = ocupado[i, j] ? Color.DarkRed : Color.DarkGreen;
                    }

                    using (SolidBrush brush = new SolidBrush(colorFondo))
                    {
                        g.FillRectangle(brush, rect);
                    }

                    // Borde negro grueso
                    using (Pen penBorde = new Pen(Color.Black, 2))
                    {
                        g.DrawRectangle(penBorde, rect);
                    }

                    // Texto del espacio
                    string numeroEspacio = (i * columnas + j + 1).ToString();
                    using (Font fNumero = new Font("Arial", 10, FontStyle.Bold))
                    {
                        g.DrawString(numeroEspacio, fNumero, Brushes.White, rect.X + 5, rect.Y + 5);
                    }

                    // Mostrar placa si está ocupado
                    if (ocupado[i, j] && !string.IsNullOrEmpty(placas[i, j]))
                    {
                        TimeSpan tiempoTranscurrido = DateTime.Now - horasEntrada[i, j];
                        string texto = $"{placas[i, j]}\n{tiempoTranscurrido.Hours}h {tiempoTranscurrido.Minutes}m";

                        using (Font f = new Font("Arial", 7))
                        {
                            g.DrawString(texto, f, Brushes.White, rect.X + 5, rect.Y + 25);
                        }
                    }
                }
            }
        }

        // Evento MouseMove - Para futuro tooltip
        private void parkingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (espaciosRect[i, j].Contains(e.Location))
                    {
                        return;
                    }
                }
            }
        }

        // Evento Click - Selecciona espacio
        private void parkingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (espaciosRect[i, j].Contains(e.Location))
                    {
                        espacioSeleccionadoFila = i;
                        espacioSeleccionadoCol = j;
                        string estadoTexto = ocupado[i, j] ? "OCUPADO" : "LIBRE";
                        string placaTexto = "";

                        if (ocupado[i, j] && !string.IsNullOrEmpty(placas[i, j]))
                        {
                            TimeSpan tiempoTranscurrido = DateTime.Now - horasEntrada[i, j];
                            placaTexto = $" - Placa: {placas[i, j]} - Tiempo: {tiempoTranscurrido.Hours}h {tiempoTranscurrido.Minutes}m";
                        }

                        lblEstado.Text = $"Espacio {i * columnas + j + 1}: {estadoTexto}{placaTexto}";
                        parkingPanel.Invalidate();
                        return;
                    }
                }
            }
        }

        // Simular entrada de vehículo
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            string placa = txtPlaca.Text.Trim();
            if (string.IsNullOrEmpty(placa))
            {
                MessageBox.Show("Ingrese una placa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si la placa ya existe
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (ocupado[i, j] && placas[i, j] == placa)
                    {
                        MessageBox.Show($"El vehículo {placa} ya está en el parqueo en el espacio {i * columnas + j + 1}", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Buscar primer espacio libre
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (!ocupado[i, j])
                    {
                        ocupado[i, j] = true;
                        placas[i, j] = placa;
                        horasEntrada[i, j] = DateTime.Now;
                        espaciosOcupados++;

                        parkingPanel.Invalidate();
                        lblEstado.Text = $"✅ Vehículo {placa} estacionado en espacio {i * columnas + j + 1}";
                        lblEstadistica.Text = $"Espacios ocupados: {espaciosOcupados}/{filas * columnas}";
                        txtPlaca.Clear();
                        txtPlaca.Focus();
                        return;
                    }
                }
            }

            MessageBox.Show("❌ Parqueo lleno. El vehículo pasa a la cola de espera", 
                "Capacidad máxima", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Simular salida de vehículo
        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (espacioSeleccionadoFila == -1 || espacioSeleccionadoCol == -1)
            {
                MessageBox.Show("Primero seleccione un espacio haciendo clic en él", 
                    "Seleccionar espacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int f = espacioSeleccionadoFila;
            int c = espacioSeleccionadoCol;

            if (!ocupado[f, c])
            {
                MessageBox.Show("El espacio seleccionado ya está libre", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string placa = placas[f, c];
            TimeSpan tiempoEstacionado = DateTime.Now - horasEntrada[f, c];
            double costo = tiempoEstacionado.TotalHours * 5; // 5 dólares por hora

            // Liberar espacio
            ocupado[f, c] = false;
            placas[f, c] = null;
            horasEntrada[f, c] = DateTime.MinValue;
            espaciosOcupados--;

            parkingPanel.Invalidate();
            lblEstado.Text = $"✅ Vehículo {placa} salió del espacio {f * columnas + c + 1} - " +
                           $"Tiempo: {tiempoEstacionado.Hours}h {tiempoEstacionado.Minutes}m - " +
                           $"Costo: ${costo:F2}";
            lblEstadistica.Text = $"Espacios ocupados: {espaciosOcupados}/{filas * columnas}";

            espacioSeleccionadoFila = -1;
            espacioSeleccionadoCol = -1;
        }
    }
}
// Diseñado y Modificado por Evanelyh
#nullable disable
using Microsoft.Data.Sqlite;
using Sistema;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema
{
    public partial class Principal : Form
    {
        private string idEspacioSeleccionado = "";

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            cmbTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;

            CargarMapaParqueo();

            Connection db = new Connection();

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Espacios WHERE estado = 'Disponible';";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        long disponibles = (long)command.ExecuteScalar();

                        MessageBox.Show($"¡Conexión exitosa al sistema de parqueo desde cero!\n\nEspacios disponibles actualmente: {disponibles}",
                                        "Éxito de Conexión",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hubo un problema al conectar con la base de datos:\n{ex.Message}",
                                    "Error Crítico",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void CargarMapaParqueo()
        {
            panelParqueo1.Controls.Clear();
            panelParqueo2.Controls.Clear();

            Connection db = new Connection();
            int contadorBotones = 0;

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id_espacio, numero, estado, seccion FROM Espacios ORDER BY numero ASC;";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idEspacio = reader["id_espacio"].ToString();
                                string numero = reader["numero"].ToString();
                                string estado = reader["estado"].ToString();
                                string seccion = reader["seccion"].ToString();

                                Button btnEspacio = new Button();
                                btnEspacio.Text = $"Espacio {numero}\n[{estado}]";
                                btnEspacio.Size = new Size(100, 70);
                                btnEspacio.Font = new Font("Arial", 9, FontStyle.Bold);
                                btnEspacio.Margin = new Padding(10);
                                btnEspacio.Tag = idEspacio;

                                if (estado.Trim().ToLower() == "disponible")
                                {
                                    btnEspacio.BackColor = Color.CadetBlue;
                                }
                                else
                                {
                                    btnEspacio.BackColor = Color.Salmon;
                                }

                                btnEspacio.Click += BotonEspacio_Click;

                                string seccionLimpia = seccion.Trim().ToLower();

                                if (seccionLimpia.Contains("1"))
                                {
                                    panelParqueo1.Controls.Add(btnEspacio);
                                    contadorBotones++;
                                }
                                else if (seccionLimpia.Contains("2"))
                                {
                                    panelParqueo2.Controls.Add(btnEspacio);
                                    contadorBotones++;
                                }
                            }
                        }
                    }

                    if (contadorBotones == 0)
                    {
                        MessageBox.Show("La conexión fue exitosa, pero la tabla 'Espacios' está vacía o las secciones no contienen los números '1' o '2'.",
                                        "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el mapa dinámico: {ex.Message}",
                                    "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BotonEspacio_Click(object sender, EventArgs e)
        {
            Button botonPresionado = sender as Button;
            if (botonPresionado != null)
            {
                idEspacioSeleccionado = botonPresionado.Tag.ToString();

                string[] lineas = botonPresionado.Text.Split('\n');
                string nombreEspacio = lineas[0];
                string estadoEspacio = lineas.Length > 1 ? lineas[1] : "";

                lblSeleccionado.Text = $"Seleccionado: {nombreEspacio} {estadoEspacio} (ID: {idEspacioSeleccionado})";

                if (panelParqueo2.Controls.Contains(botonPresionado))
                {
                    cmbTipoVehiculo.SelectedItem = "Moto";
                }
                else if (panelParqueo1.Controls.Contains(botonPresionado))
                {
                    cmbTipoVehiculo.SelectedItem = "Carro";
                }
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.EsEspacioAptoParaCobrar(idEspacioSeleccionado, lblSeleccionado.Text))
            {
                return;
            }

            int idEspacio = Convert.ToInt32(idEspacioSeleccionado);

            FormPago ventanaPago = new FormPago(idEspacio);
            ventanaPago.StartPosition = FormStartPosition.CenterParent;

            if (ventanaPago.ShowDialog() == DialogResult.OK)
            {
                idEspacioSeleccionado = "";
                lblSeleccionado.Text = "Ningún espacio seleccionado";
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;

                CargarMapaParqueo();
            }
        }

        private void btnAsignarParqueo_Click(object sender, EventArgs e)
        {
            if (!Validaciones.ValidarSeleccion(idEspacioSeleccionado)) return;
            if (!Validaciones.ValidarEspacioOcupado(lblSeleccionado.Text)) return;
            if (!Validaciones.ValidarTipoVehiculo(cmbTipoVehiculo.SelectedIndex)) return;
            if (!Validaciones.ValidarPlacaTexto(txtPlaca.Text)) return;

            string placa = txtPlaca.Text.Trim().ToUpper();
            string tipoVehiculo = cmbTipoVehiculo.SelectedItem.ToString();

            Button botonSeleccionado = null;
            foreach (Button btn in panelParqueo2.Controls)
            {
                if (btn.Tag.ToString() == idEspacioSeleccionado)
                {
                    botonSeleccionado = btn;
                    break;
                }
            }

            if (botonSeleccionado != null && tipoVehiculo != "Moto")
            {
                MessageBox.Show("¡Operación Denegada! El Parqueo 2 es de uso exclusivo para Motos.",
                                "Error de Asignación", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Connection db = new Connection();

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    if (!Validaciones.ValidarPlacaDuplicada(placa, connection)) return;

                    using (SqliteTransaction transaccion = connection.BeginTransaction())
                    {
                        string queryVehiculo = @"INSERT INTO Vehiculos (placa, tipo, hora_entrada, espacio_id) 
                                                 VALUES (@placa, @tipo, @hora, @espacioId);";

                        using (SqliteCommand cmdVehiculo = new SqliteCommand(queryVehiculo, connection, transaccion))
                        {
                            cmdVehiculo.Parameters.AddWithValue("@placa", placa);
                            cmdVehiculo.Parameters.AddWithValue("@tipo", tipoVehiculo);
                            cmdVehiculo.Parameters.AddWithValue("@hora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmdVehiculo.Parameters.AddWithValue("@espacioId", Convert.ToInt32(idEspacioSeleccionado));

                            cmdVehiculo.ExecuteNonQuery();
                        }

                        string queryEspacio = "UPDATE Espacios SET estado = 'Ocupado' WHERE id_espacio = @espacioId;";

                        using (SqliteCommand cmdEspacio = new SqliteCommand(queryEspacio, connection, transaccion))
                        {
                            cmdEspacio.Parameters.AddWithValue("@espacioId", Convert.ToInt32(idEspacioSeleccionado));
                            cmdEspacio.ExecuteNonQuery();
                        }

                        transaccion.Commit();

                        MessageBox.Show($"¡Ingreso Exitoso!\n\nVehículo registrado en la tabla Vehículos y espacio modificado a Ocupado.",
                                        "Sistema de Parqueo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtPlaca.Clear();
                        cmbTipoVehiculo.SelectedIndex = -1;
                        idEspacioSeleccionado = "";
                        lblSeleccionado.Text = "Ningún espacio seleccionado";

                        CargarMapaParqueo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar en la base de datos: {ex.Message}",
                                    "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            FormConfiguracion frmConfig = new FormConfiguracion();
            frmConfig.StartPosition = FormStartPosition.CenterParent;

            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                idEspacioSeleccionado = "";
                lblSeleccionado.Text = "Ningún espacio seleccionado";

                CargarMapaParqueo();
            }
        }

        private void panelCalle_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Yellow, 3))
            {
                pen.DashPattern = new float[] { 5, 5 };
                e.Graphics.DrawLine(pen, panelCalle.Width / 2, 0, panelCalle.Width / 2, panelCalle.Height);
            }
        }
    }
}
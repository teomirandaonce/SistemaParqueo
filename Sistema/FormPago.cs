#nullable disable
using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Sistema
{
    public partial class FormPago : Form
    {

        // Variable interna para recordar el ID que viene desde la pantalla principal
        private int idEspacioCobrar;

        // Variables para guardar los datos recuperados de la base de datos
        private int idVehiculo = 0;
        private string placaVehiculo = "";
        private string tipoVehiculo = "";
        private DateTime horaEntrada;

        // Tu constructor que recibe el parámetro
        public FormPago(int idEspacio)
        {
            InitializeComponent();
            this.idEspacioCobrar = idEspacio;
        }

        // Asegúrate de que este evento esté enlazado al 'Load' del Form en sus propiedades
        private void FormPago_Load(object sender, EventArgs e)
        {

            CargarDatosVehiculo();
        }

        private void CargarDatosVehiculo()
        {
            Connection db = new Connection();

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Consulta usando el nombre real de tu columna: id_vehiculos
                    string query = "SELECT id_vehiculos, placa, tipo, hora_entrada FROM Vehiculos WHERE espacio_id = @espacioId;";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@espacioId", idEspacioCobrar);

                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extraemos los datos de las columnas de tu esquema
                                idVehiculo = Convert.ToInt32(reader["id_vehiculos"]);
                                placaVehiculo = reader["placa"].ToString();
                                tipoVehiculo = reader["tipo"].ToString();
                                horaEntrada = Convert.ToDateTime(reader["hora_entrada"]);

                                // --- CÁLCULO DE TIEMPO ---
                                DateTime horaSalida = DateTime.Now;
                                TimeSpan tiempo = horaSalida - horaEntrada;

                                int horasACobrar = (int)Math.Ceiling(tiempo.TotalHours);
                                if (horasACobrar <= 0) horasACobrar = 1;


                                // --- OBTENCIÓN DE TARIFAS DINÁMICAS DESDE LA CONFIGURACIÓN ---
                                // Leemos los precios actuales guardados en la BD. Si no existen, usa 1.00 y 2.00 como plan de respaldo.
                                double tarifaMoto = Convert.ToDouble(GestionConfig.ObtenerConfig("tarifa_moto", "1.00"));
                                double tarifaCarro = Convert.ToDouble(GestionConfig.ObtenerConfig("tarifa_carro", "2.00"));

                                // Asignamos la tarifa dependiendo del tipo de vehículo
                                double tarifaPorHora = (tipoVehiculo == "Moto") ? tarifaMoto : tarifaCarro;

                                // Calculamos el total con la tarifa de la configuración
                                double totalPagar = horasACobrar * tarifaPorHora;


                                // --- ASIGNACIÓN AL TEXTO DEL LABEL ---
                                lblInformacionPago.Text = $"--- DETALLE DE CUENTA ---\n\n" +
                                                          $"ID Espacio: {idEspacioCobrar}\n" +
                                                          $"Placa: {placaVehiculo}\n" +
                                                          $"Vehículo: {tipoVehiculo}\n" +
                                                          $"Tarifa por Hora: ${tarifaPorHora:F2}\n\n" + // Agregamos visualmente la tarifa aplicada
                                                          $"Entrada: {horaEntrada:dd/MM/yyyy HH:mm:ss}\n" +
                                                          $"Salida: {horaSalida:dd/MM/yyyy HH:mm:ss}\n" +
                                                          $"Tiempo: {horasACobrar} Hora(s)\n\n" +
                                                          $"TOTAL A PAGAR: ${totalPagar:F2}";

                                // Guardamos el total en el Tag del botón para el cobro
                                btnConfirmarPago.Tag = totalPagar;
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron registros de vehículos en este espacio.",
                                                "Espacio Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al conectar o leer la base de datos: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        // Evento del botón "Pagar" de tu interfaz
        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (btnConfirmarPago.Tag == null) return;

            double totalPagar = Convert.ToDouble(btnConfirmarPago.Tag);
            DateTime horaSalida = DateTime.Now;

            Connection db = new Connection();

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    using (SqliteTransaction transaccion = connection.BeginTransaction())
                    {
                        // 1. Insertar cobro
                        string queryPago = "INSERT INTO Pagos (vehiculo_id, [hora_salida], [total_pagar]) VALUES (@vehiculoId, @horaSalida, @totalPagar);";
                        using (SqliteCommand cmdPago = new SqliteCommand(queryPago, connection, transaccion))
                        {
                            cmdPago.Parameters.AddWithValue("@vehiculoId", idVehiculo);
                            cmdPago.Parameters.AddWithValue("@horaSalida", horaSalida.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmdPago.Parameters.AddWithValue("@totalPagar", totalPagar);
                            cmdPago.ExecuteNonQuery();
                        }

                        // 2. Eliminar vehículo activo
                        string queryBorrar = "DELETE FROM Vehiculos WHERE id_vehiculos = @idVehiculo;";
                        using (SqliteCommand cmdBorrar = new SqliteCommand(queryBorrar, connection, transaccion))
                        {
                            cmdBorrar.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                            cmdBorrar.ExecuteNonQuery();
                        }

                        // 3. Liberar el espacio en el mapa
                        string queryEspacio = "UPDATE Espacios SET estado = 'Disponible' WHERE id_espacio = @espacioId;";
                        using (SqliteCommand cmdEspacio = new SqliteCommand(queryEspacio, connection, transaccion))
                        {
                            cmdEspacio.Parameters.AddWithValue("@espacioId", idEspacioCobrar);
                            cmdEspacio.ExecuteNonQuery();
                        }

                        transaccion.Commit();

                        MessageBox.Show("¡Pago procesado con éxito! El espacio se ha liberado.",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar la transacción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Evento del botón "Cancelar" de tu interfaz

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
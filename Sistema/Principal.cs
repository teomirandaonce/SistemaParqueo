#nullable disable // <- Con esto le decimos a .NET 10 que apague las advertencias de nulos en este archivo
using Microsoft.Data.Sqlite;
using Sistema;
using System;
using System.Drawing; // Asegúrate de tener esta directiva para Size y Color
using System.Windows.Forms;

namespace Sistema
{
    public partial class Principal : Form
    {
        // Declaramos esta variable aquí arriba para usarla en cualquier botón
        private string idEspacioSeleccionado = "";

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            // Forzamos a que el combo box no permita escritura manual desde el arranque
            cmbTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;

            CargarMapaParqueo();

            //Instanciamos nuestra clase de conexión
            Connection db = new Connection();

            //Intentamos conectar con la base de datos
            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // Consulta real basada en tu tabla 'Espacios' y columna 'estado'
                    string query = "SELECT COUNT(*) FROM Espacios WHERE estado = 'Disponible';";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        // ExecuteScalar devuelve un long en Microsoft.Data.Sqlite
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
            // 1. Limpieza absoluta de los paneles
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
                                // Lectura directa y segura gracias al #nullable disable
                                string idEspacio = reader["id_espacio"].ToString();
                                string numero = reader["numero"].ToString();
                                string estado = reader["estado"].ToString();
                                string seccion = reader["seccion"].ToString();

                                // 2. Crear el botón físico
                                Button btnEspacio = new Button();
                                btnEspacio.Text = $"Espacio {numero}\n[{estado}]";
                                btnEspacio.Size = new Size(100, 70);
                                btnEspacio.Font = new Font("Arial", 9, FontStyle.Bold);
                                btnEspacio.Margin = new Padding(10);
                                btnEspacio.Tag = idEspacio;

                                // 3. Configurar color
                                if (estado.Trim().ToLower() == "disponible")
                                {
                                    btnEspacio.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    btnEspacio.BackColor = Color.Salmon;
                                }

                                // 4. Enlazar el evento de clic
                                btnEspacio.Click += BotonEspacio_Click;

                                // 5. Filtrado flexible de secciones para evitar fallos de mayúsculas o espacios
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

                    // Mensaje de diagnóstico por si la tabla no tiene datos todavía
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

                // Extraemos las líneas del texto del botón (Línea 0: Nombre, Línea 1: [Estado])
                string[] lineas = botonPresionado.Text.Split('\n');
                string nombreEspacio = lineas[0];
                string estadoEspacio = lineas.Length > 1 ? lineas[1] : "";

                // Actualizamos el Label mostrando claramente si está libre u ocupado
                lblSeleccionado.Text = $"Seleccionado: {nombreEspacio} {estadoEspacio} (ID: {idEspacioSeleccionado})";

                // Preselección para parqueo de motos o carros automáticamente
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
            // 1. Invocamos la validación de tu clase externa pasándole las variables de la interfaz
            if (!Validaciones.EsEspacioAptoParaCobrar(idEspacioSeleccionado, lblSeleccionado.Text))
            {
                // Si la clase determina que no es apto, se detiene el flujo aquí mismo
                return;
            }

            // 2. Si la validación devuelve true, procedemos a abrir la ventana de pago con seguridad
            int idEspacio = Convert.ToInt32(idEspacioSeleccionado);

            FormPago ventanaPago = new FormPago(idEspacio);
            ventanaPago.StartPosition = FormStartPosition.CenterParent;

            if (ventanaPago.ShowDialog() == DialogResult.OK)
            {
                // Limpieza y actualización del mapa dinámico si el pago fue exitoso
                idEspacioSeleccionado = "";
                lblSeleccionado.Text = "Ningún espacio seleccionado";
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;

                CargarMapaParqueo(); // Redibuja los botones a verde en vivo
            }
        }

        private void btnAsignarParqueo_Click(object sender, EventArgs e)
        {
            // --- NUEVAS VALIDACIONES OPTIMIZADAS DESDE LA CLASE ESTÁTICA ---
            if (!Validaciones.ValidarSeleccion(idEspacioSeleccionado)) return;
            if (!Validaciones.ValidarEspacioOcupado(lblSeleccionado.Text)) return;
            if (!Validaciones.ValidarTipoVehiculo(cmbTipoVehiculo.SelectedIndex)) return;
            if (!Validaciones.ValidarPlacaTexto(txtPlaca.Text)) return;

            string placa = txtPlaca.Text.Trim().ToUpper();
            string tipoVehiculo = cmbTipoVehiculo.SelectedItem.ToString();

            // Escudo de exclusividad para el panel del Parqueo 2 (Motos)
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

            // Conexión y operaciones directas
            Connection db = new Connection();

            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    // VALIDACIÓN DE PLACA OPTIMIZADA DESDE LA CLASE
                    if (!Validaciones.ValidarPlacaDuplicada(placa, connection)) return;

                    // Procesamos la transacción física si pasó todos los filtros anteriores
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

                        // Limpieza de controles
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

            // Si la ventana devuelve DialogResult.OK significa que se hizo un reset completo
            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                // Limpiamos las cajas de texto de selección del menú lateral por seguridad
                idEspacioSeleccionado = "";
                lblSeleccionado.Text = "Ningún espacio seleccionado";

                // Volvemos a generar el mapa en tiempo real leyendo la base de datos limpia
                CargarMapaParqueo();
            }
        }
    }
}
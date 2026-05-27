#nullable disable
using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Sistema
{
    public static class Validaciones
    {
        public static bool ValidarSeleccion(string idEspacio)
        {
            if (string.IsNullOrEmpty(idEspacio))
            {
                MessageBox.Show("Por favor, seleccione primero un espacio de parqueo en el mapa.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool ValidarEspacioOcupado(string textoLabel)
        {
            if (textoLabel.Contains("[Ocupado]") || textoLabel.ToLower().Contains("ocupado"))
            {
                MessageBox.Show("Este espacio ya se encuentra ocupado. Por favor, elija un parqueo disponible (Verde).",
                                "Espacio No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool ValidarPlacaTexto(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                MessageBox.Show("Por favor, ingrese la placa del vehículo.",
                                "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool ValidarTipoVehiculo(int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione el tipo de vehículo (Carro o Moto) en el panel de control.",
                                "Tipo de Vehículo Faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool ValidarPlacaDuplicada(string placa, SqliteConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Vehiculos WHERE placa = @placa;";
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@placa", placa.Trim().ToUpper());
                long conteo = (long)command.ExecuteScalar();

                if (conteo > 0)
                {
                    MessageBox.Show($"¡Ingreso Denegado! El vehículo con placa '{placa.ToUpper()}' ya se encuentra registrado dentro del parqueo.",
                                    "Vehículo Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        public static bool EsEspacioAptoParaCobrar(string idEspacio, string textoEstadoLabel)
        {
            if (string.IsNullOrEmpty(idEspacio))
            {
                MessageBox.Show("Por favor, seleccione primero un espacio ocupado en el mapa.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textoEstadoLabel.Contains("[Disponible]") ||
                textoEstadoLabel.ToLower().Contains("disponible") ||
                textoEstadoLabel.Contains("Ningún espacio"))
            {
                MessageBox.Show("¡Operación no válida! No se puede cobrar un espacio que se encuentra disponible.",
                                "Espacio Libre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Sistema
{
    public static class GestionConfig
    {
        public static string ObtenerConfig(string clave, string valorPorDefecto)
        {
            Connection db = new Connection();
            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT valor FROM Configuracion WHERE clave = @clave;";
                    using (SqliteCommand cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@clave", clave);

                        object? result = cmd.ExecuteScalar();

                        return result != null ? result.ToString() : valorPorDefecto;
                    }
                }
                catch
                {
                    return valorPorDefecto;
                }
            }
        }

        public static string ObtenerConfig(string clave)
        {
            string resultado = ObtenerConfig(clave, "0");

            return resultado ?? "0";
        }

        public static void GuardarConfig(string clave, string nuevoValor)
        {
            Connection db = new Connection();
            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    string query = "INSERT OR REPLACE INTO Configuracion (clave, valor) VALUES (@clave, @valor);";
                    using (SqliteCommand cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@clave", clave);
                        cmd.Parameters.AddWithValue("@valor", nuevoValor);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static bool ResetearParqueoCompleto(int cantidadSeccion1, int cantidadSeccion2)
        {
            Connection db = new Connection();
            using (SqliteConnection connection = db.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqliteTransaction transaccion = connection.BeginTransaction())
                    {
                        string queryConfig = "INSERT OR REPLACE INTO Configuracion (clave, valor) VALUES (@clave, @valor);";

                        using (SqliteCommand cmd = new SqliteCommand(queryConfig, connection, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@clave", "total_espacios_seccion1");
                            cmd.Parameters.AddWithValue("@valor", cantidadSeccion1.ToString());
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@clave", "total_espacios_seccion2");
                            cmd.Parameters.AddWithValue("@valor", cantidadSeccion2.ToString());
                            cmd.ExecuteNonQuery();
                        }

                        new SqliteCommand("DELETE FROM Vehiculos;", connection, transaccion).ExecuteNonQuery();
                        new SqliteCommand("DELETE FROM Espacios;", connection, transaccion).ExecuteNonQuery();

                        string insertEspacio = "INSERT INTO Espacios (numero, estado, seccion) VALUES (@n, 'Disponible', @s);";

                        for (int i = 1; i <= cantidadSeccion1; i++)
                        {
                            using (SqliteCommand cmd = new SqliteCommand(insertEspacio, connection, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@n", i);
                                cmd.Parameters.AddWithValue("@s", "Seccion 1");
                                cmd.ExecuteNonQuery();
                            }
                        }

                        for (int i = 1; i <= cantidadSeccion2; i++)
                        {
                            using (SqliteCommand cmd = new SqliteCommand(insertEspacio, connection, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@n", cantidadSeccion1 + i);
                                cmd.Parameters.AddWithValue("@s", "Seccion 2");
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error crítico en el reseteo: {ex.Message}", "Error de Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
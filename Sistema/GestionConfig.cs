using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Sistema
{
    public static class GestionConfig
    {
        // 1. Obtener configuración con un valor por defecto (Evita errores si la BD está vacía)
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

                        // Usamos el operador '?' para indicar que 'result' puede ser nulo temporalmente
                        object? result = cmd.ExecuteScalar();

                        // Si result no es nulo, devuelve su texto; si es nulo, devuelve el valor por defecto de forma segura
                        return result != null ? result.ToString() : valorPorDefecto;
                    }
                }
                catch
                {
                    return valorPorDefecto;
                }
            }
        }

        // 2. Sobrecarga corregida para eliminar el Warning CS8603 de forma definitiva
        public static string ObtenerConfig(string clave)
        {
            string resultado = ObtenerConfig(clave, "0");

            // Si por alguna razón extrema el método de arriba diera null, nos aseguramos con un operador de fusión de nulos (??)
            return resultado ?? "0";
        }

        // 3. Guardar o actualizar una configuración individual (Tarifas)
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

        // 4. Resetear el parqueo completo regenerando las tablas
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

                        // Guardar las nuevas cantidades máximas
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

                        // Limpiar los autos parqueados actualmente y los espacios viejos
                        new SqliteCommand("DELETE FROM Vehiculos;", connection, transaccion).ExecuteNonQuery();
                        new SqliteCommand("DELETE FROM Espacios;", connection, transaccion).ExecuteNonQuery();

                        // Preparar la inserción de los nuevos espacios
                        string insertEspacio = "INSERT INTO Espacios (numero, estado, seccion) VALUES (@n, 'Disponible', @s);";

                        // Generar Sección 1 (Carros)
                        for (int i = 1; i <= cantidadSeccion1; i++)
                        {
                            using (SqliteCommand cmd = new SqliteCommand(insertEspacio, connection, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@n", i);
                                cmd.Parameters.AddWithValue("@s", "Seccion 1");
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Generar Sección 2 (Motos) - Empieza correlativo después de los carros
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
using System;
using Microsoft.Data.Sqlite;

namespace Sistema
{
    public class Connection
    {
        // Al estar en el .NET moderno, la ruta básica busca directamente en la carpeta del ejecutable
        private readonly string connectionString = "Data Source=Parqueo.db;";

        public SqliteConnection ObtenerConexion()
        {
            return new SqliteConnection(connectionString);
        }
    }
}
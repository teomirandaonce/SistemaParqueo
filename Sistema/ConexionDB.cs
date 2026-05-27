using System;
using Microsoft.Data.Sqlite;

namespace Sistema
{
    public class Connection
    {
        private readonly string connectionString = "Data Source=Parqueo.db;";

        public SqliteConnection ObtenerConexion()
        {
            return new SqliteConnection(connectionString);
        }
    }
}
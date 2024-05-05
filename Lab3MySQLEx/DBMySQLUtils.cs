using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Lab3MySQLEx
{
    class DBMySQLUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            string connString = $"Server={host};Database={database};Port={port};User Id={username};Password={password}";
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;


        }
    }
}
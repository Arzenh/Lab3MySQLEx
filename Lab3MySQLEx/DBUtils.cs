using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;


namespace Lab3MySQLEx
{
    internal class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "DESKTOP-8TD8BQ8"; 
            int port = 3306; 
            string database = "splatforelectr";
            string username = "monty"; 
            string password = "some_pass";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}

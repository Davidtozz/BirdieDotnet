using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BirdieDotnet
{
    class DatabaseManager
    {
        private string connectionString = "server=localhost;uid=root;password=root;database=test;";

        public void Connect()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Perform database operations here

                connection.Close();
            }
        }
    }
}

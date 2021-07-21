using MySql.Data.MySqlClient;
using System;

namespace RouletteBettingAPI.DataAccess
{
    public class ConnectionDataAccess
    {
        public static MySqlConnection GetMySqlConnection(string mySqlConnectionString)
        {
            MySqlConnection connectionMySql;
            try
            {
                connectionMySql = new MySqlConnection(mySqlConnectionString);
                connectionMySql.Open();
                return connectionMySql;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

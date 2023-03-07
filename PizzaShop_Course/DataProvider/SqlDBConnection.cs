using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public class SqlDBConnection
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "pizzas_shop";
            string username = "root";
            string password = "sofuck";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}

using System.Data.Common;
using System.Data.SqlClient;

namespace OnlineTalent.Database
{
    public class ConnectionFactory
    {
        public static string ConnectionString { get; set; }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }

        public static void GetCloseConnection(DbConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

    }
}

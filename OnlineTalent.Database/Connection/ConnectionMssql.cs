using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace OnlineTalent.Database
{
    public class ConnectionMssql : ConnectionBase, IConnectionFactory
    {
        public string ConnectionString { get; set; }

        public override void ConnectionBaseString(string connectionString)
        {
            //  base.ConnectionString(connectionString);
            ConnectionString = connectionString;
        }


        public void GetCloseConnection(IDbConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}

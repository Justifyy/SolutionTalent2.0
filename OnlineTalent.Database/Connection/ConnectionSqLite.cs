using System;
using System.Data;
using System.Data.Common;

namespace OnlineTalent.Database
{
    public class ConnectionSqLite: ConnectionBase, IConnectionFactory
    {
        public string ConnectionString { get; set; }

        public override void ConnectionBaseString(string connectionString)
        {
            base.ConnectionBaseString(connectionString);
        }
        public void GetCloseConnection(IDbConnection connection)
        {
            throw new NotImplementedException();
        }

        public DbConnection GetOpenConnection()
        {
            throw new NotImplementedException();
        }
    }
}

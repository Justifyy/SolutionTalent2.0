using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Database
{
    public class ConnectionMysql : ConnectionBase, IConnectionFactory
    {
        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

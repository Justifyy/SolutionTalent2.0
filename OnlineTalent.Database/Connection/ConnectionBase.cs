using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Database
{
    public class ConnectionBase
    {
        private string _connectionBaseString = string.Empty;
        public virtual void ConnectionBaseString(string connectionString)
        {
            _connectionBaseString = connectionString;
        }
    }
}

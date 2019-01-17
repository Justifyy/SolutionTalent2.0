using System.Data;
using System.Data.Common;

namespace OnlineTalent.Database
{
    public interface IConnectionFactory
    {
        string ConnectionString { get; set; }
        DbConnection GetOpenConnection();
        void GetCloseConnection(IDbConnection connection);
    }
}

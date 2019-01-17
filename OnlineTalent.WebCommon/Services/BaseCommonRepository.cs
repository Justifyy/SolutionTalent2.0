using OnlineTalent.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.WebCommon.Services
{
    public class BaseCommonRepository<T> where T : class,new()
    {
        public static Repository<T> BaseRepository(string ConnStr="")
        {
            //string ConnStr = ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;

            IConnectionFactory connectionFactory = new ConnectionMssql
            {
                ConnectionString = ConnStr
            };

            Repository<T> repository = new Repository<T>(connectionFactory);

            return repository;
        }
    }
}

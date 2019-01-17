using OnlineTalent.Core.Extensions;
using OnlineTalent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.WebCommon.Services
{
    public class ControllerPageServices<T, TPage> where T : class,new() where TPage : class,new()
    {
        public static ReturnOutput SetDeletedWithUpdate(int id, bool isDeleted)
        {
            var repository = BaseCommonRepository<T>.BaseRepository();
            ReturnOutput returnOutput = new ReturnOutput();

            T entity = repository.FindById(id, ref returnOutput);
            object value = new object();
            value = (object)isDeleted;

            var item = entity.GetType().GetProperty("IsDeleted");
            item.SetValue(entity, value);

            repository = BaseCommonRepository<T>.BaseRepository();
            var retVal = repository.Update(entity, ref returnOutput);

            return returnOutput;
        }

        public static List<TPage> GetAllDataWithMap(string whereQuery)
        {
            string baseWhereQuery = " Where IsDeleted=0";

            if (!string.IsNullOrEmpty(whereQuery))
                baseWhereQuery = baseWhereQuery + " AND " + whereQuery;

            ReturnOutput returnOutput = new ReturnOutput();

            var repository = BaseCommonRepository<T>.BaseRepository();
            var data = repository.Find(baseWhereQuery, ref returnOutput);

            var retVal = MappingExtensions.ToModel<T, TPage>(data.ToList());

            return retVal;
        }

        public static List<T> GetAllData(string whereQuery)
        {
            string baseWhereQuery = " Where IsDeleted=0";

            if (!string.IsNullOrEmpty(whereQuery))
                baseWhereQuery = whereQuery;

            //baseWhereQuery = baseWhereQuery + " AND " + whereQuery;

            ReturnOutput returnOutput = new ReturnOutput();

            var repository = BaseCommonRepository<T>.BaseRepository();
            var data = repository.Find(baseWhereQuery, ref returnOutput);

            return data.ToList();
        }
    }
}

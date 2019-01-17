using OnlineTalent.Core.Models;
using System.Collections.Generic;

namespace OnlineTalent.Database
{
    public interface IRepository<T> where T : class
    {
      //  T Find(params object[] id);
      //  IQueryable<T> Where(Expression<Func<bool, T>> predicate);
        T FindById(int id, ref ReturnOutput returnOutput);
        IEnumerable<T> Find(string whereQuery, ref ReturnOutput returnOutput);
        int Add(T entity,ref ReturnOutput returnOutput);
        T Update(T entity, ref ReturnOutput returnOutput);
        void Delete(T entity, ref ReturnOutput returnOutput);
    }
}

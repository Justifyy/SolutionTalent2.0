using OnlineTalent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineTalent.Interfaces
{
    interface IBaseRepository<T> where T : class
    {
        T Find(params object[] id);
        IQueryable<T> Where(Expression<Func<bool, T>> predicate, ref ReturnOutput returnOutput);
        T FindById(int id, ref ReturnOutput returnOutput);
        IEnumerable<T> Find(string whereQuery, ref ReturnOutput returnOutput);
        int Add(T entity, ref ReturnOutput returnOutput);
        T Update(T entity, ref ReturnOutput returnOutput);
        void Delete(T entity, ref ReturnOutput returnOutput);
        T SaveAndUpdate(T entity, ref ReturnOutput returnOutput);
    }
}

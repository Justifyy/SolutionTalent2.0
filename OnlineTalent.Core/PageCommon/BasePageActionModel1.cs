using OnlineTalent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTalent.Core.PageCommon
{
    public class BaseActionModel1<T> : IBasePageActionModel1<T>
    {
        public ReturnOutput DeleteData(int id)
        {
            throw new NotImplementedException();
        }
         
        public List<T> GetData()
        {
            throw new NotImplementedException();
        }

        public T GetDataWithId(int id)
        {
            throw new NotImplementedException();
        }

        public ReturnOutput SaveData(T entity)
        {
            throw new NotImplementedException();
        }

        public T UpdateData(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
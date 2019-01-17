using OnlineTalent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.PageCommon
{
    interface IBasePageActionModel1<T>
    {
        T GetDataWithId(int id);

        List<T> GetData();

        ReturnOutput SaveData(T entity);

        T UpdateData(T entity);

        ReturnOutput DeleteData(int id);
    }
}

using OnlineTalent.Core.Models;
using System.Collections.Generic;


namespace OnlineTalent.Interfaces
{
    public interface IBasePageActionModel<T>
    {
        List<T> GetData(ref ReturnOutput returnOutput);

        List<T> GetData(int pageCount, int showDataCount, string sortName, ref ReturnOutput returnOutput);

        T GetDataWithId(int id, ref ReturnOutput returnOutput, string extensionPathName = "");
        
        T SaveAndUpdate(T entity, ref ReturnOutput returnOutput, string extensionPathName="");

        ReturnOutput DeleteData(string fileName, string fullPath);

        ReturnOutput MoveJsonData(string fileName, string fullPath, string destinationPath);
    }
}

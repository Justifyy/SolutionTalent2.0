using System;
using System.Collections.Generic;
using OnlineTalent.Core.Models;
using OnlineTalent.JsonData;
using OnlineTalent.Core.Common;
using System.Reflection;
using System.IO;

namespace OnlineTalent.Interfaces
{
    public class BasePageActionModel<T> : IBasePageActionModel<T>
    {
        private string filePath = "";
        private string logFilePath = "";
        public BasePageActionModel(string _filePath,string _logFilePath)
        {
            filePath = _filePath;
            logFilePath = _logFilePath;
            HelperText.LogPath = logFilePath;
            JsonRepositoryService<T> repo = new JsonRepositoryService<T>(filePath, logFilePath);          
        }

        public BasePageActionModel(string _filePath)
        {
            filePath = _filePath;
            logFilePath = _filePath + "\\Log\\";

            HelperText.LogPath = logFilePath;
            JsonRepositoryService<T> repo = new JsonRepositoryService<T>(filePath, logFilePath);
        }

        private Type typeEntity = typeof(T);

        public List<T> GetData(ref ReturnOutput returnOutput)
        {
            var result = JsonRepositoryService<T>.GetReadAllJsonData(typeEntity.Name,ref returnOutput);

            //Type t = entity.GetType();

            LogHelper.WriteLog("BasePageActionModel", "GetData : " + typeEntity.Name  + Environment.NewLine + " ErrorCode = " +  returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);

            return result;
        }

        /// <summary>
        /// 
 
        /// </summary>
        /// Image eklerken alt klasör yolu gerekli
        /// <param name="returnOutput"></param>
        /// <returns></returns>
        public List<T> GetDataWithPath(string filePath, ref ReturnOutput returnOutput)
        {
            var result = JsonRepositoryService<T>.GetReadAllJsonData(filePath, ref returnOutput);

            //Type t = entity.GetType();

            LogHelper.WriteLog("BasePageActionModel", "GetData : " + typeEntity.Name + Environment.NewLine + " ErrorCode = " + returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);

            return result;
        }

        public List<T> GetReadJsonFileData(string filePath, ref ReturnOutput returnOutput)
        {
            var result = JsonRepositoryService<T>.GetReadJsonFileData(filePath, ref returnOutput);

            LogHelper.WriteLog("BasePageActionModel", "GetData : " + typeEntity.Name + Environment.NewLine + " ErrorCode = " + returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);

            return result;
        }

        

        /// <summary>
        /// Paging işlemi yapıldığında veya sorting yapıldığında kullanılacak
        /// </summary>
        /// <param name="pageCount"></param>
        /// <param name="showDataCount"></param>
        /// <param name="sortName"></param>
        /// <param name="returnOutput"></param>
        /// <returns></returns>
        public List<T> GetData(int pageCount, int showDataCount, string sortName, ref ReturnOutput returnOutput)
        {
            var retVal = JsonRepositoryService<T>.GetReadAllJsonData(typeEntity.Name, pageCount, showDataCount, sortName, ref returnOutput);

            string inputs = "pageCount = " + pageCount.ToString() + " showDataCount = " + showDataCount + " sortName " + sortName;

            LogHelper.WriteLog("BasePageActionModel", "GetData : " + typeEntity.Name + Environment.NewLine + " Inputs : " + inputs + Environment.NewLine + "ErrorCode = " + returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);
            return retVal;
        }

        /// <summary>
        /// Id ye göre json datayı yakalayıp döndürür
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnOutput"></param>
        /// <returns></returns>
        public T GetDataWithId(int id, ref ReturnOutput returnOutput,string extensionPathName = "")
        {
            string jsonFilePath = id.ToString();

            if (!string.IsNullOrEmpty(extensionPathName))
                jsonFilePath = extensionPathName + "\\" + id.ToString();

            var result = JsonRepositoryService<T>.ReadJsonAsObject(jsonFilePath, ref returnOutput);

            LogHelper.WriteLog("BasePageActionModel", "GetDataWithId : " + typeEntity.Name + Environment.NewLine +  " Inputs :  Id = " + id.ToString() + Environment.NewLine + "ErrorCode = " + returnOutput.ErrorCode + "  ErrorMessage = " + returnOutput.ErrorMessage);

            return result;
        }


        public List<T> ReadJsonAsObjectListAllData(string jsonFileName, ref ReturnOutput returnOutput)
        {
            var result = JsonRepositoryService<T>.ReadJsonAsObjectListAllData(jsonFileName, ref returnOutput);

            LogHelper.WriteLog("BasePageActionModel", "ReadJsonAsObjectListAllData : " + typeEntity.Name + Environment.NewLine + " Inputs :  jsonFileName = " + jsonFileName.ToString() + Environment.NewLine + " ErrorCode = " + returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);

            return result;
        }

        /// <summary>
        ///    https://stackoverflow.com/questions/17817407/generic-method-return-type-as-type-parameter
        /// </summary>

        public T SaveAndUpdate(T entity, ref ReturnOutput returnOutput, string extensionPathName = "")
        {
            T retData = entity;

            Type t = entity.GetType();

            PropertyInfo prop = t.GetProperty("Id");
            object retId = prop.GetValue(entity);

            if (!ReferenceEquals(retId, null) && (int)retId > 0)
            {
                // Log işlemi yap
                var retValUpdate = UpdateData(entity,ref returnOutput, extensionPathName);
            }
            else
            {
                LogHelper.WriteLog("SaveAndUpdate","InsertData");
                var result = SaveData(entity, extensionPathName);
                returnOutput = result;
            }

            return retData;
        }

        public ReturnOutput DeleteData(string fileName, string fullPath)
        {
            fullPath = fullPath + "\\" + fileName;
            var result = JsonRepositoryService<T>.DeleteJsonFile(fullPath);

            LogHelper.WriteLog("BasePageActionModel", "DeleteData : " + typeEntity.Name + Environment.NewLine +  " ErrorCode = " + result.ErrorCode + "  ErrorMessage = " + result.ErrorMessage);

            return result;
        }
        public ReturnOutput MoveJsonData(string fileName, string fullPath, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            string randomFileName = Path.GetRandomFileName() + "_" + fileName;

            destinationPath = destinationPath + "\\" + randomFileName;

            fullPath = fullPath + "\\" + fileName;
            var result = JsonRepositoryService<T>.MoveJsonFile(fullPath, destinationPath);

            LogHelper.WriteLog("BasePageActionModel", "MoveJsonData : " + typeEntity.Name + Environment.NewLine  + " ErrorCode = " + result.ErrorCode + "  ErrorMessage = " + result.ErrorMessage);

            return result;
        }

        /// <summary>
        /// Image eklerken alt klasör gerekli olduğu durumlarda
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="extensionPathName"></param>
        /// <returns></returns>
        private ReturnOutput SaveData(T entity,string extensionPathName)
        {
            var result = JsonRepositoryService<T>.WriteJson(entity, extensionPathName);

            LogHelper.WriteLog("BasePageActionModel", "SaveData : " + typeEntity.Name + Environment.NewLine + " ErrorCode = " + result.ErrorCode + "  ErrorMessage = " + result.ErrorMessage);

            return result;
        }
        private T UpdateData(T entity, ref ReturnOutput returnOutput, string extensionPathName)
        {
            string jsonFilePath = entity.GetType().Name;
            string result = string.Empty;

            // string fullPath = filePath + typeof(T).Name + "\\" + entity + ".json";

            JsonRepositoryService<T> jrs = new JsonRepositoryService<T>(filePath, logFilePath);

            var data = JsonRepositoryService<T>.UpdateJsonData(entity, ref returnOutput, extensionPathName);

            LogHelper.WriteLog("BasePageActionModel", "UpdateData : " + typeEntity.Name + Environment.NewLine + " ErrorCode = " + returnOutput.ErrorCode + " ErrorMessage = " + returnOutput.ErrorMessage);
             
            return data;
        }
    }
}
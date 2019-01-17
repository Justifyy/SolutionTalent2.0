using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineTalent.Core.Common;
using OnlineTalent.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;

namespace OnlineTalent.JsonData
{
    public class JsonRepositoryService<T>
    {        
        public static string filePath { get; set;}
      //  public static string logFilePath { get; set; }

        // private static string _filePath = @"d:\JsonData\";       
        // private static string sourceFolderPathName = "xxx";

        public JsonRepositoryService(string _filePath,string _logFilePath)
        {
            filePath = _filePath;
            HelperText.LogPath = _logFilePath;
            //JsonCoreServices.filePath = _filePath;
        }

        #region Write Json

        // Write Json
        // https://www.newtonsoft.com/json/help/html/SerializeWithJsonSerializerToFile.htm
        public static ReturnOutput WriteJson(T entity, string extensionPathName = "")
        {
            ReturnOutput returnOutput = new ReturnOutput();
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            try
            {
                int fileId = 0;
                string sourceFolderPathName = entity.GetType().Name;
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

                string fullPath = filePath + sourceFolderPathName;

                if (!string.IsNullOrEmpty(extensionPathName))
                {
                    fullPath = fullPath + "\\" + extensionPathName;
                    sourceFolderPathName = sourceFolderPathName + "\\" + extensionPathName;
                }

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                fullPath = fullPath + "\\" + JsonCoreServices.GetNewFileName(filePath, sourceFolderPathName, ref fileId);

                Type t = entity.GetType();
        
                PropertyInfo prop = t.GetProperty("Id");
            
                prop.SetValue(entity, fileId, null);

                

                using (StreamWriter file = File.CreateText(fullPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, entity);
                }
            }
            catch (Exception ex)
            {                
               string result = ex.Message.ToString();
               HelperText.CreateTextFile("WriteJson",result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }

            return returnOutput;
        }

        public static ReturnOutput WriteJson(string entity, string extensionPathName = "")
        {
            ReturnOutput returnOutput = new ReturnOutput();
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            try
            {
                int fileId = 0;
                string sourceFolderPathName = entity.GetType().Name;
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

                //       string fullPath = filePath + sourceFolderPathName + "\\" + JsonCoreServices.GetNewFileName(filePath,sourceFolderPathName,ref fileId);
                string fullPath = filePath + sourceFolderPathName;

                if (!string.IsNullOrEmpty(extensionPathName))
                {
                    fullPath = fullPath + "\\" + extensionPathName;
                    sourceFolderPathName = sourceFolderPathName + "\\" + extensionPathName;
                }

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                fullPath = fullPath + "\\" + JsonCoreServices.GetNewFileName(filePath, sourceFolderPathName, ref fileId);

                using (StreamWriter file = File.CreateText(fullPath))
                {
                    file.Write(entity);
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("WriteJson", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }

            return returnOutput;
        }
        #endregion

        #region Read JSON DATA
        // Read JSON
        //https://www.newtonsoft.com/json/help/html/ReadJson.htm
        public static string ReadJsonTest(string jsonFilePath)
        {
            string fullPath = filePath + jsonFilePath;

            JObject o1 = JObject.Parse(File.ReadAllText(fullPath));
            string ad = o1["Ad"].ToString();
            string no = o1["CustomerPropetiesList"][0]["No"].ToString();
            return ad;
        }

        public static string ReadJsonAsFile(string jsonFilePath)
        {
            string fullPath = filePath + jsonFilePath;
            string result = "";

            try
            {
                if (File.Exists(fullPath))
                 result = File.ReadAllText(fullPath);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                HelperText.CreateTextFile("ReadJsonAsFile", result);
            }

            return result;
        }

        public static T ReadJsonAsObject(string jsonFilePath)
        {
            try
            {
                string fullPath = filePath + typeof(T).Name + "\\" + jsonFilePath +".json";

                if (File.Exists(fullPath))
                {
                    string jsonData = File.ReadAllText(fullPath);
                    T resultData = JsonConvert.DeserializeObject<T>(jsonData);

                    return resultData;
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                HelperText.CreateTextFile("ReadJsonAsFile", result);
            }

            return default(T);
        }

        public static T ReadJsonAsObject(string jsonFilePath, ref ReturnOutput returnOutput)
        {
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            try
            {
                string fullPath = filePath + typeof(T).Name + "\\" + jsonFilePath + ".json";

                if (File.Exists(fullPath))
                {
                    string jsonData = File.ReadAllText(fullPath);
                    T resultData = JsonConvert.DeserializeObject<T>(jsonData);

                    return resultData;
                }

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = "File Not Exist";
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                HelperText.CreateTextFile("ReadJsonAsFile", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }

            return default(T);
        }

        public static List<T> ReadJsonAsObjectList(string jsonFilePath)
        {
           List<T> resultList = new List<T>();

            try
            {
                string fullPath = filePath + jsonFilePath;

                DirectoryInfo di = new DirectoryInfo(fullPath);
                foreach (var item in di.GetFiles())
                {
                    var fileData = File.ReadAllText(item.FullName);
                    JObject o1 = JObject.Parse(fileData);
       
                    var result =  o1.ToObject<T>();
                    resultList.Add(result);
                    //result = new List<T>((IEnumerable<T>)o1);
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                HelperText.CreateTextFile("ReadJsonAsObjectList", result);
            }

            return resultList;
        }

        public static List<T> ReadJsonAsObjectListAllData(string jsonFileName, ref ReturnOutput returnOutput)
        {
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            List<T> resultList = null;
            try
            {
                string fullPath = filePath + jsonFileName;

                var jsonText = File.ReadAllText(fullPath, Encoding.GetEncoding("Windows-1254"));
                resultList = JsonConvert.DeserializeObject<List<T>>(jsonText);
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                HelperText.CreateTextFile("ReadJsonAsObjectListAllData", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }

            return resultList;
        }

        /// <summary>
        ///  // Read All Json File
        //https://stackoverflow.com/questions/35431900/get-all-json-files-from-a-folder-and-then-serialize-in-a-single-json-file-usin
        /// </summary>
        /// <param name="entity">GetAllJsonData(T entity)</param>
        /// <returns>List<T></returns>
        public static List<T> GetReadAllJsonData(string folderName, ref ReturnOutput returnOutput)
        {
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            List<T> list = new List<T>();
            string fullPath = filePath + folderName;

            DirectoryInfo di = new DirectoryInfo(fullPath);
            try
            {
                if (di.GetFiles().Length > 0)
                {
                    foreach (var file in di.GetFiles())
                    {
                        using (StreamReader fi = File.OpenText(file.FullName))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            T customer = (T)serializer.Deserialize(fi, typeof(T));
                            list.Add(customer);
                        }
                    }
                }
                else
                {
                    using (StreamReader fi = File.OpenText(fullPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        T customer = (T)serializer.Deserialize(fi, typeof(T));
                        list.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("GetReadAllJsonData", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }
            //finally
            //{
            //    return list;
            //}

            return list;
        }

        public static List<T> GetReadJsonFileData(string fullFileName, ref ReturnOutput returnOutput)
        {
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            List<T> list = new List<T>();
            string fullPath = fullFileName;

            try
            {
                using (StreamReader fi = File.OpenText(fullPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    T customer = (T)serializer.Deserialize(fi, typeof(T));
                    list.Add(customer);
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("GetReadAllJsonData", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }
            return list;
        }

        public static List<T> GetReadAllJsonData(string folderName, int pageCount, int showDataCount, string sortName, ref ReturnOutput returnOutput)
        {
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            List<T> list = new List<T>();
            string fullPath = filePath + folderName;

            DirectoryInfo di = new DirectoryInfo(fullPath);

            int fileStartCount = ((pageCount * showDataCount)- showDataCount)+1;  // 11
            int fileFinishCount = pageCount * showDataCount; //

            if (fileFinishCount>di.GetFiles().Length)
            {
                fileFinishCount = di.GetFiles().Length;
            }

            //var data = di.GetFiles().OrderBy(s => s.FullName);

                //Where(s => s.FullName == fileStartCount.ToString() + ".json");

            try
            {
                foreach (var file in di.GetFiles())
                {
                    using (StreamReader fi = File.OpenText(file.FullName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        T customer = (T)serializer.Deserialize(fi, typeof(T));
                        list.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("GetReadAllJsonData", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }
            //finally
            //{
            //    return list;
            //}

            return list;
        }

        public static List<string> GetReadAllJsonDataReturnString(string folderName)
        {
            List<string> list = new List<string>();
            string fullPath = filePath + folderName;

            DirectoryInfo di = new DirectoryInfo(fullPath);

            try
            {
                foreach (var file in di.GetFiles())
                {
                    string result = File.ReadAllText(file.FullName);
                    list.Add(result);

                }
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("GetReadAllJsonDataReturnString", result);
            }


            return list;
        }
        #endregion

        #region Update JSON DATA
        //  Change Json File
        //https://stackoverflow.com/questions/21695185/change-values-in-json-file-writing-files
        // https://www.newtonsoft.com/json/help/html/ModifyJson.htm
        public static string ChangeJsonData(string jsonFilePath)
        {
            string result = "OK";
            try
            {
                string fullPath = filePath + jsonFilePath;
                string json = File.ReadAllText(fullPath);

                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                jsonObj["Ad"] = "Numan";

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(fullPath, output);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                HelperText.CreateTextFile("ChangeJsonData", result);
            }

            return result;
        }

        public static T UpdateJsonData(T entity, ref ReturnOutput returnOutput, string extensionPathName)
        {
            string errResult = string.Empty;

            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            Type t = entity.GetType();

            PropertyInfo prop = t.GetProperty("Id");
            object retId = prop.GetValue(entity);

            T data = entity;

            try
            {
                string fullPath = filePath + typeof(T).Name + "\\" + retId.ToString() + ".json";

                if(!string.IsNullOrEmpty(extensionPathName))
                    fullPath = filePath + typeof(T).Name + "\\" + extensionPathName  + "\\" + retId.ToString() + ".json";


                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

                    using (StreamWriter file = File.CreateText(fullPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, entity);
                    }
                }
                else
                {
                    errResult = "İşlem yapılacak dosya yok. " + fullPath;
                    HelperText.CreateTextFile("UpdateJsonData", errResult);
                    returnOutput.ErrorCode = "1002";
                    returnOutput.ErrorMessage = errResult;
                }
            }
            catch (Exception ex)
            {
               errResult = ex.Message.ToString();
               HelperText.CreateTextFile("UpdateJsonData", errResult);

               returnOutput.ErrorCode = "1001";
               returnOutput.ErrorMessage = errResult;
            }

            return data;
        }

        public static T UpdateJsonData(string jsonFilePath, T entity, ref ReturnOutput returnOutput)
        {
            string errResult = string.Empty;

            T data = entity;
            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            try
            {
                string fullPath = filePath + jsonFilePath;
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

                    using (StreamWriter file = File.CreateText(fullPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, entity);
                    }
                }
                else
                {
                    errResult = "İşlem yapılacak dosya yok. " + fullPath;
                    HelperText.CreateTextFile("UpdateJsonData", errResult);
                    returnOutput.ErrorCode = "1002";
                    returnOutput.ErrorMessage = errResult;
                }
            }
            catch (Exception ex)
            {
                errResult = ex.Message.ToString();
                HelperText.CreateTextFile("UpdateJsonData", errResult);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = errResult;
            }

            return data;
        }

        public static string UpdateJsonData(string jsonFilePath, string entity,ref ReturnOutput returnOutput)
        {
            string errResult = string.Empty;

            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";

            try
            {
                string fullPath = filePath + jsonFilePath;
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

                    using (StreamWriter file = File.CreateText(fullPath))
                    {
                        file.Write(entity);
                    }
                }
                else
                {
                    errResult = "İşlem yapılacak dosya yok. " + fullPath;
                    HelperText.CreateTextFile("UpdateJsonData", errResult);
                    returnOutput.ErrorCode = "1002";
                    returnOutput.ErrorMessage = errResult;
                }
            }
            catch (Exception ex)
            {
                errResult = ex.Message.ToString();
                HelperText.CreateTextFile("UpdateJsonData", errResult);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = errResult;
            }

            return entity;
        }

        #endregion

        #region Delete and Movement File Process
        /// <summary>
        /// Json dosya silme işlemi burad File SystemObject ile yapılır.
        /// </summary>
        /// <param name="DeleteJsonFile"></param>
        /// <returns>string jsonFilePath</returns>
        public static ReturnOutput DeleteJsonFile(string fullPath)
        {
            ReturnOutput returnOutput = new ReturnOutput();

            returnOutput.ErrorCode = "1000";
            returnOutput.ErrorMessage = "Ok";
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("DeleteJsonFile", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }

            return returnOutput;
        }

        /// Json dosya taşıma işlemi burad File SystemObject ile yapılır.
        /// typeName demek Contenet demek Model in Classın adı demek
        public static ReturnOutput MoveJsonFile(string fullPath, string destinationPath)
        {
            ReturnOutput returnOutput = new ReturnOutput
            {
                ErrorCode = "1000",
                ErrorMessage = "Ok"
            };

            try
            {
                FileInfo fi = new FileInfo(fullPath);

                File.Move(fullPath, destinationPath);
            }
            catch (Exception ex)
            {
                string result = ex.Message.ToString();
                HelperText.CreateTextFile("MoveJsonFile", result);

                returnOutput.ErrorCode = "1001";
                returnOutput.ErrorMessage = result;
            }
            return returnOutput;
        }

        #endregion
    }
}

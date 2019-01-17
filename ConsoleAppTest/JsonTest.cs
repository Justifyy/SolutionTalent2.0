using OnlineTalent.Core.Models;
using OnlineTalent.Interfaces;
using OnlineTalent.JsonData;
using System;
using System.Collections.Generic;
using TalentBaseModel.DataModel;

namespace ConsoleAppTest
{
    public class JsonTest
    {
        private static string _dataPath = @"D:\Disk1\Projeler\test.com\JsonData\tr-TR\";
        private static string _logPath = @"D:\Disk1\Projeler\test.com\JsonData\tr-TR\Log\";

        /// <summary>
        /// Bütün İmageları getirmek için
        /// </summary>
        public static void GetReadJsonFile()
        {
            ReturnOutput returnOutput = new ReturnOutput();

            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            string filePath = _dataPath + "ContentCategory\\4.json";

            var result = baseAction.GetReadJsonFileData(filePath, ref returnOutput);
            Console.WriteLine(result.Count);

            //List<CountryModel> data =  JsonRepositoryService<CountryModel>.ReadJsonAsObjectListAllData("Town_Turkey.json",ref returnOutput);
        }

        public static void ReadCascadingDataHeader()
        {
            ReturnOutput returnOutput = new ReturnOutput();
            BasePageActionModel<ContentHeader> baseActionContentHeader = new BasePageActionModel<ContentHeader>(_dataPath, _logPath);
            List<ContentHeader> contentHeaderList = baseActionContentHeader.GetData(ref returnOutput);

            BasePageActionModel<ContentCategory> baseActionContentCategory = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            List<ContentCategory> contentCategoryList = baseActionContentCategory.GetData(ref returnOutput);


            //BasePageActionModel<Content> baseActionContent = new BasePageActionModel<Content>(_dataPath, _logPath);
            //List<Content> contentList = baseActionContent.GetData(ref returnOutput);

            //var topMenuModelList = new List<TopMenuModel>();
            //foreach (var item in contentCategoryList)
            //{
            //    var contentHeaderData = contentHeaderList.Where(s => s.Id == item.ContentHeaderId).FirstOrDefault();
            //    var topMenuModel = new TopMenuModel
            //    {
            //        AddressCategoryId = 1

            //    };

            //    topMenuModelList.Add(topMenuModel);
            //}
            //contentCategoryList[0].ContentHeaderId = "";

            int deger = 0;
        }

        public static void ReadJsonAsObjetListAllData()
        {
            ReturnOutput returnOutput = new ReturnOutput();
            _dataPath = @"D:\JsonData\Dernek\Town\";
            BasePageActionModel<Content> baseAction = new BasePageActionModel<Content>(_dataPath, _logPath);
            var result = baseAction.ReadJsonAsObjectListAllData("Town_Turkey.json", ref returnOutput);

            //List<CountryModel> data =  JsonRepositoryService<CountryModel>.ReadJsonAsObjectListAllData("Town_Turkey.json",ref returnOutput);
        }

        public static void GetFileName()
        {
            int fileId = 0;

            string path = JsonCoreServices.GetNewFileName(_dataPath, "Content", ref fileId);
        }

        public static void MoveJsonData()
        {
            ContentCategory contentCategory = new ContentCategory();
            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.MoveJsonData("", "", "ContentCategory");
            Console.WriteLine(result);
        }

        public static void DeleteData()
        {
            ContentCategory contentCategory = new ContentCategory();
            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.DeleteData("", "");
            Console.WriteLine(result);
        }

        public static void SaveAndUpdate()
        {
            // 31.03.2018 00:00:00
            ContentCategory contentCategory = new ContentCategory();
            contentCategory.ContentCategoryName = "Test";
            contentCategory.ContentCategoryDescription = "Test";
            contentCategory.StartDate = DateTime.Now;
            contentCategory.EndDate = DateTime.Now;
            contentCategory.Id = 0;

            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.SaveAndUpdate(contentCategory, ref returnOutput);
            Console.WriteLine(result);
        }
        public static void GetReadAllJsonData()
        {
            BasePageActionModel<Content> baseAction = new BasePageActionModel<Content>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.GetData(ref returnOutput);
            //var result = baseAction.GetData(1,2,"",ref returnOutput);
            Console.WriteLine(result.Count);
        }

        public static void GetData()
        {
            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.GetData(ref returnOutput);
            Console.WriteLine(result.Count);
        }

        public static void GetDataWithId()
        {

            BasePageActionModel<ContentCategory> baseAction = new BasePageActionModel<ContentCategory>(_dataPath, _logPath);
            ReturnOutput returnOutput = new ReturnOutput();

            var result = baseAction.GetDataWithId(21, ref returnOutput);
            Console.WriteLine(result.ToString());
        }


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.JsonData
{
    public class JsonCoreServices
    {
        //public static string filePath { get; set; }

        //private static string filePath = @"D:\JsonData\";
        private static string fileExtension = ".json";
        public static string GetNewFileName(string _filePath, string folderName, ref int fileId)
        {
            string fileName = "";
            string fullPath = _filePath + folderName;
            DirectoryInfo di = new DirectoryInfo(fullPath);
            if (di.Exists)
            {
                fileId = 1;
                var sorted = di.GetFiles().OrderByDescending(f => f.CreationTime).FirstOrDefault();

                if (!ReferenceEquals(sorted, null))
                    fileId = GetLastFileName(di.GetFiles().Select(o => o.Name).ToArray()) + 1;

                // fileId = (int.Parse(sorted.Name.Replace(sorted.Extension.ToString(), "")) + 1);

                fileName = fileId.ToString() + fileExtension;
            }
            else
            {
                Directory.CreateDirectory(fullPath);
                fileId = 1;
                fileName = "1" + fileExtension;
            }

            return fileName;
        }

        private static int GetLastFileName(string[] files)
        {
            List<int> fileDataList = new List<int>();
            foreach (var item in files)
            {
                fileDataList.Add(int.Parse(item.ToString().Replace(".json","")));
            }

            int retVal = fileDataList.Max();
            return retVal;
        }

        private static IEnumerable<FileInfo> GetFilesByExtensions(DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");

            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension));
        }
    }
}

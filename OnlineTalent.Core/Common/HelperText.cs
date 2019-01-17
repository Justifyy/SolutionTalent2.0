using System;
using System.IO;

namespace OnlineTalent.Core.Common
{
    public class HelperText
    {
        public static string LogPath { get; set; }
        public static void CreateTextFile(string fileName1, string data)
        {
            string day = DateTime.Today.Day < 10 ? "0" + DateTime.Today.Day : DateTime.Today.Day.ToString();
            string month = DateTime.Today.Month < 10 ? "0" + DateTime.Today.Month : DateTime.Today.Month.ToString();
            string year = DateTime.Today.Year.ToString();

            string filename = year + "." + month + "." + day;

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            string fullFileName = LogPath + @"\" + filename;

            if (!string.IsNullOrEmpty(fileName1))
                fullFileName = fullFileName + "_" + fileName1;

            fullFileName = fullFileName + ".txt";

            using (StreamWriter writer = new StreamWriter(fullFileName, true))
            {
                writer.WriteLine("--------------------------------------");
                writer.WriteLine("İşlem Başladı Tarih: " + DateTime.Now);
                writer.WriteLine(data);
                writer.Close();
            }
        }

    }
}

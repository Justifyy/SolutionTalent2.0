using OnlineTalent.Log;
using OnlineTalent.Log.NLogg;
using System.Reflection;

namespace ConsoleAppTest
{
    public class NLogTest
    {

        public static void LogTest()
        {
            string method = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);
            ReceiverData receiverData = new ReceiverData(@"D:\Deger");
            receiverData.Log("de","Test");
        }

        public static void LogNTest()
        {
            //  Class1.Log();
            string fileLogPath = @"D:\hata\110.log";

            NLogFile nf = new NLogFile();
            
            nf.LogFilePath(fileLogPath);
            nf.SetConfigFile();
            nf.WriteLog("Hello World 12 !");

            NLogConsole nc = new NLogConsole();
            nc.SetConfigFile();
            nc.WriteLog("Hello World !");
        }
    }
}

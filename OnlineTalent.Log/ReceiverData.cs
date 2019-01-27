using OnlineTalent.Log.Core;
using OnlineTalent.Log.NLogg;
using System;

namespace OnlineTalent.Log
{
    public class ReceiverData
    {
        public string _FileLogPath { get; set; }

        //private string _FileLogPath;
        public ReceiverData(string fileLogPath="")
        {            
            _FileLogPath = fileLogPath;

            if (string.IsNullOrEmpty(_FileLogPath))
            {
                BaseNLog baseNLog = new BaseNLog();
                _FileLogPath = baseNLog.GetAssemblyFolder();
             }
        }

        public void Log(string methodName ,string logText)
        {
            string baseLogText = methodName + Environment.NewLine + logText;
            _FileLogPath = _FileLogPath + "\\" + methodName.ToString() + ".log";
            LogNFile(baseLogText);
            LogNConsole(baseLogText);
        }

        private void LogNFile(string logText)
        {
            NLogFile nf = new NLogFile();

            nf.LogFilePath(_FileLogPath);
            nf.SetConfigFile();
            nf.WriteLog(logText);
        }

        private void LogNConsole(string logText)
        {
            NLogConsole nc = new NLogConsole();
            nc.SetConfigFile();
            nc.WriteLog(logText);
        }
    }
}

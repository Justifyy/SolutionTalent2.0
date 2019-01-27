using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Log.Core
{
    public class BaseNLog
    {
        private string _logFilePath;
        public BaseNLog()
        {
            //string BaseFilePath { get; set; }
        }
             
        public virtual void LogFilePath(string logFilePath="")
        {
            _logFilePath = GetAssemblyFolder();
        }

        public string GetAssemblyFolder()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //assemblyFolder = assemblyFolder.Replace("bin\\Debug","");
            // NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(assemblyFolder + "\\NLog.config", true);

            return assemblyFolder;
        }
    }
}

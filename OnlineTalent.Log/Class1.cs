using NLog;
using System.IO;
using System.Reflection;

namespace OnlineTalent.Log
{
    public class Class1
    {
        public static void Log()
        {
            string filePath = @"D:\log11.txt";

            //string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //assemblyFolder = assemblyFolder.Replace("bin\\Debug","");
            // NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(assemblyFolder + "\\NLog.config", true);

            string assemblyFolder = @" D:\Disk1\Projeler\Visual Studio 2017\Projects\SolutionTalent2.0\OnlineTalent.Log\";
            var config = new NLog.Config.XmlLoggingConfiguration(assemblyFolder + "\\NLog.config", true);

            var logfile = new NLog.Targets.FileTarget() { FileName = filePath, Name = "logfile" };
            var logconsole = new NLog.Targets.ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Debug, logfile));
            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));

            NLog.LogManager.Configuration = config;

            /*
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget() { FileName = filePath, Name = "logfile" };
            //var logconsole = new NLog.Targets.ConsoleTarget() { Name = "logconsole" };

           // config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));
          //  config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Debug, logfile));

            NLog.LogManager.Configuration = config;
            */
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Hello World");
        }
    }
}

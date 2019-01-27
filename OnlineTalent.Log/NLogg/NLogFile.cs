using NLog;
using OnlineTalent.Log.Core;

namespace OnlineTalent.Log.NLogg
{
    public class NLogFile : BaseNLog, ISetConfig,IWrite
    {
        private string _logFilePath;
        // public string BaseFilePath { get; set; }

        public NLogFile()
        {
            //  ConfigFile();
        }

        public override void LogFilePath(string logFilePath = "")
        {
            _logFilePath = logFilePath;

        }

        public void SetConfigFile()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget() { FileName = _logFilePath, Name = "logfile" };
            var logconsole = new NLog.Targets.ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));
            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Debug, logfile));

            NLog.LogManager.Configuration = config;
        }

        public void WriteLog(string logData)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(logData);
        }
    }
}

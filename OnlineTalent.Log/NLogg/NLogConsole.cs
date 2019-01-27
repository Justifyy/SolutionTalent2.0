using NLog;
using OnlineTalent.Log.Core;

namespace OnlineTalent.Log.NLogg
{
    public class NLogConsole :BaseNLog, ISetConfig, IWrite
    {
        private readonly string _logFilePath;
        public NLogConsole()
        {
            // NLog Console
        }
        //  public string BaseFilePath { get; set; }

        public override void LogFilePath(string logFilePath = "")
        {
            base.LogFilePath(logFilePath);
        }

        public void SetConfigFile()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logconsole = new NLog.Targets.ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));

            NLog.LogManager.Configuration = config;
        }
        public void WriteLog(string logData)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(logData);
        }
    }
}

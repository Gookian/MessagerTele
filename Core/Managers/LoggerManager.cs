using NLog;
using NLog.Config;
using NLog.Targets;

namespace Core.Managers
{
    public class LoggerManager
    {
        public Logger logger;

        public enum LogLeavel
        {
            Trace = 0,
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5
        };

        private LoggingConfiguration _config;
        private FileTarget _logfile;
        private LogLevel _min;
        private LogLevel _max;

        public LoggerManager(string nameFile, LogLeavel min, LogLeavel max)
        {
            _config = new LoggingConfiguration();
            _logfile = new FileTarget(nameFile.Split(new char[] { '.' })[0])
            {
                FileName = nameFile
            };

            switch ((int)min)
            {
                case 0:
                    _min = LogLevel.Trace;
                    break;
                case 1:
                    _min = LogLevel.Debug;
                    break;
                case 2:
                    _min = LogLevel.Info;
                    break;
                case 3:
                    _min = LogLevel.Warn;
                    break;
                case 4:
                    _min = LogLevel.Error;
                    break;
                case 5:
                    _min = LogLevel.Fatal;
                    break;
            };

            switch ((int)max)
            {
                case 0:
                    _max = LogLevel.Trace;
                    break;
                case 1:
                    _max = LogLevel.Debug;
                    break;
                case 2:
                    _max = LogLevel.Info;
                    break;
                case 3:
                    _max = LogLevel.Warn;
                    break;
                case 4:
                    _max = LogLevel.Error;
                    break;
                case 5:
                    _max = LogLevel.Fatal;
                    break;
            };

            _config.AddRule(_min, _max, _logfile);
            LogManager.Configuration = _config;

            logger = LogManager.GetCurrentClassLogger();
        }
    }
}

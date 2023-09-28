using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System.Reflection.Metadata;

namespace WebAPI.Helpers
{
    public sealed class WebApiLogger
    {

        private static volatile WebApiLogger instance;
        private static object syncRoot = new object();
        private readonly Logger logger;
        private WebApiLogger()
        {
            logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        public static WebApiLogger GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new WebApiLogger();
                }
            }

            return instance;
        }

        public void LogInformation(string log)
        {
            if (logger == null) return;

            logger.Info(log);
        }

        public void LogDebug(string log, Exception ex = null)
        {
            if (logger == null) return;

            if (ex == null)
                logger.Debug(log);
            else
                logger.Debug(ex, log);
        }

        public void LogError(string log, Exception ex = null)
        {
            if (logger == null) return;

            if (ex == null)
                logger.Error(log);
            else
                logger.Error(ex, log);
        }

        public void LogWarning(string log, Exception ex = null)
        {
            if (logger == null) return;

            if (ex == null)
                logger.Warn(log);
            else
                logger.Warn(ex, log);

        }
    }
}

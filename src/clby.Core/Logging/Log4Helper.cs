using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace clby.Core.Logging
{
    public sealed class Log4Helper
    {
        public static ILoggerRepository LoggerRepository =
            LogManager.CreateRepository("clby.Core");

        static Log4Helper()
        {
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
        }




    }
}

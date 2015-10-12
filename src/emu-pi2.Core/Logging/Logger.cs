using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroLog;
using MetroLog.Targets;

namespace emu_pi2.Core.Logging
{
    public class Logger
    {
        private static Logger _instance = null;

        private Logger()
        {
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new FileStreamingTarget());
        }

        public static Logger Current
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();
                return _instance;
            }
        }

        public void Log(string message)
        {
            var log = LogManagerFactory.DefaultLogManager.GetLogger(typeof(Logger));
            log.Trace(message);
        }
    }
}

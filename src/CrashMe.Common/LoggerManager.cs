using System;
using System.Collections.Generic;

namespace CrashMe.Common {


    /// <summary>
    /// 
    /// </summary>
    public static class LoggerManager {

        private static IList<ILogger> Loggers = new List<ILogger>();

        /// <summary>
        /// 
        /// </summary>
        static LoggerManager() {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public static void Add(ILogger logger) {
            Loggers.Add(logger);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void Error(Exception ex, string message = null) {
            foreach (var logger in Loggers) {
                try {
                    logger.Error(ex, message);
                } catch {
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void Warn(string message) {
            foreach (var logger in Loggers) {
                try {
                    logger.Warn(message);
                } catch {
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message) {
            foreach (var logger in Loggers) {
                try {
                    logger.Log(message);
                } catch {
                }
            }

        }

    }
}

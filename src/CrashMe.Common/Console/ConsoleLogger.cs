using System;
using System.Text;

namespace CrashMe.Common {

    /// <summary>
    /// 
    /// </summary>
    public sealed class ConsoleLogger : ILogger {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void Error(Exception ex, string message = null) {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(message)) {
                stringBuilder.AppendLine(message);
            }
            while (ex != null) {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stringBuilder.ToString());
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message) {
            Console.WriteLine(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

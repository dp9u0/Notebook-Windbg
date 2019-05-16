using System;
using System.Text;
using System.Threading;

namespace CrashMe.Common.Console
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class ConsoleLogger : ILogger
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void Error(Exception ex, string message = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{DateTime.Now:yyyyMMdd hh:mm:ss.fff} [{Thread.CurrentThread.ManagedThreadId}]:");
            if (!string.IsNullOrEmpty(message)) stringBuilder.AppendLine(message);
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(stringBuilder.ToString());
            System.Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            System.Console.WriteLine($"{DateTime.Now:yyyyMMdd hh:mm:ss.fff} [{Thread.CurrentThread.ManagedThreadId}]: {message}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine($"{DateTime.Now:yyyyMMdd hh:mm:ss.fff} [{Thread.CurrentThread.ManagedThreadId}]: {message}");
            System.Console.ResetColor();
        }

    }

}
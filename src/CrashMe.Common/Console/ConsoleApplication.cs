using System.Diagnostics;
using System.Runtime;

namespace CrashMe.Common.Console
{

    /// <summary>
    /// Application For Console
    /// </summary>
    public class ConsoleApplication
    {

        /// <summary>
        /// Start Application
        /// </summary>
        public void Start()
        {
            var consoleLogger = new ConsoleLogger();
            LoggerManager.Add(consoleLogger);
            consoleLogger.Warn($"Application {Process.GetCurrentProcess().Id} Started...");
            while (true)
            {
                System.Console.Write("> ");
                var input = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) CrasherContainer.Instance.Run(input);
            }

            // ReSharper disable once FunctionNeverReturns
        }

    }

}
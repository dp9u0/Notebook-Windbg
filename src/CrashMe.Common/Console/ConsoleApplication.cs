using System;

namespace CrashMe.Common {

    /// <summary>
    /// 
    /// </summary>
    public class ConsoleApplication {
        public void Start() {
            var consoleLogger = new ConsoleLogger();
            LoggerManager.Add(consoleLogger);
            consoleLogger.Warn("Application Started...");
            while (true) {
                var input = Console.ReadLine();
                CrasherContainer.Instance.Run(input);
            }
        }

    }
}

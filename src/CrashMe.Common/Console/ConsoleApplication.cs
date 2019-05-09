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
                Console.Write("> ");
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) {
                    CrasherContainer.Instance.Run(input);
                }
            }
        }

    }
}

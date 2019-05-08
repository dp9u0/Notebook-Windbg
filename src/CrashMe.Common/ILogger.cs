using System;

namespace CrashMe.Common {
    public interface ILogger {

        void Error(Exception ex, string message = null);

        void Log(string message);

        void Warn(string message);
    }
}

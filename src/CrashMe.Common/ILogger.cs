using System;

namespace CrashMe.Common
{

    /// <summary>
    /// Interface for logger
    /// </summary>
    public interface ILogger
    {

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="ex">error</param>
        /// <param name="message">message</param>
        void Error(Exception ex, string message = null);

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="message">message</param>
        void Log(string message);

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">message</param>
        void Warn(string message);

    }

}
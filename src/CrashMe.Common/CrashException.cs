using System;

namespace CrashMe.Common
{

    /// <summary>
    /// Crash Exception
    /// </summary>
    public class CrashException : Exception
    {

        /// <summary>
        /// 
        /// </summary>
        public CrashException() : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="catchIt"></param>
        public CrashException(Exception innerException, bool catchIt = true) : base("Crash Exception", innerException)
        {
            CatchIt = catchIt;
        }

        public bool CatchIt { get; }

    }

}
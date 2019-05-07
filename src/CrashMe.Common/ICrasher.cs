namespace CrashMe.Common {

    /// <summary>
    /// Interface Crasher 
    /// </summary>
    public interface ICrasher {

        /// <summary>
        /// Crasher Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command
        /// </summary>
        string Command { get; }

        /// <summary>
        /// Run Crasher
        /// </summary>
        void Run(RunArgs args=null);
    }
}

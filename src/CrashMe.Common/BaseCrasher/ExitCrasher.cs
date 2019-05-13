using System;

namespace CrashMe.Common.BaseCrasher
{

    /// <summary>
    /// Exit Crasher
    /// </summary>
    internal class ExitCrasher : CrasherBase
    {

        /// <summary>
        /// ExitCrasher
        /// </summary>
        public ExitCrasher() : base("Exit Application", "q")
        {
        }

        /// <summary>
        /// Help
        /// </summary>
        public override string Help => "";

        /// <summary>
        /// RunCore
        /// </summary>
        /// <param name="args">argument</param>
        protected override void RunCore(RunArgs args)
        {
            LoggerManager.Warn("Going To Exit...");
            Environment.Exit(0);
        }

    }

}
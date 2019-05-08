using System;

namespace CrashMe.Common {

    internal class ExitCrasher : CrasherBase {
        public ExitCrasher() : base("Exit Application", "q") { }

        public override string Help => "";

        protected override void RunCore(RunArgs args) {
            LoggerManager.Warn("Going To Exit...");
            Environment.Exit(0);
        }
    }
}

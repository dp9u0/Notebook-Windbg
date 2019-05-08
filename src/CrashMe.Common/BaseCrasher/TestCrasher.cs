namespace CrashMe.Common {

    /// <summary>
    /// Test
    /// </summary>
    internal class TestCrasher : CrasherBase {
        public TestCrasher() : base("Test Crasher", "t") { }

        public override string Help => "";

        protected override void RunCore(RunArgs args) {
            LoggerManager.Log(string.Join(" ", args.Arguments));
        }
    }
}

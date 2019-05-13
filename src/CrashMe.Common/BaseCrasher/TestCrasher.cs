namespace CrashMe.Common.BaseCrasher
{

    /// <summary>
    /// Test
    /// </summary>
    internal class TestCrasher : CrasherBase
    {

        /// <summary>
        /// Test Crasher
        /// </summary>
        public TestCrasher() : base("Test Crasher", "t")
        {
        }

        /// <summary>
        /// Help
        /// </summary>
        public override string Help => "";

        /// <summary>
        ///  Do Nothing
        /// </summary>
        /// <param name="args"></param>
        protected override void RunCore(RunArgs args)
        {
            LoggerManager.Log(string.Join(" ", args.Arguments));
        }

    }

}
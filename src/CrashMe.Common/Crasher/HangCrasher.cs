using System.Threading;

namespace CrashMe.Common {

    /// <summary>
    /// Start Some Thread And Hang it For A few Seconds
    /// </summary>
    internal class HangCrasher : CrasherBase {

        private static object syncobj = new object();

        public HangCrasher() : base("Start Some Thread And Hang it For A few Seconds", "hang") { }

        public override string Help => "hang : hang 3 thread for 5 second as default\n" +
            "hang 5 30 : hang 5 thread for 30 seconds";

        protected override void RunCore(RunArgs args) {
            int threadCount = 0;
            if (!args.TryGetFirstAsInt(out threadCount)) {
                threadCount = 3;
            }
            for (int i = 0; i < threadCount; i++) {
                var thread = new Thread(() =>
                {
                    System.DateTime start = System.DateTime.Now;
                    lock (syncobj) {
                        if (args.TryGetSecondAsInt(out int seconds)) {
                            Thread.Sleep(seconds * 1000);
                        } else {
                            Thread.Sleep(5 * 1000);
                        }
                    }
                    System.DateTime end = System.DateTime.Now;
                    LoggerManager.Warn($"{Thread.CurrentThread.Name} Hung for {end.Subtract(start).Seconds}.{end.Subtract(start).Milliseconds}");
                });
                thread.Name = $"Thread_{i}";
                thread.Start();
            }

        }
    }
}

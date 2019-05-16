using System;
using System.Runtime;

namespace CrashMe.Common.Crasher
{

    public class GcCrasher : CrasherBase
    {

        public GcCrasher() : base("Run GC.Collect()", "gc") { }

        public override string Help => "gc g: call GC.Collect() only don't compact any heap\n" +
                                       "gc s : gc with compact soh only\n" +
                                       "gc: gc with compact soh and loh";

        protected override void RunCore(RunArgs args)
        {
            int gen = 2;
            bool compact = true;
            bool compactLoh = true;
            if (args.TryGetFirst(out string mode))
            {
                switch (mode)
                {
                    case "s":
                        compactLoh = false;
                        break;
                    case "g":
                        compact = false;
                        compactLoh = false;
                        break;
                }
            }
            if (compactLoh)
            {
                GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            }
            GC.Collect(gen, GCCollectionMode.Forced, blocking: true, compact);
            GC.WaitForPendingFinalizers();
            GC.Collect(gen, GCCollectionMode.Forced, blocking: true, compact);
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.Default;
        }

    }

}
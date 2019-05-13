using System;

namespace CrashMe.Common.Crasher
{

    public class GcCrasher : CrasherBase
    {

        public GcCrasher() : base("Run GC.Collect()", "gc")
        {
        }

        public override string Help => "gc : call GC.Collect()";

        protected override void RunCore(RunArgs args)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            GC.Collect();
        }

    }

}
using System;
using System.Threading;

namespace CrashMe.Common.Crasher
{

    /// <summary>
    /// Throw Exception To Crash Application
    /// </summary>
    internal class ExceptionCrasher : CrasherBase
    {

        public ExceptionCrasher() : base("Crash Application", "ex")
        {
        }

        public override string Help => "ex : throw an 'System.Exception' and catch it finally\n" +
                                       "ex c : throw an 'System.Exception' but don not catch it. this will crash this application\n" +
                                       "ex f : create an object that when call finalizer will throw a exception\n" +
                                       "ex fn : create an object that's finalizer will throw a exception but explicit call Dispose to suppress finalizer\n" +
                                       "ex t : run thread throw a exception";

        protected override void RunCore(RunArgs args)
        {
            try
            {
                ThrowException();
            } catch (Exception e)
            {
                if (args.TryGetFirst(out string arg))
                {
                    switch (arg)
                    {
                        case "c":
                            throw new CrashException(e, false);
                        case "f":
                            // ReSharper disable once UnusedVariable
                            var objectWhichFinalizerThrowsException = new ObjectWhichFinalizerThrowsException();
                            break;
                        case "fn":
                            using (new ObjectWhichFinalizerThrowsException())
                            {

                            }
                            break;
                        case "t":
                            new Thread(() => throw new CrashException()).Start();
                            break;
                    }
                } else
                {
                    throw new CrashException(e);
                }
            }
        }

        private void ThrowException()
        {
            throw new Exception();
        }

        /// <summary>
        /// 
        /// </summary>
        private sealed class ObjectWhichFinalizerThrowsException : IDisposable
        {

            private string _null = string.Empty;

            private void ReleaseUnmanagedResources()
            {
            }

            private void Dispose(bool disposing)
            {
                ReleaseUnmanagedResources();
                if (disposing)
                {
                }
                _null = null;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~ObjectWhichFinalizerThrowsException()
            {
                Dispose(false);
                // Will Throw NullReferenceException
                if (_null.Equals(null))
                {
                }
            }

        }

    }

}
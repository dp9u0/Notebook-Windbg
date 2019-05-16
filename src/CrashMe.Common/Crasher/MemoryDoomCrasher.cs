using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace CrashMe.Common.Crasher
{

    /// <summary>
    /// </summary>
    public class MemoryDoomCrasher : CrasherBase
    {

        private static readonly Random Random = new Random((int) DateTime.Now.Ticks); //thanks to McAden
        private long _count;

        private Hashtable _storage = new Hashtable();

        public MemoryDoomCrasher() : base("Memory Doom Crasher", "m") { }

        public override string Help => "m : memory doom, add 100 string(default length = 100) to store\n" +
                                       "m a 100 10000: add 100 string(length = 10000) to store\n" +
                                       "m c : clean store\n" +
                                       "m l : display current count of storage\n" +
                                       "m n : renew storage";

        protected override void RunCore(RunArgs args)
        {
            if (args.TryGetFirst(out var action))
                switch (action)
                {
                    case "a":
                        if (args.TryGetSecondAsInt(out var count) && args.TryGetInt(2, out var size))
                        {
                            AddString(count, size);
                        } else
                        {
                            LoggerManager.Warn("Lack Arguments...");
                        }
                        break;
                    case "c":
                        _storage.Clear();
                        break;
                    case "l":
                        LoggerManager.Log($"Storage Count {_storage.Count}");
                        break;
                    case "n":
                        _storage = new Hashtable();
                        break;
                }
            else
                AddString(100, 100);
        }

        private void AddString(int count, int size)
        {
            for (var i = 0; i < count; i++)
            {
                _storage.Add($"C_{_count++}", new StringObject(RandomString(size)));
            }
        }

        private string RandomString(int size)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor((127 - 32) * Random.NextDouble() + 32)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private class StringObject
        {

            public StringObject(string str)
            {
                RandomString = str;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            private string RandomString { get; }

            // ReSharper disable once EmptyDestructor
            ~StringObject()
            {
                Thread.SpinWait(100);
            }
        }

    }

}
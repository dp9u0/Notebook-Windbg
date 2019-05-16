using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CrashMe.Common.Crasher
{

    /// <summary>
    /// 
    /// </summary>
    internal class MemoryLeakCrasher : CrasherBase
    {

        public MemoryLeakCrasher() : base("Memory Leak Crasher", "leak") { }

        public override string Help => "leak n 100: trigger a native memory leak for loop 100 times(default 100)\n" +
                                       "leak m 100: trigger a managed memory leak for loop 100 times(default 100)";

        private event EventHandler SomeEvent;

        protected override void RunCore(RunArgs args)
        {
            if (args.TryGetFirst(out var type))
            {
                if (!args.TryGetSecondAsInt(out int count))
                {
                    count = 100;
                }
                switch (type)
                {
                    case "m":
                        Task.Run((() =>
                        {
                            LoggerManager.Log($"ManagedLeak Work On {Thread.CurrentThread.ManagedThreadId} Begin");
                            try
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    ManagedLeak();
                                }
                            } catch (Exception e)
                            {
                                LoggerManager.Error(e);
                            }
                            LoggerManager.Log($"ManagedLeak Work On {Thread.CurrentThread.ManagedThreadId} End");
                        }));
                        break;
                    case "n":
                        Task.Run((() =>
                        {
                            LoggerManager.Log($"NativeLeak Work On {Thread.CurrentThread.ManagedThreadId} Begin");

                            try
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    NativeLeak($"Prod_{count}");
                                }
                            } catch (Exception e)
                            {
                                LoggerManager.Error(e);
                            }

                            LoggerManager.Log($"NativeLeak Work On {Thread.CurrentThread.ManagedThreadId} End");
                        }));
                        break;
                }
            }
        }

        private void ManagedLeak()
        {
            SomeEvent += new MemoryLeaked().SomeAction;
        }

        private static void NativeLeak(string productName)
        {
            Product p = new Product();
            ShippingInfo s = new ShippingInfo();
            p.ProductName = productName;
            s.Distributor = "Buggy Bits";
            s.DaysToShip = 5;
            p.ShippingInfo = s;
            Type[] extraTypes = new Type[1];
            extraTypes[0] = typeof(ShippingInfo);
            MemoryStream ms = new MemoryStream();

            // slower and lead to memory leak
            XmlSerializer xs = new XmlSerializer(typeof(Product), extraTypes);

            // XmlSerializer xs = new XmlSerializer(typeof(Product));
            // ref https://docs.microsoft.com/zh-cn/dotnet/api/system.xml.serialization.xmlserializer?redirectedfrom=MSDN&view=netframework-4.8#dynamically-generated-assemblies
            xs.Serialize(ms, p);
            ms.Close();
        }

        protected virtual void OnSomeEvent()
        {
            SomeEvent?.Invoke(this, EventArgs.Empty);
        }

    }

    public class MemoryLeaked
    {

        public void SomeAction(object sender, EventArgs args) { }

    }

    public class Product
    {

        public string ProductName;
        public ShippingInfo ShippingInfo;

    }

    public class ShippingInfo
    {

        public int DaysToShip;
        public string Distributor;

    }

}
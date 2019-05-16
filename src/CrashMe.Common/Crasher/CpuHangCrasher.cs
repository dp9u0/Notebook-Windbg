using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CrashMe.Common.Crasher
{

    /// <summary>
    /// Cpu Hang
    /// </summary>
    public class CpuHangCrasher : CrasherBase
    {

        public CpuHangCrasher() : base("High Cpu Hang", "cpu") { }

        public override string Help => "cpu : Start 5 busy threads to hang\n" +
                                       "cpu 10 10000: Start 10 threads ,and each thread create mass data of 10000 rows";

        private void CreateBigData(int loop)
        {
            var dt = new DataTable();
            var dc = new DataColumn("ID", typeof(Int32)) { Unique = true };
            dt.Columns.Add(dc);
            dt.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductsTable", typeof(string)));
            //string productsTable = string.Empty;
            for (var i = 0; i < loop; i++)
            {
                var dr = dt.NewRow();
                dr["id"] = i;
                dr["ProductName"] = "Product " + i;
                dr["Description"] = "Description for Product " + i;
                dr["Price"] = "$100";
                string productsTable =
                    "<table><tr><td><B>Product ID</B></td><td><B>Product Name</B></td><td><B>Description</B></td></tr>";
                productsTable += "<tr><td>" + dr[0] + "</td><td>" + dr[1] + "</td><td>" + dr[2] + "</td></tr>";
                productsTable += "</table>";
                dr["ProductsTable"] = productsTable;
                dt.Rows.Add(dr);
            }
        }

        protected override void RunCore(RunArgs args)
        {
            if (!args.TryGetFirstAsInt(out int threadCount))
            {
                threadCount = 5;
            }
            if (!args.TryGetSecondAsInt(out int loop))
            {
                loop = 1000000;
            }
            for (int i = 0; i < threadCount; i++)
            {
                Task.Run(() =>
                {
                    LoggerManager.Log($"Work On {Thread.CurrentThread.ManagedThreadId} Begin");
                    CreateBigData(loop);
                    LoggerManager.Log($"Work On {Thread.CurrentThread.ManagedThreadId} End");
                });
            }
        }

    }

}
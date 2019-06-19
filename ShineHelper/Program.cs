using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShineHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            GitHelper h = new GitHelper();
            h.GitSourcePath = @"https://github.com/shineforwu/mqtt_Test.git";
            h.GitLocalPath = @"D:\MyWorkspace\mqtt_Test";
            h.LocalPathDisk = "D";
            //h.Init();

            h.LocalBranchName = "Test";
            h.OriginBranchName = "Test";
            h.CheckOut();
            Console.WriteLine("is f:"+h.IsChangeAfterPull());
            Console.WriteLine(h.AfterPullMessage);
            Console.ReadKey();

        }
    }
}

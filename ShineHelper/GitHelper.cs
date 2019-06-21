using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author Shine 
/// 2019
/// </summary>
namespace ShineHelper
{
    /// <summary>
    /// Run cmd process to use git 
    /// </summary>
    public class GitHelper
    {
        public string GitSourcePath { get; set; }
        public string GitLocalPath { get; set; }
        public string OriginBranchName { get; set; } = "master";
        public string LocalBranchName { get; set; } = "master";

        public string LocalPathDisk { get; set; } = "C";

        public string LastLineMessage = string.Empty;

        private Process p = null;

        public string AfterPullMessage = string.Empty;

        public void RunTest()
        {
            GitHelper h = new GitHelper();
            h.GitSourcePath = @"https://github.com/shineforwu/mqtt_Test.git";
            h.GitLocalPath = @"D:\MyWorkspace\mqtt_Test";
            h.LocalPathDisk = "D";
            //h.Init();

            h.LocalBranchName = "Test";
            h.OriginBranchName = "Test";
            //h.CheckOut();
            //Console.WriteLine("is f:" + h.IsChangeAfterPull());
            //Console.WriteLine(h.AfterPullMessage);
            h.Commit("Test");
            h.Push();
        }
        public void CdLocalPath()
        {
            try
            {
                LastLineMessage = string.Empty;
                Console.WriteLine("CdLocalPath Start");
                p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;

                p.Start();
                string cdLocalPath = string.Empty;
                if (LocalPathDisk.ToUpper().Trim() != "C")
                {
                    cdLocalPath = LocalPathDisk + ":";
                    p.StandardInput.WriteLine(cdLocalPath);
                }
                p.StandardInput.WriteLine(@"cd " + GitLocalPath);
                Console.WriteLine("CdLocalPath over");
            }
            catch (Exception ex)
            {
                CloseProcess();
                throw ex;
            }


        }
        public void CloseProcess()
        {
            if (p != null)
            {
                p.Close();
            }
            p = null;
            Console.WriteLine("The Process trun null");
        }

        public void CheckOut()
        {
            CdLocalPath();
            string OutStr = string.Empty;
            string cmd = "git checkout -b  " + LocalBranchName;
            try
            {
                Console.WriteLine("CheckOut Start");
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                while (!p.StandardOutput.EndOfStream)
                {
                    LastLineMessage += p.StandardOutput.ReadLine();
                    LastLineMessage += "\n";
                    Console.WriteLine(LastLineMessage);
                }
                p.WaitForExit();
                p.Close();
                Console.WriteLine("CheckOut Over");

            }
            catch (Exception ex)
            {
                CloseProcess();
                throw ex;
            }

        }

        public string Pull()
        {
            CdLocalPath();
            string OutStr = string.Empty;
            string cmd = "git pull origin " + OriginBranchName;

            try
            {
                Console.WriteLine("Pull Start");
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                while (!p.StandardOutput.EndOfStream)
                {
                    OutStr += p.StandardOutput.ReadLine();
                    OutStr += "\n";
                    //Console.WriteLine(LastLineMessage);
                }
                p.WaitForExit();
                p.Close();
                Console.WriteLine("Pull Over");
            }
            catch (Exception ex)
            {
                CloseProcess();
                throw ex;
            }
            AfterPullMessage = OutStr;
            return OutStr;
        }
        public bool IsChangeAfterPull()
        {
            Console.WriteLine("IsChangeAfterPull Start");
            bool flag = false;
            Pull();
            if (AfterPullMessage.Contains("changed"))
            {
                flag = true;
            }
            Console.WriteLine("IsChangeAfterPull Over");
            return flag;
        }
        public void Stash()
        {
            CdLocalPath();
            string cmd = "git stash";
            try
            {
                Console.WriteLine("Stash Start");
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                while (!p.StandardOutput.EndOfStream)
                {
                    LastLineMessage += p.StandardOutput.ReadLine();
                    Console.WriteLine(LastLineMessage);
                }
                p.WaitForExit();
                p.Close();
                Console.WriteLine("Stash Over");
            }
            catch (Exception ex)
            {
                CloseProcess();

            }
        }

        public void Commit(string commitStr)
        {
            CdLocalPath();
            string cmd = "git commit -am '" + commitStr + "'";
            try
            {
                Console.WriteLine("Commit Start");
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                while (!p.StandardOutput.EndOfStream)
                {
                    LastLineMessage += p.StandardOutput.ReadLine();
                    LastLineMessage += "\n";
                    Console.WriteLine(LastLineMessage);
                }
                p.WaitForExit();
                p.Close();
                Console.WriteLine("Commit Over");
            }
            catch (Exception ex)
            {
                CloseProcess();

            }
        }

        public void Push()
        {
            CdLocalPath();
            string cmd = "git push origin " + LocalBranchName + ":"+ OriginBranchName;
            try
            {
                Console.WriteLine("Push Start");
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                while (!p.StandardOutput.EndOfStream)
                {
                    LastLineMessage += p.StandardOutput.ReadLine();
                    Console.WriteLine(LastLineMessage);
                }
                p.WaitForExit();
                p.Close();
                Console.WriteLine("Push Over");
            }
            catch (Exception ex)
            {
                CloseProcess();

            }
        }


    }
}

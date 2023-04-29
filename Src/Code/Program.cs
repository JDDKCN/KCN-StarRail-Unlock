using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using StarRailUnlock.Properties;

namespace StarRailUnlock
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //声明互斥体 使程序只能启动一个
            Mutex mutex = new Mutex(false, "KCNStarRailUnlock");
            bool Runing = !mutex.WaitOne(0, false);
            if (Runing)
            {
                MessageBox.Show("请勿重复启动程序！", "KCN-Server");
                Application.Exit();
                return;
            }

            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {

                Application.Run(new Home());
            }
            else
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Verb = "runas";

                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return;
                }

                Application.Exit();
            }

        }

        /// <summary>
        /// 加载嵌入的dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            if (new AssemblyName(args.Name).Name == "Newtonsoft.Json")
                return Assembly.Load(Resources.Newtonsoft_Json);
            return null;
        }

    }
}

using cspClient.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace cspClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException; //  程序异常处理

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException); // 设置未处理异常由Application.ThreadException处理

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            //为用户组指定对应目录的完全访问权限
            SetAccess("Users", Application.StartupPath);
            
            LoginForm fm1 = new LoginForm();
            fm1.ShowDialog();
            if (fm1.loginSucc)
            {
                Application.Run(new MainForm(fm1.loginName,fm1.userName));
                //Application.Run(new Form1());
            }
            else {
                Application.Exit();
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {

            // 释放消息队列相关资源
            AmqpUtils.GetInstance().Close();
            String mysqlPath = Environment.CurrentDirectory + "\\mysql";
            CmdHelper.execBat(mysqlPath + "\\shutdown_5.0.bat", "\"" + mysqlPath + "\" bio-work root root 50009");
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.WriteLogLine("程序异常：" + e.Exception.Source + ",方法:"
                + e.Exception.TargetSite.ToString() + ",错误信息：" + e.Exception.Message + ",堆栈信息:" + e.Exception.StackTrace);
            Application.Exit();
        }


        // 处理程序未捕获异常 防止系统弹出程序停止工作框
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.WriteLogLine("程序出现未捕获的异常:" + e.ExceptionObject.ToString());
            Application.Exit(); //有此句则不弹异常对话框
        }

        /// <summary>
        /// 为指定用户组，授权目录指定完全访问权限
        /// </summary>
        /// <param name="user">用户组，如Users</param>
        /// <param name="folder">实际的目录</param>
        /// <returns></returns>
        private static bool SetAccess(string user, string folder)
        {
            //定义为完全控制的权限
            const FileSystemRights Rights = FileSystemRights.FullControl;

            //添加访问规则到实际目录
            var AccessRule = new FileSystemAccessRule(user, Rights,
                InheritanceFlags.None,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow);

            var Info = new DirectoryInfo(folder);
            var Security = Info.GetAccessControl(AccessControlSections.Access);

            bool Result;
            Security.ModifyAccessRule(AccessControlModification.Set, AccessRule, out Result);
            if (!Result) return false;

            //总是允许再目录上进行对象继承
            const InheritanceFlags iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

            //为继承关系添加访问规则
            AccessRule = new FileSystemAccessRule(user, Rights,
                iFlags,
                PropagationFlags.InheritOnly,
                AccessControlType.Allow);

            Security.ModifyAccessRule(AccessControlModification.Add, AccessRule, out Result);
            if (!Result) return false;

            Info.SetAccessControl(Security);

            return true;
        }



        #region 设置程序单开 双开时返回之前未关闭进程的实例
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历进程 寻找同名进程
            foreach (Process process in processes)
            {
                // 忽略当前进程
                if (process.Id != current.Id)
                {
                    // 确保进程由当前exe启动
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        // 如果之前启动了就返回之前的实例
                        return process;
                    }
                }
            }
            return null;
        }
        public static void HandleRunningInstance(Process instance)
        {
            //避免窗体最小化或隐藏掉
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            //设置显示窗体
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        #endregion
    }
}

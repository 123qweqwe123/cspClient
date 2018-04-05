using AutoUpdate.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace AutoUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args) //  版本号信息  更新操作的状态
        {
            //args = new string[3];
            //args[0] = "D:\\visualstudioWorkspaces\\SM\\SM\\bin\\x86\\Debug\\updateTemp\\201708170001_950790578.zip";
            //args[1] = "f49ea4b245fc4ceff6e0b57258989000";
            //args[2] = "201708170001";
            Application.ThreadException += Application_ThreadException; //  程序异常处理

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException); // 设置未处理异常由Application.ThreadException处理

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //为用户组指定对应目录的完全访问权限
            SetAccess("Users", Application.StartupPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoUpdate(args[0], args[1], args[2]));
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            UpdateLog.WriteLogLine("程序更新异常：" + e.Exception.Message);
            Application.Exit();
        }


        // 处理程序未捕获异常 防止系统弹出程序停止工作框
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UpdateLog.WriteLogLine("程序更新出现未捕获的异常:" + e.ExceptionObject.ToString());
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
    }
}

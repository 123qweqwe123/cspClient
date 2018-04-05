using AutoUpdate.utils;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class AutoUpdate : Form
    {
        public String fileName;
        public String md5String;
        public String newVersion;

        //public AutoUpdate()
        //{
        //    InitializeComponent();
        //}

        public AutoUpdate(String p_fileName, String p_md5String, String p_version)
        {
            InitializeComponent();

            fileName = p_fileName;
            md5String = p_md5String;
            newVersion = p_version;
            Thread t1 = new Thread(new ThreadStart(updateProgrem));
            t1.Start();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            //File.Move("D:\\visualstudioWorkspaces\\SM\\SM\\bin\\x86\\Debug\\SM.exe", "D:\\visualstudioWorkspaces\\SM\\SM\\bin\\x86\\Debug\\SM测试.exe");
        }

        public void updateProgrem()
        {
            UpdateLog.WriteLogLine("程序开始自动更新,最新版本号:" + newVersion + ",当前时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                progressBar.Value = 10;
                UpdateLog.WriteLogLine("更新包路径:" + fileName);
                //String filePath = Environment.CurrentDirectory + "\\updateTemp"; // 更新包所在路径
                int i = 0;
                while (IsFileInUse("cspClient") && i <= 30) // 查询主程序是否已退出
                {
                    UpdateLog.WriteLogLine("更新操作开始延时...");
                    Thread.Sleep(500);
                    i++;
                }
                if (i > 30)
                { // 超过15秒就有问题 退出更新
                    UpdateLog.WriteLogLine("更新超时");
                    MessageBox.Show("更新操作超时,请重启程序或者重启电脑后重试！");
                    Application.Exit();
                }
                UpdateLog.WriteLogLine("更新开始");

                bool res = ZipHelper.UnZip(fileName, Environment.CurrentDirectory);
                if (res)
                {
                    XmlConfigHelper.SetValueByKey("config.xml", "CurVersion", newVersion);// 更新配置文件版本号
                }
                else
                {
                    MessageBox.Show("更新操作失败,请重启程序或者重启电脑后重试！");
                    this.Close();
                    Application.Exit();
                    return;
                }

                Thread.Sleep(100);

                progressBar.Value = 90;
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.FileName = "D:\\visualstudioWorkspaces\\cspClient\\cspClient\\bin\\x86\\Debug\\cspClient.exe";
                p.StartInfo.FileName = Environment.CurrentDirectory + "\\cspClient.exe";
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                //skinButton1.Text = "更新完成";
                //progressBar.Value = 100;
                this.Close();
                this.Dispose();
                //Application.Exit(); // 升级完成 功成身退

                Environment.Exit(System.Environment.ExitCode); // 可以避免win7时弹出程序停止的提示
            }
            catch (Exception e)
            {
                UpdateLog.WriteLogLine("更新程序出现异常:" + e.Message);
                Application.Exit();
            }


        }

        /// <summary>
        /// 确认文件是否被占用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = false;

            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower().Equals("cspclient"))
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;//true表示正在使用,false没有使用
        }
    }
}

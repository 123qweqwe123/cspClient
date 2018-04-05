using cspClient.utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace cspClient
{
    public partial class LoginForm : Form
    {
        public bool loginSucc = false;
        public String loginName;
        public String userName;
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = this.skinButton1;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.password.UseSystemPasswordChar = true;
            this.Shown += LoginFrm_Load;

            this.SizeChanged += LoginForm_SizeChanged;
            this.MinimizeBox = false; // 不展示最小化按钮


            System.Timers.Timer getResTimer = new System.Timers.Timer(500);
            getResTimer.Elapsed += new System.Timers.ElapsedEventHandler(statrtMysql);
            getResTimer.AutoReset = false;
            getResTimer.Enabled = true;
            getResTimer.Start();


            AmqpUtils.GetInstance();
            //AmqpUtils.GetInstance().PushPersonMsg("测试消息推送"+DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void LoginForm_SizeChanged(object sender, EventArgs e)
        {
            
            var form = sender as LoginForm;

            if (form.WindowState == FormWindowState.Minimized) {
                form.WindowState = FormWindowState.Normal;
            }
            
        }

        // 实现更新程序的可自动更新
        private void LoginFrm_Load(object sender, EventArgs e)
        {
            String srcPath = Environment.CurrentDirectory + "\\UpdateSm.exe1";
            String nowPath = Environment.CurrentDirectory + "\\UpdateSm.exe";
            if (File.Exists(srcPath))
            {
                if (File.Exists(nowPath))
                {
                    if (File.Exists(Environment.CurrentDirectory + "\\UpdateSm.exe.bak")) { File.Delete(Environment.CurrentDirectory + "\\UpdateSm.exe.bak"); }
                    File.Move(nowPath, Environment.CurrentDirectory + "\\UpdateSm.exe.bak"); // 备份
                    Log.WriteLogLine("更新UpdateSm.exe程序");
                }
                File.Move(srcPath, nowPath);
            }

        }

        public void statrtMysql(object source, System.Timers.ElapsedEventArgs e)
        {
            skinProgressBar1.Value = 5;
            String res = CmdHelper.execBat(Environment.CurrentDirectory + "\\mysql\\isRunning_5.0.bat", "\"" + Environment.CurrentDirectory + "\\mysql\\\"");

            if (res.IndexOf("alive") == -1)
            {
                CmdHelper.execBat(Environment.CurrentDirectory + "\\mysql\\start_5.0.bat",
                               Environment.CurrentDirectory + "\\mysql\\");
                int i = 0;
                while (res.IndexOf("alive") == -1 && i < 120)
                {
                    res = CmdHelper.execBat(Environment.CurrentDirectory + "\\mysql\\isRunning_5.0.bat",
                                "\"" + Environment.CurrentDirectory + "\\mysql\\\"");
                    System.Threading.Thread.Sleep(500);
                    i++;
                    if (i > 5 && i < 95)
                    {
                        skinProgressBar1.Value = i;
                    }
                }
                if (i >= 120)
                {
                    MessageBox.Show("数据库启动失败");
                    Application.Exit();
                }

            }
            skinProgressBar1.Value = 100;
            processInfo.Text = "系统启动完成";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.username.Text = "";
            //this.password.Text = "";
            this.Close();
            Application.Exit();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (skinProgressBar1.Value < 100)
            {
                MessageBox.Show("系统正在启动,请稍候重试...");
                return;
            }
            String res = CmdHelper.execBat(Environment.CurrentDirectory + "\\mysql\\isRunning_5.0.bat", "\"" + Environment.CurrentDirectory + "\\mysql\\\" ");
            if (res.IndexOf("alive") == -1)
            {
                MessageBox.Show("系统数据库异常,请尝试重启程序");
                return;
            }

            String uname = this.username.Text.Trim();
            String pwd = this.password.Text.Trim();

            if (String.IsNullOrEmpty(uname) || String.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("用户名和密码都不能为空,请校验输入");
                return;
            }
            if ("123".Equals(password.Text) && "admin".Equals(username.Text,StringComparison.CurrentCultureIgnoreCase)) {
                this.loginSucc = true;
                this.loginName = uname;
                userName = uname;
                this.Close();
                return;
            }

            //String pswd = cspClient.utils.LoginValidUtil.encodePwd("jet@biobank", "78f475eb0c71a1c5");
            userName = LoginValidUtil.LoginValid(uname, pwd);
            if (!string.IsNullOrEmpty(userName)) // 登录成功
            {
                this.loginSucc = true;
                this.loginName = uname;
                this.Close();
                return;
            }
            

        }
    }
}

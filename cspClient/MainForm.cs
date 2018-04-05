using cspClient.utils;
using SM.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TxzProcess;

namespace cspClient
{
    public partial class MainForm : Form
    {

        public String username;
        public String curConfNo;
        public String loginName;

        public System.Timers.Timer timer_update; // 版本更新定时器

        public MainForm(String p_loginName,String p_userName)
        {
            InitializeComponent();
            loginName = p_loginName;
            username = p_userName;

            ToolStrip_changeConf.Click += ToolStrip_changeConf_Click;
            menubtn_queryInfo.Click += btn_queryData_Click;

            try {
                toolStrip1.Visible = false;
                if (username.Length > 4)
                {
                    userMenuStrips.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    userMenuStrips.Text = username;
                }
                else {
                    userMenuStrips.Text = username;
                }
                
                this.MainPanel.Controls.Clear();
                Index idx = new Index(this);
                idx.TopLevel = false;
                idx.Dock = DockStyle.Fill;
                idx.FormBorderStyle = FormBorderStyle.None;
                idx.Show();
                this.MainPanel.Controls.Add(idx);

                timer_update = new System.Timers.Timer(60 * 1000);
                timer_update.Elapsed += new System.Timers.ElapsedEventHandler(checkUpdate); //  版本检查定时器
                timer_update.AutoReset = true;
                timer_update.Enabled = true;
                timer_update.Start();
            }
            catch (Exception e) {
                Log.WriteLogLine("页面初始化异常:"+e);
            }
        }

        private void ToolStrip_changeConf_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = false;
            this.MainPanel.Controls.Clear();
            Index idx = new Index(this);
            idx.TopLevel = false;
            idx.Dock = DockStyle.Fill;
            idx.FormBorderStyle = FormBorderStyle.None;
            idx.Show();
            this.MainPanel.Controls.Add(idx);
        }

        private void MainButton_Click(object sender, System.EventArgs e)
        {
            foreach (Control ctl in MainPanel.Controls)
            {
                ctl.Dispose();
            }

            MainPanel.Controls.Clear();

            Form1 f = new Form1();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();
            this.MainPanel.Controls.Add(f);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(mainButton);
            this.MainPanel.Controls.Add(skinLabel1);
            this.MainPanel.Controls.Add(btn_sync);
            this.MainPanel.Controls.Add(skinLabel2);
            this.MainPanel.Controls.Add(btn_queryData);
            this.MainPanel.Controls.Add(skinLabel3);
        }

        private void mainButton_Click_1(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            //Index idx = new Index(this);
            //idx.TopLevel = false;
            //idx.Dock = DockStyle.Fill;
            //idx.FormBorderStyle = FormBorderStyle.None;
            //idx.Show();
            //this.MainPanel.Controls.Add(idx);
            var register = new Register(this);
            register.TopLevel = false;
            register.FormBorderStyle = FormBorderStyle.None;
            register.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(register);
            register.Show();
        }

        //private void toolStripSync_Click(object sender, EventArgs e)
        //{
        //    this.MainPanel.Controls.Clear();
        //    DataQuery idx = new DataQuery();
        //    idx.TopLevel = false;
        //    idx.Dock = DockStyle.Fill;
        //    idx.FormBorderStyle = FormBorderStyle.None;
        //    idx.Show();
        //    this.MainPanel.Controls.Add(idx);
        //}

        private void toolStripSM_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            var register = new Register(this);
            register.TopLevel = false;
            register.FormBorderStyle = FormBorderStyle.None;
            register.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(register);
            register.Show();
        }

        private void loginout_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();

            this.Hide();
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            if (lf.loginSucc)
            {
                this.username = lf.userName;
                this.loginName = lf.loginName;
                userMenuStrips.Text = lf.userName;
                toolStrip1.Visible = false;
                userMenuStrips.Text = username;
                this.MainPanel.Controls.Clear();
                Index idx = new Index(this);
                idx.TopLevel = false;
                idx.Dock = DockStyle.Fill;
                idx.FormBorderStyle = FormBorderStyle.None;
                idx.Show();
                this.MainPanel.Controls.Add(idx);

                this.Show();
            }
            else
            {
                // 关闭登录窗口则直接退出程序
                Application.Exit();
            }
        }

        private void btn_queryData_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            DataQuery idx = new DataQuery(this);
            idx.TopLevel = false;
            idx.Dock = DockStyle.Fill;
            idx.FormBorderStyle = FormBorderStyle.None;
            idx.Show();
            this.MainPanel.Controls.Add(idx);
        }

        // 数据同步
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.MessageInfo = "正在导出压缩上传本地数据...";
            process.BackgroundWork = this.DoWork;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
            MessageBox.Show("数据上传处理完成！");
        }

        void DoWork() {
            try {

                UploadDataHelper.UploadMainPersonData(curConfNo);

                UploadDataHelper.UploadData( curConfNo );
                
            }
            catch ( Exception e ) {
                Log.WriteLogLine("数据上传失败:"+e.Message);
            }
            
        }

        void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                //MessageBox.Show("数据上传完成！");
            }
            else
            {
                Log.WriteLogLine("进度条出现异常:" + e.BackGroundException.Message);
            }
        }

        /**
         * 版本自更新时执行更新sql
         * */
        public void ExecuteSql()
        {
            Log.WriteLogLine("自动执行更新sql");

            String filePath = Environment.CurrentDirectory + "\\updateSql\\needRun\\";
            if (!Directory.Exists(filePath))
            {
                return;

            }
            else
            {

                String[] files = Directory.GetFiles(filePath);
                if (files.Length == 0)
                {
                    Log.WriteLogLine("更新sql目录为空,暂无执行sql");
                    return;
                }

                String hasRun = Environment.CurrentDirectory + "\\updateSql\\hasRun\\";
                String runError = Environment.CurrentDirectory + "\\updateSql\\runError\\";
                if (!Directory.Exists(hasRun))
                {
                    Directory.CreateDirectory(hasRun);
                }

                if (!Directory.Exists(runError))
                {
                    Directory.CreateDirectory(runError);
                }
                foreach (String file in files)
                {
                    try
                    {
                        String sql = File.ReadAllText(file);
                        String[] sqls = sql.Split(';');
                        List<String> list = new List<String>();
                        list.AddRange(sqls);
                        DBsqliteHelper.ExecuteNonQueryTrans(list);
                        if (File.Exists(hasRun + Path.GetFileName(file))) { File.Delete(hasRun + Path.GetFileName(file)); }
                        File.Move(file, hasRun + Path.GetFileName(file));
                    }
                    catch (Exception e)
                    {
                        Log.WriteLogLine("执行sql出现异常:" + e.Message);
                        if (File.Exists(runError + Path.GetFileName(file))) { File.Delete(runError + Path.GetFileName(file)); }
                        File.Move(file, runError + Path.GetFileName(file));
                        continue;
                    }

                }

            }
        }



        public void updateVersion(String lastVersion)
        {
            //updateVersionBtn.Visible = false; // 隐藏手动更新按钮
            Log.WriteLogLine("更新版本");

            Invoke(new ChangeButtonVisble(changeBtnVisible), new Object[] { false });
            String currentVer = XmlConfigHelper.GetValueByKey("CurVersion").Trim(); //  获取当前版本号   
            try
            {
                String downLoadUrl = XmlConfigHelper.GetValueByKey("Address") +
                                     XmlConfigHelper.GetValueByKey("GetVersionZip");

                //String machineNo = XmlConfigHelper.GetValueByKey("MachineUUID");
                //if (String.IsNullOrEmpty(machineNo)) { machineNo = Utils.getMachineUUID(); } // 获取客户端机器码
                string machineNo = Utils.getMachineUUID();
                //downLoadUrl += "?machineNo=" + machineNo;
                String fileName = lastVersion + "_" + Environment.TickCount + ".zip";
                String filePath = Environment.CurrentDirectory + "\\updateTemp\\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                // 
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo[] fileArr = di.GetFiles();
                foreach (FileInfo f in fileArr)
                {
                    if (f.Name.ToString().StartsWith(lastVersion + "_"))
                    {
                        f.Delete();
                    }
                }

                // 下载时才能取到Md5 所以这里只能删掉已下载的同版本补丁包 重新下载后校验
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                filePath += fileName;

                //Dictionary<String, String> respHeads = WebDownLoadUtils.HttpDownFileAndHdeads(downLoadUrl, "", filePath);

                WebDownLoadUtils.HttpGetDownLoad(downLoadUrl,"",filePath);

                //String serverMd5 = respHeads["md5"];
                //Log.WriteLogLine("下载升级包的版本号为:" + respHeads["version"] + "，获取到服务端版本号：" + lastVersion + ",当前自身版本号:" + currentVer);

                String fileMd5 = ZipHelper.GetMD5HashFromFile(filePath); //  获取下载文件的MD5值用以比对匹配
                //if (String.IsNullOrEmpty(serverMd5) || !fileMd5.Equals(serverMd5))
                //{//获取服务端fileMd5
                //    Log.WriteLogLine("文件MD5值校验不通过");
                //    File.Delete(filePath);
                //    Log.WriteLogLine("文件MD5值校验不通过,删除已下载文件");
                //    return;
                //}
                

                if (MessageBox.Show("发现新版本,是否开始更新。确定将关闭客户端,请先确认当前操作已经保存。", "",
                    MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    this.Close();
                    //Thread.Sleep(900);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    //p.StartInfo.FileName = "D:\\visualstudioWorkspaces\\SM\\UpdateSm\\bin\\Debug\\UpdateSm.exe";//debug
                    p.StartInfo.FileName = Environment.CurrentDirectory + "\\AutoUpdate.exe";//release
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.Arguments = "\"" + filePath + "\" " + fileMd5 + " " + lastVersion;//参数以空格分隔，如果某个参数为空，可以传入""
                    p.Start();
                    Application.Exit(); // 客户端程序退出
                }
                else
                {
                    //Invoke(new ChangeButtonVisble(changeBtnVisible), new Object[] { true });
                }
            }
            catch (Exception exp)
            {
                Log.WriteLogLine("更新版本异常:" + exp.ToString());
                return;
            }
        }

        /// <summary>
        /// 比较版本号
        /// </summary>
        /// <param name="cur">当前版本号</param>
        /// <param name="lastVer">服务端获取到的最新版本号</param>
        /// <returns></returns>
        public bool hasNewVersion(String cur, String lastVer)
        {
            if (lastVer.CompareTo(cur) > 0)
            {
                return true;
            }
            return false;
        }

        private void updateVersionBtn_Click(object sender, EventArgs e)
        {
            updateVersion(lastVersion);
            lastVersion = null; //更新完毕后置空版本信息 防止重复执行
        }

        public String lastVersion;
        public void checkUpdate(object source, System.Timers.ElapsedEventArgs e)
        {
            String currentVer = XmlConfigHelper.GetValueByKey("CurVersion").Trim(); //  获取当前版本号  
            if (String.IsNullOrEmpty(currentVer)) { currentVer = "0"; }
            lastVersion = null;
            try
            {
                //lastVersion = WebUploadUtils.PostForm(XmlConfigHelper.GetValueByKey("Address") +
                //XmlConfigHelper.GetValueByKey("GetNewVersion")+ "?dictType=98&projectId=009", null); //获取Master机最新版本号     

                lastVersion  = WebDownLoadUtils.HttpGetUploadPath(XmlConfigHelper.GetValueByKey("Address") +
                XmlConfigHelper.GetValueByKey("GetNewVersion") + "?dictType=98&projectId=009");

                Log.WriteLogLine("打印获取到的服务器版本：" + lastVersion);
                lastVersion = lastVersion.Trim();
                if (String.IsNullOrEmpty(lastVersion))
                {
                    Log.WriteLogLine("更新版本定时器 获取版本信息失败");
                    return;
                }
            }
            catch (Exception exp)
            {
                Log.WriteLogLine("获取版本号异常,更新程序返回继续下一次" + exp.Message);
                return;
            }
            if (!String.IsNullOrEmpty(lastVersion) && hasNewVersion(currentVer, lastVersion))
            {

                Invoke(new ChangeButtonVisble(changeBtnVisible), new Object[] { true });

                //updateVersionBtn.Visible = true; // 手动更新可操作
                // 屏蔽检查的弹窗  改为只出现按钮让用户来操作
                //updateVersion(lastVersion);
            }
            else
            {
                //updateVersionBtn.  = false; // 隐藏手动更新按钮
                Invoke(new ChangeButtonVisble(changeBtnVisible), new Object[] { false });
            }
        }
        public delegate void ChangeButtonVisble(bool b);
        public void changeBtnVisible(bool b)
        {
            updateVersionBtn.Visible = b;
        }
    }
}

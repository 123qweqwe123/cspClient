using cspClient.projCtl;
using cspClient.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TxzProcess;

namespace cspClient
{
    public partial class Index : Form
    {
        public MainForm mainForm;
        public Index(MainForm p_mainForm)
        {
            mainForm = p_mainForm;
            InitializeComponent();

            this.skinDataGridView1.AllowUserToAddRows = false;
            this.skinDataGridView1.AllowUserToDeleteRows = false;
            this.skinDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skinDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            //List<SqlHelper.ConfList> list = initConfList();
            //InitDataGridView(list);

            this.Shown += Index_Shown;
        }

        private void Index_Shown(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.MessageInfo = "正在读取会议列表...";
            process.BackgroundWork = getServerConfInfo;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
            List<SqlHelper.ConfList> list = initConfList();
            skinDataGridView1.DataSource = null;
            //for ( int i = 0; i< skinDataGridView1.Columns.Count; i++ ) {
            if (skinDataGridView1.Columns.Count > 0) { skinDataGridView1.Columns.RemoveAt(0); }
            
            //}

            InitDataGridView(list);
        }
        void getServerConfInfo() {
            
            AnalyticJson.getConfDatafList(mainForm.loginName);
        }

        public void InitDataGridView(List<SqlHelper.ConfList> list) {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("会议编号", typeof(String)));
            dt.Columns.Add(new DataColumn("会议名称", typeof(String)));
            dt.Columns.Add(new DataColumn("创建时间", typeof(String)));
            dt.Columns.Add(new DataColumn("签到开始时间", typeof(String)));
            DataColumn dc = new DataColumn("本机数据包", typeof(String));//(基本信息/会议信息)
            dt.Columns.Add(dc);
            dt.Columns.Add(new DataColumn("服务器数据版本", typeof(String)));
            dt.Columns.Add(new DataColumn("本机数据版本", typeof(String)));
            dt.Columns.Add(new DataColumn("confList", typeof(String)));
            skinDataGridView1.DataSource = dt;

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "更新数据";
            btnColumn.FillWeight = 40;
            btnColumn.Text = "进入";
            btnColumn.UseColumnTextForButtonValue = false;
            skinDataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            skinDataGridView1.CellContentClick += DataGridView1_CellContentClick;
            btnColumn.FlatStyle = FlatStyle.Flat;
            btnColumn.UseColumnTextForButtonValue = true; // 显示按钮上的文字

            //List<SqlHelper.ConfList> list = AnalyticJson.getConfDatafList();

            //List<SqlHelper.ConfList> list = initConfList();

            List<int> dataDownList = new List<int>();
            List<int> dataUpdateList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                SqlHelper.ConfList po = list[i];
                DataRow dr = dt.NewRow();
                dr["会议编号"] = po.confNo;
                dr["会议名称"] = po.confName;
                try
                {
                    DateTime dtCreateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))
                                       .Add(new TimeSpan(long.Parse(po.createTime + "0000")));
                    dr["创建时间"] = dtCreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception e)
                {
                    dr["创建时间"] = po.createTime;
                }

                try {
                    DateTime dtCreateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))
                                       .Add(new TimeSpan(long.Parse(po.startTime + "0000")));
                    dr["签到开始时间"] = dtCreateTime.ToString("yyyy-MM-dd");
                } catch (Exception e) {
                    dr["签到开始时间"] = po.startTime;
                }

                // (基本信息/会议信息)
                dr["本机数据包"] = "无需更新";
                if ("2".Equals(po.dataState)) {
                    dataDownList.Add(i);
                    dr["本机数据包"] = "未下载";
                }
                if ("1".Equals(po.dataState)) {
                    dataUpdateList.Add(i);
                    dr["本机数据包"] = "需要更新";
                }

                dr["服务器数据版本"] = (string.IsNullOrEmpty(po.serverType2Version) ? "" : po.serverType2Version) + "/" +
                                        (string.IsNullOrEmpty(po.serverType3Version) ? "" : po.serverType3Version);
                dr["本机数据版本"] = (string.IsNullOrEmpty(po.localType2Version) ? "" : po.localType2Version) + "/" +
                                        (string.IsNullOrEmpty(po.localType3Version) ? "" : po.localType3Version);
                dr["confList"] = Newtonsoft.Json.JsonConvert.SerializeObject(po); ;// string.Join(",", po.confList.ToArray());
                dt.Rows.Add(dr);
            }

            DataGridViewButtonColumn c = new DataGridViewButtonColumn();
            var mybuttonCell = new MyButtonCell();
            MyButtonCell.downList = dataDownList;
            MyButtonCell.updateList = dataUpdateList;
            c.CellTemplate = mybuttonCell;
            //c.UseColumnTextForButtonValue = true;
            c.HeaderText = "更新数据";
            c.Text = "进入";

            this.skinDataGridView1.Columns.Add(c);
            skinDataGridView1.DataSource = dt;
            skinDataGridView1.Columns["confLIst"].Visible = false;

        }

        SqlHelper.ConfList p_ConfList;

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (!"更新数据".Equals(skinDataGridView1.Columns[e.ColumnIndex].HeaderText)) return;

            String confList = skinDataGridView1.Rows[e.RowIndex].Cells["confLIst"].Value.ToString();
            String confNo = skinDataGridView1.Rows[e.RowIndex].Cells["会议编号"].Value.ToString();
            SqlHelper.ConfList obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SqlHelper.ConfList>(confList);

            if ((obj.confList == null || obj.confList.Count == 0) || (!obj.confList.Contains("2") && !obj.confList.Contains("3")))
            { // 【进入】逻辑
                if (mainForm.username == "admin")
                {
                    MessageBox.Show("管理员账号无法进入会议页面请使用会务人员账号登录");
                    return;
                }
                mainForm.toolStrip1.Visible = true;
                mainForm.curConfNo = obj.confNo;
                mainForm.MainPanel.Controls.Clear();
                mainForm.toolStripButton1.PerformClick();
            }
            else { //【下载、更新】逻辑
                p_ConfList = obj;
                ProcessOperator process = new ProcessOperator();
                process.MessageInfo = "正在下载更新会议列表...";
                process.BackgroundWork = updateWork;
                process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
                process.Start();
                //AnalyticJson.downLoadData(obj);
                //AnalyticJson.downLoadDicData();
                mainForm.MainPanel.Controls.Clear();
                Index idx = new Index(mainForm);
                idx.TopLevel = false;
                idx.Dock = DockStyle.Fill;
                idx.FormBorderStyle = FormBorderStyle.None;
                idx.Show();
                mainForm.MainPanel.Controls.Add(idx);

            }
        }
        void updateWork()
        {
            AnalyticJson.downLoadData(p_ConfList);
            AnalyticJson.downLoadDicData();
        }






        // 更新会议列表
        private void skinButton1_Click(object sender, EventArgs e)
        {
            //AnalyticJson.downLoadConfListData(); // 更新会议列表

            ProcessOperator process = new ProcessOperator();
            process.MessageInfo = "正在下载更新会议列表...";
            process.BackgroundWork = this.DoWrok;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();

            mainForm.MainPanel.Controls.Clear();
            Index idx = new Index(mainForm);
            idx.TopLevel = false;
            idx.Dock = DockStyle.Fill;
            idx.FormBorderStyle = FormBorderStyle.None;
            idx.Show();
            mainForm.MainPanel.Controls.Add(idx);

        }

        void DoWrok() {
            AnalyticJson.downLoadConfListData();
        }
        void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                //MessageBox.Show("执行完毕");
            }
            else
            {
                Log.WriteLogLine("进度条出现异常:" + e.BackGroundException.Message);
            }
        }

        List<SqlHelper.ConfList> initConfList(){
            List<SqlHelper.ConfList> confList = new List<SqlHelper.ConfList>();
            String initSql = "";
            if ("admin".Equals(mainForm.loginName, StringComparison.CurrentCultureIgnoreCase))
            {
                initSql = "select * from CSP_CONFERENCELIST ";
            }
            else {
                initSql = "select * from CSP_CONFERENCELIST l where " +
                "exists( select 1 from csp_conf_worker w ,sys_account a where a.user_id= w.worker_id and a.login_name='" + mainForm.loginName + "' and l.id=w.conf_id)";
            }
            
            DataTable datatable = DBsqliteHelper.getDataTable(initSql);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                SqlHelper.ConfList confListdata = new SqlHelper.ConfList();
                datatable.Rows[i]["id"].ToString();
                confListdata.id = datatable.Rows[i]["id"].ToString();
                confListdata.confNo = datatable.Rows[i]["conf_No"].ToString();
                confListdata.confType = datatable.Rows[i]["conf_Type"].ToString();
                confListdata.confForm = datatable.Rows[i]["conf_Form"].ToString();
                confListdata.confHost = datatable.Rows[i]["conf_Host"].ToString();
                confListdata.confName = datatable.Rows[i]["conf_Name"].ToString();
                confListdata.confOrganiser = datatable.Rows[i]["conf_Organiser"].ToString();
                confListdata.confCoOrganiser = datatable.Rows[i]["conf_Co_Organiser"].ToString();
                confListdata.confPic = datatable.Rows[i]["conf_Pic"].ToString();
                confListdata.confTopic = datatable.Rows[i]["conf_Topic"].ToString();
                confListdata.confDescription = datatable.Rows[i]["conf_Description"].ToString();
                confListdata.startTime = datatable.Rows[i]["start_Time"].ToString();
                confListdata.endTime = datatable.Rows[i]["end_Time"].ToString();
                confListdata.confPlace = datatable.Rows[i]["conf_Place"].ToString();
                confListdata.remark = datatable.Rows[i]["remark"].ToString();
                confListdata.dataVersion = datatable.Rows[i]["data_Version"].ToString();
                confListdata.createBy = datatable.Rows[i]["create_By"].ToString();
                confListdata.createTime = datatable.Rows[i]["create_Time"].ToString();
                confListdata.updateBy = datatable.Rows[i]["update_By"].ToString();
                confListdata.updateTime = datatable.Rows[i]["update_Time"].ToString();

                String sql = "Select * from csp_conference where id= '" + confListdata.id + "'";
                DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    confListdata.localType1Version = dataTable.Rows[0]["data_version"].ToString();
                }
                confListdata.serverType1Version = datatable.Rows[i]["serverType1Version"].ToString();
                confListdata.serverType2Version = datatable.Rows[i]["serverType2Version"].ToString();
                confListdata.localType2Version = datatable.Rows[i]["localType2Version"].ToString();
                confListdata.serverType3Version = datatable.Rows[i]["serverType3Version"].ToString();
                confListdata.localType3Version = datatable.Rows[i]["localType3Version"].ToString();
                confListdata.serverType4Version = datatable.Rows[i]["serverType4Version"].ToString();
                confListdata.localType4Version = datatable.Rows[i]["localType4Version"].ToString();

                List<String> confLists = new List<String>();
                //本地没有版本号
                if (String.IsNullOrEmpty(confListdata.localType2Version) || String.IsNullOrEmpty(confListdata.localType3Version)
                    )
                {
                    confListdata.dataState = "2";
                }
                //版本号相同
                else if (
                    confListdata.serverType2Version.Equals(confListdata.localType2Version)
                    && confListdata.serverType3Version.Equals(confListdata.localType3Version)
                    )
                {
                    confListdata.dataState = "0";
                }
                else //需要更新
                {
                    confListdata.dataState = "1";
                }

                if (!confListdata.serverType2Version.Equals(confListdata.localType2Version))
                {
                    confLists.Add("2");
                }
                if (!confListdata.serverType3Version.Equals(confListdata.localType3Version))
                {
                    confLists.Add("3");
                }
                if (!confListdata.serverType4Version.Equals(confListdata.localType4Version))
                {
                    confLists.Add("4");
                }
                confListdata.confList = confLists;
                confList.Add(confListdata);
            }
            return confList;
        }


    }

  
}


public class MyButtonCell : DataGridViewButtonCell
{
    public static List<int> downList { get; set; }
    public static List<int> updateList { get; set; }
    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        if (downList.Contains(rowIndex)) { value = "下载"; }
        if (updateList.Contains(rowIndex)) { value = "更新"; }
        if (value == null || String.IsNullOrEmpty(value.ToString())){ value = "进入"; }
        //ButtonRenderer.DrawButton(graphics, cellBounds, formattedValue.ToString(), new Font("Comic Sans MS", 9.0f, FontStyle.Bold), true, System.Windows.Forms.VisualStyles.PushButtonState.Default);
        ButtonRenderer.DrawButton(graphics, cellBounds, value.ToString(),null, true, System.Windows.Forms.VisualStyles.PushButtonState.Default);

    }
}

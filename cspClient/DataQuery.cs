using cspClient.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cspClient
{
    public partial class DataQuery : Form
    {
        public String curConfNo;
        public DataQuery(MainForm p_mainForm)
        {
            curConfNo = p_mainForm.curConfNo;
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("会议名称"));
            dt.Columns.Add(new DataColumn("省市"));
            dt.Columns.Add(new DataColumn("单位名称"));
            dt.Columns.Add(new DataColumn("姓名"));
            dt.Columns.Add(new DataColumn("性别"));
            dt.Columns.Add(new DataColumn("手机号"));
            dt.Columns.Add(new DataColumn("科室"));
            dt.Columns.Add(new DataColumn("职称"));
            dt.Columns.Add(new DataColumn("职务"));
            dt.Columns.Add(new DataColumn("民族"));
            dt.Columns.Add(new DataColumn("注册状态"));
            dt.Columns.Add(new DataColumn("注册时间"));
            dt.Columns.Add(new DataColumn("数据状态"));

            skinDataGridView1.DataSource = dt;
            this.skinDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //内容居中
            skinDataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pageControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
            LoadData();
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData() {
            String srcSql = GetQuerySql();

            String countSql = " select count(1) from ( " + srcSql + " ) as total";
            pageControl1.RecordCount = int.Parse(DBsqliteHelper.getSqlResult(countSql));
            String sql = srcSql +
            " limit " + pageControl1.PageSize + " offset " + ((pageControl1.PageIndex - 1) * pageControl1.PageSize);
            DataTable dt = DBsqliteHelper.getDataTable(sql);

            skinDataGridView1.DataSource = dt;
            pageControl1.DrawControl(pageControl1.RecordCount);
        }
        public String GetQuerySql() {
            return "select  ( select CONF_NAME from csp_conference  where conf_no='"+ curConfNo + "' ) as 会议名称 ,concat(p.province,  p.city) as '省市' , p.DEPARTMENT as '单位名称', " +
                            " p.name as '姓名' , if(p.gender=1,'男','女') as '性别', p.tel as '手机号码',p.major as'科室'," +
                            " p.degree as '职称', p.duty as  '职务'," +
                            //" if(r.register_time , if( r.is_scancard = '1' , '已注册','已注册(增)' )   ,'未注册') as '注册状态' , " +//'' as '民族',
                            " if(r.register_time , if ((select count(1) from csp_conf_visitor v, csp_conf_lecturer l,csp_conf_worker w" +
                            "    where l.CONF_LECTURER_ID = p.id or p.id = v.VISITOR_ID or w.worker_id = p.id ) > 0 ,'已注册','已注册(增)' )   ,'未注册') as '注册状态' , "+
                            " r.register_time as '注册时间', if(r.isupload='1','已同步','未同步') as '数据状态'" +
                            " from csp_main_person p   " +
                            " left join csp_conf_register r  on r.person_id = p.id  " +
                            " left join ( select * from csp_conference  where conf_no = '"+curConfNo+"' ) csp  on r.conf_id = csp.id" +
                            " where 1=1 " +
                            " and( " +
                            " exists(select 1 from csp_conf_visitor v where p.id=v.VISITOR_ID and v.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) " +
                            " or exists( select 1 from csp_conf_lecturer l where l.CONF_LECTURER_ID = p.id and l.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) " +
                            " or exists(select 1 from csp_conf_worker w where w.worker_id = p.id and conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' ))" +
                            " or exists(select 1 from csp_conf_register rs where rs.person_id = p.id and conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) " +
                            ") " +
                            getQueryStr();
        }

        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            // 执行数据重新加载
            LoadData();
        }

        public String getQueryStr() {
            String res = " "; // 默认固定条件
            // 拼装查询条件
            String nameStr = name.Text;
            String phoneNum = phoneNumber.Text;
            if ( (!string.IsNullOrEmpty(nameStr)) && nameStr.Length > 0)
            {
                res += " AND p.name like '%" + nameStr + "%' ";
            }
            if ((!string.IsNullOrEmpty(phoneNum)) && phoneNum.Length > 0)
            {
                res += " AND p.tel like '%" + phoneNum + "%' ";
            }

            return res;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            name.Text = "";
            phoneNumber.Text = "";
        }

        private void btn_exportCSV_Click(object sender, EventArgs e)
        {
            //if (skinDataGridView1.Rows.Count == 0)
            //{
            //    MessageBox.Show("暂无数据可导出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            DataQueryExport dqe = new DataQueryExport();
            dqe.ShowDialog();
            if (!dqe.retVal) {
                return;
            }
            bool bool_v = dqe.cb_v.Checked;
            bool bool_l = dqe.cb_l.Checked;
            bool bool_w = dqe.cb_w.Checked;
            dqe.Close();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "保存";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = saveFileDialog.OpenFile();
                    StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.GetEncoding(-0));
                    string strLine = "";
                    try
                    {
                        String sql = "select ( select CONF_NAME from csp_conference  where conf_no='" + curConfNo + "' ) as 会议名称 ,concat(p.province,  p.city) as '省市' , p.DEPARTMENT as '单位名称', " +
                            " p.name as '姓名' , if(p.gender=1,'男','女') as '性别', p.tel as '手机号码',p.major as'科室'," +
                            " p.degree as '职称', p.duty as  '职务'," +
                            //" if(r.register_time , if( r.is_scancard = '1' , '已注册','已注册(增)' )   ,'未注册') as '注册状态' , " +//'' as '民族',
                            " if(r.register_time , if ((select count(1) from csp_conf_visitor v, csp_conf_lecturer l where l.CONF_LECTURER_ID = p.id or p.id = v.VISITOR_ID) > 0 ,'已注册','已注册(增)' )   ,'未注册') as '注册状态' , " +
                            " r.register_time as '注册时间', if(r.isupload='1','已同步','未同步') as '数据状态'" +
                           " from csp_main_person p   " +
                            " left join csp_conf_register r  on r.person_id = p.id  " +
                            " left join ( select * from csp_conference  where conf_no = '" + curConfNo + "' ) csp  on r.conf_id = csp.id" +
                            " where 1=1 " +
                            " and( " +
                            " exists(select 1 from csp_conf_register rs where rs.person_id = p.id and conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) "
                        + " or exists( select 1 from csp_conf_lecturer l where l.CONF_LECTURER_ID = p.id and l.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) "
                        + " or exists( select 1 from csp_conf_worker w where w.worker_id = p.id and w.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' ))  "
                        + " or exists(select 1 from csp_conf_visitor v where p.id=v.VISITOR_ID and v.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) "
                        + ") ";


                        if (!bool_l)
                        {
                            sql += " and not exists( select 1 from csp_conf_lecturer l where l.CONF_LECTURER_ID = p.id and l.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) ";
                        }
                        if (!bool_w)
                        {
                            sql += " and not exists( select 1 from csp_conf_worker w where w.worker_id = p.id and w.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' ))  ";
                        }
                        if (!bool_v)
                        {
                            sql += " and not exists(select 1 from csp_conf_visitor v where p.id=v.VISITOR_ID and v.conf_id in( select id from csp_conference where conf_no='" + curConfNo + "' )) ";
                        }
                        DataTable dt = DBsqliteHelper.getDataTable(sql);
                        //表头
                        for (int i = 0; i < skinDataGridView1.ColumnCount; i++)
                        {
                            if (i > 0)
                                strLine += ",";
                            strLine += skinDataGridView1.Columns[i].HeaderText;
                        }
                        strLine.Remove(strLine.Length - 1);
                        sw.WriteLine(strLine);
                        strLine = "";
                        //表的内容
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            strLine = "";
                            int colCount = skinDataGridView1.Columns.Count;
                            for (int k = 0; k < colCount; k++)
                            {
                                if (k > 0 && k < colCount)
                                    strLine += ",";
                                if (dt.Rows[j].ItemArray[k] == null)
                                    strLine += "";
                                else
                                {
                                    string cell = dt.Rows[j].ItemArray[k].ToString().Trim();
                                    //防止里面含有特殊符号
                                    cell = cell.Replace("\"", "\"\"");
                                    cell = "\"" + cell + "\"";
                                    strLine += cell;
                                }
                            }
                            sw.WriteLine(strLine);
                        }

                        sw.Close();
                        stream.Close();
                        MessageBox.Show("数据被导出到：" + saveFileDialog.FileName.ToString(), "导出完毕", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLogLine(ex.Message);
                        MessageBox.Show("导出失败,联系管理员或稍后重试", "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteLogLine(exp.Message);
                    MessageBox.Show("文件被另一程序占用导出失败", "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}

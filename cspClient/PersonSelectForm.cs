using cspClient.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cspClient
{
    public partial class PersonSelectForm : Form
    {
        public String uname = "";
        public String telNum = "";
        public String curConfNo = "";

        public String returnType = "";
        public String returnNum = "";

        public PersonSelectForm(String p_name , String p_tel  ,String p_confNo)
        {
            uname = p_name;
            telNum = p_tel;
            curConfNo = p_confNo;

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            skinDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //内容居中
            skinDataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            skinDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            initDataGrid();
        }

        public void initDataGrid() {

            String sql = " select WORKPLACE as '单位',NAME as '姓名',TEL as '手机号',isUpload,src_id,ID,ID_NUMBER,ID_TYPE,TYPE,GENDER,BIRTHDAY,EMAIL,DEPARTMENT,MAJOR,DEGREE,DUTY,PROVINCE,CITY,COUNTY,ADDRESS,DATA_VERSION,CREATE_BY,CREATE_TIME " +
                " from csp_main_person p where 1=1 " +
                " and ( " +
                " exists(select 1 from csp_conf_visitor v where p.id = v.visitor_id  and exists(select 1 from csp_conference f where f.conf_no = '"+curConfNo+"' and f.id = v.conf_id  )) " +
                " or exists(select 1 from csp_conf_lecturer l where p.id = l.conf_lecturer_id  and exists(select 1 from csp_conference f where f.conf_no = '"+ curConfNo + "' and f.id = l.conf_id  )) " +
                " or exists(select 1 from csp_conf_worker w where p.id = w.worker_id  and exists(select 1 from csp_conference f where f.conf_no = '" + curConfNo + "' and f.id = w.conf_id  )) " + 
                " )";
            
            if (!String.IsNullOrEmpty(uname)) {
                sql += " and name like '%" + uname + "%' ";
            }
            if (!String.IsNullOrEmpty(telNum)) {
                sql +=" and tel like '%" + telNum + "%' ";
            }
            
            DataTable dt = DBsqliteHelper.getDataTable(sql);
            skinDataGridView1.DataSource = dt;
            
            for ( int i = 0; i < skinDataGridView1.Columns.Count;i++ ) {
                if ( i>2 ) {
                    skinDataGridView1.Columns[i].Visible = false;
                }
            }

            skinDataGridView1.CellClick += SkinDataGridView1_CellClick;
        }

        private void SkinDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (skinDataGridView1.SelectedRows.Count == 0) { return; }
            var obj = sender as DataGridView;
            var col = skinDataGridView1.SelectedRows[0];
            //MessageBox.Show(col.Cells[0].Value.ToString());

            IdNumAddForm idForm = new IdNumAddForm();
            idForm.userName = col.Cells["姓名"].Value.ToString();
            idForm.userTelNum = col.Cells["手机号"].Value.ToString();
            idForm.Owner = this;
            //idForm.FormBorderStyle = FormBorderStyle.None;
            idForm.ShowDialog();

            if ((!string.IsNullOrEmpty(idForm.returnType)) && (!string.IsNullOrEmpty(idForm.returnNum)))
            {
                returnType = idForm.returnType;
                returnNum = idForm.returnNum;
            }
            else {
                returnType = "";
                returnNum = "";
            }
            idForm.Close();

            //col.Cells
            Register reg = (Register)this.Owner;

            reg.setValueByPersonSelect(col);

            this.Close();
        }

        public String getUserId() {
            if (skinDataGridView1.SelectedRows.Count == 0) {
                return "";
            }
            try {
                return skinDataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            }
            catch (Exception e) {
                return "";
            }
        }
    }
}

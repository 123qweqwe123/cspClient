using cspClient.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cspClient
{
    public partial class IdNumAddForm : Form
    {
        public String userName = "";
        public String userTelNum = "";

        public String returnType = "";
        public String returnNum = "";

        public IdNumAddForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            idtype_combox.DisplayMember = "pText";
            idtype_combox.ValueMember = "pValue";
            //idtype_combox.Items.Add();
            List<DropItems> list = getComboxData();
            idtype_combox.DataSource = list;
            idtype_combox.SelectedIndexChanged += Idtype_combox_SelectedIndexChanged;
            idtype_combox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Shown += IdNumAddForm_Shown;
        }

        private void IdNumAddForm_Shown(object sender, EventArgs e)
        {
            uname.Text = userName;
            telNum.Text = userTelNum;
        }

        private void Idtype_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = sender as ComboBox;
            idtype_combox.SelectedValue.ToString();
        }

        public List<DropItems> getComboxData()
        {
            List<DropItems> list = new List<DropItems>();
            String sql = " select code,value from syS_parameter where type_code = 'T003' order by code; ";
            DataTable dt = DBsqliteHelper.getDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropItems(dt.Rows[i]["CODE"].ToString(), dt.Rows[i]["VALUE"].ToString()));
            }
            return list;
        }


        public class DropItems
        {
            public DropItems(String p_value, String p_text)
            {
                Value = p_value;
                Text = p_text;
            }

            public String Value;
            public String Text;
            public String pValue()
            {
                return Value;
            }
            public String pText()
            {
                return Text;
            }
            public override string ToString()
            {
                return this.Text;
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idNum_textBox.Text))
            {

                MessageBox.Show("证件类型、证件号码不能为空");
                return;
            }

            if (("身份证".Equals(idtype_combox.Text)) && (!Regex.IsMatch(idNum_textBox.Text, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
            {
                MessageBox.Show("请输入正确的身份证号码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DropItems dt = (DropItems)idtype_combox.SelectedItem;
            returnType = dt.Value;
            returnNum = idNum_textBox.Text;

            String countsql = " select count(1) from csp_main_person where id_number = '"+ returnNum + "'";
            String res = DBsqliteHelper.getSqlResult(countsql);
            if ( int.Parse(res) > 0 ) {
                MessageBox.Show("证件号重复,请修改后重试!");
                return;
            }


            PersonSelectForm psf = (PersonSelectForm)(this.Owner);
            String uid = psf.getUserId();
            String sql = " update CSP_MAIN_PERSON set PC_idtype = '" + returnType + "',PC_idNumber='" + returnNum + "', isupload='0' where id='" + uid + "' ";
            DBsqliteHelper.ExecuteNonQuery(sql);
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            returnType = "";
            returnNum = "";
            this.Close();
        }

        private void btn_idReader_Click(object sender, EventArgs e)
        {
            int connState = -1;
            connState = CardReader.CVR_InitComm(1001);// 0 dll加载失败 1 正确 2端口打开失败
            if (connState != 1)
            {
                MessageBox.Show("读卡器连接失败");
                return;
            }
            CardReader.CVR_Authenticate();
            short refShort = 256;
            StringBuilder sb = new StringBuilder(255);
            CardReader.CVR_Read_Content(4);
            connState = CardReader.GetPeopleIDCode(sb, ref refShort); // 0错误 1正确
            if (connState == 0)
            {
                MessageBox.Show("身份证号读取有误,请重试");
                return;
            }
            else
            {
                idNum_textBox.Text = sb.ToString();
            }
            CardReader.CVR_CloseComm();
        }
    }
}

using cspClient.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cspClient
{
    public partial class Register : Form
    {

        public String confNo; // 会议编号
        public String personName = "", provStr = "", placeStr = "", qrCodeStr = "", idNumber = "";
        public String printImgPath = "";
        public MainForm mainForm;

        public String isCardScan = "1";// 1读卡器读取  0手动输入
        public Register(MainForm p_mainForm)
        {
            mainForm = p_mainForm;
            confNo = mainForm.curConfNo;
            InitializeComponent();
            gender.Items.AddRange(
                new String[] { "男", "女" }
                );
            ArrayList list = new ArrayList();
            try
            {
                String sql = " select code,value from syS_parameter where type_code = 'T003' order by code; ";
                DataTable dt = DBsqliteHelper.getDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new DictionaryEntry(dt.Rows[i]["CODE"], dt.Rows[i]["VALUE"]));
                }
            }
            catch (Exception e)
            {
                list.Add(new DictionaryEntry("1", "身份证"));
                list.Add(new DictionaryEntry("2", "其它"));
            }
            idtype.DataSource = list;
            idtype.DisplayMember = "Value";
            idtype.ValueMember = "Key";

            //idtype.Items.AddRange(new String[] {
            //    "身份证","其它"
            //});
            idtype.SelectedValue = "1";
            registerState.TextChanged += RegisterState_TextChanged;
            registerState.ReadOnly = true;
            registerState.ForeColor = Color.Red;
            registerState.BackColor = Color.White;
            id_number.TextChanged += Id_number_TextChanged;
            tel.TextChanged += Tel_TextChanged;
            prov.TextChanged += Prov_TextChanged;

            idtype.SelectedValueChanged += Idtype_SelectedValueChanged;

            this.Shown += Register_Shown;

        }

        private void Register_Shown(object sender, EventArgs e)
        {
            pictureBox1.Width = panel3.Width;
            pictureBox1.Height = panel3.Height;

            //pictureBox1.BackgroundImageChanged += PictureBox1_BackgroundImageChanged;
            //pictureBox1.
        }

        private void PictureBox1_BackgroundImageChanged(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            if (string.IsNullOrEmpty(box.ImageLocation)) { return; }
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            if (box.Image.Width > width)
            {
                pictureBox1.Height = (width / box.Image.Width) * box.Image.Height;
            }
        }

        private void Idtype_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox obj = sender as ComboBox;
            //if ("身份证".Equals(obj.SelectedItem.ToString()))
            //{
            //    queryByNum.Visible = false;
            //}
            //else {
            //    queryByNum.Visible = true;
            //}
        }

        private void Prov_TextChanged(object sender, EventArgs e)
        {
            var obj = sender as TextBox;
            if (obj.Text.IndexOf("北京") != -1
                )
            {
                city.Text = "北京市";
            }
            else if (obj.Text.IndexOf("上海") != -1)
            {
                city.Text = "上海市";
            }
            else if (obj.Text.IndexOf("天津") != -1)
            {
                city.Text = "天津市";
            }
            else if (obj.Text.IndexOf("重庆") != -1)
            {
                city.Text = "重庆市";
            }
        }

        private void Tel_TextChanged(object sender, EventArgs e)
        {
            var obj = sender as TextBox;
            if (string.IsNullOrEmpty(obj.Text)) { return; }
            Regex regex = new Regex(@"^(-)?\d+(\.\d+)?$");
            String str = obj.Text;
            if (!regex.IsMatch(str))
            {
                obj.Text = obj.Text.Substring(0, obj.Text.Length - 1);
            }
        }

        private void Id_number_TextChanged(object sender, EventArgs e)
        {
            if (idtype.SelectedItem != null && (!"身份证".Equals(((DictionaryEntry)idtype.SelectedItem).Value.ToString()))) {
                
                String sql = " select count(1) from csp_conf_register r " +
                " left join csp_main_person p on r.person_id = p.id " +
                " left join csp_conference c on r.conf_id = c.id " +
                "where c.conf_no = '" + confNo + "' and p.id_number = '" + id_number.Text + "' " +
                " and id_type='"+ ((DictionaryEntry)idtype.SelectedItem).Key.ToString() + "' " +
                "";
                String res = DBsqliteHelper.getSqlResult(sql);
                int count = int.Parse(res);
                if (count > 0)
                {
                    registerState.Text = "已注册";
                }
                else
                {
                    String isInDb = " select count(1) from csp_main_person where id_number = '" + id_number.Text + "'  ";
                    String resIsIndb = DBsqliteHelper.getSqlResult(isInDb);
                    if (int.Parse(resIsIndb) > 0)
                    {
                        registerState.Text = "未注册";
                    }
                    else
                    {
                        registerState.Text = "未在数据库中且未注册";
                    }
                }
                String sql2 = " select *  from csp_main_person where id_number='" + id_number.Text + "'";
                DataTable dt = DBsqliteHelper.getDataTable(sql2);
                if (dt.Rows.Count >0 )
                {
                    DataRow dr = dt.Rows[0];
                    name.Text = dr["NAME"].ToString();
                    prov.Text = dr["PROVINCE"].ToString();
                    id_number.Text = dr["ID_NUMBER"].ToString();
                    city.Text = dr["CITY"].ToString();
                    country.Text = dr["COUNTY"].ToString();
                    address.Text = dr["WORKPLACE"].ToString();
                    idtype.SelectedValue = dr["ID_TYPE"].ToString();
                    if ("1".Equals(dr["GENDER"].ToString()))
                    {
                        gender.SelectedItem = "男";
                    }
                    else
                    {
                        gender.SelectedItem = "女";
                    }
                    birthday.Text = dr["BIRTHDAY"].ToString();
                    tel.Text = dr["TEL"].ToString();
                    email.Text = dr["EMAIL"].ToString();

                    person_id.Text = dr["ID"].ToString();
                    person_type.Text = getPersonTypeById(dr["ID"].ToString());
                }
                return; }

            var obj = sender as TextBox;
            registerState.Text = "";

            if (string.IsNullOrEmpty(obj.Text)) { return; }
            Regex regex = new Regex(@"^\d+$");
            String str = obj.Text;
            str = str.Replace("x", "").Replace("X", "");
            if (!regex.IsMatch(str))
            {
                obj.Text = obj.Text.Substring(0, obj.Text.Length - 1);
            }
            if (obj.Text.IndexOf("x") != -1)
            {
                obj.TextChanged -= Id_number_TextChanged;
                obj.Text = obj.Text.Replace("x", "X");
                obj.Select(obj.Text.Length, 0);
                obj.ScrollToCaret();
                obj.TextChanged += Id_number_TextChanged;
            }
            if (obj.Text.Length == 15 || obj.Text.Length == 18)
            {
                String sql = " select count(1) from csp_conf_register r " +
                " left join csp_main_person p on r.person_id = p.id " +
                " left join csp_conference c on r.conf_id = c.id " +
                "where c.conf_no = '" + confNo + "' and p.id_number = '" + id_number.Text + "' ";
                String res = DBsqliteHelper.getSqlResult(sql);
                int count = int.Parse(res);
                if (count > 0)
                {
                    registerState.Text = "已注册";
                }
                else
                {
                    String isInDb = " select count(1) from csp_main_person where id_number = '" + id_number.Text + "'  ";
                    String resIsIndb = DBsqliteHelper.getSqlResult(isInDb);
                    if (int.Parse(resIsIndb) > 0)
                    {
                        registerState.Text = "未注册";
                    }
                    else
                    {
                        registerState.Text = "未在数据库中且未注册";
                    }
                }

                String sql2 = " select *  from csp_main_person where id_number='" + obj.Text + "'";
                DataTable dt = DBsqliteHelper.getDataTable(sql2);
                if (dt.Rows.Count < 1)
                {
                    //MessageBox.Show("此来宾在数据库记录中无法匹配请确认并补充信息后注册");
                    var controls = panel2.Controls;
                    foreach (Control ctl in controls)
                    {
                        if (ctl is TextBox)
                        {
                            var ctlObj = ctl as TextBox;
                            ctlObj.ReadOnly = false;
                        }
                        else if (ctl is ComboBox)
                        {
                            var ctlObj = ctl as ComboBox;
                            ctlObj.Enabled = true;

                        }
                        else if (ctl is DateTimePicker)
                        {
                            var ctlObj = ctl as DateTimePicker;
                            ctlObj.Enabled = true;
                            ctlObj.Value = DateTime.Now;
                        }
                    }
                    person_id.Text = "";
                    person_type.Text = "";
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    name.Text = dr["NAME"].ToString();
                    prov.Text = dr["PROVINCE"].ToString();
                    obj.TextChanged -= Id_number_TextChanged;
                    id_number.Text = dr["ID_NUMBER"].ToString().ToUpper();
                    obj.TextChanged += Id_number_TextChanged;
                    city.Text = dr["CITY"].ToString();
                    country.Text = dr["COUNTY"].ToString();
                    address.Text = dr["WORKPLACE"].ToString();
                    idtype.SelectedValue = "1";
                    if ("1".Equals(dr["GENDER"].ToString()))
                    {
                        gender.SelectedItem = "男";
                    }
                    else
                    {
                        gender.SelectedItem = "女";
                    }
                    birthday.Text = dr["BIRTHDAY"].ToString();
                    tel.Text = dr["TEL"].ToString();
                    email.Text = dr["EMAIL"].ToString();

                    person_id.Text = dr["ID"].ToString();
                    person_type.Text = getPersonTypeById(dr["ID"].ToString());

                    String idNum = obj.Text;
                    String dateStr = "2000-01-01";
                    int gen = 0;
                    if (idNum.Length == 15)
                    {
                        gen = int.Parse(idNum.Substring(14));

                        dateStr = "19" + idNum.Substring(6, 2) + "-"
                                    + idNum.Substring(8, 2) + "-" + idNum.Substring(10, 2);


                    }
                    else if (idNum.Length == 18)
                    {
                        gen = int.Parse(idNum.Substring(16, 1));
                        dateStr = idNum.Substring(6, 4) + "-"
                                   + idNum.Substring(10, 2) + "-" + idNum.Substring(12, 2);
                    }
                    if (gender.SelectedItem == null)
                    {
                        if (gen % 2 == 0)
                        {
                            gender.SelectedItem = "女";
                        }
                        else
                        {
                            gender.SelectedItem = "男";
                        }
                    }
                    DateTime dparse;
                    try
                    {
                        dparse = DateTime.Parse(dateStr);
                        birthday.Value = dparse;
                    }
                    catch (Exception dateExp)
                    {
                    }

                }

            }

        }

        private void btn_registPrint_Click(object sender, EventArgs e)
        {
            personName = name.Text;
            provStr = city.Text + country.Text;
            placeStr = address.Text;
            idNumber = id_number.Text;

            if (string.IsNullOrEmpty(tb_addIdNum.Text))
            {
                qrCodeStr = getQrcodeFromDb("", idNumber);
                if (string.IsNullOrEmpty(qrCodeStr))
                {
                    qrCodeStr = MD5Utils.GetMd5(confNo + ":" + idNumber);
                }
            }
            else {
                qrCodeStr = MD5Utils.GetMd5(confNo + ":" + tb_addIdNum.Text);
            }

            bool needToDb = false;

            if (String.IsNullOrEmpty(personName) || String.IsNullOrEmpty(idNumber)
                || String.IsNullOrEmpty(provStr) || String.IsNullOrEmpty(placeStr) || String.IsNullOrEmpty(qrCodeStr)
                )
            {
                MessageBox.Show("信息填写不完整");

                var controls = panel2.Controls;
                foreach (Control ctl in controls)
                {
                    if (ctl is TextBox)
                    {
                        var obj = ctl as TextBox;
                        if (string.IsNullOrEmpty(obj.Text))
                        {
                            obj.ReadOnly = false;
                        }
                    }
                    else if (ctl is ComboBox)
                    {
                        var obj = ctl as ComboBox;
                        obj.Enabled = true;
                    }
                    else if (ctl is DateTimePicker)
                    {
                        var obj = ctl as DateTimePicker;
                        obj.Enabled = true;
                    }
                }

                return;
            }
            if (!validateInput()) { return; }
            try
            {
                var controls = panel2.Controls;
                foreach (Control ctl in controls)
                {
                    if (ctl is TextBox)
                    {
                        var obj = ctl as TextBox;
                        if (!string.IsNullOrEmpty(obj.Text))
                        {
                            obj.ReadOnly = true;
                        }
                        if (!obj.ReadOnly)
                        {
                            needToDb = true;
                        }
                    }
                    else if (ctl is ComboBox)
                    {
                        var obj = ctl as ComboBox;
                        obj.Enabled = false;
                    }
                    else if (ctl is DateTimePicker)
                    {
                        var obj = ctl as DateTimePicker;
                        obj.Enabled = false;
                    }
                }

                if (needToDb || isCardScan.Equals("0"))
                { // 手工录入数据更新/插入数据库
                    String personSql = "";
                    String str_gender = "1";
                    if (gender.SelectedText.Equals("女"))
                    {
                        str_gender = "2";
                    }
                    //String idTypeStr = string.IsNullOrEmpty(person_type.Text) ? "" : person_type.Text;
                    String idTypeStr = idtype.SelectedValue.ToString();

                    String newId = Guid.NewGuid().ToString("N");
                    if (string.IsNullOrEmpty(person_id.Text)) // 新增
                    {
                        personSql = " insert into csp_main_person(id, province,city,county,workplace,id_type,id_number," +
                            "name,gender,birthday,tel,email , src_id , isupload ,data_version , pc_idtype , pc_idnumber,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME) " +
                                    " values('" + newId + "', '" + prov.Text + "', '" + city.Text + "', '" + country.Text + "'," +
                                    " '" + address.Text + "', '" + idTypeStr + "','" + id_number.Text + "', '" + name.Text + "',  " +
                                    "'" + str_gender + "', '" + birthday.Value.ToString("yyyy-MM-dd") + "', '" + tel.Text + "', '" +
                                    email.Text + "', REPLACE(UUID(), '-', ''), '0', '0','" + tb_addIdType.Text + "','" + tb_addIdNum.Text + "'" +
                                    ",'" + mainForm.username + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'"+
                                    ",'" +mainForm.username+"','"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')" +
                                    "ON DUPLICATE KEY UPDATE " +
                                    " province ='"+prov.Text+"' , city='"+city.Text+ "',county='"+country.Text+ "',workplace='"+ address .Text+ "'," +
                                    " id_type='"+ idTypeStr + "',id_number='"+ id_number .Text+ "',name='"+name.Text+ "',gender='"+ str_gender + "'," +
                                    " birthday='"+ birthday.Value.ToString("yyyy-MM-dd") + "',tel='"+tel.Text+ "',email='"+ email .Text+ "'," +
                                    "isupload = '0',pc_idtype='"+ tb_addIdType.Text + "',pc_idnumber='"+ tb_addIdNum.Text + "'," +
                                    " update_by='"+mainForm.username+ "',UPDATE_TIME='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' ";

                        int ret =  DBsqliteHelper.ExecuteNonQuery(personSql);
                        if (ret > 1)
                        {
                            String pidSql = " Select id from csp_main_person where id_type='"+ idTypeStr + "' and id_number='"+ id_number .Text+ "' ";
                            String pid = DBsqliteHelper.getSqlResult(pidSql);
                            person_id.Text = pid;
                            person_type.Text = getPersonTypeById(pid);
                        }
                        else {
                            person_id.Text = newId;
                            person_type.Text = "1";
                        }                        
                    }
                    else
                    { //更新
                        personSql = " UPDATE csp_main_person set province= '" + prov.Text + "'," +
                            "city = '" + city.Text + "'," +
                            "county= '" + country.Text + "'," +
                            "workplace= '" + address.Text + "'," +
                            "id_type= '" + idTypeStr + "'," +
                            "id_number='" + id_number.Text + "'," +
                            "name='" + name.Text + "'," +
                            "gender='" + str_gender + "'," +
                            "birthday= '" + birthday.Value.ToString("yyyy-MM-dd") + "'," +
                            "tel='"+tel.Text + "'," +
                            "email= '" + email.Text + "' ," +
                            " isupload='0' ," +
                            " pc_idtype='" + tb_addIdType.Text + "' ," +
                            " pc_idnumber='" + tb_addIdNum.Text + "' ," +
                            " update_time='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,"+
                            " update_by='"+ mainForm.username + "' " +
                            " where id='"+ person_id.Text + "' ";
                        DBsqliteHelper.ExecuteNonQuery(personSql);
                    }


                    Dictionary<String, String> dic_person = AmqpDataHandler.GetPersonInfo(person_id.Text);
                    string confId = DBsqliteHelper.getSqlResult(" select id from csp_conference where conf_no='"+ confNo +"' ");
                    string terminalId = Utils.getMachineUUID();
                    dic_person.Add("confId", confId);
                    dic_person.Add("confNo",confNo);
                    dic_person.Add("terminalId","PC_"+terminalId);
                    string tempQrStr = confNo + ":" + 
                        (string.IsNullOrEmpty(tb_addIdNum.Text) ? dic_person["idNumber"] : tb_addIdNum.Text);
                    dic_person.Add("personConfNo",MD5Utils.GetMd5(tempQrStr));
                    AmqpUtils.GetInstance().PushPersonMsg(JsonConvert.SerializeObject(dic_person));
                }
            }
            catch (Exception exp)
            {
                Log.WriteLogLine("新增数据更新插入失败:" + exp.Message);
            }

            // 打印
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("name", personName);
            dic.Add("prov", provStr);
            dic.Add("place", placeStr);
            dic.Add("qrCode", qrCodeStr);

            dic.Add("idNumber",idNumber);

            String imagePath = "";

            // 注册
            string register_id = Guid.NewGuid().ToString("N");
            string src_id_register = Guid.NewGuid().ToString("N");
            String sqlInsert = " insert into csp_conf_register( id , person_id ,person_type,conf_id ," +
                "conf_place_id,remark,register_by,register_time,create_time,create_by," +
                "is_scancard,print_img,ISUPLOAD,src_id)values('"+ register_id + "','" + person_id.Text + "','" + person_type.Text
                + "',(select id from csp_conferencelist where conf_no='" + confNo + "')," +
                " '','','" + mainForm.username + "',now(),now(),'" + mainForm.username + "','" + isCardScan + "'  " +
                ",'" + imagePath + "','0','"+ src_id_register + "' ) ";
            DBsqliteHelper.ExecuteNonQuery(sqlInsert); //登记数据入库

            imagePath = PrintImg(dic);
            // 更细图片路径
            string updateSql = " update csp_conf_register set print_img='"+ imagePath + "' where id = '"+ register_id + "' ";
            DBsqliteHelper.ExecuteNonQuery(updateSql);
            // 修改按钮状态
            var btn = sender as Button;
            btn.Enabled = false;
            registerState.Text = "已注册";

            Dictionary<String, String> dic_register = AmqpDataHandler.GetRegisterInfo(register_id);
            string tid = Utils.getMachineUUID();
            dic_register.Add("confNo", confNo);
            dic_register.Add("terminalId", "PC_" + tid);
            //AmqpUtils.GetInstance().PushMsg(JsonConvert.SerializeObject(dic_register), "csp/register/PC_" + tid);
            AmqpUtils.GetInstance().PushRegisterMsg(JsonConvert.SerializeObject(dic_register));
        }

        public String PrintImg(Dictionary<String, String> dic)
        {

            if (!Directory.Exists(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\");
            }
            if (!Directory.Exists(Environment.CurrentDirectory + "\\temp\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\temp\\");
            }

            iTextPdfUtils pdfUtils = new iTextPdfUtils();
            String pdfName;
            Image image;
            try {
                string idNumber = dic["idNumber"];
                //confNo;

                string typeSql = "select if(v.id is  not null , '1' , if (l.id is not null  ,'2' , if (w.id is not null ,'3',null ) )  )  " +
                                " from csp_main_person p " +
                                " left join csp_conf_visitor v on v.visitor_id = p.id and v.conf_id = (select id from csp_conferencelist c where c.conf_no = '"+confNo+"' ) " +
                                " left join csp_conf_worker w on w.worker_id = p.id and w.conf_id = (select id from csp_conferencelist c where c.conf_no = '"+confNo+"' ) " +
                                " left join csp_conf_lecturer l on l.conf_lecturer_id = p.id and l.conf_id = (select id from csp_conferencelist c where c.conf_no = '"+confNo+"' ) " +
                                 " where id_number = '"+idNumber+"' ";

                string res = DBsqliteHelper.getSqlResult(typeSql);
                if (string.IsNullOrEmpty(res))
                {
                    Log.WriteLogLine("人员类型查询失败 idNumber:" + idNumber);
                    pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                    pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\defaultTemplateWithOutImg.pdf", pdfName, dic);
                    Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
                    pd.LoadFromFile(pdfName);
                    image = pd.SaveAsImage(0);
                }
                else {

                    string dataMapsql = " select  file_datamap , file_localpath from csp_conf_printfile where conf_no = '"+confNo+"'" +
                        " and fileclass='2' and file_type='"+res.Trim()+"' ";
                    DataTable dt = DBsqliteHelper.getDataTable(dataMapsql);
                    if (dt.Rows.Count > 0)
                    {
                        Dictionary<String, String> mapDataDic = new Dictionary<String,String>();
                        string dataMap = dt.Rows[0]["FILE_DATAMAP"].ToString();
                        //JObject jsonObj = JObject.Parse(dataMap);
                        DataTable jsonObj = JsonConvert.DeserializeObject<DataTable>(dataMap);
                        int count = jsonObj.Rows.Count;
                        for ( int i = 0; i < count; i++ ) {
                            
                            string querySql = jsonObj.Rows[i]["querySql"].ToString();
                            // 查询人员表则增加人员idNumber的过滤条件
                            if ( querySql.ToUpper().Contains("CSP_MAIN_PERSON") ) {
                                if (querySql.ToUpper().Contains("WHERE"))
                                {
                                    querySql += " AND ID_NUMBER ='"+idNumber+"'";
                                }
                                else {
                                    querySql += " Where ID_NUMBER='"+idNumber+"' ";
                                }
                            }
                            string dicValue = DBsqliteHelper.getSqlResult(querySql);
                            if (string.IsNullOrEmpty(dicValue)) { dicValue = " "; }
                            mapDataDic.Add(jsonObj.Rows[i]["attrName"].ToString().Trim(),dicValue);
                        }
                        mapDataDic.Add("qrCode",dic["qrCode"]);

                        pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

                        if (!File.Exists(dt.Rows[0]["FILE_LOCALPATH"].ToString()))
                        {
                            Log.WriteLogLine("数据库记录的PDF模板在本地文件目录不存在" + dt.Rows[0]["FILE_LOCALPATH"].ToString());
                            pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                            pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\defaultTemplateWithOutImg.pdf", pdfName, dic);
                            Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
                            pd.LoadFromFile(pdfName);
                            image = pd.SaveAsImage(0);

                        }
                        else {
                            pdfUtils.FillForm(dt.Rows[0]["FILE_LOCALPATH"].ToString(), pdfName, mapDataDic);
                            Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
                            pd.LoadFromFile(pdfName);
                            image = pd.SaveAsImage(0);
                        }                       
                    }
                    else {
                        Log.WriteLogLine("获取本地打印模板失败 idNumber:" + idNumber+",confNo:"+confNo);
                        pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                        pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\defaultTemplateWithOutImg.pdf", pdfName, dic);
                        Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
                        pd.LoadFromFile(pdfName);
                        image = pd.SaveAsImage(0);
                    }
                }

            }
            catch (Exception e) {
                Log.WriteLogLine("通过下发数据组装pdf模版出现异常:"+e.Message);
                pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\defaultTemplateWithOutImg.pdf", pdfName, dic);
                Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
                pd.LoadFromFile(pdfName);
                image = pd.SaveAsImage(0);
            }
            



            String imagePath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\"
               + "print_" + confNo + "+" + provStr + "+" + placeStr + "+" + personName + "+" + MD5Utils.GetMd5(confNo + ":" + this.idNumber) + ".png";

            image.Save(imagePath, ImageFormat.Png);
            printImgPath = imagePath;
            PrintDocument pdoc = new PrintDocument();
            Margins margin = new Margins(0, 0, 0, 0);
            pdoc.DefaultPageSettings.Margins = margin;
            pdoc.DefaultPageSettings.PaperSize = new PaperSize("Common Test 1", 531, 728);
            pdoc.DefaultPageSettings.PrinterSettings.PrinterName = "Canon iP110 series";
            pdoc.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            
            try
            {
                pdoc.Print();
                File.Delete(pdfName);
            }
            catch (Exception exp)
            {
                Log.WriteLogLine("临时文件删除失败:" + exp.Message);
            }

            return imagePath;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            //e.Graphics.DrawImage(System.Drawing.Image.FromFile("替换结果图片.png"),new PointF(0,0));
            //e.PageBounds.Width;
            //e.PageBounds.Height;
            System.Drawing.Image temp = System.Drawing.Image.FromFile(printImgPath);//
            //System.Drawing.Image newImg = temp.GetThumbnailImage(e.PageBounds.Width, e.PageBounds.Height,
            //    new System.Drawing.Image.GetThumbnailImageAbort(IsTrue), IntPtr.Zero);
            e.Graphics.PageScale = 0.7f;
            e.Graphics.DrawImage(temp, new System.Drawing.Rectangle(0, 0, 130, 177)); // 13cm * 17.7cm

            printImgPath = "";
        }

        private void btn_reprint_Click(object sender, EventArgs e)
        {
            // 非空校验
            String sql = " select * from csp_conf_register r where exists(" +
                "select 1 from csp_main_person p where p.id_number = '" + id_number.Text + "' and p.id = r.person_id)";
            DataTable dt = DBsqliteHelper.getDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("未注册用户无法补打请先执行《注册并打印》");
                return;
            }
            else
            {

                DataRow dr = dt.Rows[0];
                if (!String.IsNullOrEmpty(dr["PRINT_IMG"].ToString().Trim()))
                {
                    if (File.Exists(dr["PRINT_IMG"].ToString().Trim()))
                    {
                        printImgPath = dr["PRINT_IMG"].ToString().Trim(); // 得到图片路径赋值
                        PrintDocument pdoc = new PrintDocument();
                        Margins margin = new Margins(0, 0, 0, 0);
                        pdoc.DefaultPageSettings.Margins = margin;
                        pdoc.DefaultPageSettings.PaperSize = new PaperSize("Common Test 1", 531, 728);
                        pdoc.DefaultPageSettings.PrinterSettings.PrinterName = "Canon iP110 series";
                        pdoc.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                        pdoc.Print();

                        return;
                    }
                }

                personName = name.Text;
                provStr = city.Text + country.Text;
                placeStr = address.Text;
                idNumber = id_number.Text;
                qrCodeStr = getQrcodeFromDb("", idNumber);
                if (string.IsNullOrEmpty(qrCodeStr)) { qrCodeStr = MD5Utils.GetMd5(confNo + ":" + idNumber); }
                
                if (String.IsNullOrEmpty(personName) || String.IsNullOrEmpty(idNumber)
                    || String.IsNullOrEmpty(provStr) || String.IsNullOrEmpty(placeStr) || String.IsNullOrEmpty(qrCodeStr)
                    )
                {
                    MessageBox.Show("信息填写不完整");
                    var controls = panel2.Controls;
                    foreach (Control ctl in controls)
                    {
                        if (ctl is TextBox)
                        {
                            var obj = ctl as TextBox;
                            if (string.IsNullOrEmpty(obj.Text))
                            {
                                obj.ReadOnly = false;
                            }

                        }
                        else if (ctl is ComboBox)
                        {
                            var obj = ctl as ComboBox;
                            obj.Enabled = true;
                        }
                        else if (ctl is DateTimePicker)
                        {
                            var obj = ctl as DateTimePicker;
                            obj.Enabled = true;
                        }
                    }
                    return;
                }

                // 打印
                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("name", personName);
                dic.Add("prov", provStr);
                dic.Add("place", placeStr);
                dic.Add("qrCode", qrCodeStr);
                dic.Add("idNumber", idNumber);
                
                String imagePath = PrintImg(dic);

                // 注册
                String sqlInsert = " update csp_conf_register  set print_img='" + imagePath + "' where id='" + dr["ID"].ToString().Trim() + "'";
                DBsqliteHelper.ExecuteNonQuery(sqlInsert); //数据入库
            }
        }

        private void queryByNum_Click(object sender, EventArgs e)
        {
            if ("1".Equals(isCardScan)) { return; }

            if (string.IsNullOrEmpty(name.Text) && string.IsNullOrEmpty(tel.Text))
            {
                MessageBox.Show("姓名、手机号不能同时为空");
                return;
            }
            PersonSelectForm psf = new PersonSelectForm(name.Text.Trim(), tel.Text.Trim(), confNo);
            psf.Owner = this;
            psf.uname = name.Text.Trim();
            psf.telNum = tel.Text.Trim();
            psf.ShowDialog();
            tb_addIdType.Text = psf.returnType;
            tb_addIdNum.Text = psf.returnNum;
            //if ( (!string.IsNullOrEmpty(psf.returnType)) && ( !string.IsNullOrEmpty(psf.returnNum) ) ) {

            //}
            //psf.Close();

            //String sql = " select count(1) from csp_conf_register r " +
            //" left join csp_main_person p on r.person_id = p.id " +
            //" left join csp_conference c on r.conf_id = c.id " +
            //" where c.conf_no = '" + confNo + "' and p.id_number = '" + id_number.Text + "' ";
            //String res = DBsqliteHelper.getSqlResult(sql);
            //int count = int.Parse(res);
            //if (count > 0)
            //{
            //    registerState.Text = "已注册";
            //}
            //else
            //{
            //    String isInDb = " select count(1) from csp_main_person where id_number = '" + id_number.Text + "'  ";
            //    String resIsIndb = DBsqliteHelper.getSqlResult(isInDb);
            //    if (int.Parse(resIsIndb) > 0)
            //    {
            //        registerState.Text = "未注册";
            //    }
            //    else
            //    {
            //        registerState.Text = "未在数据库中且未注册";
            //    }
            //}

            //String sql2 = " select *  from csp_main_person where id_number='" + id_number.Text + "'";
            //DataTable dt = DBsqliteHelper.getDataTable(sql2);
            //if (dt.Rows.Count < 1)
            //{
            //    var controls = panel2.Controls;
            //    foreach (Control ctl in controls)
            //    {
            //        if (ctl is TextBox)
            //        {
            //            var ctlObj = ctl as TextBox;
            //            ctlObj.ReadOnly = false;
            //            ctlObj.Text = "";
            //        }
            //        else if (ctl is ComboBox)
            //        {
            //            var ctlObj = ctl as ComboBox;
            //            ctlObj.Enabled = true;
            //            ctlObj.SelectedText = "";

            //        }
            //        else if (ctl is DateTimePicker)
            //        {
            //            var ctlObj = ctl as DateTimePicker;
            //            ctlObj.Enabled = true;
            //            ctlObj.Value = DateTime.Now;
            //        }
            //    }
            //    person_id.Text = "";
            //    person_type.Text = "";
            //}
            //else
            //{
            //    DataRow dr = dt.Rows[0];
            //    name.Text = dr["NAME"].ToString();
            //    prov.Text = dr["PROVINCE"].ToString();
            //    id_number.Text = dr["ID_NUMBER"].ToString();
            //    city.Text = dr["CITY"].ToString();
            //    country.Text = dr["COUNTY"].ToString();
            //    address.Text = dr["WORKPLACE"].ToString();
            //    //idtype.SelectedItem = "身份证";
            //    if ("1".Equals(dr["GENDER"].ToString()))
            //    {
            //        gender.SelectedItem = "男";
            //    }
            //    else
            //    {
            //        gender.SelectedItem = "女";
            //    }
            //    birthday.Text = dr["BIRTHDAY"].ToString();
            //    tel.Text = dr["TEL"].ToString();
            //    email.Text = dr["EMAIL"].ToString();

            //    person_id.Text = dr["ID"].ToString();
            //    person_type.Text = dr["TYPE"].ToString();
            //}
        }

        private void RegisterState_TextChanged(object sender, EventArgs e)
        {
            var obj = sender as TextBox;
            if (obj.Text.StartsWith("已"))
            {
                btn_registPrint.Enabled = false;
            }
            else
            {
                btn_registPrint.Enabled = true;
            }
        }

        private void btn_typeinput_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            registerState.Text = "";
            btn_registPrint.Enabled = true;
            id_number.Text = "";
            person_id.Text = "";
            person_type.Text = "";
            personName = name.Text;
            provStr = city.Text + country.Text;
            placeStr = address.Text;
            idNumber = id_number.Text;
            qrCodeStr = confNo + ":" + idNumber;
            //if (String.IsNullOrEmpty(personName) || String.IsNullOrEmpty(idNumber)
            //    || String.IsNullOrEmpty(provStr) || String.IsNullOrEmpty(placeStr) || String.IsNullOrEmpty(qrCodeStr)
            //    ) { MessageBox.Show("信息填写不完整"); return; }

            isCardScan = "0";

            var controls = panel2.Controls;
            foreach (Control ctl in controls)
            {
                if (ctl is TextBox)
                {
                    var obj = ctl as TextBox;
                    obj.ReadOnly = false;
                }
                else if (ctl is ComboBox)
                {
                    var obj = ctl as ComboBox;
                    obj.Enabled = true;
                }
                else if (ctl is DateTimePicker)
                {
                    var obj = ctl as DateTimePicker;
                    obj.Enabled = true;
                }
            }
        }

        private void btn_getidentity_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            btn_registPrint.Enabled = true;
            var controls = panel2.Controls;
            foreach (Control ctl in controls)
            {
                if (ctl is TextBox)
                {
                    var obj = ctl as TextBox;
                    obj.ReadOnly = true;
                }
                else if (ctl is ComboBox)
                {
                    var obj = ctl as ComboBox;
                    obj.Enabled = false;
                }
                else if (ctl is DateTimePicker)
                {
                    var obj = ctl as DateTimePicker;
                    obj.Enabled = false;
                }
            }
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
                id_number.Text = sb.ToString();
            }
            CardReader.CVR_CloseComm();

            String sql = " select count(1) from csp_conf_register r " +
                " left join csp_main_person p on r.person_id = p.id " +
                " left join csp_conference c on r.conf_id = c.id " +
                "where c.conf_no = '" + confNo + "' and p.id_number = '" + id_number.Text + "' ";
            String res = DBsqliteHelper.getSqlResult(sql);
            int count = int.Parse(res);
            if (count > 0)
            {
                registerState.Text = "已注册";
                btn_registPrint.Enabled = false;
            }
            else
            {
                String isInDb = " select count(1) from csp_main_person where id_number = '" + id_number.Text + "'  ";
                String resIsIndb = DBsqliteHelper.getSqlResult(isInDb);
                if (int.Parse(resIsIndb) > 0)
                {
                    registerState.Text = "未注册";
                }
                else
                {
                    registerState.Text = "未在数据库中且未注册";
                }
            }

            String sql2 = " select *  from csp_main_person where id_number='" + sb.ToString() + "'";
            DataTable dt = DBsqliteHelper.getDataTable(sql2);
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("此来宾在数据库记录中无法匹配请确认并补充信息后注册");
                foreach (Control ctl in controls)
                {
                    if (ctl is TextBox)
                    {
                        var obj = ctl as TextBox;
                        obj.ReadOnly = false;
                    }
                    else if (ctl is ComboBox)
                    {
                        var obj = ctl as ComboBox;
                        obj.Enabled = true;
                    }
                    else if (ctl is DateTimePicker)
                    {
                        var obj = ctl as DateTimePicker;
                        obj.Enabled = true;
                    }
                }

            }
            else
            {
                DataRow dr = dt.Rows[0];
                name.Text = dr["NAME"].ToString();
                prov.Text = dr["PROVINCE"].ToString();
                id_number.TextChanged -= Id_number_TextChanged;
                id_number.Text = dr["ID_NUMBER"].ToString();
                id_number.TextChanged += Id_number_TextChanged;

                city.Text = dr["CITY"].ToString();
                country.Text = dr["COUNTY"].ToString();
                address.Text = dr["WORKPLACE"].ToString();
                idtype.SelectedValue = "1";
                if ("1".Equals(dr["GENDER"].ToString()))
                {
                    gender.SelectedItem = "男";
                }
                else
                {
                    gender.SelectedItem = "女";
                }
                birthday.Text = dr["BIRTHDAY"].ToString();
                tel.Text = dr["TEL"].ToString();
                email.Text = dr["EMAIL"].ToString();

                person_id.Text = dr["ID"].ToString();
                person_type.Text = getPersonTypeById(dr["ID"].ToString());
            }
            isCardScan = "1";

            String idNum = sb.ToString();
            String dateStr = "2000-01-01";
            int gen = 0;
            if (idNum.Length == 15)
            {
                gen = int.Parse(idNum.Substring(14));

                dateStr = "19" + idNum.Substring(6, 2) + "-"
                            + idNum.Substring(8, 2) + "-" + idNum.Substring(10, 2);


            }
            else if (idNum.Length == 18)
            {
                gen = int.Parse(idNum.Substring(16, 1));
                dateStr = idNum.Substring(6, 4) + "-"
                           + idNum.Substring(10, 2) + "-" + idNum.Substring(12, 2);
            }
            if (gender.SelectedItem == null)
            {
                if (gen % 2 == 0)
                {
                    gender.SelectedItem = "女";
                }
                else
                {
                    gender.SelectedItem = "男";
                }
            }

            birthday.Text = dateStr;
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            personName = name.Text;
            provStr = city.Text + country.Text;
            placeStr = address.Text;
            idNumber = id_number.Text;
            if (string.IsNullOrEmpty(tb_addIdNum.Text))
            {
                qrCodeStr = getQrcodeFromDb("", idNumber);
                if (string.IsNullOrEmpty(qrCodeStr))
                {
                    qrCodeStr = MD5Utils.GetMd5(confNo + ":" + idNumber);
                }
            }
            else
            {
                qrCodeStr = MD5Utils.GetMd5(confNo + ":" + tb_addIdNum.Text);
            }
            if (String.IsNullOrEmpty(personName) || String.IsNullOrEmpty(idNumber)
                || String.IsNullOrEmpty(provStr) || String.IsNullOrEmpty(placeStr) || String.IsNullOrEmpty(qrCodeStr)
                ) { MessageBox.Show("请先读取身份证或录入信息"); return; }
            if (!validateInput()) { return; }
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("name", personName);
            dic.Add("prov", provStr);
            dic.Add("place", placeStr);
            dic.Add("qrCode", qrCodeStr);
            dic.Add("idNumber", idNumber);

            if (!Directory.Exists(Environment.CurrentDirectory + "\\temp\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\temp\\");
            }

            if (!Directory.Exists(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\");
            }

            iTextPdfUtils pdfUtils = new iTextPdfUtils();
            String pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            Image image;

            string typeSql = "select if(v.id is not null , '1' , if (l.id is not null ,'2' , if (w.id is not null ,'3',null ) )  )  " +
                                " from csp_main_person p " +
                                " left join csp_conf_visitor v on v.visitor_id = p.id and v.conf_id = (select id from csp_conferencelist c where c.conf_no = '" + confNo + "' ) " +
                                " left join csp_conf_worker w on w.worker_id = p.id and w.conf_id = (select id from csp_conferencelist c where c.conf_no = '" + confNo + "' ) " +
                                " left join csp_conf_lecturer l on l.conf_lecturer_id = p.id and l.conf_id = (select id from csp_conferencelist c where c.conf_no = '" + confNo + "' ) " +
                                 " where id_number = '" + idNumber + "' ";
            try {
                string res = DBsqliteHelper.getSqlResult(typeSql);
                if (string.IsNullOrEmpty(res))
                {
                    pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\default.pdf", pdfName, dic);
                }
                else
                {
                    string dataMapsql = " select  file_datamap , file_localpath from csp_conf_printfile where conf_no = '" + confNo + "'" +
                          " and fileclass='1' and file_type='" + res.Trim() + "' ";
                    DataTable dt = DBsqliteHelper.getDataTable(dataMapsql);
                    if (dt.Rows.Count > 0)
                    {
                        Dictionary<String, String> mapDataDic = new Dictionary<String, String>();
                        string dataMap = dt.Rows[0]["FILE_DATAMAP"].ToString();
                        //JObject jsonObj = JObject.Parse(dataMap);
                        DataTable jsonObj = JsonConvert.DeserializeObject<DataTable>(dataMap);
                        int count = jsonObj.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string querySql = jsonObj.Rows[i]["querySql"].ToString();
                            // 查询人员表则增加人员idNumber的过滤条件
                            if (querySql.ToUpper().Contains("CSP_MAIN_PERSON"))
                            {
                                if (querySql.ToUpper().Contains("WHERE"))
                                {
                                    querySql += " AND ID_NUMBER ='" + idNumber + "'";
                                }
                                else
                                {
                                    querySql += " Where ID_NUMBER='" + idNumber + "' ";
                                }
                            }
                            string dicValue = DBsqliteHelper.getSqlResult(querySql);
                            if (string.IsNullOrEmpty(dicValue)) { dicValue = " "; }
                            mapDataDic.Add(jsonObj.Rows[i]["attrName"].ToString().Trim(), dicValue);
                        }
                        mapDataDic.Add("qrCode", dic["qrCode"]);

                        pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

                        if (!File.Exists(dt.Rows[0]["FILE_LOCALPATH"].ToString()))
                        {
                            Log.WriteLogLine("数据库记录的PDF模板在本地文件目录不存在:" + dt.Rows[0]["FILE_LOCALPATH"].ToString());
                            pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                            pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\default.pdf", pdfName, dic);
                        }
                        else
                        {
                            pdfUtils.FillForm(dt.Rows[0]["FILE_LOCALPATH"].ToString(), pdfName, mapDataDic);
                        }
                    }
                    else
                    {
                        Log.WriteLogLine("获取本地打印模板失败 idNumber:" + idNumber + ",confNo:" + confNo);
                        pdfName = Environment.CurrentDirectory + "\\temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                        pdfUtils.FillForm(Environment.CurrentDirectory + "\\Templates\\default.pdf", pdfName, dic);
                    }
                }
            }
            catch (Exception exp) {
                Log.WriteLogLine("预览处理出现异常："+exp.Message);
            }
            
            Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
            pd.LoadFromFile(pdfName);
            image = pd.SaveAsImage(0);

            String imagePath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\"
                + confNo + "+" + provStr + "+" + placeStr + "+" + personName + "+" + MD5Utils.GetMd5(confNo + ":" + idNumber) + ".png";

            image.Save(imagePath, ImageFormat.Png);
            this.pictureBox1.Image = image;// Image.FromFile(Environment.CurrentDirectory + "\\sample.jpg", false);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                int width = pictureBox1.Width;
                int height = pictureBox1.Height;
                if (pictureBox1.Image.Width > width)
                {
                    pictureBox1.Height = (int)(((float)width / pictureBox1.Image.Width) * pictureBox1.Image.Height);
                }
                File.Delete(pdfName);
            }
            catch (Exception exp)
            {
                Log.WriteLogLine("临时文件删除失败：" + exp.Message);
            }
        }

        public void setValueByPersonSelect(DataGridViewRow dr)
        {
            name.Text = dr.Cells["姓名"].Value.ToString();
            prov.Text = dr.Cells["PROVINCE"].Value.ToString();

            city.Text = dr.Cells["CITY"].Value.ToString();
            country.Text = dr.Cells["COUNTY"].Value.ToString();
            address.Text = dr.Cells["单位"].Value.ToString();
            idtype.SelectedValue = dr.Cells["ID_TYPE"].Value.ToString();
            id_number.Text = dr.Cells["ID_NUMBER"].Value.ToString();
            gender.Text = dr.Cells["GENDER"].Value.ToString().Equals("1") ? "男" : "女";
            //try { birthday.Text = dr.Cells["BIRTHDAY"].Value.ToString(); }
            //catch (Exception exp) { }
            tel.Text = dr.Cells["手机号"].Value.ToString();
            email.Text = dr.Cells["EMAIL"].Value.ToString();
            person_id.Text = dr.Cells["ID"].Value.ToString();
            person_type.Text = getPersonTypeById(dr.Cells["ID"].Value.ToString()); // dr.Cells["TYPE"].Value.ToString()
            try
            {
                birthday.Text = dr.Cells["BIRTHDAY"].Value.ToString();
            }
            catch (Exception exp)
            {

            }
            String sql = " select count(1) from csp_conf_register r " +
                " left join csp_main_person p on r.person_id = p.id " +
                " left join csp_conference c on r.conf_id = c.id " +
                "where c.conf_no = '" + confNo + "' and p.id_number = '" + id_number.Text + "' ";
            String res = DBsqliteHelper.getSqlResult(sql);
            int count = int.Parse(res);
            if (count > 0)
            {
                registerState.Text = "已注册";
            }
            else
            {
                String isInDb = " select count(1) from csp_main_person where id_number = '" + id_number.Text + "'  ";
                String resIsIndb = DBsqliteHelper.getSqlResult(isInDb);
                if (int.Parse(resIsIndb) > 0)
                {
                    registerState.Text = "未注册";
                }
                else
                {
                    registerState.Text = "未在数据库中且未注册";
                }

            }
        }

        public String getPersonTypeById(String p_personId)
        {

            if (string.IsNullOrEmpty(p_personId)) { return "1"; }

            String sql = "select min(if(v.visitor_id is null ,if(l.conf_lecturer_id is null ,if(w.worker_id is null , '1', '3'),'2') ,'1') ) as p_type" +
                        " from csp_main_person p " +
                        " left join (select * from csp_conf_visitor where conf_id in(select id from csp_conference where conf_no = '" + confNo + "' ) ) v on v.visitor_id = p.id  " +
                        " left join (select* from csp_conf_lecturer where conf_id in(select id from csp_conference where conf_no= '" + confNo + "')) l on l.conf_lecturer_id = p.id " +
                        " left join (select* from csp_conf_worker where conf_id in(select id from csp_conference where conf_no= '" + confNo + "') )  w on w.worker_id = p.id  " +
                        " where p.id = '" + p_personId + "'";
            try
            {
                return DBsqliteHelper.getSqlResult(sql);
            }
            catch (Exception e)
            {
                return "1";
            }
        }

        public bool validateInput()
        {

            if (!"1".Equals(isCardScan))
            { // 手动录入才做校验

                // 身份证号
                if (("身份证".Equals(idtype.Text)) && (!Regex.IsMatch(id_number.Text, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
                {
                    MessageBox.Show("请输入正确的身份证号码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // 手机号
                if ((!Regex.IsMatch(tel.Text, @"^1\d{10}$", RegexOptions.IgnoreCase))) //@"^1[34578]\\d{9}$"
                {
                    MessageBox.Show("请输入正确的手机号码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // 电子邮箱
                if (!Regex.IsMatch(email.Text, "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("请输入正确的邮箱地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            return true;
        }

        public void ClearData()
        {

        }
        public void FillData()
        {

        }

        public String getQrcodeFromDb(String p_id, String p_idNum)
        {
            if (string.IsNullOrEmpty(p_id) && string.IsNullOrEmpty(p_idNum)) { return ""; }
            String sql = " select min(if(v.visitor_id is null ,if(l.conf_lecturer_id is null ,if(w.worker_id is null , '', w.WORKER_CON_NO), " +
                "l.LECTURER_CON_NO ) ,v.visitor_conf_no) ) as p_type " +
                         " from csp_main_person p " +
                         " left join(select * from csp_conf_visitor where conf_id in(select id from csp_conference where conf_no = '" + confNo + "' ) ) v on v.visitor_id = p.id " +
                         " left join(select* from csp_conf_lecturer where conf_id in(select id from csp_conference where conf_no= '" + confNo + "')) l on l.conf_lecturer_id = p.id " +
                         " left join(select* from csp_conf_worker where conf_id in(select id from csp_conference where conf_no= '" + confNo + "') )  w on w.worker_id = p.id " +
                         " where 1=1";
            if (!string.IsNullOrEmpty(p_id))
            {
                sql += " and  p.id = '" + p_id + "' ";
            }
            if (!string.IsNullOrEmpty(p_idNum))
            {
                sql += " and  p.id_number = '" + p_idNum + "' ";
            }
            try
            {
                return DBsqliteHelper.getSqlResult(sql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

    }
}

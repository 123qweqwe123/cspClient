using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SM.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
namespace cspClient.utils
{
    class UploadDataHelper
    {
        //register上传文件
        public static void UploadData(string confNo)
        {
            try {
                String sql = "select distinct(conf_id) from csp_conf_register r where r.ISUPLOAD ='0'  ";
                sql += " and exists( select 1 from csp_conference c where r.conf_id = c.id and c.conf_no='"+ confNo + "'  ) ";
                DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                if (dataTable.Rows.Count==0) {
                    HR_Log.WriteLogLine("没有需要上传的数据csp_conf_register--------------");
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //String sql2 = "select conf_no from csp_conference where id = '" + dataTable.Rows[i]["conf_id"].ToString() + "'";
                    //DataTable dataTable2 = DBsqliteHelper.getDataTable(sql2);
                    //if (dataTable2.Rows.Count > 0)
                    //{
                    //String confn = dataTable2.Rows[0]["conf_no"].ToString();
                    String confn = confNo;
                        List<SqlHelper.CspConfRegister> list = getCspConfRegister(dataTable.Rows[i]["conf_id"].ToString());
                        if (list.Count > 0)
                        {

                            String json = JsonConvert.SerializeObject(list);
                            String zipname = Utils.getMachineUUID() + "-register-" + Environment.TickCount + ".zip";
                            String jsonname = confn + "-register-" + Environment.TickCount + ".json";
                            String zipDirectory = Environment.CurrentDirectory + "\\zip\\" + zipname;
                            String fileDirectory = Environment.CurrentDirectory + "\\jsonfile\\" + jsonname;
                            if (!Directory.Exists(Environment.CurrentDirectory + "\\jsonfile")) { Directory.CreateDirectory(Environment.CurrentDirectory + "\\jsonfile"); }
                            WriteNewFile(fileDirectory, json);
                            ZipHelper.ZipFilesInDirectory(Environment.CurrentDirectory + "\\jsonfile", zipDirectory, "");

                            String Address = XmlConfigHelper.GetValueByKey("Address");
                            String downurl = XmlConfigHelper.GetValueByKey("ftpInfourl");
                            String pathjson = WebDownLoadUtils.HttpGetUploadPath(Address + downurl + "?confNo=" + confn + "&projectId=009");
                            JObject obj = JObject.Parse(pathjson);
                            String path = obj["ftpPath"].ToString();
                            String uploadLoadDataURL = XmlConfigHelper.GetValueByKey("uploadLoadDataURL");
                            String url = Address + uploadLoadDataURL + "?fileName=" + zipname + "&savePath=" + path + "/register";
                            FormItemModel formItemModel = new FormItemModel();
                            formItemModel.FileName = zipname;
                            formItemModel.FileContent = File.OpenRead(zipDirectory);
                            formItemModel.Key = "file";
                            List<FormItemModel> formItems = new List<FormItemModel>();
                            formItems.Add(formItemModel);
                            String uploadResultString = WebUploadUtils.PostForm(url, formItems, null, null, null, 300000);
                            JObject uploadResultobj = JObject.Parse(uploadResultString);
                            String suc = uploadResultobj["suc"].ToString();
                            if ("True".Equals(suc))
                            {
                                UploadResult uploadResult = new UploadResult();
                                uploadResult.projectId = "009";
                                uploadResult.confNo = confn;
                                uploadResult.terminalId = "";
                                uploadResult.ftpPath = path + "/register/";
                                uploadResult.fileType = "register";
                                Filelist[] filelist = new Filelist[1];
                                Filelist filelist0 = new Filelist();
                                filelist0.filename = zipname;
                                filelist0.md5 = "1";
                                filelist[0] = filelist0;
                                uploadResult.fileList = filelist;
                                String uploadResultjson = JsonConvert.SerializeObject(uploadResult);
                                String ResultURL = XmlConfigHelper.GetValueByKey("uploadResultURL");

                                String url6 = Address + ResultURL + "?kName=''&kValue=" + uploadResultjson;
                                String result = WebDownLoadUtils.getHttpPostResponse(url6);

                                List<ResultJson> resultlist = JsonConvert.DeserializeObject<List<ResultJson>>(result);
                                String res = resultlist[0].result;
                                // 1成功；2失败
                                if ("1".Equals(res))
                                {
                                    HR_Log.WriteLogLine("解析成功csp_conf_register--------------");
                                    String updatesql = "update csp_conf_register set ISUPLOAD = '1' where CONF_ID = '" + dataTable.Rows[i]["conf_id"].ToString() + "'";
                                    DBsqliteHelper.ExecuteNonQuery(updatesql);
                                }
                                if ("2".Equals(res))
                                {
                                    HR_Log.WriteLogLine("解析失败csp_conf_register--------------");
                                    //解析失败
                                }
                            }
                            else
                            {
                                HR_Log.WriteLogLine("上传失败csp_conf_register--------------");
                                //上传失败
                            }
                        }
                        else
                        {
                            HR_Log.WriteLogLine("没有需要上传的数据csp_conf_register--------------");
                        }
                        if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\jsonfile"))
                        {
                            Directory.Delete(Environment.CurrentDirectory + "\\jsonfile", true);
                        }
                        //if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\zip"))
                        //{
                        //    Directory.Delete(Environment.CurrentDirectory + "\\zip", true);
                        //}

                    }
                //}
            }
            catch(Exception e){
                HR_Log.WriteLogLine("上传csp_conf_register时异常："+e.Message);
            }
            
        }

        //mainperson上传文件
        public static void UploadMainPersonData(string confNo)
        {
            try {

                //String sql = "select distinct(conf_id) from csp_conf_register r where r.ISUPLOAD = '0' ";
                //sql += " and exists( select 1 from csp_conference c where r.conf_id = c.id and c.conf_no='" + confNo + "'  ) ";
                //DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                //for (int i = 0; i < dataTable.Rows.Count; i++)
                //{
                //    String sql2 = "select conf_no from csp_conference where id = '" + dataTable.Rows[i]["conf_id"].ToString() + "'";
                //    DataTable dataTable2 = DBsqliteHelper.getDataTable(sql2);
                //    if (dataTable2.Rows.Count > 0)
                //    {
                //        String confn = dataTable2.Rows[0]["conf_no"].ToString();
                string sql = " select id from  csp_conference where conf_no = '"+confNo+"'";
                string confId = DBsqliteHelper.getSqlResult(sql);
                        string confn = confNo;

                        List<SqlHelper.CspMainPerson> list = getCspMainPerson(confId);
                        if (list.Count > 0)
                        {
                            String json = JsonConvert.SerializeObject(list);
                            String zipname = Utils.getMachineUUID() + "-person-" + Environment.TickCount + ".zip";
                            String jsonname = confn + "-person-" + Environment.TickCount + ".json";
                            String zipDirectory = Environment.CurrentDirectory + "\\zip\\" + zipname;
                            String fileDirectory = Environment.CurrentDirectory + "\\jsonfile\\" + jsonname;
                            if (!Directory.Exists(Environment.CurrentDirectory + "\\jsonfile")) { Directory.CreateDirectory(Environment.CurrentDirectory + "\\jsonfile"); }
                            WriteNewFile(fileDirectory, json);
                            ZipHelper.ZipFilesInDirectory(Environment.CurrentDirectory + "\\jsonfile", zipDirectory, "");

                            String Address = XmlConfigHelper.GetValueByKey("Address");
                            String downurl = XmlConfigHelper.GetValueByKey("ftpInfourl");
                            String pathjson = WebDownLoadUtils.HttpGetUploadPath(Address + downurl + "?confNo=" + confn + "&projectId=009");
                            JObject obj = JObject.Parse(pathjson);
                            String path = obj["ftpPath"].ToString();
                            String uploadLoadDataURL = XmlConfigHelper.GetValueByKey("uploadLoadDataURL");
                            String url = Address + uploadLoadDataURL + "?fileName=" + zipname + "&savePath=" + path + "/person";
                            FormItemModel formItemModel = new FormItemModel();
                            formItemModel.FileName = zipname;
                            formItemModel.FileContent = File.OpenRead(zipDirectory);
                            formItemModel.Key = "file";
                            List<FormItemModel> formItems = new List<FormItemModel>();
                            formItems.Add(formItemModel);
                            String uploadResultString = WebUploadUtils.PostForm(url, formItems, null, null, null, 300000);
                            JObject uploadResultobj = JObject.Parse(uploadResultString);
                            String suc = uploadResultobj["suc"].ToString();
                            if ("True".Equals(suc))
                            {
                                UploadResult uploadResult = new UploadResult();
                                uploadResult.projectId = "009";
                                uploadResult.confNo = confn;
                                uploadResult.terminalId = "";
                                uploadResult.ftpPath = path + "/person/";
                                uploadResult.fileType = "person";
                                Filelist[] filelist = new Filelist[1];
                                Filelist filelist0 = new Filelist();
                                filelist0.filename = zipname;
                                filelist0.md5 = "1";
                                filelist[0] = filelist0;
                                uploadResult.fileList = filelist;
                                String uploadResultjson = JsonConvert.SerializeObject(uploadResult);
                                String ResultURL = XmlConfigHelper.GetValueByKey("uploadResultURL");

                                String url6 = Address + ResultURL + "?kName=''&kValue=" + uploadResultjson;
                                String result = WebDownLoadUtils.getHttpPostResponse(url6);

                                List<ResultJson> resultlist = JsonConvert.DeserializeObject<List<ResultJson>>(result);
                                String res = resultlist[0].result;
                                // 1成功；2失败
                                if ("1".Equals(res))
                                {
                                    String updatesql = "update csp_Main_Person set ISUPLOAD = '1' where id in (select PERSON_ID from csp_conf_register where CONF_ID  = '" + confId + "')";
                                    DBsqliteHelper.ExecuteNonQuery(updatesql);
                                }
                                if ("2".Equals(res))
                                {
                                    //解析失败
                                    HR_Log.WriteLogLine("解析失败csp_Main_Person--------------");
                                }
                            }
                            else
                            {
                                HR_Log.WriteLogLine("上传失败csp_Main_Person--------------");

                                //上传失败
                            }
                        }
                        else
                        {

                            HR_Log.WriteLogLine("没有需要上传的数据csp_Main_Person---------------");
                        }


                        if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\jsonfile"))
                        {
                            Directory.Delete(Environment.CurrentDirectory + "\\jsonfile", true);
                        }
                        //if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\zip"))
                        //{
                        //    Directory.Delete(Environment.CurrentDirectory + "\\zip", true);
                        //}

                //    }
                //}

            } catch (Exception e) {
                HR_Log.WriteLogLine("上传csp_main_person异常："+e.Message);
            }
            
        }


        public class ResultJson
        {
            public string result { get; set; }
            public string zipfile { get; set; }
            public string filename { get; set; }
        }


        public class UploadResult
        {
            public string projectId { get; set; }
            public string confNo { get; set; }
            public string terminalId { get; set; }
            public string ftpPath { get; set; }
            public string fileType { get; set; }
            public Filelist[] fileList { get; set; }
        }

        public class Filelist
        {
            public string filename { get; set; }
            public string md5 { get; set; }
        }

        public static List<SqlHelper.CspConfRegister> getCspConfRegister(String confid)
        {
            List<SqlHelper.CspConfRegister> list = new List<SqlHelper.CspConfRegister>();
            String sql = "select * from csp_conf_register where conf_id = '" + confid + "'";
            DataTable dataTable = DBsqliteHelper.getDataTable(sql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SqlHelper.CspConfRegister cspConfRegister = new SqlHelper.CspConfRegister();
                cspConfRegister.id = dataTable.Rows[i]["id"].ToString();
                cspConfRegister.personId = dataTable.Rows[i]["person_Id"].ToString();
                cspConfRegister.personType = dataTable.Rows[i]["person_Type"].ToString();
                cspConfRegister.confId = dataTable.Rows[i]["conf_Id"].ToString();
                cspConfRegister.confPlaceId = dataTable.Rows[i]["conf_Place_Id"].ToString();
                cspConfRegister.remark = dataTable.Rows[i]["remark"].ToString();
                cspConfRegister.registerBy = dataTable.Rows[i]["register_By"].ToString();
                cspConfRegister.registerTime = dataTable.Rows[i]["REGISTER_TIME"].ToString().Replace("/", "-");
                cspConfRegister.createTime = dataTable.Rows[i]["CREATE_TIME"].ToString().Replace("/", "-");
                cspConfRegister.createBy = dataTable.Rows[i]["CREATE_BY"].ToString();
                cspConfRegister.updateTime = dataTable.Rows[i]["UPDATE_TIME"].ToString().Replace("/", "-");
                cspConfRegister.updateBy = dataTable.Rows[i]["UPDATE_BY"].ToString();
                cspConfRegister.srcId = dataTable.Rows[i]["SRC_ID"].ToString();
                cspConfRegister.operatorType = dataTable.Rows[i]["OPERATOR_TYPE"].ToString();
                cspConfRegister.dataSource = dataTable.Rows[i]["DATA_SOURCE"].ToString();
                cspConfRegister.isUpload = dataTable.Rows[i]["ISUPLOAD"].ToString();
                cspConfRegister.isScancard = dataTable.Rows[i]["IS_SCANCARD"].ToString();
                list.Add(cspConfRegister);
            }
            return list;
        }


        public static List<SqlHelper.CspMainPerson> getCspMainPerson(String confid)
        {
            List<SqlHelper.CspMainPerson> list = new List<SqlHelper.CspMainPerson>();
            String sql = "SELECT csp_main_person.* ,csp_conf_register.PERSON_TYPE PERSON_TYPE_reall,csp_conf_register.CONF_ID FROM csp_main_person" +
                "  inner join csp_conf_register on  csp_conf_register.conf_id = '" + confid + "' and csp_main_person.id = csp_conf_register.PERSON_ID and csp_main_person.ISUPLOAD != '1' ";
            DataTable dataTable = DBsqliteHelper.getDataTable(sql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SqlHelper.CspMainPerson cspMainPerson = new SqlHelper.CspMainPerson();
                cspMainPerson.id = dataTable.Rows[i]["id"].ToString();
                cspMainPerson.idNumber = dataTable.Rows[i]["id_Number"].ToString();
                cspMainPerson.idType = dataTable.Rows[i]["id_Type"].ToString();
                cspMainPerson.type= dataTable.Rows[i]["PERSON_TYPE_reall"].ToString();
                cspMainPerson.name = dataTable.Rows[i]["name"].ToString();
                cspMainPerson.gender= dataTable.Rows[i]["gender"].ToString();
                cspMainPerson.birthday = dataTable.Rows[i]["birthday"].ToString().Replace("/", "-");
                cspMainPerson.tel = dataTable.Rows[i]["tel"].ToString();
                cspMainPerson.email = dataTable.Rows[i]["email"].ToString();
                cspMainPerson.workplace = dataTable.Rows[i]["workplace"].ToString();
                cspMainPerson.department = dataTable.Rows[i]["department"].ToString();
                cspMainPerson.major = dataTable.Rows[i]["major"].ToString();
                cspMainPerson.degree = dataTable.Rows[i]["degree"].ToString();
                cspMainPerson.duty = dataTable.Rows[i]["duty"].ToString();
                cspMainPerson.province = dataTable.Rows[i]["province"].ToString();
                cspMainPerson.city = dataTable.Rows[i]["city"].ToString();
                cspMainPerson.county = dataTable.Rows[i]["county"].ToString();
                cspMainPerson.address = dataTable.Rows[i]["address"].ToString();
                cspMainPerson.remark = dataTable.Rows[i]["remark"].ToString();
                cspMainPerson.files = dataTable.Rows[i]["files"].ToString();
                cspMainPerson.dataVersion = dataTable.Rows[i]["data_Version"].ToString();
                cspMainPerson.createBy = dataTable.Rows[i]["create_By"].ToString();
                cspMainPerson.createTime = dataTable.Rows[i]["create_Time"].ToString().Replace("/", "-");
                cspMainPerson.updateBy = dataTable.Rows[i]["update_By"].ToString();
                cspMainPerson.updateTime= dataTable.Rows[i]["update_Time"].ToString().Replace("/", "-");
                cspMainPerson.isUpload = dataTable.Rows[i]["isUpload"].ToString();
                cspMainPerson.srcId = dataTable.Rows[i]["src_Id"].ToString();
                cspMainPerson.pcIdNumber = dataTable.Rows[i]["PC_idNumber"].ToString();
                cspMainPerson.pcIdType = dataTable.Rows[i]["PC_idtype"].ToString();
                cspMainPerson.confId = dataTable.Rows[i]["conf_Id"].ToString();
                list.Add(cspMainPerson);
            }
            return list;
        }



        public static void getJson()
        {
            String sql = "select * from csp_conf_register";
            DataTable dataTable = DBsqliteHelper.getDataTable(sql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SqlHelper.CspConfRegister cspConfRegister = new SqlHelper.CspConfRegister();
                cspConfRegister.id = dataTable.Rows[i]["id"].ToString();
                cspConfRegister.personId = dataTable.Rows[i]["person_Id"].ToString();
                cspConfRegister.personType = dataTable.Rows[i]["person_Type"].ToString();
                cspConfRegister.confId = dataTable.Rows[i]["conf_Id"].ToString();
                cspConfRegister.confPlaceId = dataTable.Rows[i]["conf_Place_Id"].ToString();
                cspConfRegister.remark = dataTable.Rows[i]["remark"].ToString();
                cspConfRegister.registerBy = dataTable.Rows[i]["register_By"].ToString();
                cspConfRegister.registerTime = dataTable.Rows[i]["REGISTER_TIME"].ToString();
                cspConfRegister.createTime = dataTable.Rows[i]["CREATE_TIME"].ToString();
                cspConfRegister.createBy = dataTable.Rows[i]["CREATE_BY"].ToString();
                cspConfRegister.updateTime = dataTable.Rows[i]["UPDATE_TIME"].ToString();
                cspConfRegister.updateBy = dataTable.Rows[i]["UPDATE_BY"].ToString();
                cspConfRegister.srcId = dataTable.Rows[i]["SRC_ID"].ToString();
                cspConfRegister.operatorType = dataTable.Rows[i]["OPERATOR_TYPE"].ToString();
                cspConfRegister.dataSource = dataTable.Rows[i]["DATA_SOURCE"].ToString();
                cspConfRegister.isUpload = dataTable.Rows[i]["ISUPLOAD"].ToString();
                cspConfRegister.isScancard = dataTable.Rows[i]["IS_SCANCARD"].ToString();
                String json = JsonConvert.SerializeObject(cspConfRegister);


                String zipname = Utils.getMachineUUID() + "_register" + Environment.TickCount + ".zip";
                String jsonname = cspConfRegister.confId + "_register" + Environment.TickCount + ".json";
                String zipDirectory = Environment.CurrentDirectory + "\\zip\\" + zipname;
                String fileDirectory = Environment.CurrentDirectory + "\\jsonfile\\" + jsonname;
                if (!Directory.Exists(Environment.CurrentDirectory + "\\jsonfile")) { Directory.CreateDirectory(Environment.CurrentDirectory + "\\jsonfile"); }
                WriteNewFile(fileDirectory, json);
                ZipHelper.ZipFilesInDirectory(Environment.CurrentDirectory + "\\jsonfile", zipDirectory, "");
                String downurl = XmlConfigHelper.GetValueByKey("uploadLoadDataURL");
                String Address = XmlConfigHelper.GetValueByKey("Address");

                String json2 = WebDownLoadUtils.HttpGetUploadPath(Address+downurl + "?confNo=" + cspConfRegister.confId + "&projectId=009");
                JObject obj = JObject.Parse(json2);
                String path = obj["ftpPath"].ToString();
                String url = downurl + "?fileName =" + zipname + "&savePath=" + downurl + "/register";
                FormItemModel formItemModel = new FormItemModel();
                FileStream fileStream = File.Create(json2);
                //formItemModel.Key
                formItemModel.FileName = "";
                formItemModel.FileContent = fileStream;

                List<FormItemModel> formItems = new List<FormItemModel>();
                formItems.Add(formItemModel);
                WebUploadUtils.PostForm(" url", formItems, null, null, null, 300000);

            }



        }
        /// <summary>
                /// 生成文件
                /// </summary>
                /// <param name="FilePath"></param>
                /// <param name="Message"></param>
        public static void WriteNewFile(string FileName, string Message)
        {

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            using (FileStream fileStream = File.Create(FileName))
            {
                byte[] bytes = new UTF8Encoding(true).GetBytes(Message);
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

    }
}

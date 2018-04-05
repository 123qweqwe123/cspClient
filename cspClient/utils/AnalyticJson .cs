using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SM.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
//using static cspClient.utils.SqlHelper;

namespace cspClient.utils
{
    class AnalyticJson
    {
        //下载数据字典方法
        public static void downLoadDicData()
        {

            try
            {
                String Address = XmlConfigHelper.GetValueByKey("Address");
                String downurl = XmlConfigHelper.GetValueByKey("DownLoadDataURL");
                downurl = Address + downurl + "?dictType=0&projectId=009";
                String downLoadPath = AnalyticJson.DownLoadConfdata(downurl, "0");
                if (File.Exists(downLoadPath + @"\SYS_PARATYPE.json"))
                {
                    String json = File.ReadAllText(downLoadPath + @"\SYS_PARATYPE.json", Encoding.UTF8);
                    List<SqlHelper.SysParatype> List = JsonConvert.DeserializeObject<List<SqlHelper.SysParatype>>(json);
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.SysParatype obj in List)
                    {
                        String asql = SqlHelper.sysParatype(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQuery("truncate table SYS_PARATYPE");
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);

                }
                else
                {
                    throw new Exception("SYS_PARATYPE.json 操作失败");
                }

                if (File.Exists(downLoadPath + @"\SYS_PARAMETER.json"))
                {
                    String sysparatypejson = File.ReadAllText(downLoadPath + @"\SYS_PARAMETER.json", Encoding.UTF8);
                    List<SqlHelper.SysParameter> sysParameterjson = JsonConvert.DeserializeObject<List<SqlHelper.SysParameter>>(sysparatypejson);
                    List<String> sysParameterjsonsqlList = new List<String>();
                    foreach (SqlHelper.SysParameter obj in sysParameterjson)
                    {
                        String asql = SqlHelper.sysParameter(obj);
                        sysParameterjsonsqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQuery("truncate table SYS_PARAMETER");
                    DBsqliteHelper.ExecuteNonQueryTrans(sysParameterjsonsqlList);
                }
                else
                {
                    throw new Exception("SYS_PARAMETER.json 操作失败");
                }
                if (System.IO.Directory.Exists(downLoadPath))
                {
                    Directory.Delete(downLoadPath, true);
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("下载数据字典错误：" + e.Message);
            }
        }
        //下载会议列表并入库
        public static List<SqlHelper.ConfList> downLoadConfListData()
        {           
            List<SqlHelper.ConfList> confList = new List<SqlHelper.ConfList>();
            String downLoadPath = "";
            try
            {
                String Address = XmlConfigHelper.GetValueByKey("Address");
                String downurl = XmlConfigHelper.GetValueByKey("DownLoadDataURL");
                downurl = Address + downurl + "?dictType=1&projectId=009";
                downLoadPath = DownLoadConfdata(downurl, "1");
                String json = File.ReadAllText(downLoadPath + @"\cspConference.json", Encoding.UTF8);
                List<String> sqlList = new List<String>();
                JObject obj = JObject.Parse(json);
                JArray jarray = (JArray)obj["confs"];
                if (jarray != null && jarray.Count > 0)
                {
                    foreach (var item in jarray)
                    {
                        String sql = " REPLACE INTO CSP_CONFERENCELIST(" +
                                     "id,CONF_NO,CONF_TYPE,CONF_FORM,CONF_HOST,CONF_NAME," +
                                     "CONF_ORGANISER,CONF_CO_ORGANISER,CONF_PIC,CONF_TOPIC,CONF_DESCRIPTION," +
                                     "START_TIME,END_TIME,CONF_PLACE,REMARK,DATA_VERSION,CREATE_BY,CREATE_TIME," +
                                     "UPDATE_BY,UPDATE_TIME,status,serverType1Version,localType1Version," +
                                     "serverType2Version,localType2Version,serverType3Version,localType3Version," +
                                     "serverType4Version,localType4Version";
                        String values = "";
                        SqlHelper.ConfList confListdata = new SqlHelper.ConfList();
                        if ( string.IsNullOrEmpty(item["conf"].ToString()) ) { continue; }
                        values += "'" + item["conf"]["id"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confNo"].ToString() + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["confType"].ToString()) ? "null ," : "'" + item["conf"]["confType"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confForm"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confHost"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confName"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confOrganiser"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confCoOrganiser"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confPic"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confTopic"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["confDescription"].ToString() + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["startTime"].ToString()) ? "null ," : "'" + DateTimeHelper.ConvertStringToDateTime(item["conf"]["startTime"].ToString()) + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["endTime"].ToString()) ? " null ," : "'" + DateTimeHelper.ConvertStringToDateTime(item["conf"]["endTime"].ToString()) + "'" + ",";
                        values += "'" + item["conf"]["confPlace"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["remark"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["dataVersion"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["createBy"].ToString() + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["createTime"].ToString()) ? " null ," : "'" + DateTimeHelper.ConvertStringToDateTime(item["conf"]["createTime"].ToString()) + "'" + ",";
                        values += "'" + item["conf"]["updateBy"].ToString() + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["updateTime"].ToString()) ? "' '," : "'" + DateTimeHelper.ConvertStringToDateTime(item["conf"]["updateTime"].ToString()) + "'" + ",";
                        values += String.IsNullOrEmpty(item["conf"]["status"].ToString()) ? " null ," : "'" + item["conf"]["status"].ToString() + "'" + ",";
                        values += "'" + item["conf"]["dataVersion"].ToString() + "'" + ",";
                        values += "'" + searchConfListVersion(item["conf"]["id"].ToString()) + "'" + ",";

                        try {
                            // 下载会议相关打印模板
                            DownLoadPrintFile(item["conf"]["confNo"].ToString());

                        } catch (Exception e) {
                            Log.WriteLogLine("会议打印模板下载异常:"+e.Message);
                        }


                        JArray versions = (JArray)item["versions"];
                        List<String> confLists = new List<String>();
                        foreach (var item2 in versions)
                        {
                            if ("2".Equals(item2["dictType"].ToString()))
                            {
                                confListdata.serverType2Version = item2["dictVersion"].ToString();
                                confListdata.localType2Version = searchDifferectTypeVersion(item2["lccCode"].ToString(), item2["dictType"].ToString());

                            }
                            if ("3".Equals(item2["dictType"].ToString()))
                            {
                                confListdata.serverType3Version = item2["dictVersion"].ToString();
                                confListdata.localType3Version = searchDifferectTypeVersion(item2["lccCode"].ToString(), item2["dictType"].ToString());

                            }
                            if ("4".Equals(item2["dictType"].ToString()))
                            {
                                confListdata.serverType4Version = item2["dictType"].ToString();
                                confListdata.localType4Version = searchDifferectTypeVersion(item2["lccCode"].ToString(), item2["dictType"].ToString());
                            }
                        }
                        values += "'" + confListdata.serverType2Version + "'" + ",";
                        values += "'" + confListdata.localType2Version + "'" + ",";
                        values += "'" + confListdata.serverType3Version + "'" + ",";
                        values += "'" + confListdata.localType3Version + "'" + ",";
                        values += "'" + confListdata.serverType4Version + "'" + ",";
                        values += "'" + confListdata.localType4Version + "'" + "";
                        sql += ")values(" + values + ")";
                        sqlList.Add(sql);
                    }
                    DBsqliteHelper.ExecuteNonQuery("truncate table csp_conferencelist");
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("下载解析数据时出现异常:" + e.ToString());
            }
            if (System.IO.Directory.Exists(downLoadPath))
            {
                Directory.Delete(downLoadPath, true);
            }
            return confList;
        }

        //从数据库查询,返回展示的list

        public static List<SqlHelper.ConfList> getConfDatafList(String p_loginName)
        {

            downLoadConfListData();
            List<SqlHelper.ConfList> confList = new List<SqlHelper.ConfList>();

            //DataTable datatable =  DBsqliteHelper.getDataTable("select * from CSP_CONFERENCELIST");
            String sql_confList = "";
            if ("admin".Equals(p_loginName, StringComparison.CurrentCultureIgnoreCase))
            {
                sql_confList = "select * from CSP_CONFERENCELIST ";
            }
            else
            {
                sql_confList = "select * from CSP_CONFERENCELIST l where " +
                "exists( select 1 from csp_conf_worker w ,sys_account a where a.user_id= w.worker_id and a.login_name='" + p_loginName + "' and l.id=w.conf_id)";
            }
            DataTable datatable = DBsqliteHelper.getDataTable(sql_confList);
            try
            {
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
                    //confListdata.localType1Version = datatable.Rows[i]["localType1Version"].ToString();
                    confListdata.serverType2Version = datatable.Rows[i]["serverType2Version"].ToString();
                    confListdata.localType2Version = datatable.Rows[i]["localType2Version"].ToString();
                    confListdata.serverType3Version = datatable.Rows[i]["serverType3Version"].ToString();
                    confListdata.localType3Version = datatable.Rows[i]["localType3Version"].ToString();
                    confListdata.serverType4Version = datatable.Rows[i]["serverType4Version"].ToString();
                    confListdata.localType4Version = datatable.Rows[i]["localType4Version"].ToString();

                    List<String> confLists = new List<String>();
                    //本地没有版本号
                    if (
                        String.IsNullOrEmpty(confListdata.localType2Version) || String.IsNullOrEmpty(confListdata.localType3Version) || String.IsNullOrEmpty(confListdata.localType4Version)
                        )
                    {
                        confListdata.dataState = "2";
                    }
                    //版本号相同
                    else if (
                        !String.IsNullOrEmpty(confListdata.localType2Version) && !String.IsNullOrEmpty(confListdata.localType3Version) && !String.IsNullOrEmpty(confListdata.localType4Version)
                        &&
                        confListdata.serverType2Version.Equals(confListdata.localType2Version)
                        && confListdata.serverType3Version.Equals(confListdata.localType3Version)
                        && confListdata.serverType4Version.Equals(confListdata.localType4Version)
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

                    //本地没有版本号
                    if (String.IsNullOrEmpty(confListdata.localType2Version) || String.IsNullOrEmpty(confListdata.localType3Version)//  || String.IsNullOrEmpty(confListdata.localType4Version)
                        )
                    {
                        confListdata.dataState = "2";
                    }
                    //版本号相同
                    else if (
                        //!String.IsNullOrEmpty(confListdata.localType2Version) 
                        //&&!String.IsNullOrEmpty(confListdata.localType3Version) 
                        //&&! String.IsNullOrEmpty(confListdata.localType4Version) &&
                        confListdata.serverType2Version.Equals(confListdata.localType2Version)
                        && confListdata.serverType3Version.Equals(confListdata.localType3Version)
                        //&& confListdata.serverType4Version.Equals(confListdata.localType4Version)
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
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("查询会议列表出错：" + e.Message);
            }
            return confList;
        }

        //点击 下载或者更新，进入的方法
        public static void downLoadData(SqlHelper.ConfList confList)
        {
            try
            {
                if (confList.confList.Count != 0)
                {
                    for (int i = 0; i < confList.confList.Count; i++)
                    {
                        String Address = XmlConfigHelper.GetValueByKey("Address");

                        String downurl = XmlConfigHelper.GetValueByKey("DownLoadDataURL");
                        String url = "";
                        url = Address + downurl + "?dictType=" + confList.confList[i] + "&projectId=009&confNo=" + confList.confNo;
                        HR_Log.WriteLogLine("url地址:=======" + url);
                        String downLoadPath = DownLoadConfdata(url, i.ToString());
                        var files = Directory.GetFiles(downLoadPath, "*.json");
                        foreach (var file in files)
                        {
                            HR_Log.WriteLogLine("解析文件:---------》》》" + Path.GetFileNameWithoutExtension(file));
                            setdatatodatabase(file, confList);
                        }
                        if ("2".Equals(confList.confList[i]))
                        {

                            String sql = "replace sys_data_issue_version(DICT_VERSION, DICT_TYPE, LCC_CODE) values('" + confList.serverType2Version + "' , '2' ,'" + confList.confNo + "')";
                            DBsqliteHelper.ExecuteNonQuery(sql);
                        }
                        if ("3".Equals(confList.confList[i]))
                        {
                            String sql = "replace sys_data_issue_version(DICT_VERSION, DICT_TYPE, LCC_CODE) values('" + confList.serverType3Version + "' , '3' ,'" + confList.confNo + "')";
                            DBsqliteHelper.ExecuteNonQuery(sql);
                        }
                        if ("4".Equals(confList.confList[i]))
                        {
                            String sql = "replace sys_data_issue_version(DICT_VERSION, DICT_TYPE, LCC_CODE) values('" + confList.serverType4Version + "' , '4' ,'" + confList.confNo + "')";
                            DBsqliteHelper.ExecuteNonQuery(sql);
                        }
                        if (System.IO.Directory.Exists(downLoadPath))
                        {
                            Directory.Delete(downLoadPath, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("下载数据时出现异常:" + e.ToString());
            }
        }
        // 执行下载操作并解压文件
        public static String DownLoadConfdata(String url, String type)
        {
            String downLoadrootPath = "";
            String downLoadPath = "";
            String unzipPath = "";
            DirectoryInfo unzipDic = null;
            try
            {
                String timeString = DateTime.Now.ToFileTime().ToString();
                downLoadrootPath = Environment.CurrentDirectory + "\\sysdata\\";
                downLoadPath = Environment.CurrentDirectory + "\\sysdata\\" + timeString;
                Directory.CreateDirectory(downLoadPath);
                unzipPath = downLoadPath + "\\unzip";
                unzipDic = Directory.CreateDirectory(unzipPath);
                String zipName = WebDownLoadUtils.HttpGetDownLoad(url, "machineNo=", unzipPath + "\\" + timeString + ".zip");// POST方式下载压缩包
                HR_Log.WriteLogLine("下载压缩包名称:" + zipName);
                if ((!zipName.Equals(String.Empty)) && zipName.Length > 0)
                {
                    bool b = ZipHelper.UnZip(unzipPath + "\\" + timeString + ".zip", downLoadrootPath); //解压下载的压缩包
                    if (b) { HR_Log.WriteLogLine("压缩包解压成功"); }
                }
            }
            catch (Exception exp)
            {
                HR_Log.WriteLogLine("下载数据出现异常:" + exp.ToString());

            }
            return downLoadrootPath;
        }

        public static void DownLoadPrintFile(String p_confNo) {
            String tmpPath = Environment.CurrentDirectory + "\\tmpDir";
            if (!Directory.Exists(tmpPath)) { Directory.CreateDirectory(tmpPath); }
            String ipAddr = XmlConfigHelper.GetValueByKey("Address");
            String versionUrl = XmlConfigHelper.GetValueByKey("printFileVersion");

            string checkUrl = ipAddr + versionUrl + "?dictType=5&projectId=009&confNo=" + p_confNo.Trim();
            String res = WebDownLoadUtils.HttpGetUploadPath(checkUrl);
            if ( "-1".Equals(res) || "-2".Equals(res) ){ // 服务端无数据
                return;
            }
            // 数据版本
            string dataVersion = string.IsNullOrEmpty(res) ? "0" : res;
            string localVersionSQl = " select min(file_version ) from csp_conf_printfile where conf_no='"+p_confNo+"' ";
            string localVersion = DBsqliteHelper.getSqlResult(localVersionSQl).Trim();
            if ( (!string.IsNullOrEmpty(localVersion)) && ( string.Compare(dataVersion , localVersion) == 0 ) ) {
                // 版本一致程序无需下载即返回
                return;
            }

            String downurl = XmlConfigHelper.GetValueByKey("printFile");
            // 拼接打印模版下载地址
            String url = ipAddr + downurl + "?dictType=5&projectId=009&confNo="+ p_confNo.Trim();
            String downPath = Environment.CurrentDirectory + "\\tempDown";
            if (!Directory.Exists(downPath)){ Directory.CreateDirectory(downPath); }
            String unzipPath = downPath + "\\tempUnzip\\";
            if (!Directory.Exists(unzipPath)) { Directory.CreateDirectory(downPath); }
            String timeString = DateTime.Now.ToFileTime().ToString();
            // 下载模板文件
            String downZipName = WebDownLoadUtils.HttpGetDownLoad(url, "machineNo=", downPath + "\\" + timeString + ".zip");

            if (string.IsNullOrEmpty(downZipName))
            {
                Log.WriteLogLine("下载模版文件失败或异常:" +p_confNo);
                    return ;
            }
            else {
                Log.WriteLogLine("下载模版文件完成：" + unzipPath + "\\" + timeString + ".zip");
                if (!ZipHelper.UnZip(downPath + "\\" + timeString + ".zip", unzipPath))
                {
                    HR_Log.WriteLogLine("下载会议(会议编号：" + p_confNo + ")模版压缩包解压异常");
                }
                else {
                    String jsonStr = File.ReadAllText(unzipPath + @"\IdentityData.json", Encoding.UTF8);
                    if (string.IsNullOrEmpty(jsonStr) || "null".Equals(jsonStr))
                    {
                        Log.WriteLogLine("下载模版压缩包解压文件内容为空：" + p_confNo);
                        return;
                    }
                    else {

                        JObject jsonObj = JObject.Parse(jsonStr);
                        string confId = jsonObj["conferenceId"].ToString();
                        string confNo = jsonObj["conferenceNo"].ToString();
                        string confName = jsonObj["conferenceName"].ToString();

                        // 打印预览模版文件 fileClass : 1
                        int count = jsonObj["bottomFile"].Count();
                        for ( int i = 0; i < count; i++ ) {

                            string fileId = jsonObj["bottomFile"][i]["id"].ToString();
                            string dataMap = jsonObj["bottomFile"][i]["dataMap"].ToString();
                            string fileType = jsonObj["bottomFile"][i]["fileType"].ToString();
                            string templateFileId = jsonObj["bottomFile"][i]["templateFileId"].ToString();
                            byte[] fileBytes = Convert.FromBase64String(jsonObj["bottomFile"][i]["fileData"].ToString());
                            string pdfFile = tmpPath + "\\" + confNo + "_1_" + fileType + ".pdf";
                            if (File.Exists(pdfFile)) { File.Delete(pdfFile); }// 文件存在则首先删除
                            try {
                                File.WriteAllBytes(pdfFile, fileBytes); // bottomFile 预览文件写入本地硬盘
                                String sql = " Replace into csp_conf_printfile( conf_id,conf_no,conf_name ,fileclass,file_id,file_type," +
                                    "   file_templatefield,file_dataMap,file_localpath,file_version ,create_time,update_time ) " +
                                    " values(  '" + confId + "','" + confNo + "','" + confName + "','1', '" + fileId + "','" + fileType + "'" +
                                    ",'" + templateFileId + "','" + dataMap + "','" + pdfFile.Replace("\\","\\\\") + "',' "+ dataVersion + " '," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                DBsqliteHelper.ExecuteNonQuery(sql);// 相关数据信息记录入库
                            } catch (Exception e) {
                                Log.WriteLogLine("pdf文件写入、数据入库异常："+e.Message);
                            }
                        }

                        // 打印使用布局文件 fileClass : 2
                        int count2 = jsonObj["layoutFile"].Count();
                        for ( int i = 0; i < count2; i++ ) {

                            string fileId = jsonObj["layoutFile"][i]["id"].ToString();
                            string dataMap = jsonObj["layoutFile"][i]["dataMap"].ToString();
                            string fileType = jsonObj["layoutFile"][i]["fileType"].ToString();
                            string templateFileId = jsonObj["layoutFile"][i]["templateFileId"].ToString();
                            byte[] fileBytes = Convert.FromBase64String(jsonObj["layoutFile"][i]["fileData"].ToString());
                            string pdfFile = tmpPath + "\\" + confNo + "_2_" + fileType + ".pdf";
                            if (File.Exists(pdfFile)) { File.Delete(pdfFile); }// 文件存在则首先删除
                            try
                            {
                                File.WriteAllBytes(pdfFile, fileBytes); // bottomFile 预览文件写入本地硬盘
                                String sql = " Replace into csp_conf_printfile( conf_id,conf_no,conf_name ,fileclass,file_id,file_type," +
                                    "   file_templatefield,file_dataMap,file_localpath,file_version ,create_time,update_time ) " +
                                    " values(  '" + confId + "','" + confNo + "','" + confName + "','2', '" + fileId + "','" + fileType + "'" +
                                    ",'" + templateFileId + "','" + dataMap + "','" + pdfFile.Replace("\\","\\\\") + "',' "+ dataVersion + " '," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                DBsqliteHelper.ExecuteNonQuery(sql);// 相关数据信息记录入库
                            }
                            catch (Exception e)
                            {
                                Log.WriteLogLine("pdf文件写入、数据入库异常：" + e.Message);
                            }
                        }
                    }
                }
            }

        }


        public static String searchDifferectTypeVersion(String confnom, String type)
        {
            String result = "";
            try
            {
                String sql = "select * from sys_data_issue_version where LCC_CODE = '" + confnom + "' and  DICT_TYPE= '" + type + "'";
                DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    result = dataTable.Rows[0]["DICT_VERSION"].ToString();
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("查询本地数据包版本出错：" + e.Message);
            }
            return result;
        }
        public static String searchConfListVersion(String id)
        {
            String result = "";
            try
            {
                String sql = "select * from csp_conferencelist where id = '" + id + "'";
                DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    result = dataTable.Rows[0]["DATA_VERSION"].ToString();
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("查询会议版本异常：" + e.Message);
            }
            return result;
        }




        public static void setdatatodatabase(String filename, SqlHelper.ConfList confList)
        {
            try
            {
                if ("CSP_CONFERENCE".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    JObject obj = JObject.Parse(json);
                    String sql = SqlHelper.cspConference(obj);
                    DBsqliteHelper.ExecuteNonQuery(sql);

                }
                else if ("CSP_CONF_CHECKIN".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfCheckin> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfCheckin>>(json);
                    if (List.Count > 0)
                    {
                        DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_RECORD where conf_Id = '" + List[0].confId + "'");
                    }
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfCheckin obj in List)
                    {
                        String asql = SqlHelper.cspConfCheckin(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);

                }
                else if ("CSP_CONF_CHECKIN_PERSON".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfCheckinPerson> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfCheckinPerson>>(json);
                    DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_PERSON where CHECKIN_ID in (select id from CSP_CONF_CHECKIN where CONF_ID  = '" + confList.id + "')");
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfCheckinPerson obj in List)
                    {
                        String sql = "Select * from CSP_CONF_CHECKIN where id= '" + obj.checkinId + "'";
                        DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                        //if (dataTable.Rows.Count > 0)
                        //{
                        //    if (List.Count > 0)
                        //    {
                        //        DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_PERSON where checkin_id = '" + dataTable.Rows[0]["id"] + "'");
                        //    }
                        //}
                        String asql = SqlHelper.cspConfCheckinPerson(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                    HR_Log.WriteLogLine("CSP_CONF_CHECKIN_PERSON 插入数据库后删除MainPerson 和Account");
                    deleteMainPersonData();
                    deleteAccountData();
                }
                else if ("CSP_CONF_CHECKIN_RECORD".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfCheckinRecord> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfCheckinRecord>>(json);
                    DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_RECORD where CHECKIN_ID in (select id from CSP_CONF_CHECKIN where CONF_ID  = '" + confList.id + "')");
                    List<String> sqlList = new List<String>();
                    foreach ( SqlHelper.CspConfCheckinRecord obj in List)
                    {
                        String sql = "Select * from CSP_CONF_CHECKIN where id= '" + obj.checkinId + "'";
                        DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                        //if (dataTable.Rows.Count > 0)
                        //{
                        //    if (List.Count > 0)
                        //    {
                        //        DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_RECORD where checkin_id = '" + dataTable.Rows[0]["id"] + "'");
                        //    }
                        //}
                        String asql = SqlHelper.cspConfCheckinRecord(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
                else if ("CSP_CONF_CHECKIN_WORKER".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfCheckinWorker> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfCheckinWorker>>(json);
                    DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_WORKER where CHECKIN_ID in (select id from CSP_CONF_CHECKIN where CONF_ID  = '" + confList.id + "')");
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfCheckinWorker obj in List)
                    {
                        String sql = "Select * from CSP_CONF_CHECKIN where id= '" + obj.checkinId + "'";
                        DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                        //if (dataTable.Rows.Count > 0)
                        //{
                        //    if (List.Count > 0)
                        //    {
                        //        DBsqliteHelper.ExecuteNonQuery("delete FROM CSP_CONF_CHECKIN_WORKER where checkin_id = '" + dataTable.Rows[0]["id"] + "'");
                        //    }
                        //}
                        String asql = SqlHelper.cspConfCheckinWorker(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                    HR_Log.WriteLogLine("CSP_CONF_CHECKIN_WORKER 插入数据库后删除MainPerson 和Account");
                    deleteMainPersonData();
                    deleteAccountData();
                }
                else if ("CSP_CONF_LECTURER".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConFlecturer> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConFlecturer>>(json);
                    if (List.Count > 0)
                    {
                        DBsqliteHelper.ExecuteNonQuery("delete   FROM CSP_CONF_LECTURER where CONF_ID = '" + List[0].confId + "'");
                    }
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConFlecturer obj in List)
                    {
                        String asql = SqlHelper.cspConfLecturer(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                    HR_Log.WriteLogLine("CSP_CONF_LECTURER 插入数据库后删除MainPerson 和Account");
                    deleteMainPersonData();
                    deleteAccountData();
                }
                else if ("CSP_CONF_PLACE".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfPlace> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfPlace>>(json);
                    if (List.Count > 0)
                    {
                        DBsqliteHelper.ExecuteNonQuery("delete   FROM CSP_CONF_PLACE where CONF_ID = '" + List[0].confId + "'");
                    }
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfPlace obj in List)
                    {
                        String asql = SqlHelper.cspConfPlace(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
                else if ("CSP_CONF_REGISTER".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfRegister> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfRegister>>(json);

                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfRegister obj in List)
                    {
                        String sql = "Select * from CSP_CONF_REGISTER where SRC_ID= '" + obj.srcId + "'";
                        DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                        if (dataTable.Rows.Count == 0)
                        {
                            String asql = SqlHelper.cspConfrRegister(obj);
                            sqlList.Add(asql);
                        }
                        //if (dataTable.Rows.Count > 0)
                        //{
                        //    if (dataTable.Rows[0]["isupload"].ToString().Equals("1"))
                        //    {
                        //        String asql = SqlHelper.cspConfrRegister(obj);
                        //        sqlList.Add(asql);
                        //    }

                        //}

                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
                else if ("CSP_CONF_VISITOR".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfVisitor> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfVisitor>>(json);
                    if (List.Count > 0)
                    {
                        DBsqliteHelper.ExecuteNonQuery("delete   FROM CSP_CONF_VISITOR where CONF_ID = '" + List[0].confId + "'");
                    }
                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspConfVisitor obj in List)
                    {
                        String asql = SqlHelper.cspConfVisitor(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                    HR_Log.WriteLogLine("解析CSP_CONF_VISITOR 插入数据库后删除MainPerson 和Account");
                    deleteMainPersonData();
                    deleteAccountData();
                }
                else if ("CSP_CONF_WORKER".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspConfWorker> List = JsonConvert.DeserializeObject<List<SqlHelper.CspConfWorker>>(json);

                    List<String> sqlList = new List<String>();
                    if (List.Count > 0)
                    {
                        DBsqliteHelper.ExecuteNonQuery("delete   FROM CSP_CONF_WORKER where CONF_ID = '" + List[0].confId + "'");
                    }
                    foreach (SqlHelper.CspConfWorker obj in List)
                    {
                        String asql = SqlHelper.cspConfWorker(obj);
                        sqlList.Add(asql);
                    }

                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                    HR_Log.WriteLogLine("解析CSP_CONF_WORKER 插入数据库后删除MainPerson 和Account");
                    deleteMainPersonData();
                    deleteAccountData();
                }
                else if ("CSP_MAIN_PERSON".Equals(Path.GetFileNameWithoutExtension(filename)))
                {
                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.CspMainPerson> List = JsonConvert.DeserializeObject<List<SqlHelper.CspMainPerson>>(json);

                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.CspMainPerson obj in List)
                    {
                        String sql = "Select * from csp_main_person where id= '" + obj.id + "'";
                        DataTable dataTable = DBsqliteHelper.getDataTable(sql);
                        if (dataTable.Rows.Count > 0)
                        {
                            if (!"0".Equals(dataTable.Rows[0]["isUpload"].ToString()))
                            {
                                String asql = SqlHelper.cspMainPerson(obj);
                                sqlList.Add(asql); 
                            }
                        }
                        else {
                            String asql = SqlHelper.cspMainPerson(obj);
                            sqlList.Add(asql);
                        }
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
                else if ("SYS_ACCOUNT".Equals(Path.GetFileNameWithoutExtension(filename)))
                {

                    String json = File.ReadAllText(filename, Encoding.UTF8);
                    List<SqlHelper.SysAccount> List = JsonConvert.DeserializeObject<List<SqlHelper.SysAccount>>(json);

                    List<String> sqlList = new List<String>();
                    foreach (SqlHelper.SysAccount obj in List)
                    {
                        String asql = SqlHelper.sysAccount(obj);
                        sqlList.Add(asql);
                    }
                    DBsqliteHelper.ExecuteNonQueryTrans(sqlList);
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("解析json 插入数据库时异常：" + e.Message);
            }
        }

        public static void deleteMainPersonData()
        {
            String sql = "delete from csp_main_person where id not in(select distinct person_id from main_persion_unionid) and isupload != '0'";
            DBsqliteHelper.ExecuteNonQuery(sql);
        }

        public static void deleteAccountData()
        {
            String sql = "delete from sys_account where user_id not in(select distinct id from csp_main_person)";
            DBsqliteHelper.ExecuteNonQuery(sql);
        }
    }

    




}

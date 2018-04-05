using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static cspClient.utils.SqlHelper;

namespace cspClient.utils
{
    public class AmqpDataHandler
    {

        public static Dictionary<string, string> GetPersonInfo( String person_id ) {

            string sql = " select * from csp_main_person where id='"+person_id+"' ";
            DataTable dt = DBsqliteHelper.getDataTable(sql);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (dt.Rows.Count == 1)
            {
                DataRow row =  dt.Rows[0];
                dic.Add("id", row["ID"].ToString());
                dic.Add("idNumber", row["ID_NUMBER"].ToString());
                dic.Add("idType", row["ID_TYPE"].ToString());
                dic.Add("type", row["TYPE"].ToString());
                dic.Add("name", row["NAME"].ToString());
                dic.Add("gender", row["GENDER"].ToString());
                dic.Add("tel", row["TEL"].ToString());
                dic.Add("email", row["EMAIL"].ToString());
                dic.Add("workplace", row["WORKPLACE"].ToString());
                dic.Add("department", row["DEPARTMENT"].ToString());
                dic.Add("major", row["MAJOR"].ToString());
                dic.Add("degree", row["DEGREE"].ToString());
                dic.Add("duty", row["DUTY"].ToString());
                dic.Add("province", row["PROVINCE"].ToString());
                dic.Add("city", row["CITY"].ToString());
                dic.Add("county", row["COUNTY"].ToString());
                dic.Add("address", row["ADDRESS"].ToString());
                dic.Add("remark", row["REMARK"].ToString());
                dic.Add("files", row["FILES"].ToString());
                dic.Add("dataVersion", row["DATA_VERSION"].ToString());
                dic.Add("createBy", row["CREATE_BY"].ToString());
                dic.Add("updateBy", row["UPDATE_BY"].ToString());
                dic.Add("isUpload", row["ISUPLOAD"].ToString());
                dic.Add("srcId", row["SRC_ID"].ToString());
                dic.Add("pcIdNumber", row["PC_IDNUMBER"].ToString());
                dic.Add("pcIdType", row["PC_IDTYPE"].ToString());

                try {
                    if (row["BIRTHDAY"] != null)
                    {
                        dic.Add("birthday", ConvertStringToDateTime(((((DateTime)row["BIRTHDAY"]).ToUniversalTime().Ticks - 621355968000000000) / 10000) + ""));
                    }
                    else {
                        dic.Add("birthday","");
                    }
                    if (row["CREATE_TIME"] != null)
                    {
                        dic.Add("createTime", ConvertStringToDateTime(((((DateTime)row["CREATE_TIME"]).ToUniversalTime().Ticks - 621355968000000000) / 10000) + ""));
                    }
                    else {
                        dic.Add("createTime", "");
                    }
                    if (row["UPDATE_TIME"] != null)
                    {
                        dic.Add("updateTime", ConvertStringToDateTime(((((DateTime)row["UPDATE_TIME"]).ToUniversalTime().Ticks - 621355968000000000) / 10000) + ""));
                    }
                    else {
                        dic.Add("updateTime", "");
                    }                    
                }
                catch (Exception e) {

                }

                //dic.Add("confId", row["id"].ToString());
            }
            return dic;
        }

        public static Dictionary<string, string> GetRegisterInfo(String p_id) {

            string sql = " select * from csp_conf_register where id='"+p_id+"'";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try {
                DataTable dt = DBsqliteHelper.getDataTable(sql);
                if ( dt.Rows.Count == 1 ) {
                    DataRow dr = dt.Rows[0];

                    dic.Add("id", dr["ID"].ToString());
                    dic.Add("personId", dr["PERSON_ID"].ToString());
                    dic.Add("personType", dr["PERSON_TYPE"].ToString());
                    dic.Add("confId", dr["CONF_ID"].ToString());
                    //dic.Add("confPlaceId", dr["CONF_PLACE_ID"].ToString());
                    dic.Add("remark", dr["REMARK"].ToString());
                    dic.Add("registerBy", dr["REGISTER_BY"].ToString());
                    //dic.Add("registerTime", dr["REGISTER_TIME"].ToString());
                    //dic.Add("createTime", dr["CREATE_TIME"].ToString());
                    dic.Add("createBy", dr["CREATE_BY"].ToString());
                    //dic.Add("updateTime", dr["UPDATE_TIME"].ToString());
                    dic.Add("updateBy", dr["UPDATE_BY"].ToString());
                    dic.Add("srcId", dr["SRC_ID"].ToString());
                    dic.Add("operatorType", dr["OPERATOR_TYPE"].ToString());
                    dic.Add("dataSource", dr["DATA_SOURCE"].ToString());
                    dic.Add("isUpload", dr["ISUPLOAD"].ToString());
                    dic.Add("isScancard", dr["IS_SCANCARD"].ToString());
                    //dic.Add("", dr[""].ToString());
                    try
                    {
                        if (dr["REGISTER_TIME"] != null)
                        {
                            dic.Add("registerTime", ConvertStringToDateTime( ( (((DateTime)dr["REGISTER_TIME"]).ToUniversalTime().Ticks - 621355968000000000) / 10000)+"") + "");
                        }
                        if (dr["CREATE_TIME"] != null)
                        {
                            dic.Add("createTime", ConvertStringToDateTime(((((DateTime)dr["CREATE_TIME"]).ToUniversalTime().Ticks - 621355968000000000) / 10000) + ""));
                        }
                        if (dr["UPDATE_TIME"] != null)
                        {
                            dic.Add("updateTime", ConvertStringToDateTime(((((DateTime)dr["UPDATE_TIME"]).ToUniversalTime().Ticks - 621355968000000000) / 10000) + ""));
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            catch (Exception e) {
                Log.WriteLogLine("查询登记信息异常："+e.Message);
            }
            return dic;
        }


        public static void HandlerRegisterInfo(SqlHelper.CspConfRegister obj)
        {
            string sqlState = " select isupload from csp_conf_register where id='"+obj.id+"' ";
            string isUpload = DBsqliteHelper.getSqlResult(sqlState);
            if ( string.IsNullOrEmpty(isUpload) || (!"0".Equals(isUpload))) {
                String sql = " REPLACE INTO CSP_CONF_REGISTER(" +
                      "id,PERSON_ID,person_Type,conf_Id,CONF_PLACE_ID,remark,REGISTER_BY," +
                      "REGISTER_TIME,CREATE_TIME,create_by,UPDATE_TIME,UPDATE_BY,SRC_ID,OPERATOR_TYPE,DATA_SOURCE,ISUPLOAD";
                String values = "";
                values += "'" + obj.id + "'" + ",";
                values += "'" + obj.personId + "'" + ",";
                values += "'" + obj.personType + "'" + ",";
                values += "'" + obj.confId + "'" + ",";
                values += "'" + obj.confPlaceId + "'" + ",";
                values += "'" + obj.remark + "'" + ",";
                values += "'" + obj.registerBy + "'" + ",";
                values += obj.registerTime == null ? "null" + "," : "'" + obj.registerTime + "'" + ",";// DateTimeHelper.ConvertStringToDateTime(obj.registerTime)
                values += obj.createTime == null ? "null" + "," : "'" + obj.createTime + "'" + ",";//DateTimeHelper.ConvertStringToDateTime(obj.createTime) 
                values += "'" + obj.createBy + "'" + ",";
                values += obj.updateTime == null ? "null" + "," : "'" + obj.updateTime + "'" + ",";// DateTimeHelper.ConvertStringToDateTime(obj.updateTime) 
                values += "'" + obj.updateBy + "'" + ",";
                values += "'" + System.Guid.NewGuid().ToString() + "'" + ",'1','1','1'";
                sql += ")values(" + values + ")";
                DBsqliteHelper.ExecuteNonQuery(sql);
            }           
        }


        public static void HandlerMainPersonInfo(SqlHelper.CspMainPerson obj)
        {

            string stateSql = " select isupload from csp_main_person where id='"+obj.id+"' ";
            string stateStr = DBsqliteHelper.getSqlResult(stateSql);
            if ( string.IsNullOrEmpty(stateStr) || (!"0".Equals(stateStr)) ) {
                String sql = " REPLACE INTO CSP_MAIN_PERSON(" +
                       "id,ID_NUMBER,ID_TYPE,TYPE,NAME,gender,birthday," +
                       "tel,email,workplace,department,major,degree,duty," +
                       "province,city,county,address,remark,files,DATA_VERSION,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME,PC_idtype,PC_idNumber,isupload";
                String values = "";
                values += "'" + obj.id + "'" + ",";
                values += "'" + obj.idNumber + "'" + ",";
                values += obj.idType == null ? "null" + "," : "'" + obj.idType + "'" + ",";
                values += obj.type == null ? "null" + "," : "'" + obj.type + "'" + ",";
                values += "'" + obj.name + "'" + ",";
                values += obj.gender == null ? "null" + "," : "'" + obj.gender + "'" + ",";
                values += obj.birthday == null ? "null" + "," : "'" + obj.birthday + "'" + ",";//DateTimeHelper.ConvertStringToDateTime(obj.birthday)
                values += "'" + obj.tel + "'" + ",";
                values += "'" + obj.email + "'" + ",";
                values += "'" + obj.workplace + "'" + ",";
                values += "'" + obj.department + "'" + ",";
                values += "'" + obj.major + "'" + ",";
                values += "'" + obj.degree + "'" + ",";
                values += "'" + obj.duty + "'" + ",";
                values += "'" + obj.province + "'" + ",";
                values += "'" + obj.city + "'" + ",";
                values += "'" + obj.county + "'" + ",";
                values += "'" + obj.address + "'" + ",";
                values += "'" + obj.remark + "'" + ",";
                values += "'" + obj.files + "'" + ",";
                values += "'" + obj.dataVersion + "'" + ",";
                values += "'" + obj.createBy + "'" + ",";
                values += obj.createTime == null ? "null" + "," : "'" + obj.createTime + "'" + ",";//DateTimeHelper.ConvertStringToDateTime(obj.createTime)
                values += "'" + obj.createBy + "'" + ",";
                values += obj.updateTime == null ? "null" + "," : "'" + obj.updateTime + "'" + ",";// DateTimeHelper.ConvertStringToDateTime(obj.updateTime) 
                values += "'" + obj.pcIdType + "' , '" + obj.pcIdNumber + "','1'";
                sql += ")values(" + values + ")";
                DBsqliteHelper.ExecuteNonQuery(sql);
            }

            
        }

        public static string ConvertStringToDateTime(string p_date) {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(p_date + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow).ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}


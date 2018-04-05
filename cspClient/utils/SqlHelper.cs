using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cspClient.utils
{
    public static class SqlHelper
    {
        

        public static String sysAccount(SysAccount obj)
            {

            String sql = " REPLACE INTO sys_account(" +
                      "id,LOGIN_NAME,PASSWORD,NAME,SALT,IS_ADMIN,CREATE_BY,CREATE_TIME,UPDATE_TIME,REMARK,IS_DELETE," +
                      "HELP_CODE,UPDATE_BY,USER_ID,USER_TYPE";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.loginName + "'" + ",";
            values += "'" + obj.password + "'" + ",";
            values += "'" + obj.name + "'" + ",";
            values += "'" + obj.salt + "'" + ",";
            values += obj.isAdmin == null ? "null" + "," : "'" + obj.isAdmin + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.remark + "'" + ",";
            values += obj.isDelete == null ? "null" + "," : "'" + obj.isDelete + "'" + ",";
            values += "'" + obj.helpCode + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += "'" + obj.userId + "'" + ",";
            values += obj.userType == null ? "null" + "" : "'" + obj.userType + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }
        public static String cspConference(JObject obj)
        {

            String sql = " REPLACE INTO CSP_CONFERENCE(" +
                       "id,CONF_NO,CONF_TYPE,CONF_FORM,CONF_HOST,CONF_NAME," +
                       "CONF_ORGANISER,CONF_CO_ORGANISER,CONF_PIC,CONF_TOPIC,CONF_DESCRIPTION," +
                       "START_TIME,END_TIME,CONF_PLACE,REMARK,DATA_VERSION,CREATE_BY,CREATE_TIME," +
                       "UPDATE_BY,UPDATE_TIME,status";
            String values = "";
            values += obj["id"] == null || "".Equals(obj["id"].ToString()) ? "''" + "," : "'" + obj["id"].ToString() + "'" + ",";
            values += obj["confNo"] == null || "".Equals(obj["confNo"].ToString()) ? "''" + "," : "'" + obj["confNo"].ToString() + "'" + ",";
            values += obj["confType"] == null || "".Equals(obj["confType"].ToString()) ? "null" + "," : "'" + obj["confType"].ToString() + "'" + ",";
            values += obj["confForm"] == null || "".Equals(obj["confForm"].ToString()) ? "''" + "," : "'" + obj["confForm"].ToString() + "'" + ",";
            values += obj["confHost"] == null || "".Equals(obj["confHost"].ToString()) ? "''" + "," : "'" + obj["confHost"].ToString() + "'" + ",";
            values += obj["confName"] == null || "".Equals(obj["confName"].ToString()) ? "''" + "," : "'" + obj["confName"].ToString() + "'" + ",";
            values += obj["confOrganiser"] == null || "".Equals(obj["confOrganiser"].ToString()) ? "''" + "," : "'" + obj["confOrganiser"].ToString() + "'" + ",";
            values += obj["confCoOrganiser"] == null || "".Equals(obj["confCoOrganiser"].ToString()) ? "''" + "," : "'" + obj["confCoOrganiser"].ToString() + "'" + ",";
            values += obj["confPic"] == null || "".Equals(obj["confPic"].ToString()) ? "''" + "," : "'" + obj["confPic"].ToString() + "'" + ",";
            values += obj["confTopic"] == null || "".Equals(obj["confTopic"].ToString()) ? "''" + "," : "'" + obj["confTopic"].ToString() + "'" + ",";
            values += obj["confDescription"] == null || "".Equals(obj["confDescription"].ToString()) ? "''" + "," : "'" + obj["confDescription"].ToString() + "'" + ",";
            values += obj["startTime"] == null || "".Equals(obj["startTime"].ToString()) ? "''" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["startTime"].ToString()) + "'" + ",";
            values += obj["endTime"] == null || "".Equals(obj["endTime"].ToString()) ? "''" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["endTime"].ToString()) + "'" + ",";
            values += obj["confPlace"] == null || "".Equals(obj["confPlace"].ToString()) ? "''" + "," : "'" + obj["confPlace"].ToString() + "'" + ",";
            values += obj["remark"] == null || "".Equals(obj["remark"].ToString()) ? "''" + "," : "'" + obj["remark"].ToString() + "'" + ",";
            values += obj["dataVersion"] == null || "".Equals(obj["dataVersion"].ToString()) ? "''" + "," : "'" + obj["dataVersion"].ToString() + "'" + ",";
            values += obj["createBy"] == null || "".Equals(obj["createBy"].ToString()) ? "''" + "," : "'" + obj["createBy"].ToString() + "'" + ",";
            values += obj["createTime"] == null || "".Equals(obj["createTime"].ToString()) ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["createTime"].ToString()) + "'" + ",";
            values += obj["updateBy"] == null || "".Equals(obj["updateBy"].ToString()) ? "''" + "," : "'" + obj["updateBy"].ToString() + "'" + ",";
            values += obj["updateTime"] == null || "".Equals(obj["updateTime"].ToString()) ? " ''" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["updateTime"].ToString()) + "'" + ",";
            values += obj["status"] == null || "".Equals(obj["status"].ToString()) ? " ''" + "" : "'" + obj["status"].ToString() + "'" + "";

            sql += ")values(" + values + ")";
            return sql;
        }
        //public static String cspConferenceList(JObject obj)
        //{

        //    String sql = " REPLACE INTO CSP_CONFERENCE(" +
        //               "id,CONF_NO,CONF_TYPE,CONF_FORM,CONF_HOST,CONF_NAME," +
        //               "CONF_ORGANISER,CONF_CO_ORGANISER,CONF_PIC,CONF_TOPIC,CONF_DESCRIPTION," +
        //               "START_TIME,END_TIME,CONF_PLACE,REMARK,DATA_VERSION,CREATE_BY,CREATE_TIME," +
        //               "UPDATE_BY,UPDATE_TIME";
        //    String values = "";
        //    values += obj["conf"]["id"] == null || "".Equals(obj["id"].ToString()) ? "''" + "," : "'" + obj["id"].ToString() + "'" + ",";
        //    values += obj["conf"]["confNo"] == null || "".Equals(obj["confNo"].ToString()) ? "''" + "," : "'" + obj["confNo"].ToString() + "'" + ",";
        //    values += obj["conf"]["confType"] == null || "".Equals(obj["confType"].ToString()) ? "null" + "," : "'" + obj["confType"].ToString() + "'" + ",";
        //    values += obj["conf"]["confForm"] == null || "".Equals(obj["confForm"].ToString()) ? "''" + "," : "'" + obj["confForm"].ToString() + "'" + ",";
        //    values += obj["conf"]["confHost"] == null || "".Equals(obj["confHost"].ToString()) ? "''" + "," : "'" + obj["confHost"].ToString() + "'" + ",";
        //    values += obj["conf"]["confName"] == null || "".Equals(obj["confName"].ToString()) ? "''" + "," : "'" + obj["confName"].ToString() + "'" + ",";
        //    values += obj["conf"]["confOrganiser"] == null || "".Equals(obj["confOrganiser"].ToString()) ? "''" + "," : "'" + obj["confOrganiser"].ToString() + "'" + ",";
        //    values += obj["conf"]["confCoOrganiser"] == null || "".Equals(obj["confCoOrganiser"].ToString()) ? "''" + "," : "'" + obj["confCoOrganiser"].ToString() + "'" + ",";
        //    values += obj["conf"]["confPic"] == null || "".Equals(obj["confPic"].ToString()) ? "''" + "," : "'" + obj["confPic"].ToString() + "'" + ",";
        //    values += obj["confTopic"] == null || "".Equals(obj["confTopic"].ToString()) ? "''" + "," : "'" + obj["confTopic"].ToString() + "'" + ",";
        //    values += obj["confDescription"] == null || "".Equals(obj["confDescription"].ToString()) ? "''" + "," : "'" + obj["confDescription"].ToString() + "'" + ",";
        //    values += obj["startTime"] == null || "".Equals(obj["startTime"].ToString()) ? "''" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["startTime"].ToString()) + "'" + ",";
        //    values += obj["endTime"] == null || "".Equals(obj["endTime"].ToString()) ? "''" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["endTime"].ToString()) + "'" + ",";
        //    values += obj["confPlace"] == null || "".Equals(obj["confPlace"].ToString()) ? "''" + "," : "'" + obj["confPlace"].ToString() + "'" + ",";
        //    values += obj["remark"] == null || "".Equals(obj["remark"].ToString()) ? "''" + "," : "'" + obj["remark"].ToString() + "'" + ",";
        //    values += obj["dataVersion"] == null || "".Equals(obj["dataVersion"].ToString()) ? "''" + "," : "'" + obj["dataVersion"].ToString() + "'" + ",";
        //    values += obj["createBy"] == null || "".Equals(obj["createBy"].ToString()) ? "''" + "," : "'" + obj["createBy"].ToString() + "'" + ",";
        //    values += obj["createTime"] == null || "".Equals(obj["createTime"].ToString()) ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj["createTime"].ToString()) + "'" + ",";
        //    values += obj["updateBy"] == null || "".Equals(obj["updateBy"].ToString()) ? "''" + "," : "'" + obj["updateBy"].ToString() + "'" + ",";
        //    values += obj["updateTime"] == null || "".Equals(obj["updateTime"].ToString()) ? "null" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj["updateTime"].ToString()) + "'" + "";
        //    sql += ")values(" + values + ")";
        //    return sql;
        //}


        public static String cspConfWorker(CspConfWorker obj)
        {

            String sql = " REPLACE INTO CSP_CONF_WORKER(" +
                       "id,WORKER_ID,WORK_TYPE,WORK_DESC,DATA_VERSION,CREATE_BY,CREATE_TIME," +
                       "UPDATE_BY,UPDATE_TIME,CONF_ID,WORKER_CON_NO";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.workerId + "'" + ",";
            values += obj.workType == null ? "null" + "," : "'" + obj.workType + "'" + ",";
            values += "'" + obj.workDesc + "'" + ",";
            values += "'" + obj.dataVersion + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += "'" + obj.workerConfNo + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }

        public static String cspConference(CspConFerence obj)
        {

            String sql = " REPLACE INTO csp_conference(" +
                       "ID,CONF_NO,CONF_TYPE,CONF_FORM,CONF_HOST,CONF_NAME,CONF_ORGANISER," +
                       "CONF_CO_ORGANISER,CONF_PIC,CONF_TOPIC,CONF_DESCRIPTION,START_TIME,END_TIME,CONF_PLACE," +
                       "REMARK,DATA_VERSION,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.confNo + "'" + ",";
            values += "'" + obj.confType + "'" + ",";
            values += "'" + obj.confForm + "'" + ",";
            values += "'" + obj.confHost + "'" + ",";
            values += "'" + obj.confName + "'" + ",";
            values += "'" + obj.confOrganiser + "'" + ",";
            values += "'" + obj.confCoOrganiser + "'" + ",";
            values += "'" + obj.confPic + "'" + ",";
            values += "'" + obj.confTopic + "'" + ",";
            values += "'" + obj.confDescription + "'" + ",";
            values += obj.updateTime == null ? "'null'" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.startTime) + "'" + ",";
            values += "'" + DateTimeHelper.ConvertStringToDateTime(obj.endTime) + "'" + ",";
            values += "'" + obj.confPlace + "'" + ",";
            values += "'" + obj.remark + "'" + ",";
            values += "'" + obj.dataVersion + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "'null'" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }

        public static String cspConfPlace(CspConfPlace obj)
        {

            String sql = " REPLACE INTO CSP_CONF_PLACE(" +
                       "ID,CONF_ID,CONF_HOST,CONF_TOPIC,CONF_DESCRIPTION,START_TIME,END_TIME," +
                       "CONF_PLACE,DATA_VERSION,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += "'" + obj.confHost + "'" + ",";
            values += "'" + obj.confTopic + "'" + ",";
            values += "'" + obj.confDescription + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.startTime) + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.endTime) + "'" + ",";
            values += "'" + obj.confPlace + "'" + ",";
            values += "'" + obj.dataVersion + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + "";
            sql += ")values(" + values + ")";


            return sql;
        }


        public static String cspMainPerson(CspMainPerson obj)
        {

            String sql = " REPLACE INTO CSP_MAIN_PERSON(" +
                       "id,ID_NUMBER,ID_TYPE,TYPE,NAME,gender,birthday," +
                       "tel,email,workplace,department,major,degree,duty," +
                       "province,city,county,address,remark,files,DATA_VERSION,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME,PC_idtype,PC_idNumber";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.idNumber + "'" + ",";
            values += obj.idType == null ? "null" + "," : "'" + obj.idType + "'" + ",";
            values += obj.type == null ? "null" + "," : "'" + obj.type + "'" + ",";
            values += "'" + obj.name + "'" + ",";
            values += obj.gender == null ? "null" + "," : "'" + obj.gender + "'" + ",";
            values += obj.birthday == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.birthday) + "'" + ",";
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
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.pcIdType + "' , '" + obj.pcIdNumber + "'";
            sql += ")values(" + values + ")";
            return sql;
        }
        



        //======================================================
        public static String cspConfrRegister(CspConfRegister obj)
        {

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
            values += obj.registerTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.registerTime) + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += "'" + System.Guid.NewGuid().ToString() + "'" + ",'1','1','1'";
            sql += ")values(" + values + ")";
            return sql;
        }
        public static String cspConfVisitor(CspConfVisitor obj)
        {

            String sql = " REPLACE INTO CSP_CONF_VISITOR(" +
                       "id,VISITOR_ID,CONF_ID,CONF_PLACE_ID,VISITOR_CONF_NO,CREATE_BY,CREATE_TIME," +
                       "UPDATE_BY,UPDATE_TIME";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.visitorId + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += "'" + obj.confPlaceId + "'" + ",";
            values += "'" + obj.visitorConfNo + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }


        public static String cspConfLecturer(CspConFlecturer obj)
        {

            String sql = " REPLACE INTO CSP_CONF_LECTURER(" +
                       "id,CONF_LECTURER_ID,CONF_ID,CONF_PLACE_ID,CREATE_BY,CREATE_TIME,UPDATE_BY," +
                       "update_Time,LECTURER_CON_NO";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.confLecturerId + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += "'" + obj.confPlaceId + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.lecturerConfNo + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }

        //*-*********************************************

        public static String cspConfCheckin(CspConfCheckin obj)
        {

            String sql = " REPLACE INTO CSP_CONF_CHECKIN(" +
                       "id,CONF_ID,CHECKIN_TYPE,START_TIME,END_TIME,STATUS,REMARK," +
                       "DATA_VERSION,CREATE_BY,CREATE_TIME,UPDATE_BY,update_Time";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += obj.checkinType == null ? "null" + "," : "'" + obj.checkinType + "'" + ",";
            values += obj.startTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.startTime) + "'" + ",";
            values += obj.endTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.endTime) + "'" + ",";
            values += obj.status == null ? "null" + "," : "'" + obj.status + "'" + ",";
            values += "'" + obj.remark + "'" + ",";
            values += "'" + obj.dataVersion + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "' '" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }
        public static String cspConfCheckinRecord(CspConfCheckinRecord obj)
        {

            String sql = " REPLACE INTO CSP_CONF_CHECKIN_RECORD(" +
                       "id,VISITOR_ID,CONF_ID,CONF_PLACE_ID,VISITOR_CONF_NO,REMARK,CHECKIN_BY," +
                       "CHECKIN_TIME,CREATE_BY,CREATE_TIME,UPDATE_BY,update_Time,SRC_ID,CHECKIN_ID";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.visitorId + "'" + ",";
            values += "'" + obj.confId + "'" + ",";
            values += "'" + obj.confPlaceId + "'" + ",";
            values += "'" + obj.visitorConfNo + "'" + ",";
            values += "'" + obj.remark + "'" + ",";
            values += "'" + obj.checkinBy + "'" + ",";
            values += obj.checkinTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.checkinTime) + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.srcId + "'" + ",";

            values += "'" + obj.checkinId + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }
        public static String cspConfCheckinWorker(CspConfCheckinWorker obj)
        {

            String sql = " REPLACE INTO CSP_CONF_CHECKIN_WORKER(" +
                       "id,WORKER_ID,IS_PRIMARY,CREATE_TIME,CREATE_BY,UPDATE_TIME,UPDATE_BY," +
                       "CHECKIN_ID";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.workerId + "'" + ",";
            values += obj.isPrimary == null ? "null" + "," : "'" + obj.isPrimary + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.updateTime + "'" + ",";
            values += "'" + obj.checkinId + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }
        public static String cspConfCheckinPerson(CspConfCheckinPerson obj)
        {

            String sql = " REPLACE INTO CSP_CONF_CHECKIN_PERSON(" +
                       "id,PERSON_ID,PERSON_TYPE,CREATE_BY,CREATE_TIME,UPDATE_BY,UPDATE_TIME," +
                       "CHECKIN_ID";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.personId + "'" + ",";
            values += obj.personType == null ? "null" + "," : "'" + obj.personType + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += "'" + obj.checkinId + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }

        public static String sysParatype(SysParatype obj)
        {
            String sql = " REPLACE INTO sys_Paratype(" +
                       "id,CODE,VALUE,IS_VALID,PARENT_CODE,PARENT_NAME,HELP_CODE,REMARK,CREATE_BY,CREATE_TIME,UPDATE_BY," +
                       "UPDATE_TIME";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.code + "'" + ",";
            values += "'" + obj.value + "'" + ",";
            values += obj.isValid == null ? "null" + "," : "'" + obj.isValid + "'" + ",";
            values += "'" + obj.parentCode + "'" + ",";
            values += "'" + obj.parentName + "'" + ",";
            values += "'" + obj.helpCode + "'" + ",";
            values += "'" + obj.remark + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "" : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + "";
            sql += ")values(" + values + ")";
            return sql;
        }




        public static String sysParameter(SysParameter obj)
        {
            String sql = " REPLACE INTO sys_Parameter(" +
                       "id,TYPE_CODE,CODE,VALUE,IS_VALID,IS_DEFAULT,SEQUENCE,PARENT_CODE,PARATYPE_NAME,CREATE_BY,CREATE_TIME," +
                       "UPDATE_BY,UPDATE_TIME,VERSION,HELP_CODE,REMARK";
            String values = "";
            values += "'" + obj.id + "'" + ",";
            values += "'" + obj.typeCode + "'" + ",";
            values += "'" + obj.code + "'" + ",";
            values += "'" + obj.value + "'" + ",";
            values += obj.isValid == null ? "null" + "," : "'" + obj.isValid + "'" + ",";
            values += "'" + obj.isDefault + "'" + ",";
            values += obj.sequence == null ? "null" + "," : "'" + obj.sequence + "'" + ",";
            values += "'" + obj.parentCode + "'" + ",";
            values += "'" + obj.paratypeName + "'" + ",";
            values += "'" + obj.createBy + "'" + ",";
            values += obj.createTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.createTime) + "'" + ",";
            values += "'" + obj.updateBy + "'" + ",";
            values += obj.updateTime == null ? "null" + "," : "'" + DateTimeHelper.ConvertStringToDateTime(obj.updateTime) + "'" + ",";
            values += obj.version == null ? "null" + "," : "'" + obj.version + "'" + ",";
            values += "'" + obj.helpCode + "'" + ",";
            values += "'" + obj.remark + "'" + "";

            sql += ")values(" + values + ")";
            return sql;
        }

        public class SysParameter
        {
            public string id { get; set; }
            public string typeCode { get; set; }
            public string code { get; set; }
            public string value { get; set; }
            public string isValid { get; set; }
            public string isDefault { get; set; }
            public string sequence { get; set; }
            public string parentCode { get; set; }
            public string paratypeName { get; set; }
            public string createBy { get; set; }
            public string createTime { get; set; }
            public string updateBy { get; set; }
            public string updateTime { get; set; }
            public string version { get; set; }
            public string helpCode { get; set; }
            public string remark { get; set; }
        }



        public class SysParatype
        {
            public string id { get; set; }
            public string code { get; set; }
            public string value { get; set; }
            public string isValid { get; set; }
            public string parentCode { get; set; }
            public string parentName { get; set; }
            public string helpCode { get; set; }
            public string remark { get; set; }
            public string createBy { get; set; }
            public string createTime { get; set; }
            public string updateBy { get; set; }
            public string updateTime { get; set; }
        }
        public class CspConFerence
        {
            public string id { get; set; }
            public string confNo { get; set; }
            public String confType { get; set; }
            public String confForm { get; set; }
            public string confHost { get; set; }
            public string confName { get; set; }
            public string confOrganiser { get; set; }
            public string confCoOrganiser { get; set; }
            public String confPic { get; set; }
            public string confTopic { get; set; }
            public String confDescription { get; set; }
            public String startTime { get; set; }
            public String endTime { get; set; }
            public string confPlace { get; set; }
            public String remark { get; set; }
            public String dataVersion { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
        }


        public class CspConfPlace
        {
            public string id { get; set; }
            public string confId { get; set; }
            public string confHost { get; set; }
            public string confTopic { get; set; }
            public string confDescription { get; set; }
            public String startTime { get; set; }
            public String endTime { get; set; }
            public string confPlace { get; set; }
            public string dataVersion { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
        }




        public class CspConfRegister
        {
            public string id { get; set; }
            public string personId { get; set; }
            public string personType { get; set; }
            public string confId { get; set; }
            public string confPlaceId { get; set; }
            public string remark { get; set; }
            public string registerBy { get; set; }
            public String registerTime { get; set; }
            public String createTime { get; set; }
            public string createBy { get; set; }
            public String updateTime { get; set; }
            public string updateBy { get; set; }
            public string srcId { get; set; }
            public String operatorType { get; set; }
            public string dataSource { get; set; }
            public String isUpload { get; set; }
            public string isScancard { get; set; }
        }



        public class CspConfVisitor
        {
            public string id { get; set; }
            public string visitorId { get; set; }
            public string confId { get; set; }
            public string confPlaceId { get; set; }
            public string visitorConfNo { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
        }

        public class CspConFlecturer
        {
            public string id { get; set; }
            public string confLecturerId { get; set; }
            public string confId { get; set; }
            public string confPlaceId { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
            public String lecturerConfNo { get; set; }
                          
        }



        public class CspMainPerson
        {
            public string id { get; set; }
            public string idNumber { get; set; }
            public String idType { get; set; }
            public String type { get; set; }
            public string name { get; set; }
            public String gender { get; set; }
            public String birthday { get; set; }
            public string tel { get; set; }
            public string email { get; set; }
            public string workplace { get; set; }
            public string department { get; set; }
            public string major { get; set; }
            public string degree { get; set; }
            public string duty { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string county { get; set; }
            public string address { get; set; }
            public string remark { get; set; }
            public string files { get; set; }
            public string dataVersion { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
            public string isUpload { get; set; }
            public String srcId { get; set; }
            public String pcIdNumber { get; set; }
            public string pcIdType { get; set; }
            public string confId { get; set; }
            
        }
        public class CspConfWorker
        {
            public string id { get; set; }
            public string workerId { get; set; }
            public string workType { get; set; }
            public string workDesc { get; set; }
            public string dataVersion { get; set; }
            public string createBy { get; set; }
            public string createTime { get; set; }
            public string updateBy { get; set; }
            public string updateTime { get; set; }
            public string confId { get; set; }
            public string workerConfNo { get; set; }
            
        }

        public class CspConfCheckin
        {
            public string id { get; set; }
            public string confId { get; set; }
            public String checkinType { get; set; }
            public String startTime { get; set; }
            public String endTime { get; set; }
            public String status { get; set; }
            public string remark { get; set; }
            public string dataVersion { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
        }



        public class CspConfCheckinRecord
        {
            public string id { get; set; }
            public string visitorId { get; set; }
            public string confId { get; set; }
            public string confPlaceId { get; set; }
            public string visitorConfNo { get; set; }
            public string remark { get; set; }
            public string checkinBy { get; set; }
            public String checkinTime { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
            public string srcId { get; set; }
            public string checkinId { get; set; }
        }

        public class CspConfCheckinWorker
        {
            public string id { get; set; }
            public string workerId { get; set; }
            public String isPrimary { get; set; }
            public String createTime { get; set; }
            public string createBy { get; set; }
            public String updateTime { get; set; }
            public string updateBy { get; set; }
            public string checkinId { get; set; }
        }

        public class CspConfCheckinPerson
        {
            public string id { get; set; }
            public string personId { get; set; }
            public String personType { get; set; }
            public string createBy { get; set; }
            public String createTime { get; set; }
            public string updateBy { get; set; }
            public String updateTime { get; set; }
            public string checkinId { get; set; }
        }

        public class ConfList
        {
            public string id { get; set; }
            public string confNo { get; set; }
            public string confType { get; set; }
            public string confForm { get; set; }
            public string confHost { get; set; }
            public string confName { get; set; }
            public string confOrganiser { get; set; }
            public string confCoOrganiser { get; set; }
            public string confPic { get; set; }
            public string confTopic { get; set; }
            public string confDescription { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public string confPlace { get; set; }
            public string remark { get; set; }
            public string dataVersion { get; set; }
            public string createBy { get; set; }
            public string createTime { get; set; }
            public string updateBy { get; set; }
            public string updateTime { get; set; }
            public List<String> confList { get; set; }
            public string dataState { get; set; }
            public string serverType1Version { get; set; }
            public string serverType2Version { get; set; }
            public string serverType3Version { get; set; }
            public string serverType4Version { get; set; }
            public string localType1Version { get; set; }
            public string localType2Version { get; set; }
            public string localType3Version { get; set; }
            public string localType4Version { get; set; }
        }

        public class SysAccount
        {
            public string id { get; set; }
            public string loginName { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string salt { get; set; }
            public string isAdmin { get; set; }
            public string createBy { get; set; }
            public string createTime { get; set; }
            public string updateTime { get; set; }
            public string remark { get; set; }
            public string isDelete { get; set; }
            public string helpCode { get; set; }
            public string updateBy { get; set; }
            public string userId { get; set; }
            public string userType { get; set; }
        }

    }
}
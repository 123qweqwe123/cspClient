using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace cspClient.utils
{
    class WebUploadUtils
    {
        /// <summary>  
        /// 使用Post方法获取字符串结果  
        /// </summary>  
        /// <param name="url"></param>  
        /// <param name="formItems">Post表单内容</param>  
        /// <param name="cookieContainer"></param>  
        /// <param name="timeOut">默认5秒</param>  
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>  
        /// <returns></returns>  
        public static string PostForm(string url, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 300000)
        {
            System.GC.Collect();// 回收无效释放的连接 避免请求超时
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            #region 初始化请求对象  
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            request.ServicePoint.Expect100Continue = false;
            request.Proxy = null;


            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符  
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流  
            var postStream = new MemoryStream();
            #region 处理Form表单请求内容  
            //是否用Form上传文件  
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板  
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板  
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件  
                        formdata = string.Format(
                            fileFormdataTemplate,
                            item.Key, //表单键  
                            item.FileName);
                    }
                    else
                    {
                        //上传文本  
                        formdata = string.Format(
                            dataFormdataTemplate,
                            item.Key,
                            item.Value);
                    }

                    //统一处理  
                    byte[] formdataBytes = null;
                    //第一行不需要换行  
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容  
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        using (var stream = item.FileContent)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流  
            if (postStream != null)
            {
                postStream.Position = 0;
                //直接写入流  
                Stream requestStream;
                try
                {
                    requestStream = request.GetRequestStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    ////debug  
                    //postStream.Seek(0, SeekOrigin.Begin);  
                    //StreamReader sr = new StreamReader(postStream);  
                    //var postStr = sr.ReadToEnd();  
                    postStream.Close();//关闭文件访问 

                    requestStream.Close();
                }
                catch (Exception e)
                {
                    Log.WriteLogLine("获取request请求对象出现异常：" + e.Message);
                    return null;
                }
            }
            #endregion

            try
            {

                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                { // 捕获异常信息
                    response = (HttpWebResponse)ex.Response;
                }

                if (cookieContainer != null)
                {
                    response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        if (response != null) //  关闭使用完毕的http连接  防止超时异常
                        {
                            response.Close();
                        }
                        if (request != null)
                        {
                            request.Abort();
                        }
                        return retString;
                    }
                }
            }
            catch (Exception e)
            {

                Log.WriteLogLine("响应为空异常：" + e.ToString());

                return "";
            }

        }


        public static string HttpPost(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "POST";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";// "text/xml";
                m_hwr_Request.Timeout = -1;
                byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                m_hwr_Request.ContentLength = ping.Length;

                m_hwr_Request.Proxy = null;
                m_hwr_Request.KeepAlive = false;

                Stream myRequestStream = m_hwr_Request.GetRequestStream();
                myRequestStream.Write(ping, 0, ping.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("POST请求异常：" + e.Message);
                return "";
            }

        }

    }


    /// <summary>  
    /// 表单数据项  
    /// </summary>  
    public class FormItemModel
    {
        /// <summary>  
        /// 表单键，request["key"]  
        /// </summary>  
        public string Key { set; get; }
        /// <summary>  
        /// 表单值,上传文件时忽略，request["key"].value  
        /// </summary>  
        public string Value { set; get; }
        /// <summary>  
        /// 是否是文件  
        /// </summary>  
        public bool IsFile
        {
            get
            {
                if (FileContent == null || FileContent.Length == 0)
                    return false;

                if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                    throw new Exception("上传文件时 FileName 属性值不能为空");
                return true;
            }
        }
        /// <summary>  
        /// 上传的文件名  
        /// </summary>  
        public string FileName { set; get; }
        /// <summary>  
        /// 上传的文件内容  
        /// </summary>  
        public Stream FileContent { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace cspClient.utils
{
    class WebDownLoadUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static String HttpGetUploadPath(string Url)
        {
            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "Get";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                //byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                //m_hwr_Request.ContentLength = ping.Length;
                //Stream myRequestStream = m_hwr_Request.GetRequestStream();
                //myRequestStream.Write(ping, 0, ping.Length);
                //myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                String retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                response.Close();
                return retString;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("get请求异常：" + e.Message);
                return "";
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static String getHttpPostResponse(string url)
        {
            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(url);
                m_hwr_Request.Method = "post";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                
                //byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                //m_hwr_Request.ContentLength = ping.Length;
                //Stream myRequestStream = m_hwr_Request.GetRequestStream();
                //myRequestStream.Write(ping, 0, ping.Length);
                //myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                String retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                response.Close();
                return retString;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("get请求异常：" + e.Message);
                return "";
            }

        }



        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return path;
        }

       
        /// <summary>
        /// Http GET请求方式请求 下载文件
        /// 返回值为response头中Content-Disposition携带的文件名
        /// </summary>
        public static String HttpGetDownLoad(string Url, string postDataStr, String path)
        {
            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "Get";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                //byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                //m_hwr_Request.ContentLength = ping.Length;
                //Stream myRequestStream = m_hwr_Request.GetRequestStream();
                //myRequestStream.Write(ping, 0, ping.Length);
                //myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                String fileNameHead = response.Headers["Content-Disposition"];
                String fileName = fileNameHead.Substring(fileNameHead.IndexOf("=") + 1).Trim();

                //String isNeedDownload = response.Headers["result"].ToString();
                //if (isNeedDownload.Equals("fail"))
                //{ // 标识暂无数据需要下发处理
                //    return "";
                //}

                Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return fileName;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("POST请求异常：" + e.Message);
                return "";
            }

        }

        /// <summary>
        /// Http POST请求方式请求 下载文件
        /// 返回值为response头中Content-Disposition携带的文件名
        /// </summary>
        public static String HttpPostDownLoad(string Url, string postDataStr, String path)
        {
            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "POST";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                m_hwr_Request.ContentLength = ping.Length;
                Stream myRequestStream = m_hwr_Request.GetRequestStream();
                myRequestStream.Write(ping, 0, ping.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                String fileNameHead = response.Headers["Content-Disposition"];
                String fileName = fileNameHead.Substring(fileNameHead.IndexOf("=") + 1).Trim();

                String isNeedDownload = response.Headers["result"].ToString();
                if (isNeedDownload.Equals("fail"))
                { // 标识暂无数据需要下发处理
                    return "";
                }

                Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return fileName;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("POST请求异常：" + e.Message);
                return "";
            }

        }


        /// <summary>
        /// Http POST请求方式请求 下载文件
        /// 返回值为response头Heads
        /// </summary>
        public static Dictionary<String, String> HttpDownFileAndHdeads(string Url, string postDataStr, String path)
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();

            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "POST";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                m_hwr_Request.ContentLength = ping.Length;
                Stream myRequestStream = m_hwr_Request.GetRequestStream();
                myRequestStream.Write(ping, 0, ping.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                for (int i = 0; i < response.Headers.Count; ++i)
                {
                    dic.Add(response.Headers.Keys[i], response.Headers[i]);
                }
                //String fileNameHead = response.Headers["Content-Disposition"];
                //String fileName = fileNameHead.Substring(fileNameHead.IndexOf("=") + 1).Trim();

                //String isNeedDownload = response.Headers["result"].ToString();
                //if (isNeedDownload.Equals("fail"))
                //{ // 标识暂无数据需要下发处理
                //    return "";
                //}

                Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return dic;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("POST请求异常：" + e.Message);
                return dic;
            }

        }


        /// <summary>
        /// 解密解析 目录下的xml文件并入库为展示做准备
        /// </summary>
        public static void HandlerDownLoadXml(String path)
        {
            List<String> sqls = new List<string>();
            if (Directory.Exists(path))
            {
                String[] fileNames = Directory.GetFiles(path);

                // 存放盒号 放置重复操作
                HashSet<string> boxCodex_Arr = new HashSet<string>();

                foreach (String filePath in fileNames)
                {
                    String deStr = DeEncrypt.DecryptXmlStr(File.ReadAllText(filePath), "420500"); // 对xml文件解密
                    HashSet<string> temp_boxCodes = new HashSet<string>();

                    XDocument xDoc = XDocument.Parse(deStr); //格式化为xmlDoc对象
                    XElement rootEle = xDoc.Root;

                    IEnumerable<XElement> elements_boxcode = rootEle.XPathSelectElements("/FROZENTUBE/ITEM/BOX_CODE");
                    foreach (XElement codeEle in elements_boxcode)
                    {

                        XElement typeEle = codeEle.XPathSelectElement("../BOX_TYPE");

                        temp_boxCodes.Add(codeEle.Value + typeEle.Value);

                    }
                    foreach (String downCode in temp_boxCodes)
                    {
                        if (boxCodex_Arr.Contains(downCode))
                        {
                            continue;
                        }
                        else
                        {
                            boxCodex_Arr.Add(downCode);
                        }
                    }

                    IEnumerable<XElement> elements = rootEle.Elements();

                    foreach (XElement item in elements) // item元素
                    {
                        // 数据包含ID  存在即更新不存在则插入
                        String sql = " REPLACE INTO T_HR_PUBLIC_FROZEN_TUBE_DOWNLOAD(";
                        String values = "";// "'" + Guid.NewGuid().ToString() + "',";
                        IEnumerable<XElement> fields = item.Elements();
                        foreach (XElement field in fields)
                        {
                            values += "'" + field.Value + "',";
                            sql += field.Name + ","; ;
                        }
                        sql += "xmlname,handler_date";
                        sql += ")values(";
                        sql += values + "'" + filePath + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " )";
                        Log.WriteLogLine("下发数据SQL打印:" + sql);
                        sqls.Add(sql);
                    }
                }
            }
        }


        public static Dictionary<String, String> HttpDownFileAndHdeadsThroughLogin(string Url, string postDataStr, String path, CookieContainer cookie)
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();

            try
            {
                HttpWebRequest m_hwr_Request = (HttpWebRequest)WebRequest.Create(Url);
                m_hwr_Request.Method = "POST";
                m_hwr_Request.ContentType = "application/x-www-form-urlencoded";
                m_hwr_Request.Timeout = -1;
                byte[] ping = Encoding.UTF8.GetBytes(postDataStr);
                m_hwr_Request.ContentLength = ping.Length;
                m_hwr_Request.CookieContainer = cookie;
                Stream myRequestStream = m_hwr_Request.GetRequestStream();
                myRequestStream.Write(ping, 0, ping.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)m_hwr_Request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                for (int i = 0; i < response.Headers.Count; ++i)
                {
                    dic.Add(response.Headers.Keys[i], response.Headers[i]);
                }
                //String fileNameHead = response.Headers["Content-Disposition"];
                //String fileName = fileNameHead.Substring(fileNameHead.IndexOf("=") + 1).Trim();

                //String isNeedDownload = response.Headers["result"].ToString();
                //if (isNeedDownload.Equals("fail"))
                //{ // 标识暂无数据需要下发处理
                //    return "";
                //}

                Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return dic;
            }
            catch (System.Exception e)
            {
                Log.WriteLogLine("POST请求异常：" + e.Message);
                return dic;
            }
        }   
         public static CookieContainer PostLogin(string postData, string requestUrlString)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            //向服务端请求
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = new CookieContainer();
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //将请求的结果发送给客户端(界面、应用)
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            CookieContainer cookie = new CookieContainer();
            cookie.Add(myResponse.Cookies);
            return cookie;
        }
    }
}

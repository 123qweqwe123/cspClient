using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SM.utils
{
    /// <summary>
    /// XmlHelper 的摘要说明。
    /// xml操作类
    /// </summary>
    public class XmlConfigHelper
    {
        /// <summary>
        /// 读取config.xml：根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueByKey(string key)
        {
            string value = "";
            XmlReader xReader = null;
            try
            {
                // 获得配置文件的全路径　　  
                string strPath = Application.StartupPath;
                string strCompletedPath = strPath + "\\config.xml";

                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;
                xReader = XmlReader.Create(strCompletedPath, setting);
                XmlDocument doc = new XmlDocument();
                doc.Load(xReader);

                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性　　　　  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素　　　　  
                    if (att.Value == key)
                    {
                        //对目标元素中的第二个属性赋值　　　　　  
                        att = nodes[i].Attributes["value"];
                        value = att.Value;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("读取config.xml：根据key获取value 异常:" + e.ToString());
            }
            finally
            {
                if (xReader != null)
                {
                    xReader.Close();
                }
            }
            return value;
        }


        /// <summary>
        /// 从指定xml配置文件中读取指定的Key值
        /// </summary>
        /// <returns></returns>
        public static String getValueByKeyFromConfig(String configName, String key)
        {
            string value = "";
            XmlReader xReader = null;
            try
            {
                // 获得配置文件的全路径　　  
                string strPath = Application.StartupPath;
                string strCompletedPath = strPath + "\\" + configName;

                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;
                xReader = XmlReader.Create(strCompletedPath, setting);
                XmlDocument doc = new XmlDocument();
                doc.Load(xReader);

                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性　　　　  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素　　　　  
                    if (att.Value == key)
                    {
                        //对目标元素中的第二个属性赋值　　　　　  
                        att = nodes[i].Attributes["value"];
                        value = att.Value;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("读取config.xml：根据key获取value 异常:" + e.ToString());
            }
            finally
            {
                xReader.Close();
            }
            return value;
        }

        /// <summary>
        /// 读取config.xml，根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void SetValueByKey(String configName, string keyName, string keyValue)
        {
            XmlTextWriter writer = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                // 获得配置文件的全路径　　  
                string strPath = Environment.CurrentDirectory;
                string strCompletedPath = strPath + "\\" + configName;
                doc.Load(strCompletedPath);
                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlElement xe = nodes[i] as XmlElement;
                    //获得将当前元素的key属性　　　　  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素　　　　  
                    if (att.Value == keyName)
                    {
                        //对目标元素中的value属性赋值　　　　　  
                        xe.SetAttribute("value", keyValue);
                        break;
                    }
                }
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                writer = new XmlTextWriter(strCompletedPath, utf8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                writer.Close();
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine(configName + "属性保存失败：" + e.ToString());
                throw new Exception(configName + "属性保存失败");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 设置或者新增属性
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        public static void SetAndAddValueByKey(String configName, string keyName, string keyValue)
        {
            XmlTextWriter writer = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                // 获得配置文件的全路径　　  
                string strPath = Environment.CurrentDirectory;
                string strCompletedPath = strPath + "\\" + configName;
                doc.Load(strCompletedPath);

                XmlNodeList nodes = doc.GetElementsByTagName("add");
                bool has = false;
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlElement xe = nodes[i] as XmlElement;
                    //获得将当前元素的key属性　　　　  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素　　　　  
                    if (att.Value.Equals(keyName))
                    {
                        //对目标元素中的value属性赋值　　　　　  
                        xe.SetAttribute("value", keyValue);
                        has = true;
                        break;
                    }
                }

                if (!has)
                {
                    XmlNode xmlNode = doc.SelectSingleNode("configuration//appSettings");
                    XmlElement xele = doc.CreateElement("add");
                    xele.SetAttribute("key", keyName);
                    xele.SetAttribute("value", keyValue);
                    xmlNode.AppendChild(xele);
                }

                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                writer = new XmlTextWriter(strCompletedPath, utf8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                writer.Close();
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine(configName + "属性保存失败：" + e.ToString());
                throw new Exception(configName + "属性保存失败");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 指定xml配置文件中设置指定的Key值
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void setValueByKeyFromConfig(String configName, Dictionary<String, String> dic)
        {
            XmlTextWriter writer = null;
            try
            {
                // 获得配置文件的全路径　　  
                string strPath = Application.StartupPath;
                string strCompletedPath = strPath + "\\" + configName;
                XmlDocument doc = new XmlDocument();
                doc.Load(strCompletedPath);
                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlElement xe = nodes[i] as XmlElement;
                    //获得将当前元素的key属性　　　　  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    foreach (String tmpKey in dic.Keys)
                    {
                        if (att.Value == tmpKey)
                        {
                            //对目标元素中的第二个属性赋值　　　　　  
                            xe.SetAttribute("value", dic[tmpKey]);
                            break;
                        }
                    }

                }
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                writer = new XmlTextWriter(strCompletedPath, utf8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                writer.Close();
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine(configName + "属性保存失败：" + e.ToString());
                throw new Exception(configName + "属性保存失败");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

        }

        /// <summary>
        /// 读取xml，根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<VersionInfo> GetVersionInfos(String configName)
        {
            List<VersionInfo> list = new List<VersionInfo>();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (!File.Exists(configName))
                {
                    return list;
                }
                doc.Load(configName);
                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    VersionInfo info = new VersionInfo();
                    info.fileName = nodes[i].Attributes["fileName"].Value;
                    info.pathDirectory = nodes[i].Attributes["pathDirectory"].Value;
                    info.md5 = nodes[i].Attributes["md5"].Value;
                    info.version = nodes[i].Attributes["version"].Value;
                    info.isSQL = nodes[i].Attributes["isSQL"].Value;
                    info.description = nodes[i].Attributes["description"].Value;
                    list.Add(info);
                }
            }
            catch (Exception e)
            {
                HR_Log.WriteLogLine("更新软件-----解析“" + configName + "”异常:" + e.ToString());
                throw e;
            }
            return list;
        }
        /// <summary>
        /// 根据xml、文件名，获取版本号
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetVersion(String configName, String fileName)
        {
            string version = "";
            List<VersionInfo> VersionInfos = GetVersionInfos(configName);
            foreach (var item in VersionInfos)
            {
                if (item.fileName == fileName)
                {
                    version = item.version;
                    break;
                }
            }
            return version;
        }
    }

    /// <summary>
    /// xml版本信息实体
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// 文件目录
        /// </summary>
        public string pathDirectory { get; set; }

        /// <summary>
        /// 文件md5值
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 文件版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 是否是需要执行的sql
        /// </summary>
        public string isSQL { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
}

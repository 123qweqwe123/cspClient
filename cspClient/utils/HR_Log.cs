using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SM.utils
{
    public static class HR_Log
    {
        #region  创建日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLogLine(string strMessage)
        {
            string strPath;
            DateTime dt = DateTime.Now;
            try
            {
                strPath = System.Windows.Forms.Application.StartupPath + "\\hr_logs";
                if (Directory.Exists(strPath) == false)
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = strPath + "\\" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + ".txt";
                StreamWriter FileWriter = new StreamWriter(strPath, true);
                FileWriter.WriteLine("[" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "]  " + strMessage);
                FileWriter.Close();
            }

            catch { }
        }

        public static void WriteSqlLine(string strMessage)
        {
            string strPath;
            DateTime dt = DateTime.Now;
            try
            {
                strPath = System.Windows.Forms.Application.StartupPath + "\\hr_logs";
                if (Directory.Exists(strPath) == false)
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = strPath + "\\" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + ".sql";
                StreamWriter FileWriter = new StreamWriter(strPath, true);
                FileWriter.WriteLine("[" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "]  " + strMessage);
                FileWriter.Close();
            }
            catch { }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLogLine(Exception e)
        {
            if (e == null) return;
            String errorInfo = e.ToString();
            if (errorInfo == null) return;
            WriteLogLine(errorInfo);
        }
        #endregion
    }
}

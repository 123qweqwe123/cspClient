using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cspClient.utils
{
    class MD5Utils
    {
        public static String GetMd5(String str) {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        //public static String GetMd5(String fileName)
        //{
        //    FileStream file = new FileStream(fileName, FileMode.Open);
        //    try
        //    {
        //        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //        byte[] retVal = md5.ComputeHash(file);
        //        file.Close();

        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < retVal.Length; i++)
        //        {
        //            sb.Append(retVal[i].ToString("x2"));
        //        }
        //        return sb.ToString();
        //}
    }
}

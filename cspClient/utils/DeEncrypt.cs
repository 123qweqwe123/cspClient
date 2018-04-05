using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cspClient.utils
{
    class DeEncrypt
    {
        ///<summary><![CDATA[字符串DES加密函数]]></summary>    
        ///<param name="str"><![CDATA[被加密字符串 ]]></param>    
        ///<param name="key"><![CDATA[密钥 ]]></param>     
        ///<returns><![CDATA[加密后字符串]]></returns>       
        public static string EncodeDES(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Mode = CipherMode.ECB;
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                String res = Convert.ToBase64String(stream.ToArray());
                stream.Close();
                return res;
            }
            catch (Exception) { return ""; }
        }

        #region avro字节流数据加密
        public static String EncodeAvroDES(byte[] bytes, String keyStr)
        {
            MemoryStream stream = new MemoryStream();
            String res = "";
            try
            {
                String key = GetMD5String(keyStr).Substring(0, 8);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Mode = CipherMode.ECB;
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                res = Convert.ToBase64String(stream.ToArray()); // base64加密
                stream.Close();
                return res;
            }
            catch (Exception) { return res; }
        }
        #endregion

        ///<summary><![CDATA[字符串DES解密函数]]></summary>    
        ///<param name="str"><![CDATA[被解密字符串 ]]></param>    
        ///<param name="key"><![CDATA[密钥 ]]></param>     
        ///<returns><![CDATA[解密后字符串]]></returns>       
        public static string DecodeDES(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Mode = CipherMode.ECB;
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] buffer = Convert.FromBase64String(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                return Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
            }
            catch (Exception) { return ""; }
        }

        #region avro数据文件解密
        public static byte[] DecodeDESAvro(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Mode = CipherMode.ECB;
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] buffer = Convert.FromBase64String(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                return stream.ToArray();// Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
            }
            catch (Exception) { return new byte[] { }; }
        }

        #endregion
        public static String GetMD5String(String str)
        {
            //获取userPwd的byte类型数组  
            byte[] byteUserPwd = Encoding.UTF8.GetBytes(str);
            //实例化MD5CryptoServiceProvider  
            MD5CryptoServiceProvider myMd5 = new MD5CryptoServiceProvider();
            // byte类型数组的值转换为 byte类型的Md5值  
            byte[] byteMd5 = myMd5.ComputeHash(byteUserPwd);
            ////将byte类型的Md5值转换为字符串  
            //string res = Encoding.UTF8.GetString(byteMd5).Trim();
            ////返回Md5字符串  
            //return res;
            return Convert.ToBase64String(byteMd5);
        }

        public static byte[] GetMd5ByteArr(String str)
        {
            //获取userPwd的byte类型数组  
            byte[] byteUserPwd = Encoding.UTF8.GetBytes(str);
            //实例化MD5CryptoServiceProvider  
            MD5CryptoServiceProvider myMd5 = new MD5CryptoServiceProvider();
            // byte类型数组的值转换为 byte类型的Md5值  
            byte[] byteMd5 = myMd5.ComputeHash(byteUserPwd);
            //返回Md5字节串  
            return byteMd5;
        }


        public static String EncryptXmlStr(String srcStr, String keyStr)
        {
            String res = "";
            // 获取MD5加密后Base64加密的前八位做为密钥
            String key = GetMD5String(keyStr).Substring(0, 8);
            // 进行ECB模式的DES加密 获取加密后字符串
            res = EncodeDES(srcStr, key);
            return res;
        }

        public static String DecryptXmlStr(String srcStr, String keyStr)
        {
            String res = "";
            // 获取MD5加密后Base64加密的前八位做为密钥
            String key = GetMD5String(keyStr).Substring(0, 8);
            // 进行ECB模式的DES加密 获取加密后字符串
            res = DecodeDES(srcStr, key);
            return res;
        }

        public static byte[] DecryptAvroStr(String srcStr, String keyStr)
        {
            // 获取MD5加密后Base64加密的前八位做为密钥
            String key = GetMD5String(keyStr).Substring(0, 8);
            return DecodeDESAvro(srcStr, key);
        }
    }
}

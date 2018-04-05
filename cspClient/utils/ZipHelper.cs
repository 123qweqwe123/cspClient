using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace cspClient.utils
{
    class ZipHelper
    {
        #region 压缩  

        public static bool ZipFilesInDirectory(string folderToZip, string zipedFile, string password)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
                return result;

            if (File.Exists(zipedFile)) { File.Delete(zipedFile); }
            String dirPath = zipedFile.Replace(Path.GetFileName(zipedFile), "");
            if (!Directory.Exists(dirPath)) { Directory.CreateDirectory(dirPath); }
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipedFile)))
            {
                s.SetLevel(6);
                Compress(folderToZip, s);
                s.Finish();
                s.Close();
            }

            return result;
        }

        private static bool ZipFilesInDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();

            try
            {
                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ent = new ZipEntry(Path.Combine(parentFolderName + Path.GetFileName(file)));
                    ent.DateTime = DateTime.Now;
                    ent.Size = fs.Length;

                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    ent.Crc = crc.Value;
                    zipStream.PutNextEntry(ent);
                    zipStream.Write(buffer, 0, buffer.Length);
                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
                if (!ZipDirectory(folder, zipStream, folderToZip))
                    return false;

            return result;
        }
        /// <summary>   
        /// 递归压缩文件夹的内部方法   
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipStream">压缩输出流</param>   
        /// <param name="parentFolderName">此文件夹的上级文件夹</param>   
        /// <returns></returns>   
        private static bool ZipDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();

            try
            {
                ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/"));
                zipStream.PutNextEntry(ent);
                zipStream.Flush();

                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));
                    ent.DateTime = DateTime.Now;
                    ent.Size = fs.Length;

                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    ent.Crc = crc.Value;
                    zipStream.PutNextEntry(ent);
                    zipStream.Write(buffer, 0, buffer.Length);
                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
                if (!ZipDirectory(folder, zipStream, folderToZip))
                    return false;

            return result;
        }

        /// <summary>   
        /// 压缩文件夹    
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipedFile">压缩文件完整路径</param>   
        /// <param name="password">密码</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipDirectory(string folderToZip, string zipedFile, string password)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
                return result;

            ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile));
            zipStream.SetLevel(6);
            if (!string.IsNullOrEmpty(password)) zipStream.Password = password;

            result = ZipDirectory(folderToZip, zipStream, "");

            zipStream.Finish();
            zipStream.Close();

            return result;
        }

        /// <summary>   
        /// 压缩文件夹   
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipedFile">压缩文件完整路径</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipDirectory(string folderToZip, string zipedFile)
        {
            bool result = ZipDirectory(folderToZip, zipedFile, null);
            return result;
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileToZip">要压缩的文件全名</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="password">密码</param>   
        /// <returns>压缩结果</returns>   
        public static bool ZipFile(string fileToZip, string zipedFile, string password)
        {
            bool result = true;
            ZipOutputStream zipStream = null;
            FileStream fs = null;
            ZipEntry ent = null;

            if (!File.Exists(fileToZip))
                return false;

            try
            {
                fs = File.OpenRead(fileToZip);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                fs = File.Create(zipedFile);
                zipStream = new ZipOutputStream(fs);
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                ent = new ZipEntry(Path.GetFileName(fileToZip));
                zipStream.PutNextEntry(ent);
                zipStream.SetLevel(6);

                zipStream.Write(buffer, 0, buffer.Length);

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (ent != null)
                {
                    ent = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            GC.Collect();
            GC.Collect(1);

            return result;
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileToZip">要压缩的文件全名</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <returns>压缩结果</returns>   
        public static bool ZipFile(string fileToZip, string zipedFile)
        {
            bool result = ZipFile(fileToZip, zipedFile, null);
            return result;
        }

        /// <summary>   
        /// 压缩文件或文件夹   
        /// </summary>   
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="password">密码</param>   
        /// <returns>压缩结果</returns>   
        public static bool Zip(string fileToZip, string zipedFile, string password)
        {
            bool result = false;
            if (File.Exists(zipedFile))
            {
                File.Delete(zipedFile);
            }
            if (Directory.Exists(fileToZip))
                result = ZipDirectory(fileToZip, zipedFile, password);
            else if (File.Exists(fileToZip))
                result = ZipFile(fileToZip, zipedFile, password);

            return result;
        }

        /// <summary>   
        /// 压缩文件或文件夹   
        /// </summary>   
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <returns>压缩结果</returns>   
        public static bool Zip(string fileToZip, string zipedFile)
        {
            if (File.Exists(zipedFile + ".temp")) File.Delete(zipedFile + ".temp");
            if (File.Exists(zipedFile)) File.Delete(zipedFile);
            bool result = Zip(fileToZip, zipedFile, null);
            return result;

        }

        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="fileName">原文件</param>
        /// <param name="destFileName">生成目标文件</param>
        /// <param name="passWord">密码</param>
        public static void ZipOneFile(string fileName, string destFileName, string passWord)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string path = System.IO.Path.GetDirectoryName(fileName);
            if (File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }
            ZipOutputStream zos_Zip = new ZipOutputStream(File.Create(destFileName));
            zos_Zip.Password = passWord;
            zos_Zip.SetLevel(6);

            if (System.IO.File.Exists(fileName))
            {
                FileStream fs_File = File.OpenRead(fileName);
                byte[] buffer = new byte[fs_File.Length];
                fs_File.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(System.IO.Path.GetFileName(fileName));
                entry.DateTime = DateTime.Now;
                entry.Size = fs_File.Length;
                fs_File.Close();
                zos_Zip.PutNextEntry(entry);
                zos_Zip.Write(buffer, 0, buffer.Length);
                zos_Zip.Finish();
                zos_Zip.Close();
                System.IO.File.Delete(fileName);
            }
        }

        #endregion

        #region 解压  

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <param name="password">密码</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                                fs.Write(data, 0, size);
                            else
                                break;
                        }
                        fs.Close();
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder)
        {
            bool result = UnZip(fileToUnZip, zipedFolder, null);
            return result;
        }

        #endregion

        #region 获取文件MD5值
        public static string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            try
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                Log.WriteLogLine(fileName + " 文件的md5值为:" + sb.ToString());
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLogLine("获取Md5值失败:" + ex.Message);
                return null;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="source">待压缩文件夹</param>
        /// <param name="s">ZipOutputStream</param>
        public static void Compress(string source, ZipOutputStream s)
        {
            Log.WriteLogLine("zipTest：Compress() ");
            string[] filenames = Directory.GetFileSystemEntries(source);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    Compress(file, s);  //递归压缩子文件夹
                }
                else
                {
                    using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[4 * 1024];
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
            }
        }

    }

    /// <summary>
    /// 文件加密类
    /// </summary>
    public class FileEncrypt
    {
        #region 变量
        /// <summary>
        /// 一次处理的明文字节数
        /// </summary>
        public static readonly int encryptSize = 10000000;
        /// <summary>
        /// 一次处理的密文字节数
        /// </summary>
        public static readonly int decryptSize = 10000016;
        #endregion
        #region 加密文件
        /// <summary>
        /// 加密文件
        /// </summary>
        public static void EncryptFile(string path)
        {
            //try
            //{
            //    if (File.Exists(path + ".temp")) File.Delete(path + ".temp");
            //    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //    {
            //        if (fs.Length > 0)
            //        {
            //            using (FileStream fsnew = new FileStream(path + ".temp", FileMode.OpenOrCreate, FileAccess.Write))
            //            {
            //                if (File.Exists(path + ".temp")) File.SetAttributes(path + ".temp", FileAttributes.Hidden);
            //                int blockCount = ((int)fs.Length - 1) / encryptSize + 1;
            //                for (int i = 0; i < blockCount; i++)
            //                {
            //                    int size = encryptSize;
            //                    if (i == blockCount - 1) size = (int)(fs.Length - i * encryptSize);
            //                    byte[] bArr = new byte[size];
            //                    fs.Read(bArr, 0, size);
            //                    string dataStr = ThreeDes.EncodeCBCbyte(bArr);//加密
            //                    byte[] result = Encoding.Default.GetBytes(dataStr);

            //                    //byte[] result = AES.AESEncrypt(bArr, pwd);
            //                    fsnew.Write(result, 0, result.Length);
            //                    fsnew.Flush();
            //                }
            //                fsnew.Close();
            //                fsnew.Dispose();
            //            }
            //            fs.Close();
            //            fs.Dispose();
            //            FileAttributes fileAttr = File.GetAttributes(path);
            //            File.SetAttributes(path, FileAttributes.Archive);
            //            File.Delete(path);
            //            File.Move(path + ".temp", path);
            //            File.SetAttributes(path, fileAttr);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    File.Delete(path + ".temp");
            //    throw ex;
            //}
        }
        #endregion
        #region 解密文件
        /// <summary>
        /// 解密文件
        /// </summary>
        public static void DecryptFile(string path)
        {
            //try
            //{
            //    if (File.Exists(path + ".temp")) File.Delete(path + ".temp");
            //    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //    {
            //        if (fs.Length > 0)
            //        {
            //            using (FileStream fsnew = new FileStream(path + ".temp", FileMode.OpenOrCreate, FileAccess.Write))
            //            {
            //                if (File.Exists(path + ".temp")) File.SetAttributes(path + ".temp", FileAttributes.Hidden);
            //                int blockCount = ((int)fs.Length - 1) / decryptSize + 1;
            //                for (int i = 0; i < blockCount; i++)
            //                {
            //                    int size = decryptSize;
            //                    if (i == blockCount - 1) size = (int)(fs.Length - i * decryptSize);
            //                    byte[] bArr = new byte[size];
            //                    fs.Read(bArr, 0, size);
            //                    string dataStr = Encoding.Default.GetString(bArr);
            //                    byte[] result = ThreeDes.DecodeCBCbyte(dataStr);//加密

            //                    //byte[] result = AES.AESDecrypt(bArr, pwd);
            //                    fsnew.Write(result, 0, result.Length);
            //                    fsnew.Flush();
            //                }
            //                fsnew.Close();
            //                fsnew.Dispose();
            //            }
            //            fs.Close();
            //            fs.Dispose();
            //            FileAttributes fileAttr = File.GetAttributes(path);
            //            File.SetAttributes(path, FileAttributes.Archive);
            //            File.Delete(path);
            //            File.Move(path + ".temp", path);
            //            File.SetAttributes(path, fileAttr);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    File.Delete(path + ".temp");
            //    throw ex;
            //}
        }
        #endregion
    }

    /// <summary>
    /// AES加密解密
    /// </summary>
    public class AES
    {
        #region 加密
        #region 加密字符串
        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static string AESEncrypt(string EncryptString, string EncryptKey)
        {
            return Convert.ToBase64String(AESEncrypt(Encoding.Default.GetBytes(EncryptString), EncryptKey));
        }
        #endregion
        #region 加密字节数组
        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static byte[] AESEncrypt(byte[] EncryptByte, string EncryptKey)
        {
            if (EncryptByte.Length == 0) { throw (new Exception("明文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strEncrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strEncrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strEncrypt;
        }
        #endregion
        #endregion
        #region 解密
        #region 解密字符串
        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static string AESDecrypt(string DecryptString, string DecryptKey)
        {
            return Convert.ToBase64String(AESDecrypt(Encoding.Default.GetBytes(DecryptString), DecryptKey));
        }
        #endregion
        #region 解密字节数组
        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static byte[] AESDecrypt(byte[] DecryptByte, string DecryptKey)
        {
            if (DecryptByte.Length == 0) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strDecrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(DecryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateDecryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(DecryptByte, 0, DecryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strDecrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strDecrypt;
        }
        #endregion
        #endregion
    }
}

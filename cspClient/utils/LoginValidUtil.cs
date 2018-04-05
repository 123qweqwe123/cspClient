using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace cspClient.utils
{
   class LoginValidUtil
    {
            /// <summary>
            /// 对密码解密
            /// </summary>
            /// <param name="encodePwd"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static String encodePwd(String loginPwd, String salt)
            {
                //return encodePwd;

                char[] charArr = salt.ToCharArray();

                int len = charArr.Length;
                if ((len & 1) != 0)
                {
                    throw new Exception("Odd number of characters.");
                }
                else
                {
                    byte[] byteArr = new byte[len >> 1];
                    int i = 0;

                    for (int j = 0; j < len; ++i)
                    {
                        int f = toDigit(charArr[j], j) << 4;

                        ++j;
                        f |= toDigit(charArr[j], j);
                        ++j;
                        byteArr[i] = (byte)(f & 255);
                    }


                    var sha1 = SHA1Managed.Create();
                    sha1.TransformBlock(byteArr, 0, byteArr.Length, byteArr, 0);
                    byte[] inputBytes = Encoding.UTF8.GetBytes(loginPwd);
                    byte[] outputBytes = sha1.ComputeHash(inputBytes);

                    for (int m = 1; m < 1024; m++)
                    {
                        //sha1.TransformBlock(byteArr, 0, byteArr.Length, byteArr, 0);
                        outputBytes = sha1.ComputeHash(outputBytes);
                    }

                    return BitConverter.ToString(outputBytes).Replace("-", "").ToLower();


                }

            }

            private static int toDigit(char c, int index)
            {
                int digit = Int16.Parse(Convert.ToString(c), System.Globalization.NumberStyles.HexNumber);
                if (digit == -1)
                {
                    throw new Exception("Illegal hexadecimal character " + c + " at index " + index);
                }
                else
                {
                    return digit;
                }
            }

        public static String LoginValid(String name, String pwd)
        {

            //if (name.Equals("admin") && pwd.Equals("123"))
            //{
            //    return true;
            //}


            String sql = " Select * from sys_account where login_name = '" + name.Trim() + "' ";

            DataTable dt = DBsqliteHelper.getDataTable(sql);

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("系统不存在该用户,请校验输入");
                return "";
            }

            String salt = dt.Rows[0]["SALT"].ToString();
            String user_pwd = dt.Rows[0]["PASSWORD"].ToString();
            if (String.IsNullOrEmpty(salt) || String.IsNullOrEmpty(user_pwd))
            {
                MessageBox.Show("该用户数据缺失,请切换用户登录");
                return "";

            }
            if (LoginValidUtil.encodePwd(pwd, salt).Equals(user_pwd))
            {
                return dt.Rows[0]["NAME"].ToString();
            }
            else {
                MessageBox.Show("用户名或密码错误,请确认后重试！");
            }
                return "";
            }

        }
}

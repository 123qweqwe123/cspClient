using MySQLDriverCS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace cspClient.utils
{
    class DBsqliteHelper
    {
        #region 连接字符串
        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        /// <returns></returns>
        public static String getSQLiteConn()
        {
            return "Data Source=" + Application.StartupPath + "\\db\\db.db";  //获取绝对路径
        }
        #endregion
        private static string DBSQLConnString = "Data Source=" + Environment.CurrentDirectory + "\\db\\db.db";

        private static string server = "localhost";
        private static string database = "bio-work";
        private static string login = "root";
        private static string password = "root";
        private static int port = 50009;
        public static string getSqlResult(string sql)
        {
            //string ret = "";
            //SQLiteConnection conn = null;
            //SQLiteCommand cmd = null;
            //try
            //{
            //    conn = new SQLiteConnection(DBSQLConnString);
            //    //conn.SetPassword("ltjncrc@2016");
            //    conn.Open();//打开连接  
            //    cmd = new SQLiteCommand(conn);//实例化SQL命令
            //    cmd.CommandText = sql;
            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            //    DataTable data = new DataTable("Table");
            //    adapter.Fill(data);
            //    if (data != null && data.Rows.Count > 0 && data.Rows[0][0] != null)
            //    {
            //        ret = data.Rows[0][0].ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    HR_Log.WriteLogLine(ex);
            //}
            //finally
            //{
            //    if (cmd != null)
            //    {
            //        cmd.Dispose();
            //    }
            //    if (conn != null)
            //    {
            //        conn.Dispose();
            //        conn.Close();
            //    }
            //}
            //return ret;

            try {
                using (MySQLConnection conn = new MySQLConnection(new MySQLConnectionString(server, database, login, password, port).AsString))
                //using (MySQLConnection conn = new MySQLConnection("server=localhost;port=50009;uid=root;password=root;pooling=true;charset=utf8;database=bio-work"))
                {
                    conn.Open();
                    //防止乱码
                    MySQLCommand commn = new MySQLCommand("set names gbk", conn);
                    commn.ExecuteNonQuery();

                    MySQLCommand cmd = new MySQLCommand(sql, conn);
                    String res = cmd.ExecuteScalar().ToString();
                    if (commn != null)
                    {
                        commn.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }

                    if (conn != null)
                    {
                        conn.Dispose();
                        conn.Close();
                    }

                    return res;
                }
            }
            catch (Exception e) {
                return "";
            }

        }


        public static DataTable getDataTable(string sql)
        {
            //DataTable data = new DataTable("Table");
            //SQLiteConnection conn = null;
            //SQLiteCommand cmd = null;
            //try
            //{
            //    conn = new SQLiteConnection(DBSQLConnString);
            //    //conn.SetPassword("ltjncrc@2016");
            //    conn.Open();//打开连接  

            //    cmd = new SQLiteCommand(conn);//实例化SQL命令
            //    cmd.CommandText = sql;
            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            //    adapter.Fill(data);
            //}
            //catch (Exception ex)
            //{
            //    HR_Log.WriteLogLine(ex);
            //}
            //finally
            //{
            //    if (cmd != null)
            //    {
            //        cmd.Dispose();
            //    }
            //    if (conn != null)
            //    {
            //        conn.Dispose();
            //        conn.Close();
            //    }
            //}
            //return data;

            try {
                using (MySQLConnection conn = new MySQLConnection(new MySQLConnectionString(server, database, login, password, port).AsString))
                //using (MySQLConnection conn = new MySQLConnection("server=localhost;port=50009;uid=root;password=root;pooling=true;charset=utf8;logging=true;database=bio-work"))
                {
                    conn.Open();
                    //防止乱码
                    MySQLCommand commn = new MySQLCommand("set names gbk", conn);
                    commn.ExecuteNonQuery();

                    MySQLCommand cmd = new MySQLCommand(sql, conn);
                    MySQLDataAdapter mda = new MySQLDataAdapter(cmd);

                    //查询出的数据是存在DataTable中的，DataTable可以理解成为一个虚拟的表，DataTable中的一行为一条记录，一列为一个数据库字段  
                    DataTable dt = new DataTable();
                    mda.Fill(dt);

                    if (commn != null)
                    {
                        commn.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }

                    if (conn != null)
                    {
                        conn.Dispose();
                        conn.Close();
                    }
                    return dt;

                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int ExecuteNonQuery(string sql)
        {
            //int flag = 0;
            //SQLiteConnection conn = null;
            //SQLiteCommand cmd = null;
            //using (conn = new SQLiteConnection(DBSQLConnString))
            //{
            //    try
            //    {
            //        //conn = new SQLiteConnection(DBSQLConnString);
            //        conn.Open();//打开连接  
            //        cmd = new SQLiteCommand(conn);//实例化SQL命令  
            //        cmd.CommandText = Sql;
            //        int ret = cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        HR_Log.WriteLogLine("Sql“" + Sql + "”执行失败：" + ex.Message);
            //        flag = -1;
            //    }
            //    finally
            //    {
            //        if (cmd != null)
            //        {
            //            cmd.Dispose();
            //        }
            //        if (conn != null)
            //        {
            //            conn.Dispose();
            //            conn.Close();
            //        }
            //    }
            //}
            //return flag;


            //int affectedRows = 0;
            //using (SQLiteConnection connection = new SQLiteConnection(DBSQLConnString))
            //{
            //    using (SQLiteCommand command = new SQLiteCommand(connection))
            //    {
            //        try
            //        {
            //            connection.Open();
            //            command.CommandText = Sql;
            //            affectedRows = command.ExecuteNonQuery();
            //        }
            //        catch (Exception e) { HR_Log.WriteLogLine("数据库操作异常:"+e.Message); }
            //    }
            //}
            //return affectedRows;
            try {
                using (MySQLConnection conn = new MySQLConnection(new MySQLConnectionString(server, database, login, password, port).AsString))
                //using (MySQLConnection conn = new MySQLConnection("server=localhost;port=50009;uid=root;password=root;pooling=true;charset=utf8;logging=true;database=bio-work"))
                {

                    conn.Open();

                    //防止乱码
                    MySQLCommand commn = new MySQLCommand("set names gbk", conn);
                    commn.ExecuteNonQuery();
                    //连接语句和SQL
                    MySQLCommand cmd = new MySQLCommand(sql, conn);

                    //返回执行结果
                    try
                    {
                        int res = cmd.ExecuteNonQuery();
                        Log.WriteSqlLine("ExecuteNonQuery执行sql:" + sql + ",执行结果：" + res);
                        return res;
                    }
                    catch (Exception exp)
                    {
                        Log.WriteLogLine("执行sql异常" + exp.Message + ",sql语句：" + sql);
                        return -1;
                    }
                    finally
                    {

                        if (commn != null)
                        {
                            commn.Dispose();
                        }
                        if (cmd != null)
                        {
                            cmd.Dispose();
                        }

                        if (conn != null)
                        {
                            conn.Dispose();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception e) {
                return -1;
            }
        }


        public static int ExecuteNonQueryTrans(List<String> Sqls)
        {
            //int flag = 0;
            //int nError = 0;
            //int count = Sqls.Count();
            //if (count == 0)
            //{
            //    return flag;
            //}

            //SQLiteConnection conn = null;
            //SQLiteTransaction tran = null;
            //SQLiteCommand cmd = null;
            //try
            //{
            //    conn = new SQLiteConnection(DBSQLConnString);
            //    conn.Open();//打开连接  

            //    tran = conn.BeginTransaction();//实例化一个事务  
            //    cmd = new SQLiteCommand(conn);//实例化SQL命令 
            //    cmd.Transaction = tran;
            //    for (int i = 0; i < count; i++)
            //    {
            //        cmd.CommandText = Sqls[i];
            //        HR_Log.WriteLogLine("执行SQL语句：" + Sqls[i]);
            //        nError = i;
            //        int ret = cmd.ExecuteNonQuery();
            //        if (ret == -1)
            //        {
            //            HR_Log.WriteLogLine("执行失败语句：" + cmd.CommandText);
            //            throw new Exception("第“" + nError + "”个事务执行失败。");
            //        }
            //    }
            //    tran.Commit();
            //    flag = 1;
            //}
            //catch (Exception ex)
            //{
            //    if (tran != null)
            //    {
            //        tran.Rollback();
            //    }
            //    HR_Log.WriteLogLine(ex + "sql“" + Sqls[nError] + "”执行失败。");
            //    flag = -1;
            //}
            //finally
            //{
            //    if (tran != null)
            //    {
            //        tran.Dispose();
            //    }
            //    if (cmd != null)
            //    {
            //        cmd.Dispose();
            //    }
            //    if (conn != null)
            //    {
            //        conn.Dispose();
            //        conn.Close();
            //    }
            //}
            //return flag;


            //using (SQLiteConnection conn = new SQLiteConnection(DBSQLConnString))
            //{
            //    try { conn.Open(); }
            //    catch { throw; }
            //    using (SQLiteTransaction tran = conn.BeginTransaction())
            //    {
            //        using (SQLiteCommand cmd = new SQLiteCommand(conn))
            //        {
            //            try
            //            {
            //                foreach (var item in Sqls)
            //                {
            //                    cmd.CommandText = item;

            //                    cmd.ExecuteNonQuery();
            //                }
            //                tran.Commit();
            //            }
            //            catch (Exception e) { tran.Rollback(); HR_Log.WriteLogLine("数据库操作异常"+e.Message); }
            //        }
            //    }
            //}
            //return 0;

            try {
                using (MySQLConnection conn = new MySQLConnection(new MySQLConnectionString(server, database, login, password, port).AsString))
                //using (MySQLConnection conn = new MySQLConnection("server=localhost;port=50009;uid=root;password=root;pooling=true;charset=utf8;logging=true;database=bio-work"))
                {


                    conn.Open();
                    //using (MySQLTransaction tran = (MySQLTransaction)conn.BeginTransaction())
                    //{
                    MySQLCommand commn = new MySQLCommand("set names gbk", conn);
                    commn.ExecuteNonQuery();
                    try
                    {
                        foreach (var item in Sqls)
                        {
                            if (String.IsNullOrEmpty(item)) { continue; }
                            using (MySQLCommand cmd = new MySQLCommand(item, conn))
                            {
                                try
                                {
                                    //cmd.CommandText = item;
                                    int res = cmd.ExecuteNonQuery();
                                    Log.WriteSqlLine("执行结果:" + res + ",sql语句：" + item);
                                    if (cmd != null)
                                    {
                                        cmd.Dispose();
                                    }
                                }
                                catch (Exception e)
                                {
                                    Log.WriteLogLine("数据库操作异常" + e.Message);
                                }
                            }
                            //tran.Commit();
                            //return 1;
                        }
                    }
                    catch (Exception e)
                    {
                        //tran.Rollback();
                        Log.WriteLogLine("数据库操作异常" + e.Message); return -1;
                    }
                    finally
                    {

                        if (commn != null)
                        {
                            commn.Dispose();
                        }
                        if (conn != null)
                        {
                            conn.Dispose();
                            conn.Close();
                        }
                    }
                    //}
                    return 1;
                }
            }
            catch (Exception e) {
                return -1;
            }
        }
    }
}

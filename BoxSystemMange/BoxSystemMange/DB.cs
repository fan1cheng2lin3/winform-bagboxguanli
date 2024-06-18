using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    internal class DB
    {

        public static SqlConnection cn;//新建一个数据库连接对象

        /// <summary>
        /// 这个函数的作用是获取一个数据库连接对象。以下是它的工作流程：声明一个字符串变量 mystr，用于存储数据库连接字符串。这个字符串指定了服务器名称、数据库名称以及使用集成身份验证连接数据库的方式。检查全局变量 cn 是否为 null 或者数据库连接状态是否为关闭状态(ConnectionState.Closed)。如果 cn 是 null 或者处于关闭状态，就创建一个新的 SqlConnection 对象，使用 mystr 中指定的连接字符串进行初始化。调用 Open() 方法打开数据库连接。返回这个数据库连接对象 cn。这个函数的目的是确保在每次需要数据库连接时都返回一个可用的连接对象。如果 cn 是 null 或者处于关闭状态，它会创建一个新的连接对象并打开连接，否则直接返回现有的连接对象。这样可以避免频繁地创建和关闭数据库连接，提高了效率。
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetCn()
        {
            // 定义连接字符串
            string mystr = "server=.;database=BoxbagManageSystem;integrated security=true";

            // 确保cn不是null且连接是打开的，否则创建并打开新连接
            if (cn == null || cn.State == ConnectionState.Closed)
            {
                cn = new SqlConnection(mystr);
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    // 处理异常，例如记录日志或设置cn为null以便重试
                    MessageBox.Show("数据库连接失败：" + ex.Message);
                    cn = null;
                }
            }

            return cn;
        }


        public static DataTable GetDataSet(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }



        public static Boolean sqlEx(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                cn.Close();
                MessageBox.Show("执行失败" + ex.Message.ToString());
                return false;
            }
            return true;
        }


        public static DataTable GetDataSet(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = GetCn())
                {
                    cmd.Connection = connection;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据集失败：" + ex.Message.ToString());
            }
            return dt;
        }


        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    byte[] hash = sha256.ComputeHash(bytes);
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        stringBuilder.Append(hash[i].ToString("x2"));
                    }

                    return stringBuilder.ToString();
                }
            }
        }
    }
}

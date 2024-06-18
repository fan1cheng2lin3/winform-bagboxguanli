using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Data.OleDb;

namespace bagbox
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
            string mystr = "server=.;database=BoxbagManageSystem;integrated security=true";
            if (cn == null || cn.State == ConnectionState.Closed)
            {
                cn = new SqlConnection(mystr);
                cn.Open();
            }
            return cn;
        }

        /// <summary>
        /// 用于执行数据库查询并返回结果集。这个函数接受一个 SQL 查询字符串作为参数，并直接在函数内部构建了 SqlCommand 对象，而不是从外部传入。以下是它的工作流程：创建一个 SqlCommand 对象，将传入的 SQL 查询字符串和数据库连接(cn) 关联起来。创建一个 SqlDataAdapter 对象，将 SqlCommand 对象传入作为参数。创建一个 DataSet 对象。调用 SqlDataAdapter 对象的 Fill 方法，将数据填充到 DataSet 中。返回 DataSet 中第一个表格（通常是查询结果的唯一表格）。与之前提到的函数不同的是，这个函数返回一个 DataTable 而不是一个 DataSet。这是因为它假设查询结果只有一个表格，所以直接返回了这个表格。总的来说，这个函数的主要作用是执行数据库查询并返回结果集中的第一个表格。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataSet(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }


        /// <summary>
        /// 这个函数的作用是执行一个 SQL 语句，通常是用于执行插入、更新或删除数据库中的数据。以下是它的工作流程：创建一个 SqlCommand 对象，将传入的 SQL 语句和数据库连接(cn) 关联起来。调用 ExecuteNonQuery() 方法执行 SQL 语句，该方法用于执行没有返回结果集的 SQL 命令，例如 INSERT、UPDATE、DELETE 等。如果执行成功，关闭数据库连接，并返回 true。如果执行过程中发生 SqlException 异常，则捕获异常，关闭数据库连接，并显示一条错误消息框，消息框内容包含了异常信息。然后返回 false。这个函数的主要作用是执行 SQL 命令，并处理了可能出现的异常情况。在执行成功时返回 true，执行失败时返回 false，同时显示错误消息框。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
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

        //------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 这个功能是执行一个 SQL 查询并返回结果集（DataTable）。以下是它的工作流程：使用 SqlConnection 从数据库中获取一个连接。将传入的 SqlCommand 对象与这个连接关联。使用 SqlDataAdapter 执行 SQL 查询，并将结果填充到一个新的 DataTable 对象中。将这个填充好的 DataTable 对象返回给调用者。这个功能的主要作用是执行数据库查询，并将结果返回给调用代码，使调用者能够在应用程序中处理这些数据。如果执行查询时发生异常，它会捕获异常并显示一条错误消息框，然后返回一个空的 DataTable 对象。
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
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

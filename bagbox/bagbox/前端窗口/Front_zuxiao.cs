using bagbox.前端窗口;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static bagbox.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace bagbox
{
    public partial class Front_zuxiao : Form
    {

        
        public Front_zuxiao()
        {
            InitializeComponent();
            
        }

        private void Front_zuxiao_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOk;
            label3.Text = Backend.StrValue22;
            label4.Text = Front.StrValue2;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            if (txtPwd.Text == "")
            {
                MessageBox.Show("请输入密码");
            }
            else
            {
                DB.GetCn();
                string enteredPassword = PasswordHasher.HashPassword(txtPwd.Text);
                string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Password = @Password";
                string query2 = "SELECT * FROM Employee_Table WHERE Employee_Name = @Employee_Name AND Telephone = @Telephone";
                string query3 = "SELECT * FROM User_Table WHERE Username = @Username AND pwd = @pwd";

                // 创建 SqlCommand 对象
                SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                SqlCommand cmd2 = new SqlCommand(query2, DB.GetCn());
                SqlCommand cmd3 = new SqlCommand(query3, DB.GetCn());

                // 添加参数
                cmd.Parameters.AddWithValue("@Name", label4.Text);
                cmd2.Parameters.AddWithValue("@Employee_Name", label3.Text);
                cmd3.Parameters.AddWithValue("@Username", label3.Text);
                cmd.Parameters.AddWithValue("@Password", enteredPassword);
                cmd2.Parameters.AddWithValue("@Telephone", txtPwd.Text);
                cmd3.Parameters.AddWithValue("@pwd", txtPwd.Text);

                // 使用 SqlCommand 对象执行查询
                DataTable dt = DB.GetDataSet(cmd);
                DataTable dt2 = DB.GetDataSet(cmd2);
                DataTable dt3 = DB.GetDataSet(cmd3);
                if (dt.Rows.Count > 0)
                {
                    DB.GetCn();
                    string sdr = "DELETE FROM customer_Table WHERE Name = '" + label4.Text + "';";
                    DB.sqlEx(sdr);
                    MessageBox.Show("注销成功");
                    this.Close();
                }
                else if (dt2.Rows.Count > 0)
                {
                    DB.GetCn();
                    string sdr2 = "DELETE FROM Employee_Table WHERE Employee_Name = '" + label3.Text + "';";
                    DB.sqlEx(sdr2);
                    MessageBox.Show("注销成功");
                    this.Close();
                }
                else if(dt3.Rows.Count > 0)
                {
                    DB.GetCn();
                    string sdr3 = "DELETE FROM User_Table WHERE Username = '" + label3.Text + "';";
                    DB.sqlEx(sdr3);
                    MessageBox.Show("注销成功");
                    this.Close();

                }
                else
                {
                    MessageBox.Show("密码错误");
                }

            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPwd.PasswordChar = '\0';
            }
            else
            {
                txtPwd.PasswordChar = '*';
            }
        }
    }
}

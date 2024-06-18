using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace bagbox
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            string query = "SELECT * FROM customer_Table WHERE Name = @Name";
            SqlCommand cmd2 = new SqlCommand(query, DB.GetCn());
            cmd2.Parameters.AddWithValue("@Name", txtName.Text);
            DataTable dt = DB.GetDataSet(cmd2);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("此用户名已存在，请重新输入");
            }
            else if (Regex.IsMatch(txtPwd.Text, @"[\u4e00-\u9fa5]") || txtPwd.Text.Length < 6)
            {
                
                txtPwd.Clear();
                MessageBox.Show("不能包含中文且6位数以上");

            }
            else if (!Regex.IsMatch(txtEmail.Text, pattern))
            {
                txtEmail.Clear();
                MessageBox.Show("邮箱格式错误，请输入正确的邮箱");
            }
            
            else
            {
                if (txtName.Text == "" || txtPwd.Text == "" || txtEmail.Text == "")
                {
                    MessageBox.Show("请输入账号密码和邮箱");

                }
                else
                {
                    
                        string hashedPassword = DB.PasswordHasher.HashPassword(txtPwd.Text);
                        string sdr = "SET IDENTITY_INSERT customer_Table ON; " +
                                     "insert into customer_Table(Customerld, Name, Password, Email) values " +
                                    "(IDENT_CURRENT('customer_Table') + 1, @Name, @Password, @Email);" +
                                    "SET IDENTITY_INSERT customer_Table OFF;";

                        SqlCommand cmd = new SqlCommand(sdr, DB.GetCn());
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        DB.GetDataSet(cmd);
                        MessageBox.Show("注册成功");
                        this.Close();
                    
                }
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
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

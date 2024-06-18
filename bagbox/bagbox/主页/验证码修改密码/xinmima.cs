using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static bagbox.DB;

namespace bagbox
{
    public partial class xinmima : Form
    {
        public xinmima()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.GetCn();
            string enteredPassword = PasswordHasher.HashPassword(txtNewPwd.Text);
            string enteredPassword2 = txtPwd.Text;
            string hashedEnteredPassword = PasswordHasher.HashPassword(enteredPassword2);

            string query = "SELECT Password FROM customer_Table WHERE Name = @Name";
            SqlCommand cmd2 = new SqlCommand(query, DB.GetCn());
            cmd2.Parameters.AddWithValue("@Name", mimazhaohui.a);
            DataTable dt = DB.GetDataSet(cmd2);
            string passwordFromDatabase = dt.Rows[0]["Password"].ToString();


            if (txtPwd.Text == "")
            {
                MessageBox.Show("请输入密码");
                

            }
           
            else if(hashedEnteredPassword == passwordFromDatabase)
            {
                MessageBox.Show("密码与前密码相同");
            }
            else if (txtNewPwd.Text == txtPwd.Text)
            {

                DB.GetCn();
                string sdr = "UPDATE customer_Table SET Password = @NewPwd WHERE Name = @Name";
                // 创建 SqlCommand 对象
                SqlCommand cmd = new SqlCommand(sdr, DB.GetCn());
                cmd.Parameters.AddWithValue("@NewPwd", enteredPassword);
                cmd.Parameters.AddWithValue("@Name", mimazhaohui.a);
                DB.GetDataSet(cmd);
                MessageBox.Show("修改成功");
                this.Close();
                
            }
            else
            {
                MessageBox.Show("请再次输入密码");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xinmima_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

      

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txtNewPwd.PasswordChar = '\0';
                txtPwd.PasswordChar = '\0';
            }
            else
            {
                txtPwd.PasswordChar = '*';
                txtNewPwd.PasswordChar = '*';
            }
        }
    }
}

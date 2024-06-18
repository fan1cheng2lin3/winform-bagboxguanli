using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static bagbox.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace bagbox
{
    public partial class Front_xiugaimima : Form
    {
        public Front_xiugaimima()
        {
            InitializeComponent();
        }

        

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            label4.Text = Login.StrValue;
            if (txtPwd.Text == "" )
            {
                MessageBox.Show("请输入正确密码");

            }
            
            else if(txtNewPwd.Text == "")
            {
                MessageBox.Show("请输入新密码");
            }
            else if (txtNewPwd.Text == txtPwd.Text)
            {
                MessageBox.Show("密码与原密码相同，请重新输入密码");
                return;
            }
            else
            {
                DB.GetCn();
                string enteredPassword1 = PasswordHasher.HashPassword(txtPwd.Text);
                string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                cmd.Parameters.AddWithValue("@Password", enteredPassword1);
                cmd.Parameters.AddWithValue("@Name", label4.Text);
                DataTable dt = DB.GetDataSet(cmd);

                if (dt.Rows.Count > 0)
                {
                    DB.GetCn();
                    string enteredPassword = PasswordHasher.HashPassword(txtNewPwd.Text);
                    string sdr = "UPDATE customer_Table SET Password = @Password where Name = @Name ";
                    SqlCommand cmd3 = new SqlCommand(sdr, DB.GetCn());
                    cmd3.Parameters.AddWithValue("@Password", enteredPassword);
                    cmd3.Parameters.AddWithValue("@Name", label4.Text);

                    DB.GetDataSet(cmd3);
                    MessageBox.Show("修改成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或者密码错误");
                }
            }
                
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtNewPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Front_xiugaimima_Load(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = Login.StrValue11;
            this.AcceptButton = button1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
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


using bagbox.前端窗口;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace bagbox
{
    public partial class Backend_xiugaimima : Form
    {
        public Backend_xiugaimima()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text == "")
            {
                MessageBox.Show("请输入正确密码");
            }
            else if (txtNewPwd.Text == "")
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
                string queryUser = "SELECT * FROM User_Table WHERE Username = '" + label4.Text + "' AND pwd = '" + txtPwd.Text + "'";
                string queryEmployee = "SELECT * FROM Employee_Table WHERE Employee_Name = '" + label4.Text + "'";
                DataTable dtUser = DB.GetDataSet(queryUser);
                DataTable dtEmployee = DB.GetDataSet(queryEmployee);

                if (dtUser.Rows.Count == 0 && dtEmployee.Rows.Count == 0)
                {
                    MessageBox.Show("此用户名不存在，请重新输入");
                }
                else
                {
                    if (dtUser.Rows.Count > 0)
                    {
                        string updateQuery = "UPDATE User_Table SET pwd = '" + txtNewPwd.Text + "' WHERE Username = '" + label4.Text + "'";
                        DB.sqlEx(updateQuery);
                    }
                    else if (dtEmployee.Rows.Count > 0)
                    {
                        string updateQuery = "UPDATE Employee_Table SET Telephone = '" + txtNewPwd.Text + "' WHERE Employee_Name = '" + label4.Text + "'";
                        DB.sqlEx(updateQuery);
                    }

                    MessageBox.Show("修改成功");
                    this.Close();
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

        private void Backend_xiugaimima_Load(object sender, EventArgs e)
        {
            label4.Text = Login.StrValue11;
            this.AcceptButton = button1;
        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

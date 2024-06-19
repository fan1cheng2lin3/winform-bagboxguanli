using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSystemMange.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace BoxSystemMange
{
    public partial class Login : Form
    {

        public static string StrValue = string.Empty;
        public static int CustomerId;
        public static string Employee_Id = " ";
        public static bool Dflag = false;//false:是昔通员工，true是销售部员工
        public static bool Bflag = false;//false:是昔通员工，true是管理员
        public static bool iflogin = false;//false:是昔通员工，true是管理员

        public delegate void HidePanelDelegate();
        public HidePanelDelegate HidePanelHandler; // 声明一个委托变量




        public delegate void HidePanel();
        public HidePanel HidePanebbb; // 声明一个委托变量

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "" || txtPwd.Text == "")
            {
                MessageBox.Show("请输入账号密码");
            }
            else
            {
                // 执行查询并验证用户信息是否匹配
                DB.GetCn();

                if (radioButton1.Checked)
                {
                    string enteredPassword = PasswordHasher.HashPassword(txtPwd.Text);
                    string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Password", enteredPassword);
                    DataTable dt = DB.GetDataSet(cmd);



                    if (dt.Rows.Count > 0)
                    {

                        //Form1.Aflog = true;

                        Name = txtName.Text;
                        StrValue = txtName.Text;
                        CustomerId = Convert.ToInt32(dt.Rows[0]["Customerld"]);
                        HidePanebbb?.Invoke();
                        iflogin = true;
                        this.Close();

                        



                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }


                }

                if (radioButton2.Checked)
                {
                    string query = "SELECT * FROM Employee_Table WHERE Employee_Name = @Employee_Name AND Telephone = @Telephone";
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                    cmd.Parameters.AddWithValue("@Employee_Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Telephone", txtPwd.Text);
                    DataTable dt = DB.GetDataSet(cmd);

                    if (dt.Rows.Count > 0)
                    {

                        //MessageBox.Show("登录成功");
                        Name = txtName.Text;
                        Dflag = true;
                        Bflag = false;
                        Employee_Id = Convert.ToString(dt.Rows[0]["Employee_ID"]);


                        StrValue = txtName.Text;
                        HidePanebbb?.Invoke();

                        this.Close();

                        iflogin = true;

                        //Form1.Aflog = true;
                        //Form1.flag = 2;



                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }
                }

                if (radioButton3.Checked)
                {
                    string query = "SELECT * FROM User_Table WHERE Username = @Username AND pwd = @pwd";
                    SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                    cmd.Parameters.AddWithValue("@Username", txtName.Text);
                    cmd.Parameters.AddWithValue("@pwd", txtPwd.Text);
                    DataTable dt = DB.GetDataSet(cmd);
                    if (dt.Rows.Count > 0)
                    {

                        //MessageBox.Show("登录成功");
                        Name = txtName.Text;
                        //Form1.Aflog = true;
                        //Form1.flag = 3;
                        HidePanebbb?.Invoke();
                        Dflag = false;
                        Bflag = true;

                        iflogin = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }
                }


            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            HidePanelHandler?.Invoke();
            
        }



        private void label5_Click(object sender, EventArgs e)
        {
            
        }
    }
}

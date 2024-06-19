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
    public partial class houtaidenglu : Form
    {

        public static string StrValue = string.Empty;
        public static string StrValue11 = string.Empty;
        public static int CustomerId;
        public static string Employee_Id = " ";
        public static bool Dflag = false;//false:是昔通员工，true是销售部员工
        public static bool Bflag = false;//false:是昔通员工，true是管理员
        public static bool ifdelu = false;//false:是昔通员工，true是管理员

        public houtaidenglu()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPwd.Text == "")
            {
                MessageBox.Show("请输入账号密码");
            }
            else
            {
                // 执行查询并验证用户信息是否匹配
                DB.GetCn();

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
                        this.Close();

                       

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

                        this.Visible = false;
                        this.Close();
                        Dflag = false;
                        Bflag = true;
                        houtaichuangti t1 = new houtaichuangti();
                        t1.ShowDialog();

                        ifdelu = true;


                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误");
                    }
                }
            }
        }

        private void houtaidenglu_Load(object sender, EventArgs e)
        {

        }
    }
}

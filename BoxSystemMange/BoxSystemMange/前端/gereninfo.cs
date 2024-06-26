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
using static BoxSystemMange.Login;


namespace BoxSystemMange
{
    public partial class gereninfo : Form
    {

        zhuye form1;
        public static string path_source = "";
        private string name;


        public delegate void HidePanelDelegate();
        public HidePanelDelegate HidePanelHandler;

        public gereninfo(zhuye form1)
        {
            InitializeComponent();

            this.form1 = form1;

            // 订阅Form1的事件
            this.form1.UpdatePictureBoxEvent += Form1_UpdatePictureBoxEvent;

            name = Login.bbb;
        }


        private void Form1_UpdatePictureBoxEvent(Image image, string labelText)
        {
            // 这里使用Form1的pictureBox1控件

            
                form1.pictureBox1.Image = image;
                form1.label3.Text = labelText;
            
        }

        private void gereninfo_Load(object sender, EventArgs e)
        {
            //LoadDataFromDatabase();
            panel8.Visible = false;
            panel8.Dock = DockStyle.Fill;
            inittouxiang();
        }

        public void inittouxiang()
        {
            DB.GetCn();

            string query = "SELECT [touxiang] FROM [customer_Table] WHERE [Customerld] = '" + name + "'";

            DataTable result = DB.GetDataSet(query);

            if (result.Rows.Count > 0)
            {
                string a = result.Rows[0]["touxiang"].ToString();
                pictureBox1.Image = System.Drawing.Image.FromFile(a);

            }

            DB.cn.Close();
        }

        public string InitData(string columnName)
        {
            DB.GetCn();
            string query = "SELECT " + columnName + " FROM customer_Table WHERE Customerld = '" + name + "'";
            DataTable result = DB.GetDataSet(query);
            string re = "";
            if (result.Rows.Count > 0)
            {
                re = result.Rows[0][columnName].ToString().Trim();
            }
            DB.cn.Close();

            return re;
        }


        public void initupdata(string columnName, string columnName2)
        {

            DB.GetCn();
            string updateQuery = "UPDATE customer_Table SET " + columnName + " = '" + columnName2 + "' WHERE Customerld = '" + name + "'";
            DB.sqlEx(updateQuery);
            DB.cn.Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            //名称
            panel8.Visible = true;
            label18.Text = "名字";
            textBox1.Text = InitData("Name");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label18.Text == "名字")
            {

                string query = "SELECT * FROM customer_Table WHERE Name = @Name";
                SqlCommand cmd2 = new SqlCommand(query, DB.GetCn());
                cmd2.Parameters.AddWithValue("@Name", textBox1.Text);
                DataTable dt = DB.GetDataSet(cmd2);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("此用户名已存在，请重新输入");
                }
                else
                {
                    initupdata("Name", textBox1.Text.Trim());
                    form1.label3.Text = textBox1.Text;

                }

            }
            else if (label18.Text == "邮箱")
            {
                initupdata("Email", textBox1.Text.Trim());
                MessageBox.Show("已更换邮箱");
            }
           
            else if (label18.Text == "")
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel8.Visible=false;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            panel8.Visible = true;
            label18.Text = "邮箱";
            textBox1.Text = InitData("email");
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                path_source = dlg.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(path_source);

                DB.GetCn();
                string updateQuery = "UPDATE customer_Table SET touxiang = '" + path_source + "' WHERE Customerld = '" + name + "'";
                DB.sqlEx(updateQuery);
                DB.cn.Close();

                genxin();

            }
        }

        public void genxin()
        {
            Image image = Image.FromFile(InitData("touxiang"));
            form1.OnUpdatePictureBox(image, InitData("Name"));
        }


        public static bool isSuccess = true;

        private void button1_Click(object sender, EventArgs e)
        {

            Login.iflogin = false;
            Login.bbb = "";
            Login.StrValue = "";

            form1.label3.Text = "请登录";
            form1.pictureBox1.Image = Image.FromFile(@"C:\Users\Administrator\Pictures\头像.jpg");



            HidePanelHandler?.Invoke();
            this.Close();
        }
    }
}

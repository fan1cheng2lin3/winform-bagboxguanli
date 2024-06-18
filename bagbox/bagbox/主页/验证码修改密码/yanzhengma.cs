using bagbox.前端窗口;
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
using System.Xml.Linq;

namespace bagbox
{
    public partial class yanzhengma : Form
    {
        public yanzhengma()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textCaptcha.Text == "") {
                MessageBox.Show("请输入验证码");
            }
            else
            {
                DB.GetCn();
                string query = "SELECT * FROM customer_Table WHERE  Name = @Name and Captcha = @Captcha ";
                SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                cmd.Parameters.AddWithValue("@Captcha", textCaptcha.Text);
                cmd.Parameters.AddWithValue("@Name", mimazhaohui.a);
                DataTable dt = DB.GetDataSet(cmd);

                if (dt.Rows.Count > 0)
                {
                    xinmima t1 = new xinmima();
                    t1.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("验证码错误");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yanzhengma_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }
    }
}

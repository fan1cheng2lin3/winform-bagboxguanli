using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange.管理
{
    public partial class houtaizhuye : Form
    {
        public houtaizhuye()
        {
            InitializeComponent();
        }

        public static Form frm;

        private void houtaizhuye_Load(object sender, EventArgs e)
        {

            if (frm == null || string.IsNullOrEmpty(frm.Text))
            {
                frm = new zhuye();
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
            }

            panel2.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.Fill;
            flowLayoutPanel2.Dock = DockStyle.Fill;


            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 设置窗体边框样式
            this.MaximizeBox = false; // 禁用最大化按钮
            this.MinimizeBox = false; // 禁用最小化按钮
            this.ResizeRedraw = false; // 禁止重绘操作
            this.ClientSize = new Size(this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            this.ClientSize = new Size(this.ClientSize.Width + 1, this.ClientSize.Height + 1);
        }

        public void Zichuangti(Form childForm)
        {
            // 假设 panel5 和 zhuyepanel 是已经定义好的控件

            panel2.BringToFront();
            panel2.Visible = true;
            // childForm 是函数参数，代表要添加的子窗体
            childForm.TopLevel = false; // 设置子窗体为非顶层窗体
            childForm.Dock = DockStyle.Fill; // 让子窗体填充整个面板
            childForm.FormBorderStyle = FormBorderStyle.None; // 可选：去除子窗体的边框
            childForm.WindowState = FormWindowState.Maximized;
            flowLayoutPanel2.Controls.Add(childForm); // 将子窗体添加到面板中
            childForm.Show(); // 显示子窗体

            // 关闭 zhuyeflow 面板中的其它子窗体
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                {
                    form.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            houtaichuangti t1 = new houtaichuangti();
            Zichuangti(t1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            Select_Log t1 = new Select_Log();
            Zichuangti(t1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            departmentguanli t1 = new departmentguanli();
            Zichuangti(t1);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            houtaicommunity t1 = new houtaicommunity();
            Zichuangti(t1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            houtaidingdan t1 = new houtaidingdan();
            Zichuangti(t1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            houtaixinxi t1 = new houtaixinxi();
            Zichuangti(t1);
        }
    }
}

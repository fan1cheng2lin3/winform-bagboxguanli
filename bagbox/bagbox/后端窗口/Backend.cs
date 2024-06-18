using bagbox.商品管理;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bagbox
{
    public partial class Backend : Form
    {
        public static string StrValue22 = string.Empty;

        public Backend()
        {
            InitializeComponent();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backend_xiugaimima t1 = new Backend_xiugaimima();
            t1.ShowDialog();
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 注销账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StrValue22 = Login.StrValue11;
            Front_zuxiao t1 = new Front_zuxiao();
            t1.ShowDialog();
        }

        private void Backend_Load(object sender, EventArgs e)
        {

        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zhuye t1 = new zhuye();
            t1.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 增加商品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert_product t1= new Insert_product();
            t1.ShowDialog();
        }

        private void 修改商品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update_product t1 = new Update_product();
            t1.ShowDialog();
        }

        private void shanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_product t1 = new delete_product();
            t1.ShowDialog();
        }

        private void 审核订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check_Order c1 = new Check_Order();
            c1.ShowDialog();
        }

        private void 查看日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Select_Order c1 = new Select_Order();
            c1.ShowDialog();
        }
    }
}

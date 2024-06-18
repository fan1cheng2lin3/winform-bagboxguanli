using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace bagbox.前端窗口
{

    
    public partial class Front : Form
    {
        public static string StrValue2 = string.Empty;

        public Front()
        {
            InitializeComponent();
            
        }
        private string userName;
        public Front(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Front_xiugaimima t1 = new Front_xiugaimima();
            t1.ShowDialog();
            this.Visible = false;
        }

        
        private void 注销账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            StrValue2 = Login.StrValue;




            Front_zuxiao t3 = new Front_zuxiao();
            t3 .ShowDialog();
            
            

        }
        
        private void Front_Load(object sender, EventArgs e)
        {
            
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

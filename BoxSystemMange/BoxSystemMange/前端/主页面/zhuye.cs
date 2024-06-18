using BoxSystemMange.脚本类;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class zhuye : Form
    {
        public static string StaticNewIdentifier = "";
        private Timer timer;
        private new AutoAdaptWindowsSize AutoSize;
        private Point mPoint;
        private Timer slideTimer;
        private AutoAdaptWindowsSize autoAdaptWindowsSize;

        public zhuye()
        {
            InitializeComponent();
            UpdateLabels();


            //InitializeSlidePanel();
            //InitializeTimer();

            this.IsMdiContainer = true; // 设置主窗体为MDI容器
            autoAdaptWindowsSize = new AutoAdaptWindowsSize(this);
            this.SizeChanged += new EventHandler(zhuye_SizeChanged);// 添加窗体大小变化事件处理
        }



        
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            // 更新 label1 显示当前的小时和分钟
            label1.Text = DateTime.Now.ToString("HH:mm:ss");

            // 更新 label2 显示当前的星期几
            label2.Text = DateTime.Now.ToString("dddd");
        }
        private void InitializeSlidePanel()
        {
            // 设置面板初始位置（隐藏在窗体下方）
            //panel4.Location = new System.Drawing.Point(0, this.ClientSize.Height);
            //panel4.Visible = false; // 初始时面板不可见
        }

         

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }


        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        { 
            
             
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }

        }


        private void SetFlowLayoutPanelHeightToContainInnerFlowLayout(FlowLayoutPanel outerFlowLayout)
        {
            // 确保外层FlowLayoutPanel中有控件
            if (outerFlowLayout.Controls.Count > 0)
            {
                // 获取最后一个控件，假设是flowLayoutPanel6
                Control innerFlowLayout = outerFlowLayout.Controls[outerFlowLayout.Controls.Count - 1];

                // 检查是否是我们要找的flowLayoutPanel6
                if (innerFlowLayout is FlowLayoutPanel && innerFlowLayout.Name == "flowLayoutPanel6")
                {
                    // 计算所需的高度：flowLayoutPanel6的Y坐标加上其高度
                    int requiredHeight = innerFlowLayout.Location.Y + innerFlowLayout.Height;

                    // 设置外层FlowLayoutPanel的高度
                    outerFlowLayout.Height = requiredHeight;
                }
            }
        }


        private void zhuye_Load(object sender, EventArgs e)
        {

            LoadDataFromDatabase(flowLayoutPanel3, "优惠");
            LoadDataFromDatabase(flowLayoutPanel1, "推荐");
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "1");

            
            AutoSize = new AutoAdaptWindowsSize(this);
            zhuyepanel.Visible = false;
            zhuyeflow.Dock = DockStyle.Fill;
            zhuyepanel.Dock = DockStyle.Fill;
            panel5.Dock = DockStyle.Fill;

            // 重新计算行数并调整大小
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width + panel4.Width-200, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + panel4.Width-200 , 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width + panel4.Width-200 , 350 * CalculateRowCount(flowLayoutPanel6));

            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);

        }



        private void zhuye_StyleChanged(object sender, EventArgs e)
        {

        }



        private void zhuye_SizeChanged(object sender, EventArgs e)
        {


            if (AutoSize != null) // 一定加这个判断，电脑缩放布局不是100%的时候，会报错
            {
                AutoSize.FormSizeChanged();
            }

            //autoAdaptWindowsSize.FormSizeChanged();  // 当窗体大小变化时，调用适配方法
            // 获取当前窗口的大小
            int currentWidth = this.Width;
            int currentHeight = this.Height;

            // 当窗体大小变化时，调用适配方法，只有当窗体大小的x轴和y轴分别大于等于1387和885时，才调用方法
            if (currentWidth <= 2000 || currentHeight <= 885)
            {
                autoAdaptWindowsSize?.FormSizeChanged();
            }

            

            panel4.Visible = false;
            button5.Visible = false;
            button4.Visible = true;

            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {

                zhuye.StaticNewIdentifier = pictureBox.Tag.ToString();


                Tproducts t1 = new Tproducts();
                Zichuangti(t1);

            }
        }

        private void LoadDataFromDatabase2(FlowLayoutPanel flowLayoutPanel, string type,string Classification)//string aaa
        {
            DB.GetCn();

            flowLayoutPanel.Controls.Clear();

            int maskYH = 1; // 优惠
            int maskTJ = 2; // 推荐
            int maskFL = 4; // 分类

            // 根据输入的类型确定位掩码的组合
            int combinedMask = 0;
            if (type.Contains("优惠"))
            {
                combinedMask |= maskYH;
            }
            if (type.Contains("推荐"))
            {
                combinedMask |= maskTJ;
            }
            if (type.Contains("分类"))
            {
                combinedMask |= maskFL;
            }

            // 查询数据库
            string sql = "SELECT * FROM Product_Table WHERE (leixing & " + combinedMask + ") = " + combinedMask + "and Classification_ID = '"+ Classification+"'";    
            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                // 创建一个新的Panel来代替Image控件
                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(190, 332);
                imagePanel.BackColor = Color.Transparent;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = row["image"].ToString();
                pictureBox.BackColor = Color.White;
                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["Goods_ID"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);


                //// 创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["Price"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = "￥" + labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 18, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                imagePanel.Controls.Add(label1);
                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
        }


        private void LoadDataFromDatabase(FlowLayoutPanel flowLayoutPanel,string type)//string aaa
        {
            DB.GetCn();
            
            flowLayoutPanel.Controls.Clear();
            
            int maskYH = 1; // 优惠
            int maskTJ = 2; // 推荐
            int maskFL = 4; // 分类

            // 根据输入的类型确定位掩码的组合
            int combinedMask = 0;
            if (type.Contains("优惠"))
            {
                combinedMask |= maskYH;
            }
            if (type.Contains("推荐"))
            {
                combinedMask |= maskTJ;
            }
            if (type.Contains("分类"))
            {
                combinedMask |= maskFL;
            }

            // 查询数据库
            string sql = "SELECT * FROM Product_Table WHERE (leixing & " + combinedMask + ") = " + combinedMask;         // 替换为您的表名和列名
            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                // 创建一个新的Panel来代替Image控件
                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(190, 332);
                imagePanel.BackColor = Color.Transparent;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = row["image"].ToString();
                pictureBox.BackColor = Color.White;
                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["Goods_ID"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);

                //// 创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["Price"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = "￥"+labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily,18,FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                imagePanel.Controls.Add(label1);
                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
        }

        private void LoadDataFromDatabase3(FlowLayoutPanel flowLayoutPanel)//string aaa
        {
            DB.GetCn();

            flowLayoutPanel.Controls.Clear();
            // 查询数据库
            string sql = "SELECT * FROM Product_Table" ;         // 替换为您的表名和列名
            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                // 创建一个新的Panel来代替Image控件
                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(190, 332);
                imagePanel.BackColor = Color.Transparent;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = row["image"].ToString();
                pictureBox.BackColor = Color.White;
                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["Goods_ID"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);

                //// 创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["Price"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = "￥" + labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 18, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                imagePanel.Controls.Add(label1);
                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zhuyepanel.Visible = false;
            panel5.AutoScrollPosition = new Point(0, 0);

            
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);

            }

        }

        public void daxiao(FlowLayoutPanel flowLayoutPanel1,FlowLayoutPanel flowLayoutPanel3,FlowLayoutPanel flowLayoutPanel6)
        {
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width, flowLayoutPanel3.Height);
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width, flowLayoutPanel6.Height);

            // 重新计算行数并调整大小
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width, 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width, 350 * CalculateRowCount(flowLayoutPanel6));

            
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 变小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {

            panel4.Visible = true;
            button5.Visible = true;
            button4.Visible = false;

            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width - panel4.Width, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width - panel4.Width, 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width - panel4.Width, 350 * CalculateRowCount(flowLayoutPanel6));

            panel5.AutoScrollPosition = new Point(0, 0);
        }

        /// <summary>
        /// 定制面板出现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

            panel4.Visible = false;
            button5.Visible = false;
            button4.Visible = true;
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width + panel4.Width, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + panel4.Width, 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width + panel4.Width, 350 * CalculateRowCount(flowLayoutPanel6));


            panel5.AutoScrollPosition = new Point(0, 0);
        }

        // 方法：计算行数
        /// <summary>
        /// 
        /// 定制面板消失
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private int CalculateRowCount(FlowLayoutPanel panel)
        {
            int rowCount = 0;
            int previousTop = -1;

            foreach (Control control in panel.Controls)
            {
                if (control.Top != previousTop)
                {
                    // 新的一行开始
                    rowCount++;
                    previousTop = control.Top;
                }
            }

            return rowCount;
        }



        private void button12_Click(object sender, EventArgs e)
        {

        }


        private void button6_Click(object sender, EventArgs e)
        {
            zhuyepanel.Visible = false;


            panel5.AutoScrollPosition = new Point(0, 0);

            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }



        private void button13_Click(object sender, EventArgs e)
        {

            ShopCart t1 = new ShopCart();
            Zichuangti(t1);

        }


        public void Zichuangti(Form childForm)
        {
            // 假设 panel5 和 zhuyepanel 是已经定义好的控件
            panel5.AutoScrollPosition = new Point(0, 0);



            flowLayoutPanel5.Height = 1500;
            zhuyepanel.BringToFront();
            zhuyepanel.Visible = true;
            // childForm 是函数参数，代表要添加的子窗体
            childForm.TopLevel = false; // 设置子窗体为非顶层窗体
            childForm.Dock = DockStyle.Fill; // 让子窗体填充整个面板
            childForm.FormBorderStyle = FormBorderStyle.None; // 可选：去除子窗体的边框
            zhuyeflow.Controls.Add(childForm); // 将子窗体添加到面板中
            childForm.Show(); // 显示子窗体

            // 关闭 zhuyeflow 面板中的其它子窗体
            foreach (Control control in zhuyeflow.Controls)
            {
                if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                {
                    form.Close();
                }
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            dindan t1 = new dindan();
            Zichuangti(t1);
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            community t1 = new community();
            Zichuangti(t1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            information t1 = new information();
            Zichuangti(t1);
        }

        private void zhuye_Resize(object sender, EventArgs e)
        {
            panel5.AutoScrollPosition = new Point(0, 0);
        }

        private void zhuye_ResizeEnd(object sender, EventArgs e)
        {


            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }





        //没用的
        private void timer2_Tick(object sender, EventArgs e)
        {
            //int step = 40; // 每次移动的步长（像素）

            //if (isPanelVisible)
            //{
            //    // 滑动显示面板
            //    if (panel4.Location.Y > this.ClientSize.Height - panel4.Height)
            //    {
            //        panel4.Location = new System.Drawing.Point(0, panel4.Location.Y - step);
            //    }
            //    else
            //    {
            //        panel4.Location = new System.Drawing.Point(0, this.ClientSize.Height - panel4.Height);
            //        slideTimer.Stop(); // 滑动完成，停止计时器
            //    }
            //}
            //else
            //{
            //    // 滑动隐藏面板
            //    if (panel4.Location.Y < this.ClientSize.Height)
            //    {
            //        panel4.Location = new System.Drawing.Point(0, panel4.Location.Y + step);
            //    }
            //    else
            //    {
            //        panel4.Location = new System.Drawing.Point(0, this.ClientSize.Height);
            //        panel4.Visible = false;
            //        slideTimer.Stop(); // 滑动完成，停止计时器
            //    }
            //}
        }

        private void InitializeTimer()
        {
            // 初始化 Timer 控件
            timer = new Timer
            {
                Interval = 1000 // 设置间隔为1分钟
            };
            timer.Tick += timer1_Tick;
            timer.Start();


            slideTimer = new Timer
            {
                Interval = 10 // 设置间隔为10毫秒，以实现平滑动画
            };
            slideTimer.Tick += timer2_Tick;


        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "1");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "2");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "4");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "5");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase2(flowLayoutPanel6, "分类", "3");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase3(flowLayoutPanel6);
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login t1 = new Login();
            t1.ShowDialog();

        }
    }
}

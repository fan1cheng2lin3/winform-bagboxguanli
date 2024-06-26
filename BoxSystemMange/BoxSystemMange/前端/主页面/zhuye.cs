using BoxSystemMange.前端.主页面;
using BoxSystemMange.管理;
using BoxSystemMange.脚本类;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSystemMange.Login;


namespace BoxSystemMange
{
    public partial class zhuye : Form
    {
        public static string StaticNewIdentifier = "";
        private new AutoAdaptWindowsSize AutoSize;
        private Point mPoint;
        private AutoAdaptWindowsSize autoAdaptWindowsSize;
        public Point originalAutoScrollMinSize;
        private bool button6ClickedOnce = false;
        private int clickCount = 0;
        public bool dengluqingkuang = false;
        public static string selectedGoodsID;

        // 定义一个委托类型
        public delegate void UpdatePictureBoxEventHandler(Image image, string labelText);
        // 定义一个事件，使用上面的委托类型
        public event UpdatePictureBoxEventHandler UpdatePictureBoxEvent;

        private string currentPriceSortOrder = "";
        private string currentXiaoliangSortOrder = "";
        private string currentHaopinglvSortOrder = "";

        public zhuye()
        {

            InitializeComponent();
            UpdateLabels();

            this.KeyPreview = true;
            this.IsMdiContainer = true; // 设置主窗体为MDI容器
            autoAdaptWindowsSize = new AutoAdaptWindowsSize(this);
            this.SizeChanged += new EventHandler(zhuye_SizeChanged);// 添加窗体大小变化事件处理
            this.KeyDown += new KeyEventHandler(zhuye_KeyDown);

        }


        private void zhuye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 禁止默认的处理（比如换行）
                button12.PerformClick();
            }
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

        /// <summary>
        /// 计算flowLayoutPanel5的高度传
        /// </summary>
        /// <param name="outerFlowLayout"></param>
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

        private void SetFlowLayoutPanelHeightToContainInnerFlowLayout2(FlowLayoutPanel outerFlowLayout)
        {
            // 确保外层FlowLayoutPanel中有控件
            if (outerFlowLayout.Controls.Count > 0)
            {
                // 获取最后一个控件，假设是flowLayoutPanel6
                Control innerFlowLayout = outerFlowLayout.Controls[outerFlowLayout.Controls.Count - 1];

                // 检查是否是我们要找的flowLayoutPanel6
                if (innerFlowLayout is FlowLayoutPanel && innerFlowLayout.Name == "flowLayoutPanel4")
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
            init();
            
        }

        public void init()
        {


            timer1.Interval = 1000; // 更新频率为每秒
            timer1.Tick += new EventHandler(timer1_Tick); // 绑定 Tick 事件
            timer1.Start(); // 启动定时器

            LoadDataFromDatabase4(flowLayoutPanel3, "优惠");
            LoadDataFromDatabase4(flowLayoutPanel1, "推荐");
            LoadDataFromDatabase4(flowLayoutPanel6);

            AutoSize = new AutoAdaptWindowsSize(this);
            zhuyepanel.Visible = false;
            zhuyepanel2.Visible = false;

            zhuyepanel.Dock = DockStyle.Fill;
            zhuyepanel2.Dock = DockStyle.Fill;


            zhuyeflow.Dock = DockStyle.Fill;
            zhuyeflow2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Dock = DockStyle.Fill;

            panel5.Dock = DockStyle.Fill;
            panel11.Dock = DockStyle.Fill;

            // 重新计算行数并调整大小
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel6));

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

        
        private void pictureBox_Click(object sender, EventArgs e)
        {
            if(pingxing == false)
            {

                textBox1.Visible = false;
                panel14.Visible = true;
                button6.Visible = true;
                button35.Visible = false;
                if (sender is PictureBox pictureBox)
                {
                    if (pictureBox != null)
                    {
                        selectedGoodsID = pictureBox.Tag.ToString();
                    }
                    pingxingchuangkou childForm = new pingxingchuangkou();
                    // 假设 panel5 和 zhuyepanel 是已经定义好的控件


                    panel1.BringToFront();
                    panel1.Visible = true;
                    // childForm 是函数参数，代表要添加的子窗体
                    childForm.TopLevel = false; // 设置子窗体为非顶层窗体
                    childForm.Dock = DockStyle.Fill; // 让子窗体填充整个面板
                    childForm.FormBorderStyle = FormBorderStyle.None; // 可选：去除子窗体的边框
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



               
            }
            else
            {
                originalAutoScrollMinSize = panel5.AutoScrollPosition;
                if (originalAutoScrollMinSize.Y < 0)
                {
                    originalAutoScrollMinSize.Y = -originalAutoScrollMinSize.Y;
                }

                textBox1.Visible = false;
                panel11.Visible = false;
                panel5.AutoScroll = false;
                button6.Visible = true;
                button35.Visible = false;

                if (sender is PictureBox pictureBox)
                {
                    if (pictureBox != null)
                    {
                        selectedGoodsID = pictureBox.Tag.ToString();
                    }
                    Tproducts t1 = new Tproducts();
                    Zichuangti(t1);
                    panel8.Visible = true;

                }
            }


            


        }


        private string ToggleSortOrder(string currentSortOrder)
        {

            if (string.IsNullOrEmpty(currentSortOrder))
            {
                return "ASC";
            }
            else if (currentSortOrder == "ASC")
            {
                return "DESC";
            }
            else
            {
                return "";
            }
        }


        private void UpdateButtonUI(Button button, string sortOrder)
        {
            // 更新按钮颜色或其他UI状态
            if (sortOrder == "ASC")
            {
                button.BackColor = Color.Green;
            }
            else if (sortOrder == "DESC")
            {
                button.BackColor = Color.Red;
            }
            else
            {
                button.BackColor = DefaultBackColor;
            }
        }

        private void LoadDataFromDatabase4leibie(FlowLayoutPanel flowLayoutPanel, string searchKeyword = "", string classification = "", string orderBy = "", string priceFilter = "")
        {
            DB.GetCn();
            flowLayoutPanel.Controls.Clear();

            // 构建SQL查询语句
            string sql = "SELECT * FROM Product_Table WHERE 1=1";

            if (!string.IsNullOrEmpty(classification))
            {
                sql += " AND Classification_ID = '" + classification + "'";
            }

            if (!string.IsNullOrEmpty(priceFilter))
            {
                sql += " AND " + priceFilter;
            }

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                sql += " AND Goods_Name LIKE '%" + searchKeyword + "%'";
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " ORDER BY " + orderBy;
            }

            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(190, 332);
                imagePanel.BackColor = Color.Transparent;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = Application.StartupPath + row["image"].ToString();
                pictureBox.BackColor = Color.White;

                // 为PictureBox添加Click事件
                pictureBox.Tag = row["Goods_ID"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);

                //创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["Price"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = "￥" + labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                imagePanel.Controls.Add(label1);

                Label label2 = new Label();
                string labelText2 = row["Goods_Name"].ToString().Replace("<br>", Environment.NewLine);
                label2.Text = labelText2;
                label2.AutoSize = true;
                label2.Font = new Font(label2.Font.FontFamily, 16, FontStyle.Bold);
                label2.ForeColor = Color.Black;
                label2.BackColor = Color.Transparent;
                label2.Location = new Point(10, 235);
                imagePanel.Controls.Add(label2);


                Label label3 = new Label();
                string labelText3 = row["xiaoliang"].ToString().Replace("<br>", Environment.NewLine);
                if (labelText3 == "")
                {
                    labelText3 = "0";
                }
                label3.Text = "销量: " + labelText3;
                label3.AutoSize = true;
                label3.Font = new Font(label3.Font.FontFamily, 10, FontStyle.Bold);
                label3.ForeColor = Color.Black;
                label3.BackColor = Color.Transparent;
                label3.Location = new Point(10, 290);
                imagePanel.Controls.Add(label3);


                Label label4 = new Label();

                string haopinglvStr = row["haopinglv"].ToString();

                // 尝试将字符串转换为double类型
                double haopinglvDouble;
                if (double.TryParse(haopinglvStr, out haopinglvDouble))
                {
                    // 转换成功，乘以100
                    double result = haopinglvDouble * 100;
                    label4.Text = "好评率: " + result + "%";
                    // 这里result
                }
                else
                {
                    label4.Text = "好评率: 0%";
                }


                label4.AutoSize = true;
                label4.Font = new Font(label4.Font.FontFamily, 10, FontStyle.Bold);
                label4.ForeColor = Color.Black;
                label4.BackColor = Color.Transparent;
                label4.Location = new Point(10, 310);
                imagePanel.Controls.Add(label4);

                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
        }




        private void LoadDataFromDatabase4(FlowLayoutPanel flowLayoutPanel, string type = "", string classification = "")
        {
            DB.GetCn();
            flowLayoutPanel.Controls.Clear();

            int maskYH = 1; // 优惠
            int maskTJ = 2; // 推荐
            int maskFL = 4; // 分类

            // 根据输入的类型确定位掩码的组合
            int combinedMask = 0;
            if (!string.IsNullOrEmpty(type))
            {
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
            }

            // 构建SQL查询语句
            string sql = "SELECT * FROM Product_Table";
            if (combinedMask > 0)
            {
                sql += " WHERE (leixing & " + combinedMask + ") = " + combinedMask;
            }
            if (!string.IsNullOrEmpty(classification))
            {
                sql += (combinedMask > 0 ? " AND " : " WHERE ") + "Classification_ID = '" + classification + "'";
            }

            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(190, 332);
                imagePanel.BackColor = Color.Transparent;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = Application.StartupPath + row["image"].ToString();
                pictureBox.BackColor = Color.White;

                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["Goods_ID"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);

                //创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["Price"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = "￥" + labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 14, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                imagePanel.Controls.Add(label1);

                Label label2 = new Label();
                string labelText2 = row["Goods_Name"].ToString().Replace("<br>", Environment.NewLine);
                label2.Text = labelText2;
                label2.AutoSize = true;
                label2.Font = new Font(label2.Font.FontFamily, 14, FontStyle.Bold);
                label2.ForeColor = Color.Black;
                label2.BackColor = Color.Transparent;
                label2.Location = new Point(10, 235);
                imagePanel.Controls.Add(label2);

                Label label3 = new Label();
                string labelText3 = row["xiaoliang"].ToString().Replace("<br>", Environment.NewLine);
                if (labelText3 == "")
                {
                    labelText3 = "0";
                }
                label3.Text = "销量: "+labelText3;
                label3.AutoSize = true;
                label3.Font = new Font(label3.Font.FontFamily, 10, FontStyle.Bold);
                label3.ForeColor = Color.Black;
                label3.BackColor = Color.Transparent;
                label3.Location = new Point(10, 290);
                imagePanel.Controls.Add(label3);


                Label label4 = new Label();

                string haopinglvStr = row["haopinglv"].ToString();

                // 尝试将字符串转换为double类型
                double haopinglvDouble;
                if (double.TryParse(haopinglvStr, out haopinglvDouble))
                {
                    // 转换成功，乘以100
                    double result = haopinglvDouble * 100;
                    label4.Text = "好评率: " + result + "%";
                    // 这里result
                }
                else
                {
                    label4.Text = "好评率: 0%";
                }
              
                
                label4.AutoSize = true;
                label4.Font = new Font(label4.Font.FontFamily, 10, FontStyle.Bold);
                label4.ForeColor = Color.Black;
                label4.BackColor = Color.Transparent;
                label4.Location = new Point(10, 310);
                imagePanel.Controls.Add(label4);

                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
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
        /// 
        /// 方法：计算行数
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




        private bool pingxing = true;

        /// <summary>
        /// 平行窗口出现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
           

            if (pingxing == true)
            {
                panel4.Visible = true;
                button5.Visible = true;
                button4.Visible = false;


                if (flowLayoutPanel2.Controls.Count>0)
                {
                    panel14.Visible = true;
                }
                else
                {
                    panel14.Visible = false;
                }
                
                flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width - panel4.Width, flowLayoutPanel3.Height);
                flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width - panel4.Width, flowLayoutPanel1.Height);
                flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width - panel4.Width, flowLayoutPanel6.Height);
                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width - panel4.Width, flowLayoutPanel4.Height);



                daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, flowLayoutPanel4.Height);
                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, 350 * CalculateRowCount(flowLayoutPanel4));
                SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
                SetFlowLayoutPanelHeightToContainInnerFlowLayout2(flowLayoutPanel7);

                



                pingxing = false;
            }


        }

        /// <summary>
        /// 平行窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            pingxing = true;
            panel4.Visible = false;
            button5.Visible = false;
            button4.Visible = true;
            panel14.Visible = false;

            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width + panel4.Width, flowLayoutPanel3.Height);
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + panel4.Width, flowLayoutPanel1.Height);
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width + panel4.Width, flowLayoutPanel6.Height);
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width + panel4.Width, flowLayoutPanel4.Height);

            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, flowLayoutPanel4.Height);
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, 350 * CalculateRowCount(flowLayoutPanel4));
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout2(flowLayoutPanel7);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                return;
            }

            button6.Visible = false;
            button35.Visible = true;

            textBox1.Text = textBox1.Text.Trim();
            flowLayoutPanel5.Visible = false;
            panel15.Visible = true;
            panel15.Dock = DockStyle.Fill;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, textBox1.Text);
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, flowLayoutPanel4.Height);
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, 350 * CalculateRowCount(flowLayoutPanel4));
            SetFlowLayoutPanelHeightToContainInnerFlowLayout2(flowLayoutPanel7);

        }


        private void button6_Click(object sender, EventArgs e)
        {
            
            if (panel15.Visible == true)
            {
                button6.Visible = false;
                button35.Visible = true;
            }

            textBox1.Visible = true;
            panel5.AutoScroll = true;
            flowLayoutPanel5.Visible = true;
            if (zhuyepanel2.Visible == true)
            {
                zhuyepanel.Visible = true;
                zhuyepanel2.Visible = false;

                panel8.Visible = true;
                if (!Tproducts.isTproductsClosed)
                {

                    panel11.Visible = false;
                }
                button6ClickedOnce = true;
            }
            else if (!button6ClickedOnce || zhuyepanel.Visible == true)
            {
                // 第一次点击时的操作
                //MessageBox.Show(originalAutoScrollMinSize.ToString());

                zhuyepanel.Visible = false;
                panel8.Visible = false;

                LoadDataFromDatabase4(flowLayoutPanel6);
                daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
                SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
                //MessageBox.Show(originalAutoScrollMinSize.ToString());
                panel5.AutoScrollPosition = originalAutoScrollMinSize;
                button6ClickedOnce = true; // 更新按钮点击状态
            }
            else
            {
                // 第二次点击时的操作
                panel5.AutoScrollPosition = new Point(0, 0); // 重置滚动位置

            }

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        public void denglu()
        {
            if (Login.iflogin == false)
            {
                panel11.Visible = true;
            }
            else
            {
                panel11.Visible = false;
            }

        }
        public void gundongtiao()
        {
            originalAutoScrollMinSize = panel5.AutoScrollPosition;
            if (originalAutoScrollMinSize.Y < 0)
            {
                originalAutoScrollMinSize.Y = -originalAutoScrollMinSize.Y;
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {

            panel8.Visible = false;
            gundongtiao();
            panel5.AutoScroll = false;
            denglu();
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
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
            panel8.Visible = false;
            gundongtiao();
            panel5.AutoScroll = false;
            denglu();
            dindan t1 = new dindan();
            Zichuangti(t1);
        }


        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;

            gundongtiao();
            panel5.AutoScroll = false;
            denglu();
            community t1 = new community();
            Zichuangti(t1);
            //this.Size = new Size(1589, 900);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel8.Visible = false; 
            gundongtiao();
            panel5.AutoScroll = false;
            denglu();
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
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void button7_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6, "分类", "1");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);


        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6, "分类", "2");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6, "分类", "3");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6, "分类", "4");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6, "分类", "5");
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel6);
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel11.Visible = false;
            panel8.Visible = false;
            originalAutoScrollMinSize = panel5.AutoScrollPosition;
            if (originalAutoScrollMinSize.Y < 0)
            {
                originalAutoScrollMinSize.Y = -originalAutoScrollMinSize.Y;

            }

            if(zhuyepanel.Visible == false)
            {
                SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
                panel5.AutoScroll = false;

                if (Login.iflogin == false)
                {
                    Login t1 = new Login(this);
                    Zichuangti(t1);
                    t1.HidePanelHandler = new Login.HidePanelDelegate(jiaruSignup);
                    t1.HidePanebbb = new Login.HidePanel(hidepanel);
                }
                else
                {
                    gereninfo t2 = new gereninfo(this);
                    Zichuangti(t2);
                    t2.HidePanelHandler = new gereninfo.HidePanelDelegate(HidePanel10);
                }
            }
            else
            {
                zhuyepanel.Visible = false;
            }

        }

        public void HidePanel10()
        {
            zhuyepanel.Visible = false;
        }

        private void hidepanel()
        {
            panel5.AutoScroll = true;
            zhuyepanel.Visible = false;
            panel8.Visible = false;

            // 执行其他布局调整方法
            daxiao(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel6);
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
            panel5.AutoScrollPosition = originalAutoScrollMinSize;
            button6ClickedOnce = true; // 更新按钮点击状态
        }


        /// <summary>
        /// 二级窗体
        /// </summary>
        private void jiaruSignup()
        {
            Signup childForm = new Signup();

            zhuyepanel2.BringToFront();
            zhuyepanel2.Visible = true;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None; 
            zhuyeflow2.Controls.Add(childForm); 
            childForm.Show(); // 显示子窗体

            // 关闭 zhuyeflow 面板中的其它子窗体
            foreach (Control control in zhuyeflow2.Controls)
            {
                if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                {
                    form.Close();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

            if(Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
                return;
            }


            panel8.Visible = false;

            if (!string.IsNullOrEmpty(selectedGoodsID))
            {
                // 这里可以使用 selectedGoodsID 来执行你想要的操作
                // 例如，将信息显示在 zhuyepanel2 面板上

                //// 显示 zhuyepanel2 面板
                //zhuyepanel.Visible = true;
                ShopCart childForm = new ShopCart();

                zhuyepanel2.BringToFront();
                zhuyepanel2.Visible = true;
                childForm.TopLevel = false;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                zhuyeflow2.Controls.Add(childForm);
                childForm.Show(); // 显示子窗体

                // 关闭 zhuyeflow 面板中的其它子窗体
                foreach (Control control in zhuyeflow2.Controls)
                {
                    if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                    {
                        form.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("No item selected.");
            }


        }

        private void button22_Click(object sender, EventArgs e)
        {
            if(Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
            }
            else
            {
                UpdateZhuangtaiRecord(Login.StrValue, selectedGoodsID);
            }
            



        }

        public static string a1, b2, c3,f6;
        public static int d4,e5;

        public static bool UpdateZhuangtaiRecord(string Customerld, string Proid)
        {
            try
            {
                // 查询商品信息
                string query = "SELECT * FROM Product_Table WHERE Goods_ID = '" + selectedGoodsID + "'";
                DataTable result = DB.GetDataSet(query);
                if (result.Rows.Count > 0)
                {


                    // 现有字段的处理
                    a1 = result.Rows[0]["Goods_Name"].ToString();
                    b2 = result.Rows[0]["Price"].ToString();
                    c3 = result.Rows[0]["Unit_Price"].ToString();
                    d4 = 1;
                    e5 = Convert.ToInt32(result.Rows[0]["Stock_Quantity"]);

                    if (e5 <= 0)
                    {
                        MessageBox.Show("该商品无货");
                        return false;
                    }
                    f6 = result.Rows[0]["image"].ToString();
                }
                else
                {
                    MessageBox.Show("没有找到记录。");
                    return false;
                }

                // 检查购物车中是否已经存在相同的商品
                string checkQuery = "SELECT * FROM CartItem WHERE Customerld = '" + Customerld + "' AND Proid = '" + Proid + "'";
                DataTable checkResult = DB.GetDataSet(checkQuery);

                // 使用GetCn方法获取数据库连接
                SqlConnection connection = DB.GetCn();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                if (checkResult.Rows.Count > 0)
                {
                    // 更新现有记录的数量
                    string updateQuery = "UPDATE CartItem SET Qty = Qty + 1 WHERE Customerld = @Customerld AND Proid = @Proid";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@Customerld", Customerld);
                    command.Parameters.AddWithValue("@Proid", Proid);

                    int result2 = command.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        return true; // 更新成功
                    }
                    else
                    {
                        MessageBox.Show("没有行被更新。");
                        return false; // 没有行被更新
                    }
                }
                else
                {
                    // 插入新记录
                    string insertQuery = "INSERT INTO CartItem (Customerld, Proid, ProName, ListPrice, Unprice, Qty, Stock_Quantity, image) " +
                                         "VALUES (@Customerld, @Proid, @ProName, @ListPrice, @Unprice, @Qty, @Stock_Quantity, @image)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@Customerld", Customerld);
                    command.Parameters.AddWithValue("@Proid", Proid);
                    command.Parameters.AddWithValue("@ProName", a1);
                    command.Parameters.AddWithValue("@ListPrice", b2);
                    command.Parameters.AddWithValue("@Unprice", c3);
                    command.Parameters.AddWithValue("@Qty", d4);
                    command.Parameters.AddWithValue("@Stock_Quantity", e5);
                    command.Parameters.AddWithValue("@image", f6);

                    int result2 = command.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        return true; // 插入成功
                    }
                    else
                    {
                        MessageBox.Show("没有行被插入。");
                        return false; // 没有行被插入
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                MessageBox.Show("操作失败：" + ex.Message);
                return false;
            }
            finally
            {
                DB.cn.Close();
                
            }
        }


        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            zhuyepanel.Visible = false;

            panel8.Visible = false;
            //flowLayoutPanel6.Width = 10;
            //panel5.AutoScrollPosition = new Point(0, 0);



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
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label15_Click(object sender, EventArgs e)
        {

            if (Login.iflogin == true)
            {
                //panel1.Visible = false;


            }

            originalAutoScrollMinSize = panel5.AutoScrollPosition;
            if (originalAutoScrollMinSize.Y < 0)
            {
                originalAutoScrollMinSize.Y = -originalAutoScrollMinSize.Y;
            }


            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);
            panel5.AutoScroll = false;

            if (Login.iflogin == false)
            {
                Login t1 = new Login(this);
                Zichuangti(t1);
                t1.HidePanelHandler = new Login.HidePanelDelegate(jiaruSignup);
                t1.HidePanebbb = new Login.HidePanel(hidepanel);
            }

            panel11.Visible = false;
        }

        
        private void label12_Click(object sender, EventArgs e)
        {
            clickCount++; // 每次点击时，点击次数加1

            if (clickCount == 3)
            {
                this.Visible = false;
                houtaidenglu t1 = new houtaidenglu();
                t1.ShowDialog();

                clickCount = 0;

            }

        }

        private void zhuye_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void zhuyepanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Visible = false;
            houtaizhuye t1 = new houtaizhuye();
            t1.ShowDialog();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
            }
            else
            {
                UpdateZhuangtaiRecord(Login.StrValue, selectedGoodsID);
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {

            if (Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
            }
            else
            {
                panel8.Visible = false;
                if (!string.IsNullOrEmpty(selectedGoodsID))
                {
                    if (e5 <= 0)
                    {
                        MessageBox.Show("该商品无货");
                        return;
                    }

                    zhuyeinfo childForm = new zhuyeinfo();

                    zhuyepanel2.BringToFront();
                    zhuyepanel2.Visible = true;
                    childForm.TopLevel = false;
                    childForm.Dock = DockStyle.Fill;
                    childForm.FormBorderStyle = FormBorderStyle.None;
                    zhuyeflow2.Controls.Add(childForm);
                    childForm.Show(); // 显示子窗体

                    // 关闭 zhuyeflow 面板中的其它子窗体
                    foreach (Control control in zhuyeflow2.Controls)
                    {
                        if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                        {
                            form.Close();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }

        }


        private string currentClassification = "";
        private void button28_Click(object sender, EventArgs e)
        {
            currentClassification = "2";
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);
            AdjustFlowLayoutPanelHeight(flowLayoutPanel4);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            currentClassification = "1";
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);
            AdjustFlowLayoutPanelHeight(flowLayoutPanel4);
        }


        private void button27_Click_1(object sender, EventArgs e)
        {
            currentClassification = "4";
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);
            AdjustFlowLayoutPanelHeight(flowLayoutPanel4);
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            currentClassification = "5";
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);
            AdjustFlowLayoutPanelHeight(flowLayoutPanel4);
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            currentClassification = "3";
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);
            AdjustFlowLayoutPanelHeight(flowLayoutPanel4);
        }


        private void AdjustFlowLayoutPanelHeight(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.Size = new Size(flowLayoutPanel.Width, 350 * CalculateRowCount(flowLayoutPanel));
            SetFlowLayoutPanelHeightToContainInnerFlowLayout2(flowLayoutPanel7);
        }

        private void button34_Click(object sender, EventArgs e)
        {

            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, currentClassification, "", priceFilter);


            //string str = "";
            //DB.GetCn();
            //int i = comboBox1.SelectedIndex;
            //switch (i)
            //{
            //    case 0:
            //        str = "Price<=50";
            //        break;
            //    case 1:
            //        str = "Price>50 and Price<=100";
            //        break;
            //    case 2:
            //        str = "Price>100 and Price<=200";
            //        break;
            //    case 3:
            //        str = "Price>200 and Price<=500";
            //        break;
            //    case 4:
            //        str = "Price>500 and Price<=1000";
            //        break;
            //    default:
            //        str = "Price>=1000";
            //        break;
            //}
            //string sdr = "select * from Product_Table where " + str;
            //SqlCommand cmd2 = new SqlCommand(sdr, DB.cn);
            //SqlDataReader rdr = cmd2.ExecuteReader();




        }


        public bool feileipanel = true;
        private void button3_Click(object sender, EventArgs e)
        {

            if(feileipanel == true)
            {
                
                panel16.Size = new Size(panel16.Width, panel16.Height+50);
                feileipanel = false;
            }
            else
            {
                LoadDataFromDatabase4leibie(flowLayoutPanel4, textBox1.Text);

                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, flowLayoutPanel4.Height);
                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, 350 * CalculateRowCount(flowLayoutPanel4));
                SetFlowLayoutPanelHeightToContainInnerFlowLayout2(flowLayoutPanel7);
                flowLayoutPanel7.Height = flowLayoutPanel7.Height + 50;
                panel16.Size = new Size(panel16.Width, panel16.Height - 50);
                feileipanel = true;
            }

        }

        private void button30_Click(object sender, EventArgs e)
        {

            currentPriceSortOrder = ToggleSortOrder(currentPriceSortOrder);
            currentXiaoliangSortOrder = "";
            currentHaopinglvSortOrder = "";

            string orderBy = string.IsNullOrEmpty(currentPriceSortOrder) ? "" : "Price " + currentPriceSortOrder;
            string classification = currentClassification;
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, "", orderBy, priceFilter);

            UpdateButtonUI(button30, currentPriceSortOrder);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            currentXiaoliangSortOrder = ToggleSortOrder(currentXiaoliangSortOrder);
            currentPriceSortOrder = "";
            currentHaopinglvSortOrder = "";

            string orderBy = string.IsNullOrEmpty(currentXiaoliangSortOrder) ? "" : "xiaoliang " + currentXiaoliangSortOrder;
            string classification = currentClassification;
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, "", orderBy, priceFilter);

            UpdateButtonUI(button31, currentXiaoliangSortOrder);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            currentHaopinglvSortOrder = ToggleSortOrder(currentHaopinglvSortOrder);
            currentPriceSortOrder = "";
            currentXiaoliangSortOrder = "";

            string orderBy = string.IsNullOrEmpty(currentHaopinglvSortOrder) ? "" : "haopinglv " + currentHaopinglvSortOrder;
            string classification = currentClassification;
            string priceFilter = GetPriceFilter(comboBox1.SelectedIndex);
            string searchKeyword = textBox1.Text;

            LoadDataFromDatabase4leibie(flowLayoutPanel4, searchKeyword, "", orderBy, priceFilter);

            UpdateButtonUI(button32, currentHaopinglvSortOrder);
        }

        private string GetPriceFilter(int selectedIndex)
        {
            if (comboBox1.Text == "")
            {
                return "";
            }
            
            switch (selectedIndex)
            {
                case 0:
                    return "Price<=50";
                case 1:
                    return "Price>50 AND Price<=100";
                case 2:
                    return "Price>100 AND Price<=200";
                case 3:
                    return "Price>200 AND Price<=500";
                case 4:
                    return "Price>500 AND Price<=1000";
                case 5:
                    return "Price>=1000";
                default:
                    return "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 禁止换行符
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            panel15.Visible = false;
            flowLayoutPanel5.Visible = true;
            
            button6.Visible = true;
            button35.Visible = false;

        }

        private void button36_Click(object sender, EventArgs e)
        {

           
        }

        private void button20_Click(object sender, EventArgs e)
        {

            if (Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
                return;
            }


            panel8.Visible = false;

            if (!string.IsNullOrEmpty(selectedGoodsID))
            {
                // 这里可以使用 selectedGoodsID 来执行你想要的操作
                // 例如，将信息显示在 zhuyepanel2 面板上

                //// 显示 zhuyepanel2 面板
                //zhuyepanel.Visible = true;
                information childForm = new information();

                zhuyepanel2.BringToFront();
                zhuyepanel2.Visible = true;
                childForm.TopLevel = false;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                zhuyeflow2.Controls.Add(childForm);
                childForm.Show(); // 显示子窗体

                // 关闭 zhuyeflow 面板中的其它子窗体
                foreach (Control control in zhuyeflow2.Controls)
                {
                    if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                    {
                        form.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("No item selected.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            LoadDataFromDatabase4(flowLayoutPanel3, "优惠");
            LoadDataFromDatabase4(flowLayoutPanel1, "推荐");
            LoadDataFromDatabase4(flowLayoutPanel6);

            // 重新计算行数并调整大小
            flowLayoutPanel3.Size = new Size(flowLayoutPanel3.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel3));
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel1));
            flowLayoutPanel6.Size = new Size(flowLayoutPanel6.Width + panel4.Width - 200, 350 * CalculateRowCount(flowLayoutPanel6));
           
            SetFlowLayoutPanelHeightToContainInnerFlowLayout(flowLayoutPanel5);


        }

        private void button21_Click(object sender, EventArgs e)
        {



            if (e5 <= 0)
            {
                MessageBox.Show("该商品无货");
                return;
            }

            if (Login.StrValue == "")
            {
                MessageBox.Show("请先登录");
            }
            else
            {
                panel8.Visible = false;
                if (!string.IsNullOrEmpty(selectedGoodsID))
                {

                    zhuyeinfo childForm = new zhuyeinfo();

                    zhuyepanel2.BringToFront();
                    zhuyepanel2.Visible = true;
                    childForm.TopLevel = false;
                    childForm.Dock = DockStyle.Fill;
                    childForm.FormBorderStyle = FormBorderStyle.None;
                    zhuyeflow2.Controls.Add(childForm);
                    childForm.Show(); // 显示子窗体

                    // 关闭 zhuyeflow 面板中的其它子窗体
                    foreach (Control control in zhuyeflow2.Controls)
                    {
                        if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
                        {
                            form.Close();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("No item selected.");
                }
            }

           
        }

        public void OnUpdatePictureBox(Image image, string labelText)
        {
            if (pictureBox1.InvokeRequired)
            {
                // 如果是在非UI线程调用，确保在UI线程上更新pictureBox1
                pictureBox1.Invoke(new Action(() => UpdatePictureBoxEvent?.Invoke(image, labelText)));
            }
            else
            {
                UpdatePictureBoxEvent?.Invoke(image, labelText);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class dindan : Form
    {
        public dindan()
        {
            InitializeComponent();
        }


        public void Zichuangti(Form childForm)
        {
            // 假设 panel5 和 zhuyepanel 是已经定义好的控件

            //panel5.AutoScrollPosition = new Point(0, 0);

            //flowLayoutPanel5.Height = 1500;
            //zhuyepanel.BringToFront();
            //zhuyepanel.Visible = true;
            //// childForm 是函数参数，代表要添加的子窗体
            //childForm.TopLevel = false; // 设置子窗体为非顶层窗体
            //childForm.Dock = DockStyle.Fill; // 让子窗体填充整个面板
            //childForm.FormBorderStyle = FormBorderStyle.None; // 可选：去除子窗体的边框
            //zhuyeflow.Controls.Add(childForm); // 将子窗体添加到面板中
            //childForm.Show(); // 显示子窗体

            //// 关闭 zhuyeflow 面板中的其它子窗体
            //foreach (Control control in zhuyeflow.Controls)
            //{
            //    if (control is Form form && form != childForm) // 确保不关闭当前添加的子窗体
            //    {
            //        form.Close();
            //    }
            //}
        }


        private void LoadDataFromDatabase4(FlowLayoutPanel flowLayoutPanel, string status = "")
        {
            DB.GetCn();
            flowLayoutPanel.Controls.Clear();

            string sql = "SELECT * FROM Order_Table";

            // 根据状态构建SQL查询语句
            if (!string.IsNullOrEmpty(status))
            {
                sql += " WHERE Status = '" + status + "'";
            }

            sql += " ORDER BY OrderDate DESC"; // 按时间排序，早的在上面，晚的在下面

            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 350);
                panel1.BackColor = Color.Transparent;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(185, 201);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = Application.StartupPath + row["image"].ToString();
                pictureBox.BackColor = Color.White;
                pictureBox.Tag = row["Order"].ToString();
                panel1.Controls.Add(pictureBox);

                Label label1 = new Label();
                string labelText1 = row["UserName"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 18, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                panel1.Controls.Add(label1);

                Label label2 = new Label();
                label2.Text = $"价格: {Convert.ToDecimal(row["Price"]).ToString("F2")}";
                label2.AutoSize = true;
                label2.Font = new Font(label2.Font.FontFamily, 12, FontStyle.Regular);
                label2.Location = new Point(5, 235);
                panel1.Controls.Add(label2);

                Label label3 = new Label();
                label3.Text = $"数量: {Convert.ToInt32(row["Qty"])}";
                label3.AutoSize = true;
                label3.Font = new Font(label3.Font.FontFamily, 12, FontStyle.Regular);
                label3.Location = new Point(5, 265);
                panel1.Controls.Add(label3);

                // 根据订单状态创建相应按钮
                int buttonY = 295;
                switch (status)
                {
                    case "待发货":
                        Button btnCancelOrder = new Button();
                        btnCancelOrder.Text = "取消订单";
                        btnCancelOrder.Click += (s, e) => { CancelOrder(row["OrderID"]); };
                        btnCancelOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnCancelOrder);

                        Button btnConfirmReceipt = new Button();
                        btnConfirmReceipt.Text = "确认收货";
                        btnConfirmReceipt.Click += (s, e) => { ConfirmReceipt(row["OrderID"]); };
                        btnConfirmReceipt.Location = new Point(100, buttonY);
                        panel1.Controls.Add(btnConfirmReceipt);
                        break;

                    case "已收货":
                        Button btnReturnOrder = new Button();
                        btnReturnOrder.Text = "退货";
                        btnReturnOrder.Click += (s, e) => { ReturnOrder(row["OrderID"]); };
                        btnReturnOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnReturnOrder);

                        Button btnReviewOrder = new Button();
                        btnReviewOrder.Text = "评价";
                        btnReviewOrder.Click += (s, e) => { ReviewOrder(row["OrderID"]); };
                        btnReviewOrder.Location = new Point(100, buttonY);
                        panel1.Controls.Add(btnReviewOrder);

                        Button btnReorder = new Button();
                        btnReorder.Text = "再次购买";
                        btnReorder.Click += (s, e) => { Reorder(row["OrderID"]); };
                        btnReorder.Location = new Point(55, buttonY + 30);
                        panel1.Controls.Add(btnReorder);
                        break;

                    case "已取消":
                        Button btnDeleteOrder = new Button();
                        btnDeleteOrder.Text = "删除订单";
                        btnDeleteOrder.Click += (s, e) => { DeleteOrder(row["OrderID"]); };
                        btnDeleteOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnDeleteOrder);

                        Button btnRepurchase = new Button();
                        btnRepurchase.Text = "重新购买";
                        btnRepurchase.Click += (s, e) => { Repurchase(row["OrderID"]); };
                        btnRepurchase.Location = new Point(100, buttonY);
                        panel1.Controls.Add(btnRepurchase);
                        break;

                    default:
                        // 全部订单不需要额外按钮处理
                        break;
                }

                // 添加到flowLayoutPanel
                flowLayoutPanel.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
        }

        // 处理按钮点击事件的示例方法
        private void CancelOrder(object orderId)
        {
            // 取消订单逻辑
        }

        private void ConfirmReceipt(object orderId)
        {
            // 确认收货逻辑
        }

        private void ReturnOrder(object orderId)
        {
            // 退货逻辑
        }

        private void ReviewOrder(object orderId)
        {
            // 评价逻辑
        }

        private void Reorder(object orderId)
        {
            // 再次购买逻辑
        }

        private void DeleteOrder(object orderId)
        {
            // 删除订单逻辑
        }

        private void Repurchase(object orderId)
        {
            // 重新购买逻辑
        }

        private void dindan_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel1,"待发货");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //LoadDataFromDatabase4();
        }
    }
}

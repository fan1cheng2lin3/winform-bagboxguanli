using BoxSystemMange.脚本类;
using NPOI.SS.Formula.Functions;
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

namespace BoxSystemMange
{
    public partial class ShopCart : Form
    {
        public ShopCart()
        {
            InitializeComponent();
        }


        SqlDataAdapter daProduct;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GetCn();
            string str = "select * from CartItem where Customerld='" + Login.StrValue + "'";
            daProduct = new SqlDataAdapter(str, DB.cn);
            daProduct.Fill(ds, "product_info");
        }

        private void ShopCart_Load(object sender, EventArgs e)
        {
            init();
            LoadDataFromDatabase();
        }

        private Dictionary<Panel, bool> panelSelection = new Dictionary<Panel, bool>();


        private void shanchu()
        {
            DB.GetCn();

            
            List<string> productIDsToRemove = new List<string>();

            // 遍历 flowLayoutPanel1 中的所有 Panel
            foreach (Panel panel in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                // 找到选择的 Panel
                var selectButton = panel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "取消选择");

                if (selectButton != null)
                {
                    // 从Panel的Tag属性中获取商品ID
                    string productID = panel.Tag as string;
                    if (!string.IsNullOrEmpty(productID))
                    {
                        productIDsToRemove.Add(productID);
                    }
                    // 将 Panel 从 flowLayoutPanel1 中移除
                    flowLayoutPanel1.Controls.Remove(panel);
                }
                else
                {
                    
                }
            }

            // 删除数据库中对应的记录
            if (productIDsToRemove.Count > 0)
            {
                string joinedProductIDs = string.Join("','", productIDsToRemove);
                string deleteSql = $"DELETE FROM CartItem WHERE Customerld='{Login.StrValue}' AND Proid IN ('{joinedProductIDs}')";
                SqlCommand deleteCommand = new SqlCommand(deleteSql, DB.cn);
                deleteCommand.ExecuteNonQuery();
            }

            // 清空 FlowLayoutPanel 中的所有控件
            //flowLayoutPanel1.Controls.Clear();

            // 更新数据库
            SqlCommandBuilder dbProduct = new SqlCommandBuilder(daProduct);
            daProduct.Update(ds, "product_info");

            // 更新汇总信息
            UpdateSummary();

            // 关闭数据库连接
            DB.cn.Close();

        }


        private void LoadDataFromDatabase2()
        {
            DB.GetCn();
            // 清空现有的控件
            flowLayoutPanel2.Controls.Clear();

            // 用于存储总价和总数量
            decimal totalPrice = 0;
            int totalQuantity = 0;

            // 遍历flowLayoutPanel1中的Panel，只显示已选择的Panel
            foreach (Panel panel in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                var selectButton = panel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "取消选择");
                if (selectButton != null)
                {

                    // 获取 Proid
                    string productID = panel.Tag.ToString();
                    Panel panel2 = new Panel();
                    panel2.Size = new Size(851, 100);
                    panel2.BackColor = Color.LightGray;
                    panel2.Tag = productID;


                    // 创建一个新的Panel
                    Panel imagePanel = new Panel();
                    imagePanel.Size = new Size(100, 100);
                    imagePanel.Location = new Point(2, 2);
                    imagePanel.BackColor = Color.LightGray;
                    panel2.Controls.Add(imagePanel);

                    // 查询数据库获取图片路径
                    string sql = $"SELECT image FROM CartItem WHERE Proid = '{productID}'";
                    DataTable dataTable = DB.GetDataSet(sql);

                    if (dataTable.Rows.Count > 0)
                    {
                        // 获取图片路径
                        string imagePath = dataTable.Rows[0]["image"].ToString();

                        // 创建新的 PictureBox 并设置属性
                        PictureBox pictureBox2 = new PictureBox();
                        pictureBox2.Size = new Size(100, 100);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox2.ImageLocation = Application.StartupPath + imagePath; // 设置图片路径
                        pictureBox2.BackColor = Color.White;
                        pictureBox2.Location = new Point(1, 1);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox2.Tag = imagePath; // 设置 Tag 属性为图片路径
                        imagePanel.Controls.Add(pictureBox2);

                        // 添加到 panel2 中
                        panel2.Controls.Add(imagePanel);

                        // 添加 panel2 到 flowLayoutPanel2
                        flowLayoutPanel2.Controls.Add(panel2);
                    }
                    else
                    {
                        // 数据库未找到对应的记录，处理错误或者跳过当前循环
                        MessageBox.Show($"未找到 Proid 为 {productID} 的商品记录");
                        continue;
                    }


                    // 复制标签控件
                    Label label1 = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(200, 30));
                    if (label1 != null)
                    {
                        Label label1Copy = new Label();
                        label1Copy.Text = label1.Text;
                        label1Copy.AutoSize = true;
                        label1Copy.Location = new Point(200, 30);
                        panel2.Controls.Add(label1Copy);
                    }

                    Label label2 = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(200, 10));
                    if (label2 != null)
                    {
                        Label label2Copy = new Label();
                        label2Copy.Text = label2.Text;
                        label2Copy.AutoSize = true;
                        label2Copy.Location = new Point(200, 10);
                        panel2.Controls.Add(label2Copy);
                    }

                   

                    Label label4 = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "数量");
                    if (label4 != null)
                    {
                        Label label4Copy = new Label();
                        label4Copy.Text = label4.Text;
                        label4Copy.AutoSize = true;
                        label4Copy.Location = new Point(200, 40);
                        panel2.Controls.Add(label4Copy);
                    }

                    // 获取数量文本框的值
                    TextBox textBox = panel.Controls.OfType<TextBox>().FirstOrDefault();
                    if (textBox != null)
                    {
                        int quantity;
                        if (int.TryParse(textBox.Text, out quantity))
                        {
                            totalQuantity += quantity;
                            decimal price;
                            if (decimal.TryParse(label2.Text, out price))
                            {
                                totalPrice += price * quantity;
                            }

                            Label label3Copy = new Label();
                            label3Copy.Text = textBox.Text;
                            label3Copy.AutoSize = true;
                            label3Copy.Location = new Point(300, 40);
                            panel2.Controls.Add(label3Copy);
                        }
                    }

                    flowLayoutPanel2.Controls.Add(panel2);
                }
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel2.WrapContents = true;

            // 更新总价和总数量的标签
            Label lblTotalPrice2 = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label31");
            if (lblTotalPrice2 != null) lblTotalPrice2.Text = "总价: ￥" + totalPrice + "元";

            Label lblTotalItems = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotalItems");
            if (lblTotalItems != null) lblTotalItems.Text = "已选商品:" + flowLayoutPanel2.Controls.OfType<Panel>().Count();

            Label lblTotalQuantity = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotalQuantity");
            if (lblTotalQuantity != null) lblTotalQuantity.Text = "共" + totalQuantity + "件";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DB.GetCn();

            // 遍历 flowLayoutPanel2 中的每个 Panel
            foreach (Panel panel2 in flowLayoutPanel2.Controls.OfType<Panel>())
            {
                // 获取商品数量
                Label label3Copy = panel2.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(300, 40));
                if (label3Copy == null)
                {
                    MessageBox.Show("未找到数量信息");
                    continue; // 如果未找到数量信息，则跳过当前Panel
                }
                int quantity;
                if (!int.TryParse(label3Copy.Text, out quantity))
                {
                    MessageBox.Show("数量信息格式错误");
                    continue; // 如果数量信息格式错误，则跳过当前Panel
                }

                // 获取 Proid
                string productID = panel2.Tag.ToString();
                string imagePath;
                // 查询数据库获取图片路径
                string sql = $"SELECT image FROM CartItem WHERE Proid = '{productID}'";
                DataTable dataTable = DB.GetDataSet(sql);

                if (dataTable.Rows.Count > 0)
                {
                    // 获取图片路径
                    imagePath = dataTable.Rows[0]["image"].ToString();
                    
                }
                else
                {
                    MessageBox.Show("未找到图片信息");
                    imagePath = "null";
                }

                // 获取价格
                Label label2Copy = panel2.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(200, 10));
                if (label2Copy == null)
                {
                    MessageBox.Show("未找到价格信息");
                    continue; // 如果未找到价格信息，则跳过当前Panel
                }
                decimal price;
                if (!decimal.TryParse(label2Copy.Text, out price))
                {
                    MessageBox.Show("价格信息格式错误");
                    continue; // 如果价格信息格式错误，则跳过当前Panel
                }
                shanchu();


                // 获取商品名称
                Label label1Copy = panel2.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(200, 30));
                if (label1Copy == null)
                {
                    MessageBox.Show("未找到商品名称信息");
                    continue; // 如果未找到商品名称信息，则跳过当前Panel
                }
                string productName = label1Copy.Text;
                DB.GetCn();
                // 构建插入订单的 SQL 语句
                string str = $"INSERT INTO Order_Table VALUES ('{Login.StrValue}', '{DateTime.Now}', '{textBox3.Text}', " +
                             $"'{comboBox1.Text}', '{textBox1.Text}', '{textBox6.Text}', '{textBox4.Text}', " +
                             $"'{quantity}', '待发货', null, '{imagePath}', '{price}', '{productName}', '{productID}',null)";
                
                // 执行 SQL 插入操作
                DB.sqlEx(str);
            }

            // 清空结账后的购物车
            panel1.Visible = false;
            MessageBox.Show("已经结算咯");
        }


        private void LoadDataFromDatabase()
        {
            DB.GetCn();
            // 清空现有的控件
            flowLayoutPanel1.Controls.Clear();

            // 查询数据库
            string sql = "SELECT * FROM CartItem where Customerld='" + Login.StrValue + "'";
            DataTable dataTable = DB.GetDataSet(sql);

            // 用于存储每个Panel是否被选择的字典
            Dictionary<Panel, bool> panelSelection = new Dictionary<Panel, bool>();

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(851, 120);
                panel1.BackColor = Color.LightGray;
                panel1.Tag = row["Proid"].ToString();

                // 创建一个新的Panel
                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(120, 120);

                imagePanel.Location = new Point(2, 2);
                imagePanel.BackColor = Color.LightGray;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(120, 120);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = Application.StartupPath + row["image"].ToString();
                pictureBox.BackColor = Color.White;
                pictureBox.Location = new Point(1, 1);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["image"].ToString();
                //MessageBox.Show(pictureBox.Tag.ToString());
                pictureBox.Click += new EventHandler(pictureBox_Click);
                imagePanel.Controls.Add(pictureBox);

                // 创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["ProName"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = labelText1;
                label1.AutoSize = true;
                label1.Location = new Point(200, 30);
                panel1.Controls.Add(label1);

                // 对其他标签重复相同的处理
                Label label2 = new Label();
                string labelText2 = row["ListPrice"].ToString().Replace("<br>", Environment.NewLine);
                label2.Text = labelText2;
                label2.AutoSize = true;
                label2.Location = new Point(200, 10);
                panel1.Controls.Add(label2);

                Label label3 = new Label();
                string labelText3 = row["Stock_Quantity"].ToString().Replace("<br>", Environment.NewLine);
                label3.Text = labelText3;
                label3.AutoSize = true;
                label3.Location = new Point(575, 102);
                panel1.Controls.Add(label3);

                Label label4 = new Label();
                label4.Text = "数量";
                label4.AutoSize = true;
                label4.Location = new Point(500, 82);
                panel1.Controls.Add(label4);

                Label label5 = new Label();
                label5.Text = "库存量";
                label5.AutoSize = true;
                label5.Location = new Point(500, 102);
                panel1.Controls.Add(label5);

                // 创建TextBox用于显示和编辑商品数量
                TextBox textBox = new TextBox();
                textBox.Text = row["Qty"].ToString().Replace("<br>", Environment.NewLine); // 初始数量设为1，可以根据实际情况调整
                textBox.Location = new Point(570, 80);
                textBox.Size = new Size(50, 25);
                textBox.TextChanged += (s, e) =>
                {
                    int quantity;
                    if (int.TryParse(textBox.Text, out quantity))
                    {
                        if (quantity < 1)
                        {
                            textBox.Text = "1";
                        }
                        else if (quantity > int.Parse(label3.Text))
                        {
                            textBox.Text = label3.Text;
                        }
                    }
                    else
                    {
                        textBox.Text = "1";
                    }
                };
                panel1.Controls.Add(textBox);

                // 创建增加数量的Button
                Button btnIncrease = new Button();
                btnIncrease.Text = "+";
                btnIncrease.Size = new Size(30, 26);
                btnIncrease.Location = new Point(620, 78);
                btnIncrease.Click += (s, e) =>
                {
                    int quantity;
                    if (int.TryParse(textBox.Text, out quantity))
                    {
                        if (quantity < int.Parse(label3.Text))
                        {
                            textBox.Text = (quantity + 1).ToString();
                        }
                    }
                    UpdateSummary();
                };
                panel1.Controls.Add(btnIncrease);

                // 创建减少数量的Button
                Button btnDecrease = new Button();
                btnDecrease.Text = "-";
                btnDecrease.Size = new Size(30, 26);
                btnDecrease.Location = new Point(540, 78);
                btnDecrease.Click += (s, e) =>
                {
                    int quantity;

                    if (int.TryParse(textBox.Text, out quantity) && quantity > 1)
                    {
                        textBox.Text = (quantity - 1).ToString();
                    }
                    UpdateSummary();
                };
                panel1.Controls.Add(btnDecrease);

                // 创建选择/取消选择的Button
                Button btnSelect = new Button();
                btnSelect.Text = "选择";
                btnSelect.Size = new Size(100, 30);
                btnSelect.Location = new Point(570, 0);
                btnSelect.Click += (s, e) =>
                {
                    panelSelection[panel1] = !panelSelection[panel1];
                    btnSelect.Text = panelSelection[panel1] ? "取消选择" : "选择";
                    UpdateSummary();

                    
                };
                panel1.Controls.Add(btnSelect);

                // 初始化panelSelection字典
                panelSelection[panel1] = false;

                // 添加到flowLayoutPanel
                flowLayoutPanel1.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
        }


        private string sum;

        private void UpdateSummary()
        {
            decimal totalPrice = 0;
            int totalItems = 0;
            int totalQuantity = 0;

            foreach (Panel panel in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                if (panel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "取消选择") != null)
                {
                    Label priceLabel = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Location == new Point(200, 10));
                    TextBox quantityBox = panel.Controls.OfType<TextBox>().FirstOrDefault();

                    if (priceLabel != null && quantityBox != null)
                    {
                        totalItems++;
                        int quantity;
                        if (int.TryParse(quantityBox.Text, out quantity))
                        {
                            totalQuantity += quantity;
                            decimal price;
                            if (decimal.TryParse(priceLabel.Text, out price))
                            {
                                totalPrice += price * quantity;
                            }
                        }
                    }
                }
            }

            Label lblTotalPrice = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotalPrice");
            Label lblTotalItems = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotalItems");
            Label lblTotalQuantity = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotalQuantity");

            if (lblTotalPrice != null) lblTotalPrice.Text = "￥" + totalPrice + "元";

            sum = lblTotalPrice.Text;


            if (lblTotalItems != null) lblTotalItems.Text = "已选商品:" + totalItems;
            if (lblTotalQuantity != null) lblTotalQuantity.Text = "共" + totalQuantity + "件";
        }



        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                string videoNumber = clickedPictureBox.Tag.ToString();
                //AA2 t1 = new AA2(videoNumber); // 将视频编号传递给AA2窗体
                //t1.ShowDialog();
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (lblTotalPrice.Text == "￥0元")
            {
                MessageBox.Show("请选择商品");
                return;
            }
            lblTotalPrice2.Text = sum;
            panel1.Visible = true;
            panel1.Dock = DockStyle.Fill;
            LoadDataFromDatabase2();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 假设button1用于全选和取消全选
            bool allSelected = flowLayoutPanel1.Controls.OfType<Panel>().All(p => p.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "取消选择") != null);
            foreach (var panel in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                var selectButton = panel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "选择" || b.Text == "取消选择");
                if (selectButton != null)
                {
                    selectButton.Text = allSelected ? "选择" : "取消选择";
                    panelSelection[panel] = !allSelected;
                }
            }
            UpdateSummary();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("购物车为空");
                return;
            }

            DialogResult result = MessageBox.Show("确定要清除购物车吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DB.GetCn();
                    string deleteSql = "DELETE FROM CartItem WHERE Customerld='" + Login.StrValue + "'";
                    SqlCommand deleteCommand = new SqlCommand(deleteSql, DB.cn);
                    deleteCommand.ExecuteNonQuery();

                    // 清空FlowLayoutPanel中的所有控件
                    flowLayoutPanel1.Controls.Clear();

                    // 更新数据库
                    SqlCommandBuilder dbProduct = new SqlCommandBuilder(daProduct);
                    daProduct.Update(ds, "product_info");

                    // 更新汇总信息
                    UpdateSummary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("清空购物车失败：" + ex.Message);
                }
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}

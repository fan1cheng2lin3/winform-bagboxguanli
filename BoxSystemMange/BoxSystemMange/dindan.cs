using NPOI.SS.Formula.Functions;
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

namespace BoxSystemMange
{
    public partial class dindan : Form
    {

        private List<Button> ratingButtons;
        private int totalClicks;
        private int positiveClicks;
        private float haopinglv;
        private int Goods_ID;
        public static string a1, b2, c3, f6;

        public dindan()
        {
            InitializeComponent();
            InitializeRatingButtons();
            InitializeButtons();
        }

        private void InitializeRatingButtons()
        {
            this.ratingButtons = new List<Button>();
            for (int i = 0; i < 5; i++)
            {
                var button = new Button();
                button.Name = "button" + (i + 1);
                button.Text = (i + 1).ToString();
                button.Size = new System.Drawing.Size(50, 50);
                button.Location = new System.Drawing.Point(10 + i * 60, 10);
                button.Click += new EventHandler(RatingButton_Click);
                this.ratingButtons.Add(button);
                this.panel4.Controls.Add(button);
            }

            ResetButtons();
        }

        private void ResetButtons()
        {
            totalClicks = 0;
            positiveClicks = 0;
            haopinglv = 0;
            UpdateButtons(0); // 重置按钮颜色
        }

        private void InitializeButtons()
        {
            totalClicks = 0;
            positiveClicks = 0;
            UpdateButtons(0);
        }

        private void RatingButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = ratingButtons.IndexOf(clickedButton) + 1;
            UpdateButtons(index);

            totalClicks++;
            positiveClicks += index;

            haopinglv = (float)positiveClicks / (totalClicks * 5);
        }


        private void UpdateButtons(int numberOfButtonsToHighlight)
        {
            for (int i = 0; i < ratingButtons.Count; i++)
            {
                if (i < numberOfButtonsToHighlight)
                {
                    ratingButtons[i].BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    ratingButtons[i].BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        private void LoadDataFromDatabase4(FlowLayoutPanel flowLayoutPanel, string status = "")
        {
            DB.GetCn();
            flowLayoutPanel.Controls.Clear();

            string sql = "SELECT * FROM Order_Table";
            // 根据状态构建SQL查询语句
            if (!string.IsNullOrEmpty(status))
            {
                sql += " WHERE Status = '" + status + "' AND UserName = '" + Login.StrValue + "'";
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
                pictureBox.Tag = row["OrderDate"].ToString();

                string orderDate = pictureBox.Tag.ToString();
                string currentProductId = row["productID"].ToString().Replace("<br>", Environment.NewLine);

                panel1.Controls.Add(pictureBox);

                Label label1 = new Label();
                string labelText1 = row["productname"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = labelText1;
                label1.AutoSize = true;
                label1.Font = new Font(label1.Font.FontFamily, 12, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Transparent;
                label1.Location = new Point(5, 205);
                panel1.Controls.Add(label1);

                Label label2 = new Label();
                label2.Text = $"价格: {row["Price"].ToString()}";
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
                        btnCancelOrder.Text = row["Status2"].ToString() == "申请取消" ? "撤回申请" : "取消订单";
                        btnCancelOrder.Click += (s, e) =>
                        {
                            if (row["Status2"].ToString() == "申请取消")
                            {
                                // 处理撤回申请逻辑
                                WithdrawCancelOrder(orderDate, currentProductId);
                            }
                            else
                            {
                                // 处理取消订单逻辑
                                CancelOrder(orderDate, currentProductId);
                            }
                        };
                        btnCancelOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnCancelOrder);

                        Button btnConfirmReceipt = new Button();
                        btnConfirmReceipt.Text = "确认收货";
                        btnConfirmReceipt.Click += (s, e) => ConfirmReceipt(orderDate, currentProductId,Convert.ToInt32(row["Qty"]));
                        btnConfirmReceipt.Location = new Point(100, buttonY);

                        // 根据订单状态2决定是否隐藏确认收货按钮
                        if (row["Status2"].ToString() == "申请取消")
                        {
                            btnConfirmReceipt.Visible = false;
                        }

                        panel1.Controls.Add(btnConfirmReceipt);
                        break;

                    case "已收货":
                        Button btnReturnOrder = new Button();
                        btnReturnOrder.Text = DateTime.Parse(row["OrderDate"].ToString()) > DateTime.Now.AddDays(-7) && row["Status2"].ToString() == "申请退货" ? "撤回申请" : "退货";
                        btnReturnOrder.Click += (s, e) =>
                        {
                            if (DateTime.Parse(row["OrderDate"].ToString()) > DateTime.Now.AddDays(-7) && row["Status2"].ToString() == "申请退货")
                            {
                                // 处理撤回申请逻辑
                                WithdrawReturnOrder(orderDate, currentProductId);
                            }
                            else
                            {
                                // 处理申请退货逻辑
                                ReturnOrder(orderDate, currentProductId);
                            }
                        };
                        btnReturnOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnReturnOrder);

                        Button btnReviewOrder = new Button();
                        btnReviewOrder.Text = "评价";
                        btnReviewOrder.Click += (s, e) => ReviewOrder(currentProductId); // 传递当前订单日期
                        btnReviewOrder.Location = new Point(100, buttonY);


                        
                        panel1.Controls.Add(btnReviewOrder);

                        Button btnReorder = new Button();
                        btnReorder.Text = "再次购买";
                        btnReorder.Click += (s, e) => UpdateZhuangtaiRecord(Login.StrValue, currentProductId); // 传递当前订单日期
                        btnReorder.Location = new Point(55, buttonY + 30);


                        panel1.Controls.Add(btnReorder);
                        break;

                    case "已取消":
                        Button btnDeleteOrder = new Button();
                        btnDeleteOrder.Text = "删除订单";
                        btnDeleteOrder.Click += (s, e) => DeleteOrder(orderDate, currentProductId); // 传递当前订单日期和产品ID
                        btnDeleteOrder.Location = new Point(5, buttonY);
                        panel1.Controls.Add(btnDeleteOrder);

                        Button btnRepurchase = new Button();
                        btnRepurchase.Text = "重新购买";
                        btnRepurchase.Click += (s, e) => UpdateZhuangtaiRecord(Login.StrValue, currentProductId); // 传递当前订单日期
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



        private void WithdrawCancelOrder(string orderDate, string productId)
        {
            DB.GetCn();
            string sql = $"UPDATE Order_Table SET Status2 = NULL WHERE OrderDate = '{orderDate}' and productID = '{productId}'";
            try
            {
                DB.sqlEx(sql);
                MessageBox.Show("申请取消订单已成功撤回。");

                // 刷新订单列表
                LoadDataFromDatabase4(flowLayoutPanel1, "待发货");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"撤回申请失败：{ex.Message}");
            }
        }


        private void WithdrawReturnOrder(string orderDate, string productId)
        {
            DB.GetCn();
            string sql = $"UPDATE Order_Table SET Status2 = NULL WHERE OrderDate = '{orderDate}' and productID = '{productId}'";
            try
            {
                DB.sqlEx(sql);
                MessageBox.Show("申请退货已成功撤回。");

                // 刷新订单列表
                LoadDataFromDatabase4(flowLayoutPanel1, "已收货");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"撤回申请失败：{ex.Message}");
            }
        }



        // 处理按钮点击事件的示例方法
        private void CancelOrder(string orderDate, string productId)
        {
            DB.GetCn();
            string sql = $"UPDATE Order_Table SET Status2 = '申请取消' WHERE OrderDate = '{orderDate}' and productID = '{productId}'";
            try
            {
                DB.sqlEx(sql);
                MessageBox.Show("订单已成功申请取消。");

                // 刷新订单列表
                LoadDataFromDatabase4(flowLayoutPanel1, "待发货");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"取消订单失败：{ex.Message}");
            }
        }

        private void ConfirmReceipt(string orderDate, string productId,int xiaoliang)
        {
            DB.GetCn();
            string sql = $"UPDATE Order_Table SET Status = '已收货' WHERE OrderDate = '{orderDate}' and productID = '{productId}'";
            string sql2 = $"UPDATE Product_Table SET xiaoliang = xiaoliang + {xiaoliang}, Stock_Quantity = Stock_Quantity -{xiaoliang} WHERE Goods_ID = '{productId}'";
           
            try
            {
                DB.sqlEx(sql);
                DB.GetCn();
                DB.sqlEx(sql2);
                MessageBox.Show("订单已成功确认收货。");

                LoadDataFromDatabase4(flowLayoutPanel1, "待发货");// 重新加载当前订单列表
            }
            catch (Exception ex)
            {
                MessageBox.Show($"确认收货失败：{ex.Message}");
            }

            DB.cn.Close();
        }


        /// <summary>
        /// 申请退货
        /// </summary>
        /// <param name="orderId"></param>
        private void ReturnOrder(string orderDate, string productId)
        {
            DB.GetCn();
            string sql = $"UPDATE Order_Table SET Status2 = '申请退货' WHERE OrderDate = '{orderDate}' and productID = '{productId}'";
            try
            {
                DB.sqlEx(sql);
                MessageBox.Show("订单已成功申请退货。");

                // 刷新订单列表
                LoadDataFromDatabase4(flowLayoutPanel1, "已收货");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"申请退货失败：{ex.Message}");
            }
        }

        private void ReviewOrder(object Proid)
        {
            panel3.Visible = true;
            DB.GetCn();
            // 查询商品信息
            string query = "SELECT * FROM Product_Table WHERE Goods_ID = '" + Proid + "'";

            DataTable result = DB.GetDataSet(query);
            try
            {
                Goods_ID = Convert.ToInt32(result.Rows[0]["Goods_ID"]);
            }
            catch 
            {
                DB.GetCn();
                string sql = $"DELETE FROM Order_Table WHERE  productID = '{Proid}'";
                try
                {
                    DB.sqlEx(sql);
                    MessageBox.Show("商品已经删除了，订单已删除。");

                    // 刷新订单列表
                    LoadDataFromDatabase4(flowLayoutPanel1, "已收货");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"收货失败，删除订单失败：{ex.Message}");
                }
            }
            
           

            // 评价逻辑
        }


        private void DeleteOrder(string orderDate, string productId)
        {
            DB.GetCn();
            string sql = $"DELETE FROM Order_Table WHERE OrderDate = '{orderDate}' AND productID = '{productId}'";
            try
            {
                DB.sqlEx(sql);
                MessageBox.Show("订单已成功删除。");

                // 刷新订单列表
                LoadDataFromDatabase4(flowLayoutPanel1, "已取消");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除订单失败：{ex.Message}");
            }
        }

      

        private void dindan_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel1, "待发货");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel1, "待发货");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel1, "已收货");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase4(flowLayoutPanel1, "已取消");
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            DB.GetCn();

            string sql = $"UPDATE Product_Table SET haopinglv = (haopinglv + {haopinglv}) / 2  WHERE Goods_ID = '{Goods_ID}'";

            try
            { 
                
                panel3.Visible = false;
                DB.sqlEx(sql);
                MessageBox.Show("已成功评价。");
                ResetButtons(); // 提交后重置按钮状态


            }
            catch (Exception ex)
            {
                MessageBox.Show($"评价失败：{ex.Message}");
            }


        }

        public static int d4, e5;


        /// <summary>
        /// 重新购买
        /// </summary>
        /// <param name="Customerld"></param>
        /// <param name="Proid"></param>
        /// <returns></returns>
        public static bool UpdateZhuangtaiRecord(string Customerld, string Proid)
        {
            try
            {
                // 查询商品信息
                string query = "SELECT * FROM Product_Table WHERE Goods_ID = '" + Proid + "'";
                DataTable result = DB.GetDataSet(query);
                if (result.Rows.Count > 0)
                {
                    // 现有字段的处理
                    a1 = result.Rows[0]["Goods_Name"].ToString();
                    b2 = result.Rows[0]["Price"].ToString();
                    c3 = result.Rows[0]["Unit_Price"].ToString();
                    d4 = 1;
                    e5 = Convert.ToInt32(result.Rows[0]["Stock_Quantity"]);
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
                        MessageBox.Show("已重新加入购物车");
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



    }
}

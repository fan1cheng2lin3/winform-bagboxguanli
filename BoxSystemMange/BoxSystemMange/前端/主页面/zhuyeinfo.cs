using BoxSystemMange.脚本类;
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

namespace BoxSystemMange.前端.主页面
{
    public partial class zhuyeinfo : Form
    {
        public zhuyeinfo()
        {
            InitializeComponent();
        }
        public static decimal profit = 0;
        SqlDataAdapter daCartItem, daProduct;
        DataSet ds = new DataSet();


        private void zhuyeinfo_Load(object sender, EventArgs e)
        {
            DB.GetCn();
            label199.Text = zhuye.selectedGoodsID;

            
            Bind();
        }



        public int Stock_Quantity;
        void Bind()
        {
            init();

            
            DB.GetCn();
            // 使用DB类中的GetDataSet方法来获取数据
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.selectedGoodsID + "'";
            DataTable dt = DB.GetDataSet(sql);

            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row = dt.Rows[0];
                decimal price = Convert.ToDecimal(row["Price"]);

                // 去除小数点后两位
                string priceStr = price.ToString("F0"); // F0 格式化字符串，不显示小数点后的数字

                // 将去除小数后的字符串转换回 decimal 类型
                decimal priceWithoutDecimal = Convert.ToDecimal(priceStr);

                // 执行乘法操作
                decimal result = priceWithoutDecimal+Convert.ToInt32(label14.Text);

                // 设置 label9 的文本，保留两位小数
                label9.Text = "￥" + result.ToString("F2");
                label9.Font = new Font(label9.Font.FontFamily, 26, FontStyle.Bold);
                label9.ForeColor = Color.Red;
                label9.BackColor = Color.Transparent;


                label13.Text = "￥" + row["Price"].ToString().Remove(row["Price"].ToString().Length - 2, 2);

                Stock_Quantity = Convert.ToInt32( row["Stock_Quantity"]);
                label11.Text = Stock_Quantity.ToString();
                
            }
            else
            {
                MessageBox.Show("未找到数据。");
            }




            // 获取当前商品信息
            DataRow productRow = ds.Tables["product_info"].AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("Goods_ID") == Convert.ToInt32(zhuye.selectedGoodsID));

            if (productRow != null)
            {
                // 显示商品信息
                label8.Text = productRow["Goods_Name"].ToString();
                textBox6.Text = "1";  // 假设初始购买数量为 1

                // 显示商品图片
                string imagePath = productRow["image"].ToString();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    pictureBox1.ImageLocation = Application.StartupPath + imagePath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

        }

        void init()
        {

            DB.GetCn();
            string str = "select Product_ID,Customerld,Proid,ProName,ListPrice,Unprice,Qty from CartItem where Customerld = '" + Login.CustomerId +"'";
            daCartItem = new SqlDataAdapter(str, DB.cn);
            daCartItem.Fill(ds, "CartItem_info");

            string sdr = "select * from Product_Table";
            daProduct = new SqlDataAdapter(sdr, DB.cn);
            daProduct.Fill(ds, "product_info");

        }



        public string qty;
        private void button2_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(textBox6.Text) >= Convert.ToInt32(label11.Text))
            {
                return;
            }

            qty = (Convert.ToInt32( textBox6.Text)+1).ToString();
            textBox6 .Text = qty;

            DB.GetCn();
            // 使用DB类中的GetDataSet方法来获取数据
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.selectedGoodsID + "'";
            DataTable dt = DB.GetDataSet(sql);

            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row = dt.Rows[0];


                decimal price = Convert.ToDecimal(row["Price"]);
                decimal textBoxValue = Convert.ToDecimal(textBox6.Text);

                // 去除小数点后两位
                string priceStr = price.ToString("F0"); // F0 格式化字符串，不显示小数点后的数字

                // 将去除小数后的字符串转换回 decimal 类型
                decimal priceWithoutDecimal = Convert.ToDecimal(priceStr);

                // 执行乘法操作
                decimal result = priceWithoutDecimal * textBoxValue + Convert.ToInt32(label14.Text);

                // 设置 label9 的文本，保留两位小数
                label9.Text = "￥" + result.ToString("F2");
                label9.Font = new Font(label9.Font.FontFamily, 26, FontStyle.Bold);
                label9.ForeColor = Color.Red;
                label9.BackColor = Color.Transparent;

            }
            else
            {
                MessageBox.Show("未找到数据。");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (Convert.ToInt32(textBox6.Text)-1 <= 0)
            {
                return;
            }
            qty = (Convert.ToInt32(textBox6.Text) - 1).ToString();
            textBox6.Text = qty;

            DB.GetCn();
            // 使用DB类中的GetDataSet方法来获取数据
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.selectedGoodsID + "'";
            DataTable dt = DB.GetDataSet(sql);

            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row = dt.Rows[0];

                decimal price = Convert.ToDecimal(row["Price"]);
                decimal textBoxValue = Convert.ToDecimal(textBox6.Text);
                string priceStr = price.ToString("F0"); 
                decimal priceWithoutDecimal = Convert.ToDecimal(priceStr);
                decimal result = priceWithoutDecimal * textBoxValue + Convert.ToInt32(label14.Text);

                label9.Text = "￥" + result.ToString("F2");
                label9.Font = new Font(label9.Font.FontFamily, 26, FontStyle.Bold);
                label9.ForeColor = Color.Red;
                label9.BackColor = Color.Transparent;

            }
            else
            {
                MessageBox.Show("未找到数据。");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB.GetCn();
            string str = "insert into Order_Table values('" + Login.StrValue + "','" + DateTime.Today + "','" + textBox3.Text  + "','"
                + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','"
                + textBox6.Text + "','待发货', null)";// ShopCart.profit +
            //UpdateStockQuantity();
            //ShopCart.profit = 0;
            DB.sqlEx(str);
            MessageBox.Show("已经结算咯");

        }

   

       
    }
}

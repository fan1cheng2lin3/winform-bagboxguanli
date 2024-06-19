using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BoxSystemMange
{
    public partial class houtaichuangti : Form
    {
        public houtaichuangti()
        {
            InitializeComponent();
        }


        SqlDataAdapter daProduct, daLog,dazhanshi;
        DataSet ds = new DataSet();
        public static string path_source = "";

        void init()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            string str2 = "select Goods_ID,Goods_Name,Classification_ID,Supplier_ID,Unit_Price,Order_Quantity,Stock_Quantity,Price from Product_Table";
            string sdr = "select * from Log_Table";
            daProduct = new SqlDataAdapter(str, DB.cn);
            daLog = new SqlDataAdapter(sdr, DB.cn);
            dazhanshi = new SqlDataAdapter(str2, DB.cn);
            daProduct.Fill(ds, "product_info");
            dazhanshi.Fill(ds, "product_info");
            daLog.Fill(ds, "log_info");

        }

        void showAll()
        {
            DataView dvProduct = new DataView(ds.Tables["product_info"]);
            dataGridView1.DataSource = dvProduct;
        }


        private void houtaichuangti_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet3.Designshi_Table”中。您可以根据需要移动或移除它。
            this.designshi_TableTableAdapter.Fill(this.boxbagManageSystemDataSet3.Designshi_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet1.Category_Table”中。您可以根据需要移动或移除它。
            this.category_TableTableAdapter.Fill(this.boxbagManageSystemDataSet1.Category_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet.Product_Table”中。您可以根据需要移动或移除它。
            this.product_TableTableAdapter.Fill(this.boxbagManageSystemDataSet.Product_Table);


            //if (frm == null || string.IsNullOrEmpty(frm.Text))
            //{
            //    frm = new zhuye();
            //    frm.Show();
            //}
            //else
            //{
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.BringToFront();
            //}
            init();
            showAll();
            tabControl1.TabPages[0].Text = "增加商品信息";
            tabControl1.TabPages[1].Text = "修改商品信息";
            tabControl1.TabPages[2].Text = "删除商品信息";





            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 设置窗体边框样式
            this.MaximizeBox = false; // 禁用最大化按钮
            this.MinimizeBox = false; // 禁用最小化按钮
            this.ResizeRedraw = false; // 禁止重绘操作
            this.ClientSize = new Size(this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            this.ClientSize = new Size(this.ClientSize.Width + 1, this.ClientSize.Height + 1);
            

        }
        public static Form frm;
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_source = ofd.FileName;
                pictureBox1.Image = Image.FromFile(path_source);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            
            
        }


        public void savetupian(string imagePath)
        {
            // 查询当前商品的图片数量
            string selectQuery = "SELECT im1, im2, im3, im4 FROM Product_Table WHERE Goods_ID = @Goods_ID";
            string updateQuery = "";

            using (SqlConnection connection = DB.GetCn())
            {

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Goods_ID", textBox1.Text);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader["im1"] == DBNull.Value)
                        {
                            updateQuery = "UPDATE Product_Table SET im1 = @im WHERE Goods_ID = @Goods_ID";
                        }
                        else if (reader["im2"] == DBNull.Value)
                        {
                            updateQuery = "UPDATE Product_Table SET im2 = @im WHERE Goods_ID = @Goods_ID";
                        }
                        else if (reader["im3"] == DBNull.Value)
                        {
                            updateQuery = "UPDATE Product_Table SET im3 = @im WHERE Goods_ID = @Goods_ID";
                        }
                        else if (reader["im4"] == DBNull.Value)
                        {
                            updateQuery = "UPDATE Product_Table SET im4 = @im WHERE Goods_ID = @Goods_ID";
                        }
                        else
                        {
                            MessageBox.Show("已达到图片存储上限。");
                            return;
                        }
                    }
                    reader.Close();
                }

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@im", imagePath);
                    updateCommand.Parameters.AddWithValue("@Goods_ID", textBox1.Text);

                    try
                    {
                        int result = updateCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存图片失败：" + ex.Message);
                    }
                }
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path_source = ofd.FileName;

                // 创建一个新的PictureBox
                PictureBox newPictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage, // 设置图片显示模式
                    Image = Image.FromFile(path_source), // 加载图片
                    Size = new Size(170, 160),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // 将PictureBox添加到flowLayoutPanel2中
                flowLayoutPanel2.Controls.Add(newPictureBox);
                newPictureBox.Dock = DockStyle.Top;

                foreach (Control ctrl in flowLayoutPanel2.Controls)
                {
                    if (ctrl is Button)
                    {
                        ctrl.SendToBack();
                    }
                }

                string filename = Path.GetFileName(path_source);
                string fileFolder = Directory.GetCurrentDirectory() + "\\Prod_Images\\";

                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string newFilename = dateTime + Path.GetExtension(filename);

                // 完整的新文件路径
                string newFilePath = Path.Combine(fileFolder, newFilename);

                // 复制文件到新路径
                File.Copy(path_source, newFilePath);

                string dbImagePath = "Prod_Images\\" + newFilename;

                savetupian(dbImagePath);
            }

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox2.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("商品名称，成本价格，库存不能为空");
            }
            else
            {
                DialogResult dr = MessageBox.Show("您确定要修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    string filename;
                    string fileFolder;
                    string dateTime = "";
                    filename = Path.GetFileName(path_source);
                    dateTime += DateTime.Now.Year.ToString();
                    dateTime += DateTime.Now.Month.ToString("00");
                    dateTime += DateTime.Now.Day.ToString("00");
                    dateTime += DateTime.Now.Hour.ToString("00");
                    dateTime += DateTime.Now.Minute.ToString("00");
                    dateTime += DateTime.Now.Second.ToString("00");
                    filename = dateTime + filename;
                    fileFolder = Directory.GetCurrentDirectory() + "\\" + "Prod_Images\\"
                        +  "\\";
                    fileFolder += filename;

                    int a = dataGridView1.CurrentRow.Index;
                    string str = dataGridView1.Rows[a].Cells["Goods_ID"].Value.ToString();
                    DataRow[] ProRows = ds.Tables["product_info"].Select("Goods_ID='" + str + "'");
                    DataRow[] ProRows2 = ds.Tables["product_info"].Select("Goods_ID='" + str + "'");
                    ProRows[0]["Classification_ID"] = comboBox1.SelectedValue;
                    ProRows[0]["Supplier_ID"] = comboBox2.SelectedValue;
                    ProRows[0]["Price"] = decimal.Parse(textBox3.Text.Trim());
                    ProRows[0]["Unit_Price"] = decimal.Parse(textBox2.Text.Trim());
                    ProRows[0]["Stock_Quantity"] = Convert.ToInt32( textBox5.Text.Trim());
                    ProRows[0]["Goods_Name"] = textBox4.Text.Trim();

                    if (path_source != "")
                    {

                        ProRows[0]["image"] = "Prod_Images\\" + filename; 
                        File.Copy(path_source, fileFolder, true);
                    }

                    DataRow drLog = ds.Tables["log_info"].NewRow();


                    drLog["username"] = Login.StrValue;
                    drLog["type"] = "修改";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);
                    try
                    {
                        SqlCommandBuilder dbProuct = new SqlCommandBuilder(daProduct);
                        daProduct.Update(ds, "product_info");

                        SqlCommandBuilder dbProuct2 = new SqlCommandBuilder(dazhanshi);
                        dazhanshi.Update(ds, "product_info");
                        SqlCommandBuilder dbLog = new SqlCommandBuilder(daLog);
                        daLog.Update(ds, "log_info");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    DB.cn.Close();
                }
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["Goods_ID"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Classification_ID"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Unit_Price"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["Supplier_ID"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Goods_Name"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["Order_Quantity"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Stock_Quantity"].Value.ToString();

            // 清空 flowLayoutPanel2 现有的控件
            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                if (!(flowLayoutPanel2.Controls[i] is Button))
                {
                    flowLayoutPanel2.Controls.RemoveAt(i);
                }
            }

            string[] imageColumns = { "im1", "im2", "im3", "im4" };

            foreach (string column in imageColumns)
            {
                string imagePath = dataGridView1.CurrentRow.Cells[column].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    PictureBox newPictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = Image.FromFile(imagePath),
                        Size = new Size(170, 160),
                        BorderStyle = BorderStyle.FixedSingle,
                        Dock = DockStyle.Top
                    };

                    // 将 PictureBox 添加到 flowLayoutPanel2 中
                    flowLayoutPanel2.Controls.Add(newPictureBox);
                }
            }

            // 处理 flowLayoutPanel2 中的按钮
            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.SendToBack();
                }
            }

            // 设置主图片
            try
            {
                pictureBox1.Image = Image.FromFile(dataGridView1.CurrentRow.Cells["image"].Value.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }
        }

    }
}

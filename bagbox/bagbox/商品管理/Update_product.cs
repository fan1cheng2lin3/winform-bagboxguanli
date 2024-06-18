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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace bagbox.商品管理
{
    public partial class Update_product : Form
    {
        public Update_product()
        {
            InitializeComponent();
        }


        SqlDataAdapter daProduct, daLog;
        DataSet ds = new DataSet();
        public static string path_source = "";

        void init()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            string sdr = "select * from Log_Table";
            daProduct = new SqlDataAdapter(str,DB.cn);
            daLog = new SqlDataAdapter(sdr,DB.cn);
            daProduct.Fill(ds,"product_info");
            daLog.Fill(ds,"log_info");

        }

        void showAll()
        {
            DataView dvProduct = new DataView(ds.Tables["product_info"]);
            dataGridView1.DataSource = dvProduct;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["Goods_ID"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Classification_ID"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Unit_Price"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Goods_Name"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["Order_Quantity"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Stock_Quantity"].Value.ToString();
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath
                    + dataGridView1.CurrentRow.Cells["image"].Value.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            }
            catch {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                path_source = ofd.FileName;
                pictureBox1.Image = Image.FromFile(path_source);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox4.Text == ""|| textBox2.Text == ""|| textBox5.Text == "")
            {
                MessageBox.Show("商品名称，成本价格，库存不能为空");
            }
            else
            {
                DialogResult dr =MessageBox.Show("您确定要修改吗？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                if(dr == DialogResult.OK)
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
                        + comboBox1.SelectedText + "\\";
                    fileFolder += filename;

                    int a = dataGridView1.CurrentRow.Index;
                    string str = dataGridView1.Rows[a].Cells["Goods_ID"].Value.ToString();
                    DataRow[] ProRows = ds.Tables["product_info"].Select("Goods_ID='" + str + "'");
                    ProRows[0]["Classification_ID"] = comboBox1.SelectedValue;
                    ProRows[0]["Supplier_ID"] = comboBox2.SelectedValue;
                    ProRows[0]["Price"] = decimal.Parse(textBox3.Text.Trim());
                    ProRows[0]["Unit_Price"] = decimal.Parse(textBox2.Text.Trim());
                    ProRows[0]["Order_Quantity"] = textBox6.Text.Trim();
                    ProRows[0]["Goods_Name"] = textBox4.Text.Trim();

                    if (path_source !="")
                    {
                        File.Copy(path_source, fileFolder, true);
                        ProRows[0]["image"] = "\\Prod_Images\\" + comboBox1.SelectedText + "\\"
                            + filename;
                    }
                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = Login.StrValue11;
                    drLog["type"] = "修改";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);
                    try
                    {
                        SqlCommandBuilder dbProuct = new SqlCommandBuilder(daProduct);
                        daProduct.Update(ds, "product_info");
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Update_product_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet6.Design_Table”中。您可以根据需要移动或移除它。
            this.design_TableTableAdapter.Fill(this.boxbagManageSystemDataSet6.Design_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet5.Category_Table”中。您可以根据需要移动或移除它。
            this.category_TableTableAdapter.Fill(this.boxbagManageSystemDataSet5.Category_Table);


            init();
            showAll();
        }

        
    }
}

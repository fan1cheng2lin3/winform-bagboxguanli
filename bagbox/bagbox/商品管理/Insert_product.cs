using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace bagbox.商品管理
{
    public partial class Insert_product : Form
    {
        public Insert_product()
        {
            InitializeComponent();
        }

        public static string path_source = "";
        SqlDataAdapter daProduct, dalog;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            string sdr = "select * from Log_Table";
            daProduct = new SqlDataAdapter(str, DB.cn);
            dalog = new SqlDataAdapter(sdr, DB.cn);
            daProduct.Fill(ds, "product_info");
            dalog.Fill(ds, "log_info");

        }

        void showAll()
        {
            DataView dvProduct = new DataView(ds.Tables["product_info"]);
            dataGridView1.DataSource = dvProduct;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

      

        private void Insert_product_Load(object sender, EventArgs e)
        {

            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet1.Design_Table”中。您可以根据需要移动或移除它。
            this.design_TableTableAdapter.Fill(this.boxbagManageSystemDataSet1.Design_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet.Category_Table”中。您可以根据需要移动或移除它。
            this.category_TableTableAdapter.Fill(this.boxbagManageSystemDataSet.Category_Table);
            init();
            showAll();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                path_source = dlg.FileName;
                pictureBox1.Image = Image.FromFile(path_source);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == ""|| textBox2.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("必填不能为空");
            }
            else
            {
                DB.GetCn();
                string str = "select * from Product_Table where Goods_ID ='" + textBox1.Text + "'";
                DataTable dt = DB.GetDataSet(str);
                if (dt.Rows.Count>0)
                {
                    MessageBox.Show("此商品已存在，请重新输入编号");
                }
                else
                {

                    string filename;
                    string fileFolder;
                    string dateTime = "";
                    dateTime += DateTime.Now.Year.ToString();
                    dateTime += DateTime.Now.Month.ToString("00"); 
                    dateTime += DateTime.Now.Day.ToString("00");
                    dateTime += DateTime.Now.Hour.ToString("00");
                    dateTime += DateTime.Now.Minute.ToString("00");
                    dateTime += DateTime.Now.Second.ToString("00");
                    filename = Path.GetFileName(path_source);
                    filename = dateTime + filename;
                    fileFolder = Directory.GetCurrentDirectory() + "\\" + "Prod_Images\\"
                        + comboBox1.Text + "\\";
                    fileFolder += filename;


                    DataRow drPro = ds.Tables["product_info"].NewRow();




                    //drPro["ProductID"] = Convert.ToInt32(textBox1.Text);
                    drPro["Goods_ID"] = int.Parse(textBox1.Text);
                    drPro["Classification_ID"] = comboBox1.SelectedValue;
                    drPro["Unit_Price"] = decimal.Parse(textBox3.Text);
                    drPro["Price"] = decimal.Parse(textBox2.Text);
                    drPro["Supplier_ID"] = comboBox2.SelectedValue;
                    drPro["Goods_Name"] = textBox4.Text;
                    drPro["Order_Quantity"] = textBox6.Text;
                    drPro["Stock_Quantity"] = textBox5.Text;


                  
                    if (path_source != "")
                    {
                        File.Copy(path_source, fileFolder, true);
                        drPro["image"] = "\\Prod_Images\\" + comboBox1.Text
                            + "\\" + filename;
                    }
                    else
                    {
                        drPro["image"] = "780.jpg";
                    }


                    ds.Tables["product_info"].Rows.Add(drPro);


                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = Login.StrValue11;
                    drLog["type"] = "添加";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);

                    
                    
                    try
                    {
                        SqlCommandBuilder dbProuct = new SqlCommandBuilder(daProduct);
                        daProduct.Update(ds, "product_info");
                        SqlCommandBuilder dbLog = new SqlCommandBuilder(dalog);
                        dalog.Update(ds, "log_info");
                        MessageBox.Show("增加成功");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    DB.cn.Close();
                }
                
            }
        }
    }
}

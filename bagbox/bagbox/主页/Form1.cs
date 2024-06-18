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
using bagbox.前端窗口;
using bagbox.商品管理;

namespace bagbox
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        public static int flag = 1;


        public static string Productid = "";
        public static bool Aflog = false;
        void showAllProduct()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            SqlCommand cmd = new SqlCommand(str, DB.cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int index = dgvPriduct.Rows.Add();
                dgvPriduct.Rows[index].Cells[0].Value = rdr[0];
                dgvPriduct.Rows[index].Cells[1].Value = rdr[1];
                dgvPriduct.Rows[index].Cells[2].Value = rdr[2];
                dgvPriduct.Rows[index].Cells[3].Value = rdr[3];
                dgvPriduct.Rows[index].Cells[4].Value = rdr[4];
                dgvPriduct.Rows[index].Cells[5].Value = rdr[5];
                dgvPriduct.Rows[index].Cells[6].Value = rdr[6];
                try
                {
                    Image imageColum = Image.FromFile(Application.StartupPath + rdr[8]);
                    dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                }
                catch
                {
                    Image imageColum = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
                    dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                }
            }
            rdr.Close();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Login rl = new Login();
            rl.ShowDialog();
            label4.Visible = true;
            label4.Text = Login.StrValue;
            //Front t1 = new Front();
            //t1.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register rl = new Register();
            rl.ShowDialog();
            //Backend t1 = new Backend();
            //t1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dgvPriduct.Rows.Clear();
            showAllProduct();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = Login.StrValue;
            dgvPriduct.RowTemplate.Height = 90;
            showAllProduct();

        }

        /// <summary>
        /// 按商品名称关键字查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch1_Click(object sender, EventArgs e)
        {
            if (txtPName.Text == "")
            {
                MessageBox.Show("请输入查询关键字");
            }
            else
            {
                dgvPriduct.Rows.Clear();
                DB.GetCn();
                string str= "select * from Product_Table where Goods_Name like '%" + txtPName.Text+"%'";
                SqlCommand cmd = new SqlCommand(str,DB.cn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int index = dgvPriduct.Rows.Add();
                    dgvPriduct.Rows[index].Cells[0].Value = rdr[0];
                    dgvPriduct.Rows[index].Cells[1].Value = rdr[1];
                    dgvPriduct.Rows[index].Cells[2].Value = rdr[2];
                    dgvPriduct.Rows[index].Cells[3].Value = rdr[3];
                    dgvPriduct.Rows[index].Cells[4].Value = rdr[4];
                    dgvPriduct.Rows[index].Cells[5].Value = rdr[5];
                    dgvPriduct.Rows[index].Cells[6].Value = rdr[6];
                    try
                    {
                        Image imageColum = Image.FromFile(Application.StartupPath + rdr[8]);
                        dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                    }
                    catch
                    {
                        Image imageColum = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
                        dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                    }

                }
                rdr.Close();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 按产品价格区间查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch2_Click(object sender, EventArgs e)
        {
            dgvPriduct.Rows.Clear();
            string str = "";
            DB.GetCn();
            int i = comboBox1.SelectedIndex;
            switch (i)
            {
                case 0:
                    str = "Unit_Price<=50";
                    break;
                case 1:
                    str = "Unit_Price>50 and Unit_Price<=100";
                    break;
                case 2:
                    str = "Unit_Price>100 and Unit_Price<=200";
                    break;
                case 3:
                    str = "Unit_Price>200 and Unit_Price<=500";
                    break;
                case 4:
                    str = "Unit_Price>500 and Unit_Price<=1000";
                    break;
                default:
                    str = "Unit_Price>=1000";
                    break;
            }
            string sdr = "select * from Product_Table where " + str;
            SqlCommand cmd2 = new SqlCommand(sdr, DB.cn);
            SqlDataReader rdr = cmd2.ExecuteReader();
            while (rdr.Read())
            {
                int index = dgvPriduct.Rows.Add();
                dgvPriduct.Rows[index].Cells[0].Value = rdr[0];
                dgvPriduct.Rows[index].Cells[1].Value = rdr[1];
                dgvPriduct.Rows[index].Cells[2].Value = rdr[2];
                dgvPriduct.Rows[index].Cells[3].Value = rdr[3];
                dgvPriduct.Rows[index].Cells[4].Value = rdr[4];
                dgvPriduct.Rows[index].Cells[5].Value = rdr[5];
                dgvPriduct.Rows[index].Cells[6].Value = rdr[6];
                try
                {
                    Image imageColum = Image.FromFile(Application.StartupPath + rdr[8]);
                    dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                }
                catch
                {
                    Image imageColum = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
                    dgvPriduct.Rows[index].Cells["Column8"].Value = imageColum;

                }
            }
            rdr.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShopCart t2 = new ShopCart();
            t2.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvPriduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Cindex = e.ColumnIndex;
            if (Cindex == 8)
            {
                int rowindex = e.RowIndex;
                Productid = dgvPriduct.Rows[rowindex].Cells[0].Value.ToString();
                Tproducts t1 = new Tproducts();
                t1.ShowDialog();
            }

            if(Cindex == 9)
            {

                if(Aflog == false)
                {
                    MessageBox.Show("请先登录");
                    Login t1 = new Login();
                    t1.ShowDialog();
                    label4.Text = Login.StrValue;
                }
                else if(Aflog == true) 
                {
                    int rowindex = e.RowIndex;
                    Productid = dgvPriduct.Rows[rowindex].Cells[0].Value.ToString();
                    if (Convert.ToInt32(dgvPriduct.Rows[rowindex].Cells[6].Value) < 1)
                    {
                        MessageBox.Show("该商品暂时缺货");

                    }
                    else
                    {
                        ShopCart t2 = new ShopCart();
                        t2.ShowDialog();
                    }

                }
            }
        }
    }
}

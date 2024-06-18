using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bagbox
{
    public partial class Tproducts : Form
    {
        public Tproducts()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Tproducts_Load(object sender, EventArgs e)
        {
            DB.GetCn();
            string str = "select * from Product_Table where Goods_ID = " + Form1.Productid+ "";
            DataTable dt = DB.GetDataSet(str);

            label10.Text = Form1.Productid;
            label7.Text = dt.Rows[0][1].ToString();
            label11.Text = dt.Rows[0][2].ToString();
            label13.Text = dt.Rows[0][3].ToString();
            label12.Text = dt.Rows[0][4].ToString();
            label15.Text = dt.Rows[0][5].ToString();
            label16.Text = dt.Rows[0][6].ToString();

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + dt.Rows[0][8].ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch {

                pictureBox1.Image = Image.FromFile(Application.StartupPath + "//"+"780.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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

namespace bagbox
{
    public partial class SubCart : Form
    {
        public SubCart()
        {
            InitializeComponent();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.GetCn();
            string str = "insert into Order_Table values('" + Login.StrValue + "','" + DateTime.Today + "','" + textBox1.Text + "','" 
                + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text 
                + "','未审核', null)";// ShopCart.profit +
            //UpdateStockQuantity();
            ShopCart.profit = 0;
            DB.sqlEx(str);
            MessageBox.Show("已经结算咯");
            
            this.Close();
        }

        
        private void SubCart_Load(object sender, EventArgs e)
        {

        }
    }
}

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

namespace BoxSystemMange.管理
{
    public partial class houtaidingdan : Form
    {
        public houtaidingdan()
        {
            InitializeComponent();
        }


        SqlDataAdapter daorder, daorder2;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GetCn();
            string str = "select productID,UserName,OrderDate,Status,Status2, Employeeld from Order_Table where Status2 = '申请退货' and Status = '已收货' ";
            string str2 = "select productID,UserName,OrderDate,Status,Status2, Employeeld from Order_Table where Status2 = '申请取消' and Status = '待发货' ";
            daorder = new SqlDataAdapter(str, DB.cn);
            daorder2 = new SqlDataAdapter(str2, DB.cn);
            daorder.Fill(ds, "order_info");
            daorder2.Fill(ds, "order2_info");


            DataView dvOrder = new DataView(ds.Tables["order_info"]);
            DataView dvOrder2 = new DataView(ds.Tables["order2_info"]);
            dataGridView1.DataSource = dvOrder;
            dataGridView2.DataSource = dvOrder2;
        }



        void showAll()
        {
            init();
        }

        void showXz()
        {

            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn acCode2 = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode2.Name = "acCode";
            acCode.HeaderText = "选择";
            acCode2.HeaderText = "选择";
            dataGridView1.Columns.Add(acCode);
            dataGridView2.Columns.Add(acCode2);
        }

        private void houtaidingdan_Load(object sender, EventArgs e)
        {
            showXz();
            showAll();
            
        }

        private void dgvPriduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells["acCode"] as DataGridViewCheckBoxCell;
                    if (i != e.RowIndex)
                    {
                        ck.Value = false;
                    }
                    else
                    {
                        ck.Value = true;
                    }
                }

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int s = dataGridView2.Rows.Count;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
                {
                    dataGridView2.Rows[i].Cells["Status"].Value = "已取消";
                    if (string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["Employeeld"].Value?.ToString()))
                    {
                        dataGridView2.Rows[i].Cells["Employeeld"].Value = Login.Employee_Id;

                    }
                    UpdateDB();
                }
                else
                {
                    s = s - 1;

                }

            }
            if (s == 0)
            {
                MessageBox.Show("请选择要审核的项");
            }
        }

        void UpdateDB()
        {
            try
            {
                SqlCommandBuilder dbOrder = new SqlCommandBuilder(daorder);
                SqlCommandBuilder dbOrder2 = new SqlCommandBuilder(daorder2);
                daorder.Update(ds, "Order_info");
                daorder2.Update(ds, "Order2_info");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int s = dataGridView1.Rows.Count;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
                {
                    dataGridView1.Rows[i].Cells["Status"].Value = "已取消";
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Employeeld"].Value?.ToString()))
                    {
                        dataGridView1.Rows[i].Cells["Employeeld"].Value = Login.Employee_Id;
                    }
                    UpdateDB();
                }
                else
                {
                    s = s - 1;

                }

            }
            if (s == 0)
            {
                MessageBox.Show("请选择要审核的项");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}

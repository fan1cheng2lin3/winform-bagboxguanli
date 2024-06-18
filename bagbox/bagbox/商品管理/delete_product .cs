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
    public partial class delete_product : Form
    {
        public delete_product()
        {
            InitializeComponent();
        }



        SqlDataAdapter daProdout, dalog;
        DataSet ds = new DataSet();
        void init()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            string sdr = "select * from Log_Table";
            daProdout = new SqlDataAdapter(str, DB.cn);
            dalog = new SqlDataAdapter(sdr, DB.cn);
            daProdout.Fill(ds, "product_info");
            dalog.Fill(ds, "log_info");
        }

        void ShowAll()
        {
            DataView dvProduct = new DataView(ds.Tables["product_info"]);
            dataGridView1.DataSource = dvProduct;
        }



        void showXz()
        {
            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode.HeaderText = "选择";
            dataGridView1.Columns.Add(acCode);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

    

      

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }




        private void button1_Click_1(object sender, EventArgs e)
        {
            int s = dataGridView1.Rows.Count;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["acCode"].EditedFormattedValue.ToString() == "True")
                {
                    DialogResult dr = MessageBox.Show("你确认要删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        DB.GetCn();
                        DataRow drLog = ds.Tables["log_info"].NewRow();
                        drLog["username"] = Login.StrValue11;
                        drLog["type"] = "删除";
                        drLog["action_date"] = DateTime.Now;
                        drLog["action_table"] = "product表";
                        ds.Tables["log_info"].Rows.Add(drLog);

                        UpdateDB();
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    s = s - 1;
                }
            }
            if (s == 0)
            {
                MessageBox.Show("请选择要删除的项");
            }
        }


        void UpdateDB()
        {
            try
            {
                SqlCommandBuilder dbProduct = new SqlCommandBuilder(daProdout);
                daProdout.Update(ds, "product_info");

                SqlCommandBuilder dbLog = new SqlCommandBuilder(dalog);
                dalog.Update(ds, "log_info");

                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除操作失败：" + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void delete_product_Load(object sender, EventArgs e)
        {
            showXz();
            init();
            ShowAll();
            
        }
    }
}

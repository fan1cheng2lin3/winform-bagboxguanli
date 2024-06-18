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

namespace bagbox
{
    public partial class Select_Order : Form
    {
        public Select_Order()
        {
            InitializeComponent();
        }

        public int pageSize = 15;
        public int recordCount = 0;
        public int pageCount = 0;
        public int currentPage = 1;

        SqlDataAdapter daLog;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GetCn();
            string str = "select * from Log_Table";
            daLog = new SqlDataAdapter(str, DB.cn);
            daLog.Fill(ds, "log_info");

        }

        void showAll()
        {
            DataView dvLog = new DataView(ds.Tables["log_info"]);
            dataGridView1.DataSource = dvLog;
        }

        private void Select_Order_Load(object sender, EventArgs e)
        {
            init();
            LoadPage();

        }



        private void LoadPage()
        {
            recordCount = ds.Tables["log_info"].Rows.Count;
            pageCount = recordCount / pageSize;
            if((recordCount % pageSize) > 0)
            {
                pageCount++;

            }
            if(currentPage<1) currentPage = 1;
            if(currentPage>pageCount) currentPage = pageCount;

            int beginRecord;
            int endRecord;
            DataTable dt = ds.Tables["log_info"];
            DataTable st = new DataTable();
            st = dt.Clone();
            beginRecord = pageSize *(currentPage - 1);
            endRecord = currentPage * pageSize;


            if(currentPage == pageCount)
                endRecord = recordCount;

            for(int i = beginRecord; i < endRecord; i++)
            {
                st.ImportRow(dt.Rows[i]);
            }
            dataGridView1.DataSource = st;
            label1.Text = currentPage.ToString();
            label2.Text = pageCount.ToString();
            label3.Text = recordCount.ToString();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentPage = pageSize;
            LoadPage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentPage--;
            LoadPage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadPage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

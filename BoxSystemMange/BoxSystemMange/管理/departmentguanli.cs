using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BoxSystemMange.管理
{
    public partial class departmentguanli : Form
    {
        public departmentguanli()
        {
            InitializeComponent();
        }

        private void departmentguanli_Load(object sender, EventArgs e)
        {
            
            tabControl1.TabPages[0].Text = "增加部门信息";
            tabControl1.TabPages[1].Text = "修改部门信息";
            tabControl1.TabPages[2].Text = "删除部门信息";

            showXz();
            init();
            showALL();
            dgvHead();
        }


        SqlDataAdapter daDepartment, daLog;
        DataSet ds = new DataSet();

        void init()
        {
            DB.GetCn();
            string str = "select * from department_Table";
            string str2 = "select * from Log_Table";
            daDepartment = new SqlDataAdapter(str, DB.cn);
            daLog = new SqlDataAdapter(str2, DB.cn);
            daDepartment.Fill(ds, "department_info");
            daLog.Fill(ds, "log_info");
            
        }

        void showALL()
        {
            DataView dvDepartment = new DataView(ds.Tables["department_info"]);
            dataGridView1.DataSource = dvDepartment;
            dataGridView1.Columns[3].Width = 100;
            dataGridView2.DataSource = dvDepartment;
            dataGridView2.Columns[3].Width = 100;
            dataGridView3.DataSource = dvDepartment;
            dataGridView3.Columns[3].Width = 100;

        }

        void dgvHead()
        {
            this.dataGridView1.Columns[0].HeaderText = "部门编号";
            this.dataGridView1.Columns[1].HeaderText = "部门名称";
            this.dataGridView1.Columns[2].HeaderText = "部门经理";
            this.dataGridView1.Columns[3].HeaderText = "部门描述";

            this.dataGridView2.Columns[0].HeaderText = "部门编号";
            this.dataGridView2.Columns[1].HeaderText = "部门名称";
            this.dataGridView2.Columns[2].HeaderText = "部门经理";
            this.dataGridView2.Columns[3].HeaderText = "部门描述";

            this.dataGridView3.Columns[1].HeaderText = "部门编号";
            this.dataGridView3.Columns[2].HeaderText = "部门名称";
            this.dataGridView3.Columns[3].HeaderText = "部门经理";
            this.dataGridView3.Columns[4].HeaderText = "部门描述";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("部门编号或部门名称不能为空");
            }
            else
            {
                DB.GetCn();
                DialogResult dr = MessageBox.Show("你确定要添加吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    string s_id = "select * from department_Table where Department_id='" + txtId.Text + "'";
                    DataTable dt1 = DB.GetDataSet(s_id);

                    string s_name = "select * from department_Table where Department_id='" + txtName.Text + "'";
                    DataTable dt2 = DB.GetDataSet(s_name);

                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("该部门编号已存在，请重新输入");
                    }
                    else if (dt2.Rows.Count > 0)
                    {
                        MessageBox.Show("该部门名称已存在，请重新输入");
                    }
                    else
                    {
                        DataRow DepRow = ds.Tables["department_info"].NewRow();

                        DepRow["Department_id"] = txtId.Text.Trim();
                        DepRow["Department_name"] = txtName.Text.Trim();
                        DepRow["Manager_ld"] = txtManager.Text.Trim();
                        DepRow["Department_description"] = txtDescn.Text.Trim();

                        ds.Tables["department_info"].Rows.Add(DepRow);


                        // 使用存储过程添加日志信息
                        SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("type", SqlDbType.NVarChar));
                        cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.DateTime));
                        cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.NVarChar));

                        cmd.Parameters["username"].Value = houtaidenglu.StrValue;
                        cmd.Parameters["type"].Value = "添加";
                        cmd.Parameters["action_date"].Value = DateTime.Now;
                        cmd.Parameters["action_table"].Value = "department表";

                        try
                        {
                            SqlCommandBuilder dbDepartment = new SqlCommandBuilder(daDepartment);
                            daDepartment.Update(ds, "department_info");
                            cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("这个是添加" + ex.Message);
                        }
                        finally
                        {
                            DB.cn.Close();
                        }
                    }
                }
                else
                {
                    return;
                }

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("部门名称不能为空");
            }
            else
            {
                DB.GetCn();
                DialogResult dr = MessageBox.Show("你确定要修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.OK)
                {
                    int a = dataGridView2.CurrentRow.Index;
                    string str = dataGridView2.Rows[a].Cells["Department_id"].Value.ToString();
                    DataRow[] CustRows = ds.Tables["department_info"].Select("Department_id='" + str + "'");
                    CustRows[0]["Department_id"] = textBox4.Text.Trim();
                    CustRows[0]["Department_name"] = textBox1.Text.Trim();
                    CustRows[0]["Manager_ld"] = textBox3.Text.Trim();
                    CustRows[0]["Department_description"] = textBox2.Text.Trim();
                    MessageBox.Show("修改成功");


                    // 使用存储过程修改日志信息
                    SqlCommand cmd = new SqlCommand("add_log", DB.cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("username", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("type", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("action_date", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("action_table", SqlDbType.NVarChar));

                    cmd.Parameters["username"].Value = houtaidenglu.StrValue;
                   
                    cmd.Parameters["type"].Value = "修改";
                    cmd.Parameters["action_date"].Value = DateTime.Now;
                    cmd.Parameters["action_table"].Value = "bumenbiao";


                    try
                    {
                        SqlCommandBuilder dbProuct = new SqlCommandBuilder(daDepartment);
                        daDepartment.Update(ds, "department_info");
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("这个是修改"+ex.Message);
                    }
                    finally
                    {
                        DB.cn.Close();
                    }

                }
                else
                {
                    return;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells["Department_id"].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["Department_name"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Manager_ld"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Department_description"].Value.ToString();
        }

        void showXz()
        {
            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode.HeaderText = "选择";
            dataGridView3.Columns.Add(acCode);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell ck = dataGridView3.Rows[i].Cells["acCode"] as DataGridViewCheckBoxCell;
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // 遍历 dataGridView2 中的每一行，反转复选框的选中状态
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;

                // 反转复选框的选中状态
                chk.Value = !(bool)chk.Value;
            }

            radioButton1.Visible = true;
            radioButton2.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


            // 遍历 dataGridView2 中的每一行，切换复选框的选中状态
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;

                // 设置复选框的选中状态为 true
                chk.Value = true;
            }
            radioButton1.Visible = false;
            radioButton2.Visible = true;
        }

        public bool dsa;

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (dsa == false)
            {
                // 遍历 dataGridView2 中的每一行，反转复选框的选中状态
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;

                    // 反转复选框的选中状态
                    chk.Value = !(bool)chk.Value;
                }
                dsa = true;
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            dsa = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;


                if (Convert.ToBoolean(chk.Value))
                {
                    selectedCount++;

                    // 将要删除的行添加到集合中
                    rowsToDelete.Add(row);

                    // 添加日志记录
                    DB.GetCn();
                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = houtaidenglu.StrValue;
                    drLog["type"] = "删除";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);
                    MessageBox.Show("删除成功");


                }
            }


            // 执行集合中的行删除
            foreach (DataGridViewRow row in rowsToDelete)
            {
                dataGridView3.Rows.Remove(row);
            }


            try
            {
                // 更新数据库
                SqlCommandBuilder dbProduct = new SqlCommandBuilder(daDepartment);
                daDepartment.Update(ds, "department_info");

                SqlCommandBuilder dbLog = new SqlCommandBuilder(daLog);
                daLog.Update(ds, "log_info");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除操作失败：" + ex.Message);
            }



            // 如果没有选中任何行，显示提示消息
            if (selectedCount == 0)
            {
                MessageBox.Show("请选择要删除的项");
            }


            
        }
    }
}

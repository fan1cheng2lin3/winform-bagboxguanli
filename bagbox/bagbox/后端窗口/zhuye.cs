using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bagbox.DB;
using System.Data.OleDb;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bagbox
{
    public partial class zhuye : Form
    {
        public zhuye()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       

        //public void UpdateEmployeeField(string fieldName, string value, string employeeName, System.Windows.Forms.Label label)
        //{
        //    if (value.Length > 0)
        //    {
        //        if(label.Text.Length > 0)
        //        {
        //            label.Visible = false;
        //        }
        //        string query = "UPDATE Employee_Table SET " + fieldName + " = ? WHERE Employee_Name= ?";
        //        using (OleDbConnection connection = new OleDbConnection("Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BoxbagManageSystem;Data Source=."))
        //        {
        //            using (OleDbCommand cmd = new OleDbCommand(query, connection))
        //            {
        //                cmd.Parameters.AddWithValue(fieldName, value);
        //                cmd.Parameters.AddWithValue("Employee_Name", employeeName);
        //                connection.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        label.Visible = true; 
        //        label.Text = "未填";
        //    }
        //}





        private void button1_Click(object sender, EventArgs e)
        {
            DB.GetCn();

            //UpdateEmployeeField("Employee_ID", textBox1.Text, Login.StrValue, label12);
            //UpdateEmployeeField("Sex", checkedListBox1.Text, Login.StrValue, label14);
            //UpdateEmployeeField("Address", textBox6.Text, Login.StrValue, label21);
            //UpdateEmployeeField("Telephone", textBox7.Text, Login.StrValue, label20);
            //UpdateEmployeeField("Wages", textBox8.Text, Login.StrValue, label19);
            //UpdateEmployeeField("Department_id", textBox9.Text, Login.StrValue, label18);
            //UpdateEmployeeField("Resume", textBox10.Text, Login.StrValue, label17);
            //UpdateEmployeeField("[Birth Date]", dateTimePicker1.Value.ToString(), Login.StrValue, label15);
            //UpdateEmployeeField("Hire_Date", dateTimePicker2.Value.ToString(), Login.StrValue, label16);
            //MessageBox.Show("修改成功");

           

            using (SqlCommand cmd = new SqlCommand())
            {
                
                cmd.CommandText = "update Employee_Table set Employee_ID = @EmployeeID, sex = @Sex, [Birth Date] = @BirthDate, Hire_Date = @HireDate, Address = @Address, Telephone = @Telephone, Wages = @Wages, Department_id = @DepartmentID, Resume = @Resume where Employee_Name = @Employee_Name";

                cmd.Parameters.AddWithValue("@EmployeeID", textBox1.Text);
                cmd.Parameters.AddWithValue("@Sex", comboBox1.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@HireDate", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@Address", textBox6.Text);
                cmd.Parameters.AddWithValue("@Telephone", textBox7.Text);
                cmd.Parameters.AddWithValue("@Wages", textBox8.Text);
                cmd.Parameters.AddWithValue("@DepartmentID", textBox9.Text);
                cmd.Parameters.AddWithValue("@Resume", textBox10.Text);
                cmd.Parameters.AddWithValue("@Employee_Name", Login.StrValue);
                DataTable dt = DB.GetDataSet(cmd); 
                MessageBox.Show("修改成功");
            }

        }

        public void weitian(System.Windows.Forms.TextBox a, System.Windows.Forms.Label b)
        {
            if(a.Text == "")
            {
                b.Visible = true;
            }
            else
            {
                b.Visible=false;
            }
        }
        private void zhuye_Load(object sender, EventArgs e)
        {
            

            DB.GetCn();
            string str = "select * from Employee_Table where Employee_Name ='" + Login.StrValue + "'";
            DataTable dt = DB.GetDataSet(str);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Text = dt.Rows[0][1].ToString();
            comboBox1.Text = dt.Rows[0][2].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][3]);
            dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0][4]);
            textBox6.Text = dt.Rows[0][5].ToString();
            textBox7.Text = dt.Rows[0][6].ToString();
            textBox8.Text = dt.Rows[0][7].ToString();
            textBox9.Text = dt.Rows[0][8].ToString();
            textBox10.Text = dt.Rows[0][9].ToString();


            weitian(textBox1, label12);
            weitian(textBox8, label19);
            weitian(textBox7, label20);
            weitian(textBox6, label21);
            weitian(textBox10, label17);
            if(comboBox1.Text == "")
            {
                label14.Visible = true;
            }
            if (textBox9.Text == "")
            {
                label18.Visible = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

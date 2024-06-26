using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class houtaixinxi : Form
    {
        public static string lk;


        public houtaixinxi()
        {
            InitializeComponent();
        }

        private void houtaixinxi_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            DB.GetCn();
            flowLayoutPanel2.Controls.Clear();

            string sql = "SELECT Customerld,Name,touxiang FROM customer_Table"; // 替换为您的表名和列名

            DataTable dataTable = DB.GetDataSet(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                string Customerld = row["Customerld"].ToString().Trim();

                

                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 80);
                panel1.BackColor = Color.White;

                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(80, 80);
                imagePanel.BackColor = Color.LightGray;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(80, 80);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = row["touxiang"].ToString();
                pictureBox.BackColor = Color.White;
                pictureBox.Location = new Point(0, 0);
                pictureBox.Tag = row["Customerld"].ToString();

                imagePanel.Controls.Add(pictureBox);

                Label label1 = new Label();
                string labelText1 = row["Name"].ToString().Trim();
                label1.Text = labelText1;
                label1.AutoSize = true;
                label1.Location = new Point(110, 10);
                label1.Font = new Font(label1.Font.FontFamily, 12, FontStyle.Bold);
                label1.Enabled = true;
                label1.FlatStyle = FlatStyle.Flat;
                label1.BringToFront();
                label1.Click += new EventHandler(Control_Click);
                pictureBox.Click += new EventHandler(Control_Click);
                panel1.Click += new EventHandler(Control_Click);
                panel1.Controls.Add(label1);

                flowLayoutPanel2.Controls.Add(panel1);
            }

            flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel2.WrapContents = true;
        }


        public static string name;
        private async void Control_Click(object sender, EventArgs e)
        {
            
            if (sender is Control clickedControl)
            {
               
                try
                {
                   
                    string Name = null;

                    // 点击了 Label 控件
                    if (clickedControl is Label label)
                    {
                        Name = label.Text.Trim();
                       
                    }
                    // 点击了 PictureBox 控件
                    else if (clickedControl is PictureBox pictureBox)
                    {
                        
                        // 找到 PictureBox 控件的父控件 Panel
                        Panel panel = pictureBox.Parent as Panel;
                        if (panel != null && panel.Controls.Count > 1) // 检查子控件数量
                        {
                            Name = ((Label)panel.Controls[1]).Text.Trim(); // 假设 label1 是面板的第一个控件
                           
                        }
                    }
                    // 点击了 Panel 控件
                    else if (clickedControl is Panel panel)
                    {
                        if (panel.Controls.Count > 1) // 检查子控件数量
                        {
                            Name = ((Label)panel.Controls[1]).Text.Trim(); // 假设 label1 是面板的第一个控件
                            
                        }
                    }
             
                    if (Name != null)
                    {
                       
                        // 执行数据库查询
                        string query = "SELECT Customerld FROM customer_Table WHERE Name = '" + Name + "'";
                        DataTable result = await Task.Run(() => DB.GetDataSet(query));

                        if (result.Rows.Count > 0)
                        {
                            string customerId = result.Rows[0]["Customerld"].ToString();
                            lk = customerId;
                        }
                        name = Name;
                       
                        // 创建并显示 xinxi 窗体
                        houtaixngxi2 form2 = new houtaixngxi2();
                        form2.TopLevel = false;
                        form2.Dock = DockStyle.Fill;
                        form2.FormBorderStyle = FormBorderStyle.None;
                       
                        form2.Label1Text = Name;

                        flowLayoutPanel3.Controls.Add(form2); // 将 form2 添加到 flowLayoutPanel3
                        form2.Show();

                        // 关闭除当前窗体外的其他子窗体
                        foreach (Control control in flowLayoutPanel3.Controls)
                        {
                            if (control is Form form && form != form2)
                            {
                                form.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发生错误: " + ex.Message);
                }
            }
        }

    }
}

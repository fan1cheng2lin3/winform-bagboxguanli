using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class pingxingchuangkou : Form
    {
        public pingxingchuangkou()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pingxingchuangkou_Load(object sender, EventArgs e)
        {
            DB.GetCn();
            // 使用DB类中的GetDataSet方法来获取数据
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.selectedGoodsID + "'";
            DataTable dt = DB.GetDataSet(sql);
            flowLayoutPanel8.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row2 = dt.Rows[0];

                // 更新控件文本，并处理<br>标签
                label6.Text = "￥" + row2["Price"].ToString().Remove(row2["Price"].ToString().Length - 2, 2);
                label6.AutoSize = true;
                label6.Font = new Font(label6.Font.FontFamily, 18, FontStyle.Bold);
                label6.ForeColor = Color.Red;
                label6.BackColor = Color.Transparent;

                label18.Text = row2["Goods_Name"].ToString().Replace("<br>", Environment.NewLine);
                label18.Font = new Font(label18.Font.FontFamily, 14, FontStyle.Bold);


                foreach (DataRow row in dt.Rows)
                {


                    //创建Label控件并处理<br>标签以实现换行
                    System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
                    label1.Text = "详细";
                    label1.AutoSize = true;
                    label1.Font = new Font(label1.Font.FontFamily, 30, FontStyle.Bold);
                    label1.ForeColor = Color.Black;
                    label1.BackColor = Color.Transparent;


                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(this.ClientSize.Width / (26/10), (this.ClientSize.Width / (26/10)) * 9 / 8);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.ImageLocation = row["im1"].ToString();
                    pictureBox.BackColor = Color.White;
                    pictureBox.Location = new Point(1, 1);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox2 = new PictureBox();
                    pictureBox2.Size = new Size(this.ClientSize.Width / (26/10), (this.ClientSize.Width / (26/10)) * 9 / 8);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.ImageLocation = row["im2"].ToString();
                    pictureBox2.BackColor = Color.White;
                    pictureBox2.Location = new Point(1, 1);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox3 = new PictureBox();
                    pictureBox3.Size = new Size(this.ClientSize.Width / (26/10), (this.ClientSize.Width / (26/10)) * 9 / 8);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.ImageLocation = row["im3"].ToString();
                    pictureBox3.BackColor = Color.White;
                    pictureBox3.Location = new Point(1, 1);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox4 = new PictureBox();
                    pictureBox4.Size = new Size(this.ClientSize.Width / (26/10), (this.ClientSize.Width / (26/10)) * 9 / 8);
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.ImageLocation = row["im4"].ToString();
                    pictureBox4.BackColor = Color.White;
                    pictureBox4.Location = new Point(1, 1);
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox5 = new PictureBox();
                    pictureBox5.Size = new Size(this.ClientSize.Width / (26/10), (this.ClientSize.Width / (26/10)) * 9 / 8);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox5.ImageLocation = row["im5"].ToString();
                    pictureBox5.BackColor = Color.White;
                    pictureBox5.Location = new Point(1, 1);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox6 = new PictureBox();
                    pictureBox6.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox6.ImageLocation = Application.StartupPath + row["ye1"].ToString();
                    pictureBox6.BackColor = Color.White;
                    pictureBox6.Location = new Point(1, 1);
                    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox7 = new PictureBox();
                    pictureBox7.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox7.ImageLocation = Application.StartupPath + row["ye2"].ToString();
                    pictureBox7.BackColor = Color.White;
                    pictureBox7.Location = new Point(1, 1);
                    pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox8 = new PictureBox();
                    pictureBox8.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox8.ImageLocation = Application.StartupPath + row["ye3"].ToString();
                    pictureBox8.BackColor = Color.White;
                    pictureBox8.Location = new Point(1, 1);
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;


                    PictureBox pictureBox9 = new PictureBox();
                    pictureBox9.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox9.ImageLocation = Application.StartupPath + row["ye4"].ToString();
                    pictureBox9.BackColor = Color.White;
                    pictureBox9.Location = new Point(1, 1);
                    pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox10 = new PictureBox();
                    pictureBox10.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox10.ImageLocation = Application.StartupPath + row["ye5"].ToString();
                    pictureBox10.BackColor = Color.White;
                    pictureBox10.Location = new Point(1, 1);
                    pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox11 = new PictureBox();
                    pictureBox11.Size = new Size((this.ClientSize.Width / 2) * 9 / 5, this.ClientSize.Width  * 3/2);
                    pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox11.ImageLocation = Application.StartupPath + row["ye6"].ToString();
                    pictureBox11.BackColor = Color.White;
                    pictureBox11.Location = new Point(1, 1);
                    pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;



                    flowLayoutPanel8.Controls.Add(label1);
                    flowLayoutPanel4.Controls.Add(pictureBox);
                    flowLayoutPanel4.Controls.Add(pictureBox2);
                    flowLayoutPanel4.Controls.Add(pictureBox3);
                    flowLayoutPanel4.Controls.Add(pictureBox4);
                    flowLayoutPanel4.Controls.Add(pictureBox5);
                    flowLayoutPanel8.Controls.Add(pictureBox6);
                    flowLayoutPanel8.Controls.Add(pictureBox7);
                    flowLayoutPanel8.Controls.Add(pictureBox8);
                    flowLayoutPanel8.Controls.Add(pictureBox9);
                    flowLayoutPanel8.Controls.Add(pictureBox10);
                    flowLayoutPanel8.Controls.Add(pictureBox11);
                    




                }

                // 设置FlowLayoutPanel的属性
                flowLayoutPanel4.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel8.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel4.WrapContents = true;
                flowLayoutPanel8.WrapContents = true;



                //flowLayoutPanel4.Size = new Size(this.ClientSize.Width * (100000 / 49999), flowLayoutPanel4.Height);
                daxiao(flowLayoutPanel8, this.ClientSize.Width *2);
                flowLayoutPanel8.Size = new Size(flowLayoutPanel8.Width, flowLayoutPanel8.Height );
                flowLayoutPanel7.Size = new Size(flowLayoutPanel7.Width, flowLayoutPanel8.Height);
                flowLayoutPanel2.Size = new Size(flowLayoutPanel7.Width, flowLayoutPanel8.Height-880);
                //MessageBox.Show(flowLayoutPanel8.Size.ToString());
                //MessageBox.Show(flowLayoutPanel7.Size.ToString());


            }
            else
            {
                MessageBox.Show("未找到数据。");
            }


        }

        public void daxiao(FlowLayoutPanel flowLayoutPanel4, int a)
        {
            // 重新计算行数并调整大小
            flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, a * CalculateRowCount(flowLayoutPanel4));
        }

        private int CalculateRowCount(FlowLayoutPanel panel)
        {
            int rowCount = 0;
            int previousTop = -1;

            foreach (Control control in panel.Controls)
            {
                if (control.Top != previousTop)
                {
                    // 新的一行开始
                    rowCount++;
                    previousTop = control.Top;
                }
            }

            return rowCount;
        }

    }
}

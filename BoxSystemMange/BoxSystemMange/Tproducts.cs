using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class Tproducts : Form
    {
        public Tproducts()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Tproducts_Resize);
        }

       
        public string AutoWrapText(string text, int maxCharsPerLine)
        {
            StringBuilder wrappedText = new StringBuilder();
            int currentLength = 0;
            string breakTag = "<br>";

            for (int i = 0; i < text.Length; i++)
            {

                if (currentLength >= maxCharsPerLine || text.Substring(i).StartsWith(breakTag))
                {
                    if (text.Substring(i).StartsWith(breakTag) && (i + breakTag.Length < text.Length) && char.IsPunctuation(text[i + breakTag.Length]))
                    {
                        // 添加 "<br>" 和紧接着的标点符号
                        wrappedText.Append(text.Substring(i, breakTag.Length + 1));
                        i += breakTag.Length; // 跳过 "<br>" 和标点符号
                    }
                    else
                    {
                        // 达到最大字符数，添加换行符
                        wrappedText.Append(Environment.NewLine);
                    }
                    currentLength = 0; // 重置计数器
                }
                else
                {
                    wrappedText.Append(text[i]);
                    currentLength++;
                }
            }

            return wrappedText.ToString();
        }


        private void Tproducts_Load(object sender, EventArgs e)
        {


            DB.GetCn();
            // 使用DB类中的GetDataSet方法来获取数据
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.selectedGoodsID + "'";
            DataTable dt = DB.GetDataSet(sql);
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row2 = dt.Rows[0];

                // 更新控件文本，并处理<br>标签
                label9.Text = "￥" + row2["Price"].ToString().Remove(row2["Price"].ToString().Length - 2, 2);
                label9.AutoSize = true;
                label9.Font = new Font(label9.Font.FontFamily, 36, FontStyle.Bold);
                label9.ForeColor = Color.Red;
                label9.BackColor = Color.Transparent;

                label10.Text = row2["Goods_Name"].ToString().Replace("<br>", Environment.NewLine);
                label10.Font = new Font(label10.Font.FontFamily, 12, FontStyle.Bold);


                foreach (DataRow row in dt.Rows)
                {


                    //创建Label控件并处理<br>标签以实现换行
                    Label label1 = new Label();
                    label1.Text = "详细";
                    label1.AutoSize = true;
                    label1.Font = new Font(label1.Font.FontFamily, 30, FontStyle.Bold);
                    label1.ForeColor = Color.Black;
                    label1.BackColor = Color.Transparent;


                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(this.ClientSize.Width / 5, (this.ClientSize.Width / 5) * 9 / 8);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.ImageLocation = row["im1"].ToString();
                    pictureBox.BackColor = Color.White;
                    pictureBox.Location = new Point(1, 1);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox2 = new PictureBox();
                    pictureBox2.Size = new Size(this.ClientSize.Width /5, (this.ClientSize.Width / 5) * 9 / 8);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.ImageLocation = row["im2"].ToString();
                    pictureBox2.BackColor = Color.White;
                    pictureBox2.Location = new Point(1, 1);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox3 = new PictureBox();
                    pictureBox3.Size = new Size(this.ClientSize.Width / 5, (this.ClientSize.Width / 5) * 9 / 8);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.ImageLocation = row["im3"].ToString();
                    pictureBox3.BackColor = Color.White;
                    pictureBox3.Location = new Point(1, 1);
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox4 = new PictureBox();
                    pictureBox4.Size = new Size(this.ClientSize.Width / 5, (this.ClientSize.Width / 5) * 9 / 8);
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox4.ImageLocation = row["im4"].ToString();
                    pictureBox4.BackColor = Color.White;
                    pictureBox4.Location = new Point(1, 1);
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox5 = new PictureBox();
                    pictureBox5.Size = new Size(this.ClientSize.Width /5, (this.ClientSize.Width / 5) * 9 / 8);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox5.ImageLocation = row["im5"].ToString();
                    pictureBox5.BackColor = Color.White;
                    pictureBox5.Location = new Point(1, 1);
                    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox6 = new PictureBox();
                    pictureBox6.Size = new Size((this.ClientSize.Width / 2)* 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox6.ImageLocation = Application.StartupPath + row["ye1"].ToString();
                    pictureBox6.BackColor = Color.White;
                    pictureBox6.Location = new Point(1, 1);
                    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox7 = new PictureBox();
                    pictureBox7.Size = new Size((this.ClientSize.Width / 2) * 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox7.ImageLocation = Application.StartupPath + row["ye2"].ToString();
                    pictureBox7.BackColor = Color.White;
                    pictureBox7.Location = new Point(1, 1);
                    pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox8 = new PictureBox();
                    pictureBox8.Size = new Size((this.ClientSize.Width / 2) * 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox8.ImageLocation = Application.StartupPath + row["ye3"].ToString();
                    pictureBox8.BackColor = Color.White;
                    pictureBox8.Location = new Point(1, 1);
                    pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;


                    PictureBox pictureBox9 = new PictureBox();
                    pictureBox9.Size = new Size((this.ClientSize.Width / 2) * 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox9.ImageLocation = Application.StartupPath + row["ye4"].ToString();
                    pictureBox9.BackColor = Color.White;
                    pictureBox9.Location = new Point(1, 1);
                    pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox10 = new PictureBox();
                    pictureBox10.Size = new Size((this.ClientSize.Width / 2) * 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox10.ImageLocation = Application.StartupPath + row["ye5"].ToString();
                    pictureBox10.BackColor = Color.White;
                    pictureBox10.Location = new Point(1, 1);
                    pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;

                    PictureBox pictureBox11 = new PictureBox();
                    pictureBox11.Size = new Size((this.ClientSize.Width / 2) * 9 / 7, (this.ClientSize.Width / 2) * 6 / 4);
                    pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox11.ImageLocation = Application.StartupPath + row["ye6"].ToString();
                    pictureBox11.BackColor = Color.White;
                    pictureBox11.Location = new Point(1, 1);
                    pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;



                    flowLayoutPanel4.Controls.Add(label1);
                    flowLayoutPanel1.Controls.Add(pictureBox);
                    flowLayoutPanel1.Controls.Add(pictureBox2);
                    flowLayoutPanel1.Controls.Add(pictureBox3);
                    flowLayoutPanel1.Controls.Add(pictureBox4);
                    flowLayoutPanel1.Controls.Add(pictureBox5);
                    flowLayoutPanel4.Controls.Add(pictureBox6);
                    flowLayoutPanel4.Controls.Add(pictureBox7);
                    flowLayoutPanel4.Controls.Add(pictureBox8);
                    flowLayoutPanel4.Controls.Add(pictureBox9);
                    flowLayoutPanel4.Controls.Add(pictureBox10);
                    flowLayoutPanel4.Controls.Add(pictureBox11);
                    




                }

                // 设置FlowLayoutPanel的属性
                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel4.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel1.WrapContents = true;
                flowLayoutPanel4.WrapContents = true;



                //flowLayoutPanel1.Size = new Size(this.ClientSize.Width * (100000 / 49999), flowLayoutPanel1.Height);
                daxiao(flowLayoutPanel4, (this.ClientSize.Width / 2) * 6 / 4);
                flowLayoutPanel4.Size = new Size(flowLayoutPanel4.Width, flowLayoutPanel4.Height-250);
                flowLayoutPanel5.Size = new Size(flowLayoutPanel5.Width, flowLayoutPanel4.Height-150);
               
                
                
            }
            else
            {
                MessageBox.Show("未找到数据。");
            }


            
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

        public void daxiao(FlowLayoutPanel flowLayoutPanel1,int a)
        {
            // 重新计算行数并调整大小
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width, a * CalculateRowCount(flowLayoutPanel1));
        }



        public static bool isTproductsClosed = true;
        private void Tproducts_FormClosed(object sender, FormClosedEventArgs e)
        {
            isTproductsClosed = false;
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void panel16_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {

        }

        private void Tproducts_Resize(object sender, EventArgs e)
        {
           
        }

     

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

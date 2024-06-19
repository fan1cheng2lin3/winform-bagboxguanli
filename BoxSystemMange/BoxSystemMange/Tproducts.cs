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
    public partial class Tproducts : Form
    {
        public Tproducts()
        {
            InitializeComponent();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

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
            string sql = "SELECT * FROM Product_Table WHERE Goods_ID = '" + zhuye.StaticNewIdentifier + "'";
            DataTable dt = DB.GetDataSet(sql);

            // 检查DataTable是否有数据
            if (dt.Rows.Count > 0)
            {
                // 假设每个new标识符只有一条记录，获取第一行数据
                DataRow row = dt.Rows[0];

                // 更新控件文本，并处理<br>标签
                label9.Text = "￥" + AutoWrapText(row["Price"].ToString(), 13);
                label9.AutoSize = true;
                label9.Font = new Font(label9.Font.FontFamily, 36, FontStyle.Bold);
                label9.ForeColor = Color.Red;
                label9.BackColor = Color.Transparent;


                label10.Text = AutoWrapText(row["Goods_Name"].ToString(), 13);



                //label4.Text = AutoWrapText(row["label3"].ToString(), 13);
                //string a = row["im1"].ToString();
                //pictureBox1.Image = System.Drawing.Image.FromFile(a);

                //string b = row["im2"].ToString();
                //pictureBox2.Image = System.Drawing.Image.FromFile(b);

                //string c = row["im3"].ToString();
                //pictureBox3.Image = System.Drawing.Image.FromFile(c);

            }
            else
            {
                MessageBox.Show("未找到数据。");
            }
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
    }
}

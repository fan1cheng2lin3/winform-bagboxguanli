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
    public partial class community : Form
    {
        public community()
        {
            InitializeComponent();
        }
        private void LoadDataFromDatabase()
        {
            DB.GetCn();
            // 清空现有的控件
            //flowLayoutPanel1.Controls.Clear();

            // 查询数据库
            string sql = "SELECT * FROM community_Table";
            DataTable dataTable = DB.GetDataSet(sql);

            // 为每条记录创建Panel
            foreach (DataRow row in dataTable.Rows)
            {
                Panel panel1 = new Panel();
                panel1.Size = new Size(200, 185);
                panel1.BackColor = Color.Transparent;

                // 创建一个新的Panel
                Panel imagePanel = new Panel();
                imagePanel.Size = new Size(150, 120);

                imagePanel.Location = new Point(2, 2);
                imagePanel.BackColor = Color.LightGray;
                panel1.Controls.Add(imagePanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(140, 105);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = row["imagepath"].ToString();
                pictureBox.BackColor = Color.White;
                pictureBox.Location = new Point(1, 1);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                // 为PictureBox添加Click事件
                // 将new标识符存储在Tag属性中
                pictureBox.Tag = row["number"].ToString();
                pictureBox.Click += new EventHandler(pictureBox_Click);

                imagePanel.Controls.Add(pictureBox);

                // 创建Label控件并处理<br>标签以实现换行
                Label label1 = new Label();
                string labelText1 = row["text"].ToString().Replace("<br>", Environment.NewLine);
                label1.Text = labelText1;
                label1.AutoSize = true;
                label1.Location = new Point(20, 130);
                panel1.Controls.Add(label1);

                // 对其他标签重复相同的处理
                Label label2 = new Label();
                string labelText2 = row["date"].ToString().Replace("<br>", Environment.NewLine);
                label2.Text = labelText2;
                label2.AutoSize = true;
                label2.Location = new Point(20, 150);
                panel1.Controls.Add(label2);



                // 添加到flowLayoutPanel
                flowLayoutPanel1.Controls.Add(panel1);
            }

            // 设置FlowLayoutPanel的属性
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                string videoNumber = clickedPictureBox.Tag.ToString();
                OpenVideoFile(videoNumber);
            }
        }

        /// <summary>
        /// 这个函数呢是打开与
        /// </summary>
        private void OpenVideoFile(string videoNumber)
        {


            DB.GetCn();


            flowLayoutPanel2.AutoScrollPosition = new Point(0, 0);
            // 使用视频编号查询数据库，找到对应的视频路径
            string sql = $"SELECT mp4path FROM community_Table WHERE number = '{videoNumber}'";
            DataTable dataTable = DB.GetDataSet(sql);
            if (dataTable.Rows.Count > 0)
            {
                // 假设mp4path列包含视频文件的路径
                string videoPath = dataTable.Rows[0]["mp4path"].ToString();

                // 设置视频播放器控件的URL属性
                axWindowsMediaPlayer1.URL = videoPath;
                axWindowsMediaPlayer1.Ctlcontrols.play(); // 播放视频
            }
            //AA2 t1 = new AA2(videoNumber); // 将视频编号传递给AA2窗体
            //t1.ShowDialog();
        }
        private void community_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void community_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
    }
}

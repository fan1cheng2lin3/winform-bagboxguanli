using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BoxSystemMange
{
    public partial class houtaichuangti : Form
    {
        public houtaichuangti()
        {
            InitializeComponent();
        }


        SqlDataAdapter daProduct, daLog;
        DataSet ds = new DataSet();
        public static string path_source = "";
        public static string path_source2 = "";
        private Point clickLocation;

        void init()
        {
            DB.GetCn();
            string str = "select * from Product_Table";
            string sdr = "select * from Log_Table";

            daProduct = new SqlDataAdapter(str, DB.cn);
            daLog = new SqlDataAdapter(sdr, DB.cn);
            daProduct.Fill(ds, "product_info");
            daLog.Fill(ds, "log_info");



            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button10.Visible = false;
            }
            else
            {
                button10.Visible = true;
            }
            
        }


        void showAll()
        {
            DataView dvProduct = new DataView(ds.Tables["product_info"]);
            dgvPriduct.DataSource = dvProduct;
            dataGridView1.DataSource = dvProduct;
            dataGridView2.DataSource = dvProduct;
        }

       
        private void houtaichuangti_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet3.Designshi_Table”中。您可以根据需要移动或移除它。
            this.designshi_TableTableAdapter.Fill(this.boxbagManageSystemDataSet3.Designshi_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet1.Category_Table”中。您可以根据需要移动或移除它。
            this.category_TableTableAdapter.Fill(this.boxbagManageSystemDataSet1.Category_Table);
            // TODO: 这行代码将数据加载到表“boxbagManageSystemDataSet.Product_Table”中。您可以根据需要移动或移除它。
            this.product_TableTableAdapter.Fill(this.boxbagManageSystemDataSet.Product_Table);


            if (frm == null || string.IsNullOrEmpty(frm.Text))
            {
                frm = new zhuye();
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
            }

            showXz();
            init();
            showAll();
            tabControl1.TabPages[0].Text = "增加商品信息";
            tabControl1.TabPages[1].Text = "修改商品信息";
            tabControl1.TabPages[2].Text = "删除商品信息";





            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 设置窗体边框样式
            this.MaximizeBox = false; // 禁用最大化按钮
            this.MinimizeBox = false; // 禁用最小化按钮
            this.ResizeRedraw = false; // 禁止重绘操作
            this.ClientSize = new Size(this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            this.ClientSize = new Size(this.ClientSize.Width + 1, this.ClientSize.Height + 1);
            

        }
        public static Form frm;
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_source = ofd.FileName;
                pictureBox1.Image = Image.FromFile(path_source);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            
            
        }


      

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // 存储点击位置
            clickLocation = e.Location;

            // 显示上下文菜单
            contextMenuStrip1.Show((Control)sender, new Point(e.X, e.Y));
        }


        public bool aaa =true;
        private void button10_Click(object sender, EventArgs e)
        {

            if (!aaa)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path_source = ofd.FileName;
                    // 创建一个新的PictureBox
                    PictureBox newPictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage, // 设置图片显示模式
                        Image = Image.FromFile(path_source), // 加载图片
                        Size = new Size(160, 150),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    newPictureBox.MouseClick += new MouseEventHandler(PictureBox_MouseClick);
                    // 将PictureBox添加到flowLayoutPanel2中
                    flowLayoutPanel2.Controls.Add(newPictureBox);
                    newPictureBox.Dock = DockStyle.Top;

                    foreach (Control ctrl in flowLayoutPanel2.Controls)
                    {
                        if (ctrl is Button)
                        {
                            ctrl.SendToBack();
                        }
                    }

                    string filename = Path.GetFileName(path_source);
                    string fileFolder = Directory.GetCurrentDirectory() + "\\Prod_Images\\";

                    if (!Directory.Exists(fileFolder))
                    {
                        Directory.CreateDirectory(fileFolder);
                    }

                    string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string newFilename = dateTime + Path.GetExtension(filename);

                    // 完整的新文件路径
                    string newFilePath = Path.Combine(fileFolder, newFilename);

                    // 复制文件到新路径
                    File.Copy(path_source, newFilePath);

                    string dbImagePath = "Prod_Images\\" + newFilename;

                    // 将图片路径保存到 PictureBox 的 Tag 属性
                    newPictureBox.Tag = dbImagePath;
                }
            }
            else
            {
                for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
                {
                    // 如果控件是PictureBox类型，则移除它
                    if (flowLayoutPanel2.Controls[i] is PictureBox)
                    {
                        flowLayoutPanel2.Controls.RemoveAt(i);
                    }
                }

                aaa = false;
            }

            
            

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox2.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("商品名称，成本价格，库存不能为空");
            }
            else
            {
                DialogResult dr = MessageBox.Show("您确定要修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    string filename;
                    string fileFolder;
                    string dateTime = "";
                    filename = Path.GetFileName(path_source);
                    dateTime += DateTime.Now.Year.ToString();
                    dateTime += DateTime.Now.Month.ToString("00");
                    dateTime += DateTime.Now.Day.ToString("00");
                    dateTime += DateTime.Now.Hour.ToString("00");
                    dateTime += DateTime.Now.Minute.ToString("00");
                    dateTime += DateTime.Now.Second.ToString("00");
                    filename = dateTime + filename;
                    fileFolder = Directory.GetCurrentDirectory() + "\\" + "Prod_Images\\"
                        + "\\";
                    fileFolder += filename;




                    int a = dgvPriduct.CurrentRow.Index;
                    string str = dgvPriduct.Rows[a].Cells["Goods_ID"].Value.ToString();
                    DataRow[] ProRows = ds.Tables["product_info"].Select("Goods_ID='" + str + "'");
                    ProRows[0]["Classification_ID"] = comboBox1.SelectedValue;
                    ProRows[0]["Supplier_ID"] = comboBox2.SelectedValue;
                    ProRows[0]["Price"] = decimal.Parse(textBox3.Text.Trim());
                    ProRows[0]["Unit_Price"] = decimal.Parse(textBox2.Text.Trim());
                    ProRows[0]["Stock_Quantity"] = Convert.ToInt32(textBox5.Text.Trim());
                    ProRows[0]["Goods_Name"] = textBox4.Text.Trim();
                    if (path_source != "")
                    {

                        ProRows[0]["image"] = "Prod_Images\\" + filename;
                        File.Copy(path_source, fileFolder, true);
                    }



                    // 保存图片到数据库


                    //添加
                    int imageIndex = 1;
                    foreach (Control ctrl in flowLayoutPanel2.Controls)
                    {
                        if (ctrl is PictureBox)
                        {
                            string imagePath = ctrl.Tag as string;

                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                ProRows[0]["im" + imageIndex] = imagePath;
                                imageIndex++;
                                if (imageIndex > 5)
                                {
                                    break;
                                }
                            }
                        }
                    }


                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = Login.StrValue;
                    drLog["type"] = "修改";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);

                    try
                    {
                        SqlCommandBuilder dbProduct = new SqlCommandBuilder(daProduct);
                        daProduct.Update(ds, "product_info");

                        SqlCommandBuilder dbLog = new SqlCommandBuilder(daLog);
                        daLog.Update(ds, "log_info");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    DB.cn.Close();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


        private PictureBox GetPictureBoxAtLocation(Point location)
        {
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                // 检查控件是否是PictureBox
                if (control is PictureBox pictureBox)
                {
                    // 检查点击位置是否在PictureBox内
                    if (pictureBox.ClientRectangle.Contains(location))
                    {
                        return pictureBox;
                    }
                }
            }
            return null;
        }



        private void fggToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 使用存储的点击位置来确定哪个PictureBox被点击
            PictureBox clickedPictureBox = GetPictureBoxAtLocation(clickLocation);

            if (clickedPictureBox != null)
            {
                for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
                {
                    // 如果控件是PictureBox类型，则移除它
                    if (flowLayoutPanel2.Controls[i] is PictureBox)
                    {
                        flowLayoutPanel2.Controls.RemoveAt(i);
                    }
                }
                // 删除图片
                //clickedPictureBox.Image = null;

                //// 从容器中移除PictureBox
                //flowLayoutPanel2.Controls.Remove(clickedPictureBox);

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path_source = ofd.FileName;
                // 创建一个新的PictureBox
                PictureBox newPictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage, // 设置图片显示模式
                    Image = Image.FromFile(path_source), // 加载图片
                    Size = new Size(160, 150),
                    BorderStyle = BorderStyle.FixedSingle
                };
                newPictureBox.MouseClick += new MouseEventHandler(PictureBox_MouseClick);
                // 将PictureBox添加到flowLayoutPanel2中
                flowLayoutPanel3.Controls.Add(newPictureBox);
                newPictureBox.Dock = DockStyle.Top;

                foreach (Control ctrl in flowLayoutPanel3.Controls)
                {
                    if (ctrl is Button)
                    {
                        ctrl.SendToBack();
                    }
                }

                string filename = Path.GetFileName(path_source);
                string fileFolder = Directory.GetCurrentDirectory() + "\\Prod_Images\\";

                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string newFilename = dateTime + Path.GetExtension(filename);

                // 完整的新文件路径
                string newFilePath = Path.Combine(fileFolder, newFilename);

                // 复制文件到新路径
                File.Copy(path_source, newFilePath);

                string dbImagePath = "Prod_Images\\" + newFilename;

                // 将图片路径保存到 PictureBox 的 Tag 属性
                newPictureBox.Tag = dbImagePath;
            }


            //if (!aaa)
            //{
                
            //}
            //else
            //{
            //    for (int i = flowLayoutPanel3.Controls.Count - 1; i >= 0; i--)
            //    {
            //        // 如果控件是PictureBox类型，则移除它
            //        if (flowLayoutPanel3.Controls[i] is PictureBox)
            //        {
            //            flowLayoutPanel3.Controls.RemoveAt(i);
            //        }
            //    }

            //    aaa = false;
            //}
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "" || textBox10.Text == "" || textBox8.Text == ""|| textBox13.Text == "")
            {
                MessageBox.Show("商品名称，成本价格，库存,商品编号不能为空");
            }
            else
            {
                DB.GetCn();
                string str = "select * from Product_Table where Goods_ID ='" + textBox13.Text + "'";
                DataTable dt = DB.GetDataSet(str);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("此商品已存在，请重新输入编号");
                }
                else
                {

                    string filename;
                    string fileFolder;
                    string dateTime = "";
                    filename = Path.GetFileName(path_source2);
                    dateTime += DateTime.Now.Year.ToString();
                    dateTime += DateTime.Now.Month.ToString("00");
                    dateTime += DateTime.Now.Day.ToString("00");
                    dateTime += DateTime.Now.Hour.ToString("00");
                    dateTime += DateTime.Now.Minute.ToString("00");
                    dateTime += DateTime.Now.Second.ToString("00");
                    filename = dateTime + filename;
                    fileFolder = Directory.GetCurrentDirectory() + "\\" + "Prod_Images\\"
                        + comboBox1.Text + "\\";
                    fileFolder += filename;


                    DataRow drPro = ds.Tables["product_info"].NewRow();
                    //drPro["ProductID"] = Convert.ToInt32(textBox1.Text);
                    drPro["Goods_ID"] = int.Parse(textBox13.Text);
                    drPro["Classification_ID"] = comboBox4.SelectedValue;
                    drPro["Unit_Price"] = decimal.Parse(textBox11.Text);
                    drPro["Price"] = decimal.Parse(textBox10.Text);
                    drPro["Supplier_ID"] = comboBox3.SelectedValue;
                    drPro["Goods_Name"] = textBox9.Text;
                    drPro["Stock_Quantity"] = textBox8.Text;



                    if (path_source2 != "")
                    {
                        File.Copy(path_source2, fileFolder, true);
                        drPro["image"] = "Prod_Images\\" + filename;
                    }
                    else
                    {
                        drPro["image"] = "780.jpg";
                    }


                    ds.Tables["product_info"].Rows.Add(drPro);


                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = Login.StrValue;
                    drLog["type"] = "添加";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);



                    try
                    {
                        SqlCommandBuilder dbProuct = new SqlCommandBuilder(daProduct);
                        daProduct.Update(ds, "product_info");
                        SqlCommandBuilder dbLog = new SqlCommandBuilder(daLog);
                        daLog.Update(ds, "log_info");
                        MessageBox.Show("增加成功");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    DB.cn.Close();
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_source2 = ofd.FileName;
                pictureBox2.Image = Image.FromFile(path_source2);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        void showXz()
        {
            DataGridViewCheckBoxColumn acCode = new DataGridViewCheckBoxColumn();
            acCode.Name = "acCode";
            acCode.HeaderText = "选择";
            dataGridView2.Columns.Add(acCode);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 单击单元格时，确保只选中当前行
            //if (e.RowIndex >= 0)
            //{
            //    foreach (DataGridViewRow row in dataGridView2.Rows)
            //    {
            //        DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;
            //        if (row.Index != e.RowIndex)
            //        {
            //            chk.Value = false;
            //        }
            //        else
            //        {
            //            chk.Value = true;
            //        }
            //    }
            //}
        }

        void UpdateDB()
        {
            try
            {
                // 更新数据库
                SqlCommandBuilder dbProduct = new SqlCommandBuilder(daProduct);
                daProduct.Update(ds, "product_info");

                SqlCommandBuilder dbLog = new SqlCommandBuilder(daLog);
                daLog.Update(ds, "log_info");

                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除操作失败：" + ex.Message);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
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
                    drLog["username"] = Login.StrValue;
                    drLog["type"] = "删除";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);

                    
                    
                }
            }
            MessageBox.Show("已删除");

            // 执行集合中的行删除
            foreach (DataGridViewRow row in rowsToDelete)
            {
                dataGridView2.Rows.Remove(row);
            }

            // 更新数据库
            UpdateDB();

            // 如果没有选中任何行，显示提示消息
            if (selectedCount == 0)
            {
                MessageBox.Show("请选择要删除的项");
            }
        }

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            
            // 遍历 dataGridView2 中的每一行，切换复选框的选中状态
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;

                // 设置复选框的选中状态为 true
                chk.Value = true;
            }
            radioButton1.Visible = false;
            radioButton2.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

           

            // 遍历 dataGridView2 中的每一行，反转复选框的选中状态
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["acCode"] as DataGridViewCheckBoxCell;

                // 反转复选框的选中状态
                chk.Value = !(bool)chk.Value;
            }

            radioButton1.Visible = true;
            radioButton2.Visible = false;
        }


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


        public bool dsa;
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            dsa = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            aaa = true;
            textBox1.Text = dgvPriduct.CurrentRow.Cells["Goods_ID"].Value.ToString();
            comboBox1.Text = dgvPriduct.CurrentRow.Cells["Classification_ID"].Value.ToString();
            textBox3.Text = dgvPriduct.CurrentRow.Cells["Price"].Value.ToString();
            textBox2.Text = dgvPriduct.CurrentRow.Cells["Unit_Price"].Value.ToString();
            comboBox2.Text = dgvPriduct.CurrentRow.Cells["Supplier_ID"].Value.ToString();
            textBox4.Text = dgvPriduct.CurrentRow.Cells["Goods_Name"].Value.ToString();
            textBox6.Text = dgvPriduct.CurrentRow.Cells["Order_Quantity"].Value.ToString();
            textBox5.Text = dgvPriduct.CurrentRow.Cells["Stock_Quantity"].Value.ToString();

            // 清空 flowLayoutPanel2 现有的控件
            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                if (!(flowLayoutPanel2.Controls[i] is Button))
                {
                    flowLayoutPanel2.Controls.RemoveAt(i);
                }
            }

            string[] imageColumns = { "im1", "im2", "im3", "im4" ,"im5"};

            foreach (string column in imageColumns)
            {
                string imagePath = dgvPriduct.CurrentRow.Cells[column].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    PictureBox newPictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = Image.FromFile(imagePath),
                        Size = new Size(160, 150),
                        BorderStyle = BorderStyle.FixedSingle,
                        Dock = DockStyle.Top
                    };
                    newPictureBox.MouseClick += new MouseEventHandler(PictureBox_MouseClick);
                    // 将 PictureBox 添加到 flowLayoutPanel2 中
                    flowLayoutPanel2.Controls.Add(newPictureBox);
                }
            }

            // 处理 flowLayoutPanel2 中的按钮
            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.SendToBack();
                }
            }

            // 设置主图片
            try
            {
                pictureBox1.Image = Image.FromFile(dgvPriduct.CurrentRow.Cells["image"].Value.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button10.Visible = false;
            }
            else
            {
                button10.Visible = true;
            }
        }



       

    }
}

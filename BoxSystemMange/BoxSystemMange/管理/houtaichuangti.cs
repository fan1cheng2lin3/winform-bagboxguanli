using BoxSystemMange.脚本类;
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
using System.Windows.Forms.VisualStyles;
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
       
        public static string path_sourceye1 = "";
        public static string path_sourceye2 = "";
        public static string path_sourceye3 = "";
        public static string path_sourceye4 = "";
        public static string path_sourceye5 = "";
        public static string path_sourceye6 = "";


        public static string path_source2 = "";

        public static string path_sourcegai1 = "";
        public static string path_sourcegai2 = "";
        public static string path_sourcegai3 = "";
        public static string path_sourcegai4 = "";
        public static string path_sourcegai5 = "";
        public static string path_sourcegai6 = "";
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

            dataGridView1 .AutoGenerateColumns = true;



            showXz();
            init();
            showAll();
            tabControl1.TabPages[0].Text = "增加商品信息";
            tabControl1.TabPages[1].Text = "修改商品信息";
            tabControl1.TabPages[2].Text = "删除商品信息";


            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

        }
      
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
            

            //MessageBox.Show("是否促销："+ass  +  "     是否推荐：" + b + "   是否推荐促销：" + c);
            

            if (textBox4.Text == "" || textBox2.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("商品名称，成本价格，库存不能为空");
            }
            else
            {
                DialogResult dr = MessageBox.Show("您确定要修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                   
                    string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string uniqueFolder = Directory.GetCurrentDirectory() + "\\Prod_Images\\" + dateTime ;
                    Directory.CreateDirectory(uniqueFolder);

                    int a = dgvPriduct.CurrentRow.Index;
                    string str = dgvPriduct.Rows[a].Cells["Goods_ID"].Value.ToString();
                    DataRow[] ProRows = ds.Tables["product_info"].Select("Goods_ID='" + str + "'");
                    ProRows[0]["Classification_ID"] = comboBox1.SelectedValue;
                    ProRows[0]["Supplier_ID"] = comboBox2.SelectedValue;
                    ProRows[0]["Price"] = decimal.Parse(textBox3.Text.Trim());
                    ProRows[0]["Unit_Price"] = decimal.Parse(textBox2.Text.Trim());
                    ProRows[0]["Stock_Quantity"] = Convert.ToInt32(textBox5.Text.Trim());
                    ProRows[0]["Goods_Name"] = textBox4.Text.Trim();


                    //if (path_source != "")
                    //{
                    //    ProRows[0]["image"] = "Prod_Images\\" + filename;
                    //    File.Copy(path_source, fileFolder, true);
                    //}
                    CopyImageAndSetDataRow(path_source, uniqueFolder, ProRows[0], "image", "780.jpg");
                    CopyImageAndSetDataRow(path_sourcegai1, uniqueFolder, ProRows[0], "ye1", "NULL");
                    CopyImageAndSetDataRow(path_sourcegai2, uniqueFolder, ProRows[0], "ye2", "NULL");
                    CopyImageAndSetDataRow(path_sourcegai3, uniqueFolder, ProRows[0], "ye3", "NULL");
                    CopyImageAndSetDataRow(path_sourcegai4, uniqueFolder, ProRows[0] , "ye4", "NULL");
                    CopyImageAndSetDataRow(path_sourcegai5, uniqueFolder, ProRows[0], "ye5", "NULL");
                    CopyImageAndSetDataRow(path_sourcegai6, uniqueFolder, ProRows[0], "ye6", "NULL");


                    if (c == "促销推荐")
                    {
                        ProRows[0]["leixing"] = 7;
                       
                    }
                    else if ( ass == "促销")
                    {
                        ProRows[0]["leixing"] = 5;
                       
                    }
                    else if( b == "推荐")
                    {
                        ProRows[0]["leixing"] = 6;
                        
                    }
                    else
                    {
                        ProRows[0]["leixing"] = 4;
                        //MessageBox.Show("默认");
                    }

                    c = "";
                    ass = "";
                    b = "";

                   
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
                    drLog["username"] = houtaidenglu.StrValue;
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
            if (false)
            {
                
            }
            else
            {
                DB.GetCn();
                DataTable dataTable = (DataTable)dataGridView1.DataSource;

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string goodsID = dataRow["Goods_ID"].ToString();
                    string str = "select * from Product_Table where Goods_ID ='" + goodsID + "'";
                    DataTable dt = DB.GetDataSet(str);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("商品编号 " + goodsID + " 已存在，请重新输入编号");
                        continue;
                    }

                    string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string uniqueFolder = Directory.GetCurrentDirectory() + "\\Prod_Images\\" + dateTime;
                    Directory.CreateDirectory(uniqueFolder);

                    DataRow drPro = ds.Tables["product_info"].NewRow();
                    drPro["Goods_ID"] = int.Parse(dataRow["Goods_ID"].ToString());
                    drPro["Classification_ID"] = dataRow["Classification_ID"];
                    drPro["Unit_Price"] = decimal.Parse(dataRow["Unit_Price"].ToString());
                    drPro["Price"] = decimal.Parse(dataRow["Price"].ToString());
                    drPro["Supplier_ID"] = dataRow["Supplier_ID"];
                    drPro["Goods_Name"] = dataRow["Goods_Name"].ToString();
                    drPro["Stock_Quantity"] = dataRow["Stock_Quantity"].ToString();
                    drPro["leixing"] = 4;

                    CopyImageAndSetDataRow(path_source2, uniqueFolder, drPro, "image", "780.jpg");
                    CopyImageAndSetDataRow(path_sourceye1, uniqueFolder, drPro, "ye1", "NULL");
                    CopyImageAndSetDataRow(path_sourceye2, uniqueFolder, drPro, "ye2", "NULL");
                    CopyImageAndSetDataRow(path_sourceye3, uniqueFolder, drPro, "ye3", "NULL");
                    CopyImageAndSetDataRow(path_sourceye4, uniqueFolder, drPro, "ye4", "NULL");
                    CopyImageAndSetDataRow(path_sourceye5, uniqueFolder, drPro, "ye5", "NULL");
                    CopyImageAndSetDataRow(path_sourceye6, uniqueFolder, drPro, "ye6", "NULL");

                    ds.Tables["product_info"].Rows.Add(drPro);

                    DataRow drLog = ds.Tables["log_info"].NewRow();
                    drLog["username"] = houtaidenglu.StrValue;
                    drLog["type"] = "添加";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);
                }
                showXz();
                init();
                showAll();
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

        public void CopyImageAndSetDataRow(string pathSource, string fileFolder, DataRow drPro, string imageColumnName,string defaultImagePath)
        {


            if (!string.IsNullOrEmpty(pathSource))
            {
                string filename = Path.GetFileName(pathSource);
                string destinationPath = "Prod_Images\\" + Path.GetFileName(fileFolder) + filename;
                File.Copy(pathSource, destinationPath, true);
                drPro[imageColumnName] = "\\Prod_Images\\" + Path.GetFileName(fileFolder) + filename;
            }
            else
            {
                drPro[imageColumnName] = defaultImagePath;
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
                    drLog["username"] = houtaidenglu.StrValue;
                    drLog["type"] = "删除";
                    drLog["action_date"] = DateTime.Now;
                    drLog["action_table"] = "product表";
                    ds.Tables["log_info"].Rows.Add(drLog);

                }
            }


            // 如果没有选中任何行，显示提示消息
            if (selectedCount == 0)
            {
                MessageBox.Show("请选择要删除的项");
            }
            else
            {
                // 执行集合中的行删除
                foreach (DataGridViewRow row in rowsToDelete)
                {
                    dataGridView2.Rows.Remove(row);
                }

                // 更新数据库
                UpdateDB();

                // 显示删除成功的消息
                MessageBox.Show("删除成功");
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

        public string ass, b,c;
        public bool aa = true, bb = true;

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye2 = ofd.FileName;
                pictureBox4.Image = Image.FromFile(path_sourceye2);
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye3 = ofd.FileName;
                pictureBox5.Image = Image.FromFile(path_sourceye3);
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye4 = ofd.FileName;
                pictureBox6.Image = Image.FromFile(path_sourceye4);
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye5 = ofd.FileName;
                pictureBox7.Image = Image.FromFile(path_sourceye5);
                pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye6 = ofd.FileName;
                pictureBox8.Image = Image.FromFile(path_sourceye6);
                pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai1 = ofd.FileName;
                pictureBox9.Image = Image.FromFile(path_sourcegai1);
                pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai2 = ofd.FileName;
                pictureBox10.Image = Image.FromFile(path_sourcegai2);
                pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai3 = ofd.FileName;
                pictureBox11.Image = Image.FromFile(path_sourcegai3);
                pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai4 = ofd.FileName;
                pictureBox12.Image = Image.FromFile(path_sourcegai4);
                pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai5 = ofd.FileName;
                pictureBox13.Image = Image.FromFile(path_sourcegai5);
                pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourcegai6 = ofd.FileName;
                pictureBox14.Image = Image.FromFile(path_sourcegai6);
                pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path_sourceye1 = ofd.FileName;
                pictureBox3.Image = Image.FromFile(path_sourceye1);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFile.FileName;
                DataTable excelDt = ExcelHelper.ReadFromExcel(filePath);

                if (excelDt == null)
                {
                    MessageBox.Show("读取Excel文件失败，请检查文件格式或路径是否正确。");
                }
                else
                {

                    if(dataGridView1.Rows.Count == 0)
                    {
                        // 如果 dataGridView1 为空，直接导入数据
                        dataGridView1.DataSource = excelDt;
                    }
                    else
                    {
                        // 提示用户选择
                        var result = MessageBox.Show("是否清空现有数据？选择“是”清空，选择“否”追加数据。", "导入选项", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            // 清空现有数据
                            dataGridView1.DataSource = null;
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear();
                            dataGridView1.DataSource = excelDt;
                        }
                        else if (result == DialogResult.No)
                        {
                            // 在已有数据基础上追加数据
                            DataTable currentDt = (DataTable)dataGridView1.DataSource;
                            if (currentDt == null)
                            {
                                dataGridView1.DataSource = excelDt;
                            }
                            else
                            {
                                foreach (DataRow row in excelDt.Rows)
                                {
                                    currentDt.ImportRow(row);
                                }
                            }
                        }
                    }
                   
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // 检查 dataGridView1 是否有数据
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出！");
                return;
            }

            // 将 DataGridView 数据转换为 DataTable
            DataTable dataTable = new DataTable();

            // 添加列
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataTable.Columns.Add(column.HeaderText, typeof(string));
            }

            // 添加行
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value?.ToString();
                }
                dataTable.Rows.Add(dataRow);
            }

            // 打开保存文件对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel 文件|*.xlsx;*.xls";
            saveFileDialog.Title = "保存 Excel 文件";
            saveFileDialog.ShowDialog();

            // 如果用户选择了保存路径
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                try
                {
                    // 使用 ExcelHelper 导出 DataTable 到 Excel 文件
                    int result = ExcelHelper.DataTableToExcel(dataTable, "Sheet1", saveFileDialog.FileName, true);
                    if (result > 0)
                    {
                        MessageBox.Show("导出成功！");
                    }
                    else
                    {
                        MessageBox.Show("导出失败！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出过程中发生错误：" + ex.Message);
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (aa == true)
            {
                button23.BackColor = Color.FromArgb(135, 206, 235); 
                ass = "促销";
                aa = false;

            }
            else
            {
                button23.BackColor = Color.White;
                ass = "";
                aa = true;
            }

            c = (ass == "促销" && b == "推荐") ? "促销推荐" : "";

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (bb == true)
            {
                b = "推荐";
                button24.BackColor = Color.FromArgb(135, 206, 235); ;
                bb = false;
            }
            else
            {
                button24.BackColor = Color.White;
                b = "";
                bb = true;
            }

            c = (ass == "促销" && b == "推荐") ? "促销推荐" : "";

        }



        private void LoadImage(PictureBox pictureBox, string imagePath)
        {
            try
            {
                pictureBox.Image = Image.FromFile(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                string defaultImagePath = Path.Combine(Application.StartupPath, "780.jpg");
                pictureBox.Image = Image.FromFile(defaultImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
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



            button23.BackColor = Color.White;
            button24.BackColor = Color.White;
            aa = false;
            bb = false;
            c = "";
            ass = "";
            b = "";

            if (Convert.ToInt32(dgvPriduct.CurrentRow.Cells["leixing"].Value) == 4)
            {
                button23.BackColor = Color.White;
                button24.BackColor = Color.White;
                aa = true;
                bb = true;
                c = "";
                ass = "";
                b = "";
            }
            else if (Convert.ToInt32(dgvPriduct.CurrentRow.Cells["leixing"].Value) == 5)
            {
                button23.BackColor = Color.FromArgb(135, 206, 235);
                button24.BackColor = Color.White;
                aa = false;
                bb = true;
                c = "";
                ass = "促销";
                b = "";
            }
            else if (Convert.ToInt32(dgvPriduct.CurrentRow.Cells["leixing"].Value) == 6)
            {
                button24.BackColor = Color.FromArgb(135, 206, 235);
                button23.BackColor = Color.White;
                aa = true;
                bb = false;
                c = "";
                ass = "";
                b = "推荐";
            }
            else if (Convert.ToInt32(dgvPriduct.CurrentRow.Cells["leixing"].Value) == 7)
            {
                button23.BackColor = Color.FromArgb(135, 206, 235);
                button24.BackColor = Color.FromArgb(135, 206, 235);
                aa = false;
                bb = false;
                c = "促销推荐";
                ass = "";
                b = "";
            }



            // 设置主图片
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath+ dgvPriduct.CurrentRow.Cells["image"].Value.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }


            try
            {
                pictureBox9.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye1"].Value.ToString());
                pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox9.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }


            try
            {
                pictureBox10.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye2"].Value.ToString());
                pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox10.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }


            try
            {
                pictureBox11.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye3"].Value.ToString());
                pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox11.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }


            try
            {

                pictureBox12.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye4"].Value.ToString());
                pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox12.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }


            try
            {

                pictureBox13.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye5"].Value.ToString());
                pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch
            {
                pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox13.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
            }


            try
            {

                pictureBox14.Image = Image.FromFile(Application.StartupPath + dgvPriduct.CurrentRow.Cells["ye6"].Value.ToString());
                pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + "\\" + "780.jpg");
                pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;
            }






            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button10.Visible = false;
                flowLayoutPanel4.Visible = false;
            }
            else
            {
                button10.Visible = true;
                flowLayoutPanel4.Visible = true;

            }
        }






    }
}

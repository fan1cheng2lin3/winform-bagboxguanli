namespace bagbox
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.欢迎来到箱包系统 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.dgvPriduct = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch1 = new System.Windows.Forms.Button();
            this.txtPName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearch2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriduct)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // 欢迎来到箱包系统
            // 
            this.欢迎来到箱包系统.AutoSize = true;
            this.欢迎来到箱包系统.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.欢迎来到箱包系统.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.欢迎来到箱包系统.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.欢迎来到箱包系统.Location = new System.Drawing.Point(567, 29);
            this.欢迎来到箱包系统.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.欢迎来到箱包系统.Name = "欢迎来到箱包系统";
            this.欢迎来到箱包系统.Size = new System.Drawing.Size(377, 58);
            this.欢迎来到箱包系统.TabIndex = 0;
            this.欢迎来到箱包系统.Text = "箱包后台管理系统";
            this.欢迎来到箱包系统.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(1236, 29);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 29);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(1350, 29);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 29);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgvPriduct
            // 
            this.dgvPriduct.AllowUserToAddRows = false;
            this.dgvPriduct.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dgvPriduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPriduct.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenVertical;
            this.dgvPriduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPriduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dgvPriduct.GridColor = System.Drawing.Color.IndianRed;
            this.dgvPriduct.Location = new System.Drawing.Point(16, 292);
            this.dgvPriduct.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPriduct.Name = "dgvPriduct";
            this.dgvPriduct.RowHeadersWidth = 51;
            this.dgvPriduct.RowTemplate.Height = 23;
            this.dgvPriduct.Size = new System.Drawing.Size(1434, 509);
            this.dgvPriduct.TabIndex = 3;
            this.dgvPriduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPriduct_CellContentClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "货物ID";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "货物名称";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "分类ID";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "供应商ID";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "单价";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "库存数量";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "订单数量";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "图片";
            this.Column8.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(15, 25, 20, 15);
            this.Column9.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column9.HeaderText = "详细信息";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Text = "详细信息";
            this.Column9.UseColumnTextForButtonValue = true;
            this.Column9.Width = 125;
            // 
            // Column10
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(20, 25, 30, 20);
            this.Column10.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column10.HeaderText = "加入购物车";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.Text = "加入购物车";
            this.Column10.UseColumnTextForButtonValue = true;
            this.Column10.Width = 125;
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(1189, 234);
            this.btnShowAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(100, 39);
            this.btnShowAll.TabIndex = 4;
            this.btnShowAll.Text = "显示全部";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Controls.Add(this.btnSearch1);
            this.groupBox1.Controls.Add(this.txtPName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(212, 132);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(475, 141);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "按商品名称查询";
            // 
            // btnSearch1
            // 
            this.btnSearch1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSearch1.Location = new System.Drawing.Point(231, 101);
            this.btnSearch1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(93, 32);
            this.btnSearch1.TabIndex = 2;
            this.btnSearch1.Text = "查询";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // txtPName
            // 
            this.txtPName.Location = new System.Drawing.Point(200, 55);
            this.txtPName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPName.Name = "txtPName";
            this.txtPName.Size = new System.Drawing.Size(251, 28);
            this.txtPName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(25, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入查询关键字：";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox2.Controls.Add(this.btnSearch2);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(705, 132);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(455, 141);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "按产品价格范围查询";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnSearch2
            // 
            this.btnSearch2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSearch2.Location = new System.Drawing.Point(241, 101);
            this.btnSearch2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch2.Name = "btnSearch2";
            this.btnSearch2.Size = new System.Drawing.Size(93, 35);
            this.btnSearch2.TabIndex = 2;
            this.btnSearch2.Text = "查询";
            this.btnSearch2.UseVisualStyleBackColor = true;
            this.btnSearch2.Click += new System.EventHandler(this.btnSearch2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "50元以下",
            "50元-100元",
            "100元-200元",
            "200元-500元",
            "500元-1000元",
            "1000元以上"});
            this.comboBox1.Location = new System.Drawing.Point(213, 55);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(213, 26);
            this.comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(5, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "请选择商品价格区间：";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aquamarine;
            this.button1.Location = new System.Drawing.Point(12, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 31);
            this.button1.TabIndex = 7;
            this.button1.Text = "测试按钮";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(1277, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "用户：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(1357, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1444, 836);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.dgvPriduct);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.欢迎来到箱包系统);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriduct)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 欢迎来到箱包系统;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.DataGridView dgvPriduct;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSearch2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Column8;
        private System.Windows.Forms.DataGridViewButtonColumn Column9;
        private System.Windows.Forms.DataGridViewButtonColumn Column10;
    }
}


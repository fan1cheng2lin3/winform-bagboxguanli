namespace bagbox
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCance = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(749, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎登录";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(667, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(684, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(760, 358);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(284, 25);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Location = new System.Drawing.Point(760, 394);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(284, 25);
            this.txtPwd.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(671, 460);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(387, 128);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择登录方式";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton3.Location = new System.Drawing.Point(280, 51);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(93, 24);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "管理员";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton2.Location = new System.Drawing.Point(144, 51);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "员工";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton1.Location = new System.Drawing.Point(9, 51);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 24);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "客户";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(728, 614);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(97, 51);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "登录";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCance
            // 
            this.btnCance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCance.Location = new System.Drawing.Point(909, 615);
            this.btnCance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCance.Name = "btnCance";
            this.btnCance.Size = new System.Drawing.Size(120, 49);
            this.btnCance.TabIndex = 7;
            this.btnCance.Text = "取消";
            this.btnCance.UseVisualStyleBackColor = true;
            this.btnCance.Click += new System.EventHandler(this.btnCance_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Firebrick;
            this.button1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(936, 438);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "忘记密码";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(1027, 396);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1444, 735);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCance);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnCance;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
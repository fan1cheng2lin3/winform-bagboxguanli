namespace bagbox.前端窗口
{
    partial class Front
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Front));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注销账户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.联系我们ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.订ToolStripMenuItem,
            this.联系我们ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem,
            this.注销账户ToolStripMenuItem,
            this.主页ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.开始ToolStripMenuItem.Text = "开始";
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 注销账户ToolStripMenuItem
            // 
            this.注销账户ToolStripMenuItem.Name = "注销账户ToolStripMenuItem";
            this.注销账户ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.注销账户ToolStripMenuItem.Text = "注销账户";
            this.注销账户ToolStripMenuItem.Click += new System.EventHandler(this.注销账户ToolStripMenuItem_Click);
            // 
            // 主页ToolStripMenuItem
            // 
            this.主页ToolStripMenuItem.Name = "主页ToolStripMenuItem";
            this.主页ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.主页ToolStripMenuItem.Text = "主页";
            this.主页ToolStripMenuItem.Click += new System.EventHandler(this.主页ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 订ToolStripMenuItem
            // 
            this.订ToolStripMenuItem.Name = "订ToolStripMenuItem";
            this.订ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.订ToolStripMenuItem.Text = "订单管理";
            // 
            // 联系我们ToolStripMenuItem
            // 
            this.联系我们ToolStripMenuItem.Name = "联系我们ToolStripMenuItem";
            this.联系我们ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.联系我们ToolStripMenuItem.Text = "联系我们";
            // 
            // Front
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Front";
            this.Text = "Front";
            this.Load += new System.EventHandler(this.Front_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 联系我们ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 注销账户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}
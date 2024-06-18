namespace BoxSystemMange
{
    partial class dindan
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
            this.label999 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label999
            // 
            this.label999.AutoSize = true;
            this.label999.Font = new System.Drawing.Font("宋体", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label999.Location = new System.Drawing.Point(22, 19);
            this.label999.Name = "label999";
            this.label999.Size = new System.Drawing.Size(199, 44);
            this.label999.TabIndex = 1;
            this.label999.Text = "我的订单";
            // 
            // dindan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 810);
            this.Controls.Add(this.label999);
            this.Name = "dindan";
            this.Text = "dindan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label999;
    }
}
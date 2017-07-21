namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfLinkLabelNForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ribbonLinkLabel1 = new GISShare.Controls.WinForm.WFNew.LinkLabelN();
            this.SuspendLayout();
            // 
            // ribbonLinkLabel1
            // 
            this.ribbonLinkLabel1.Location = new System.Drawing.Point(12, 12);
            this.ribbonLinkLabel1.Name = "ribbonLinkLabel1";
            this.ribbonLinkLabel1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonLinkLabel1.Size = new System.Drawing.Size(226, 22);
            this.ribbonLinkLabel1.TabIndex = 0;
            this.ribbonLinkLabel1.Text = "链接标签（LinkLabel）";
            // 
            // DemoOfLinkLabelNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 43);
            this.Controls.Add(this.ribbonLinkLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfLinkLabelNForm";
            this.Text = "LinkLabelN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.LinkLabelN ribbonLinkLabel1;
    }
}
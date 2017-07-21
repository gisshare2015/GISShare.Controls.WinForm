namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfImageAreaBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfImageAreaBoxForm));
            this.imageAreaBox1 = new GISShare.Controls.WinForm.WFNew.ImageAreaBox();
            this.SuspendLayout();
            // 
            // imageAreaBox1
            // 
            this.imageAreaBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageAreaBox1.Image = ((System.Drawing.Image)(resources.GetObject("imageAreaBox1.Image")));
            this.imageAreaBox1.ImageSize = new System.Drawing.Size(200, 160);
            this.imageAreaBox1.Location = new System.Drawing.Point(0, 0);
            this.imageAreaBox1.Name = "imageAreaBox1";
            this.imageAreaBox1.Padding = new System.Windows.Forms.Padding(0);
            this.imageAreaBox1.ShowBackgroud = false;
            this.imageAreaBox1.ShowOutLine = true;
            this.imageAreaBox1.Size = new System.Drawing.Size(613, 334);
            this.imageAreaBox1.TabIndex = 0;
            this.imageAreaBox1.Text = "imageAreaBox1";
            // 
            // DemoOfImageAreaBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 334);
            this.Controls.Add(this.imageAreaBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfImageAreaBoxForm";
            this.Text = "ImageAreaBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ImageAreaBox imageAreaBox1;
    }
}
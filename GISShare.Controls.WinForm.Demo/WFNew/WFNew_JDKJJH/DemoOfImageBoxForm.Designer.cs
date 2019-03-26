namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfImageBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfImageBoxForm));
            this.imageBox1 = new GISShare.Controls.WinForm.WFNew.ImageBoxItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.imageBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.imageBox1.Image = ((System.Drawing.Image)(resources.GetObject("imageBox1.Image")));
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(824, 522);
            this.imageBox1.Tag = null;
            this.imageBox1.Text = "imageBox1";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.imageBox1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(824, 522);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfImageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelItemsEventNC = false;
            this.ClientSize = new System.Drawing.Size(824, 522);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfImageBoxForm";
            this.Text = "ImageBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ImageBoxItem imageBox1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
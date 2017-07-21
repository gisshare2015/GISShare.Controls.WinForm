namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfScrollBarForm
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
            this.ribbonScrollBar1 = new GISShare.Controls.WinForm.WFNew.ScrollBar();
            this.ribbonScrollBar2 = new GISShare.Controls.WinForm.WFNew.ScrollBar();
            this.SuspendLayout();
            // 
            // ribbonScrollBar1
            // 
            this.ribbonScrollBar1.Location = new System.Drawing.Point(12, 159);
            this.ribbonScrollBar1.LockHeight = true;
            this.ribbonScrollBar1.Name = "ribbonScrollBar1";
            this.ribbonScrollBar1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonScrollBar1.Size = new System.Drawing.Size(176, 16);
            this.ribbonScrollBar1.TabIndex = 0;
            this.ribbonScrollBar1.Text = "ribbonScrollBar1";
            // 
            // ribbonScrollBar2
            // 
            this.ribbonScrollBar2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonScrollBar2.Location = new System.Drawing.Point(194, 12);
            this.ribbonScrollBar2.LockWith = true;
            this.ribbonScrollBar2.Name = "ribbonScrollBar2";
            this.ribbonScrollBar2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonScrollBar2.Size = new System.Drawing.Size(16, 141);
            this.ribbonScrollBar2.TabIndex = 1;
            this.ribbonScrollBar2.Text = "ribbonScrollBar2";
            // 
            // DemoOfScrollBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 187);
            this.Controls.Add(this.ribbonScrollBar2);
            this.Controls.Add(this.ribbonScrollBar1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfScrollBarForm";
            this.Text = "ScrollBar控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ScrollBar ribbonScrollBar1;
        private GISShare.Controls.WinForm.WFNew.ScrollBar ribbonScrollBar2;
    }
}
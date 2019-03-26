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
            this.ribbonScrollBar1 = new GISShare.Controls.WinForm.WFNew.ScrollBarItem();
            this.ribbonScrollBar2 = new GISShare.Controls.WinForm.WFNew.ScrollBarItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonScrollBar1
            // 
            this.ribbonScrollBar1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonScrollBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.ribbonScrollBar1.LockHeight = true;
            this.ribbonScrollBar1.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonScrollBar1.Name = "ribbonScrollBar1";
            this.ribbonScrollBar1.Size = new System.Drawing.Size(264, 24);
            this.ribbonScrollBar1.Tag = null;
            this.ribbonScrollBar1.Text = "ribbonScrollBar1";
            // 
            // ribbonScrollBar2
            // 
            this.ribbonScrollBar2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonScrollBar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonScrollBar2.Location = new System.Drawing.Point(0, 0);
            this.ribbonScrollBar2.LockHeight = true;
            this.ribbonScrollBar2.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonScrollBar2.Name = "ribbonScrollBar2";
            this.ribbonScrollBar2.Size = new System.Drawing.Size(24, 218);
            this.ribbonScrollBar2.Tag = null;
            this.ribbonScrollBar2.Text = "ribbonScrollBar2";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.ribbonScrollBar1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 238);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(264, 24);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.ribbonScrollBar2;
            this.baseItemHost2.Location = new System.Drawing.Point(291, 18);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(24, 218);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfScrollBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 276);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DemoOfScrollBarForm";
            this.Text = "ScrollBar控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ScrollBarItem ribbonScrollBar1;
        private GISShare.Controls.WinForm.WFNew.ScrollBarItem ribbonScrollBar2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
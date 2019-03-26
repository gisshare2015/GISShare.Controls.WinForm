namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfLabelSeparatorForm
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
            this.ribbonLabelSeparator1 = new GISShare.Controls.WinForm.WFNew.LabelSeparatorItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonLabelSeparator1
            // 
            this.ribbonLabelSeparator1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.ribbonLabelSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonLabelSeparator1.Location = new System.Drawing.Point(0, 0);
            this.ribbonLabelSeparator1.Name = "ribbonLabelSeparator1";
            this.ribbonLabelSeparator1.Size = new System.Drawing.Size(390, 36);
            this.ribbonLabelSeparator1.Tag = null;
            this.ribbonLabelSeparator1.Text = "分栏标签（LabelSeparator)";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.ribbonLabelSeparator1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(390, 36);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfLabelSeparatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelItemsEventNC = false;
            this.ClientSize = new System.Drawing.Size(458, 98);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfLabelSeparatorForm";
            this.Text = "LabelSeparator控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.LabelSeparatorItem ribbonLabelSeparator1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
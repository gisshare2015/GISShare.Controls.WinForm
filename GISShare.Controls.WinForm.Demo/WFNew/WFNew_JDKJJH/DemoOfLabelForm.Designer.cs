namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfLabelForm
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
            this.ribbonLabel1 = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonLabel1
            // 
            this.ribbonLabel1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonLabel1.Location = new System.Drawing.Point(0, 0);
            this.ribbonLabel1.Name = "ribbonLabel1";
            this.ribbonLabel1.Size = new System.Drawing.Size(222, 36);
            this.ribbonLabel1.Tag = null;
            this.ribbonLabel1.Text = "标签控件（Label）";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.ribbonLabel1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(222, 36);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfLabelNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 103);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfLabelNForm";
            this.Text = "LabelN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.LabelItem ribbonLabel1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
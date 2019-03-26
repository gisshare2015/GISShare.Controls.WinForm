namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfDescriptionButtonForm
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
            this.ribbonDescriptionButton1 = new GISShare.Controls.WinForm.WFNew.DescriptionButtonItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonDescriptionButton1
            // 
            this.ribbonDescriptionButton1.Description = "该按钮可以在下方显示其它附加的描述信息";
            this.ribbonDescriptionButton1.DescriptionFont = new System.Drawing.Font("宋体", 9F);
            this.ribbonDescriptionButton1.DescriptionForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonDescriptionButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.ribbonDescriptionButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonDescriptionButton1.Image = null;
            this.ribbonDescriptionButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonDescriptionButton1.Location = new System.Drawing.Point(0, 0);
            this.ribbonDescriptionButton1.Name = "ribbonDescriptionButton1";
            this.ribbonDescriptionButton1.Padding = new System.Windows.Forms.Padding(3);
            this.ribbonDescriptionButton1.ShowNomalState = true;
            this.ribbonDescriptionButton1.Size = new System.Drawing.Size(320, 86);
            this.ribbonDescriptionButton1.Tag = null;
            this.ribbonDescriptionButton1.Text = "描述按钮";
            this.ribbonDescriptionButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonDescriptionButton1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(320, 86);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfDescriptionButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 118);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfDescriptionButtonForm";
            this.Text = "DescriptionButton控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DescriptionButtonItem ribbonDescriptionButton1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
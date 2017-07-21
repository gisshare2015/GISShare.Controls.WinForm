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
            this.ribbonDescriptionButton1 = new GISShare.Controls.WinForm.WFNew.DescriptionButton();
            this.SuspendLayout();
            // 
            // ribbonDescriptionButton1
            // 
            this.ribbonDescriptionButton1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonDescriptionButton1.Description = "该按钮可以在下方显示其它附加的描述信息";
            this.ribbonDescriptionButton1.DescriptionFont = new System.Drawing.Font("宋体", 9F);
            this.ribbonDescriptionButton1.DescriptionForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonDescriptionButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.ribbonDescriptionButton1.Image = null;
            this.ribbonDescriptionButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonDescriptionButton1.Location = new System.Drawing.Point(12, 12);
            this.ribbonDescriptionButton1.Name = "ribbonDescriptionButton1";
            this.ribbonDescriptionButton1.Padding = new System.Windows.Forms.Padding(3);
            this.ribbonDescriptionButton1.Size = new System.Drawing.Size(213, 57);
            this.ribbonDescriptionButton1.TabIndex = 0;
            this.ribbonDescriptionButton1.Text = "描述按钮";
            this.ribbonDescriptionButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DemoOfDescriptionButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 81);
            this.Controls.Add(this.ribbonDescriptionButton1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfDescriptionButtonForm";
            this.Text = "DescriptionButton控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DescriptionButton ribbonDescriptionButton1;
    }
}
namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfBaseButtonNForm
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
            this.ribbonBaseButton7 = new GISShare.Controls.WinForm.WFNew.BaseButtonN();
            this.ribbonBaseButton6 = new GISShare.Controls.WinForm.WFNew.BaseButtonN();
            this.SuspendLayout();
            // 
            // ribbonBaseButton7
            // 
            this.ribbonBaseButton7.AutoPlanTextRectangle = false;
            this.ribbonBaseButton7.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBaseButton7.Image = null;
            this.ribbonBaseButton7.LeftBottomRadius = 10;
            this.ribbonBaseButton7.LeftTopRadius = 10;
            this.ribbonBaseButton7.Location = new System.Drawing.Point(38, 12);
            this.ribbonBaseButton7.Name = "ribbonBaseButton7";
            this.ribbonBaseButton7.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonBaseButton7.RightBottomRadius = 10;
            this.ribbonBaseButton7.RightTopRadius = 10;
            this.ribbonBaseButton7.Size = new System.Drawing.Size(126, 36);
            this.ribbonBaseButton7.TabIndex = 18;
            this.ribbonBaseButton7.Text = "圆角效果";
            // 
            // ribbonBaseButton6
            // 
            this.ribbonBaseButton6.AutoPlanTextRectangle = false;
            this.ribbonBaseButton6.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBaseButton6.Image = null;
            this.ribbonBaseButton6.LeftBottomRadius = 0;
            this.ribbonBaseButton6.LeftTopRadius = 0;
            this.ribbonBaseButton6.Location = new System.Drawing.Point(38, 54);
            this.ribbonBaseButton6.Name = "ribbonBaseButton6";
            this.ribbonBaseButton6.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonBaseButton6.RightBottomRadius = 0;
            this.ribbonBaseButton6.RightTopRadius = 0;
            this.ribbonBaseButton6.Size = new System.Drawing.Size(126, 36);
            this.ribbonBaseButton6.TabIndex = 17;
            this.ribbonBaseButton6.Text = "无圆角效果";
            // 
            // DemoOfBaseButtonNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 104);
            this.Controls.Add(this.ribbonBaseButton7);
            this.Controls.Add(this.ribbonBaseButton6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfBaseButtonNForm";
            this.Text = "BaseButtonN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.BaseButtonN ribbonBaseButton7;
        private GISShare.Controls.WinForm.WFNew.BaseButtonN ribbonBaseButton6;
    }
}
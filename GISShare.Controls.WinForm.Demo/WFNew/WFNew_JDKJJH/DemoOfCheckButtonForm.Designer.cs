namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfCheckButtonForm
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
            this.ribbonCheckButton6 = new GISShare.Controls.WinForm.WFNew.CheckButton();
            this.ribbonCheckButton7 = new GISShare.Controls.WinForm.WFNew.CheckButton();
            this.SuspendLayout();
            // 
            // ribbonCheckButton6
            // 
            this.ribbonCheckButton6.AutoPlanTextRectangle = false;
            this.ribbonCheckButton6.BackColor = System.Drawing.Color.Transparent;
            this.ribbonCheckButton6.Image = null;
            this.ribbonCheckButton6.LeftBottomRadius = 0;
            this.ribbonCheckButton6.LeftTopRadius = 0;
            this.ribbonCheckButton6.Location = new System.Drawing.Point(43, 54);
            this.ribbonCheckButton6.Name = "ribbonCheckButton6";
            this.ribbonCheckButton6.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonCheckButton6.RightBottomRadius = 0;
            this.ribbonCheckButton6.RightTopRadius = 0;
            this.ribbonCheckButton6.Size = new System.Drawing.Size(126, 36);
            this.ribbonCheckButton6.TabIndex = 25;
            this.ribbonCheckButton6.Text = "无圆角效果";
            // 
            // ribbonCheckButton7
            // 
            this.ribbonCheckButton7.AutoPlanTextRectangle = false;
            this.ribbonCheckButton7.BackColor = System.Drawing.Color.Transparent;
            this.ribbonCheckButton7.Image = null;
            this.ribbonCheckButton7.LeftBottomRadius = 10;
            this.ribbonCheckButton7.LeftTopRadius = 10;
            this.ribbonCheckButton7.Location = new System.Drawing.Point(43, 12);
            this.ribbonCheckButton7.Name = "ribbonCheckButton7";
            this.ribbonCheckButton7.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonCheckButton7.RightBottomRadius = 10;
            this.ribbonCheckButton7.RightTopRadius = 10;
            this.ribbonCheckButton7.Size = new System.Drawing.Size(126, 36);
            this.ribbonCheckButton7.TabIndex = 26;
            this.ribbonCheckButton7.Text = "圆角效果";
            // 
            // DemoOfCheckButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 103);
            this.Controls.Add(this.ribbonCheckButton7);
            this.Controls.Add(this.ribbonCheckButton6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfCheckButtonForm";
            this.Text = "CheckButton控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.CheckButton ribbonCheckButton6;
        private GISShare.Controls.WinForm.WFNew.CheckButton ribbonCheckButton7;
    }
}
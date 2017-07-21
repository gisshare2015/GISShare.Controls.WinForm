namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfSplitButtonExForm
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
            this.ribbonSplitButtonEx1 = new GISShare.Controls.WinForm.WFNew.SplitButtonEx();
            this.ribbonSplitButtonEx2 = new GISShare.Controls.WinForm.WFNew.SplitButtonEx();
            this.SuspendLayout();
            // 
            // ribbonSplitButtonEx1
            // 
            this.ribbonSplitButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonSplitButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonSplitButtonEx1.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eRight;
            this.ribbonSplitButtonEx1.Image = null;
            this.ribbonSplitButtonEx1.Location = new System.Drawing.Point(73, 12);
            this.ribbonSplitButtonEx1.Name = "ribbonSplitButtonEx1";
            this.ribbonSplitButtonEx1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonSplitButtonEx1.Size = new System.Drawing.Size(116, 29);
            this.ribbonSplitButtonEx1.TabIndex = 0;
            this.ribbonSplitButtonEx1.Text = "水平方向布局";
            // 
            // ribbonSplitButtonEx2
            // 
            this.ribbonSplitButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonSplitButtonEx2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonSplitButtonEx2.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eDown;
            this.ribbonSplitButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonSplitButtonEx2.Image = null;
            this.ribbonSplitButtonEx2.Location = new System.Drawing.Point(116, 47);
            this.ribbonSplitButtonEx2.Name = "ribbonSplitButtonEx2";
            this.ribbonSplitButtonEx2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonSplitButtonEx2.Size = new System.Drawing.Size(29, 116);
            this.ribbonSplitButtonEx2.TabIndex = 1;
            this.ribbonSplitButtonEx2.Text = "竖直方向布局";
            // 
            // DemoOfSplitButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 181);
            this.Controls.Add(this.ribbonSplitButtonEx2);
            this.Controls.Add(this.ribbonSplitButtonEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfSplitButtonExForm";
            this.Text = "SplitButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.SplitButtonEx ribbonSplitButtonEx1;
        private GISShare.Controls.WinForm.WFNew.SplitButtonEx ribbonSplitButtonEx2;
    }
}
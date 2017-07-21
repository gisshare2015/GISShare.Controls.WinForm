namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfDropDownButtonExForm
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
            this.ribbonDropDownButtonEx1 = new GISShare.Controls.WinForm.WFNew.DropDownButtonEx();
            this.ribbonDropDownButtonEx2 = new GISShare.Controls.WinForm.WFNew.DropDownButtonEx();
            this.SuspendLayout();
            // 
            // ribbonDropDownButtonEx1
            // 
            this.ribbonDropDownButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonDropDownButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonDropDownButtonEx1.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eRight;
            this.ribbonDropDownButtonEx1.Image = null;
            this.ribbonDropDownButtonEx1.Location = new System.Drawing.Point(73, 12);
            this.ribbonDropDownButtonEx1.Name = "ribbonDropDownButtonEx1";
            this.ribbonDropDownButtonEx1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonDropDownButtonEx1.Size = new System.Drawing.Size(116, 29);
            this.ribbonDropDownButtonEx1.TabIndex = 0;
            this.ribbonDropDownButtonEx1.Text = "水平方向布局";
            // 
            // ribbonDropDownButtonEx2
            // 
            this.ribbonDropDownButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonDropDownButtonEx2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonDropDownButtonEx2.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eDown;
            this.ribbonDropDownButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonDropDownButtonEx2.Image = null;
            this.ribbonDropDownButtonEx2.Location = new System.Drawing.Point(116, 47);
            this.ribbonDropDownButtonEx2.Name = "ribbonDropDownButtonEx2";
            this.ribbonDropDownButtonEx2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonDropDownButtonEx2.Size = new System.Drawing.Size(29, 116);
            this.ribbonDropDownButtonEx2.TabIndex = 1;
            this.ribbonDropDownButtonEx2.Text = "竖直方向布局";
            // 
            // DemoOfDropDownButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 181);
            this.Controls.Add(this.ribbonDropDownButtonEx2);
            this.Controls.Add(this.ribbonDropDownButtonEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfDropDownButtonExForm";
            this.Text = "DropDownButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DropDownButtonEx ribbonDropDownButtonEx1;
        private GISShare.Controls.WinForm.WFNew.DropDownButtonEx ribbonDropDownButtonEx2;
    }
}
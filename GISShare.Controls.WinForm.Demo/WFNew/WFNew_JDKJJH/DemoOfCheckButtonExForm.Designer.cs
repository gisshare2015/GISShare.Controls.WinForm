namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfCheckButtonExForm
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
            this.ribbonCheckButtonEx2 = new GISShare.Controls.WinForm.WFNew.CheckButtonEx();
            this.ribbonCheckButtonEx1 = new GISShare.Controls.WinForm.WFNew.CheckButtonEx();
            this.SuspendLayout();
            // 
            // ribbonCheckButtonEx2
            // 
            this.ribbonCheckButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonCheckButtonEx2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonCheckButtonEx2.Checked = true;
            this.ribbonCheckButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonCheckButtonEx2.Image = null;
            this.ribbonCheckButtonEx2.Location = new System.Drawing.Point(92, 47);
            this.ribbonCheckButtonEx2.Name = "ribbonCheckButtonEx2";
            this.ribbonCheckButtonEx2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonCheckButtonEx2.Size = new System.Drawing.Size(29, 116);
            this.ribbonCheckButtonEx2.TabIndex = 3;
            this.ribbonCheckButtonEx2.Text = "竖直方向布局";
            // 
            // ribbonCheckButtonEx1
            // 
            this.ribbonCheckButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonCheckButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonCheckButtonEx1.Image = null;
            this.ribbonCheckButtonEx1.Location = new System.Drawing.Point(49, 12);
            this.ribbonCheckButtonEx1.Name = "ribbonCheckButtonEx1";
            this.ribbonCheckButtonEx1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonCheckButtonEx1.Size = new System.Drawing.Size(116, 29);
            this.ribbonCheckButtonEx1.TabIndex = 2;
            this.ribbonCheckButtonEx1.Text = "水平方向布局";
            // 
            // DemoOfCheckButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 168);
            this.Controls.Add(this.ribbonCheckButtonEx2);
            this.Controls.Add(this.ribbonCheckButtonEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfCheckButtonExForm";
            this.Text = "CheckButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.CheckButtonEx ribbonCheckButtonEx2;
        private GISShare.Controls.WinForm.WFNew.CheckButtonEx ribbonCheckButtonEx1;
    }
}
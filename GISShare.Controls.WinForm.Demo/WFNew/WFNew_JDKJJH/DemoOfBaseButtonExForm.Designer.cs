namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfBaseButtonExForm
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
            this.ribbonBaseButtonEx1 = new GISShare.Controls.WinForm.WFNew.BaseButtonEx();
            this.ribbonBaseButtonEx2 = new GISShare.Controls.WinForm.WFNew.BaseButtonEx();
            this.SuspendLayout();
            // 
            // ribbonBaseButtonEx1
            // 
            this.ribbonBaseButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonBaseButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBaseButtonEx1.Image = null;
            this.ribbonBaseButtonEx1.Location = new System.Drawing.Point(50, 12);
            this.ribbonBaseButtonEx1.Name = "ribbonBaseButtonEx1";
            this.ribbonBaseButtonEx1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonBaseButtonEx1.Size = new System.Drawing.Size(116, 29);
            this.ribbonBaseButtonEx1.TabIndex = 0;
            this.ribbonBaseButtonEx1.Text = "水平方向布局";
            // 
            // ribbonBaseButtonEx2
            // 
            this.ribbonBaseButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonBaseButtonEx2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBaseButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonBaseButtonEx2.Image = null;
            this.ribbonBaseButtonEx2.Location = new System.Drawing.Point(94, 47);
            this.ribbonBaseButtonEx2.Name = "ribbonBaseButtonEx2";
            this.ribbonBaseButtonEx2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonBaseButtonEx2.Size = new System.Drawing.Size(29, 116);
            this.ribbonBaseButtonEx2.TabIndex = 1;
            this.ribbonBaseButtonEx2.Text = "竖直方向布局";
            // 
            // DemoOfBaseButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 174);
            this.Controls.Add(this.ribbonBaseButtonEx2);
            this.Controls.Add(this.ribbonBaseButtonEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfBaseButtonExForm";
            this.Text = "BaseButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.BaseButtonEx ribbonBaseButtonEx1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonEx ribbonBaseButtonEx2;
    }
}
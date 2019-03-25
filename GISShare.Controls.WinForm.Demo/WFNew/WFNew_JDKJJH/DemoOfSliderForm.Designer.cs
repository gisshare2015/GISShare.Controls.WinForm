namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfSliderForm
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
            this.ribbonSlider1 = new GISShare.Controls.WinForm.WFNew.BaseItemHostControl();
            this.ribbonSlider2 = new GISShare.Controls.WinForm.WFNew.BaseItemHostControl();
            this.SuspendLayout();
            // 
            // ribbonSlider1
            // 
            //this.ribbonSlider1.Location = new System.Drawing.Point(12, 12);
            //this.ribbonSlider1.LockHeight = true;
            //this.ribbonSlider1.Maximum = 100;
            //this.ribbonSlider1.Minimum = 0;
            //this.ribbonSlider1.Name = "ribbonSlider1";
            //this.ribbonSlider1.Padding = new System.Windows.Forms.Padding(0);
            //this.ribbonSlider1.Size = new System.Drawing.Size(160, 20);
            //this.ribbonSlider1.Step = 1;
            //this.ribbonSlider1.TabIndex = 0;
            //this.ribbonSlider1.Text = "ribbonSlider1";
            //this.ribbonSlider1.Value = 50;
            // 
            // ribbonSlider2
            // 
            //this.ribbonSlider2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            //this.ribbonSlider2.Location = new System.Drawing.Point(80, 38);
            //this.ribbonSlider2.LockWith = true;
            //this.ribbonSlider2.Maximum = 100;
            //this.ribbonSlider2.Minimum = 0;
            //this.ribbonSlider2.Name = "ribbonSlider2";
            //this.ribbonSlider2.Padding = new System.Windows.Forms.Padding(0);
            //this.ribbonSlider2.Size = new System.Drawing.Size(24, 125);
            //this.ribbonSlider2.Step = 1;
            //this.ribbonSlider2.TabIndex = 1;
            //this.ribbonSlider2.Text = "ribbonSlider2";
            //this.ribbonSlider2.Value = 60;
            // 
            // DemoOfSliderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 174);
            this.Controls.Add(this.ribbonSlider2);
            this.Controls.Add(this.ribbonSlider1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfSliderForm";
            this.Text = "Slider控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.BaseItemHostControl ribbonSlider1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHostControl ribbonSlider2;
    }
}
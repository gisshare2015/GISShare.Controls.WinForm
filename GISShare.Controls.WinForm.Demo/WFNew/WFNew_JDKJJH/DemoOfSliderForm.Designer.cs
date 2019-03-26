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
            this.ribbonSlider1 = new GISShare.Controls.WinForm.WFNew.SliderItem();
            this.ribbonSlider2 = new GISShare.Controls.WinForm.WFNew.SliderItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonSlider1
            // 
            this.ribbonSlider1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSlider1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSlider1.Location = new System.Drawing.Point(0, 0);
            this.ribbonSlider1.LockHeight = true;
            this.ribbonSlider1.Maximum = 100D;
            this.ribbonSlider1.Minimum = 0D;
            this.ribbonSlider1.Name = "ribbonSlider1";
            this.ribbonSlider1.Size = new System.Drawing.Size(240, 20);
            this.ribbonSlider1.Step = 1D;
            this.ribbonSlider1.Tag = null;
            this.ribbonSlider1.Text = "ribbonSlider1";
            this.ribbonSlider1.Value = 50D;
            // 
            // ribbonSlider2
            // 
            this.ribbonSlider2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonSlider2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSlider2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSlider2.Location = new System.Drawing.Point(0, 0);
            this.ribbonSlider2.LockWith = true;
            this.ribbonSlider2.Maximum = 100D;
            this.ribbonSlider2.Minimum = 0D;
            this.ribbonSlider2.Name = "ribbonSlider2";
            this.ribbonSlider2.Size = new System.Drawing.Size(24, 188);
            this.ribbonSlider2.Step = 1D;
            this.ribbonSlider2.Tag = null;
            this.ribbonSlider2.Text = "ribbonSlider2";
            this.ribbonSlider2.Value = 60D;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.ribbonSlider1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(240, 20);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.ribbonSlider2;
            this.baseItemHost2.Location = new System.Drawing.Point(120, 57);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(24, 188);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfSliderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 289);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfSliderForm";
            this.Text = "Slider控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.SliderItem ribbonSlider1;
        private GISShare.Controls.WinForm.WFNew.SliderItem ribbonSlider2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
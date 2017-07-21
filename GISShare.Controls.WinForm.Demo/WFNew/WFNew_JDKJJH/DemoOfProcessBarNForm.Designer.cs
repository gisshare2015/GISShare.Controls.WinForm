namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfProcessBarNForm
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
            this.ribbonProcessBar1 = new GISShare.Controls.WinForm.WFNew.ProcessBarN();
            this.ribbonProcessBar2 = new GISShare.Controls.WinForm.WFNew.ProcessBarN();
            this.SuspendLayout();
            // 
            // ribbonProcessBar1
            // 
            this.ribbonProcessBar1.Location = new System.Drawing.Point(12, 12);
            this.ribbonProcessBar1.Name = "ribbonProcessBar1";
            this.ribbonProcessBar1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonProcessBar1.Size = new System.Drawing.Size(176, 20);
            this.ribbonProcessBar1.TabIndex = 0;
            this.ribbonProcessBar1.Text = "ribbonProcessBar1";
            this.ribbonProcessBar1.Value = 50;
            // 
            // ribbonProcessBar2
            // 
            this.ribbonProcessBar2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonProcessBar2.Location = new System.Drawing.Point(89, 38);
            this.ribbonProcessBar2.Name = "ribbonProcessBar2";
            this.ribbonProcessBar2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonProcessBar2.Size = new System.Drawing.Size(21, 127);
            this.ribbonProcessBar2.TabIndex = 1;
            this.ribbonProcessBar2.Text = "ribbonProcessBar2";
            this.ribbonProcessBar2.Value = 60;
            // 
            // DemoOfProcessBarNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 173);
            this.Controls.Add(this.ribbonProcessBar2);
            this.Controls.Add(this.ribbonProcessBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfProcessBarNForm";
            this.Text = "ProcessBarN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ProcessBarN ribbonProcessBar1;
        private GISShare.Controls.WinForm.WFNew.ProcessBarN ribbonProcessBar2;
    }
}
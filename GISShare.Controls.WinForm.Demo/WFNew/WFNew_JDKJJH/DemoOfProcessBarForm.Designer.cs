namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfProcessBarForm
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
            this.ribbonProcessBar1 = new GISShare.Controls.WinForm.WFNew.ProcessBarItem();
            this.ribbonProcessBar2 = new GISShare.Controls.WinForm.WFNew.ProcessBarItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonProcessBar1
            // 
            this.ribbonProcessBar1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonProcessBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonProcessBar1.Location = new System.Drawing.Point(0, 0);
            this.ribbonProcessBar1.Name = "ribbonProcessBar1";
            this.ribbonProcessBar1.Size = new System.Drawing.Size(264, 30);
            this.ribbonProcessBar1.Tag = null;
            this.ribbonProcessBar1.Text = "ribbonProcessBar1";
            this.ribbonProcessBar1.Value = 50;
            // 
            // ribbonProcessBar2
            // 
            this.ribbonProcessBar2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonProcessBar2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonProcessBar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonProcessBar2.Location = new System.Drawing.Point(0, 0);
            this.ribbonProcessBar2.Name = "ribbonProcessBar2";
            this.ribbonProcessBar2.Size = new System.Drawing.Size(32, 190);
            this.ribbonProcessBar2.Tag = null;
            this.ribbonProcessBar2.Text = "ribbonProcessBar2";
            this.ribbonProcessBar2.Value = 60;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.ribbonProcessBar1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(264, 30);
            this.baseItemHost1.TabIndex = 1;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.ribbonProcessBar2;
            this.baseItemHost2.Location = new System.Drawing.Point(134, 57);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(32, 190);
            this.baseItemHost2.TabIndex = 0;
            // 
            // DemoOfProcessBarNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 288);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfProcessBarNForm";
            this.Text = "ProcessBarN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ProcessBarItem ribbonProcessBar1;
        private GISShare.Controls.WinForm.WFNew.ProcessBarItem ribbonProcessBar2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
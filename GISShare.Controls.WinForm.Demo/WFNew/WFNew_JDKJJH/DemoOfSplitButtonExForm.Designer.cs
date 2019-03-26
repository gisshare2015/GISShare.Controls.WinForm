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
            this.ribbonSplitButtonEx1 = new GISShare.Controls.WinForm.WFNew.SplitButtonExItem();
            this.ribbonSplitButtonEx2 = new GISShare.Controls.WinForm.WFNew.SplitButtonExItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonSplitButtonEx1
            // 
            this.ribbonSplitButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonSplitButtonEx1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSplitButtonEx1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSplitButtonEx1.Image = null;
            this.ribbonSplitButtonEx1.Location = new System.Drawing.Point(0, 0);
            this.ribbonSplitButtonEx1.Name = "ribbonSplitButtonEx1";
            this.ribbonSplitButtonEx1.ShowNomalState = true;
            this.ribbonSplitButtonEx1.Size = new System.Drawing.Size(174, 44);
            this.ribbonSplitButtonEx1.Tag = null;
            this.ribbonSplitButtonEx1.Text = "水平方向布局";
            // 
            // ribbonSplitButtonEx2
            // 
            this.ribbonSplitButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonSplitButtonEx2.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eDown;
            this.ribbonSplitButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonSplitButtonEx2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSplitButtonEx2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSplitButtonEx2.Image = null;
            this.ribbonSplitButtonEx2.Location = new System.Drawing.Point(0, 0);
            this.ribbonSplitButtonEx2.Name = "ribbonSplitButtonEx2";
            this.ribbonSplitButtonEx2.ShowNomalState = true;
            this.ribbonSplitButtonEx2.Size = new System.Drawing.Size(44, 174);
            this.ribbonSplitButtonEx2.Tag = null;
            this.ribbonSplitButtonEx2.Text = "竖直方向布局";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonSplitButtonEx1;
            this.baseItemHost1.Location = new System.Drawing.Point(110, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(174, 44);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonSplitButtonEx2;
            this.baseItemHost2.Location = new System.Drawing.Point(174, 70);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(44, 174);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfSplitButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 300);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfSplitButtonExForm";
            this.Text = "SplitButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.SplitButtonExItem ribbonSplitButtonEx1;
        private GISShare.Controls.WinForm.WFNew.SplitButtonExItem ribbonSplitButtonEx2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
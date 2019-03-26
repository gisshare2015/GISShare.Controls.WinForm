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
            this.ribbonDropDownButtonEx1 = new GISShare.Controls.WinForm.WFNew.DropDownButtonExItem();
            this.ribbonDropDownButtonEx2 = new GISShare.Controls.WinForm.WFNew.DropDownButtonExItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonDropDownButtonEx1
            // 
            this.ribbonDropDownButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonDropDownButtonEx1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonDropDownButtonEx1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonDropDownButtonEx1.Image = null;
            this.ribbonDropDownButtonEx1.Location = new System.Drawing.Point(0, 0);
            this.ribbonDropDownButtonEx1.Name = "ribbonDropDownButtonEx1";
            this.ribbonDropDownButtonEx1.ShowNomalState = true;
            this.ribbonDropDownButtonEx1.Size = new System.Drawing.Size(174, 44);
            this.ribbonDropDownButtonEx1.Tag = null;
            this.ribbonDropDownButtonEx1.Text = "水平方向布局";
            // 
            // ribbonDropDownButtonEx2
            // 
            this.ribbonDropDownButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonDropDownButtonEx2.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eDown;
            this.ribbonDropDownButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonDropDownButtonEx2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonDropDownButtonEx2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonDropDownButtonEx2.Image = null;
            this.ribbonDropDownButtonEx2.Location = new System.Drawing.Point(0, 0);
            this.ribbonDropDownButtonEx2.Name = "ribbonDropDownButtonEx2";
            this.ribbonDropDownButtonEx2.ShowNomalState = true;
            this.ribbonDropDownButtonEx2.Size = new System.Drawing.Size(44, 174);
            this.ribbonDropDownButtonEx2.Tag = null;
            this.ribbonDropDownButtonEx2.Text = "竖直方向布局";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonDropDownButtonEx1;
            this.baseItemHost1.Location = new System.Drawing.Point(110, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(174, 44);
            this.baseItemHost1.TabIndex = 1;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonDropDownButtonEx2;
            this.baseItemHost2.Location = new System.Drawing.Point(174, 70);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(44, 174);
            this.baseItemHost2.TabIndex = 0;
            // 
            // DemoOfDropDownButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 300);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfDropDownButtonExForm";
            this.Text = "DropDownButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DropDownButtonExItem ribbonDropDownButtonEx1;
        private GISShare.Controls.WinForm.WFNew.DropDownButtonExItem ribbonDropDownButtonEx2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
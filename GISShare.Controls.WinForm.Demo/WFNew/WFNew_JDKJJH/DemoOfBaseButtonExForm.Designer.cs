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
            this.ribbonBaseButtonEx1 = new GISShare.Controls.WinForm.WFNew.BaseButtonExItem();
            this.ribbonBaseButtonEx2 = new GISShare.Controls.WinForm.WFNew.BaseButtonExItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonBaseButtonEx1
            // 
            this.ribbonBaseButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonBaseButtonEx1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonBaseButtonEx1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonBaseButtonEx1.Image = null;
            this.ribbonBaseButtonEx1.Location = new System.Drawing.Point(0, 0);
            this.ribbonBaseButtonEx1.Name = "ribbonBaseButtonEx1";
            this.ribbonBaseButtonEx1.ShowNomalState = true;
            this.ribbonBaseButtonEx1.Size = new System.Drawing.Size(174, 44);
            this.ribbonBaseButtonEx1.Tag = null;
            this.ribbonBaseButtonEx1.Text = "水平方向布局";
            // 
            // ribbonBaseButtonEx2
            // 
            this.ribbonBaseButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonBaseButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonBaseButtonEx2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonBaseButtonEx2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonBaseButtonEx2.Image = null;
            this.ribbonBaseButtonEx2.Location = new System.Drawing.Point(0, 0);
            this.ribbonBaseButtonEx2.Name = "ribbonBaseButtonEx2";
            this.ribbonBaseButtonEx2.ShowNomalState = true;
            this.ribbonBaseButtonEx2.Size = new System.Drawing.Size(44, 174);
            this.ribbonBaseButtonEx2.Tag = null;
            this.ribbonBaseButtonEx2.Text = "竖直方向布局";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonBaseButtonEx1;
            this.baseItemHost1.Location = new System.Drawing.Point(75, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(174, 44);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonBaseButtonEx2;
            this.baseItemHost2.Location = new System.Drawing.Point(141, 70);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(44, 174);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfBaseButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 313);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DemoOfBaseButtonExForm";
            this.Text = "BaseButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.BaseButtonExItem ribbonBaseButtonEx1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonExItem ribbonBaseButtonEx2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
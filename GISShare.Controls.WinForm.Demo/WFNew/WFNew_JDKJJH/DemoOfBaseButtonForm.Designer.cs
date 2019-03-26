namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfBaseButtonForm
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
            this.ribbonBaseButton7 = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.ribbonBaseButton6 = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonBaseButton7
            // 
            this.ribbonBaseButton7.AutoPlanTextRectangle = false;
            this.ribbonBaseButton7.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonBaseButton7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonBaseButton7.Image = null;
            this.ribbonBaseButton7.LeftBottomRadius = 10;
            this.ribbonBaseButton7.LeftTopRadius = 10;
            this.ribbonBaseButton7.Location = new System.Drawing.Point(0, 0);
            this.ribbonBaseButton7.Name = "ribbonBaseButton7";
            this.ribbonBaseButton7.RightBottomRadius = 10;
            this.ribbonBaseButton7.RightTopRadius = 10;
            this.ribbonBaseButton7.ShowNomalState = true;
            this.ribbonBaseButton7.Size = new System.Drawing.Size(189, 54);
            this.ribbonBaseButton7.Tag = null;
            this.ribbonBaseButton7.Text = "圆角效果";
            // 
            // ribbonBaseButton6
            // 
            this.ribbonBaseButton6.AutoPlanTextRectangle = false;
            this.ribbonBaseButton6.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonBaseButton6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonBaseButton6.Image = null;
            this.ribbonBaseButton6.LeftBottomRadius = 0;
            this.ribbonBaseButton6.LeftTopRadius = 0;
            this.ribbonBaseButton6.Location = new System.Drawing.Point(0, 0);
            this.ribbonBaseButton6.Name = "ribbonBaseButton6";
            this.ribbonBaseButton6.RightBottomRadius = 0;
            this.ribbonBaseButton6.RightTopRadius = 0;
            this.ribbonBaseButton6.ShowNomalState = true;
            this.ribbonBaseButton6.Size = new System.Drawing.Size(189, 54);
            this.ribbonBaseButton6.Tag = null;
            this.ribbonBaseButton6.Text = "无圆角效果";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonBaseButton7;
            this.baseItemHost1.Location = new System.Drawing.Point(57, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(189, 54);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonBaseButton6;
            this.baseItemHost2.Location = new System.Drawing.Point(57, 81);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(189, 54);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfBaseButtonNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 184);
            this.Controls.Add(this.baseItemHost1);
            this.Controls.Add(this.baseItemHost2);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfBaseButtonNForm";
            this.Text = "BaseButtonN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.BaseButtonItem ribbonBaseButton7;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem ribbonBaseButton6;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfCheckButtonForm
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
            this.ribbonCheckButton6 = new GISShare.Controls.WinForm.WFNew.CheckButtonItem();
            this.ribbonCheckButton7 = new GISShare.Controls.WinForm.WFNew.CheckButtonItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonCheckButton6
            // 
            this.ribbonCheckButton6.AutoPlanTextRectangle = false;
            this.ribbonCheckButton6.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonCheckButton6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonCheckButton6.Image = null;
            this.ribbonCheckButton6.LeftBottomRadius = 0;
            this.ribbonCheckButton6.LeftTopRadius = 0;
            this.ribbonCheckButton6.Location = new System.Drawing.Point(0, 0);
            this.ribbonCheckButton6.Name = "ribbonCheckButton6";
            this.ribbonCheckButton6.RightBottomRadius = 0;
            this.ribbonCheckButton6.RightTopRadius = 0;
            this.ribbonCheckButton6.ShowNomalState = true;
            this.ribbonCheckButton6.Size = new System.Drawing.Size(189, 54);
            this.ribbonCheckButton6.Tag = null;
            this.ribbonCheckButton6.Text = "无圆角效果";
            // 
            // ribbonCheckButton7
            // 
            this.ribbonCheckButton7.AutoPlanTextRectangle = false;
            this.ribbonCheckButton7.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonCheckButton7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonCheckButton7.Image = null;
            this.ribbonCheckButton7.LeftBottomRadius = 10;
            this.ribbonCheckButton7.LeftTopRadius = 10;
            this.ribbonCheckButton7.Location = new System.Drawing.Point(0, 0);
            this.ribbonCheckButton7.Name = "ribbonCheckButton7";
            this.ribbonCheckButton7.RightBottomRadius = 10;
            this.ribbonCheckButton7.RightTopRadius = 10;
            this.ribbonCheckButton7.ShowNomalState = true;
            this.ribbonCheckButton7.Size = new System.Drawing.Size(189, 54);
            this.ribbonCheckButton7.Tag = null;
            this.ribbonCheckButton7.Text = "圆角效果";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonCheckButton6;
            this.baseItemHost1.Location = new System.Drawing.Point(64, 81);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(189, 54);
            this.baseItemHost1.TabIndex = 25;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonCheckButton7;
            this.baseItemHost2.Location = new System.Drawing.Point(64, 18);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(189, 54);
            this.baseItemHost2.TabIndex = 26;
            // 
            // DemoOfCheckButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 182);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfCheckButtonForm";
            this.Text = "CheckButton控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.CheckButtonItem ribbonCheckButton6;
        private GISShare.Controls.WinForm.WFNew.CheckButtonItem ribbonCheckButton7;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
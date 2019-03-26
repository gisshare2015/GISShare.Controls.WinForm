namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfCheckButtonExForm
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
            this.ribbonCheckButtonEx2 = new GISShare.Controls.WinForm.WFNew.CheckButtonExItem();
            this.ribbonCheckButtonEx1 = new GISShare.Controls.WinForm.WFNew.CheckButtonExItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonCheckButtonEx2
            // 
            this.ribbonCheckButtonEx2.AutoPlanTextRectangle = false;
            this.ribbonCheckButtonEx2.Checked = true;
            this.ribbonCheckButtonEx2.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonCheckButtonEx2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonCheckButtonEx2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonCheckButtonEx2.Image = null;
            this.ribbonCheckButtonEx2.Location = new System.Drawing.Point(0, 0);
            this.ribbonCheckButtonEx2.Name = "ribbonCheckButtonEx2";
            this.ribbonCheckButtonEx2.ShowNomalState = true;
            this.ribbonCheckButtonEx2.Size = new System.Drawing.Size(44, 174);
            this.ribbonCheckButtonEx2.Tag = null;
            this.ribbonCheckButtonEx2.Text = "竖直方向布局";
            // 
            // ribbonCheckButtonEx1
            // 
            this.ribbonCheckButtonEx1.AutoPlanTextRectangle = false;
            this.ribbonCheckButtonEx1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonCheckButtonEx1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonCheckButtonEx1.Image = null;
            this.ribbonCheckButtonEx1.Location = new System.Drawing.Point(0, 0);
            this.ribbonCheckButtonEx1.Name = "ribbonCheckButtonEx1";
            this.ribbonCheckButtonEx1.ShowNomalState = true;
            this.ribbonCheckButtonEx1.Size = new System.Drawing.Size(174, 44);
            this.ribbonCheckButtonEx1.Tag = null;
            this.ribbonCheckButtonEx1.Text = "水平方向布局";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonCheckButtonEx2;
            this.baseItemHost1.Location = new System.Drawing.Point(138, 70);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(44, 174);
            this.baseItemHost1.TabIndex = 3;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonCheckButtonEx1;
            this.baseItemHost2.Location = new System.Drawing.Point(74, 18);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(174, 44);
            this.baseItemHost2.TabIndex = 2;
            // 
            // DemoOfCheckButtonExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 280);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfCheckButtonExForm";
            this.Text = "CheckButtonEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.CheckButtonExItem ribbonCheckButtonEx2;
        private GISShare.Controls.WinForm.WFNew.CheckButtonExItem ribbonCheckButtonEx1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
namespace GISShare.Controls.WinForm.WFNew.Forms
{
    partial class TBMessageBoxForm
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
            this.btnButton3 = new GISShare.Controls.WinForm.WFNew.BaseButtonN();
            this.btnButton2 = new GISShare.Controls.WinForm.WFNew.BaseButtonN();
            this.btnButton1 = new GISShare.Controls.WinForm.WFNew.BaseButtonN();
            this.SuspendLayout();
            // 
            // btnButton3
            // 
            this.btnButton3.AutoPlanTextRectangle = false;
            this.btnButton3.BackColor = System.Drawing.Color.Transparent;
            this.btnButton3.Image = null;
            this.btnButton3.Location = new System.Drawing.Point(239, 94);
            this.btnButton3.Name = "btnButton3";
            this.btnButton3.Padding = new System.Windows.Forms.Padding(0);
            this.btnButton3.Size = new System.Drawing.Size(90, 26);
            this.btnButton3.TabIndex = 0;
            this.btnButton3.Text = "ribbonBaseButton1";
            // 
            // btnButton2
            // 
            this.btnButton2.AutoPlanTextRectangle = false;
            this.btnButton2.BackColor = System.Drawing.Color.Transparent;
            this.btnButton2.Image = null;
            this.btnButton2.Location = new System.Drawing.Point(134, 94);
            this.btnButton2.Name = "btnButton2";
            this.btnButton2.Padding = new System.Windows.Forms.Padding(0);
            this.btnButton2.Size = new System.Drawing.Size(90, 26);
            this.btnButton2.TabIndex = 1;
            this.btnButton2.Text = "ribbonBaseButton2";
            // 
            // btnButton1
            // 
            this.btnButton1.AutoPlanTextRectangle = false;
            this.btnButton1.BackColor = System.Drawing.Color.Transparent;
            this.btnButton1.Image = null;
            this.btnButton1.Location = new System.Drawing.Point(26, 94);
            this.btnButton1.Name = "btnButton1";
            this.btnButton1.Padding = new System.Windows.Forms.Padding(0);
            this.btnButton1.Size = new System.Drawing.Size(90, 26);
            this.btnButton1.TabIndex = 2;
            this.btnButton1.Text = "ribbonBaseButton3";
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(341, 132);
            this.Controls.Add(this.btnButton1);
            this.Controls.Add(this.btnButton2);
            this.Controls.Add(this.btnButton3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //this.Text = "MessageBoxForm";
            this.ResumeLayout(false);

        }

        #endregion

        private BaseButtonN btnButton3;
        private BaseButtonN btnButton2;
        private BaseButtonN btnButton1;
    }
}
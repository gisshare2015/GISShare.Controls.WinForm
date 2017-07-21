namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfTextBoxNForm
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
            this.ribbonTextBox1 = new GISShare.Controls.WinForm.WFNew.TextBoxN();
            this.ribbonTextBox2 = new GISShare.Controls.WinForm.WFNew.TextBoxN();
            this.SuspendLayout();
            // 
            // ribbonTextBox1
            // 
            this.ribbonTextBox1.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eNone;
            this.ribbonTextBox1.Location = new System.Drawing.Point(12, 12);
            this.ribbonTextBox1.LockHeight = true;
            this.ribbonTextBox1.Name = "ribbonTextBox1";
            this.ribbonTextBox1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonTextBox1.Size = new System.Drawing.Size(260, 14);
            this.ribbonTextBox1.TabIndex = 0;
            this.ribbonTextBox1.Text = "ribbonTextBox1";
            // 
            // ribbonTextBox2
            // 
            this.ribbonTextBox2.Enabled = false;
            this.ribbonTextBox2.Location = new System.Drawing.Point(12, 39);
            this.ribbonTextBox2.LockHeight = true;
            this.ribbonTextBox2.Name = "ribbonTextBox2";
            this.ribbonTextBox2.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonTextBox2.Size = new System.Drawing.Size(260, 20);
            this.ribbonTextBox2.TabIndex = 1;
            this.ribbonTextBox2.Text = "我是文本输入框对象";
            // 
            // DemoOfTextBoxNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 72);
            this.Controls.Add(this.ribbonTextBox2);
            this.Controls.Add(this.ribbonTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfTextBoxNForm";
            this.Text = "TextBoxN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.TextBoxN ribbonTextBox1;
        private GISShare.Controls.WinForm.WFNew.TextBoxN ribbonTextBox2;
    }
}
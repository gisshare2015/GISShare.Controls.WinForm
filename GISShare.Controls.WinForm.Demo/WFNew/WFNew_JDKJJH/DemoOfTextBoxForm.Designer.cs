namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfTextBoxForm
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
            this.ribbonTextBox1 = new GISShare.Controls.WinForm.WFNew.TextBoxItem();
            this.ribbonTextBox2 = new GISShare.Controls.WinForm.WFNew.TextBoxItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ribbonTextBox1
            // 
            this.ribbonTextBox1.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eNone;
            this.ribbonTextBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonTextBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonTextBox1.Location = new System.Drawing.Point(0, 0);
            this.ribbonTextBox1.LockHeight = true;
            this.ribbonTextBox1.Name = "ribbonTextBox1";
            this.ribbonTextBox1.PasswordChar = '\0';
            this.ribbonTextBox1.Size = new System.Drawing.Size(390, 21);
            this.ribbonTextBox1.Tag = null;
            this.ribbonTextBox1.Text = "ribbonTextBox1";
            // 
            // ribbonTextBox2
            // 
            this.ribbonTextBox2.Enabled = false;
            this.ribbonTextBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonTextBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonTextBox2.Location = new System.Drawing.Point(0, 0);
            this.ribbonTextBox2.LockHeight = true;
            this.ribbonTextBox2.Name = "ribbonTextBox2";
            this.ribbonTextBox2.PasswordChar = '\0';
            this.ribbonTextBox2.Size = new System.Drawing.Size(390, 27);
            this.ribbonTextBox2.Tag = null;
            this.ribbonTextBox2.Text = "我是文本输入框对象";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ribbonTextBox1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(390, 21);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ribbonTextBox2;
            this.baseItemHost2.Location = new System.Drawing.Point(18, 58);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(390, 27);
            this.baseItemHost2.TabIndex = 1;
            // 
            // DemoOfTextBoxNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 136);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfTextBoxNForm";
            this.Text = "TextBoxN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.TextBoxItem ribbonTextBox1;
        private GISShare.Controls.WinForm.WFNew.TextBoxItem ribbonTextBox2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
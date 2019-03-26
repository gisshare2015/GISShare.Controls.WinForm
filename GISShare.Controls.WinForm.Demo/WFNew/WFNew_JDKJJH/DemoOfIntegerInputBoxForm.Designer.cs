namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfIntegerInputBoxForm
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
            this.integerInputBox1 = new GISShare.Controls.WinForm.WFNew.IntegerInputBoxItem();
            this.integerInputBox2 = new GISShare.Controls.WinForm.WFNew.IntegerInputBoxItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // integerInputBox1
            // 
            this.integerInputBox1.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eNone;
            this.integerInputBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.integerInputBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.integerInputBox1.Location = new System.Drawing.Point(0, 0);
            this.integerInputBox1.LockHeight = true;
            this.integerInputBox1.Maximum = 2147483647;
            this.integerInputBox1.Minimum = -2147483648;
            this.integerInputBox1.Name = "integerInputBox1";
            this.integerInputBox1.Size = new System.Drawing.Size(585, 21);
            this.integerInputBox1.Tag = null;
            this.integerInputBox1.Text = "integerInputBox1";
            this.integerInputBox1.Value = 6;
            // 
            // integerInputBox2
            // 
            this.integerInputBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.integerInputBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.integerInputBox2.Location = new System.Drawing.Point(0, 0);
            this.integerInputBox2.LockHeight = true;
            this.integerInputBox2.Maximum = 2147483647;
            this.integerInputBox2.Minimum = -2147483648;
            this.integerInputBox2.Name = "integerInputBox2";
            this.integerInputBox2.Size = new System.Drawing.Size(585, 27);
            this.integerInputBox2.Tag = null;
            this.integerInputBox2.Text = "integerInputBox2";
            this.integerInputBox2.Value = 6;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.integerInputBox1;
            this.baseItemHost1.Location = new System.Drawing.Point(27, 27);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(585, 21);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.integerInputBox2;
            this.baseItemHost2.Location = new System.Drawing.Point(27, 90);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(585, 27);
            this.baseItemHost2.TabIndex = 0;
            // 
            // DemoOfIntegerInputBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 148);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfIntegerInputBoxForm";
            this.Text = "IntegerInputBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.IntegerInputBoxItem integerInputBox1;
        private GISShare.Controls.WinForm.WFNew.IntegerInputBoxItem integerInputBox2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
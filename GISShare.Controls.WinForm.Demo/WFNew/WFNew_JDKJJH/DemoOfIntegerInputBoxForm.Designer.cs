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
            this.integerInputBox1 = new GISShare.Controls.WinForm.WFNew.IntegerInputBox();
            this.integerInputBox2 = new GISShare.Controls.WinForm.WFNew.IntegerInputBox();
            this.SuspendLayout();
            // 
            // integerInputBox1
            // 
            this.integerInputBox1.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eNone;
            this.integerInputBox1.Location = new System.Drawing.Point(12, 12);
            this.integerInputBox1.LockHeight = true;
            this.integerInputBox1.Maximum = 2147483647;
            this.integerInputBox1.Minimum = -2147483648;
            this.integerInputBox1.Name = "integerInputBox1";
            this.integerInputBox1.Padding = new System.Windows.Forms.Padding(0);
            this.integerInputBox1.Size = new System.Drawing.Size(260, 14);
            this.integerInputBox1.TabIndex = 0;
            this.integerInputBox1.Text = "integerInputBox1";
            this.integerInputBox1.Value = 6;
            // 
            // integerInputBox2
            // 
            this.integerInputBox2.Location = new System.Drawing.Point(12, 40);
            this.integerInputBox2.LockHeight = true;
            this.integerInputBox2.Maximum = 2147483647;
            this.integerInputBox2.Minimum = -2147483648;
            this.integerInputBox2.Name = "integerInputBox2";
            this.integerInputBox2.Padding = new System.Windows.Forms.Padding(0);
            this.integerInputBox2.Size = new System.Drawing.Size(260, 20);
            this.integerInputBox2.TabIndex = 1;
            this.integerInputBox2.Text = "integerInputBox2";
            this.integerInputBox2.Value = 6;
            // 
            // DemoOfIntegerInputBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 72);
            this.Controls.Add(this.integerInputBox2);
            this.Controls.Add(this.integerInputBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfIntegerInputBoxForm";
            this.Text = "IntegerInputBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.IntegerInputBox integerInputBox1;
        private GISShare.Controls.WinForm.WFNew.IntegerInputBox integerInputBox2;
    }
}
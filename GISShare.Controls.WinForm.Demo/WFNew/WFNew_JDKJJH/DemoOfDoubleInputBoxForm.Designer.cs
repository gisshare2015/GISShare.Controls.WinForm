namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfDoubleInputBoxForm
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
            this.doubleInputBox1 = new GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem();
            this.doubleInputBox2 = new GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // doubleInputBox1
            // 
            this.doubleInputBox1.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eNone;
            this.doubleInputBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.doubleInputBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doubleInputBox1.Location = new System.Drawing.Point(0, 0);
            this.doubleInputBox1.LockHeight = true;
            this.doubleInputBox1.Maximum = 1.7976931348623157E+308D;
            this.doubleInputBox1.Minimum = -1.7976931348623157E+308D;
            this.doubleInputBox1.Name = "integerInputBox1";
            this.doubleInputBox1.Size = new System.Drawing.Size(390, 21);
            this.doubleInputBox1.Step = 1D;
            this.doubleInputBox1.Tag = null;
            this.doubleInputBox1.Text = "integerInputBox1";
            this.doubleInputBox1.Value = 6.6D;
            // 
            // doubleInputBox2
            // 
            this.doubleInputBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.doubleInputBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doubleInputBox2.Location = new System.Drawing.Point(0, 0);
            this.doubleInputBox2.LockHeight = true;
            this.doubleInputBox2.Maximum = 1.7976931348623157E+308D;
            this.doubleInputBox2.Minimum = -1.7976931348623157E+308D;
            this.doubleInputBox2.Name = "integerInputBox2";
            this.doubleInputBox2.Size = new System.Drawing.Size(390, 27);
            this.doubleInputBox2.Step = 1D;
            this.doubleInputBox2.Tag = null;
            this.doubleInputBox2.Text = "integerInputBox2";
            this.doubleInputBox2.Value = 6.6D;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.doubleInputBox1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(390, 21);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.doubleInputBox2;
            this.baseItemHost2.Location = new System.Drawing.Point(18, 60);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(390, 27);
            this.baseItemHost2.TabIndex = 0;
            // 
            // DemoOfDoubleInputBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 136);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfDoubleInputBoxForm";
            this.Text = "DoubleInputBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem doubleInputBox1;
        private GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem doubleInputBox2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
    }
}
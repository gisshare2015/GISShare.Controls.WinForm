namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfButtonValueBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.buttonValueBoxItem1 = new GISShare.Controls.WinForm.WFNew.ButtonValueBoxItem();
            this.SuspendLayout();
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.buttonValueBoxItem1;
            this.baseItemHost1.Location = new System.Drawing.Point(12, 12);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost1.Size = new System.Drawing.Size(451, 24);
            this.baseItemHost1.TabIndex = 0;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // buttonValueBoxItem1
            // 
            this.buttonValueBoxItem1.CanEdit = false;
            this.buttonValueBoxItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.buttonValueBoxItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonValueBoxItem1.Location = new System.Drawing.Point(0, 0);
            this.buttonValueBoxItem1.LockHeight = true;
            this.buttonValueBoxItem1.Name = "buttonValueBoxItem1";
            this.buttonValueBoxItem1.PasswordChar = '\0';
            this.buttonValueBoxItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.buttonValueBoxItem1.Size = new System.Drawing.Size(451, 24);
            this.buttonValueBoxItem1.Tag = null;
            this.buttonValueBoxItem1.Text = null;
            this.buttonValueBoxItem1.ValueItem = null;
            this.buttonValueBoxItem1.ButtonClick += new System.EventHandler(this.buttonValueBoxItem1_ButtonClick);
            // 
            // DemoOfButtonValueBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 258);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfButtonValueBoxForm";
            this.Text = "ButtonTextBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.ButtonValueBoxItem buttonValueBoxItem1;

    }
}
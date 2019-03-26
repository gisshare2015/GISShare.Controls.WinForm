namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfComboDateForm
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
            this.comboDate1 = new GISShare.Controls.WinForm.WFNew.ComboDateItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // comboDate1
            // 
            this.comboDate1.DropDownHeight = -1;
            this.comboDate1.DropDownWidth = -1;
            this.comboDate1.Font = new System.Drawing.Font("宋体", 9F);
            this.comboDate1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboDate1.Location = new System.Drawing.Point(0, 0);
            this.comboDate1.LockHeight = true;
            this.comboDate1.Name = "comboDate1";
            this.comboDate1.SelectedDate = new System.DateTime(2013, 6, 11, 0, 0, 0, 0);
            this.comboDate1.ShowDropDownNum = 1;
            this.comboDate1.Size = new System.Drawing.Size(393, 27);
            this.comboDate1.Tag = null;
            this.comboDate1.Text = "2013/6/11";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.comboDate1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(393, 27);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfComboDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 266);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfComboDateForm";
            this.Text = "ComboDate控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ComboDateItem comboDate1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
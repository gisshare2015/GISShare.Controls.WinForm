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
            this.comboDate1 = new GISShare.Controls.WinForm.WFNew.ComboDate();
            this.SuspendLayout();
            // 
            // comboDate1
            // 
            this.comboDate1.DropDownHeight = 157;
            this.comboDate1.DropDownWidth = 180;
            this.comboDate1.Location = new System.Drawing.Point(12, 12);
            this.comboDate1.LockHeight = true;
            this.comboDate1.Name = "comboDate1";
            this.comboDate1.Padding = new System.Windows.Forms.Padding(0);
            this.comboDate1.SelectedDate = new System.DateTime(2013, 6, 11, 0, 0, 0, 0);
            this.comboDate1.Size = new System.Drawing.Size(262, 20);
            this.comboDate1.TabIndex = 0;
            this.comboDate1.Text = "2013/6/11";
            // 
            // DemoOfComboDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 159);
            this.Controls.Add(this.comboDate1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfComboDateForm";
            this.Text = "ComboDate控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ComboDate comboDate1;
    }
}
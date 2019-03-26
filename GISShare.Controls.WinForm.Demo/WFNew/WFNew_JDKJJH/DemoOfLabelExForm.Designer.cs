namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfLabelExForm
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
            this.labelNEx1 = new GISShare.Controls.WinForm.WFNew.LabelExItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // labelNEx1
            // 
            this.labelNEx1.Font = new System.Drawing.Font("宋体", 9F);
            this.labelNEx1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelNEx1.IsMultipleLine = true;
            this.labelNEx1.Location = new System.Drawing.Point(0, 0);
            this.labelNEx1.Name = "labelNEx1";
            this.labelNEx1.Size = new System.Drawing.Size(412, 107);
            this.labelNEx1.Tag = null;
            this.labelNEx1.Text = "labelNEx（控件）";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.labelNEx1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(412, 107);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfLabelNExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 109);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfLabelNExForm";
            this.Text = "LabelNEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.LabelExItem labelNEx1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
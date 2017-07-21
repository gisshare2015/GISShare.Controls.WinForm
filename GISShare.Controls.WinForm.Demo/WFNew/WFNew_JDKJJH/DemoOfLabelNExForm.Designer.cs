namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfLabelNExForm
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
            this.labelNEx1 = new GISShare.Controls.WinForm.WFNew.LabelNEx();
            this.SuspendLayout();
            // 
            // labelNEx1
            // 
            this.labelNEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNEx1.IsMultipleLine = true;
            this.labelNEx1.Location = new System.Drawing.Point(0, 0);
            this.labelNEx1.Name = "labelNEx1";
            this.labelNEx1.Padding = new System.Windows.Forms.Padding(0);
            this.labelNEx1.Size = new System.Drawing.Size(253, 54);
            this.labelNEx1.TabIndex = 0;
            this.labelNEx1.Text = "labelNEx（控件）";
            // 
            // DemoOfLabelNExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 54);
            this.Controls.Add(this.labelNEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfLabelNExForm";
            this.Text = "LabelNEx控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.LabelNEx labelNEx1;
    }
}
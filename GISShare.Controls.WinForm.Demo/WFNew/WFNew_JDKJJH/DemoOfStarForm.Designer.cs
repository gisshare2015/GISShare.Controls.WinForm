namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfStarForm
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
            this.ratingStar1 = new GISShare.Controls.WinForm.WFNew.RatingStar();
            this.ratingStar2 = new GISShare.Controls.WinForm.WFNew.RatingStar();
            this.ratingStar3 = new GISShare.Controls.WinForm.WFNew.RatingStar();
            this.SuspendLayout();
            // 
            // star1
            // 
            this.ratingStar1.Location = new System.Drawing.Point(12, 12);
            this.ratingStar1.Name = "star1";
            this.ratingStar1.Padding = new System.Windows.Forms.Padding(0);
            this.ratingStar1.Size = new System.Drawing.Size(90, 15);
            this.ratingStar1.TabIndex = 0;
            this.ratingStar1.Text = "star1";
            this.ratingStar1.Value = 2;
            // 
            // star2
            // 
            this.ratingStar2.Location = new System.Drawing.Point(12, 33);
            this.ratingStar2.Name = "star2";
            this.ratingStar2.Padding = new System.Windows.Forms.Padding(0);
            this.ratingStar2.Size = new System.Drawing.Size(126, 22);
            this.ratingStar2.StarSize = 21;
            this.ratingStar2.TabIndex = 1;
            this.ratingStar2.Text = "star2";
            this.ratingStar2.Value = 3;
            // 
            // star3
            // 
            this.ratingStar3.Enabled = false;
            this.ratingStar3.Location = new System.Drawing.Point(12, 78);
            this.ratingStar3.Name = "star3";
            this.ratingStar3.Padding = new System.Windows.Forms.Padding(0);
            this.ratingStar3.Size = new System.Drawing.Size(183, 15);
            this.ratingStar3.StarCount = 10;
            this.ratingStar3.TabIndex = 2;
            this.ratingStar3.Text = "star3";
            this.ratingStar3.Value = 10;
            // 
            // DemoOfStarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 106);
            this.Controls.Add(this.ratingStar3);
            this.Controls.Add(this.ratingStar2);
            this.Controls.Add(this.ratingStar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfRatingStarForm";
            this.Text = "RatingStar控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.RatingStar ratingStar1;
        private GISShare.Controls.WinForm.WFNew.RatingStar ratingStar2;
        private GISShare.Controls.WinForm.WFNew.RatingStar ratingStar3;
    }
}
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
            this.ratingStar1 = new GISShare.Controls.WinForm.WFNew.RatingStarItem();
            this.ratingStar2 = new GISShare.Controls.WinForm.WFNew.RatingStarItem();
            this.ratingStar3 = new GISShare.Controls.WinForm.WFNew.RatingStarItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // ratingStar1
            // 
            this.ratingStar1.Font = new System.Drawing.Font("宋体", 9F);
            this.ratingStar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ratingStar1.Location = new System.Drawing.Point(0, 0);
            this.ratingStar1.Name = "star1";
            this.ratingStar1.Size = new System.Drawing.Size(135, 22);
            this.ratingStar1.Tag = null;
            this.ratingStar1.Text = "star1";
            this.ratingStar1.Value = 2;
            // 
            // ratingStar2
            // 
            this.ratingStar2.Font = new System.Drawing.Font("宋体", 9F);
            this.ratingStar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ratingStar2.Location = new System.Drawing.Point(0, 0);
            this.ratingStar2.Name = "star2";
            this.ratingStar2.Size = new System.Drawing.Size(189, 33);
            this.ratingStar2.StarSize = 21;
            this.ratingStar2.Tag = null;
            this.ratingStar2.Text = "star2";
            this.ratingStar2.Value = 3;
            // 
            // ratingStar3
            // 
            this.ratingStar3.Enabled = false;
            this.ratingStar3.Font = new System.Drawing.Font("宋体", 9F);
            this.ratingStar3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ratingStar3.Location = new System.Drawing.Point(0, 0);
            this.ratingStar3.Name = "star3";
            this.ratingStar3.Size = new System.Drawing.Size(274, 22);
            this.ratingStar3.StarCount = 10;
            this.ratingStar3.Tag = null;
            this.ratingStar3.Text = "star3";
            this.ratingStar3.Value = 10;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.ratingStar1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(135, 22);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.ratingStar2;
            this.baseItemHost2.Location = new System.Drawing.Point(18, 50);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(189, 33);
            this.baseItemHost2.TabIndex = 1;
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost3.BaseItemObject = this.ratingStar3;
            this.baseItemHost3.Location = new System.Drawing.Point(18, 117);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(274, 22);
            this.baseItemHost3.TabIndex = 2;
            // 
            // DemoOfStarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 187);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfStarForm";
            this.Text = "RatingStar控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.RatingStarItem ratingStar1;
        private GISShare.Controls.WinForm.WFNew.RatingStarItem ratingStar2;
        private GISShare.Controls.WinForm.WFNew.RatingStarItem ratingStar3;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
    }
}
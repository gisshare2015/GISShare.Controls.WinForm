namespace GISShare.Controls.WinForm.Demo
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.zoomableImageBox1 = new GISShare.Controls.WinForm.WFNew.ImageZoomableBoxItem();
            this.buttonX1 = new GISShare.Controls.WinForm.ButtonX();
            this.comboTree1 = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.baseItemHost1 = new Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            //
            // baseItemHost1
            //
            this.baseItemHost1.BaseItemObject = this.zoomableImageBox1;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Size = new System.Drawing.Size(458, 464);
            this.baseItemHost1.TabIndex = 2;
            // 
            // zoomableImageBox1
            // 
            this.zoomableImageBox1.Image = ((System.Drawing.Image)(resources.GetObject("zoomableImageBox1.Image")));
            this.zoomableImageBox1.Location = new System.Drawing.Point(0, 0);
            this.zoomableImageBox1.Margin = new System.Windows.Forms.Padding(4);
            this.zoomableImageBox1.Name = "zoomableImageBox1";
            this.zoomableImageBox1.Padding = new System.Windows.Forms.Padding(0);
            this.zoomableImageBox1.ShowOutLine = true;
            this.zoomableImageBox1.Size = new System.Drawing.Size(458, 464);
            this.zoomableImageBox1.Text = "zoomableImageBox1";
            // 
            // buttonX1
            // 
            this.buttonX1.AutoPlanTextRectangle = false;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.Location = new System.Drawing.Point(466, 234);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(184, 80);
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "buttonX1";
            this.buttonX1.UseVisualStyleBackColor = false;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            //
            // baseItemHost2
            //
            this.baseItemHost2.BaseItemObject = this.comboTree1;
            this.baseItemHost2.Location = new System.Drawing.Point(468, 128);
            this.baseItemHost2.Size = new System.Drawing.Size(249, 27);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost2.TabIndex = 0;
            // 
            // comboTree1
            // 
            this.comboTree1.DropDownHeight = 28;
            this.comboTree1.DropDownWidth = 166;
            this.comboTree1.Location = new System.Drawing.Point(468, 128);
            this.comboTree1.LockHeight = true;
            this.comboTree1.Margin = new System.Windows.Forms.Padding(4);
            this.comboTree1.Name = "comboTree1";
            this.comboTree1.Padding = new System.Windows.Forms.Padding(0);
            this.comboTree1.SelectedNode = null;
            this.comboTree1.ShowDropDownNum = 0;
            this.comboTree1.Size = new System.Drawing.Size(249, 27);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 455);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.baseItemHost1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.ComboTreeItem comboTree1;
        private ButtonX buttonX1;
        private Controls.WinForm.WFNew.ImageZoomableBoxItem zoomableImageBox1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;

    }
}
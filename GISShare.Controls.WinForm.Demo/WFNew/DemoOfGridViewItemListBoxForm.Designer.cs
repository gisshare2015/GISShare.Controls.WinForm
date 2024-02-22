namespace GISShare.Controls.WinForm.Demo.WFNew
{
    partial class DemoOfGridViewItemListBoxForm
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
            this.gridViewItemListBoxItem1 = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem();
            this.SuspendLayout();
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.gridViewItemListBoxItem1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost1.Size = new System.Drawing.Size(919, 506);
            this.baseItemHost1.TabIndex = 0;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // gridViewItemListBoxItem1
            // 
            this.gridViewItemListBoxItem1.AutoGetFocus = true;
            this.gridViewItemListBoxItem1.BackgroundColor = System.Drawing.Color.Transparent;
            this.gridViewItemListBoxItem1.BackgroundImage = null;
            this.gridViewItemListBoxItem1.DataSource = null;
            this.gridViewItemListBoxItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.gridViewItemListBoxItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBoxItem1.Location = new System.Drawing.Point(0, 0);
            this.gridViewItemListBoxItem1.Name = "gridViewItemListBoxItem1";
            this.gridViewItemListBoxItem1.OutLineColor = System.Drawing.Color.Transparent;
            this.gridViewItemListBoxItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBoxItem1.ShowOutLine = true;
            this.gridViewItemListBoxItem1.Size = new System.Drawing.Size(919, 506);
            this.gridViewItemListBoxItem1.Tag = null;
            this.gridViewItemListBoxItem1.Text = "gridViewItemListBoxItem1";
            // 
            // DemoOfGridViewItemListBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 506);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfGridViewItemListBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GridViewItemListBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.View.GridViewItemListBoxItem gridViewItemListBoxItem1;
    }
}
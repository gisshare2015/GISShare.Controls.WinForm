namespace GISShare.Controls.WinForm.Demo.WFNew
{
    partial class DemoOfGridNodeViewItemTreeForm
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
            this.gridNodeViewItemTreeItem1 = new GISShare.Controls.WinForm.WFNew.View.GridNodeViewItemTreeItem();
            this.SuspendLayout();
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.gridNodeViewItemTreeItem1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost1.Size = new System.Drawing.Size(919, 502);
            this.baseItemHost1.TabIndex = 0;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // gridNodeViewItemTreeItem1
            // 
            this.gridNodeViewItemTreeItem1.AutoGetFocus = true;
            this.gridNodeViewItemTreeItem1.BackgroundColor = System.Drawing.Color.Transparent;
            this.gridNodeViewItemTreeItem1.BackgroundImage = null;
            this.gridNodeViewItemTreeItem1.DataSource = null;
            this.gridNodeViewItemTreeItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.gridNodeViewItemTreeItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridNodeViewItemTreeItem1.Location = new System.Drawing.Point(0, 0);
            this.gridNodeViewItemTreeItem1.Name = "gridNodeViewItemTreeItem1";
            this.gridNodeViewItemTreeItem1.OutLineColor = System.Drawing.Color.Transparent;
            this.gridNodeViewItemTreeItem1.SelectedNode = null;
            this.gridNodeViewItemTreeItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.gridNodeViewItemTreeItem1.ShowOutLine = true;
            this.gridNodeViewItemTreeItem1.Size = new System.Drawing.Size(919, 502);
            this.gridNodeViewItemTreeItem1.Tag = null;
            this.gridNodeViewItemTreeItem1.Text = "gridNodeViewItemTreeItem1";
            // 
            // DemoOfGridNodeViewItemTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 502);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfGridNodeViewItemTreeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GridNodeViewItemTree控件";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.View.GridNodeViewItemTreeItem gridNodeViewItemTreeItem1;
    }
}
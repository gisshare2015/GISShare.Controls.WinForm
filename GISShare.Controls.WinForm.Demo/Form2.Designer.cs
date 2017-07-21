namespace GISShare.Controls.WinForm.Demo
{
    partial class Form2
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.viewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBox();
            this.comboBoxN1 = new GISShare.Controls.WinForm.WFNew.ComboBoxN();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree();
            this.gridViewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBox();
            this.gridNodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.GridNodeViewItemTree();
            this.comboTree1 = new GISShare.Controls.WinForm.WFNew.ComboTree();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(997, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(560, 319);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(183, 52);
            this.checkedListBox1.TabIndex = 6;
            // 
            // viewItemListBox1
            // 
            this.viewItemListBox1.AutoGetFocus = true;
            this.viewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.viewItemListBox1.Location = new System.Drawing.Point(382, 319);
            this.viewItemListBox1.Name = "viewItemListBox1";
            this.viewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.viewItemListBox1.Size = new System.Drawing.Size(172, 56);
            this.viewItemListBox1.TabIndex = 5;
            this.viewItemListBox1.Text = "viewItemListBox1";
            // 
            // comboBoxN1
            // 
            this.comboBoxN1.AutoClosePopup = false;
            this.comboBoxN1.CheckedDropDownList = true;
            this.comboBoxN1.DropDownHeight = 28;
            this.comboBoxN1.DropDownWidth = 364;
            this.comboBoxN1.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.comboBoxN1.Location = new System.Drawing.Point(12, 319);
            this.comboBoxN1.LockHeight = true;
            this.comboBoxN1.Name = "comboBoxN1";
            this.comboBoxN1.Padding = new System.Windows.Forms.Padding(0);
            this.comboBoxN1.Size = new System.Drawing.Size(364, 20);
            this.comboBoxN1.TabIndex = 4;
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.AutoGetFocus = true;
            this.nodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(759, 16);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(162, 296);
            this.nodeViewItemTree1.TabIndex = 2;
            this.nodeViewItemTree1.Text = "nodeViewItemTree1";
            // 
            // gridViewItemListBox1
            // 
            this.gridViewItemListBox1.AutoGetFocus = true;
            this.gridViewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.gridViewItemListBox1.DataSource = null;
            this.gridViewItemListBox1.Location = new System.Drawing.Point(382, 12);
            this.gridViewItemListBox1.Name = "gridViewItemListBox1";
            this.gridViewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.gridViewItemListBox1.Size = new System.Drawing.Size(361, 301);
            this.gridViewItemListBox1.TabIndex = 1;
            this.gridViewItemListBox1.Text = "gridViewItemListBox1";
            // 
            // gridNodeViewItemTree1
            // 
            this.gridNodeViewItemTree1.AutoGetFocus = true;
            this.gridNodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.gridNodeViewItemTree1.DataSource = null;
            this.gridNodeViewItemTree1.Location = new System.Drawing.Point(12, 12);
            this.gridNodeViewItemTree1.Name = "gridNodeViewItemTree1";
            this.gridNodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.gridNodeViewItemTree1.SelectedNode = null;
            this.gridNodeViewItemTree1.Size = new System.Drawing.Size(364, 301);
            this.gridNodeViewItemTree1.TabIndex = 0;
            this.gridNodeViewItemTree1.Text = "gridNodeViewItemTree1";
            // 
            // comboTree1
            // 
            this.comboTree1.AutoClosePopup = false;
            this.comboTree1.CheckedDropDownList = true;
            this.comboTree1.DropDownHeight = 20;
            this.comboTree1.DropDownWidth = 2;
            this.comboTree1.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.comboTree1.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.comboTree1.Location = new System.Drawing.Point(12, 345);
            this.comboTree1.LockHeight = true;
            this.comboTree1.Name = "comboTree1";
            this.comboTree1.Padding = new System.Windows.Forms.Padding(0);
            this.comboTree1.SelectedNode = null;
            this.comboTree1.Size = new System.Drawing.Size(364, 20);
            this.comboTree1.TabIndex = 7;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 404);
            this.Controls.Add(this.comboTree1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.viewItemListBox1);
            this.Controls.Add(this.comboBoxN1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.nodeViewItemTree1);
            this.Controls.Add(this.gridViewItemListBox1);
            this.Controls.Add(this.gridNodeViewItemTree1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.WinForm.WFNew.View.GridNodeViewItemTree gridNodeViewItemTree1;
        private Controls.WinForm.WFNew.View.GridViewItemListBox gridViewItemListBox1;
        private Controls.WinForm.WFNew.View.NodeViewItemTree nodeViewItemTree1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.WinForm.WFNew.ComboBoxN comboBoxN1;
        private Controls.WinForm.WFNew.View.ViewItemListBox viewItemListBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private Controls.WinForm.WFNew.ComboTree comboTree1;

    }
}
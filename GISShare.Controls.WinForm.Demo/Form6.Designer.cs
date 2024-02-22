namespace GISShare.Controls.WinForm.Demo
{
    partial class Form6
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
            GISShare.Controls.WinForm.WFNew.View.ColumnViewItem columnViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem();
            GISShare.Controls.WinForm.WFNew.View.ColumnViewItem columnViewItem2 = new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.baseItemHost5 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.gridViewItemListBoxItem1 = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.playProcessBarItem1 = new GISShare.Controls.WinForm.WFNew.PlayProcessBarItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.panelControl1 = new GISShare.Controls.WinForm.WFNew.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.labelExItem1 = new GISShare.Controls.WinForm.WFNew.LabelExItem();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 63);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(597, 368);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 25);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "1111111";
            // 
            // baseItemHost5
            // 
            this.baseItemHost5.BaseItemObject = this.gridViewItemListBoxItem1;
            this.baseItemHost5.Location = new System.Drawing.Point(597, 26);
            this.baseItemHost5.Name = "baseItemHost5";
            this.baseItemHost5.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost5.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost5.Size = new System.Drawing.Size(422, 280);
            this.baseItemHost5.TabIndex = 8;
            this.baseItemHost5.Text = "baseItemHost5";
            // 
            // gridViewItemListBoxItem1
            // 
            this.gridViewItemListBoxItem1.AutoGetFocus = true;
            this.gridViewItemListBoxItem1.BackgroundColor = System.Drawing.Color.Transparent;
            this.gridViewItemListBoxItem1.BackgroundImage = null;
            this.gridViewItemListBoxItem1.CanSelect = true;
            columnViewItem1.BaseItemObject = null;
            columnViewItem1.FieldName = null;
            columnViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            columnViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            columnViewItem1.Name = null;
            columnViewItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            columnViewItem1.Text = "ViewItem";
            columnViewItem1.Width = 100;
            columnViewItem2.BaseItemObject = null;
            columnViewItem2.FieldName = null;
            columnViewItem2.Font = new System.Drawing.Font("宋体", 9F);
            columnViewItem2.ForeColor = System.Drawing.SystemColors.ControlText;
            columnViewItem2.Name = null;
            columnViewItem2.ShadowColor = System.Drawing.SystemColors.ControlText;
            columnViewItem2.Text = "ViewItem";
            columnViewItem2.Width = 100;
            this.gridViewItemListBoxItem1.ColumnViewItems.Add(columnViewItem1);
            this.gridViewItemListBoxItem1.ColumnViewItems.Add(columnViewItem2);
            this.gridViewItemListBoxItem1.DataSource = null;
            this.gridViewItemListBoxItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.gridViewItemListBoxItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBoxItem1.Location = new System.Drawing.Point(0, 0);
            this.gridViewItemListBoxItem1.Name = "gridViewItemListBoxItem1";
            this.gridViewItemListBoxItem1.OutLineColor = System.Drawing.Color.Transparent;
            this.gridViewItemListBoxItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBoxItem1.ShowOutLine = true;
            this.gridViewItemListBoxItem1.Size = new System.Drawing.Size(422, 280);
            this.gridViewItemListBoxItem1.Tag = null;
            this.gridViewItemListBoxItem1.Text = "gridViewItemListBoxItem1";
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.playProcessBarItem1;
            this.baseItemHost2.Location = new System.Drawing.Point(52, 406);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost2.Size = new System.Drawing.Size(485, 21);
            this.baseItemHost2.TabIndex = 4;
            this.baseItemHost2.Text = "baseItemHost2";
            // 
            // playProcessBarItem1
            // 
            this.playProcessBarItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.playProcessBarItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.playProcessBarItem1.Location = new System.Drawing.Point(0, 0);
            this.playProcessBarItem1.LockHeight = true;
            this.playProcessBarItem1.Name = "playProcessBarItem1";
            this.playProcessBarItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.playProcessBarItem1.Size = new System.Drawing.Size(485, 21);
            this.playProcessBarItem1.Tag = null;
            this.playProcessBarItem1.Text = "playProcessBarItem1";
            this.playProcessBarItem1.Value = 50;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = null;
            this.baseItemHost1.Location = new System.Drawing.Point(518, 56);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost1.Size = new System.Drawing.Size(47, 51);
            this.baseItemHost1.TabIndex = 2;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoScroll = true;
            this.panelControl1.BackgroundColor = System.Drawing.Color.Transparent;
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Location = new System.Drawing.Point(52, 72);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.OutLineColor = System.Drawing.Color.Transparent;
            this.panelControl1.Padding = new System.Windows.Forms.Padding(0);
            this.panelControl1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.panelControl1.Size = new System.Drawing.Size(424, 317);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Text = "panelControl1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(232, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 137);
            this.panel1.TabIndex = 1;
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BaseItemObject = this.labelExItem1;
            this.baseItemHost3.Location = new System.Drawing.Point(855, 364);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost3.Size = new System.Drawing.Size(220, 78);
            this.baseItemHost3.TabIndex = 9;
            this.baseItemHost3.Text = "baseItemHost3";
            // 
            // labelExItem1
            // 
            this.labelExItem1.BackgroundColor = System.Drawing.Color.Transparent;
            this.labelExItem1.BackgroundImage = null;
            this.labelExItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.labelExItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelExItem1.IsMultipleLine = true;
            this.labelExItem1.Location = new System.Drawing.Point(0, 0);
            this.labelExItem1.Name = "labelExItem1";
            this.labelExItem1.OutLineColor = System.Drawing.Color.Transparent;
            this.labelExItem1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.labelExItem1.Size = new System.Drawing.Size(220, 78);
            this.labelExItem1.Tag = null;
            this.labelExItem1.Text = "labelExItem1234567890";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 501);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.WinForm.WFNew.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private Controls.WinForm.WFNew.PlayProcessBarItem playProcessBarItem1;
        private System.Windows.Forms.TextBox textBox1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost5;
        private Controls.WinForm.WFNew.View.GridViewItemListBoxItem gridViewItemListBoxItem1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
        private Controls.WinForm.WFNew.LabelExItem labelExItem1;
    }
}
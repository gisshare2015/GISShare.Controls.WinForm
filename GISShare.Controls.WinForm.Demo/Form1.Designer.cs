namespace GISShare.Controls.WinForm.Demo
{
    partial class Form1
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
            this.gridViewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.viewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxN1 = new GISShare.Controls.WinForm.WFNew.TextBoxItem();
            this.buttonN1 = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost4 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewItemListBox1
            // 
            this.gridViewItemListBox1.AutoGetFocus = true;
            this.gridViewItemListBox1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridViewItemListBox1.BackgroundImage = null;
            this.gridViewItemListBox1.DataSource = null;
            this.gridViewItemListBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.gridViewItemListBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBox1.Location = new System.Drawing.Point(0, 0);
            this.gridViewItemListBox1.MultipleSelect = true;
            this.gridViewItemListBox1.Name = "gridViewItemListBox1";
            this.gridViewItemListBox1.OutLineColor = System.Drawing.Color.Transparent;
            this.gridViewItemListBox1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.gridViewItemListBox1.ShowOutLine = true;
            this.gridViewItemListBox1.Size = new System.Drawing.Size(859, 454);
            this.gridViewItemListBox1.Tag = null;
            this.gridViewItemListBox1.Text = "gridViewItemListBox1";
            this.gridViewItemListBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewItemListBox1_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(867, 433);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(872, 362);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 50);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // viewItemListBox1
            // 
            this.viewItemListBox1.AutoGetFocus = true;
            this.viewItemListBox1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.viewItemListBox1.BackgroundImage = null;
            this.viewItemListBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.viewItemListBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.viewItemListBox1.Location = new System.Drawing.Point(0, 0);
            this.viewItemListBox1.Name = "viewItemListBox1";
            this.viewItemListBox1.OutLineColor = System.Drawing.Color.Transparent;
            this.viewItemListBox1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.viewItemListBox1.ShowHScrollBar = true;
            this.viewItemListBox1.ShowOutLine = true;
            this.viewItemListBox1.Size = new System.Drawing.Size(329, 205);
            this.viewItemListBox1.Tag = null;
            this.viewItemListBox1.Text = "viewItemListBox1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(867, 212);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(329, 117);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // textBoxN1
            // 
            this.textBoxN1.Font = new System.Drawing.Font("宋体", 9F);
            this.textBoxN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxN1.Location = new System.Drawing.Point(0, 0);
            this.textBoxN1.LockHeight = true;
            this.textBoxN1.Name = "textBoxN1";
            this.textBoxN1.PasswordChar = '\0';
            this.textBoxN1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.textBoxN1.Size = new System.Drawing.Size(195, 24);
            this.textBoxN1.Tag = null;
            this.textBoxN1.Text = "textBoxN1";
            this.textBoxN1.TextChanged += new System.EventHandler(this.textBoxN1_TextChanged);
            this.textBoxN1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxN1_KeyDown);
            // 
            // buttonN1
            // 
            this.buttonN1.BackgroundColor = System.Drawing.Color.Transparent;
            this.buttonN1.BackgroundImage = null;
            this.buttonN1.Font = new System.Drawing.Font("宋体", 9F);
            this.buttonN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonN1.Image = null;
            this.buttonN1.Location = new System.Drawing.Point(0, 0);
            this.buttonN1.Name = "buttonN1";
            this.buttonN1.OutLineColor = System.Drawing.Color.Transparent;
            this.buttonN1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.buttonN1.ShowNomalState = true;
            this.buttonN1.Size = new System.Drawing.Size(77, 40);
            this.buttonN1.Tag = null;
            this.buttonN1.Text = "buttonN1";
            this.buttonN1.Click += new System.EventHandler(this.buttonN1_Click);
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.SystemColors.Window;
            this.baseItemHost1.BaseItemObject = this.gridViewItemListBox1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Left;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost1.Size = new System.Drawing.Size(859, 454);
            this.baseItemHost1.TabIndex = 2;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.viewItemListBox1;
            this.baseItemHost2.Location = new System.Drawing.Point(867, 0);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost2.Size = new System.Drawing.Size(329, 205);
            this.baseItemHost2.TabIndex = 5;
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BaseItemObject = this.textBoxN1;
            this.baseItemHost3.Location = new System.Drawing.Point(973, 377);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost3.Size = new System.Drawing.Size(195, 24);
            this.baseItemHost3.TabIndex = 7;
            // 
            // baseItemHost4
            // 
            this.baseItemHost4.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost4.BaseItemObject = this.buttonN1;
            this.baseItemHost4.Location = new System.Drawing.Point(1001, 422);
            this.baseItemHost4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.baseItemHost4.Name = "baseItemHost4";
            this.baseItemHost4.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost4.ShadowColor = System.Drawing.SystemColors.ControlText;
            this.baseItemHost4.Size = new System.Drawing.Size(77, 40);
            this.baseItemHost4.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelItemsEventNC = false;
            this.ClientSize = new System.Drawing.Size(1200, 454);
            this.Controls.Add(this.baseItemHost1);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.baseItemHost4);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Controls.WinForm.WFNew.View.GridViewItemListBoxItem gridViewItemListBox1;
        private Controls.WinForm.WFNew.View.ViewItemListBoxItem viewItemListBox1;
        private Controls.WinForm.WFNew.TextBoxItem textBoxN1;
        private Controls.WinForm.WFNew.ButtonItem buttonN1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost4;

    }
}
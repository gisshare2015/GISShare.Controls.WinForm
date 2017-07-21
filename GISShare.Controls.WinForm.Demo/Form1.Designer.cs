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
            this.gridViewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.viewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxN1 = new GISShare.Controls.WinForm.WFNew.TextBoxN();
            this.buttonN1 = new GISShare.Controls.WinForm.WFNew.ButtonN();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewItemListBox1
            // 
            this.gridViewItemListBox1.AutoGetFocus = true;
            this.gridViewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.gridViewItemListBox1.DataSource = null;
            this.gridViewItemListBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridViewItemListBox1.Location = new System.Drawing.Point(0, 0);
            this.gridViewItemListBox1.MultipleSelect = true;
            this.gridViewItemListBox1.Name = "gridViewItemListBox1";
            this.gridViewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.gridViewItemListBox1.Size = new System.Drawing.Size(644, 391);
            this.gridViewItemListBox1.TabIndex = 2;
            this.gridViewItemListBox1.Text = "gridViewItemListBox1";
            this.gridViewItemListBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewItemListBox1_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(650, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(654, 289);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // viewItemListBox1
            // 
            this.viewItemListBox1.AutoGetFocus = true;
            this.viewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.viewItemListBox1.Location = new System.Drawing.Point(650, 0);
            this.viewItemListBox1.Name = "viewItemListBox1";
            this.viewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.viewItemListBox1.ShowHScrollBar = true;
            this.viewItemListBox1.Size = new System.Drawing.Size(247, 164);
            this.viewItemListBox1.TabIndex = 5;
            this.viewItemListBox1.Text = "viewItemListBox1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(650, 170);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(247, 93);
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
            this.textBoxN1.Location = new System.Drawing.Point(730, 301);
            this.textBoxN1.LockHeight = true;
            this.textBoxN1.Name = "textBoxN1";
            this.textBoxN1.Padding = new System.Windows.Forms.Padding(0);
            this.textBoxN1.PasswordChar = '\0';
            this.textBoxN1.Size = new System.Drawing.Size(146, 20);
            this.textBoxN1.TabIndex = 7;
            this.textBoxN1.Text = "textBoxN1";
            this.textBoxN1.TextChanged += new System.EventHandler(this.textBoxN1_TextChanged);
            this.textBoxN1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxN1_KeyDown);
            // 
            // buttonN1
            // 
            this.buttonN1.BackColor = System.Drawing.Color.Transparent;
            this.buttonN1.Image = null;
            this.buttonN1.Location = new System.Drawing.Point(751, 337);
            this.buttonN1.Name = "buttonN1";
            this.buttonN1.Padding = new System.Windows.Forms.Padding(0);
            this.buttonN1.Size = new System.Drawing.Size(58, 32);
            this.buttonN1.TabIndex = 8;
            this.buttonN1.Text = "buttonN1";
            this.buttonN1.Click += new System.EventHandler(this.buttonN1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 391);
            this.Controls.Add(this.buttonN1);
            this.Controls.Add(this.textBoxN1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.viewItemListBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridViewItemListBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.View.GridViewItemListBox gridViewItemListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Controls.WinForm.WFNew.View.ViewItemListBox viewItemListBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Controls.WinForm.WFNew.TextBoxN textBoxN1;
        private Controls.WinForm.WFNew.ButtonN buttonN1;

    }
}
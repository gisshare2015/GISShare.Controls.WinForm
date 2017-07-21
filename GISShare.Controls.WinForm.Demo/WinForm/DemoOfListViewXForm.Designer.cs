namespace GISShare.Controls.WinForm.Demo.WinForm
{
    partial class DemoOfListViewXForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("矢量数据", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("栅格数据", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("文本数据", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "点数据",
            "Point"}, 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "线数据",
            "Polyline"}, 2);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "面数据",
            "Polygon"}, 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "表数据",
            "Table"}, 5);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "文本数据",
            "Text"}, 6);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "栅格数据",
            "Raster"}, 4);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfListViewXForm));
            this.listViewX1 = new GISShare.Controls.WinForm.ListViewX();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cbView = new System.Windows.Forms.ComboBox();
            this.chShowCheckBox = new GISShare.Controls.WinForm.CheckBoxX();
            this.SuspendLayout();
            // 
            // listViewX1
            // 
            this.listViewX1.AutoMouseMoveSeleced = true;
            this.listViewX1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            listViewGroup1.Header = "矢量数据";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "栅格数据";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "文本数据";
            listViewGroup3.Name = "listViewGroup3";
            this.listViewX1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.ToolTipText = "点数据";
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 0;
            listViewItem2.ToolTipText = "线数据";
            listViewItem3.Group = listViewGroup1;
            listViewItem3.StateImageIndex = 0;
            listViewItem3.ToolTipText = "面数据";
            listViewItem4.Group = listViewGroup3;
            listViewItem4.StateImageIndex = 0;
            listViewItem4.ToolTipText = "表数据";
            listViewItem5.Group = listViewGroup3;
            listViewItem5.StateImageIndex = 0;
            listViewItem5.ToolTipText = "文本数据";
            listViewItem6.Group = listViewGroup2;
            listViewItem6.StateImageIndex = 0;
            listViewItem6.ToolTipText = "栅格数据";
            this.listViewX1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listViewX1.LargeImageList = this.imageList2;
            this.listViewX1.Location = new System.Drawing.Point(12, 32);
            this.listViewX1.Name = "listViewX1";
            this.listViewX1.OwnerDraw = true;
            this.listViewX1.Size = new System.Drawing.Size(389, 309);
            this.listViewX1.SmallImageList = this.imageList1;
            this.listViewX1.TabIndex = 0;
            this.listViewX1.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "数据类型";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 113;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "POINT32.ico");
            this.imageList2.Images.SetKeyName(1, "POLYGON32.ico");
            this.imageList2.Images.SetKeyName(2, "POLYLINE32.ico");
            this.imageList2.Images.SetKeyName(3, "ANNOTATION32.ico");
            this.imageList2.Images.SetKeyName(4, "RASTERBAND32.ico");
            this.imageList2.Images.SetKeyName(5, "TABLE32.ico");
            this.imageList2.Images.SetKeyName(6, "TXT32.ico");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "POINT16.ico");
            this.imageList1.Images.SetKeyName(1, "POLYGON16.ico");
            this.imageList1.Images.SetKeyName(2, "POLYLINE16.ico");
            this.imageList1.Images.SetKeyName(3, "ANNOTATION16.ico");
            this.imageList1.Images.SetKeyName(4, "RASTERBAND16.ico");
            this.imageList1.Images.SetKeyName(5, "TABLE16.ico");
            this.imageList1.Images.SetKeyName(6, "TXT16.ico");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "视图类型（View）：";
            // 
            // cbView
            // 
            this.cbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbView.FormattingEnabled = true;
            this.cbView.Items.AddRange(new object[] {
            "LargeIcon",
            "Details",
            "SmallIcon",
            "List",
            "Tile"});
            this.cbView.Location = new System.Drawing.Point(122, 6);
            this.cbView.Name = "cbView";
            this.cbView.Size = new System.Drawing.Size(189, 20);
            this.cbView.TabIndex = 2;
            this.cbView.SelectedIndexChanged += new System.EventHandler(this.cbView_SelectedIndexChanged);
            // 
            // chShowCheckBox
            // 
            this.chShowCheckBox.AutoSize = true;
            this.chShowCheckBox.Location = new System.Drawing.Point(317, 9);
            this.chShowCheckBox.Name = "chShowCheckBox";
            this.chShowCheckBox.Size = new System.Drawing.Size(84, 16);
            this.chShowCheckBox.TabIndex = 3;
            this.chShowCheckBox.Text = "显示复选框";
            this.chShowCheckBox.UseVisualStyleBackColor = true;
            this.chShowCheckBox.VOffset = -1;
            this.chShowCheckBox.CheckedChanged += new System.EventHandler(this.chShowCheckBox_CheckedChanged);
            // 
            // DemoOfListViewXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 353);
            this.Controls.Add(this.chShowCheckBox);
            this.Controls.Add(this.cbView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfListViewXForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListViewX控件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GISShare.Controls.WinForm.ListViewX listViewX1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbView;
        private GISShare.Controls.WinForm.CheckBoxX chShowCheckBox;
    }
}
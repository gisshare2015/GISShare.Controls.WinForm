namespace GISShare.Controls.WinForm.Demo.WinForm
{
    partial class DemoOfDockBarManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfDockBarManagerForm));
            this.dockBarManager1 = new GISShare.Controls.WinForm.DockBar.DockBarManager();
            this.contextMenu1 = new GISShare.Controls.WinForm.DockBar.ContextMenu();
            this.miOpen2 = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miAbout2 = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miExist2 = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.dockBarDockAreaBottom1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaBottom();
            this.statusBar1 = new GISShare.Controls.WinForm.DockBar.StatusBar();
            this.lbliNum = new GISShare.Controls.WinForm.DockBar.LabelItem();
            this.dockBarDockAreaLeft1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaLeft();
            this.dockBarDockAreaRight1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaRight();
            this.dockBarDockAreaTop1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaTop();
            this.menuBar1 = new GISShare.Controls.WinForm.DockBar.MenuBar();
            this.menuItem1 = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miOpen = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miSave = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.separatorItem1 = new GISShare.Controls.WinForm.DockBar.SeparatorItem();
            this.miExist = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.menuItem5 = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miHelp = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.miAbout = new GISShare.Controls.WinForm.DockBar.MenuItem();
            this.toolBar1 = new GISShare.Controls.WinForm.DockBar.ToolBar();
            this.btniOpen = new GISShare.Controls.WinForm.DockBar.ButtonItem();
            this.btniAbout = new GISShare.Controls.WinForm.DockBar.ButtonItem();
            this.separatorItem2 = new GISShare.Controls.WinForm.DockBar.SeparatorItem();
            this.btniExist = new GISShare.Controls.WinForm.DockBar.ButtonItem();
            this.toolBar2 = new GISShare.Controls.WinForm.DockBar.ToolBar();
            this.radioButtonItem1 = new GISShare.Controls.WinForm.DockBar.RadioButtonItem();
            this.checkBoxItem1 = new GISShare.Controls.WinForm.DockBar.CheckBoxItem();
            this.separatorItem3 = new GISShare.Controls.WinForm.DockBar.SeparatorItem();
            this.numericUpDownItem1 = new GISShare.Controls.WinForm.DockBar.NumericUpDownItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenu1.SuspendLayout();
            this.dockBarDockAreaBottom1.SuspendLayout();
            this.statusBar1.SuspendLayout();
            this.dockBarDockAreaTop1.SuspendLayout();
            this.menuBar1.SuspendLayout();
            this.toolBar1.SuspendLayout();
            this.toolBar2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockBarManager1
            // 
            this.dockBarManager1.ContextMenus.Add(this.contextMenu1);
            this.dockBarManager1.DockBarDockAreaBottom = this.dockBarDockAreaBottom1;
            this.dockBarManager1.DockBarDockAreaLeft = this.dockBarDockAreaLeft1;
            this.dockBarManager1.DockBarDockAreaRight = this.dockBarDockAreaRight1;
            this.dockBarManager1.DockBarDockAreaTop = this.dockBarDockAreaTop1;
            this.dockBarManager1.MenuBar = this.menuBar1;
            this.dockBarManager1.ParentForm = this;
            this.dockBarManager1.ShowLargeImage = false;
            this.dockBarManager1.StatusBar = this.statusBar1;
            this.dockBarManager1.ToolBars.Add(this.toolBar1);
            this.dockBarManager1.ToolBars.Add(this.toolBar2);
            // 
            // contextMenu1
            // 
            this.contextMenu1.Items.Add(this.miOpen2);
            this.contextMenu1.Items.Add(this.miAbout2);
            this.contextMenu1.Items.Add(this.miExist2);
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(101, 70);
            this.contextMenu1.Text = "快捷菜单";
            // 
            // miOpen2
            // 
            this.miOpen2.Image = ((System.Drawing.Image)(resources.GetObject("miOpen2.Image")));
            this.miOpen2.Name = "miOpen2";
            this.miOpen2.Size = new System.Drawing.Size(100, 22);
            this.miOpen2.Text = "打开";
            this.miOpen2.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miAbout2
            // 
            this.miAbout2.Image = ((System.Drawing.Image)(resources.GetObject("miAbout2.Image")));
            this.miAbout2.Name = "miAbout2";
            this.miAbout2.Size = new System.Drawing.Size(100, 22);
            this.miAbout2.Text = "关于";
            this.miAbout2.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miExist2
            // 
            this.miExist2.Image = ((System.Drawing.Image)(resources.GetObject("miExist2.Image")));
            this.miExist2.Name = "miExist2";
            this.miExist2.Size = new System.Drawing.Size(100, 22);
            this.miExist2.Text = "退出";
            this.miExist2.Click += new System.EventHandler(this.miExist_Click);
            // 
            // dockBarDockAreaBottom1
            // 
            this.dockBarDockAreaBottom1.Controls.Add(this.statusBar1);
            this.dockBarDockAreaBottom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBarDockAreaBottom1.Location = new System.Drawing.Point(0, 370);
            this.dockBarDockAreaBottom1.Name = "dockBarDockAreaBottom1";
            this.dockBarDockAreaBottom1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.dockBarDockAreaBottom1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dockBarDockAreaBottom1.Size = new System.Drawing.Size(615, 23);
            // 
            // statusBar1
            // 
            this.statusBar1.Image = null;
            this.statusBar1.Items.Add(this.lbliNum);
            this.statusBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusBar1.Location = new System.Drawing.Point(0, 0);
            this.statusBar1.MinimumSize = new System.Drawing.Size(23, 23);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(615, 23);
            this.statusBar1.TabIndex = 0;
            this.statusBar1.Text = "statusBar1";
            // 
            // lbliNum
            // 
            this.lbliNum.Name = "lbliNum";
            this.lbliNum.Size = new System.Drawing.Size(75, 21);
            this.lbliNum.Text = "当前行数：0";
            // 
            // dockBarDockAreaLeft1
            // 
            this.dockBarDockAreaLeft1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockBarDockAreaLeft1.Location = new System.Drawing.Point(0, 50);
            this.dockBarDockAreaLeft1.Name = "dockBarDockAreaLeft1";
            this.dockBarDockAreaLeft1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dockBarDockAreaLeft1.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dockBarDockAreaLeft1.Size = new System.Drawing.Size(0, 320);
            // 
            // dockBarDockAreaRight1
            // 
            this.dockBarDockAreaRight1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockBarDockAreaRight1.Location = new System.Drawing.Point(615, 50);
            this.dockBarDockAreaRight1.Name = "dockBarDockAreaRight1";
            this.dockBarDockAreaRight1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dockBarDockAreaRight1.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dockBarDockAreaRight1.Size = new System.Drawing.Size(0, 320);
            // 
            // dockBarDockAreaTop1
            // 
            this.dockBarDockAreaTop1.Controls.Add(this.menuBar1);
            this.dockBarDockAreaTop1.Controls.Add(this.toolBar1);
            this.dockBarDockAreaTop1.Controls.Add(this.toolBar2);
            this.dockBarDockAreaTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockBarDockAreaTop1.Location = new System.Drawing.Point(0, 0);
            this.dockBarDockAreaTop1.Name = "dockBarDockAreaTop1";
            this.dockBarDockAreaTop1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.dockBarDockAreaTop1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dockBarDockAreaTop1.Size = new System.Drawing.Size(615, 50);
            // 
            // menuBar1
            // 
            this.menuBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuBar1.Image = ((System.Drawing.Image)(resources.GetObject("menuBar1.Image")));
            this.menuBar1.Items.Add(this.menuItem1);
            this.menuBar1.Items.Add(this.menuItem5);
            this.menuBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuBar1.Location = new System.Drawing.Point(0, 0);
            this.menuBar1.MinimumSize = new System.Drawing.Size(23, 23);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.Size = new System.Drawing.Size(615, 25);
            this.menuBar1.TabIndex = 1;
            this.menuBar1.Text = "主菜单";
            // 
            // menuItem1
            // 
            this.menuItem1.DropDownItems.Add(this.miOpen);
            this.menuItem1.DropDownItems.Add(this.miSave);
            this.menuItem1.DropDownItems.Add(this.separatorItem1);
            this.menuItem1.DropDownItems.Add(this.miExist);
            this.menuItem1.Items.Add(this.miOpen);
            this.menuItem1.Items.Add(this.miSave);
            this.menuItem1.Items.Add(this.separatorItem1);
            this.menuItem1.Items.Add(this.miExist);
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Size = new System.Drawing.Size(44, 21);
            this.menuItem1.Text = "文件";
            // 
            // miOpen
            // 
            this.miOpen.Image = ((System.Drawing.Image)(resources.GetObject("miOpen.Image")));
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(152, 22);
            this.miOpen.Text = "打开";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Image = ((System.Drawing.Image)(resources.GetObject("miSave.Image")));
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(152, 22);
            this.miSave.Text = "保存";
            // 
            // separatorItem1
            // 
            this.separatorItem1.Name = "separatorItem1";
            this.separatorItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // miExist
            // 
            this.miExist.Image = ((System.Drawing.Image)(resources.GetObject("miExist.Image")));
            this.miExist.Name = "miExist";
            this.miExist.Size = new System.Drawing.Size(152, 22);
            this.miExist.Text = "退出";
            this.miExist.Click += new System.EventHandler(this.miExist_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.DropDownItems.Add(this.miHelp);
            this.menuItem5.DropDownItems.Add(this.miAbout);
            this.menuItem5.Items.Add(this.miHelp);
            this.menuItem5.Items.Add(this.miAbout);
            this.menuItem5.Name = "menuItem5";
            this.menuItem5.Size = new System.Drawing.Size(44, 21);
            this.menuItem5.Text = "帮助";
            // 
            // miHelp
            // 
            this.miHelp.Image = ((System.Drawing.Image)(resources.GetObject("miHelp.Image")));
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(152, 22);
            this.miHelp.Text = "帮助";
            // 
            // miAbout
            // 
            this.miAbout.Image = ((System.Drawing.Image)(resources.GetObject("miAbout.Image")));
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(152, 22);
            this.miAbout.Text = "关于";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Image = null;
            this.toolBar1.Items.Add(this.btniOpen);
            this.toolBar1.Items.Add(this.btniAbout);
            this.toolBar1.Items.Add(this.separatorItem2);
            this.toolBar1.Items.Add(this.btniExist);
            this.toolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolBar1.Location = new System.Drawing.Point(3, 25);
            this.toolBar1.MinimumSize = new System.Drawing.Size(23, 23);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(188, 25);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Text = "常用工具条";
            // 
            // btniOpen
            // 
            this.btniOpen.Category = "常用工具条";
            this.btniOpen.Image = ((System.Drawing.Image)(resources.GetObject("btniOpen.Image")));
            this.btniOpen.Name = "btniOpen";
            this.btniOpen.Size = new System.Drawing.Size(52, 22);
            this.btniOpen.Text = "打开";
            this.btniOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // btniAbout
            // 
            this.btniAbout.Category = "常用工具条";
            this.btniAbout.Image = ((System.Drawing.Image)(resources.GetObject("btniAbout.Image")));
            this.btniAbout.Name = "btniAbout";
            this.btniAbout.Size = new System.Drawing.Size(52, 22);
            this.btniAbout.Text = "关于";
            this.btniAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // separatorItem2
            // 
            this.separatorItem2.Category = "常用工具条";
            this.separatorItem2.Name = "separatorItem2";
            this.separatorItem2.Size = new System.Drawing.Size(6, 25);
            // 
            // btniExist
            // 
            this.btniExist.Category = "常用工具条";
            this.btniExist.Image = ((System.Drawing.Image)(resources.GetObject("btniExist.Image")));
            this.btniExist.Name = "btniExist";
            this.btniExist.Size = new System.Drawing.Size(52, 22);
            this.btniExist.Text = "退出";
            this.btniExist.Click += new System.EventHandler(this.miExist_Click);
            // 
            // toolBar2
            // 
            this.toolBar2.Image = null;
            this.toolBar2.Items.Add(this.radioButtonItem1);
            this.toolBar2.Items.Add(this.checkBoxItem1);
            this.toolBar2.Items.Add(this.separatorItem3);
            this.toolBar2.Items.Add(this.numericUpDownItem1);
            this.toolBar2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolBar2.Location = new System.Drawing.Point(191, 25);
            this.toolBar2.MinimumSize = new System.Drawing.Size(23, 23);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.Size = new System.Drawing.Size(257, 25);
            this.toolBar2.TabIndex = 2;
            this.toolBar2.Text = "新增项";
            // 
            // radioButtonItem1
            // 
            this.radioButtonItem1.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonItem1.Name = "radioButtonItem1";
            this.radioButtonItem1.Size = new System.Drawing.Size(62, 22);
            this.radioButtonItem1.Text = "单选框";
            this.radioButtonItem1.VOffset = 1;
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.Size = new System.Drawing.Size(63, 22);
            this.checkBoxItem1.Text = "复选框";
            this.checkBoxItem1.VOffset = 1;
            // 
            // separatorItem3
            // 
            this.separatorItem3.Name = "separatorItem3";
            this.separatorItem3.Size = new System.Drawing.Size(6, 25);
            // 
            // numericUpDownItem1
            // 
            this.numericUpDownItem1.AutoSize = false;
            this.numericUpDownItem1.Name = "numericUpDownItem1";
            this.numericUpDownItem1.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownItem1.Text = "0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.contextMenu1;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 50);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(615, 320);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // DemoOfDockBarManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 393);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dockBarDockAreaLeft1);
            this.Controls.Add(this.dockBarDockAreaRight1);
            this.Controls.Add(this.dockBarDockAreaTop1);
            this.Controls.Add(this.dockBarDockAreaBottom1);
            this.MainMenuStrip = this.menuBar1;
            this.Name = "DemoOfDockBarManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DockBarManager控件";
            this.Load += new System.EventHandler(this.DemoOfDockBarManagerForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DemoOfDockBarManagerForm_FormClosed);
            this.contextMenu1.ResumeLayout(false);
            this.dockBarDockAreaBottom1.ResumeLayout(false);
            this.dockBarDockAreaBottom1.PerformLayout();
            this.statusBar1.ResumeLayout(false);
            this.dockBarDockAreaTop1.ResumeLayout(false);
            this.dockBarDockAreaTop1.PerformLayout();
            this.menuBar1.ResumeLayout(false);
            this.toolBar1.ResumeLayout(false);
            this.toolBar2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager1;
        private GISShare.Controls.WinForm.DockBar.DockBarDockAreaBottom dockBarDockAreaBottom1;
        private GISShare.Controls.WinForm.DockBar.DockBarDockAreaLeft dockBarDockAreaLeft1;
        private GISShare.Controls.WinForm.DockBar.DockBarDockAreaRight dockBarDockAreaRight1;
        private GISShare.Controls.WinForm.DockBar.DockBarDockAreaTop dockBarDockAreaTop1;
        private GISShare.Controls.WinForm.DockBar.StatusBar statusBar1;
        private GISShare.Controls.WinForm.DockBar.ToolBar toolBar1;
        private GISShare.Controls.WinForm.DockBar.MenuBar menuBar1;
        private GISShare.Controls.WinForm.DockBar.MenuItem menuItem1;
        private GISShare.Controls.WinForm.DockBar.MenuItem miOpen;
        private GISShare.Controls.WinForm.DockBar.MenuItem miSave;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private GISShare.Controls.WinForm.DockBar.LabelItem lbliNum;
        private GISShare.Controls.WinForm.DockBar.ButtonItem btniOpen;
        private GISShare.Controls.WinForm.DockBar.ButtonItem btniAbout;
        private GISShare.Controls.WinForm.DockBar.SeparatorItem separatorItem2;
        private GISShare.Controls.WinForm.DockBar.ButtonItem btniExist;
        private GISShare.Controls.WinForm.DockBar.SeparatorItem separatorItem1;
        private GISShare.Controls.WinForm.DockBar.MenuItem miExist;
        private GISShare.Controls.WinForm.DockBar.MenuItem menuItem5;
        private GISShare.Controls.WinForm.DockBar.MenuItem miHelp;
        private GISShare.Controls.WinForm.DockBar.MenuItem miAbout;
        private GISShare.Controls.WinForm.DockBar.ToolBar toolBar2;
        private GISShare.Controls.WinForm.DockBar.RadioButtonItem radioButtonItem1;
        private GISShare.Controls.WinForm.DockBar.CheckBoxItem checkBoxItem1;
        private GISShare.Controls.WinForm.DockBar.SeparatorItem separatorItem3;
        private GISShare.Controls.WinForm.DockBar.NumericUpDownItem numericUpDownItem1;
        private GISShare.Controls.WinForm.DockBar.ContextMenu contextMenu1;
        private GISShare.Controls.WinForm.DockBar.MenuItem miOpen2;
        private GISShare.Controls.WinForm.DockBar.MenuItem miAbout2;
        private GISShare.Controls.WinForm.DockBar.MenuItem miExist2;
    }
}
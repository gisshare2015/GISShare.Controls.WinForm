namespace GISShare.Controls.WinForm.Demo.WFNew
{
    partial class DemoOfDockPanelManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfDockPanelManagerForm));
            this.dockPanelManager1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager();
            this.basePanel1 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.basePanel2 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.basePanel4 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.basePanel5 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.basePanel3 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.basePanel6 = new GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dockPanelDockAreaLeft1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaLeft();
            this.dockPanel1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel();
            this.dockPanelDockAreaBottom1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaBottom();
            this.dockPanel2 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel();
            this.dockPanelDockAreaRight1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaRight();
            this.dockPanel4 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel();
            this.dockPanel3 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel();
            this.documentDockArea1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DocumentDockArea();
            this.btnOpen = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.basePanel2.SuspendLayout();
            this.basePanel4.SuspendLayout();
            this.basePanel5.SuspendLayout();
            this.basePanel3.SuspendLayout();
            this.basePanel6.SuspendLayout();
            this.dockPanelDockAreaLeft1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanelDockAreaBottom1.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.dockPanelDockAreaRight1.SuspendLayout();
            this.dockPanel4.SuspendLayout();
            this.dockPanel3.SuspendLayout();
            this.documentDockArea1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanelManager1
            // 
            this.dockPanelManager1.BasePanels.Add(this.basePanel1);
            this.dockPanelManager1.BasePanels.Add(this.basePanel2);
            this.dockPanelManager1.BasePanels.Add(this.basePanel4);
            this.dockPanelManager1.BasePanels.Add(this.basePanel5);
            this.dockPanelManager1.BasePanels.Add(this.basePanel3);
            this.dockPanelManager1.BasePanels.Add(this.basePanel6);
            this.dockPanelManager1.DockPanelDockAreas.Add(this.dockPanelDockAreaLeft1);
            this.dockPanelManager1.DockPanelDockAreas.Add(this.dockPanelDockAreaBottom1);
            this.dockPanelManager1.DockPanelDockAreas.Add(this.dockPanelDockAreaRight1);
            this.dockPanelManager1.DockPanels.Add(this.dockPanel1);
            this.dockPanelManager1.DockPanels.Add(this.dockPanel2);
            this.dockPanelManager1.DockPanels.Add(this.dockPanel3);
            this.dockPanelManager1.DockPanels.Add(this.dockPanel4);
            this.dockPanelManager1.DocumentArea = this.documentDockArea1;
            this.dockPanelManager1.ParentForm = this;
            // 
            // basePanel1
            // 
            this.basePanel1.Image = ((System.Drawing.Image)(resources.GetObject("basePanel1.Image")));
            this.basePanel1.Location = new System.Drawing.Point(1, 20);
            this.basePanel1.Name = "basePanel1";
            this.basePanel1.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel1.Size = new System.Drawing.Size(179, 389);
            this.basePanel1.TabIndex = 0;
            this.basePanel1.Text = "工具箱";
            // 
            // basePanel2
            // 
            this.basePanel2.Controls.Add(this.listBox2);
            this.basePanel2.Image = ((System.Drawing.Image)(resources.GetObject("basePanel2.Image")));
            this.basePanel2.Location = new System.Drawing.Point(1, 20);
            this.basePanel2.Name = "basePanel2";
            this.basePanel2.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel2.Size = new System.Drawing.Size(489, 90);
            this.basePanel2.TabIndex = 0;
            this.basePanel2.Text = "输出";
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(489, 88);
            this.listBox2.TabIndex = 0;
            // 
            // basePanel4
            // 
            this.basePanel4.Controls.Add(this.listBox1);
            this.basePanel4.Image = ((System.Drawing.Image)(resources.GetObject("basePanel4.Image")));
            this.basePanel4.Location = new System.Drawing.Point(1, 20);
            this.basePanel4.Name = "basePanel4";
            this.basePanel4.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel4.Size = new System.Drawing.Size(530, 90);
            this.basePanel4.TabIndex = 1;
            this.basePanel4.Text = "命令";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(530, 88);
            this.listBox1.TabIndex = 0;
            // 
            // basePanel5
            // 
            this.basePanel5.Controls.Add(this.richTextBox1);
            this.basePanel5.Image = ((System.Drawing.Image)(resources.GetObject("basePanel5.Image")));
            this.basePanel5.Location = new System.Drawing.Point(0, 21);
            this.basePanel5.Name = "basePanel5";
            this.basePanel5.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel5.Size = new System.Drawing.Size(327, 253);
            this.basePanel5.TabIndex = 0;
            this.basePanel5.Text = "布局文件";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(327, 253);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // basePanel3
            // 
            this.basePanel3.Controls.Add(this.propertyGrid1);
            this.basePanel3.Image = ((System.Drawing.Image)(resources.GetObject("basePanel3.Image")));
            this.basePanel3.Location = new System.Drawing.Point(1, 20);
            this.basePanel3.Name = "basePanel3";
            this.basePanel3.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel3.Size = new System.Drawing.Size(188, 232);
            this.basePanel3.TabIndex = 0;
            this.basePanel3.Text = "属性";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(188, 232);
            this.propertyGrid1.TabIndex = 0;
            // 
            // basePanel6
            // 
            this.basePanel6.Controls.Add(this.treeView1);
            this.basePanel6.Image = ((System.Drawing.Image)(resources.GetObject("basePanel6.Image")));
            this.basePanel6.Location = new System.Drawing.Point(1, 20);
            this.basePanel6.Name = "basePanel6";
            this.basePanel6.Padding = new System.Windows.Forms.Padding(0);
            this.basePanel6.Size = new System.Drawing.Size(199, 232);
            this.basePanel6.TabIndex = 1;
            this.basePanel6.Text = "解决方案资源管理器";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(199, 232);
            this.treeView1.TabIndex = 0;
            // 
            // dockPanelDockAreaLeft1
            // 
            this.dockPanelDockAreaLeft1.Controls.Add(this.dockPanel1);
            this.dockPanelDockAreaLeft1.InternalMinWidth = 25;
            this.dockPanelDockAreaLeft1.Location = new System.Drawing.Point(0, 0);
            this.dockPanelDockAreaLeft1.Name = "dockPanelDockAreaLeft1";
            this.dockPanelDockAreaLeft1.OuterMinWidth = 100;
            this.dockPanelDockAreaLeft1.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanelDockAreaLeft1.Size = new System.Drawing.Size(185, 410);
            this.dockPanelDockAreaLeft1.TabIndex = 0;
            this.dockPanelDockAreaLeft1.Text = "DockPanelDockAreaLeft";
            // 
            // dockPanel1
            // 
            this.dockPanel1.BasePanels.Add(this.basePanel1);
            this.dockPanel1.Controls.Add(this.basePanel1);
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanel1.Size = new System.Drawing.Size(181, 410);
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Text = "工具箱";
            // 
            // dockPanelDockAreaBottom1
            // 
            this.dockPanelDockAreaBottom1.Controls.Add(this.dockPanel2);
            this.dockPanelDockAreaBottom1.InternalMinWidth = 25;
            this.dockPanelDockAreaBottom1.Location = new System.Drawing.Point(185, 274);
            this.dockPanelDockAreaBottom1.Name = "dockPanelDockAreaBottom1";
            this.dockPanelDockAreaBottom1.OuterMinWidth = 100;
            this.dockPanelDockAreaBottom1.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanelDockAreaBottom1.Size = new System.Drawing.Size(532, 136);
            this.dockPanelDockAreaBottom1.SplitPanelDock = System.Windows.Forms.DockStyle.Bottom;
            this.dockPanelDockAreaBottom1.TabIndex = 1;
            this.dockPanelDockAreaBottom1.Text = "DockPanelDockAreaBottom";
            // 
            // dockPanel2
            // 
            this.dockPanel2.BasePanels.Add(this.basePanel2);
            this.dockPanel2.BasePanels.Add(this.basePanel4);
            this.dockPanel2.BasePanelSelectedIndex = 1;
            this.dockPanel2.Controls.Add(this.basePanel2);
            this.dockPanel2.Controls.Add(this.basePanel4);
            this.dockPanel2.Location = new System.Drawing.Point(0, 4);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanel2.Size = new System.Drawing.Size(532, 132);
            this.dockPanel2.TabIndex = 0;
            this.dockPanel2.Text = "命令";
            // 
            // dockPanelDockAreaRight1
            // 
            this.dockPanelDockAreaRight1.Controls.Add(this.dockPanel4);
            this.dockPanelDockAreaRight1.InternalMinWidth = 25;
            this.dockPanelDockAreaRight1.Location = new System.Drawing.Point(512, 0);
            this.dockPanelDockAreaRight1.Name = "dockPanelDockAreaRight1";
            this.dockPanelDockAreaRight1.OuterMinWidth = 100;
            this.dockPanelDockAreaRight1.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanelDockAreaRight1.Size = new System.Drawing.Size(205, 274);
            this.dockPanelDockAreaRight1.SplitPanelDock = System.Windows.Forms.DockStyle.Right;
            this.dockPanelDockAreaRight1.TabIndex = 3;
            this.dockPanelDockAreaRight1.Text = "DockPanelDockAreaRight";
            // 
            // dockPanel4
            // 
            this.dockPanel4.BasePanels.Add(this.basePanel3);
            this.dockPanel4.BasePanels.Add(this.basePanel6);
            this.dockPanel4.BasePanelSelectedIndex = 1;
            this.dockPanel4.Controls.Add(this.basePanel3);
            this.dockPanel4.Controls.Add(this.basePanel6);
            this.dockPanel4.Location = new System.Drawing.Point(4, 0);
            this.dockPanel4.Name = "dockPanel4";
            this.dockPanel4.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanel4.Size = new System.Drawing.Size(201, 274);
            this.dockPanel4.TabIndex = 0;
            this.dockPanel4.Text = "解决方案资源管理器";
            // 
            // dockPanel3
            // 
            this.dockPanel3.BasePanels.Add(this.basePanel5);
            this.dockPanel3.Controls.Add(this.basePanel5);
            this.dockPanel3.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.Padding = new System.Windows.Forms.Padding(0);
            this.dockPanel3.Size = new System.Drawing.Size(327, 274);
            this.dockPanel3.TabIndex = 0;
            this.dockPanel3.Text = "布局文件";
            // 
            // documentDockArea1
            // 
            this.documentDockArea1.Controls.Add(this.dockPanel3);
            this.documentDockArea1.Location = new System.Drawing.Point(185, 0);
            this.documentDockArea1.Name = "documentDockArea1";
            this.documentDockArea1.Padding = new System.Windows.Forms.Padding(0);
            this.documentDockArea1.Size = new System.Drawing.Size(327, 274);
            this.documentDockArea1.TabIndex = 2;
            this.documentDockArea1.Text = "DocumentDockArea";
            // 
            // btnOpen
            // 
            this.btnOpen.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnOpen.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Name = null;
            this.btnOpen.Size = new System.Drawing.Size(98, 18);
            this.btnOpen.Tag = null;
            this.btnOpen.Text = "加载布局文件";
            this.btnOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOpen_MouseClick);
            // 
            // DemoOfDockPanelManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 410);
            this.Controls.Add(this.documentDockArea1);
            this.Controls.Add(this.dockPanelDockAreaRight1);
            this.Controls.Add(this.dockPanelDockAreaBottom1);
            this.Controls.Add(this.dockPanelDockAreaLeft1);
            this.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfDockPanelManagerForm";
            this.ShowQuickAccessToolbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DockPanelManager控件";
            this.ToolbarItems.Add(this.btnOpen);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DemoOfDockPanelManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.DemoOfDockPanelManagerForm_Load);
            this.basePanel2.ResumeLayout(false);
            this.basePanel4.ResumeLayout(false);
            this.basePanel5.ResumeLayout(false);
            this.basePanel3.ResumeLayout(false);
            this.basePanel6.ResumeLayout(false);
            this.dockPanelDockAreaLeft1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanelDockAreaBottom1.ResumeLayout(false);
            this.dockPanel2.ResumeLayout(false);
            this.dockPanelDockAreaRight1.ResumeLayout(false);
            this.dockPanel4.ResumeLayout(false);
            this.dockPanel3.ResumeLayout(false);
            this.documentDockArea1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel2;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaLeft dockPanelDockAreaLeft1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel dockPanel1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaBottom dockPanelDockAreaBottom1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel dockPanel2;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DocumentDockArea documentDockArea1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel4;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel5;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel dockPanel3;
        private System.Windows.Forms.ListBox listBox1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel3;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel6;
        private System.Windows.Forms.TreeView treeView1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelDockAreaRight dockPanelDockAreaRight1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel dockPanel4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnOpen;
    }
}
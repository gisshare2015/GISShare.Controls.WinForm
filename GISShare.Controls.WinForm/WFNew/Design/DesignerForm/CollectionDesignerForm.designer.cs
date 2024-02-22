namespace GISShare.Controls.WinForm.WFNew.Design
{
    partial class CollectionDesignerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionDesignerForm));
            this.treeView1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            this.ribbonPopup1 = new GISShare.Controls.WinForm.WFNew.ContextPopup();
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.btnTopForm = new GISShare.Controls.WinForm.WFNew.CheckButtonItem();
            this.ribbonSeparatorItem1 = new GISShare.Controls.WinForm.WFNew.SeparatorItem();
            this.btnExpand = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapse = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AutoGetFocus = true;
            this.treeView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.treeView1.Font = new System.Drawing.Font("宋体", 9F);
            this.treeView1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedNode = null;
            this.treeView1.Size = new System.Drawing.Size(861, 493);
            this.treeView1.Tag = null;
            this.treeView1.Text = null;
            this.treeView1.SelectedNodeChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(this.treeView1_SelectedNodeChanged);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // ribbonPopup1
            // 
            this.ribbonPopup1.DropShadowEnabled = false;
            this.ribbonPopup1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ribbonPopup1.Name = "ribbonPopup1";
            this.ribbonPopup1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonPopup1.ShowItemToolTips = false;
            this.ribbonPopup1.Size = new System.Drawing.Size(10, 20);
            // 
            // toolBarN1
            // 
            this.toolBarN1.BaseItems.Add(this.btnTopForm);
            this.toolBarN1.BaseItems.Add(this.ribbonSeparatorItem1);
            this.toolBarN1.BaseItems.Add(this.btnExpand);
            this.toolBarN1.BaseItems.Add(this.btnCollapse);
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Margin = new System.Windows.Forms.Padding(4);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(2);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(861, 38);
            this.toolBarN1.Tag = null;
            this.toolBarN1.Text = "baseBar1";
            // 
            // btnTopForm
            // 
            this.btnTopForm.Checked = true;
            this.btnTopForm.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnTopForm.Font = new System.Drawing.Font("宋体", 9F);
            this.btnTopForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTopForm.Image = null;
            this.btnTopForm.Location = new System.Drawing.Point(12, 2);
            this.btnTopForm.Name = "btnTopForm";
            this.btnTopForm.Size = new System.Drawing.Size(81, 34);
            this.btnTopForm.Tag = null;
            this.btnTopForm.Text = "窗体置顶";
            this.btnTopForm.UsingViewOverflow = false;
            this.btnTopForm.CheckedChanged += new System.EventHandler(this.btnTopForm_CheckedChanged);
            // 
            // ribbonSeparatorItem1
            // 
            this.ribbonSeparatorItem1.AutoLayout = true;
            this.ribbonSeparatorItem1.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonSeparatorItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSeparatorItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSeparatorItem1.Location = new System.Drawing.Point(94, 2);
            this.ribbonSeparatorItem1.LockWith = true;
            this.ribbonSeparatorItem1.MinimumSize = new System.Drawing.Size(3, 3);
            this.ribbonSeparatorItem1.Name = "ribbonSeparatorItem1";
            this.ribbonSeparatorItem1.Size = new System.Drawing.Size(3, 34);
            this.ribbonSeparatorItem1.Tag = null;
            this.ribbonSeparatorItem1.Text = "ribbonSeparatorItem1";
            this.ribbonSeparatorItem1.UsingViewOverflow = false;
            // 
            // btnExpand
            // 
            this.btnExpand.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpand.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpand.Image = null;
            this.btnExpand.Location = new System.Drawing.Point(98, 2);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(62, 34);
            this.btnExpand.Tag = null;
            this.btnExpand.Text = "展开树";
            this.btnExpand.UsingViewOverflow = false;
            this.btnExpand.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnExpand_MouseClick);
            // 
            // btnCollapse
            // 
            this.btnCollapse.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnCollapse.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCollapse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCollapse.Image = null;
            this.btnCollapse.Location = new System.Drawing.Point(161, 2);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(62, 34);
            this.btnCollapse.Tag = null;
            this.btnCollapse.Text = "折叠树";
            this.btnCollapse.UsingViewOverflow = false;
            this.btnCollapse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapse_MouseClick);
            // 
            // btnInfo
            // 
            this.btnInfo.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.Location = new System.Drawing.Point(48, 6);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(78, 33);
            this.btnInfo.Tag = null;
            this.btnInfo.Text = "关于";
            this.btnInfo.UsingViewOverflow = false;
            this.btnInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnInfo_MouseClick);
            // 
            // toolBarN2
            // 
            this.toolBarN2.BaseItems.Add(this.lblInfo);
            this.toolBarN2.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN2.eToolBarStyle = GISShare.Controls.WinForm.WFNew.ToolBarStyle.eStatusBar;
            this.toolBarN2.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN2.IsRestrictItems = true;
            this.toolBarN2.Location = new System.Drawing.Point(0, 0);
            this.toolBarN2.Margin = new System.Windows.Forms.Padding(4);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(861, 37);
            this.toolBarN2.Tag = null;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(239, 37);
            this.lblInfo.Tag = null;
            this.lblInfo.Text = "GISShare.Controls.WinForm";
            this.lblInfo.UsingViewOverflow = false;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.toolBarN1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(861, 38);
            this.baseItemHost1.TabIndex = 4;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.toolBarN2;
            this.baseItemHost2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseItemHost2.Location = new System.Drawing.Point(0, 531);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(861, 37);
            this.baseItemHost2.TabIndex = 5;
            this.baseItemHost2.Text = "baseItemHost2";
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BackColor = System.Drawing.Color.White;
            this.baseItemHost3.BaseItemObject = this.treeView1;
            this.baseItemHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost3.Location = new System.Drawing.Point(0, 38);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(861, 493);
            this.baseItemHost3.TabIndex = 6;
            this.baseItemHost3.Text = "baseItemHost3";
            // 
            // CollectionDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 568);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CollectionDesignerForm";
            this.ShowQuickAccessToolbar = true;
            this.Text = "集合控件设计器";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem treeView1;
        private GISShare.Controls.WinForm.WFNew.ContextPopup ribbonPopup1;
        private ToolBarItem toolBarN1;
        private BaseButtonItem btnExpand;
        private BaseButtonItem btnCollapse;
        private SeparatorItem ribbonSeparatorItem1;
        private CheckButtonItem btnTopForm;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnInfo;
        private ToolBarItem toolBarN2;
        private LabelItem lblInfo;
        private BaseItemHost baseItemHost1;
        private BaseItemHost baseItemHost2;
        private BaseItemHost baseItemHost3;
    }
}
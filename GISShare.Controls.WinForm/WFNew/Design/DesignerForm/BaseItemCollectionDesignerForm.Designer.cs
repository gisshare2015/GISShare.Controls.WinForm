namespace GISShare.Controls.WinForm.WFNew.Design
{
    partial class BaseItemCollectionDesignerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseItemCollectionDesignerForm));
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree();
            this.ribbonPopup1 = new GISShare.Controls.WinForm.WFNew.ContextPopup();
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.btnTopForm = new GISShare.Controls.WinForm.WFNew.CheckButtonItem();
            this.ribbonSeparatorItem1 = new GISShare.Controls.WinForm.WFNew.SeparatorItem();
            this.btnExpand = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapse = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.SuspendLayout();
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 25);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(623, 411);
            this.nodeViewItemTree1.TabIndex = 1;
            this.nodeViewItemTree1.SelectedNodeChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(this.nodeViewItemTree1_SelectedNodeChanged);
            this.nodeViewItemTree1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nodeViewItemTree1_MouseClick);
            // 
            // ribbonPopup1
            // 
            this.ribbonPopup1.DropShadowEnabled = false;
            this.ribbonPopup1.Name = "ribbonPopup1";
            this.ribbonPopup1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonPopup1.ShowItemToolTips = false;
            this.ribbonPopup1.Size = new System.Drawing.Size(10, 20);
            // 
            // toolBarN1
            // 
            this.toolBarN1.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN1.BaseItems.Add(this.btnTopForm);
            this.toolBarN1.BaseItems.Add(this.ribbonSeparatorItem1);
            this.toolBarN1.BaseItems.Add(this.btnExpand);
            this.toolBarN1.BaseItems.Add(this.btnCollapse);
            this.toolBarN1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(1);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(623, 25);
            this.toolBarN1.TabIndex = 2;
            this.toolBarN1.Text = "baseBar1";
            // 
            // btnTopForm
            // 
            this.btnTopForm.Checked = true;
            this.btnTopForm.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnTopForm.Font = new System.Drawing.Font("宋体", 9F);
            this.btnTopForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTopForm.Image = null;
            this.btnTopForm.Name = "btnTopForm";
            this.btnTopForm.Size = new System.Drawing.Size(54, 23);
            this.btnTopForm.Tag = null;
            this.btnTopForm.Text = "窗体置顶";
            this.btnTopForm.CheckedChanged += new System.EventHandler(this.btnTopForm_CheckedChanged);
            // 
            // ribbonSeparatorItem1
            // 
            this.ribbonSeparatorItem1.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.ribbonSeparatorItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.ribbonSeparatorItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonSeparatorItem1.LockWith = true;
            this.ribbonSeparatorItem1.Name = "ribbonSeparatorItem1";
            this.ribbonSeparatorItem1.Size = new System.Drawing.Size(3, 23);
            this.ribbonSeparatorItem1.Tag = null;
            this.ribbonSeparatorItem1.Text = "ribbonSeparatorItem1";
            // 
            // btnExpand
            // 
            this.btnExpand.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpand.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpand.Image = null;
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(42, 23);
            this.btnExpand.Tag = null;
            this.btnExpand.Text = "展开树";
            this.btnExpand.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnExpand_MouseClick);
            // 
            // btnCollapse
            // 
            this.btnCollapse.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnCollapse.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCollapse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCollapse.Image = null;
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(42, 23);
            this.btnCollapse.Tag = null;
            this.btnCollapse.Text = "折叠树";
            this.btnCollapse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapse_MouseClick);
            // 
            // btnInfo
            // 
            this.btnInfo.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(49, 19);
            this.btnInfo.Tag = null;
            this.btnInfo.Text = "关于";
            this.btnInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnInfo_MouseClick);
            // 
            // toolBarN2
            // 
            this.toolBarN2.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN2.BaseItems.Add(this.lblInfo);
            this.toolBarN2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBarN2.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN2.eToolBarStyle = GISShare.Controls.WinForm.WFNew.ToolBarStyle.eStatusBar;
            this.toolBarN2.IsRestrictItems = true;
            this.toolBarN2.Location = new System.Drawing.Point(0, 436);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(623, 23);
            this.toolBarN2.TabIndex = 3;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(128, 23);
            this.lblInfo.Tag = null;
            this.lblInfo.Text = "GISShare.Controls.WinForm";
            // 
            // BaseItemCollectionDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 459);
            this.Controls.Add(this.nodeViewItemTree1);
            this.Controls.Add(this.toolBarN2);
            this.Controls.Add(this.toolBarN1);
            this.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BaseItemCollectionDesignerForm";
            this.ShowQuickAccessToolbar = true;
            this.Text = "集合控件设计器";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree nodeViewItemTree1;
        private ContextPopup ribbonPopup1;
        private ToolBarN toolBarN1;
        private BaseButtonItem btnExpand;
        private BaseButtonItem btnCollapse;
        private SeparatorItem ribbonSeparatorItem1;
        private CheckButtonItem btnTopForm;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnInfo;
        private ToolBarN toolBarN2;
        private LabelItem lblInfo;
    }
}
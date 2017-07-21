namespace GISShare.Controls.Plugin.WinForm
{
    partial class PluginCategoryDictionaryTBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginCategoryDictionaryTBForm));
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblCategory = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.ctCategory = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.btnShow = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.separatorItem1 = new GISShare.Controls.WinForm.WFNew.SeparatorItem();
            this.btnExpandAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapseAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblNum = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN1.BaseItems.Add(this.lblCategory);
            this.toolBarN1.BaseItems.Add(this.ctCategory);
            this.toolBarN1.BaseItems.Add(this.btnShow);
            this.toolBarN1.BaseItems.Add(this.separatorItem1);
            this.toolBarN1.BaseItems.Add(this.btnExpandAll);
            this.toolBarN1.BaseItems.Add(this.btnCollapseAll);
            this.toolBarN1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(577, 25);
            this.toolBarN1.TabIndex = 0;
            this.toolBarN1.Text = "toolBarN1";
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(66, 25);
            this.lblCategory.Tag = null;
            this.lblCategory.Text = "插件目录：";
            // 
            // ctCategory
            // 
            this.ctCategory.DropDownHeight = 138;
            this.ctCategory.DropDownWidth = 160;
            this.ctCategory.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.ctCategory.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.ctCategory.Font = new System.Drawing.Font("宋体", 9F);
            this.ctCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctCategory.LockHeight = true;
            this.ctCategory.Name = "comboTreeItem1";
            this.ctCategory.SelectedNode = null;
            this.ctCategory.Size = new System.Drawing.Size(239, 21);
            this.ctCategory.Tag = null;
            this.ctCategory.Text = "";
            this.ctCategory.SelectedNodeChanged += new Controls.WinForm.PropertyChangedEventHandler(this.ctCategory_SelectedNodeChanged);
            // 
            // btnShow
            // 
            this.btnShow.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eRight;
            this.btnShow.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnShow.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnShow.Image = null;
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(29, 25);
            this.btnShow.Tag = null;
            this.btnShow.Text = "显示";
            this.btnShow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShow_MouseClick);
            // 
            // separatorItem1
            // 
            this.separatorItem1.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.separatorItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.separatorItem1.LockWith = true;
            this.separatorItem1.Name = "separatorItem1";
            this.separatorItem1.Size = new System.Drawing.Size(3, 25);
            this.separatorItem1.Tag = null;
            this.separatorItem1.Text = "separatorItem1";
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpandAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpandAll.Image = null;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(29, 25);
            this.btnExpandAll.Tag = null;
            this.btnExpandAll.Text = "展开";
            this.btnExpandAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnExpandAll_MouseClick);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnCollapseAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCollapseAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCollapseAll.Image = null;
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(29, 25);
            this.btnCollapseAll.Tag = null;
            this.btnCollapseAll.Text = "收起";
            this.btnCollapseAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapseAll_MouseClick);
            // 
            // toolBarN2
            // 
            this.toolBarN2.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN2.BaseItems.Add(this.lblNum);
            this.toolBarN2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBarN2.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN2.eToolBarStyle = GISShare.Controls.WinForm.WFNew.ToolBarStyle.eStatusBar;
            this.toolBarN2.IsRestrictItems = true;
            this.toolBarN2.Location = new System.Drawing.Point(0, 329);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(577, 23);
            this.toolBarN2.TabIndex = 1;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblNum
            // 
            this.lblNum.Font = new System.Drawing.Font("宋体", 9F);
            this.lblNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(72, 23);
            this.lblNum.Tag = null;
            this.lblNum.Text = "当前插件：0";
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 25);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(577, 304);
            this.nodeViewItemTree1.TabIndex = 2;
            this.nodeViewItemTree1.Text = "nodeViewItemTree1";
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
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnInfo_MouseClick);
            // 
            // PluginCategoryDictionaryTBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 352);
            this.Controls.Add(this.nodeViewItemTree1);
            this.Controls.Add(this.toolBarN2);
            this.Controls.Add(this.toolBarN1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PluginCategoryDictionaryTBForm";
            this.ShowQuickAccessToolbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件目录字典";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.ToolBarN toolBarN1;
        private Controls.WinForm.WFNew.LabelItem lblCategory;
        private Controls.WinForm.WFNew.ComboTreeItem ctCategory;
        private Controls.WinForm.WFNew.ButtonItem btnShow;
        private Controls.WinForm.WFNew.ToolBarN toolBarN2;
        private Controls.WinForm.WFNew.View.NodeViewItemTree nodeViewItemTree1;
        private Controls.WinForm.WFNew.SeparatorItem separatorItem1;
        private Controls.WinForm.WFNew.BaseButtonItem btnExpandAll;
        private Controls.WinForm.WFNew.BaseButtonItem btnCollapseAll;
        private Controls.WinForm.WFNew.LabelItem lblNum;
        private Controls.WinForm.WFNew.BaseButtonItem btnInfo;
    }
}
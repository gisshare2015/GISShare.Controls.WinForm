namespace GISShare.Controls.Plugin.WinForm
{
    partial class PluginCategoryDictionaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginCategoryDictionaryForm));
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblCategory = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.ctCategory = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.btnShow = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.separatorItem1 = new GISShare.Controls.WinForm.WFNew.SeparatorItem();
            this.btnExpandAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapseAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblNum = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BaseItems.Add(this.lblCategory);
            this.toolBarN1.BaseItems.Add(this.ctCategory);
            this.toolBarN1.BaseItems.Add(this.btnShow);
            this.toolBarN1.BaseItems.Add(this.separatorItem1);
            this.toolBarN1.BaseItems.Add(this.btnExpandAll);
            this.toolBarN1.BaseItems.Add(this.btnCollapseAll);
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(866, 38);
            this.toolBarN1.Tag = null;
            this.toolBarN1.Text = "toolBarN1";
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategory.Location = new System.Drawing.Point(10, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(99, 38);
            this.lblCategory.Tag = null;
            this.lblCategory.Text = "插件目录：";
            this.lblCategory.UsingViewOverflow = false;
            // 
            // ctCategory
            // 
            this.ctCategory.CanEdit = false;
            this.ctCategory.DropDownHeight = 174;
            this.ctCategory.DropDownWidth = 160;
            this.ctCategory.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.ctCategory.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.ctCategory.Font = new System.Drawing.Font("宋体", 9F);
            this.ctCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctCategory.Location = new System.Drawing.Point(110, 8);
            this.ctCategory.LockHeight = true;
            this.ctCategory.Name = "ctCategory";
            this.ctCategory.SelectedNode = null;
            this.ctCategory.ShowDropDownNum = 0;
            this.ctCategory.Size = new System.Drawing.Size(239, 21);
            this.ctCategory.Tag = null;
            this.ctCategory.Text = "";
            this.ctCategory.UsingViewOverflow = false;
            this.ctCategory.SelectedNodeChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(this.ctCategory_SelectedNodeChanged);
            // 
            // btnShow
            // 
            this.btnShow.eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eRight;
            this.btnShow.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnShow.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnShow.Image = null;
            this.btnShow.Location = new System.Drawing.Point(350, 0);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(44, 38);
            this.btnShow.Tag = null;
            this.btnShow.Text = "显示";
            this.btnShow.UsingViewOverflow = false;
            this.btnShow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShow_MouseClick);
            // 
            // separatorItem1
            // 
            this.separatorItem1.AutoLayout = true;
            this.separatorItem1.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorItem1.Font = new System.Drawing.Font("宋体", 9F);
            this.separatorItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.separatorItem1.Location = new System.Drawing.Point(395, 0);
            this.separatorItem1.LockWith = true;
            this.separatorItem1.MinimumSize = new System.Drawing.Size(3, 3);
            this.separatorItem1.Name = "separatorItem1";
            this.separatorItem1.Size = new System.Drawing.Size(3, 38);
            this.separatorItem1.Tag = null;
            this.separatorItem1.Text = "separatorItem1";
            this.separatorItem1.UsingViewOverflow = false;
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpandAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpandAll.Image = null;
            this.btnExpandAll.Location = new System.Drawing.Point(399, 0);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(44, 38);
            this.btnExpandAll.Tag = null;
            this.btnExpandAll.Text = "展开";
            this.btnExpandAll.UsingViewOverflow = false;
            this.btnExpandAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnExpandAll_MouseClick);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnCollapseAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCollapseAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCollapseAll.Image = null;
            this.btnCollapseAll.Location = new System.Drawing.Point(444, 0);
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(44, 38);
            this.btnCollapseAll.Tag = null;
            this.btnCollapseAll.Text = "收起";
            this.btnCollapseAll.UsingViewOverflow = false;
            this.btnCollapseAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapseAll_MouseClick);
            // 
            // toolBarN2
            // 
            this.toolBarN2.BaseItems.Add(this.lblNum);
            this.toolBarN2.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN2.eToolBarStyle = GISShare.Controls.WinForm.WFNew.ToolBarStyle.eStatusBar;
            this.toolBarN2.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN2.IsRestrictItems = true;
            this.toolBarN2.Location = new System.Drawing.Point(0, 0);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(866, 34);
            this.toolBarN2.Tag = null;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblNum
            // 
            this.lblNum.Font = new System.Drawing.Font("宋体", 9F);
            this.lblNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNum.Location = new System.Drawing.Point(0, 0);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(108, 34);
            this.lblNum.Tag = null;
            this.lblNum.Text = "当前插件：0";
            this.lblNum.UsingViewOverflow = false;
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.AutoGetFocus = true;
            this.nodeViewItemTree1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Font = new System.Drawing.Font("宋体", 9F);
            this.nodeViewItemTree1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 0);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(866, 456);
            this.nodeViewItemTree1.Tag = null;
            this.nodeViewItemTree1.Text = "nodeViewItemTree1";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.toolBarN1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(866, 38);
            this.baseItemHost1.TabIndex = 0;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.toolBarN2;
            this.baseItemHost2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseItemHost2.Location = new System.Drawing.Point(0, 494);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(866, 34);
            this.baseItemHost2.TabIndex = 1;
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BackColor = System.Drawing.SystemColors.Window;
            this.baseItemHost3.BaseItemObject = this.nodeViewItemTree1;
            this.baseItemHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost3.Location = new System.Drawing.Point(0, 38);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(866, 456);
            this.baseItemHost3.TabIndex = 2;
            // 
            // PluginCategoryDictionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 528);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PluginCategoryDictionaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件目录字典";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.LabelItem lblCategory;
        private Controls.WinForm.WFNew.ComboTreeItem ctCategory;
        private Controls.WinForm.WFNew.ButtonItem btnShow;
        private Controls.WinForm.WFNew.ToolBarItem toolBarN1;
        private Controls.WinForm.WFNew.ToolBarItem toolBarN2;
        private Controls.WinForm.WFNew.View.NodeViewItemTreeItem nodeViewItemTree1;
        private Controls.WinForm.WFNew.SeparatorItem separatorItem1;
        private Controls.WinForm.WFNew.BaseButtonItem btnExpandAll;
        private Controls.WinForm.WFNew.BaseButtonItem btnCollapseAll;
        private Controls.WinForm.WFNew.LabelItem lblNum;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
    }
}
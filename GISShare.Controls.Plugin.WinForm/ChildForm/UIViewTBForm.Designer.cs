namespace GISShare.Controls.Plugin.WinForm
{
    partial class UIViewTBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIViewTBForm));
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.btnExpandAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapseAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BaseItems.Add(this.btnExpandAll);
            this.toolBarN1.BaseItems.Add(this.btnCollapseAll);
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(1);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(850, 38);
            this.toolBarN1.Tag = null;
            this.toolBarN1.Text = "baseBar1";
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpandAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpandAll.Image = null;
            this.btnExpandAll.Location = new System.Drawing.Point(11, 1);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(44, 36);
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
            this.btnCollapseAll.Location = new System.Drawing.Point(56, 1);
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(44, 36);
            this.btnCollapseAll.Tag = null;
            this.btnCollapseAll.Text = "收起";
            this.btnCollapseAll.UsingViewOverflow = false;
            this.btnCollapseAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapseAll_MouseClick);
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
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(850, 34);
            this.toolBarN2.Tag = null;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(192, 34);
            this.lblInfo.Tag = null;
            this.lblInfo.Text = "LiuZhenHong.Controls";
            this.lblInfo.UsingViewOverflow = false;
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
            this.nodeViewItemTree1.Size = new System.Drawing.Size(850, 505);
            this.nodeViewItemTree1.Tag = null;
            this.nodeViewItemTree1.Text = "nodeViewItemTree1";
            // 
            // btnInfo
            // 
            this.btnInfo.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.Location = new System.Drawing.Point(44, 6);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(78, 33);
            this.btnInfo.Tag = null;
            this.btnInfo.Text = "关于";
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.UsingViewOverflow = false;
            this.btnInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnInfo_MouseClick);
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.toolBarN1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(850, 38);
            this.baseItemHost1.TabIndex = 2;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.toolBarN2;
            this.baseItemHost2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseItemHost2.Location = new System.Drawing.Point(0, 543);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(850, 34);
            this.baseItemHost2.TabIndex = 3;
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BackColor = System.Drawing.Color.White;
            this.baseItemHost3.BaseItemObject = this.nodeViewItemTree1;
            this.baseItemHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost3.Location = new System.Drawing.Point(0, 38);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(850, 505);
            this.baseItemHost3.TabIndex = 3;
            // 
            // UIViewTBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelItemsEventNC = false;
            this.ClientSize = new System.Drawing.Size(850, 577);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UIViewTBForm";
            this.ShowQuickAccessToolbar = true;
            this.Text = "界面视图";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.ToolBarItem toolBarN1;
        private Controls.WinForm.WFNew.ToolBarItem toolBarN2;
        private Controls.WinForm.WFNew.LabelItem lblInfo;
        private Controls.WinForm.WFNew.BaseButtonItem btnExpandAll;
        private Controls.WinForm.WFNew.BaseButtonItem btnCollapseAll;
        private Controls.WinForm.WFNew.View.NodeViewItemTreeItem nodeViewItemTree1;
        private Controls.WinForm.WFNew.BaseButtonItem btnInfo;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
    }
}
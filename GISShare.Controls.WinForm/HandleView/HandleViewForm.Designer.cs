namespace GISShare.Controls.WinForm
{
    partial class HandleViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleViewForm));
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblQueryText = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.ctQueryList = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.btnQuery = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarItem();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BaseItems.Add(this.lblQueryText);
            this.toolBarN1.BaseItems.Add(this.ctQueryList);
            this.toolBarN1.BaseItems.Add(this.btnQuery);
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolBarN1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.LeftBottomRadius = 0;
            this.toolBarN1.LeftTopRadius = 0;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.RightBottomRadius = 0;
            this.toolBarN1.RightTopRadius = 0;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(903, 36);
            this.toolBarN1.Tag = null;
            this.toolBarN1.Text = "toolBarN1";
            // 
            // lblQueryText
            // 
            this.lblQueryText.Font = new System.Drawing.Font("宋体", 9F);
            this.lblQueryText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblQueryText.Location = new System.Drawing.Point(10, 0);
            this.lblQueryText.Name = "lblQueryText";
            this.lblQueryText.Size = new System.Drawing.Size(99, 36);
            this.lblQueryText.Tag = null;
            this.lblQueryText.Text = "监测对象：";
            this.lblQueryText.UsingViewOverflow = false;
            // 
            // ctQueryList
            // 
            this.ctQueryList.CanEdit = false;
            this.ctQueryList.DropDownHeight = 246;
            this.ctQueryList.DropDownWidth = 28;
            this.ctQueryList.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.ctQueryList.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.ctQueryList.Font = new System.Drawing.Font("宋体", 9F);
            this.ctQueryList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctQueryList.Location = new System.Drawing.Point(110, 7);
            this.ctQueryList.LockHeight = true;
            this.ctQueryList.Name = "cbQueryList";
            this.ctQueryList.SelectedNode = null;
            this.ctQueryList.ShowDropDownNum = 0;
            this.ctQueryList.Size = new System.Drawing.Size(160, 21);
            this.ctQueryList.Tag = null;
            this.ctQueryList.Text = "";
            this.ctQueryList.UsingViewOverflow = false;
            // 
            // btnQuery
            // 
            this.btnQuery.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F);
            this.btnQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuery.Image = null;
            this.btnQuery.Location = new System.Drawing.Point(271, 0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnQuery.Size = new System.Drawing.Size(56, 36);
            this.btnQuery.Tag = null;
            this.btnQuery.Text = "检 索";
            this.btnQuery.UsingViewOverflow = false;
            this.btnQuery.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnQuery_MouseClick);
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.AutoGetFocus = true;
            this.nodeViewItemTree1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Font = new System.Drawing.Font("宋体", 9F);
            this.nodeViewItemTree1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 0);
            this.nodeViewItemTree1.Margin = new System.Windows.Forms.Padding(4);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(903, 622);
            this.nodeViewItemTree1.Tag = null;
            this.nodeViewItemTree1.Text = null;
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
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(903, 36);
            this.toolBarN2.Tag = null;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(44, 36);
            this.lblInfo.Tag = null;
            this.lblInfo.Text = "信息";
            this.lblInfo.UsingViewOverflow = false;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.toolBarN1;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(903, 36);
            this.baseItemHost1.TabIndex = 4;
            this.baseItemHost1.Text = "baseItemHost1";
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.toolBarN2;
            this.baseItemHost2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseItemHost2.Location = new System.Drawing.Point(0, 658);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(903, 36);
            this.baseItemHost2.TabIndex = 5;
            this.baseItemHost2.Text = "baseItemHost2";
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.AutoGetFocus = true;
            this.baseItemHost3.BackColor = System.Drawing.Color.White;
            this.baseItemHost3.BaseItemObject = this.nodeViewItemTree1;
            this.baseItemHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost3.Location = new System.Drawing.Point(0, 36);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(903, 622);
            this.baseItemHost3.TabIndex = 3;
            // 
            // HandleViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 694);
            this.Controls.Add(this.baseItemHost3);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.baseItemHost1);
            this.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HandleViewForm";
            this.ShowQuickAccessToolbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "句柄监测";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ToolBarItem toolBarN1;
        private GISShare.Controls.WinForm.WFNew.LabelItem lblQueryText;
        private GISShare.Controls.WinForm.WFNew.ComboTreeItem ctQueryList;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnQuery;
        private GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem nodeViewItemTree1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnInfo;
        private WFNew.ToolBarItem toolBarN2;
        private WFNew.LabelItem lblInfo;
        private WFNew.BaseItemHost baseItemHost1;
        private WFNew.BaseItemHost baseItemHost2;
        private WFNew.BaseItemHost baseItemHost3;

    }
}
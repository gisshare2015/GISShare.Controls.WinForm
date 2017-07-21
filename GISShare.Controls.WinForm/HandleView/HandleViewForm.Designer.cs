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
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblQueryText = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.ctQueryList = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.btnQuery = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree();
            this.btnInfo = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN1.BaseItems.Add(this.lblQueryText);
            this.toolBarN1.BaseItems.Add(this.ctQueryList);
            this.toolBarN1.BaseItems.Add(this.btnQuery);
            this.toolBarN1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.LeftBottomRadius = 0;
            this.toolBarN1.LeftTopRadius = 0;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.RightBottomRadius = 0;
            this.toolBarN1.RightTopRadius = 0;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(602, 25);
            this.toolBarN1.TabIndex = 1;
            this.toolBarN1.Text = "toolBarN1";
            // 
            // lblQueryText
            // 
            this.lblQueryText.Font = new System.Drawing.Font("宋体", 9F);
            this.lblQueryText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblQueryText.Name = "lblQueryText";
            this.lblQueryText.Size = new System.Drawing.Size(66, 25);
            this.lblQueryText.Tag = null;
            this.lblQueryText.Text = "监测对象：";
            // 
            // ctQueryList
            // 
            this.ctQueryList.DropDownHeight = 138;
            this.ctQueryList.DropDownWidth = 28;
            this.ctQueryList.eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
            this.ctQueryList.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.ctQueryList.Font = new System.Drawing.Font("宋体", 9F);
            this.ctQueryList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctQueryList.LockHeight = true;
            this.ctQueryList.Name = "cbQueryList";
            this.ctQueryList.SelectedNode = null;
            this.ctQueryList.Size = new System.Drawing.Size(160, 21);
            this.ctQueryList.Tag = null;
            this.ctQueryList.Text = "";
            // 
            // btnQuery
            // 
            this.btnQuery.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F);
            this.btnQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuery.Image = null;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnQuery.Size = new System.Drawing.Size(38, 25);
            this.btnQuery.Tag = null;
            this.btnQuery.Text = "检 索";
            this.btnQuery.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnQuery_MouseClick);
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 25);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(602, 415);
            this.nodeViewItemTree1.TabIndex = 3;
            // 
            // btnInfo
            // 
            this.btnInfo.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(48, 18);
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
            this.toolBarN2.Location = new System.Drawing.Point(0, 440);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(602, 23);
            this.toolBarN2.TabIndex = 4;
            this.toolBarN2.Text = "toolBarN2";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(29, 23);
            this.lblInfo.Tag = null;
            this.lblInfo.Text = "信息";
            // 
            // HandleViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 463);
            this.Controls.Add(this.nodeViewItemTree1);
            this.Controls.Add(this.toolBarN1);
            this.Controls.Add(this.toolBarN2);
            this.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "HandleViewForm";
            this.ShowQuickAccessToolbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "句柄监测";
            this.ToolbarItems.Add(this.btnInfo);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ToolBarN toolBarN1;
        private GISShare.Controls.WinForm.WFNew.LabelItem lblQueryText;
        private GISShare.Controls.WinForm.WFNew.ComboTreeItem ctQueryList;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnQuery;
        private GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree nodeViewItemTree1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem btnInfo;
        private WFNew.ToolBarN toolBarN2;
        private WFNew.LabelItem lblInfo;

    }
}
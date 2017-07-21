namespace GISShare.Controls.Plugin.WinForm
{
    partial class UIViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIViewForm));
            this.toolBarN1 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.btnExpandAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.btnCollapseAll = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.toolBarN2 = new GISShare.Controls.WinForm.WFNew.ToolBarN();
            this.lblInfo = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.nodeViewItemTree1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTree();
            this.SuspendLayout();
            // 
            // toolBarN1
            // 
            this.toolBarN1.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN1.BaseItems.Add(this.btnExpandAll);
            this.toolBarN1.BaseItems.Add(this.btnCollapseAll);
            this.toolBarN1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBarN1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN1.IsRestrictItems = true;
            this.toolBarN1.Location = new System.Drawing.Point(0, 0);
            this.toolBarN1.Name = "toolBarN1";
            this.toolBarN1.Padding = new System.Windows.Forms.Padding(1);
            this.toolBarN1.RestrictItemsHeight = -1;
            this.toolBarN1.RestrictItemsWidth = -1;
            this.toolBarN1.ShowNomalState = true;
            this.toolBarN1.Size = new System.Drawing.Size(567, 25);
            this.toolBarN1.TabIndex = 2;
            this.toolBarN1.Text = "baseBar1";
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.eImageSizeStyle = GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            this.btnExpandAll.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpandAll.Image = null;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(29, 23);
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
            this.btnCollapseAll.Size = new System.Drawing.Size(29, 23);
            this.btnCollapseAll.Tag = null;
            this.btnCollapseAll.Text = "收起";
            this.btnCollapseAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCollapseAll_MouseClick);
            // 
            // toolBarN2
            // 
            this.toolBarN2.BackColor = System.Drawing.Color.Transparent;
            this.toolBarN2.BaseItems.Add(this.lblInfo);
            this.toolBarN2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBarN2.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolBarN2.eToolBarStyle = GISShare.Controls.WinForm.WFNew.ToolBarStyle.eStatusBar;
            this.toolBarN2.IsRestrictItems = true;
            this.toolBarN2.Location = new System.Drawing.Point(0, 375);
            this.toolBarN2.Name = "toolBarN2";
            this.toolBarN2.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarN2.RestrictItemsHeight = -1;
            this.toolBarN2.RestrictItemsWidth = -1;
            this.toolBarN2.ShowGripRegion = false;
            this.toolBarN2.ShowNomalState = true;
            this.toolBarN2.Size = new System.Drawing.Size(567, 23);
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
            this.lblInfo.Text = "LiuZhenHong.Controls";
            // 
            // nodeViewItemTree1
            // 
            this.nodeViewItemTree1.BackColor = System.Drawing.SystemColors.Window;
            this.nodeViewItemTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeViewItemTree1.Location = new System.Drawing.Point(0, 25);
            this.nodeViewItemTree1.Name = "nodeViewItemTree1";
            this.nodeViewItemTree1.Padding = new System.Windows.Forms.Padding(0);
            this.nodeViewItemTree1.SelectedNode = null;
            this.nodeViewItemTree1.Size = new System.Drawing.Size(567, 350);
            this.nodeViewItemTree1.TabIndex = 4;
            this.nodeViewItemTree1.Text = "nodeViewItemTree1";
            // 
            // UIViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 398);
            this.Controls.Add(this.nodeViewItemTree1);
            this.Controls.Add(this.toolBarN2);
            this.Controls.Add(this.toolBarN1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UIViewForm";
            this.Text = "界面视图";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.ToolBarN toolBarN1;
        private Controls.WinForm.WFNew.ToolBarN toolBarN2;
        private Controls.WinForm.WFNew.LabelItem lblInfo;
        private Controls.WinForm.WFNew.BaseButtonItem btnExpandAll;
        private Controls.WinForm.WFNew.BaseButtonItem btnCollapseAll;
        private Controls.WinForm.WFNew.View.NodeViewItemTree nodeViewItemTree1;
    }
}
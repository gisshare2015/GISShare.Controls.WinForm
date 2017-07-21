namespace GISShare.Controls.Plugin.WinForm.Demo
{
    partial class RibbonHostForm
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
            this.ribbonControl1 = new GISShare.Controls.WinForm.WFNew.RibbonControl();
            this.ribbonStatusBar1 = new GISShare.Controls.WinForm.WFNew.RibbonStatusBar();
            this.dockPanelManager1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager();
            this.documentArea1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DocumentArea();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextPopupManager1 = new GISShare.Controls.WinForm.WFNew.ContextPopupManager();
            this.documentArea1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonControl1.CanExchangeItem = false;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl1.ePNLayoutStyle = GISShare.Controls.WinForm.WFNew.PNLayoutStyle.eTail;
            this.ribbonControl1.eQuickAccessToolbarStyle = GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eHalfRound;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.LockHeight = true;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonControl1.ParentForm = this;
            this.ribbonControl1.Size = new System.Drawing.Size(673, 141);
            this.ribbonControl1.TabIndex = 0;
            this.ribbonControl1.Text = "[Demo]插件结构的文编编辑器[Demo]";
            this.ribbonControl1.UsingCloseTabButton = false;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ribbonStatusBar1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.ribbonStatusBar1.IsRestrictItems = true;
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 413);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Padding = new System.Windows.Forms.Padding(1);
            this.ribbonStatusBar1.RestrictItemsHeight = -1;
            this.ribbonStatusBar1.RestrictItemsWidth = -1;
            this.ribbonStatusBar1.ShowNomalState = true;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(673, 23);
            this.ribbonStatusBar1.TabIndex = 1;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            // 
            // dockPanelManager1
            // 
            this.dockPanelManager1.DocumentArea = this.documentArea1;
            this.dockPanelManager1.ParentForm = this;
            // 
            // documentArea1
            // 
            this.documentArea1.Controls.Add(this.richTextBox1);
            this.documentArea1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentArea1.Location = new System.Drawing.Point(0, 141);
            this.documentArea1.Name = "documentArea1";
            this.documentArea1.Padding = new System.Windows.Forms.Padding(0);
            this.documentArea1.Size = new System.Drawing.Size(673, 272);
            this.documentArea1.TabIndex = 3;
            this.documentArea1.Text = "DocumentArea";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(673, 272);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // RibbonHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 436);
            this.Controls.Add(this.documentArea1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "RibbonHostForm";
            this.RibbonControl = this.ribbonControl1;
            this.Text = "[Demo]插件结构的文编编辑器[Demo]";
            this.documentArea1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.RibbonControl ribbonControl1;
        private GISShare.Controls.WinForm.WFNew.RibbonStatusBar ribbonStatusBar1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DocumentArea documentArea1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager1;
        private GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager1;
    }
}


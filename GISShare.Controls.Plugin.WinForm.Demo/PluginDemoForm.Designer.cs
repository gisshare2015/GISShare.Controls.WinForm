namespace GISShare.Controls.Plugin.WinForm.Demo
{
    partial class PluginDemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginDemoForm));
            this.dbRibbonHostForm = new GISShare.Controls.WinForm.WFNew.DescriptionButton();
            this.dbDockBarHostTBForm = new GISShare.Controls.WinForm.WFNew.DescriptionButton();
            this.dbDockBarHostForm = new GISShare.Controls.WinForm.WFNew.DescriptionButton();
            this.SuspendLayout();
            // 
            // dbRibbonHostForm
            // 
            this.dbRibbonHostForm.BackColor = System.Drawing.Color.Transparent;
            this.dbRibbonHostForm.Description = "基于RibbonControl、RibbonStatus、ContextPopupManager和DockPanelManager控件解析的插件应用程序Demo（" +
    "这些控件/组件成员是可选的）";
            this.dbRibbonHostForm.DescriptionFont = new System.Drawing.Font("宋体", 9F);
            this.dbRibbonHostForm.DescriptionForeColor = System.Drawing.SystemColors.ControlText;
            this.dbRibbonHostForm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.dbRibbonHostForm.Image = null;
            this.dbRibbonHostForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbRibbonHostForm.Location = new System.Drawing.Point(12, 12);
            this.dbRibbonHostForm.Name = "dbRibbonHostForm";
            this.dbRibbonHostForm.Padding = new System.Windows.Forms.Padding(3);
            this.dbRibbonHostForm.Size = new System.Drawing.Size(339, 76);
            this.dbRibbonHostForm.TabIndex = 1;
            this.dbRibbonHostForm.Text = "RibbonHostForm";
            this.dbRibbonHostForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbRibbonHostForm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbRibbonHostForm_MouseClick);
            // 
            // dbDockBarHostTBForm
            // 
            this.dbDockBarHostTBForm.BackColor = System.Drawing.Color.Transparent;
            this.dbDockBarHostTBForm.Description = "基于TBForm、DockBarManager和DockPanelManager控件解析的插件应用程序Demo（这些窗体/组件成员是可选的）";
            this.dbDockBarHostTBForm.DescriptionFont = new System.Drawing.Font("宋体", 9F);
            this.dbDockBarHostTBForm.DescriptionForeColor = System.Drawing.SystemColors.ControlText;
            this.dbDockBarHostTBForm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.dbDockBarHostTBForm.Image = null;
            this.dbDockBarHostTBForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbDockBarHostTBForm.Location = new System.Drawing.Point(12, 171);
            this.dbDockBarHostTBForm.Name = "dbDockBarHostTBForm";
            this.dbDockBarHostTBForm.Padding = new System.Windows.Forms.Padding(3);
            this.dbDockBarHostTBForm.Size = new System.Drawing.Size(339, 71);
            this.dbDockBarHostTBForm.TabIndex = 2;
            this.dbDockBarHostTBForm.Text = "DockBarHostTBForm";
            this.dbDockBarHostTBForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbDockBarHostTBForm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbDockBarHostTBForm_MouseClick);
            // 
            // dbDockBarHostForm
            // 
            this.dbDockBarHostForm.BackColor = System.Drawing.Color.Transparent;
            this.dbDockBarHostForm.Description = "基于DockBarManager和DockPanelManager控件解析的插件应用程序Demo（这些组件成员是可选的）";
            this.dbDockBarHostForm.DescriptionFont = new System.Drawing.Font("宋体", 9F);
            this.dbDockBarHostForm.DescriptionForeColor = System.Drawing.SystemColors.ControlText;
            this.dbDockBarHostForm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.dbDockBarHostForm.Image = null;
            this.dbDockBarHostForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbDockBarHostForm.Location = new System.Drawing.Point(12, 94);
            this.dbDockBarHostForm.Name = "dbDockBarHostForm";
            this.dbDockBarHostForm.Padding = new System.Windows.Forms.Padding(3);
            this.dbDockBarHostForm.Size = new System.Drawing.Size(339, 71);
            this.dbDockBarHostForm.TabIndex = 3;
            this.dbDockBarHostForm.Text = "DockBarHostForm";
            this.dbDockBarHostForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbDockBarHostForm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbDockBarHostForm_MouseClick);
            // 
            // PluginDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 254);
            this.Controls.Add(this.dbDockBarHostForm);
            this.Controls.Add(this.dbDockBarHostTBForm);
            this.Controls.Add(this.dbRibbonHostForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMiddleCaptionText = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PluginDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.DescriptionButton dbRibbonHostForm;
        private GISShare.Controls.WinForm.WFNew.DescriptionButton dbDockBarHostTBForm;
        private GISShare.Controls.WinForm.WFNew.DescriptionButton dbDockBarHostForm;

    }
}
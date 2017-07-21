namespace GISShare.Controls.WinForm.Demo.WFNew
{
    partial class DemoOfCollapsableSplitPanelForm
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
            this.collapsableSplitPanel1 = new GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.collapsableSplitPanel2 = new GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.collapsableSplitPanel3 = new GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.collapsableSplitPanel1.SuspendLayout();
            this.collapsableSplitPanel2.SuspendLayout();
            this.collapsableSplitPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapsableSplitPanel1
            // 
            this.collapsableSplitPanel1.Controls.Add(this.richTextBox1);
            this.collapsableSplitPanel1.InternalMinWidth = 25;
            this.collapsableSplitPanel1.Location = new System.Drawing.Point(0, 0);
            this.collapsableSplitPanel1.Name = "collapsableSplitPanel1";
            this.collapsableSplitPanel1.OuterMinWidth = 50;
            this.collapsableSplitPanel1.Padding = new System.Windows.Forms.Padding(0);
            this.collapsableSplitPanel1.Size = new System.Drawing.Size(154, 374);
            this.collapsableSplitPanel1.SplitLineWidth = 6;
            this.collapsableSplitPanel1.TabIndex = 0;
            this.collapsableSplitPanel1.Text = "collapsableSplitPanel1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(148, 374);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "折叠分割面板";
            // 
            // collapsableSplitPanel2
            // 
            this.collapsableSplitPanel2.Controls.Add(this.richTextBox2);
            this.collapsableSplitPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsableSplitPanel2.eCollapseSplitPanelStyles = GISShare.Controls.WinForm.WFNew.CollapseSplitPanelStyles.CollapsablePanel;
            this.collapsableSplitPanel2.InternalMinWidth = 25;
            this.collapsableSplitPanel2.Location = new System.Drawing.Point(154, 226);
            this.collapsableSplitPanel2.Name = "collapsableSplitPanel2";
            this.collapsableSplitPanel2.OuterMinWidth = 50;
            this.collapsableSplitPanel2.Padding = new System.Windows.Forms.Padding(0);
            this.collapsableSplitPanel2.Size = new System.Drawing.Size(489, 148);
            this.collapsableSplitPanel2.SplitLineWidth = 6;
            this.collapsableSplitPanel2.SplitPanelDock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsableSplitPanel2.TabIndex = 1;
            this.collapsableSplitPanel2.Text = "collapsableSplitPanel2";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(0, 6);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(489, 142);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "折叠面板";
            // 
            // collapsableSplitPanel3
            // 
            this.collapsableSplitPanel3.Controls.Add(this.richTextBox3);
            this.collapsableSplitPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsableSplitPanel3.eCollapseSplitPanelStyles = GISShare.Controls.WinForm.WFNew.CollapseSplitPanelStyles.SplitPanel;
            this.collapsableSplitPanel3.InternalMinWidth = 25;
            this.collapsableSplitPanel3.Location = new System.Drawing.Point(491, 0);
            this.collapsableSplitPanel3.Name = "collapsableSplitPanel3";
            this.collapsableSplitPanel3.OuterMinWidth = 50;
            this.collapsableSplitPanel3.Padding = new System.Windows.Forms.Padding(0);
            this.collapsableSplitPanel3.Size = new System.Drawing.Size(152, 226);
            this.collapsableSplitPanel3.SplitLineWidth = 6;
            this.collapsableSplitPanel3.SplitPanelDock = System.Windows.Forms.DockStyle.Right;
            this.collapsableSplitPanel3.TabIndex = 2;
            this.collapsableSplitPanel3.Text = "collapsableSplitPanel3";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(6, 0);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(146, 226);
            this.richTextBox3.TabIndex = 0;
            this.richTextBox3.Text = "分割面板";
            // 
            // DemoOfCollapsableSplitPanelForm
            // 
            this.ClientSize = new System.Drawing.Size(643, 374);
            this.Controls.Add(this.collapsableSplitPanel3);
            this.Controls.Add(this.collapsableSplitPanel2);
            this.Controls.Add(this.collapsableSplitPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfCollapsableSplitPanelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CollapsableSplitPanel控件";
            this.collapsableSplitPanel1.ResumeLayout(false);
            this.collapsableSplitPanel2.ResumeLayout(false);
            this.collapsableSplitPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel collapsableSplitPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel collapsableSplitPanel2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private GISShare.Controls.WinForm.WFNew.CollapsableSplitPanel collapsableSplitPanel3;
        private System.Windows.Forms.RichTextBox richTextBox3;














    }
}
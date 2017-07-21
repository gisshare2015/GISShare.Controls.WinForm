namespace GISShare.Controls.Plugin.WinForm.Demo
{
    partial class DockBarHostTBForm
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
            this.dockBarManager1 = new GISShare.Controls.WinForm.DockBar.DockBarManager();
            this.dockBarDockAreaBottom1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaBottom();
            this.dockBarDockAreaLeft1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaLeft();
            this.dockBarDockAreaRight1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaRight();
            this.dockBarDockAreaTop1 = new GISShare.Controls.WinForm.DockBar.DockBarDockAreaTop();
            this.dockPanelManager1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager();
            this.documentArea1 = new GISShare.Controls.WinForm.WFNew.DockPanel.DocumentArea();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.documentArea1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockBarManager1
            // 
            this.dockBarManager1.DockBarDockAreaBottom = this.dockBarDockAreaBottom1;
            this.dockBarManager1.DockBarDockAreaLeft = this.dockBarDockAreaLeft1;
            this.dockBarManager1.DockBarDockAreaRight = this.dockBarDockAreaRight1;
            this.dockBarManager1.DockBarDockAreaTop = this.dockBarDockAreaTop1;
            this.dockBarManager1.MenuBar = null;
            this.dockBarManager1.ParentForm = this;
            this.dockBarManager1.ShowLargeImage = false;
            this.dockBarManager1.StatusBar = null;
            // 
            // dockBarDockAreaBottom1
            // 
            this.dockBarDockAreaBottom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBarDockAreaBottom1.Location = new System.Drawing.Point(0, 341);
            this.dockBarDockAreaBottom1.Name = "dockBarDockAreaBottom1";
            this.dockBarDockAreaBottom1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.dockBarDockAreaBottom1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dockBarDockAreaBottom1.Size = new System.Drawing.Size(574, 0);
            // 
            // dockBarDockAreaLeft1
            // 
            this.dockBarDockAreaLeft1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockBarDockAreaLeft1.Location = new System.Drawing.Point(0, 0);
            this.dockBarDockAreaLeft1.Name = "dockBarDockAreaLeft1";
            this.dockBarDockAreaLeft1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dockBarDockAreaLeft1.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dockBarDockAreaLeft1.Size = new System.Drawing.Size(0, 341);
            // 
            // dockBarDockAreaRight1
            // 
            this.dockBarDockAreaRight1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockBarDockAreaRight1.Location = new System.Drawing.Point(574, 0);
            this.dockBarDockAreaRight1.Name = "dockBarDockAreaRight1";
            this.dockBarDockAreaRight1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dockBarDockAreaRight1.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dockBarDockAreaRight1.Size = new System.Drawing.Size(0, 341);
            // 
            // dockBarDockAreaTop1
            // 
            this.dockBarDockAreaTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockBarDockAreaTop1.Location = new System.Drawing.Point(0, 0);
            this.dockBarDockAreaTop1.Name = "dockBarDockAreaTop1";
            this.dockBarDockAreaTop1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.dockBarDockAreaTop1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dockBarDockAreaTop1.Size = new System.Drawing.Size(574, 0);
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
            this.documentArea1.Location = new System.Drawing.Point(0, 0);
            this.documentArea1.Name = "documentArea1";
            this.documentArea1.Padding = new System.Windows.Forms.Padding(0);
            this.documentArea1.Size = new System.Drawing.Size(574, 341);
            this.documentArea1.TabIndex = 4;
            this.documentArea1.Text = "DocumentArea";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(574, 341);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // DockBarHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 341);
            this.Controls.Add(this.documentArea1);
            this.Controls.Add(this.dockBarDockAreaLeft1);
            this.Controls.Add(this.dockBarDockAreaRight1);
            this.Controls.Add(this.dockBarDockAreaTop1);
            this.Controls.Add(this.dockBarDockAreaBottom1);
            this.IsMiddleCaptionText = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DockBarHostForm";
            this.Text = "[Demo]插件结构的文编编辑器[Demo]";
            this.documentArea1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.WinForm.DockBar.DockBarManager dockBarManager1;
        private Controls.WinForm.DockBar.DockBarDockAreaBottom dockBarDockAreaBottom1;
        private Controls.WinForm.DockBar.DockBarDockAreaLeft dockBarDockAreaLeft1;
        private Controls.WinForm.DockBar.DockBarDockAreaRight dockBarDockAreaRight1;
        private Controls.WinForm.DockBar.DockBarDockAreaTop dockBarDockAreaTop1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DocumentArea documentArea1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager1;


    }
}
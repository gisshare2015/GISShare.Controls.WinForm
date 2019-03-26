namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfComboTreeForm
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
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
            GISShare.Controls.WinForm.WFNew.View.ImageNodeViewItem imageNodeViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ImageNodeViewItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfComboTreeForm));
            this.comboTree1 = new GISShare.Controls.WinForm.WFNew.ComboTreeItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // comboTree1
            // 
            this.comboTree1.DropDownHeight = 118;
            this.comboTree1.DropDownWidth = 266;
            this.comboTree1.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.comboTree1.Font = new System.Drawing.Font("宋体", 9F);
            this.comboTree1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboTree1.Location = new System.Drawing.Point(0, 0);
            this.comboTree1.LockHeight = true;
            this.comboTree1.Name = "comboTree1";
            nodeViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            nodeViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            nodeViewItem1.Name = null;
            nodeViewItem1.ShowNomalState = false;
            nodeViewItem1.Text = "简单节点视图（NodeViewItem）";
            nodeViewItem1.TitleBackgroundBegin = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            nodeViewItem1.TitleBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            nodeViewItem1.TitleBorder = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(179)))), ((int)(((byte)(185)))));
            imageNodeViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            imageNodeViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            imageNodeViewItem1.Image = ((System.Drawing.Image)(resources.GetObject("imageNodeViewItem1.Image")));
            imageNodeViewItem1.Name = null;
            imageNodeViewItem1.ShowNomalState = false;
            imageNodeViewItem1.Text = "图片节点视图（ImageNodeViewItem）";
            imageNodeViewItem1.TitleBackgroundBegin = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            imageNodeViewItem1.TitleBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            imageNodeViewItem1.TitleBorder = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(179)))), ((int)(((byte)(185)))));
            this.comboTree1.NodeViewItems.Add(nodeViewItem1);
            this.comboTree1.NodeViewItems.Add(imageNodeViewItem1);
            this.comboTree1.SelectedNode = null;
            this.comboTree1.ShowDropDownNum = 0;
            this.comboTree1.Size = new System.Drawing.Size(399, 20);
            this.comboTree1.Tag = null;
            this.comboTree1.Text = "";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.comboTree1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(399, 20);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfComboTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 272);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfComboTreeForm";
            this.ShowIcon = false;
            this.Text = "ComboTree控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ComboTreeItem comboTree1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}
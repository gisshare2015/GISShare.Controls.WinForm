namespace GISShare.Controls.WinForm.Demo.WinForm
{
    partial class DemoOfTreeViewXForm
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
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem1 = new GISShare.Controls.WinForm.TreeNodeItem("ButtonX");
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem2 = new GISShare.Controls.WinForm.TreeNodeItem("CheckBoxX");
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem3 = new GISShare.Controls.WinForm.TreeNodeItem("RadioButtonX");
            GISShare.Controls.WinForm.TitleTreeNodeItem titleTreeNodeItem1 = new GISShare.Controls.WinForm.TitleTreeNodeItem("LiuZhenHong控件", new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(treeNodeItem1)),
            ((System.Windows.Forms.TreeNode)(treeNodeItem2)),
            ((System.Windows.Forms.TreeNode)(treeNodeItem3))});
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem4 = new GISShare.Controls.WinForm.TreeNodeItem("Button");
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem5 = new GISShare.Controls.WinForm.TreeNodeItem("CheckBox");
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem6 = new GISShare.Controls.WinForm.TreeNodeItem("RadioButton");
            GISShare.Controls.WinForm.TitleTreeNodeItem titleTreeNodeItem2 = new GISShare.Controls.WinForm.TitleTreeNodeItem("所有Windows窗体", new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(treeNodeItem4)),
            ((System.Windows.Forms.TreeNode)(treeNodeItem5)),
            ((System.Windows.Forms.TreeNode)(treeNodeItem6))});
            GISShare.Controls.WinForm.TreeNodeItem treeNodeItem7 = new GISShare.Controls.WinForm.TreeNodeItem("没有可用控件");
            GISShare.Controls.WinForm.TitleTreeNodeItem titleTreeNodeItem3 = new GISShare.Controls.WinForm.TitleTreeNodeItem("常规", new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(treeNodeItem7))});
            this.treeViewX1 = new GISShare.Controls.WinForm.TreeViewX();
            this.chShowLines = new GISShare.Controls.WinForm.CheckBoxX();
            this.chShowPlusMinus = new GISShare.Controls.WinForm.CheckBoxX();
            this.chShowRootLines = new GISShare.Controls.WinForm.CheckBoxX();
            this.chAutoMouseMoveSelected = new GISShare.Controls.WinForm.CheckBoxX();
            this.chShowGripRegion = new GISShare.Controls.WinForm.CheckBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNodeRegionStyle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // treeViewX1
            // 
            this.treeViewX1.AutoMouseMoveSeleced = true;
            this.treeViewX1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.ePlusMinusToRight;
            this.treeViewX1.ItemHeight = 20;
            this.treeViewX1.Location = new System.Drawing.Point(12, 12);
            this.treeViewX1.Name = "treeViewX1";
            treeNodeItem1.Name = "TreeNodeItem15";
            treeNodeItem1.Text = "ButtonX";
            treeNodeItem2.Name = "TreeNodeItem16";
            treeNodeItem2.Text = "CheckBoxX";
            treeNodeItem3.Name = "TreeNodeItem17";
            treeNodeItem3.Text = "RadioButtonX";
            titleTreeNodeItem1.Name = "TitleTreeNodeItem0";
            titleTreeNodeItem1.Text = "LiuZhenHong控件";
            titleTreeNodeItem1.TitleBackgroundBegin = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            titleTreeNodeItem1.TitleBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            titleTreeNodeItem1.TitleBorder = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(179)))), ((int)(((byte)(185)))));
            treeNodeItem4.Name = "TreeNodeItem18";
            treeNodeItem4.Text = "Button";
            treeNodeItem5.Name = "TreeNodeItem19";
            treeNodeItem5.Text = "CheckBox";
            treeNodeItem6.Name = "TreeNodeItem20";
            treeNodeItem6.Text = "RadioButton";
            titleTreeNodeItem2.Name = "TitleTreeNodeItem2";
            titleTreeNodeItem2.Text = "所有Windows窗体";
            titleTreeNodeItem2.TitleBackgroundBegin = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            titleTreeNodeItem2.TitleBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            titleTreeNodeItem2.TitleBorder = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(179)))), ((int)(((byte)(185)))));
            treeNodeItem7.Name = "TreeNodeItem21";
            treeNodeItem7.Text = "没有可用控件";
            titleTreeNodeItem3.Name = "TitleTreeNodeItem14";
            titleTreeNodeItem3.Text = "常规";
            titleTreeNodeItem3.TitleBackgroundBegin = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(251)))), ((int)(((byte)(247)))));
            titleTreeNodeItem3.TitleBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            titleTreeNodeItem3.TitleBorder = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(179)))), ((int)(((byte)(185)))));
            this.treeViewX1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            titleTreeNodeItem1,
            titleTreeNodeItem2,
            titleTreeNodeItem3});
            this.treeViewX1.ShowLines = false;
            this.treeViewX1.Size = new System.Drawing.Size(192, 370);
            this.treeViewX1.TabIndex = 0;
            // 
            // chShowLines
            // 
            this.chShowLines.AutoSize = true;
            this.chShowLines.Location = new System.Drawing.Point(210, 12);
            this.chShowLines.Name = "chShowLines";
            this.chShowLines.Size = new System.Drawing.Size(78, 16);
            this.chShowLines.TabIndex = 1;
            this.chShowLines.Text = "ShowLines";
            this.chShowLines.UseVisualStyleBackColor = true;
            this.chShowLines.VOffset = 2;
            this.chShowLines.CheckedChanged += new System.EventHandler(this.chShowLines_CheckedChanged);
            // 
            // chShowPlusMinus
            // 
            this.chShowPlusMinus.AutoSize = true;
            this.chShowPlusMinus.Checked = true;
            this.chShowPlusMinus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chShowPlusMinus.Location = new System.Drawing.Point(210, 56);
            this.chShowPlusMinus.Name = "chShowPlusMinus";
            this.chShowPlusMinus.Size = new System.Drawing.Size(102, 16);
            this.chShowPlusMinus.TabIndex = 2;
            this.chShowPlusMinus.Text = "ShowPlusMinus";
            this.chShowPlusMinus.UseVisualStyleBackColor = true;
            this.chShowPlusMinus.VOffset = 2;
            this.chShowPlusMinus.CheckedChanged += new System.EventHandler(this.chShowPlusMinus_CheckedChanged);
            // 
            // chShowRootLines
            // 
            this.chShowRootLines.AutoSize = true;
            this.chShowRootLines.Checked = true;
            this.chShowRootLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chShowRootLines.Location = new System.Drawing.Point(210, 34);
            this.chShowRootLines.Name = "chShowRootLines";
            this.chShowRootLines.Size = new System.Drawing.Size(102, 16);
            this.chShowRootLines.TabIndex = 3;
            this.chShowRootLines.Text = "ShowRootLines";
            this.chShowRootLines.UseVisualStyleBackColor = true;
            this.chShowRootLines.VOffset = 2;
            this.chShowRootLines.CheckedChanged += new System.EventHandler(this.chShowRootLines_CheckedChanged);
            // 
            // chAutoMouseMoveSelected
            // 
            this.chAutoMouseMoveSelected.AutoSize = true;
            this.chAutoMouseMoveSelected.Checked = true;
            this.chAutoMouseMoveSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chAutoMouseMoveSelected.Location = new System.Drawing.Point(210, 100);
            this.chAutoMouseMoveSelected.Name = "chAutoMouseMoveSelected";
            this.chAutoMouseMoveSelected.Size = new System.Drawing.Size(150, 16);
            this.chAutoMouseMoveSelected.TabIndex = 4;
            this.chAutoMouseMoveSelected.Text = "AutoMouseMoveSelected";
            this.chAutoMouseMoveSelected.UseVisualStyleBackColor = true;
            this.chAutoMouseMoveSelected.VOffset = 2;
            this.chAutoMouseMoveSelected.CheckedChanged += new System.EventHandler(this.chAutoMouseMoveSelected_CheckedChanged);
            // 
            // chShowGripRegion
            // 
            this.chShowGripRegion.AutoSize = true;
            this.chShowGripRegion.Location = new System.Drawing.Point(210, 78);
            this.chShowGripRegion.Name = "chShowGripRegion";
            this.chShowGripRegion.Size = new System.Drawing.Size(108, 16);
            this.chShowGripRegion.TabIndex = 5;
            this.chShowGripRegion.Text = "ShowGripRegion";
            this.chShowGripRegion.UseVisualStyleBackColor = true;
            this.chShowGripRegion.VOffset = 2;
            this.chShowGripRegion.CheckedChanged += new System.EventHandler(this.chShowGripRegion_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "eNodeRegionStyle：";
            // 
            // cbNodeRegionStyle
            // 
            this.cbNodeRegionStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNodeRegionStyle.FormattingEnabled = true;
            this.cbNodeRegionStyle.Items.AddRange(new object[] {
            "eRow",
            "ePlusMinusToRight",
            "eGripToRight",
            "eTextToRight",
            "eText"});
            this.cbNodeRegionStyle.Location = new System.Drawing.Point(210, 362);
            this.cbNodeRegionStyle.Name = "cbNodeRegionStyle";
            this.cbNodeRegionStyle.Size = new System.Drawing.Size(150, 20);
            this.cbNodeRegionStyle.TabIndex = 7;
            this.cbNodeRegionStyle.SelectedIndexChanged += new System.EventHandler(this.cbNodeRegionStyle_SelectedIndexChanged);
            // 
            // DemoOfTreeViewXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 394);
            this.Controls.Add(this.cbNodeRegionStyle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chShowGripRegion);
            this.Controls.Add(this.chAutoMouseMoveSelected);
            this.Controls.Add(this.chShowRootLines);
            this.Controls.Add(this.chShowPlusMinus);
            this.Controls.Add(this.chShowLines);
            this.Controls.Add(this.treeViewX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoOfTreeViewXForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TreeViewX控件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GISShare.Controls.WinForm.TreeViewX treeViewX1;
        private GISShare.Controls.WinForm.CheckBoxX chShowLines;
        private GISShare.Controls.WinForm.CheckBoxX chShowPlusMinus;
        private GISShare.Controls.WinForm.CheckBoxX chShowRootLines;
        private GISShare.Controls.WinForm.CheckBoxX chAutoMouseMoveSelected;
        private GISShare.Controls.WinForm.CheckBoxX chShowGripRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNodeRegionStyle;
    }
}
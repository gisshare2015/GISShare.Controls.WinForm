using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockPanelCustomizeForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm//Form
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
            this.tabControl1 = new GISShare.Controls.WinForm.WFNew.TabControl();
            this.tabPage_BasePanel = new GISShare.Controls.WinForm.WFNew.TabPage();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.checkedListBox_BasePanel = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            this.tabPage_DockPanel = new GISShare.Controls.WinForm.WFNew.TabPage();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.checkedListBox_DockPanel = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            this.tabPage_PanelTree = new GISShare.Controls.WinForm.WFNew.TabPage();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.treeView_PanelTree = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            this.button_Cancel = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
            this.tabControl1.SuspendLayout();
            this.tabPage_BasePanel.SuspendLayout();
            this.tabPage_DockPanel.SuspendLayout();
            this.tabPage_PanelTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AutoVisibleTabButton = false;
            this.tabControl1.CanExchangeItem = false;
            this.tabControl1.Controls.Add(this.tabPage_BasePanel);
            this.tabControl1.Controls.Add(this.tabPage_DockPanel);
            this.tabControl1.Controls.Add(this.tabPage_PanelTree);
            this.tabControl1.Location = new System.Drawing.Point(14, 18);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Windows.Forms.Padding(0);
            this.tabControl1.ShowOutLine = true;
            this.tabControl1.Size = new System.Drawing.Size(663, 474);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabPages.Add(this.tabPage_BasePanel);
            this.tabControl1.TabPages.Add(this.tabPage_DockPanel);
            this.tabControl1.TabPages.Add(this.tabPage_PanelTree);
            this.tabControl1.TabPageSelectedIndex = 1;
            this.tabControl1.UsingCloseTabButton = false;
            // 
            // tabPage_BasePanel
            // 
            this.tabPage_BasePanel.BackColor = System.Drawing.Color.White;
            this.tabPage_BasePanel.Controls.Add(this.baseItemHost2);
            this.tabPage_BasePanel.Image = null;
            this.tabPage_BasePanel.Location = new System.Drawing.Point(1, 22);
            this.tabPage_BasePanel.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_BasePanel.Name = "tabPage_BasePanel";
            this.tabPage_BasePanel.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage_BasePanel.Size = new System.Drawing.Size(661, 451);
            this.tabPage_BasePanel.TabIndex = 0;
            this.tabPage_BasePanel.Text = "BasePanel";
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.BaseItemObject = this.checkedListBox_BasePanel;
            this.baseItemHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost2.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(661, 451);
            this.baseItemHost2.TabIndex = 0;
            // 
            // checkedListBox_BasePanel
            // 
            this.checkedListBox_BasePanel.AutoGetFocus = true;
            this.checkedListBox_BasePanel.BackgroundColor = System.Drawing.SystemColors.Window;
            this.checkedListBox_BasePanel.Font = new System.Drawing.Font("宋体", 9F);
            this.checkedListBox_BasePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkedListBox_BasePanel.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox_BasePanel.Name = "checkedListBox_BasePanel";
            this.checkedListBox_BasePanel.Size = new System.Drawing.Size(661, 451);
            this.checkedListBox_BasePanel.Tag = null;
            this.checkedListBox_BasePanel.Text = null;
            // 
            // tabPage_DockPanel
            // 
            this.tabPage_DockPanel.Controls.Add(this.baseItemHost3);
            this.tabPage_DockPanel.Image = null;
            this.tabPage_DockPanel.Location = new System.Drawing.Point(1, 22);
            this.tabPage_DockPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_DockPanel.Name = "tabPage_DockPanel";
            this.tabPage_DockPanel.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage_DockPanel.ShowOutLine = true;
            this.tabPage_DockPanel.Size = new System.Drawing.Size(661, 451);
            this.tabPage_DockPanel.TabIndex = 1;
            this.tabPage_DockPanel.Text = "DockPanel";
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.BackColor = System.Drawing.Color.White;
            this.baseItemHost3.BaseItemObject = this.checkedListBox_DockPanel;
            this.baseItemHost3.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(660, 440);
            this.baseItemHost3.TabIndex = 0;
            // 
            // checkedListBox_DockPanel
            // 
            this.checkedListBox_DockPanel.AutoGetFocus = true;
            this.checkedListBox_DockPanel.BackgroundColor = System.Drawing.SystemColors.Window;
            this.checkedListBox_DockPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.checkedListBox_DockPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkedListBox_DockPanel.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox_DockPanel.Name = "checkedListBox_DockPanel";
            this.checkedListBox_DockPanel.Size = new System.Drawing.Size(660, 440);
            this.checkedListBox_DockPanel.Tag = null;
            this.checkedListBox_DockPanel.Text = null;
            // 
            // tabPage_PanelTree
            // 
            this.tabPage_PanelTree.BackColor = System.Drawing.Color.White;
            this.tabPage_PanelTree.Controls.Add(this.baseItemHost1);
            this.tabPage_PanelTree.Image = null;
            this.tabPage_PanelTree.Location = new System.Drawing.Point(1, 22);
            this.tabPage_PanelTree.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_PanelTree.Name = "tabPage_PanelTree";
            this.tabPage_PanelTree.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage_PanelTree.ShowOutLine = true;
            this.tabPage_PanelTree.Size = new System.Drawing.Size(661, 451);
            this.tabPage_PanelTree.TabIndex = 2;
            this.tabPage_PanelTree.Text = "PanelTree";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.treeView_PanelTree;
            this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseItemHost1.Location = new System.Drawing.Point(0, 0);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(661, 451);
            this.baseItemHost1.TabIndex = 0;
            // 
            // treeView_PanelTree
            // 
            this.treeView_PanelTree.AutoGetFocus = true;
            this.treeView_PanelTree.BackgroundColor = System.Drawing.SystemColors.Window;
            this.treeView_PanelTree.Font = new System.Drawing.Font("宋体", 9F);
            this.treeView_PanelTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.treeView_PanelTree.Location = new System.Drawing.Point(0, 0);
            this.treeView_PanelTree.Name = "treeView_PanelTree";
            this.treeView_PanelTree.SelectedNode = null;
            this.treeView_PanelTree.Size = new System.Drawing.Size(661, 451);
            this.treeView_PanelTree.Tag = null;
            this.treeView_PanelTree.Text = null;
            // 
            // button_Cancel
            // 
            this.button_Cancel.AutoPlanTextRectangle = false;
            this.button_Cancel.Font = new System.Drawing.Font("宋体", 9F);
            this.button_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Cancel.Image = null;
            this.button_Cancel.Location = new System.Drawing.Point(361, 332);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 25);
            this.button_Cancel.Tag = null;
            this.button_Cancel.Text = "Close";
            // 
            // DockPanelCustomizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 544);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DockPanelCustomizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customize";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_BasePanel.ResumeLayout(false);
            this.tabPage_DockPanel.ResumeLayout(false);
            this.tabPage_PanelTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.TabControl tabControl1;
        private GISShare.Controls.WinForm.WFNew.TabPage tabPage_BasePanel;
        private GISShare.Controls.WinForm.WFNew.TabPage tabPage_DockPanel;
        private GISShare.Controls.WinForm.WFNew.TabPage tabPage_PanelTree;
        private GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem checkedListBox_BasePanel;
        private GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem checkedListBox_DockPanel;
        private GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem treeView_PanelTree;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private GISShare.Controls.WinForm.WFNew.BaseButtonItem button_Cancel;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost3;

        //
        //
        //

        private DockPanelManager m_DockPanelManager = null;
        private Dictionary<int, BasePanel> m_BasePanelDictionary = new Dictionary<int, BasePanel>();

        public DockPanelCustomizeForm(DockPanelManager dockPanelManager)
        {
            InitializeComponent();
            //
            //
            //
            #region Language
            this.Text = GISShare.Controls.WinForm.WFNew.DockPanel.Language.LanguageStrategy.CustomizeFormTitle;
            this.button_Cancel.Text = GISShare.Controls.WinForm.WFNew.DockPanel.Language.LanguageStrategy.CustomizeForm_ButtonCancelText;
            this.tabPage_BasePanel.Text = GISShare.Controls.WinForm.WFNew.DockPanel.Language.LanguageStrategy.CustomizeForm_TabPageBasePanelText;
            this.tabPage_DockPanel.Text = GISShare.Controls.WinForm.WFNew.DockPanel.Language.LanguageStrategy.CustomizeForm_TabPageDockPanelText;
            this.tabPage_PanelTree.Text = GISShare.Controls.WinForm.WFNew.DockPanel.Language.LanguageStrategy.CustomizeForm_TabPagePanelTreeText;
            #endregion
            //
            //
            //
            this.m_DockPanelManager = dockPanelManager;
            this.SetDockPanelCustomizeForm();
            //
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
        }
        private void SetDockPanelCustomizeForm()
        {
            this.RestCheckedListBoxBasePanel();
            //
            //
            //
            this.checkedListBox_DockPanel.ViewItems.Clear();
            foreach (DockPanel one in this.m_DockPanelManager.DockPanels)
            {
                if (one.BasePanels.Count < 1) continue;
                //
                ImageCheckBoxItem ribbonImageCheckBoxItem = new ImageCheckBoxItem();
                ribbonImageCheckBoxItem.Name = "DockPanel";
                ribbonImageCheckBoxItem.Text = one.Text;
                ribbonImageCheckBoxItem.Image = one.Image;
                ribbonImageCheckBoxItem.CDSpace = 3;
                ribbonImageCheckBoxItem.Checked = one.ePanelNodeState == PanelNodeState.eShow;
                ribbonImageCheckBoxItem.ImageAlign = ContentAlignment.MiddleLeft;
                ribbonImageCheckBoxItem.TextAlign = ContentAlignment.MiddleLeft;
                ribbonImageCheckBoxItem.eHAlignmentStyle = HAlignmentStyle.eStretch;
                ribbonImageCheckBoxItem.eVAlignmentStyle = VAlignmentStyle.eStretch;
                ribbonImageCheckBoxItem.Tag = one;
                ribbonImageCheckBoxItem.CheckedChanged += new EventHandler(ImageCheckBoxItem_CheckedChanged);
                this.checkedListBox_DockPanel.ViewItems.Add(new View.SuperViewItem(ribbonImageCheckBoxItem));
            }
            //
            //
            //
            this.treeView_PanelTree.NodeViewItems.Clear();
            View.NodeViewItem treeNode_Form = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.MainFormText + Language.LanguageStrategy.SquareBrackets_Right + this.m_DockPanelManager.ParentForm.Text);
            treeNode_Form.ShowNomalState = true;
            treeNode_Form.Expand();
            this.treeView_PanelTree.NodeViewItems.Add(treeNode_Form);
            //
            View.NodeViewItem treeNode_DocumentArea = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.DocumentAreaText + Language.LanguageStrategy.SquareBrackets_Right);
            treeNode_DocumentArea.ShowNomalState = true;
            //treeNode_DocumentArea.Expand();
            treeNode_Form.NodeViewItems.Add(treeNode_DocumentArea);
            if (this.m_DockPanelManager.DocumentArea != null && 
                this.m_DockPanelManager.DocumentArea is DocumentDockArea)
            {
                View.NodeViewItem treeNode = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.RootNodeText + Language.LanguageStrategy.SquareBrackets_Right + this.m_DockPanelManager.DocumentArea.Text + Language.LanguageStrategy.RoundBrackets_Left + this.m_DockPanelManager.DocumentArea.Name + Language.LanguageStrategy.RoundBrackets_Right);
                treeNode_DocumentArea.NodeViewItems.Add(treeNode);
                this.SetNodeInfo_DG(((DocumentDockArea)this.m_DockPanelManager.DocumentArea).ChildNode, treeNode);
            }
            //
            View.NodeViewItem treeNode_DockPanelDockArea = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.DockAreaText + Language.LanguageStrategy.SquareBrackets_Right);
            treeNode_DockPanelDockArea.ShowNomalState = true;
            //treeNode_DockPanelDockArea.Expand();
            treeNode_Form.NodeViewItems.Add(treeNode_DockPanelDockArea);
            foreach (DockPanelDockArea one in this.m_DockPanelManager.DockPanelDockAreas)
            {
                View.NodeViewItem treeNode = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.RootNodeText + Language.LanguageStrategy.SquareBrackets_Right + one.Text + Language.LanguageStrategy.RoundBrackets_Left + one.Name + Language.LanguageStrategy.RoundBrackets_Right);
                treeNode_DockPanelDockArea.NodeViewItems.Add(treeNode);
                this.SetNodeInfo_DG(one.ChildNode, treeNode);
            }
            //
            View.NodeViewItem treeNode_DockPanelFloatForm = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.FloatFormText + Language.LanguageStrategy.SquareBrackets_Right);
            treeNode_DockPanelFloatForm.ShowNomalState = true;
            //treeNode_DockPanelFloatForm.Expand();
            treeNode_Form.NodeViewItems.Add(treeNode_DockPanelFloatForm);
            foreach (DockPanelFloatForm one in this.m_DockPanelManager.DockPanelFloatForms)
            {
                View.NodeViewItem treeNode = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.RootNodeText + Language.LanguageStrategy.SquareBrackets_Right + one.Text + Language.LanguageStrategy.RoundBrackets_Left + one.Name + Language.LanguageStrategy.RoundBrackets_Right);
                treeNode_DockPanelFloatForm.NodeViewItems.Add(treeNode);
                this.SetNodeInfo_DG(one.ChildNode, treeNode);
            }
        }
        private void RestCheckedListBoxBasePanel()
        {
            this.m_Stop_CheckedListBoxBasePanelItemCheck = true;
            //
            this.checkedListBox_BasePanel.ViewItems.Clear();
            foreach (BasePanel one in this.m_DockPanelManager.BasePanels)
            {
                ImageCheckBoxItem ribbonImageCheckBoxItem = new ImageCheckBoxItem();
                ribbonImageCheckBoxItem.Name = "BasePanel";
                ribbonImageCheckBoxItem.Text = one.Text;
                ribbonImageCheckBoxItem.Image = one.Image;
                ribbonImageCheckBoxItem.CDSpace = 3;
                ribbonImageCheckBoxItem.Checked = one.ePanelNodeState == PanelNodeState.eShow;
                ribbonImageCheckBoxItem.ImageAlign = ContentAlignment.MiddleLeft;
                ribbonImageCheckBoxItem.TextAlign = ContentAlignment.MiddleLeft;
                ribbonImageCheckBoxItem.eHAlignmentStyle = HAlignmentStyle.eStretch;
                ribbonImageCheckBoxItem.eVAlignmentStyle = VAlignmentStyle.eStretch;
                ribbonImageCheckBoxItem.Tag = one;
                ribbonImageCheckBoxItem.CheckedChanged += new EventHandler(ImageCheckBoxItem_CheckedChanged);
                this.checkedListBox_BasePanel.ViewItems.Add(new View.SuperViewItem(ribbonImageCheckBoxItem));
            }
            //
            this.m_Stop_CheckedListBoxBasePanelItemCheck = false;
        }
        private bool m_Stop_CheckedListBoxBasePanelItemCheck = false;
        private void ImageCheckBoxItem_CheckedChanged(object sender, EventArgs e)
        {
            ImageCheckBoxItem ribbonImageCheckBoxItem = sender as ImageCheckBoxItem;
            if (ribbonImageCheckBoxItem == null || ribbonImageCheckBoxItem.Tag == null) return;
            if (ribbonImageCheckBoxItem.Name == "BasePanel")
            {
                if (this.m_Stop_CheckedListBoxBasePanelItemCheck) return;
                //
                BasePanel basePanel = ribbonImageCheckBoxItem.Tag as BasePanel;
                if (basePanel == null) return;
                basePanel.VisibleEx = ribbonImageCheckBoxItem.Checked;
                //
                this.RestCheckedListBoxBasePanel();
            }
            else if (ribbonImageCheckBoxItem.Name == "DockPanel")
            {
                DockPanel dockPanel = ribbonImageCheckBoxItem.Tag as DockPanel;
                if (dockPanel == null) return;
                dockPanel.VisibleEx = ribbonImageCheckBoxItem.Checked;
                //
                this.RestCheckedListBoxBasePanel();
            }
        }
        private void SetNodeInfo_DG(IBaseNode pBaseNode, View.NodeViewItem treeNode)
        {
            if (pBaseNode == null) return;
            //
            switch(pBaseNode.eNodeStyle)
            {
                case NodeStyle.eBinaryNode:
                    View.NodeViewItem treeNode1 = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.BinaryNodeText + Language.LanguageStrategy.SquareBrackets_Right + pBaseNode.Text + Language.LanguageStrategy.RoundBrackets_Left + pBaseNode.Name + Language.LanguageStrategy.RoundBrackets_Right);
                    treeNode.NodeViewItems.Add(treeNode1);
                    IBinaryNode pBinaryNode = pBaseNode as IBinaryNode;
                    if (pBinaryNode != null)
                    {
                        this.SetNodeInfo_DG(pBinaryNode.LeftNode, treeNode1);
                        this.SetNodeInfo_DG(pBinaryNode.RightNode, treeNode1);
                    }
                    break;
                case NodeStyle.eMultipleNode:
                    View.NodeViewItem treeNode2 = new View.NodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.MultipleNodeText + Language.LanguageStrategy.SquareBrackets_Right + pBaseNode.Text + Language.LanguageStrategy.RoundBrackets_Left + pBaseNode.Name + Language.LanguageStrategy.RoundBrackets_Right);
                    treeNode.NodeViewItems.Add(treeNode2);
                    IMultipleNode pMultipleNode = pBaseNode as IMultipleNode;
                    if (pMultipleNode != null)
                    {
                        foreach (IBaseNode one in pMultipleNode.ChildNodes)
                        {
                            this.SetNodeInfo_DG(one, treeNode2);
                        }
                    }
                    break;
                case NodeStyle.eBottomNode:
                    View.ImageNodeViewItem treeNode3 = new View.ImageNodeViewItem(Language.LanguageStrategy.SquareBrackets_Left + Language.LanguageStrategy.BottomNodeText + Language.LanguageStrategy.SquareBrackets_Right + pBaseNode.Text + Language.LanguageStrategy.RoundBrackets_Left + pBaseNode.Name + Language.LanguageStrategy.RoundBrackets_Right);
                    BasePanel basePanel = pBaseNode as BasePanel;
                    if (basePanel != null) treeNode3.Image = basePanel.Image;
                    treeNode.NodeViewItems.Add(treeNode3);
                    break;
                default:
                    break;
            }
        }


        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
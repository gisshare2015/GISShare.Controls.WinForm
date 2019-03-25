using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class NodeViewItemCollectionEditor : CollectionEditor
    {
        public NodeViewItemCollectionEditor()
            : base(typeof(NodeViewItemCollection))
        {
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(NodeViewItem),
                typeof(ImageNodeViewItem),
                typeof(ResizeNodeViewItem),
                typeof(ResizeImageNodeViewItem),
                //
                typeof(RowNodeViewItem),
                typeof(VRowNodeViewItem),
                typeof(RowImageNodeViewItem),
                typeof(VRowImageNodeViewItem),
                typeof(FlexibleRowNodeViewItem),
                typeof(FlexibleVRowNodeViewItem),
                typeof(FlexibleRowImageNodeViewItem),
                typeof(FlexibleVRowImageNodeViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(NodeViewItem);
        }

        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            return new NodeViewItemCollectionForm(this);
        }
                
        //
        //
        //

        private class NodeViewItemCollectionForm : CollectionEditor.CollectionForm
        {
            private static object STATIC_NextNodeKey = new object();

            private NodeViewItemCollectionEditor m_Editor;
            private int m_IntialNextNode;
            private int m_NextNode;
            private NodeViewItem m_CurrentNode;

            // Fields
            private System.Windows.Forms.Label lblLeft;
            private System.Windows.Forms.Label lblRight;
            private System.Windows.Forms.Button btnMoveDown;
            private System.Windows.Forms.Button btnMoveUp;
            private System.Windows.Forms.Button btnOk;
            private System.Windows.Forms.Button btnCancel;
            private System.Windows.Forms.Button btnDelete;
            private GISShare.Controls.WinForm.VsButton btnAddRoot;
            private GISShare.Controls.WinForm.VsButton btnAddChild;
            private NodeViewItemTreeItem treeView1;
            private BaseItemHost baseItemHost1;
            private GISShare.Controls.WinForm.VsPropertyGrid vsPropertyGrid1;
            private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
            private System.Windows.Forms.TableLayoutPanel tlpAddNode;
            private System.Windows.Forms.TableLayoutPanel tlpOkAndCancel;
            private System.Windows.Forms.TableLayoutPanel tlpMoveDelete;
            private System.Windows.Forms.TableLayoutPanel tlpAll;

            // Methods
            public NodeViewItemCollectionForm(NodeViewItemCollectionEditor editor)
                : base(editor)
            {
                this.m_Editor = editor;
                this.InitializeComponent();
                this.HookEvents();
                this.m_IntialNextNode = this.NextNode;
                this.SetButtonsState();
            }

            private void InitializeComponent()
            {
                this.tlpOkAndCancel = new TableLayoutPanel();
                this.btnOk = new System.Windows.Forms.Button();
                this.btnCancel = new System.Windows.Forms.Button();
                this.tlpAddNode = new System.Windows.Forms.TableLayoutPanel();
                this.btnAddRoot = new GISShare.Controls.WinForm.VsButton(this.m_Editor.CreateNewItemTypes().Length > 1);
                this.btnAddChild = new GISShare.Controls.WinForm.VsButton(this.m_Editor.CreateNewItemTypes().Length > 1);
                this.btnDelete = new System.Windows.Forms.Button();
                this.btnMoveDown = new System.Windows.Forms.Button();
                this.btnMoveUp = new System.Windows.Forms.Button();
                this.vsPropertyGrid1 = new GISShare.Controls.WinForm.VsPropertyGrid(base.Context);
                this.lblRight = new System.Windows.Forms.Label();
                this.treeView1 = new NodeViewItemTreeItem();
                this.baseItemHost1 = new BaseItemHost();
                this.lblLeft = new System.Windows.Forms.Label();
                this.tlpAll = new System.Windows.Forms.TableLayoutPanel();
                this.tlpMoveDelete = new System.Windows.Forms.TableLayoutPanel();
                this.tlpOkAndCancel.SuspendLayout();
                this.tlpAddNode.SuspendLayout();
                this.tlpAll.SuspendLayout();
                this.tlpMoveDelete.SuspendLayout();
                base.SuspendLayout();
                //
                // contextMenuStrip1
                //
                this.contextMenuStrip1 = new ContextMenuStrip();
                this.contextMenuStrip1.Name = "contextMenuStrip1";
                this.contextMenuStrip1.Text = "contextMenuStrip1";
                // 
                // okCancelPanel
                // 
                this.tlpOkAndCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.59447F));
                this.tlpOkAndCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.40553F));
                this.tlpOkAndCancel.Controls.Add(this.btnOk, 0, 0);
                this.tlpOkAndCancel.Controls.Add(this.btnCancel, 1, 0);
                this.tlpOkAndCancel.Dock = System.Windows.Forms.DockStyle.Right;
                this.tlpOkAndCancel.Location = new System.Drawing.Point(412, 300);
                this.tlpOkAndCancel.Margin = new System.Windows.Forms.Padding(3, 3, 13, 13);
                this.tlpOkAndCancel.Name = "tlpOkAndCancel";
                this.tlpOkAndCancel.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpOkAndCancel.Size = new System.Drawing.Size(217, 23);
                this.tlpOkAndCancel.TabIndex = 6;
                // 
                // btnOk
                // 
                this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
                this.btnOk.Location = new System.Drawing.Point(60, 0);
                this.btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                this.btnOk.Name = "btnOk";
                this.btnOk.Size = new System.Drawing.Size(75, 23);
                this.btnOk.TabIndex = 0;
                this.btnOk.Text = "确定";
                // 
                // btnCancel
                // 
                this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
                this.btnCancel.Location = new System.Drawing.Point(142, 0);
                this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
                this.btnCancel.Name = "btnCancel";
                this.btnCancel.Size = new System.Drawing.Size(75, 23);
                this.btnCancel.TabIndex = 1;
                this.btnCancel.Text = "取消";
                // 
                // tlpAddNode
                // 
                this.tlpAddNode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tlpAddNode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tlpAddNode.Controls.Add(this.btnAddChild, 1, 0);
                this.tlpAddNode.Controls.Add(this.btnAddRoot, 0, 0);
                this.tlpAddNode.Dock = System.Windows.Forms.DockStyle.Fill;
                this.tlpAddNode.Location = new System.Drawing.Point(13, 265);
                this.tlpAddNode.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
                this.tlpAddNode.Name = "tlpAddNode";
                this.tlpAddNode.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpAddNode.Size = new System.Drawing.Size(283, 29);
                this.tlpAddNode.TabIndex = 5;
                // 
                // btnAddRoot
                // 
                this.btnAddRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                this.btnAddRoot.Location = new System.Drawing.Point(154, 3);
                this.btnAddRoot.Margin = new System.Windows.Forms.Padding(3, 3, 13, 3);
                this.btnAddRoot.Name = "btnAddRoot";
                this.btnAddRoot.Size = new System.Drawing.Size(126, 23);
                this.btnAddRoot.TabIndex = 0;
                this.btnAddRoot.Text = "添加根（&R）";
                // 
                // btnAddChild
                // 
                this.btnAddChild.Dock = System.Windows.Forms.DockStyle.Fill;
                this.btnAddChild.Location = new System.Drawing.Point(3, 3);
                this.btnAddChild.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
                this.btnAddChild.Name = "btnAddChild";
                this.btnAddChild.Size = new System.Drawing.Size(125, 23);
                this.btnAddChild.TabIndex = 1;
                this.btnAddChild.Text = "添加子级（&C）";
                // 
                // btnDelete
                // 
                this.btnDelete.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.Image.Delete.bmp"));
                this.btnDelete.Location = new System.Drawing.Point(0, 53);
                this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
                this.btnDelete.Name = "btnDelete";
                this.btnDelete.Size = new System.Drawing.Size(28, 23);
                this.btnDelete.TabIndex = 1;
                // 
                // btnMoveDown
                // 
                this.btnMoveDown.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.Image.MoveDown.bmp"));
                this.btnMoveDown.Location = new System.Drawing.Point(0, 24);
                this.btnMoveDown.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
                this.btnMoveDown.Name = "btnMoveDown";
                this.btnMoveDown.Size = new System.Drawing.Size(28, 23);
                this.btnMoveDown.TabIndex = 2;
                // 
                // btnMoveUp
                // 
                this.btnMoveUp.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.Image.MoveUp.bmp"));
                this.btnMoveUp.Location = new System.Drawing.Point(0, 0);
                this.btnMoveUp.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
                this.btnMoveUp.Name = "btnMoveUp";
                this.btnMoveUp.Size = new System.Drawing.Size(28, 23);
                this.btnMoveUp.TabIndex = 0;
                // 
                // vsPropertyGrid1
                // 
                this.vsPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.vsPropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
                this.vsPropertyGrid1.Location = new System.Drawing.Point(346, 31);
                this.vsPropertyGrid1.Margin = new System.Windows.Forms.Padding(3, 3, 13, 3);
                this.vsPropertyGrid1.Name = "vsPropertyGrid1";
                this.tlpAll.SetRowSpan(this.vsPropertyGrid1, 2);
                this.vsPropertyGrid1.Size = new System.Drawing.Size(283, 263);
                this.vsPropertyGrid1.TabIndex = 2;
                // 
                // lblRight
                // 
                this.lblRight.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.lblRight.Location = new System.Drawing.Point(346, 13);
                this.lblRight.Margin = new System.Windows.Forms.Padding(3, 13, 13, 3);
                this.lblRight.Name = "lblRight";
                this.lblRight.Size = new System.Drawing.Size(283, 12);
                this.lblRight.TabIndex = 1;
                this.lblRight.Text = "节点 属性（&P）：";
                // 
                // baseItemHost1
                // 
                this.baseItemHost1.AllowDrop = true;
                this.baseItemHost1.BaseItemObject = this.treeView1;
                this.baseItemHost1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.baseItemHost1.Location = new System.Drawing.Point(13, 31);
                this.baseItemHost1.Name = "treeView1";
                this.baseItemHost1.Size = new System.Drawing.Size(283, 228);
                // 
                // treeView1
                // 
                this.treeView1.Location = new System.Drawing.Point(13, 31);
                this.treeView1.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
                this.treeView1.Name = "treeView1";
                this.treeView1.Size = new System.Drawing.Size(283, 228);
                // 
                // lblLeft
                // 
                this.lblLeft.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.lblLeft.Location = new System.Drawing.Point(13, 13);
                this.lblLeft.Margin = new System.Windows.Forms.Padding(13, 13, 3, 3);
                this.lblLeft.Name = "lblLeft";
                this.lblLeft.Size = new System.Drawing.Size(283, 12);
                this.lblLeft.TabIndex = 4;
                this.lblLeft.Text = "选择需要编辑的节点（&N）：";
                // 
                // overarchingTableLayoutPanel
                // 
                this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tlpAll.Controls.Add(this.tlpMoveDelete, 1, 1);
                this.tlpAll.Controls.Add(this.lblRight, 2, 0);
                this.tlpAll.Controls.Add(this.vsPropertyGrid1, 2, 1);
                this.tlpAll.Controls.Add(this.baseItemHost1, 0, 1);
                this.tlpAll.Controls.Add(this.lblLeft, 0, 0);
                this.tlpAll.Controls.Add(this.tlpAddNode, 0, 2);
                this.tlpAll.Controls.Add(this.tlpOkAndCancel, 2, 3);
                this.tlpAll.Dock = System.Windows.Forms.DockStyle.Fill;
                this.tlpAll.Location = new System.Drawing.Point(0, 0);
                this.tlpAll.Name = "tlpAll";
                this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpAll.Size = new System.Drawing.Size(642, 336);
                this.tlpAll.TabIndex = 0;
                // 
                // navigationButtonsTableLayoutPanel
                // 
                this.tlpMoveDelete.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tlpMoveDelete.Controls.Add(this.btnMoveUp, 0, 0);
                this.tlpMoveDelete.Controls.Add(this.btnDelete, 0, 2);
                this.tlpMoveDelete.Controls.Add(this.btnMoveDown, 0, 1);
                this.tlpMoveDelete.Location = new System.Drawing.Point(302, 31);
                this.tlpMoveDelete.Margin = new System.Windows.Forms.Padding(3, 3, 13, 3);
                this.tlpMoveDelete.Name = "tlpMoveDelete";
                this.tlpMoveDelete.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpMoveDelete.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpMoveDelete.RowStyles.Add(new System.Windows.Forms.RowStyle());
                this.tlpMoveDelete.Size = new System.Drawing.Size(28, 83);
                this.tlpMoveDelete.TabIndex = 0;
                // 
                // TreeNodeCollectionForm
                // 
                this.AcceptButton = this.btnOk;
                this.CancelButton = this.btnCancel;
                this.ClientSize = new System.Drawing.Size(642, 336);
                this.Controls.Add(this.tlpAll);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.HelpButton = true;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "TreeNodeCollectionForm";
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.SizeGripStyle = SizeGripStyle.Show;
                this.Text = "TreeNode 编辑器";
                this.tlpOkAndCancel.ResumeLayout(false);
                this.tlpAddNode.ResumeLayout(false);
                this.tlpAll.ResumeLayout(false);
                this.tlpMoveDelete.ResumeLayout(false);
                this.ResumeLayout(false);
            }

            private void HookEvents()
            {
                this.btnOk.Click += new EventHandler(this.btnOK_Click);
                this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
                this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
                this.vsPropertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
                this.treeView1.SelectedNodeChanged += new PropertyChangedEventHandler(treeView1_SelectedNodeChanged);
                this.HelpButtonClicked += new CancelEventHandler(this.TreeNodeCollectionEditor_HelpButtonClicked);
                this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
                this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
                this.btnAddChild.MouseClick += new MouseEventHandler(this.btnAddChild_MouseClick);
                this.btnAddRoot.MouseClick += new MouseEventHandler(this.btnAddRoot_MouseClick);
                this.btnAddChild.MouseDown += new MouseEventHandler(btnAddChild_MouseDown);
                this.btnAddRoot.MouseDown += new MouseEventHandler(btnAddRoot_MouseDown);
                //
                foreach (Type one in this.m_Editor.CreateNewItemTypes())
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(one.Name);
                    item.Name = one.Name;
                    item.Tag = one;
                    item.Click += new EventHandler(Item_Click);
                    this.contextMenuStrip1.Items.Add(item);
                }
            }

            private void Add(NodeViewItem parent, Type type)
            {
                NodeViewItem node = Activator.CreateInstance(type) as NodeViewItem;
                //TreeNode node = base.CreateInstance(type) as TreeNode;
                if (node == null) return;
                //
                int num;
                this.NextNode = (num = this.NextNode) + 1;
                node.Name = type.Name + num.ToString();
                node.Text = node.Name;
                //
                if (parent == null)
                {
                    this.treeView1.NodeViewItems.Add(node);
                }
                else
                {
                    parent.NodeViewItems.Add(node);
                    parent.Expand();
                }
                //
                if (parent != null)
                {
                    this.treeView1.SelectedNode = parent;
                }
                else
                {
                    this.treeView1.SelectedNode = node;
                    this.SetNodeProps(node);
                }
            }

            private void btnAddRoot_MouseClick(object sender, MouseEventArgs e)
            {
                if (!this.btnAddRoot.ContainsSplitRectangle(e.Location))
                {
                    this.Add(null, this.m_Editor.CreateCollectionItemType());
                    this.SetButtonsState();
                }
            }

            private void btnAddChild_MouseClick(object sender, MouseEventArgs e)
            {
                if (!this.btnAddChild.ContainsSplitRectangle(e.Location))
                {
                    this.Add(this.m_CurrentNode, this.m_Editor.CreateCollectionItemType());
                    this.SetButtonsState();
                }
            }

            private void btnAddRoot_MouseDown(object sender, MouseEventArgs e)
            {
                if (this.btnAddRoot.ContainsSplitRectangle(e.Location))
                {
                    this.contextMenuStrip1.Tag = "L";
                    this.contextMenuStrip1.Show(this.btnAddRoot.PointToScreen(new Point(this.btnAddRoot.DisplayRectangle.Left, this.btnAddRoot.DisplayRectangle.Bottom)));
                }
            }

            private void btnAddChild_MouseDown(object sender, MouseEventArgs e)
            {
                if (this.btnAddChild.ContainsSplitRectangle(e.Location))
                {
                    this.contextMenuStrip1.Tag = "R";
                    this.contextMenuStrip1.Show(this.btnAddChild.PointToScreen(new Point(this.btnAddChild.DisplayRectangle.Left, this.btnAddChild.DisplayRectangle.Bottom)));
                }
            }

            private void Item_Click(object sender, EventArgs e)
            {
                if (this.contextMenuStrip1.Tag == null) return;
                //
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null) return;
                //
                if (this.contextMenuStrip1.Tag.ToString() == "L")
                {
                    this.Add(null, item.Tag as Type);
                    this.SetButtonsState();
                }
                else
                {
                    this.Add(this.m_CurrentNode, item.Tag as Type);
                    this.SetButtonsState();
                }
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                if (this.NextNode != this.m_IntialNextNode)
                {
                    this.NextNode = this.m_IntialNextNode;
                }
            }

            private void btnDelete_Click(object sender, EventArgs e)
            {
                INodeViewList pNodeViewList = this.m_CurrentNode.NodeViewList;
                if (pNodeViewList != null)
                {
                    pNodeViewList.NodeViewItems.Remove(this.m_CurrentNode);
                    if (this.treeView1.NodeViewItems.Count == 0)
                    {
                        this.m_CurrentNode = null;
                        this.SetNodeProps(null);
                    }
                    this.SetButtonsState();
                }
            }

            private void btnOK_Click(object sender, EventArgs e)
            {
                object[] objArray = new object[this.treeView1.NodeViewItems.Count];
                for (int i = 0; i < objArray.Length; i++)
                {
                    objArray[i] = this.treeView1.NodeViewItems[i].Clone();
                }
                base.Items = objArray;
                this.treeView1.Dispose();
                this.treeView1 = null;
            }

            private bool CheckParent(NodeViewItem child, NodeViewItem parent)
            {
                while (child != null)
                {
                    if (parent == child.ParentNode)
                    {
                        return true;
                    }
                    child = child.ParentNode;
                }
                return false;
            }

            private void btnMoveDown_Click(object sender, EventArgs e)
            {
                NodeViewItem curNode = this.m_CurrentNode;
                NodeViewItem parent = this.m_CurrentNode.ParentNode;
                if (parent == null)
                {
                    INodeViewList pNodeViewList = curNode.NodeViewList;
                    if (pNodeViewList != null && pNodeViewList.NodeViewItems.Contains(curNode))
                    {
                        int index = pNodeViewList.NodeViewItems.IndexOf(curNode);
                        this.treeView1.NodeViewItems.RemoveAt(index);
                        this.treeView1.NodeViewItems[index].NodeViewItems.Insert(0, curNode);
                    }
                }
                else
                {
                    INodeViewList pNodeViewList = curNode.NodeViewList;
                    if (pNodeViewList != null && pNodeViewList.NodeViewItems.Contains(curNode))
                    {
                        int index = pNodeViewList.NodeViewItems.IndexOf(curNode);
                        parent.NodeViewItems.RemoveAt(index);
                        if (index < parent.NodeViewItems.Count)
                        {
                            parent.NodeViewItems[index].NodeViewItems.Insert(0, curNode);
                        }
                        else if (parent.ParentNode == null)
                        {
                            INodeViewList pNodeViewList2 = parent.NodeViewList;
                            if (pNodeViewList2 != null && pNodeViewList.NodeViewItems.Contains(parent))
                            {
                                this.treeView1.NodeViewItems.Insert(pNodeViewList2.NodeViewItems.IndexOf(parent) + 1, curNode);
                            }
                        }
                        else
                        {
                            INodeViewList pNodeViewList2 = parent.NodeViewList;
                            if (pNodeViewList2 != null && pNodeViewList.NodeViewItems.Contains(parent))
                            {
                                parent.ParentNode.NodeViewItems.Insert(pNodeViewList2.NodeViewItems.IndexOf(parent) + 1, curNode);
                            }
                        }
                    }
                }
                this.treeView1.SelectedNode = curNode;
                this.m_CurrentNode = curNode;
            }

            private void btnMoveUp_Click(object sender, EventArgs e)
            {
                NodeViewItem curNode = this.m_CurrentNode;
                NodeViewItem parent = this.m_CurrentNode.ParentNode;
                if (parent == null)
                {
                    INodeViewList pNodeViewList = curNode.NodeViewList;
                    if (pNodeViewList != null && pNodeViewList.NodeViewItems.Contains(curNode))
                    {
                        int index = pNodeViewList.NodeViewItems.IndexOf(curNode);
                        this.treeView1.NodeViewItems.RemoveAt(index);
                        this.treeView1.NodeViewItems[index - 1].NodeViewItems.Add(curNode);
                    }
                }
                else
                {
                    INodeViewList pNodeViewList = curNode.NodeViewList;
                    if (pNodeViewList != null && pNodeViewList.NodeViewItems.Contains(curNode))
                    {
                        int index = pNodeViewList.NodeViewItems.IndexOf(curNode);
                        parent.NodeViewItems.RemoveAt(index);
                        if (index == 0)
                        {
                            INodeViewList pNodeViewList2 = parent.NodeViewList;
                            if (pNodeViewList2 != null && pNodeViewList.NodeViewItems.Contains(parent))
                            {
                                int index2 = pNodeViewList.NodeViewItems.IndexOf(curNode);
                                if (parent.ParentNode == null)
                                {
                                    this.treeView1.NodeViewItems.Insert(index2, curNode);
                                }
                                else
                                {
                                    parent.ParentNode.NodeViewItems.Insert(index2, curNode);
                                }
                            }
                        }
                        else
                        {
                            parent.NodeViewItems[index - 1].NodeViewItems.Add(curNode);
                        }
                    }
                }
                this.treeView1.SelectedNode = curNode;
                this.m_CurrentNode = curNode;
            }

            protected override void OnEditValueChanged()
            {
                if (base.EditValue != null)
                {
                    object[] items = base.Items;
                    //this.vsPropertyGrid1.Site = new CollectionEditor.PropertyGridSite(base.Context, this.vsPropertyGrid1);
                    NodeViewItem[] nodes = new NodeViewItem[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        nodes[i] = (NodeViewItem)((NodeViewItem)items[i]).Clone();
                    }
                    this.treeView1.NodeViewItems.Clear();
                    //foreach (NodeViewItem one in this.treeView1.NodeViewItems)
                    foreach (NodeViewItem one in nodes)
                    {
                        this.treeView1.NodeViewItems.Add(one);
                    }
                    this.m_CurrentNode = null;
                    this.btnAddChild.Enabled = false;
                    this.btnDelete.Enabled = false;
                    NodeViewItemTreeItem treeView = this.TreeView;
                    if ((items.Length > 0) && (nodes[0] != null))
                    {
                        this.treeView1.SelectedNode = nodes[0];
                    }
                }
            }

            private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
            {
                if (this.treeView1.SelectedNode != null)
                {
                    this.lblRight.Text = this.treeView1.SelectedNode.Name + " 属性（&P）";
                }
                else
                {
                    this.lblRight.Text = "节点 属性（&P）";
                }
            }

            private void SetButtonsState()
            {
                bool flag = this.treeView1.NodeViewItems.Count > 0;
                this.btnAddChild.Enabled = flag;
                this.btnDelete.Enabled = flag;
                this.btnMoveDown.Enabled = (flag && ((this.m_CurrentNode != this.LastNode))) && (this.m_CurrentNode != this.treeView1.NodeViewItems[this.treeView1.NodeViewItems.Count - 1]);
                this.btnMoveUp.Enabled = flag && (this.m_CurrentNode != this.treeView1.NodeViewItems[0]);
            }

            private void SetNodeProps(NodeViewItem node)
            {
                if (node != null)
                {
                    this.lblRight.Text = node.Name.ToString() + " 属性（&P）";
                }
                else
                {
                    this.lblRight.Text = "节点 属性（&P）";
                }
                this.vsPropertyGrid1.SelectedObject = node;
            }

            private void TreeNodeCollectionEditor_HelpButtonClicked(object sender, CancelEventArgs e)
            {
                e.Cancel = true;
                this.m_Editor.ShowHelp();
            }

            private void treeView1_SelectedNodeChanged(object sender, PropertyChangedEventArgs e)
            {
                this.m_CurrentNode = e.NewValue as NodeViewItem;
                this.SetNodeProps(this.m_CurrentNode);
                this.SetButtonsState();
            }

            // Properties
            private NodeViewItem LastNode
            {
                get
                {
                    NodeViewItem node = this.treeView1.NodeViewItems[this.treeView1.NodeViewItems.Count - 1];
                    while (node.NodeViewItems.Count > 0)
                    {
                        node = node.NodeViewItems[node.NodeViewItems.Count - 1];
                    }
                    return node;
                }
            }

            private int NextNode
            {
                get
                {
                    if ((this.TreeView != null) && (this.TreeView.Site != null))
                    {
                        IDictionaryService service = (IDictionaryService)this.TreeView.Site.GetService(typeof(IDictionaryService));
                        if (service != null)
                        {
                            object obj2 = service.GetValue(STATIC_NextNodeKey);
                            if (obj2 != null)
                            {
                                this.m_NextNode = (int)obj2;
                            }
                            else
                            {
                                this.m_NextNode = 0;
                                service.SetValue(STATIC_NextNodeKey, 0);
                            }
                        }
                    }
                    return this.m_NextNode;
                }
                set
                {
                    this.m_NextNode = value;
                    if ((this.TreeView != null) && (this.TreeView.Site != null))
                    {
                        IDictionaryService service = (IDictionaryService)this.TreeView.Site.GetService(typeof(IDictionaryService));
                        if (service != null)
                        {
                            service.SetValue(STATIC_NextNodeKey, this.m_NextNode);
                        }
                    }
                }
            }

            private NodeViewItemTreeItem TreeView
            {
                get
                {
                    if ((base.Context != null) && (base.Context.Instance is TreeView))
                    {
                        return (NodeViewItemTreeItem)base.Context.Instance;
                    }
                    return null;
                }
            }
        }

    }
}

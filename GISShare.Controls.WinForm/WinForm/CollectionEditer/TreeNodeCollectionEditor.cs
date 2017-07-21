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

namespace GISShare.Controls.WinForm
{
    public class TreeNodeCollectionEditor : CollectionEditor
    {
        public TreeNodeCollectionEditor()
            : base(typeof(TreeNodeCollection))
        {
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(TreeNodeItem), typeof(TitleTreeNodeItem), typeof(TreeNode) };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(TreeNodeItem);
        }

        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            return new TreeNodeCollectionForm(this);
        }
                
        //
        //
        //

        private class TreeNodeCollectionForm : CollectionEditor.CollectionForm
        {
            private static object STATIC_NextNodeKey = new object();

            private TreeNodeCollectionEditor m_Editor;
            private int m_IntialNextNode;
            private int m_NextNode;
            private TreeNode m_CurrentNode;

            // Fields
            private Label lblLeft;
            private Label lblRight;
            private Button btnMoveDown;
            private Button btnMoveUp;
            private Button btnOk;
            private Button btnCancel;
            private Button btnDelete;
            private VsButton btnAddRoot;
            private VsButton btnAddChild;
            private TreeView treeView1;
            private VsPropertyGrid vsPropertyGrid1;
            private ContextMenuStrip contextMenuStrip1;
            private TableLayoutPanel tlpAddNode;
            private TableLayoutPanel tlpOkAndCancel;
            private TableLayoutPanel tlpMoveDelete;
            private TableLayoutPanel tlpAll;

            // Methods
            public TreeNodeCollectionForm(TreeNodeCollectionEditor editor)
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
                this.btnOk = new Button();
                this.btnCancel = new Button();
                this.tlpAddNode = new TableLayoutPanel();
                this.btnAddRoot = new VsButton(this.m_Editor.CreateNewItemTypes().Length > 1);
                this.btnAddChild = new VsButton(this.m_Editor.CreateNewItemTypes().Length > 1);
                this.btnDelete = new Button();
                this.btnMoveDown = new Button();
                this.btnMoveUp = new Button();
                this.vsPropertyGrid1 = new VsPropertyGrid(base.Context);
                this.lblRight = new Label();
                this.treeView1 = new TreeView();
                this.lblLeft = new Label();
                this.tlpAll = new TableLayoutPanel();
                this.tlpMoveDelete = new TableLayoutPanel();
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
                // treeView1
                // 
                this.treeView1.AllowDrop = true;
                this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.treeView1.HideSelection = false;
                this.treeView1.Location = new System.Drawing.Point(13, 31);
                this.treeView1.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
                this.treeView1.Name = "treeView1";
                this.treeView1.Size = new System.Drawing.Size(283, 228);
                this.treeView1.TabIndex = 3;
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
                this.tlpAll.Controls.Add(this.treeView1, 0, 1);
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
                this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
                this.treeView1.DragEnter += new DragEventHandler(this.treeView1_DragEnter);
                this.treeView1.ItemDrag += new ItemDragEventHandler(this.treeView1_ItemDrag);
                this.treeView1.DragDrop += new DragEventHandler(this.treeView1_DragDrop);
                this.treeView1.DragOver += new DragEventHandler(this.treeView1_DragOver);
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

            private void Add(TreeNode parent, Type type)
            {
                TreeNode node = Activator.CreateInstance(type) as TreeNode;
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
                    this.treeView1.Nodes.Add(node);
                }
                else
                {
                    parent.Nodes.Add(node);
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
                this.m_CurrentNode.Remove();
                if (this.treeView1.Nodes.Count == 0)
                {
                    this.m_CurrentNode = null;
                    this.SetNodeProps(null);
                }
                this.SetButtonsState();
            }

            private void btnOK_Click(object sender, EventArgs e)
            {
                object[] objArray = new object[this.treeView1.Nodes.Count];
                for (int i = 0; i < objArray.Length; i++)
                {
                    objArray[i] = this.treeView1.Nodes[i].Clone();
                }
                base.Items = objArray;
                this.treeView1.Dispose();
                this.treeView1 = null;
            }

            private bool CheckParent(TreeNode child, TreeNode parent)
            {
                while (child != null)
                {
                    if (parent == child.Parent)
                    {
                        return true;
                    }
                    child = child.Parent;
                }
                return false;
            }

            private void btnMoveDown_Click(object sender, EventArgs e)
            {
                TreeNode curNode = this.m_CurrentNode;
                TreeNode parent = this.m_CurrentNode.Parent;
                if (parent == null)
                {
                    this.treeView1.Nodes.RemoveAt(curNode.Index);
                    this.treeView1.Nodes[curNode.Index].Nodes.Insert(0, curNode);
                }
                else
                {
                    parent.Nodes.RemoveAt(curNode.Index);
                    if (curNode.Index < parent.Nodes.Count)
                    {
                        parent.Nodes[curNode.Index].Nodes.Insert(0, curNode);
                    }
                    else if (parent.Parent == null)
                    {
                        this.treeView1.Nodes.Insert(parent.Index + 1, curNode);
                    }
                    else
                    {
                        parent.Parent.Nodes.Insert(parent.Index + 1, curNode);
                    }
                }
                this.treeView1.SelectedNode = curNode;
                this.m_CurrentNode = curNode;
            }

            private void btnMoveUp_Click(object sender, EventArgs e)
            {
                TreeNode curNode = this.m_CurrentNode;
                TreeNode parent = this.m_CurrentNode.Parent;
                if (parent == null)
                {
                    this.treeView1.Nodes.RemoveAt(curNode.Index);
                    this.treeView1.Nodes[curNode.Index - 1].Nodes.Add(curNode);
                }
                else
                {
                    parent.Nodes.RemoveAt(curNode.Index);
                    if (curNode.Index == 0)
                    {
                        if (parent.Parent == null)
                        {
                            this.treeView1.Nodes.Insert(parent.Index, curNode);
                        }
                        else
                        {
                            parent.Parent.Nodes.Insert(parent.Index, curNode);
                        }
                    }
                    else
                    {
                        parent.Nodes[curNode.Index - 1].Nodes.Add(curNode);
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
                    TreeNode[] nodes = new TreeNode[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        nodes[i] = (TreeNode)((TreeNode)items[i]).Clone();
                    }
                    this.treeView1.Nodes.Clear();
                    this.treeView1.Nodes.AddRange(nodes);
                    this.m_CurrentNode = null;
                    this.btnAddChild.Enabled = false;
                    this.btnDelete.Enabled = false;
                    TreeView treeView = this.TreeView;
                    if (treeView != null)
                    {
                        this.SetImageProps(treeView);
                    }
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
                bool flag = this.treeView1.Nodes.Count > 0;
                this.btnAddChild.Enabled = flag;
                this.btnDelete.Enabled = flag;
                this.btnMoveDown.Enabled = (flag && ((this.m_CurrentNode != this.LastNode) || (this.m_CurrentNode.Level > 0))) && (this.m_CurrentNode != this.treeView1.Nodes[this.treeView1.Nodes.Count - 1]);
                this.btnMoveUp.Enabled = flag && (this.m_CurrentNode != this.treeView1.Nodes[0]);
            }

            private void SetImageProps(TreeView actualTreeView)
            {
                if (actualTreeView.ImageList != null)
                {
                    this.treeView1.ImageList = actualTreeView.ImageList;
                    this.treeView1.ImageIndex = actualTreeView.ImageIndex;
                    this.treeView1.SelectedImageIndex = actualTreeView.SelectedImageIndex;
                }
                else
                {
                    this.treeView1.ImageList = null;
                    this.treeView1.ImageIndex = -1;
                    this.treeView1.SelectedImageIndex = -1;
                }
                if (actualTreeView.StateImageList != null)
                {
                    this.treeView1.StateImageList = actualTreeView.StateImageList;
                }
                else
                {
                    this.treeView1.StateImageList = null;
                }
                this.treeView1.CheckBoxes = actualTreeView.CheckBoxes;
            }

            private void SetNodeProps(TreeNode node)
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

            private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
            {
                this.m_CurrentNode = e.Node;
                this.SetNodeProps(this.m_CurrentNode);
                this.SetButtonsState();
            }

            private void treeView1_DragDrop(object sender, DragEventArgs e)
            {
                TreeNode data = this.GetDataEx(e);
                if (data == null) return;
                Point p = new Point(e.X, e.Y);
                p = this.treeView1.PointToClient(p);
                TreeNode nodeAt = this.treeView1.GetNodeAt(p);
                if (data != nodeAt)
                {
                    this.treeView1.Nodes.Remove(data);
                    if ((nodeAt != null) && !this.CheckParent(nodeAt, data))
                    {
                        nodeAt.Nodes.Add(data);
                    }
                    else
                    {
                        this.treeView1.Nodes.Add(data);
                    }
                }
            }
            private TreeNode GetDataEx(DragEventArgs e)
            {
                TreeNode data = null;
                //
                foreach (Type one in this.m_Editor.CreateNewItemTypes())
                {
                    data = e.Data.GetData(one) as TreeNode;
                    if (data != null) break;
                }
                //
                return data;
            }

            private void treeView1_DragEnter(object sender, DragEventArgs e)
            {
                if (this.GetDataPresentEx(e))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            private bool GetDataPresentEx(DragEventArgs e) 
            {
                foreach(Type one in this.m_Editor.CreateNewItemTypes())
                {
                    if (e.Data.GetDataPresent(one)) return true;
                }
                //
                return false;
            }

            private void treeView1_DragOver(object sender, DragEventArgs e)
            {
                Point p = new Point(e.X, e.Y);
                p = this.treeView1.PointToClient(p);
                TreeNode nodeAt = this.treeView1.GetNodeAt(p);
                this.treeView1.SelectedNode = nodeAt;
            }

            private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
            {
                TreeNode item = (TreeNode)e.Item;
                base.DoDragDrop(item, DragDropEffects.Move);
            }

            // Properties
            private TreeNode LastNode
            {
                get
                {
                    TreeNode node = this.treeView1.Nodes[this.treeView1.Nodes.Count - 1];
                    while (node.Nodes.Count > 0)
                    {
                        node = node.Nodes[node.Nodes.Count - 1];
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

            private TreeView TreeView
            {
                get
                {
                    if ((base.Context != null) && (base.Context.Instance is TreeView))
                    {
                        return (TreeView)base.Context.Instance;
                    }
                    return null;
                }
            }
        }

    }
}

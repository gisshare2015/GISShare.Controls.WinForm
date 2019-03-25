using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public abstract partial class CollectionDesignerForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm//Form
    {
        public GetServiceCallBack GetServiceCallBackEx;

        private IObjectDesignHelper m_pObjectDesignHelper = null;

        public CollectionDesignerForm(IObjectDesignHelper pObjectDesignHelper)
        {
            InitializeComponent();
            //
            this.m_pObjectDesignHelper = pObjectDesignHelper;
            this.BuildPopup();
            this.BuildTree_DG(this.m_pObjectDesignHelper, this.treeView1.NodeViewItems);
            if(this.treeView1.NodeViewItems.Count > 0) this.treeView1.NodeViewItems[0].Expand();
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.TopMost = true;
            infoForm.ShowDialog();
        }
        
        private void BuildPopup()
        {
            BaseButtonItem item = new BaseButtonItem();//0
            item.Name = "Expand";
            item.Text = "展开该节点";
            item.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item);
            //
            BaseButtonItem item2 = new BaseButtonItem();//1
            item2.Name = "ExpandAll";
            item2.Text = "展开所有子节点";
            item2.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item2);
            //
            BaseButtonItem item3 = new BaseButtonItem();//2
            item3.Name = "Collapse";
            item3.Text = "折叠该节点";
            item3.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item3);
            //
            SeparatorItem item4 = new SeparatorItem();//3
            item4.Name = "EECSeparator";
            item4.Text = "EECSeparator";
            this.ribbonPopup1.Items.Add(item4);
            //
            BaseButtonItem item5 = new BaseButtonItem();//4
            item5.Name = "Delete";
            item5.Text = "删除";
            item5.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item5);
            //
            SeparatorItem item6 = new SeparatorItem();//5
            item6.Name = "DSeparator";
            item6.Text = "DSeparator";
            this.ribbonPopup1.Items.Add(item6);//5
            //
            DropDownButtonItem item7 = new DropDownButtonItem();//6
            item7.Name = "AddItem";
            item7.Text = "添加子项";
            item7.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item7);
            //
            BaseButtonItem item8 = new BaseButtonItem();//7
            item8.Name = "ClearItems";
            item8.Text = "清除子项";
            item8.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item8);
            //
            SeparatorItem item9 = new SeparatorItem();//8
            item9.Name = "ACSeparator";
            item9.Text = "ACSeparator";
            this.ribbonPopup1.Items.Add(item9);
            //
            BaseButtonItem item10 = new BaseButtonItem();//9
            item10.Name = "Up";
            item10.Text = "上移";
            item10.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item10);
            //
            BaseButtonItem item11 = new BaseButtonItem();//10
            item11.Name = "Down";
            item11.Text = "下移";
            item11.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item11);
            //
            SeparatorItem item12 = new SeparatorItem();//11
            item12.Name = "UDSeparator";
            item12.Text = "UDSeparator";
            this.ribbonPopup1.Items.Add(item12);
            //
            BaseButtonItem item13 = new BaseButtonItem();//12
            item13.Name = "ShowPopup";
            item13.Text = "展现弹出菜单";
            item13.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item13);
            //
            BaseButtonItem item14 = new BaseButtonItem();//13
            item14.Name = "ClosePopup";
            item14.Text = "关闭弹出菜单";
            item14.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item14);
            //
            SeparatorItem item15 = new SeparatorItem();//14
            item15.Name = "SCSeparator";
            item15.Text = "SCSeparator";
            this.ribbonPopup1.Items.Add(item15);
            //
            BaseButtonItem item16 = new BaseButtonItem();//15
            item16.Name = "Refresh";
            item16.Text = "刷新";
            item16.MouseClick += new MouseEventHandler(Item_MouseClick);
            this.ribbonPopup1.Items.Add(item16);
        }
        void Item_MouseClick(object sender, MouseEventArgs e)
        {
            IBaseItem pBaseItem = sender as IBaseItem;
            if (pBaseItem == null) return;
            //
            View.NodeViewItem node = this.ribbonPopup1.Tag as View.NodeViewItem;
            if (node == null) return;
            //
            View.NodeViewItem nodeParent = node.ParentNode;
            //
            if (nodeParent == null)
            {
                this.PopupItemOperation(pBaseItem.Name, null, node);
            }
            else if (nodeParent.Tag is IList)
            {
                this.PopupItemOperation(pBaseItem.Name, nodeParent.Tag as IList, node);
            }
            else if (nodeParent.Tag is ICollectionObjectDesignHelper)
            {
                this.PopupItemOperation(pBaseItem.Name, ((ICollectionObjectDesignHelper)nodeParent.Tag).List, node);
            }
            else
            {
                this.PopupItemOperation(pBaseItem.Name, null, node);
            }
        }
        private void PopupItemOperation(string sender, IList parentList, View.NodeViewItem node)
        {
            if (sender == "Expand") { node.Expand(); return; }
            if (sender == "ExpandAll") { node.ExpandAll(); return; }
            if (sender == "Collapse") { node.Collapse(); return; }
            //
            if (parentList != null && parentList.Contains(node.Tag))
            {
                switch (sender)
                {
                    case "Delete":
                        if (!parentList.IsReadOnly)
                        {
                            Component one = node.Tag as Component;
                            if (one != null) one.Dispose();
                            parentList.Remove(node.Tag);
                            View.INodeViewList pNodeViewList = node.pOwner as View.INodeViewList;
                            if (pNodeViewList != null)
                            {
                                pNodeViewList.NodeViewItems.Remove(node);
                                this.treeView1.Refresh();
                            }
                        }
                        break;
                    case "AddItem":
                        break;
                    case "ClearItems":
                        if (node.Tag is ICollectionObjectDesignHelper)
                        {
                            ICollectionObjectDesignHelper pCollectionObjectDesignHelper = (ICollectionObjectDesignHelper)node.Tag;
                            if (!pCollectionObjectDesignHelper.List.IsReadOnly && pCollectionObjectDesignHelper.List.Count > 0)
                            {
                                foreach (Component one in pCollectionObjectDesignHelper.List)
                                {
                                    if (one != null) one.Dispose();
                                }
                                pCollectionObjectDesignHelper.List.Clear();
                                node.NodeViewItems.Clear();
                                this.treeView1.Refresh();
                            }
                        }
                        else if (node.Tag is IList)
                        {
                            IList list = (IList)node.Tag;
                            if (!list.IsReadOnly && list.Count > 0)
                            {
                                foreach (Component one in list)
                                {
                                    if (one != null) one.Dispose();
                                }
                                list.Clear();
                                node.NodeViewItems.Clear();
                                this.treeView1.Refresh();
                            }
                        }
                        break;
                    case "Up":
                        if (!parentList.IsReadOnly)
                        {
                            int iNow = parentList.IndexOf(node.Tag);
                            if (iNow > 0)
                            {
                                IFlexibleList pFlexibleList = parentList as IFlexibleList;
                                if (pFlexibleList != null)
                                {
                                    View.NodeViewItem nodeParent = node.ParentNode;
                                    //
                                    if (nodeParent != null)
                                    {
                                        int iNodeIndex = nodeParent.NodeViewItems.IndexOf(node);
                                        if (nodeParent.NodeViewItems.ExchangeItem(iNodeIndex - 1, iNodeIndex))
                                        {
                                            pFlexibleList.ExchangeItem(iNow - 1, iNow);
                                            this.treeView1.SelectedNode = node;
                                            nodeParent.Refresh();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Down":
                        if (!parentList.IsReadOnly)
                        {
                            int iNow = parentList.IndexOf(node.Tag);
                            if (iNow < parentList.Count - 1)
                            {
                                IFlexibleList pFlexibleList = parentList as IFlexibleList;
                                if (pFlexibleList != null)
                                {
                                    View.NodeViewItem nodeParent = node.ParentNode;
                                    //
                                    if (nodeParent != null)
                                    {
                                        int iNodeIndex = nodeParent.NodeViewItems.IndexOf(node);
                                        if (nodeParent.NodeViewItems.ExchangeItem(iNodeIndex + 1, iNodeIndex))
                                        {
                                            pFlexibleList.ExchangeItem(iNow + 1, iNow);
                                            this.treeView1.SelectedNode = node;
                                            nodeParent.Refresh();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "ShowPopup":
                        IPopupObjectDesignHelper pPopupObjectDesignHelper = node.Tag as IPopupObjectDesignHelper;
                        if (pPopupObjectDesignHelper != null)
                        {
                            pPopupObjectDesignHelper.ShowPopup();
                        }
                        break;
                    case "ClosePopup":
                        IPopupObjectDesignHelper pPopupObjectDesignHelper2 = node.Tag as IPopupObjectDesignHelper;
                        if (pPopupObjectDesignHelper2 != null)
                        {
                            pPopupObjectDesignHelper2.ClosePopup();
                        }
                        break;
                    case "Refresh":
                        IObjectDesignHelper pObjectDesignHelper = node.Tag as IObjectDesignHelper;
                        if (pObjectDesignHelper != null)
                        {
                            pObjectDesignHelper.Refresh();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sender)
                {
                    case "Delete":
                        break;
                    case "AddItem":
                        break;
                    case "ClearItems":
                        if (node.Tag is ICollectionObjectDesignHelper)
                        {
                            ICollectionObjectDesignHelper pCollectionObjectDesignHelper = (ICollectionObjectDesignHelper)node.Tag;
                            if (!pCollectionObjectDesignHelper.List.IsReadOnly && pCollectionObjectDesignHelper.List.Count > 0)
                            {
                                foreach (Component one in pCollectionObjectDesignHelper.List)
                                {
                                    if (one != null) one.Dispose();
                                }
                                pCollectionObjectDesignHelper.List.Clear();
                                node.NodeViewItems.Clear();
                            }
                        }
                        else if (node.Tag is IList)
                        {
                            IList list = (IList)node.Tag;
                            if (!list.IsReadOnly && list.Count > 0)
                            {
                                foreach (Component one in list)
                                {
                                    if (one != null) one.Dispose();
                                }
                                list.Clear();
                                node.NodeViewItems.Clear();
                            }
                        }
                        break;
                    case "Up":
                        break;
                    case "Down":
                        break;
                    case "ShowPopup":
                        IPopupObjectDesignHelper pPopupObjectDesignHelper = node.Tag as IPopupObjectDesignHelper;
                        if (pPopupObjectDesignHelper != null)
                        {
                            pPopupObjectDesignHelper.ShowPopup();
                        }
                        break;
                    case "ClosePopup":
                        IPopupObjectDesignHelper pPopupObjectDesignHelper2 = node.Tag as IPopupObjectDesignHelper;
                        if (pPopupObjectDesignHelper2 != null)
                        {
                            pPopupObjectDesignHelper2.ClosePopup();
                        }
                        break;
                    case "Refresh":
                        IObjectDesignHelper pObjectDesignHelper = node.Tag as IObjectDesignHelper;
                        if (pObjectDesignHelper != null)
                        {
                            pObjectDesignHelper.Refresh();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 创建加载子项类型的Popup
        /// </summary>
        /// <param name="node"></param>
        private void CreateNewItemTypePopup(View.NodeViewItem node)
        {
            IDropDownButtonItem newItems = this.CreateNewItemTypeDropDownButton;
            if (newItems == null) return;
            newItems.BaseItems.Clear();
            //
            string strName = this.GetNewItemTypesDictionaryKey(node);
            //
            Dictionary<string, Type[]> createNewItemTypesDictionary = this.CreateNewItemTypesDictionary();
            Type[] types;
            if (createNewItemTypesDictionary == null ||
                !createNewItemTypesDictionary.ContainsKey(strName))
            {
                types = this.CreateNewItemTypes();
            }
            else
            {
                types = createNewItemTypesDictionary[strName];
            }
            foreach (Type one in types)
            {
                if (one == null)
                {
                    SeparatorItem item = new SeparatorItem();
                    newItems.BaseItems.Add(item);
                }
                else
                {
                    BaseButtonItem item = new BaseButtonItem();
                    item.Name = one.Name;
                    item.Text = one.Name;
                    item.Tag = one;
                    item.MouseClick += new MouseEventHandler(CreateItem_MouseClick);
                    newItems.BaseItems.Add(item);
                }
            }
        }
        protected virtual string GetNewItemTypesDictionaryKey(View.NodeViewItem node)
        {
            return node.Tag == null ? node.Name : node.Tag.GetType().FullName;
        }
        void CreateItem_MouseClick(object sender, MouseEventArgs e)
        {
            BaseButtonItem item = sender as BaseButtonItem;
            if (item == null) return;
            //
            View.NodeViewItem node = this.CreateNewItemTypeDropDownButton.Tag as View.NodeViewItem;
            if (node == null) return;
            //
            ICollectionObjectDesignHelper pCollectionObjectDesignHelper = node.Tag as ICollectionObjectDesignHelper;
            if (pCollectionObjectDesignHelper != null)
            {
                this.Add(node, item.Tag as Type, pCollectionObjectDesignHelper.List);
            }
            else
            {
                this.Add(node, item.Tag as Type, node.Tag as IList);
            }
        }
        private void Add(View.NodeViewItem parent, Type type, IList list)
        {
            if (parent == null) return;
            if (list == null) return;
            //
            IDesignerHost host = (IDesignerHost)GetServiceCallBackEx(typeof(IDesignerHost));
            if (host != null)
            {
                IComponent pComponent = host.CreateComponent(type);
                if (this.SetCreateTypeInfo(pComponent))
                {
                    if (list.Add(pComponent) >= 0)
                    {
                        this.BuildTree_DG(pComponent as IObjectDesignHelper, parent.NodeViewItems);
                        parent.Refresh();
                    }
                    else { pComponent.Dispose(); }
                }
                else { pComponent.Dispose(); }
            }
        }

        private void treeView1_SelectedNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (this.treeView1.NodeViewItems.Contains(e.Node)) return;
            //
            View.NodeViewItem node = e.NewValue as View.NodeViewItem;
            if (node == null) return;
            //
            if (!this.FiltrationSelected(node)) return;
            //
            Component component = node.Tag as Component;
            if (component == null) return;
            //
            ISelectionService pSelectionService = this.GetServiceCallBackEx(typeof(ISelectionService)) as ISelectionService;
            if (pSelectionService != null)
            {
                pSelectionService.SetSelectedComponents(new Component[] { component }, SelectionTypes.Primary);
                this.SelectedComponent(component);
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            //
            View.NodeViewItem node = this.treeView1.TryGetNodeViewItemFromPoint(e.Location);
            if (node != this.treeView1.SelectedNode) return;
            //
            this.ribbonPopup1.Tag = node;
            //
            if (!this.FiltrationShowPopup(node))
            {
                this.ribbonPopup1.Items["Expand"].Visible = false;
                this.ribbonPopup1.Items["ExpandAll"].Visible = false;
                this.ribbonPopup1.Items["Collapse"].Visible = false;
                this.ribbonPopup1.Items["EECSeparator"].Visible = false;
                this.ribbonPopup1.Items["Delete"].Visible = false;
                this.ribbonPopup1.Items["DSeparator"].Visible = false;
                this.ribbonPopup1.Items["AddItem"].Visible = false;
                this.ribbonPopup1.Items["ClearItems"].Visible = false;
                this.ribbonPopup1.Items["ACSeparator"].Visible = false;
                this.ribbonPopup1.Items["Up"].Visible = false;
                this.ribbonPopup1.Items["Down"].Visible = false;
                this.ribbonPopup1.Items["UDSeparator"].Visible = false;
                this.ribbonPopup1.Items["ShowPopup"].Visible = false;
                this.ribbonPopup1.Items["ClosePopup"].Visible = false;
                this.ribbonPopup1.Items["SCSeparator"].Visible = false;
                this.ribbonPopup1.Items["Refresh"].Visible = false;
            }
            else
            {
                if (node.Tag != null)
                {
                    this.CreateNewItemTypePopup(node);
                    this.CreateNewItemTypeDropDownButton.Tag = node;
                }
                //
                View.NodeViewItem nodeParent = node.ParentNode;
                //
                if (nodeParent == null)
                {
                    if (!this.FiltrationPopupItem(null, node.Tag)) return;
                }
                else if (nodeParent.Tag is IList)
                {
                    if (!this.FiltrationPopupItem((IList)nodeParent.Tag, node.Tag)) return;
                }
                else if (nodeParent.Tag is ICollectionObjectDesignHelper)
                {
                    if (!this.FiltrationPopupItem(((ICollectionObjectDesignHelper)nodeParent.Tag).List, node.Tag)) return;
                }
                else
                {
                    if (!this.FiltrationPopupItem(null, node.Tag)) return;
                }
            }
            //
            this.ribbonPopup1.Show(this.treeView1.PointToScreen(e.Location));
        }
        private bool FiltrationPopupItem(IList parentList, object value)
        {
            if (value == null) return false;
            //
            if (parentList != null && parentList.Contains(value))
            {
                bool bD = parentList.IsReadOnly ? false : true;
                this.ribbonPopup1.Items["Delete"].Visible = bD;
                this.ribbonPopup1.Items["DSeparator"].Visible = bD;
                //
                bool bEEC = false;
                bool bAC = false;
                if (value is ICollectionObjectDesignHelper)
                {
                    ICollectionObjectDesignHelper pCollectionObjectDesignHelper = (ICollectionObjectDesignHelper)value;
                    bAC = (pCollectionObjectDesignHelper == null || pCollectionObjectDesignHelper.List.IsReadOnly) ? false : true;
                    bEEC = (pCollectionObjectDesignHelper == null || pCollectionObjectDesignHelper.List.Count <= 0) ? false : true;
                }
                else if (value is IList)
                {
                    IList list = (IList)value;
                    bAC = (list == null || list.IsReadOnly) ? false : true;
                    bEEC = (list == null || list.Count <= 0) ? false : true;
                }
                this.ribbonPopup1.Items["Expand"].Visible = bEEC;
                this.ribbonPopup1.Items["ExpandAll"].Visible = bEEC;
                this.ribbonPopup1.Items["Collapse"].Visible = bEEC;
                this.ribbonPopup1.Items["EECSeparator"].Visible = bEEC;
                this.ribbonPopup1.Items["AddItem"].Visible = bAC;
                this.ribbonPopup1.Items["ClearItems"].Visible = bAC;
                this.ribbonPopup1.Items["ACSeparator"].Visible = bAC;
                //
                bool bUD = (parentList.IsReadOnly || parentList.Count <= 1) ? false : true;
                this.ribbonPopup1.Items["Up"].Visible = bUD;
                this.ribbonPopup1.Items["Down"].Visible = bUD;
                this.ribbonPopup1.Items["UDSeparator"].Visible = bUD;
                //
                bool bSC = value is IPopupObjectDesignHelper;
                this.ribbonPopup1.Items["ShowPopup"].Visible = bSC;
                this.ribbonPopup1.Items["ClosePopup"].Visible = bSC;
                this.ribbonPopup1.Items["SCSeparator"].Visible = bSC;
                this.ribbonPopup1.Items["Refresh"].Visible = true;
                //
                return true;
            }
            else
            {
                this.ribbonPopup1.Items["Delete"].Visible = false;
                this.ribbonPopup1.Items["DSeparator"].Visible = false;
                //
                bool bEEC = false;
                bool bAC = false;
                if (value is ICollectionObjectDesignHelper)
                {
                    ICollectionObjectDesignHelper pCollectionObjectDesignHelper = (ICollectionObjectDesignHelper)value;
                    bAC = (pCollectionObjectDesignHelper == null || pCollectionObjectDesignHelper.List.IsReadOnly) ? false : true;
                    bEEC = (pCollectionObjectDesignHelper == null || pCollectionObjectDesignHelper.List.Count <= 0) ? false : true;
                }
                else if (value is IList)
                {
                    IList list = (IList)value;
                    bAC = (list == null || list.IsReadOnly) ? false : true;
                    bEEC = (list == null || list.Count <= 0) ? false : true;
                }
                this.ribbonPopup1.Items["Expand"].Visible = bEEC;
                this.ribbonPopup1.Items["ExpandAll"].Visible = bEEC;
                this.ribbonPopup1.Items["Collapse"].Visible = bEEC;
                this.ribbonPopup1.Items["EECSeparator"].Visible = bEEC;
                this.ribbonPopup1.Items["AddItem"].Visible = bAC;
                this.ribbonPopup1.Items["ClearItems"].Visible = bAC;
                this.ribbonPopup1.Items["ACSeparator"].Visible = bAC;
                //
                this.ribbonPopup1.Items["Up"].Visible = false;
                this.ribbonPopup1.Items["Down"].Visible = false;
                this.ribbonPopup1.Items["UDSeparator"].Visible = false;
                //
                bool bSC = value is IPopupObjectDesignHelper;
                this.ribbonPopup1.Items["ShowPopup"].Visible = bSC;
                this.ribbonPopup1.Items["ClosePopup"].Visible = bSC;
                this.ribbonPopup1.Items["SCSeparator"].Visible = bSC;
                this.ribbonPopup1.Items["Refresh"].Visible = true;
                //
                return true;
            }
        }

        /// <summary>
        /// 加载子项DropDownButtonItem列表
        /// </summary>
        internal protected IDropDownButtonItem CreateNewItemTypeDropDownButton
        {
            get
            {
                return this.ribbonPopup1.Items["AddItem"] as IDropDownButtonItem;
            }
        }

        /// <summary>
        /// node1 与 node2 互换位置
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="nodes"></param>
        protected bool ExchangeNode(TreeNode node1, TreeNode node2, TreeNodeCollection nodes)
        {
            if (node1 == null || node2 == null || node1 == node2) return false;
            //
            int index1 = nodes.IndexOf(node1);
            int index2 = nodes.IndexOf(node2);
            //
            if (index1 == index2) return false;
            if ((index1 < 0) || (index1 >= nodes.Count)) return false;
            if ((index2 < 0) || (index2 >= nodes.Count)) return false;
            //
            if (index1 < index2)
            {
                nodes.Remove(node2);
                nodes.Remove(node1);
                nodes.Insert(index1, node2);
                nodes.Insert(index2, node1);
            }
            else
            {
                nodes.Remove(node1);
                nodes.Remove(node2);
                nodes.Insert(index2, node1);
                nodes.Insert(index1, node2);
            }
            //
            return true;
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="pBaseItem"></param>
        /// <param name="nodes"></param>
        protected void BuildTree_DG(IObjectDesignHelper pObjectDesignHelper, View.NodeViewItemCollection nodes)
        {
            if (pObjectDesignHelper == null) return;
            //
            if (!this.FiltrationBaseItem(pObjectDesignHelper)) return;
            //
            View.NodeViewItem node = this.CreateNode(pObjectDesignHelper);
            node.Name = pObjectDesignHelper.Name;
            node.Tag = pObjectDesignHelper;
            node.Text = this.GetTypeDescription(node);
            nodes.Add(node);
            //
            this.BuildTreeEx(node);
            //
            ICollectionObjectDesignHelper pCollectionObjectDesignHelper = pObjectDesignHelper as ICollectionObjectDesignHelper;
            if (pCollectionObjectDesignHelper != null)
            {
                foreach (IObjectDesignHelper one in pCollectionObjectDesignHelper.List)
                {
                    if(one != null) this.BuildTree_DG(one, node.NodeViewItems);
                }
            }
        }

        protected virtual void BuildTreeEx(View.NodeViewItem node) { }

        /// <summary>
        /// 当节点对应的组建被选中置顶后截获该组件,用于实现额外的操作
        /// </summary>
        /// <param name="pComponent"></param>
        /// <returns></returns>
        internal protected virtual void SelectedComponent(Component component)
        {

        }

        /// <summary>
        /// 用来设置创建实例的属性信息
        /// </summary>
        /// <param name="pComponent">创建的组建</param>
        /// <returns></returns>
        protected virtual bool SetCreateTypeInfo(IComponent pComponent)
        {
            if (this.m_pObjectDesignHelper is ICanvasItem)
            {
                ISetBaseItemHelper pSetBaseItemHelper = pComponent as ISetBaseItemHelper;
                if (pSetBaseItemHelper != null)
                {
                    pSetBaseItemHelper.SetLocation(((IBaseItem)this.m_pObjectDesignHelper).Location);
                }
            }
            //
            IObjectDesignHelper item = pComponent as IObjectDesignHelper;
            if (item == null) return false;
            item.Name = pComponent.Site.Name;
            item.Text = item.Name;
            //
            return true;
        }

        /// <summary>
        /// 插入树节点
        /// </summary>
        /// <param name="depthList">深度节点位置</param>
        /// <param name="index">插入位置</param>
        /// <param name="node">插入的节点</param>
        protected virtual void InsertTreeNode(int[] depthList, int index, View.NodeViewItem node)
        {
            if (node == null) return;
            //
            View.NodeViewItemCollection nodes = this.treeView1.NodeViewItems;
            foreach (int one in depthList)
            {
                if (nodes.Count <= 0) break;
                //
                int iNum = one;
                if (iNum < 0) iNum = 0;
                if (iNum >= nodes.Count) iNum = nodes.Count - 1;
                nodes = nodes[iNum].NodeViewItems;
            }
            if (index < 0) index = 0;
            if (index > nodes.Count) index = nodes.Count;
            nodes.Insert(index, node);
        }

        /// <summary>
        /// 创建节点类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual View.NodeViewItem CreateNode(object value)
        {
            return new View.NodeViewItem();
        }

        /// <summary>
        /// 是否展现基础节点，返回 true展现 false不展示
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool FiltrationBaseItem(object value)
        {
            return value != null;
        }

        /// <summary>
        /// 该子项是否可以选中，返回 true可选 false不可选
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual bool FiltrationSelected(View.NodeViewItem node)
        {
            return node != null;
        }

        /// <summary>
        /// 节点是否展现Popup，返回 true展现 false不展示
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual bool FiltrationShowPopup(View.NodeViewItem node)
        {
            return node != null;
        }

        /// <summary>
        /// 节点描述名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual string GetTypeDescription(View.NodeViewItem node)
        {
            IObjectDesignHelper pObjectDesignHelper = node.Tag as IObjectDesignHelper;
            return pObjectDesignHelper == null ? "null" : pObjectDesignHelper.Text + "（" + pObjectDesignHelper.Name + "）";
        }

        /// <summary>
        /// 默认的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected abstract Type[] CreateNewItemTypes();

        /// <summary>
        /// 各类型 对应的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, Type[]> CreateNewItemTypesDictionary();

        private void btnTopForm_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.btnTopForm.Checked;
        }

        private void btnExpand_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void btnCollapse_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        /// <summary>
        /// 展开所有节点
        /// </summary>
        public void CollapseAll()
        { 
            this.treeView1.CollapseAll(); 
        }

        /// <summary>
        /// 折叠所有节点
        /// </summary>
        public void ExpandAll()
        { 
            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// 折叠当前节点
        /// </summary>
        public void Expand()
        {
            TreeNode node = this.ribbonPopup1.Tag as TreeNode;
            if (node == null) return;
            node.Expand();
        }

        /// <summary>
        /// 获取弹出菜子项单项
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        protected BaseItem GetPopupItem(string strName)
        {
            return this.ribbonPopup1.Items[strName];
        }

        /// <summary>
        /// 添加弹出菜子项单项
        /// </summary>
        /// <param name="index"></param>
        /// <param name="baseItem"></param>
        protected bool InsertPopupItem(int index, BaseItem baseItem)
        {
            foreach (BaseItem one in this.ribbonPopup1.Items)
            {
                if (one.Name == baseItem.Name) return false;
            }
            //
            this.ribbonPopup1.Items.Insert(index, baseItem);
            //
            return true;
        }

        #region 类型嵌入
        public static Type[] Catalogue_Label = new Type[] { typeof(LabelItem),  typeof(LabelExItem), typeof(ImageLabelItem) };
        public static Type[] Catalogue_LinkLabel = new Type[] { typeof(LinkLabelItem), typeof(ImageLinkLabelItem) };
        public static Type[] Catalogue_Button = new Type[] { typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(DropDownButtonItem), typeof(SplitButtonItem) };
        public static Type[] Catalogue_GlyphButton = new Type[] { typeof(GlyphButtonItem) };
        public static Type[] Catalogue_ButtonEx = new Type[] { typeof(BaseButtonExItem), typeof(CheckButtonExItem), typeof(DropDownButtonExItem), typeof(SplitButtonExItem) };
        public static Type[] Catalogue_DescriptionButton = new Type[] { typeof(DescriptionButtonItem) };
        public static Type[] Catalogue_CheckBox = new Type[] { typeof(CheckBoxItem), typeof(ImageCheckBoxItem) };
        public static Type[] Catalogue_RadioButton = new Type[] { typeof(RadioButtonItem), typeof(ImageRadioButtonItem) };
        public static Type[] Catalogue_InputBox = new Type[] { typeof(TextBoxItem), typeof(IntegerInputBoxItem), typeof(DoubleInputBoxItem), typeof(ButtonTextBoxItem) };
        public static Type[] Catalogue_ComboBox = new Type[] { typeof(ComboBoxItem), typeof(ComboTreeItem), typeof(ComboDateItem)/*, typeof(ComboDateTimeItem)*/ };
        public static Type[] Catalogue_ProcessBar = new Type[] { typeof(ProcessBarItem) };
        public static Type[] Catalogue_RatingStar = new Type[] { typeof(RatingStarItem) };
        public static Type[] Catalogue_ScrollBar = new Type[] { typeof(SliderItem), typeof(ScrollBarItem) };
        public static Type[] Catalogue_Separator = new Type[] { typeof(SeparatorItem) };
        public static Type[] Catalogue_LabelSeparator = new Type[] { typeof(LabelSeparatorItem), typeof(ImageLabelSeparatorItem) };
        public static Type[] Catalogue_ImageBox = new Type[] { typeof(ImageBoxItem), typeof(ImageAreaBoxItem), typeof(ImageZoomableBoxItem) };
        public static Type[] Catalogue_CombineControl = new Type[] { typeof(DateTimeCombineItem), typeof(DegreeCombineItem), typeof(VersionCombinetem) };
        public static Type[] Catalogue_BaseItemStack = new Type[] { typeof(BaseItemStackItem), typeof(BaseItemStackExItem) };
        public static Type[] Catalogue_Canvas = new Type[] { typeof(CanvasItem) };
        public static Type[] Catalogue_PopupPanel = new Type[] { typeof(ContextPopupPanelItem), typeof(DescriptionMenuPopupPanelItem), typeof(RibbonApplicationPopupPanelItem) };
        public static Type[] Catalogue_Bar = new Type[] { typeof(BaseBarItem), typeof(ToolBarItem), typeof(StatusBarItem) };
        public static Type[] Catalogue_ButtonGroup = new Type[] { typeof(ButtonGroupItem) };
        public static Type[] Catalogue_RibbonGallery = new Type[] { typeof(RibbonGalleryItem) };
        public static Type[] Catalogue_RibbonBar = new Type[] { typeof(RibbonBarItem) };
        public static Type[] Catalogue_RibbonPage = new Type[] { typeof(RibbonPageItem) };
        public static Type[] Catalogue_RibbonQuickAccessToolbar = new Type[] { typeof(RibbonQuickAccessToolbarItem) };
        public static Type[] Catalogue_RibbonStatusBar = new Type[] { typeof(RibbonStatusBarItem) };
        public static Type[] Catalogue_RibbonControl = new Type[] { typeof(RibbonControlItem) };
        public static Type[] Catalogue_ListBox = new Type[] { typeof(View.ViewItemListBoxItem) };
        public static Type[] Catalogue_Tree = new Type[] { typeof(View.NodeViewItemTreeItem) };
        public static Type[] Catalogue_GridView = new Type[] { typeof(View.GridViewItemListBoxItem) };
        public static Type[] Catalogue_GridNodeView = new Type[] { typeof(View.GridNodeViewItemTreeItem) };

        public static Type[] CombineTypes(params Type[][] typesArray)
        {
            List<Type> list = new List<Type>();
            foreach (Type[] one in typesArray) 
            {
                list.AddRange(one);
            }
            return list.ToArray();
        }

        public static Type[] GetRibbonBarNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                Catalogue_ButtonEx,
                //Catalogue_DescriptionButton,
                Catalogue_CheckBox,
                Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                Catalogue_ProcessBar,
                Catalogue_RatingStar,
                Catalogue_ScrollBar,
                Catalogue_Separator,
                Catalogue_LabelSeparator,
                Catalogue_ImageBox,
                Catalogue_CombineControl,
                Catalogue_BaseItemStack,
                Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                Catalogue_ButtonGroup,
                Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                Catalogue_ListBox,
                Catalogue_Tree,
                Catalogue_GridView,
                Catalogue_GridNodeView
                );
        }
        public static Type[] GetBaseBarNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                Catalogue_ButtonEx,
                //Catalogue_DescriptionButton,
                Catalogue_CheckBox,
                Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                Catalogue_ProcessBar,
                Catalogue_RatingStar,
                Catalogue_ScrollBar,
                Catalogue_Separator,
                Catalogue_LabelSeparator,
                Catalogue_ImageBox,
                Catalogue_CombineControl,
                Catalogue_BaseItemStack,
                //Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                Catalogue_ButtonGroup,
                Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                Catalogue_ListBox,
                Catalogue_Tree,
                Catalogue_GridView,
                Catalogue_GridNodeView
                );
        }
        public static Type[] GetBaseItemStackNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                Catalogue_ButtonEx,
                Catalogue_DescriptionButton,
                Catalogue_CheckBox,
                Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                Catalogue_ProcessBar,
                Catalogue_RatingStar,
                Catalogue_ScrollBar,
                Catalogue_Separator,
                Catalogue_LabelSeparator,
                Catalogue_ImageBox,
                Catalogue_CombineControl,
                Catalogue_BaseItemStack,
                Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                Catalogue_ButtonGroup,
                Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                Catalogue_ListBox,
                Catalogue_Tree,
                Catalogue_GridView,
                Catalogue_GridNodeView
                );
        }
        public static Type[] GetButtonGroupNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Button,
                Catalogue_GlyphButton
                );
        }
        public static Type[] GetPopupNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                //Catalogue_ButtonEx,
                //Catalogue_DescriptionButton,
                Catalogue_CheckBox,
                Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                Catalogue_ProcessBar,
                Catalogue_RatingStar,
                Catalogue_ScrollBar,
                Catalogue_Separator,
                Catalogue_LabelSeparator,
                Catalogue_ImageBox,
                Catalogue_CombineControl,
                Catalogue_BaseItemStack,
                //Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                Catalogue_ButtonGroup,
                Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                Catalogue_ListBox,
                Catalogue_Tree,
                Catalogue_GridView,
                Catalogue_GridNodeView
                );
        }
        public static Type[] GetRibbonQuickAccessToolbarNewItemTypes()
        {
            return CombineTypes(
                //Catalogue_Label,
                //Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                //Catalogue_ButtonEx,
                //Catalogue_DescriptionButton,
                //Catalogue_CheckBox,
                //Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                //Catalogue_ProcessBar,
                //Catalogue_RatingStar,
                //Catalogue_ScrollBar,
                Catalogue_Separator//,
                //Catalogue_LabelSeparator,
                //Catalogue_ImageBox,
                //Catalogue_CombineControl,
                //Catalogue_BaseItemStack,
                //Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                //Catalogue_ButtonGroup,
                //Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                //Catalogue_ListBox,
                //Catalogue_Tree,
                //Catalogue_GridView,
                //Catalogue_GridNodeView
                );
        }
        public static Type[] GetRibbonPageContentContainerNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                //Catalogue_ButtonEx,
                //Catalogue_DescriptionButton,
                //Catalogue_CheckBox,
                //Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                //Catalogue_ProcessBar,
                //Catalogue_RatingStar,
                //Catalogue_ScrollBar,
                Catalogue_Separator//,
                //Catalogue_LabelSeparator,
                //Catalogue_ImageBox,
                //Catalogue_CombineControl,
                //Catalogue_BaseItemStack,
                //Catalogue_Canvas,
                //Catalogue_PopupPanel,
                //Catalogue_Bar,
                //Catalogue_ButtonGroup,
                //Catalogue_RibbonGallery,
                //Catalogue_RibbonBar,
                //Catalogue_RibbonPage,
                //Catalogue_RibbonQuickAccessToolbar,
                //Catalogue_RibbonStatusBar,
                //Catalogue_RibbonControl,
                //Catalogue_ListBox,
                //Catalogue_Tree,
                //Catalogue_GridView,
                //Catalogue_GridNodeView
                );
        }
        public static Type[] GetCreateNewItemTypes()
        {
            return CombineTypes(
                Catalogue_Label,
                Catalogue_LinkLabel,
                Catalogue_Button,
                Catalogue_GlyphButton,
                Catalogue_ButtonEx,
                Catalogue_DescriptionButton,
                Catalogue_CheckBox,
                Catalogue_RadioButton,
                Catalogue_InputBox,
                Catalogue_ComboBox,
                Catalogue_ProcessBar,
                Catalogue_RatingStar,
                Catalogue_ScrollBar,
                Catalogue_Separator,
                Catalogue_LabelSeparator,
                Catalogue_ImageBox,
                Catalogue_CombineControl,
                Catalogue_BaseItemStack,
                Catalogue_Canvas,
                Catalogue_PopupPanel,
                Catalogue_Bar,
                Catalogue_ButtonGroup,
                Catalogue_RibbonGallery,
                Catalogue_RibbonBar,
                Catalogue_RibbonPage,
                Catalogue_RibbonQuickAccessToolbar,
                Catalogue_RibbonStatusBar,
                Catalogue_RibbonControl,
                Catalogue_ListBox,
                Catalogue_Tree,
                Catalogue_GridView,
                Catalogue_GridNodeView
                );
        }

        public static Dictionary<string, Type[]> GetCreateNewItemTypesDictionary()
        {
            Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = new Dictionary<string, Type[]>();
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleLeftItem",
                new Type[] { typeof(MenuButtonItem), typeof(SeparatorItem)  });
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleRightItem",
                new Type[] { typeof(LabelSeparatorItem), typeof(BaseButtonItem), typeof(SeparatorItem) });
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelBottomItem",
                new Type[] {  typeof(BaseButtonItem), typeof(SeparatorItem) });
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseBar",
                GetBaseBarNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseBarItem",
                GetBaseBarNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonBar",
                GetRibbonBarNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonBarItem",
                GetRibbonBarNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStack",
                GetBaseItemStackNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackItem",
                GetBaseItemStackNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackEx",
                GetBaseItemStackNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackExItem",
                GetBaseItemStackNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ButtonGroup",
                GetButtonGroupNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ButtonGroupItem",
                GetButtonGroupNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.DropDownButton",
                GetPopupNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.DropDownButtonItem",
                GetPopupNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonGallery",
                new Type[] { typeof(RibbonGalleryRowItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonGalleryItem",
                new Type[] { typeof(RibbonGalleryRowItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonGalleryRow",
                new Type[] { typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(ButtonItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonGalleryRowItem",
                new Type[] { typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(ButtonItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.MenuButton",
                CombineTypes(Catalogue_DescriptionButton, GetPopupNewItemTypes())
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.MenuButtonItem",
                CombineTypes(Catalogue_DescriptionButton, GetPopupNewItemTypes())
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonPage",
                new Type[] { typeof(RibbonBarItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonPageItem",
                new Type[] { typeof(RibbonBarItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ContextPopupPanel",
                GetPopupNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ContextPopupPanelItem",
                GetPopupNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonQuickAccessToolbar",
                GetRibbonQuickAccessToolbarNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonQuickAccessToolbarItem",
                GetRibbonQuickAccessToolbarNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.SplitButton",
                GetPopupNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.SplitButtonItem",
                GetPopupNewItemTypes()
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonStatusBar",
                GetBaseBarNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonStatusBarItem",
                GetBaseBarNewItemTypes()
                );
            //
            return typeCreateNewItemTypesDictionary;
        }
        #endregion

    }
}
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
    public partial class BaseItemCollectionDesignerForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm//Form
    {
        public GetServiceCallBack GetServiceCallBackEx;

        private IBaseItem m_pBaseItem = null;

        public BaseItemCollectionDesignerForm(IBaseItem pBaseItem)
        {
            InitializeComponent();
            this.m_pBaseItem = pBaseItem;
            this.BuildPopup();
            this.BuildTree_DG(this.m_pBaseItem, this.nodeViewItemTree1.NodeViewItems);
            if (this.nodeViewItemTree1.NodeViewItems.Count > 0) this.nodeViewItemTree1.NodeViewItems[0].Expand();
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
            else
            {
                ICollectionItem pCollectionItem = nodeParent.Tag as ICollectionItem;
                if (pCollectionItem == null)
                {
                    this.PopupItemOperation(pBaseItem.Name, null, node);
                }
                else
                {
                    this.PopupItemOperation(pBaseItem.Name, pCollectionItem.BaseItems, node);
                }
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
                            }
                        }
                        break;
                    case "AddItem":
                        break;
                    case "ClearItems":
                        if (node.Tag is ICollectionItem)
                        {
                            ICollectionItem pCollectionItem = node.Tag as ICollectionItem;
                            if (!pCollectionItem.BaseItems.Locked && pCollectionItem.BaseItems.Count > 0)
                            {
                                foreach (Component one in pCollectionItem.BaseItems)
                                {
                                    if (one != null) one.Dispose();
                                }
                                pCollectionItem.BaseItems.Clear();
                                node.NodeViewItems.Clear();
                            }
                        }
                        else if (node.Tag is IList)
                        {
                            IList list = node.Tag as IList;
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
                                            this.nodeViewItemTree1.SelectedNode = node;
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
                                            this.nodeViewItemTree1.SelectedNode = node;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "ShowPopup":
                        IPopupOwner pPopupOwner = node.Tag as IPopupOwner;
                        if (pPopupOwner != null)
                        {
                            pPopupOwner.ShowPopup();
                        }
                        break;
                    case "ClosePopup":
                        IPopupOwner pPopupOwner2 = node.Tag as IPopupOwner;
                        if (pPopupOwner2 != null)
                        {
                            pPopupOwner2.ClosePopup();
                        }
                        break;
                    case "Refresh":
                        IBaseItem pBaseItem = node.Tag as IBaseItem;
                        if (pBaseItem != null)
                        {
                            pBaseItem.Refresh();
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
                        if (node.Tag is ICollectionItem)
                        {
                            ICollectionItem pCollectionItem = node.Tag as ICollectionItem;
                            if (!pCollectionItem.BaseItems.Locked && pCollectionItem.BaseItems.Count > 0)
                            {
                                foreach (Component one in pCollectionItem.BaseItems)
                                {
                                    if (one != null) one.Dispose();
                                }
                                pCollectionItem.BaseItems.Clear();
                                node.NodeViewItems.Clear();
                            }
                        }
                        else if (node.Tag is IList)
                        {
                            IList list = node.Tag as IList;
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
                        IPopupOwner pPopupOwner = node.Tag as IPopupOwner;
                        if (pPopupOwner != null)
                        {
                            pPopupOwner.ShowPopup();
                        }
                        break;
                    case "ClosePopup":
                        IPopupOwner pPopupOwner2 = node.Tag as IPopupOwner;
                        if (pPopupOwner2 != null)
                        {
                            pPopupOwner2.ClosePopup();
                        }
                        break;
                    case "Refresh":
                        IBaseItem pBaseItem = node.Tag as IBaseItem;
                        if (pBaseItem != null)
                        {
                            pBaseItem.Refresh();
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
        /// <param name="pCollectionItem"></param>
        private void CreateNewItemTypePopup(Type type)
        {
            IDropDownButtonItem newItems = this.CreateNewItemTypeDropDownButton;
            if (newItems == null) return;
            newItems.BaseItems.Clear();
            //
            Type[] types;
            if (this.CreateNewItemTypesDictionary() == null ||
                !this.CreateNewItemTypesDictionary().ContainsKey(type.FullName))
            {
                types = this.CreateNewItemTypes();
            }
            else
            {
                types = this.CreateNewItemTypesDictionary()[type.FullName];
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
        void CreateItem_MouseClick(object sender, MouseEventArgs e)
        {
            BaseButtonItem item = sender as BaseButtonItem;
            if (item == null) return;
            //
            View.NodeViewItem node = this.CreateNewItemTypeDropDownButton.Tag as View.NodeViewItem;
            if (node == null) return;
            //
            ICollectionItem pCollectionItem = node.Tag as ICollectionItem;
            if (pCollectionItem != null)
            {
                this.Add(node, item.Tag as Type, pCollectionItem.BaseItems);
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
                        this.BuildTree_DG(pComponent as IBaseItem, parent.NodeViewItems);
                    }
                }
            }
        }

        private void nodeViewItemTree1_SelectedNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (this.nodeViewItemTree1.Nodes.Contains(e.Node)) return;
            //
            View.NodeViewItem node = e.NewValue as View.NodeViewItem;
            if (node == null) return;
            //
            if (!this.FiltrationSelected(node.Tag)) return;
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

        private void nodeViewItemTree1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            //
            View.NodeViewItem node = this.nodeViewItemTree1.TryGetNodeViewItemFromPoint(e.Location);
            if (node != this.nodeViewItemTree1.SelectedNode) return;
            //
            this.ribbonPopup1.Tag = node;
            //
            if (!this.FiltrationShowPopup(node.Tag))
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
                    this.CreateNewItemTypePopup(node.Tag.GetType());
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
                    if (!this.FiltrationPopupItem(nodeParent.Tag as IList, node.Tag)) return;
                }
                else
                {
                    ICollectionItem pCollectionItem = nodeParent.Tag as ICollectionItem;
                    if (pCollectionItem == null)
                    {
                        if (!this.FiltrationPopupItem(null, node.Tag)) return;
                    }
                    else
                    {
                        if (!this.FiltrationPopupItem(pCollectionItem.BaseItems, node.Tag)) return;
                    }
                }
            }
            //
            this.ribbonPopup1.Show(this.nodeViewItemTree1.PointToScreen(e.Location));
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
                if (value is ICollectionItem)
                {
                    ICollectionItem pCollectionItem = value as ICollectionItem;
                    bAC = (pCollectionItem == null || pCollectionItem.BaseItems.Locked) ? false : true;
                    bEEC = (pCollectionItem == null || pCollectionItem.BaseItems.Count <= 0) ? false : true;
                }
                else if (value is IList)
                {
                    IList list = value as IList;
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
                bool bSC = value is IPopupOwner;
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
                if (value is ICollectionItem)
                {
                    ICollectionItem pCollectionItem = value as ICollectionItem;
                    bAC = (pCollectionItem == null || pCollectionItem.BaseItems.Locked) ? false : true;
                    bEEC = (pCollectionItem == null || pCollectionItem.BaseItems.Count <= 0) ? false : true;
                }
                else if (value is IList)
                {
                    IList list = value as IList;
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
                bool bSC = value is IPopupOwner;
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
        protected void BuildTree_DG(IBaseItem pBaseItem, GISShare.Controls.WinForm.WFNew.View.NodeViewItemCollection nodes)
        {
            if (pBaseItem == null) return;
            //
            if (!this.FiltrationBaseItem(pBaseItem)) return;
            //
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = this.CreateNode(pBaseItem);
            node.Name = pBaseItem.Name;
            node.Text = this.GetTypeDescription(pBaseItem);
            node.Tag = pBaseItem;
            nodes.Add(node);
            //
            ICollectionItem pCollectionItem = pBaseItem as ICollectionItem;
            if (pCollectionItem != null)
            {
                foreach (IBaseItem one in pCollectionItem.BaseItems)
                {
                    this.BuildTree_DG(one, node.NodeViewItems);
                }
            }
        }

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
            IBaseItem item = pComponent as IBaseItem;
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
            View.NodeViewItemCollection nodes = this.nodeViewItemTree1.NodeViewItems;
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
            return new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
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
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool FiltrationSelected(object value)
        {
            return value != null;
        }

        /// <summary>
        /// 节点是否展现Popup，返回 true展现 false不展示
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool FiltrationShowPopup(object value)
        {
            return value != null;
        }

        /// <summary>
        /// 节点描述名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual string GetTypeDescription(object value)
        {
            IBaseItem pBaseItem = value as IBaseItem;
            return pBaseItem == null ? "null" : pBaseItem.Text + "（" + pBaseItem.Name + "）";
        }

        /// <summary>
        /// 默认的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected virtual Type[] CreateNewItemTypes()
        {
            return BaseItemCollectionDesignerForm.GetCreateNewItemTypes();
        }

        /// <summary>
        /// 各类型 对应的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        {
            return BaseItemCollectionDesignerForm.GetCreateNewItemTypesDictionary();
        }

        private void btnTopForm_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.btnTopForm.Checked;
        }

        private void btnExpand_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.ExpandAll();
        }

        private void btnCollapse_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.CollapseAll();
        }

        /// <summary>
        /// 展开所有节点
        /// </summary>
        public void CollapseAll()
        {
            this.nodeViewItemTree1.CollapseAll();
        }

        /// <summary>
        /// 折叠所有节点
        /// </summary>
        public void ExpandAll()
        {
            this.nodeViewItemTree1.ExpandAll();
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
        public static Type[] GetCreateNewItemTypes()
        {
            return new Type[] { 
                typeof(LabelItem), 
                typeof(LabelExItem), 
                typeof(ImageLabelItem), 
                typeof(LinkLabelItem), 
                typeof(ImageLinkLabelItem),  
                //
                typeof(BaseButtonItem),
                typeof(CheckButtonItem),
                typeof(DropDownButtonItem),
                typeof(SplitButtonItem), 
                typeof(ButtonItem),
                typeof(DescriptionButtonItem), 
                typeof(BaseButtonExItem),
                typeof(CheckButtonExItem),
                typeof(DropDownButtonExItem), 
                typeof(SplitButtonExItem),
                typeof(ButtonExItem), 
                typeof(GlyphButtonItem), 
                //
                typeof(CheckBoxItem), 
                typeof(ImageCheckBoxItem), 
                //
                typeof(RadioButtonItem),
                typeof(ImageRadioButtonItem),
                //
                typeof(TextBoxItem),
                typeof(IntegerInputBoxItem),
                typeof(DoubleInputBoxItem),
                typeof(ButtonTextBoxItem), 
                //
                typeof(ComboBoxItem), 
                typeof(ComboTreeItem),
                typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                //
                typeof(ProcessBarItem), 
                //
                typeof(RatingStarItem),
                //
                typeof(SliderItem),
                typeof(ScrollBarItem),
                //
                typeof(SeparatorItem),
                typeof(LabelSeparatorItem), 
                typeof(ImageLabelSeparatorItem),
                //
                typeof(ButtonGroupItem),
                typeof(RibbonGalleryItem),
                typeof(BaseItemStackItem),
                typeof(BaseItemStackExItem),
                //
                typeof(CanvasItem),
                //
                typeof(ImageBoxItem),
                typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)};
        }

        public static Dictionary<string, Type[]> GetCreateNewItemTypesDictionary()
        {
            Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = new Dictionary<string, Type[]>();
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleLeftItem",
                new Type[] { typeof(MenuButtonItem), 
                    typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleRightItem",
                new Type[] { typeof(LabelSeparatorItem), 
                    typeof(BaseButtonItem), 
                    typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelBottomItem",
                new Type[] { typeof(BaseButtonItem), 
                    typeof(SeparatorItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseBar",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem), 
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem), 
                    typeof(IntegerInputBoxItem), 
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseBarItem",
                new Type[] {
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem), 
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem), 
                    typeof(IntegerInputBoxItem), 
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)  }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonBar",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonBarItem",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStack",
                new Type[] {  
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackItem",
                new Type[] {  
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackEx",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.BaseItemStackExItem",
                new Type[] {  
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(DescriptionButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    typeof(LabelSeparatorItem), 
                    typeof(ImageLabelSeparatorItem),
                    //
                    typeof(ButtonGroupItem),
                    typeof(RibbonGalleryItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ButtonGroup",
                new Type[] { typeof(BaseButtonItem), 
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(GlyphButtonItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ButtonGroupItem",
                new Type[] { typeof(BaseButtonItem), 
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem), 
                    typeof(ButtonItem),
                    typeof(GlyphButtonItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.DropDownButton",
                new Type[] {   
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.DropDownButtonItem",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
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
                new Type[] { typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(ButtonItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonGalleryRowItem",
                new Type[] { typeof(BaseButtonItem), 
                    typeof(CheckButtonItem), 
                    typeof(ButtonItem) }
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.MenuButton",
                new Type[] { typeof(DescriptionButtonItem),
                    //
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.MenuButtonItem",
                new Type[] { typeof(DescriptionButtonItem),
                    //
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
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
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.ContextPopupPanelItem",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem) ,
                typeof(ImageZoomableBoxItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonQuickAccessToolbar",
                new Type[] { typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem),
                    typeof(ButtonItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(LabelItem),
                    typeof(ImageLabelItem),
                    typeof(LinkLabelItem),
                    typeof(ImageLinkLabelItem), 
                    //
                    typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonQuickAccessToolbarItem",
                new Type[] { typeof(BaseButtonItem),
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem),
                    typeof(SplitButtonItem),
                    typeof(ButtonItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(LabelItem),
                    typeof(ImageLabelItem),
                    typeof(LinkLabelItem),
                    typeof(ImageLinkLabelItem), 
                    //
                    typeof(SeparatorItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.SplitButton",
                new Type[] {
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.SplitButtonItem",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem),
                    typeof(CheckButtonItem), 
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem),
                    typeof(IntegerInputBoxItem),
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(SeparatorItem),
                    null,
                    typeof(LabelSeparatorItem),
                    typeof(BaseItemStackItem),
                    typeof(BaseItemStackExItem),
                    //
                    typeof(CanvasItem),
                    //
                    typeof(ImageBoxItem),
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem)}
                );
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonStatusBar",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem), 
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem), 
                    typeof(IntegerInputBoxItem), 
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem), 
                    //
                    typeof(SeparatorItem),
                    //
                    typeof(ImageBoxItem) ,
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonStatusBarItem",
                new Type[] { 
                    typeof(LabelItem), 
                    typeof(LabelExItem), 
                    typeof(ImageLabelItem), 
                    typeof(LinkLabelItem), 
                    typeof(ImageLinkLabelItem),  
                    //
                    typeof(BaseButtonItem), 
                    typeof(CheckButtonItem),
                    typeof(DropDownButtonItem), 
                    typeof(SplitButtonItem),
                    typeof(ButtonItem), 
                    typeof(BaseButtonExItem),
                    typeof(CheckButtonExItem),
                    typeof(DropDownButtonExItem), 
                    typeof(SplitButtonExItem),
                    typeof(ButtonExItem),
                    typeof(GlyphButtonItem),
                    //
                    typeof(CheckBoxItem), 
                    typeof(ImageCheckBoxItem), 
                    //
                    typeof(RadioButtonItem),
                    typeof(ImageRadioButtonItem),
                    //
                    typeof(TextBoxItem), 
                    typeof(IntegerInputBoxItem), 
                    typeof(DoubleInputBoxItem),
                    typeof(ButtonTextBoxItem),
                    //
                    typeof(ComboBoxItem), 
                    typeof(ComboTreeItem),
                    typeof(ComboDateItem),
                typeof(ComboDateTimeItem),
                    //
                    typeof(ProcessBarItem), 
                    //
                    typeof(RatingStarItem),
                    //
                    typeof(SliderItem),
                    typeof(ScrollBarItem),
                    //
                    typeof(SeparatorItem),
                    //
                    typeof(ImageBoxItem) ,
                    typeof(ImageAreaBoxItem),
                typeof(ImageZoomableBoxItem) }
                );
            //
            return typeCreateNewItemTypesDictionary;
        }
        #endregion
    }
}
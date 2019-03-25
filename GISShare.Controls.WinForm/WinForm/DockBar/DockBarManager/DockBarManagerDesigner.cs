using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarManagerDesigner : System.ComponentModel.Design.ComponentDesigner
    {
        private DockBarManager m_DockBarManager = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    if (this.m_DockBarManager.MenuBar  == null)
                    { verbs.Add(new DesignerVerb("创建主菜单", new EventHandler(CreateMenuBar))); }
                    verbs.Add(new DesignerVerb("在顶部创建工具条", new EventHandler(CreateTopToolBar)));
                    verbs.Add(new DesignerVerb("在左边创建工具条", new EventHandler(CreateLeftToolBar)));
                    verbs.Add(new DesignerVerb("在右边创建工具条", new EventHandler(CreateRightToolBar)));
                    verbs.Add(new DesignerVerb("在底部创建工具条", new EventHandler(CreateBottomToolBar)));
                    if (this.m_DockBarManager.StatusBar == null)
                    { verbs.Add(new DesignerVerb("创建状态栏", new EventHandler(CreateStatusBar))); }
                    verbs.Add(new DesignerVerb("创建快捷菜单", new EventHandler(CreateContextMenu)));
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_DockBarManager = base.Component as DockBarManager;
            if (this.m_DockBarManager == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("DockBarManager == null");
                return;
            }
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host != null)
            {
                System.Windows.Forms.Form form = host.RootComponent as System.Windows.Forms.Form;
                this.m_DockBarManager.ParentForm = form;
            }
        }

        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host != null)
            {
                if (this.m_DockBarManager.DockBarDockAreaLeft == null)
                {
                    //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("");
                    this.m_DockBarManager.DockBarDockAreaLeft = host.CreateComponent(typeof(DockBarDockAreaLeft)) as DockBarDockAreaLeft;
                    this.m_DockBarManager.ParentForm.Controls.Add(this.m_DockBarManager.DockBarDockAreaLeft);
                }
                if (this.m_DockBarManager.DockBarDockAreaRight == null)
                {
                    this.m_DockBarManager.DockBarDockAreaRight = host.CreateComponent(typeof(DockBarDockAreaRight)) as DockBarDockAreaRight;
                    this.m_DockBarManager.ParentForm.Controls.Add(this.m_DockBarManager.DockBarDockAreaRight);
                }
                if (this.m_DockBarManager.DockBarDockAreaTop == null)
                {
                    this.m_DockBarManager.DockBarDockAreaTop = host.CreateComponent(typeof(DockBarDockAreaTop)) as DockBarDockAreaTop;
                    this.m_DockBarManager.ParentForm.Controls.Add(this.m_DockBarManager.DockBarDockAreaTop);
                }
                if (this.m_DockBarManager.DockBarDockAreaBottom == null)
                {
                    this.m_DockBarManager.DockBarDockAreaBottom = host.CreateComponent(typeof(DockBarDockAreaBottom)) as DockBarDockAreaBottom;
                    this.m_DockBarManager.ParentForm.Controls.Add(this.m_DockBarManager.DockBarDockAreaBottom);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (this.m_DockBarManager != null && host != null)
                {
                    if (this.m_DockBarManager.DockBarDockAreaTop != null)
                    {
                        this.m_DockBarManager.ParentForm.Controls.Remove(this.m_DockBarManager.DockBarDockAreaTop);
                        host.DestroyComponent(this.m_DockBarManager.DockBarDockAreaTop);
                    }
                    if (this.m_DockBarManager.DockBarDockAreaBottom != null)
                    {
                        this.m_DockBarManager.ParentForm.Controls.Remove(this.m_DockBarManager.DockBarDockAreaBottom);
                        host.DestroyComponent(this.m_DockBarManager.DockBarDockAreaBottom);
                    }
                    if (this.m_DockBarManager.DockBarDockAreaLeft != null)
                    {
                        this.m_DockBarManager.ParentForm.Controls.Remove(this.m_DockBarManager.DockBarDockAreaLeft);
                        host.DestroyComponent(this.m_DockBarManager.DockBarDockAreaLeft);
                    }
                    if (this.m_DockBarManager.DockBarDockAreaRight != null)
                    {
                        this.m_DockBarManager.ParentForm.Controls.Remove(this.m_DockBarManager.DockBarDockAreaRight);
                        host.DestroyComponent(this.m_DockBarManager.DockBarDockAreaRight);
                    }
                }
            }
            base.Dispose(disposing);
        }

        private void CreateMenuBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            MenuBar menuBar = host.CreateComponent(typeof(MenuBar)) as MenuBar;
            if (menuBar == null) return;
            menuBar.Text = menuBar.Name;
            menuBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.ParentForm.MainMenuStrip = menuBar;
            this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(menuBar);
            this.m_DockBarManager.MenuBar = menuBar;
        }

        private void CreateTopToolBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            ToolBar toolBar = host.CreateComponent(typeof(ToolBar)) as ToolBar;
            if (toolBar == null) return;
            toolBar.Text = toolBar.Name;
            toolBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(toolBar);
            this.m_DockBarManager.ToolBars.Add(toolBar);
        }

        private void CreateBottomToolBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            ToolBar toolBar = host.CreateComponent(typeof(ToolBar)) as ToolBar;
            if (toolBar == null) return;
            toolBar.Text = toolBar.Name;
            toolBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.DockBarDockAreaBottom.Controls.Add(toolBar);
            this.m_DockBarManager.ToolBars.Add(toolBar);
        }

        private void CreateLeftToolBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            ToolBar toolBar = host.CreateComponent(typeof(ToolBar)) as ToolBar;
            if (toolBar == null) return;
            toolBar.Text = toolBar.Name;
            toolBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.DockBarDockAreaLeft.Controls.Add(toolBar);
            this.m_DockBarManager.ToolBars.Add(toolBar);
        }

        private void CreateRightToolBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            ToolBar toolBar = host.CreateComponent(typeof(ToolBar)) as ToolBar;
            if (toolBar == null) return;
            toolBar.Text = toolBar.Name;
            toolBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.DockBarDockAreaRight.Controls.Add(toolBar);
            this.m_DockBarManager.ToolBars.Add(toolBar);
        }

        private void CreateStatusBar(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            StatusBar statusBar = host.CreateComponent(typeof(StatusBar)) as StatusBar;
            if (statusBar == null) return;
            statusBar.Text = statusBar.Name;
            statusBar.MinimumSize = new Size(23, 23);
            this.m_DockBarManager.DockBarDockAreaBottom.Controls.Add(statusBar);
            this.m_DockBarManager.StatusBar = statusBar;
        }

        private void CreateContextMenu(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            ContextMenu contextMenu = host.CreateComponent(typeof(ContextMenu)) as ContextMenu;
            if (contextMenu == null) return;
            contextMenu.Text = contextMenu.Name;
            this.m_DockBarManager.ContextMenus.Add(contextMenu);
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemDBCollectionDesignerFormEx baseItemCollectionDesignerForm = new BaseItemDBCollectionDesignerFormEx(this.m_DockBarManager);
            baseItemCollectionDesignerForm.GetServiceCallBackEx = new WFNew.GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerForm.TopMost = true;
            baseItemCollectionDesignerForm.Location = new Point(360, 150);
            baseItemCollectionDesignerForm.Show();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        private class BaseItemDBCollectionDesignerFormEx : BaseItemDBCollectionDesignerForm
        {
            private DockBarManager m_DockBarManager = null;

            private WFNew.ContextPopup m_ContextPopup = new GISShare.Controls.WinForm.WFNew.ContextPopup();
            public BaseItemDBCollectionDesignerFormEx(DockBarManager dockBarManager)
                : base(null)
            {
                this.m_DockBarManager = dockBarManager;
                if (this.m_DockBarManager == null) return;
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node3 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                node3.Name = "m_DockBarManager";
                node3.Text = "浮动工具条管理器";
                node3.ShowNomalState = true;
                node3.Tag = this.m_DockBarManager;
                this.InsertTreeNode(new int[] { 0 }, 0, node3);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                node.Name = "ToolBars";
                node.Text = "浮动工具条集合";
                node.ShowNomalState = true;
                node.Tag = this.m_DockBarManager.ToolBars;
                foreach (ToolBar one in this.m_DockBarManager.ToolBars)
                {
                    if (one != null) this.BuildTree_DG(one, node.NodeViewItems);
                }
                this.InsertTreeNode(new int[] { 0 }, 0, node);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node2 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                node2.Name = "ContextMenus";
                node2.Text = "快捷菜单条集合";
                node2.ShowNomalState = true;
                node2.Tag = this.m_DockBarManager.ContextMenus;
                foreach (ContextMenu one in this.m_DockBarManager.ContextMenus)
                {
                    if (one != null) this.BuildTree_DG(one, node2.NodeViewItems);
                }
                this.InsertTreeNode(new int[] { 0 }, 1, node2);
                //
                //
                //
                this.AppandPopupItem();
            }
            private void AppandPopupItem()
            {
                WFNew.BaseButtonItem item = new WFNew.BaseButtonItem();//0
                item.Name = "Expand_Appand";
                item.Text = "展开该节点";
                item.MouseClick += new MouseEventHandler(Item_MouseClick);
                this.InsertPopupItem(0, item);
                //
                WFNew.BaseButtonItem item2 = new WFNew.BaseButtonItem();//1
                item2.Name = "ExpandAll_Appand";
                item2.Text = "展开所有子节点";
                item2.MouseClick += new MouseEventHandler(Item_MouseClick);
                this.InsertPopupItem(1, item2);
                //
                WFNew.BaseButtonItem item3 = new WFNew.BaseButtonItem();//2
                item3.Name = "Collapse_Appand";
                item3.Text = "折叠该节点";
                item3.MouseClick += new MouseEventHandler(Item_MouseClick);
                this.InsertPopupItem(2, item3);
            }
            void Item_MouseClick(object sender, MouseEventArgs e)
            {
                System.Windows.Forms.ToolStripItem pBaseItem = sender as System.Windows.Forms.ToolStripItem;
                if (pBaseItem == null) return;
                //
                switch (pBaseItem.Name)
                {
                    case "Expand_Appand":
                        this.Expand();
                        break;
                    case "ExpandAll_Appand":
                        this.ExpandAll();
                        break;
                    case "Collapse_Appand":
                        this.CollapseAll();
                        break;
                    default:
                        break;
                }
            }

            protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
            {
                Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = base.CreateNewItemTypesDictionary();
                //
                //typeCreateNewItemTypesDictionary.Add
                //    (
                //    "GISShare.Controls.WinForm.DockBar.DockBarManager",
                //    new Type[] { typeof(MenuBar), typeof(StatusBar) }
                //    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.DockBar.DockBarManager+ToolBarCollection",
                    new Type[] { typeof(ToolBar) }
                    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.DockBar.DockBarManager+ContextMenuCollection",
                    new Type[] { typeof(ContextMenu) }
                    );
                //
                return typeCreateNewItemTypesDictionary;
            }

            protected override bool SetCreateTypeInfo(IComponent pComponent)
            {
                if (pComponent is MenuBar)
                {
                    MenuBar menuBar = pComponent as MenuBar;
                    menuBar.MinimumSize = new Size(23, 23);
                    this.m_DockBarManager.ParentForm.MainMenuStrip = menuBar;
                    this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(menuBar);
                    this.m_DockBarManager.MenuBar = menuBar;
                }
                else if (pComponent is ToolBar)
                {
                    ToolBar toolBar = pComponent as ToolBar;
                    toolBar.MinimumSize = new Size(23, 23);
                    this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(toolBar);
                    //this.m_DockBarManager.ToolBars.Add(toolBar);
                }
                //else if (pComponent is ContextMenu) { }
                else if (pComponent is StatusBar)
                {
                    StatusBar statusBar = pComponent as StatusBar;
                    statusBar.MinimumSize = new Size(23, 23);
                    this.m_DockBarManager.DockBarDockAreaBottom.Controls.Add(statusBar);
                    this.m_DockBarManager.StatusBar = statusBar;
                }
                //
                return base.SetCreateTypeInfo(pComponent);
            }

            protected override bool FiltrationShowPopup(WFNew.View.NodeViewItem node)
            {
                if (node.Tag is DockBarManager)
                {
                    this.GetPopupItem("Expand_Appand").Visible = true;
                    this.GetPopupItem("ExpandAll_Appand").Visible = true;
                    this.GetPopupItem("Collapse_Appand").Visible = true;
                    //
                    return false;
                }
                else
                {
                    this.GetPopupItem("Expand_Appand").Visible = false;
                    this.GetPopupItem("ExpandAll_Appand").Visible = false;
                    this.GetPopupItem("Collapse_Appand").Visible = false;
                }
                //
                return base.FiltrationShowPopup(node);
            }
        }

        #region 已抛弃
        //private class BaseItemDBCollectionDesignerFormEx : BaseItemDBCollectionDesignerForm
        //{
        //    private DockBarManager m_DockBarManager = null;

        //    private WFNew.ContextPopup m_ContextPopup = new GISShare.Controls.WinForm.WFNew.ContextPopup();
        //    public BaseItemDBCollectionDesignerFormEx(DockBarManager dockBarManager)
        //        : base(null)
        //    {
        //        this.m_DockBarManager = dockBarManager;
        //        if (this.m_DockBarManager == null) return;
        //        //
        //        //
        //        //
        //        WinForm.TitleTreeNodeItem node3 = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //        node3.Name = "m_DockBarManager";
        //        node3.Text = "浮动工具条管理器";
        //        node3.Tag = this.m_DockBarManager;
        //        this.InsertTreeNode(new int[] { 0 }, 0, node3);
        //        //
        //        WinForm.TitleTreeNodeItem node = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //        node.Name = "ToolBars";
        //        node.Text = "浮动工具条集合";
        //        node.Tag = this.m_DockBarManager.ToolBars;
        //        foreach (ToolBar one in this.m_DockBarManager.ToolBars)
        //        {
        //            if (one != null) this.BuildTree_DG(one, node.Nodes);
        //        }
        //        this.InsertTreeNode(new int[] { 0 }, 0, node);
        //        //
        //        WinForm.TitleTreeNodeItem node2 = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //        node2.Name = "ContextMenus";
        //        node2.Text = "快捷菜单条集合";
        //        node2.Tag = this.m_DockBarManager.ContextMenus;
        //        foreach (ContextMenu one in this.m_DockBarManager.ContextMenus)
        //        {
        //            if (one != null) this.BuildTree_DG(one, node2.Nodes);
        //        }
        //        this.InsertTreeNode(new int[] { 0 }, 1, node2);
        //        //
        //        if (this.m_DockBarManager.StatusBar != null)
        //        {
        //            WinForm.TitleTreeNodeItem node4 = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //            node4.Name = "StatusBar";
        //            node4.Text = "状态栏";
        //            node4.Tag = this.m_DockBarManager.StatusBar;
        //            this.InsertTreeNode(new int[] { 0 }, 0, node4);
        //        }
        //        //
        //        if (this.m_DockBarManager.MenuBar != null)
        //        {
        //            WinForm.TitleTreeNodeItem node5 = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //            node5.Name = "MenuBar";
        //            node5.Text = "主菜单";
        //            node5.Tag = this.m_DockBarManager.MenuBar;
        //            this.InsertTreeNode(new int[] { 0 }, 0, node5);
        //        }
        //        //
        //        //
        //        //
        //        this.AppandPopupItem();
        //    }
        //    private void AppandPopupItem()
        //    {
        //        WFNew.BaseButtonItem item = new WFNew.BaseButtonItem();//0
        //        item.Name = "Expand_Appand";
        //        item.Text = "展开该节点";
        //        item.MouseClick += new MouseEventHandler(Item_MouseClick);
        //        this.InsertPopupItem(0, item);
        //        //
        //        WFNew.BaseButtonItem item2 = new WFNew.BaseButtonItem();//1
        //        item2.Name = "ExpandAll_Appand";
        //        item2.Text = "展开所有子节点";
        //        item2.MouseClick += new MouseEventHandler(Item_MouseClick);
        //        this.InsertPopupItem(1, item2);
        //        //
        //        WFNew.BaseButtonItem item3 = new WFNew.BaseButtonItem();//2
        //        item3.Name = "Collapse_Appand";
        //        item3.Text = "折叠该节点";
        //        item3.MouseClick += new MouseEventHandler(Item_MouseClick);
        //        this.InsertPopupItem(2, item3);
        //        //
        //        WFNew.SeparatorItem item4 = new WFNew.SeparatorItem();//3
        //        item4.Name = "EECSeparator_Appand";
        //        item4.Text = "EECSeparator_Appand";
        //        this.InsertPopupItem(3, item4);
        //        //
        //        WFNew.BaseButtonItem item5 = new WFNew.BaseButtonItem();//4
        //        item5.Name = "AddMenuBar_Appand";
        //        item5.Text = "添加主菜单";
        //        item5.MouseClick += new MouseEventHandler(Item_MouseClick);
        //        this.InsertPopupItem(4, item5);
        //        //
        //        WFNew.BaseButtonItem item6 = new WFNew.BaseButtonItem();//5
        //        item6.Name = "AddStatusBar_Appand";
        //        item6.Text = "添加状态栏";
        //        item6.MouseClick += new MouseEventHandler(Item_MouseClick);
        //        this.InsertPopupItem(5, item6);
        //    }
        //    void Item_MouseClick(object sender, MouseEventArgs e)
        //    {
        //        WFNew.IBaseItem pBaseItem = sender as WFNew.IBaseItem;
        //        if (pBaseItem == null) return;
        //        //
        //        switch (pBaseItem.Name)
        //        {
        //            case "Expand_Appand":
        //                this.Expand();
        //                break;
        //            case "ExpandAll_Appand":
        //                this.ExpandAll();
        //                break;
        //            case "Collapse_Appand":
        //                this.CollapseAll();
        //                break;
        //            case "AddMenuBar_Appand":
        //                IDesignerHost host = (IDesignerHost)GetServiceCallBackEx(typeof(IDesignerHost));
        //                if (host != null)
        //                {
        //                    IComponent pComponent = host.CreateComponent(typeof(MenuBar));
        //                    if (this.SetCreateTypeInfo(pComponent))
        //                    {
        //                        WinForm.TitleTreeNodeItem node = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //                        node.Name = "MenuBar";
        //                        node.Text = "主菜单";
        //                        node.Tag = this.m_DockBarManager.MenuBar;
        //                        this.InsertTreeNode(new int[] { 0 }, 0, node);
        //                    }
        //                    else { pComponent.Dispose(); }
        //                }
        //                break;
        //            case "AddStatusBar_Appand":
        //                IDesignerHost host2 = (IDesignerHost)GetServiceCallBackEx(typeof(IDesignerHost));
        //                if (host2 != null)
        //                {
        //                    IComponent pComponent = host2.CreateComponent(typeof(StatusBar));
        //                    if (this.SetCreateTypeInfo(pComponent))
        //                    {
        //                        WinForm.TitleTreeNodeItem node = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //                        node.Name = "StatusBar";
        //                        node.Text = "状态栏";
        //                        node.Tag = this.m_DockBarManager.StatusBar;
        //                        if (this.m_DockBarManager.MenuBar == null) this.InsertTreeNode(new int[] { 0 }, 0, node);
        //                        else this.InsertTreeNode(new int[] { 0 }, 1, node);
        //                    }
        //                    else { pComponent.Dispose(); }
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        //    {
        //        Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = base.CreateNewItemTypesDictionary();
        //        //
        //        //typeCreateNewItemTypesDictionary.Add
        //        //    (
        //        //    "GISShare.Controls.WinForm.DockBar.DockBarManager",
        //        //    new Type[] { typeof(MenuBar), typeof(StatusBar) }
        //        //    );
        //        typeCreateNewItemTypesDictionary.Add
        //            (
        //            "GISShare.Controls.WinForm.DockBar.DockBarManager+ToolBarCollection",
        //            new Type[] { typeof(ToolBar) }
        //            );
        //        typeCreateNewItemTypesDictionary.Add
        //            (
        //            "GISShare.Controls.WinForm.DockBar.DockBarManager+ContextMenuCollection",
        //            new Type[] { typeof(ContextMenu) }
        //            );
        //        //
        //        return typeCreateNewItemTypesDictionary;
        //    }

        //    protected override bool SetCreateTypeInfo(IComponent pComponent)
        //    {
        //        if (pComponent is MenuBar)
        //        {
        //            MenuBar menuBar = pComponent as MenuBar;
        //            menuBar.MinimumSize = new Size(23, 23);
        //            this.m_DockBarManager.ParentForm.MainMenuStrip = menuBar;
        //            this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(menuBar);
        //            this.m_DockBarManager.MenuBar = menuBar;
        //        }
        //        else if (pComponent is ToolBar)
        //        {
        //            ToolBar toolBar = pComponent as ToolBar;
        //            toolBar.MinimumSize = new Size(23, 23);
        //            this.m_DockBarManager.DockBarDockAreaTop.Controls.Add(toolBar);
        //            //this.m_DockBarManager.ToolBars.Add(toolBar);
        //        }
        //        //else if (pComponent is ContextMenu) { }
        //        else if (pComponent is StatusBar)
        //        {
        //            StatusBar statusBar = pComponent as StatusBar;
        //            statusBar.MinimumSize = new Size(23, 23);
        //            this.m_DockBarManager.DockBarDockAreaBottom.Controls.Add(statusBar);
        //            this.m_DockBarManager.StatusBar = statusBar;
        //        }
        //        //
        //        return base.SetCreateTypeInfo(pComponent);
        //    }

        //    protected override bool FiltrationShowPopup(object value)
        //    {
        //        if (value is DockBarManager)
        //        {
        //            this.GetPopupItem("Expand_Appand").Visible = true;
        //            this.GetPopupItem("ExpandAll_Appand").Visible = true;
        //            this.GetPopupItem("Collapse_Appand").Visible = true;
        //            this.GetPopupItem("EECSeparator_Appand").Visible = (this.m_DockBarManager.MenuBar == null || this.m_DockBarManager.StatusBar == null);
        //            this.GetPopupItem("AddMenuBar_Appand").Visible = (this.m_DockBarManager.MenuBar == null);
        //            this.GetPopupItem("AddStatusBar_Appand").Visible = (this.m_DockBarManager.StatusBar == null);
        //            //
        //            return false;
        //        }
        //        else
        //        {
        //            this.GetPopupItem("Expand_Appand").Visible = false;
        //            this.GetPopupItem("ExpandAll_Appand").Visible = false;
        //            this.GetPopupItem("Collapse_Appand").Visible = false;
        //            this.GetPopupItem("EECSeparator_Appand").Visible = false;
        //            this.GetPopupItem("AddMenuBar_Appand").Visible = false;
        //            this.GetPopupItem("AddStatusBar_Appand").Visible = false;
        //        }
        //        //
        //        return base.FiltrationShowPopup(value);
        //    }
        //}
        #endregion
    }
}

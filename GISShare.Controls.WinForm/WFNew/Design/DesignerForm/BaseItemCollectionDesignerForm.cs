using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class BaseItemCollectionDesignerForm : CollectionDesignerForm
    {
        public BaseItemCollectionDesignerForm(IObjectDesignHelper pObjectDesignHelper)
            : base(pObjectDesignHelper) { }

        protected override void BuildTreeEx(View.NodeViewItem node)
        {
            if (node.Tag is IRibbonControl)
            {
                IRibbonControl pRibbonControl = (IRibbonControl)node.Tag;
                //
                View.NodeViewItem node0 = new View.NodeViewItem();
                node0.Name = "R_ToolbarItems";
                node0.Text = "快捷工具条";
                node0.ShowNomalState = true;
                node0.Tag = pRibbonControl.ToolbarItems;
                foreach (IObjectDesignHelper one in pRibbonControl.ToolbarItems)
                {
                    if (one != null) this.BuildTree_DG(one, node0.NodeViewItems);
                }
                node.NodeViewItems.Add(node0);
                //
                View.NodeViewItem node1 = new View.NodeViewItem();
                node1.Name = "R_PageContents";
                node1.Text = "功能区面板右侧按钮列表";
                node1.ShowNomalState = true;
                node1.Tag = pRibbonControl.PageContents;
                foreach (IObjectDesignHelper one in pRibbonControl.PageContents)
                {
                    if (one != null) this.BuildTree_DG(one, node1.NodeViewItems);
                }
                node.NodeViewItems.Add(node1);
                //
                this.BuildTree_DG(pRibbonControl.ApplicationPopup as IObjectDesignHelper, node.NodeViewItems);
                int iIndex = node.NodeViewItems.Count - 1;
                if (iIndex >= 0) 
                {
                    View.NodeViewItem node2 = node.NodeViewItems[iIndex];
                    node2.Name = "R_RibbonApplicationPopup";
                    node2.Text = "应用程序快捷菜单";
                    node2.ShowNomalState = true;
                }
                //
                View.NodeViewItem node3 = new View.NodeViewItem();
                node3.Name = "R_RibbonPages";
                node3.Text = "功能区面板集合";
                node3.ShowNomalState = true;
                node3.Tag = pRibbonControl.TabPages;
                foreach (IObjectDesignHelper one in pRibbonControl.TabPages)
                {
                    if (one != null) this.BuildTree_DG(one, node3.NodeViewItems);
                }
                node.NodeViewItems.Add(node3);
            }
        }

        protected override bool FiltrationSelected(View.NodeViewItem node)
        {
            if (node.Name == "R_ToolbarItems") return false;
            if (node.Name == "R_PageContents") return false;
            if (node.Name == "R_RibbonApplicationPopup") return false;
            if (node.Name == "R_RibbonPages") return false;
            //
            if (node.Tag == null) return false;
            string strName = node.Tag.GetType().Name;
            if (strName == "RibbonApplicationPopupPanelMiddleLeftItem") return false;
            if (strName == "RibbonApplicationPopupPanelMiddleRightItem") return false;
            if (strName == "RibbonApplicationPopupPanelBottomItem") return false;
            //
            return base.FiltrationSelected(node);
        }

        protected override string GetTypeDescription(View.NodeViewItem node)
        {
            if (node.Name == "R_ToolbarItems") return "快捷工具条";
            if (node.Name == "R_PageContents") return "功能区面板右侧按钮列表";
            if (node.Name == "R_RibbonApplicationPopup") return "应用程序快捷菜单";
            if (node.Name == "R_RibbonPages") return "功能区面板集合";
            //
            if (node.Tag == null) return "null";
            string strName = node.Tag.GetType().Name;
            if (strName == "RibbonApplicationPopupPanelMiddleLeftItem") return "菜单栏";
            if (strName == "RibbonApplicationPopupPanelMiddleRightItem") return "记录栏";
            if (strName == "RibbonApplicationPopupPanelBottomItem") return "操作栏";
            //
            return base.GetTypeDescription(node);
        }

        protected override string GetNewItemTypesDictionaryKey(View.NodeViewItem node)
        {
            switch (node.Name) 
            {
                case "R_ToolbarItems":
                case "R_PageContents":
                case "R_RibbonApplicationPopup":
                case "R_RibbonPages":
                    return node.Name;
                default:
                    return base.GetNewItemTypesDictionaryKey(node);
            }
        }

        /// <summary>
        /// 默认的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected override Type[] CreateNewItemTypes()
        {
            return CollectionDesignerForm.GetCreateNewItemTypes();
        }

        /// <summary>
        /// 各类型 对应的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        {
            Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = CollectionDesignerForm.GetCreateNewItemTypesDictionary();
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "R_ToolbarItems",
                GetRibbonQuickAccessToolbarNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "R_PageContents",
                GetRibbonPageContentContainerNewItemTypes()
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "R_RibbonPages",
                new Type[] { typeof(RibbonPageItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.WFNew.RibbonControlItem+RibbonPageCollection",
                new Type[] { typeof(RibbonPageItem) }
                );
            //
            return typeCreateNewItemTypesDictionary;
        }

        protected internal override void SelectedComponent(Component component)
        {
            base.SelectedComponent(component);
            //
            if (component is RibbonPageItem)
            {
                RibbonPageItem item = (RibbonPageItem)component;
                IRibbonControl pRibbonControl = item.TryGetDependRibbonControl();
                if (pRibbonControl != null)
                {
                    int index = pRibbonControl.TabPages.IndexOf(item);
                    if (index >= 0 && index != pRibbonControl.RibbonPageSelectedIndex)
                    {
                        pRibbonControl.RibbonPageSelectedIndex = index;
                    }
                }
            }
        }

        protected override bool SetCreateTypeInfo(IComponent pComponent)
        {
            if (pComponent is RibbonPageItem)
            {
                RibbonPageItem item = (RibbonPageItem)pComponent;
                IRibbonControl pRibbonControl = item.TryGetDependRibbonControl();
                if (pRibbonControl != null)
                {
                    item.LineDistance = 2;
                    item.ColumnDistance = 2;
                    //item.ShowBackgroud = true;
                }
            }
            else if (pComponent is RibbonBarItem)
            {
                RibbonBarItem item = (RibbonBarItem)pComponent;
                IRibbonControl pRibbonControl = item.TryGetDependRibbonControl();
                if (pRibbonControl != null)
                {
                    item.Padding = new Padding(3, 3, 3, 2);
                }
            }
            else if (pComponent is TextBoxItem)
            {
                TextBoxItem item = (TextBoxItem)pComponent;
                IRibbonControl pRibbonControl = item.TryGetDependRibbonControl();
                if (pRibbonControl != null)
                {
                    item.Size = new Size(100, 21);
                }
            }
            //
            return base.SetCreateTypeInfo(pComponent);
        }
    }
}

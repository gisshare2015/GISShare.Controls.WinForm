using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonControlDesignerForm : WFNew.Design.BaseItemCollectionDesignerForm
    {
            private IRibbonControl m_pRibbonControl = null;

            public RibbonControlDesignerForm(IObjectDesignHelper pObjectDesignHelper)
                : base(pObjectDesignHelper)
            {
                this.m_pRibbonControl = (IRibbonControl)pObjectDesignHelper;
                //
                View.NodeViewItem node = new View.NodeViewItem();
                this.BuildTree_DG(this.m_pRibbonControl.ApplicationPopup as IObjectDesignHelper, node.NodeViewItems);
                this.InsertTreeNode(new int[] { 0 }, 2, node.NodeViewItems[0]);
                //
                View.NodeViewItem node2 = new View.NodeViewItem();
                node2.Name = "RibbonPages";
                node2.Text = "功能区面板集合";
                node2.ShowNomalState = true;
                node2.Tag = this.m_pRibbonControl.TabPages;
                foreach (IObjectDesignHelper one in this.m_pRibbonControl.TabPages)
                {
                    if (one != null) this.BuildTree_DG(one, node2.NodeViewItems);
                }
                this.InsertTreeNode(new int[] { 0 }, 3, node2);
            }

            protected override bool FiltrationShowPopup(object value)
            {
                if (value == null) return false;
                //
                if (value.GetType().Name == "RibbonControl") return false;
                if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
                if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
                if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
                //
                if (value.GetType().Name == "RibbonApplicationPopup")
                {
                    return false;
                }
                //
                if (value.GetType().Name == "RibbonPageCollection")
                {
                    return true;
                }
                //
                return base.FiltrationShowPopup(value);
            }

            protected override bool FiltrationSelected(object value)
            {
                if (value == null) return false;
                //
                if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
                if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
                if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
                //
                if (value.GetType().Name == "RibbonApplicationPopup") return false;
                //if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return false;
                //if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return false;
                //if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return false;
                //
                if (value.GetType().Name == "RibbonQuickAccessToolbarItemEx") return false;
                if (value.GetType().Name == "RibbonPageContentContainerItem") return false;
                //
                return base.FiltrationSelected(value);
            }

            protected override bool FiltrationBaseItem(object value)
            {
                if (value == null) return false;
                //
                if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
                if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
                if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
                if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
                //
                return base.FiltrationBaseItem(value);
            }

            protected override string GetTypeDescription(object value)
            {
                if (value == null) return "null";
                //
                if (value.GetType().Name == "RibbonQuickAccessToolbarItemEx") return "快捷工具条";
                if (value.GetType().Name == "RibbonPageContentContainerItem") return "功能区面板右侧按钮列表";
                //
                if (value.GetType().Name == "RibbonApplicationPopup") return "应用程序快捷菜单";
                //if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return "菜单栏";
                //if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return "记录栏";
                //if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return "操作栏";
                //
                return base.GetTypeDescription(value);
            }

            protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
            {
                Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = base.CreateNewItemTypesDictionary();
                //
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.RibbonControlItem+RibbonQuickAccessToolbarItemEx",
                    GetRibbonQuickAccessToolbarNewItemTypes()
                    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.RibbonControlItem+RibbonPageContentContainerItem",
                    GetRibbonPageContentContainerNewItemTypes()
                    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.RibbonControlItem+RibbonPageCollection",
                    new Type[] { typeof(RibbonPageItem) }
                    );
                //
                return typeCreateNewItemTypesDictionary;
            }

            protected override GISShare.Controls.WinForm.WFNew.View.NodeViewItem CreateNode(object value)
            {
                if (value is WFNew.ICollectionObjectDesignHelper)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                    node.ShowNomalState = true;
                    return node;
                }
                //
                return base.CreateNode(value);
            }

            protected internal override void SelectedComponent(Component component)
            {
                base.SelectedComponent(component);
                //
                if (component is RibbonPageItem)
                {
                    RibbonPageItem item = component as RibbonPageItem;
                    int index = this.m_pRibbonControl.TabPages.IndexOf(item);
                    if (index >= 0 && index != this.m_pRibbonControl.RibbonPageSelectedIndex)
                    {
                        this.m_pRibbonControl.RibbonPageSelectedIndex = index;
                    }
                }
            }

            protected override bool SetCreateTypeInfo(IComponent pComponent)
            {
                if (pComponent is IRibbonPageItem)
                {
                    IRibbonPageItem pItem = pComponent as IRibbonPageItem;
                    pItem.LineDistance = 2;
                    pItem.ColumnDistance = 2;
                    //pItem.ShowBackgroud = true;
                }
                else if (pComponent is IRibbonBarItem)
                {
                    IRibbonBarItem pItem = pComponent as IRibbonBarItem;
                    pItem.Padding = new Padding(3, 3, 3, 2);
                }
                else if (pComponent is ITextBoxItem)
                {
                    //ITextBoxItem pItem = pComponent as ITextBoxItem;
                    //pItem.Size = new Size(100, 21);
                    BaseItem baseItem = pComponent as BaseItem;
                    if (baseItem != null) this.Size = new Size(100, 21);
                }
                //
                return base.SetCreateTypeInfo(pComponent);
            }
    }
}

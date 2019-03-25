using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class BaseItemHostDesigner : ControlDesigner
    {
        private Control m_Control = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_Control = base.Component as Control;
            if (this.m_Control == null)
            {
                this.DisplayError(new ArgumentException("Control == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IBaseItemHost pBaseItemHost = this.m_Control as IBaseItemHost;
                if (pBaseItemHost != null && pBaseItemHost.BaseItemObject != null)
                {
                    pBaseItemHost.BaseItemObject.Dispose();
                    pBaseItemHost.BaseItemObject = null;
                }
            }
            base.Dispose(disposing);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(this.BuildTreeView)));
                //
                return verbs;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            using (Pen p = new Pen(Color.Black))
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                ISelectionService host = GetService(typeof(ISelectionService)) as ISelectionService;
                if (host != null)
                {
                    foreach (IComponent one in host.GetSelectedComponents())
                    {
                        BaseItem item = one as BaseItem;
                        if (item == null) continue;
                        Rectangle rectangle = this.GetDrawBorder(item);
                        pe.Graphics.SetClip(rectangle);
                        pe.Graphics.DrawRectangle(p, Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1));
                        break;
                    }
                }
            }
            //
            base.OnPaintAdornments(pe);
        }
        private Rectangle GetDrawBorder(BaseItem baseItem)
        {
            Point point = baseItem.PointToScreen(baseItem.DesignMouseSelectedRectangle.Location);
            point = this.m_Control.PointToClient(point);
            return new Rectangle(point, baseItem.DesignMouseSelectedRectangle.Size);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_Control.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_Control.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_Control.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseUp(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }
        private Point m_MouseDownPoint = Point.Empty;
        private BaseItem m_Item1 = null;
        private BaseItem m_Item2 = null;
        private bool SelectCompnentMouseDown(Point point)
        {
            IBaseItemHost pBaseItemHost = this.m_Control as IBaseItemHost;
            if (pBaseItemHost == null || pBaseItemHost.BaseItemObject == null) return false;
            //
            return this.SelectCompnentMouseDown_DG(pBaseItemHost.BaseItemObject as IUICollectionItem, point);
        }
        private bool SelectCompnentMouseDown_DG(IUICollectionItem pUICollectionItem, Point point)
        {
            if (pUICollectionItem == null) return false;
            if (!((IBaseItem)pUICollectionItem).Visible) return false;
            //
            bool bIsTabButtonContainerItem = pUICollectionItem is ITabButtonContainerItem;
            //
            foreach (BaseItem one in pUICollectionItem.BaseItems)
            {
                if (bIsTabButtonContainerItem && one is TabButtonItem)
                {
                    TabButtonItem tabButtonItem = (TabButtonItem)one;
                    if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item1 = tabButtonItem;
                            BaseItem tabPageItem = tabButtonItem.pTabPageItem as BaseItem;
                            if (tabPageItem != null)
                            {
                                if (this.SetSelectedComponents(pSelectionService, tabPageItem))
                                {
                                    this.m_Control.Refresh();
                                }
                            }
                            ITabButtonContainerItem pTabButtonContainerItem = (ITabButtonContainerItem)pUICollectionItem;
                            pTabButtonContainerItem.TabButtonItemSelectedIndex = pTabButtonContainerItem.BaseItems.IndexOf(tabButtonItem);
                            //
                            return true;
                        }
                    }
                }
                else
                {
                    if (this.SelectCompnentMouseDown_DG(one as IUICollectionItem, point)) return true;
                    //
                    if (one.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item1 = one;
                            this.m_MouseDownPoint = point;
                            if (this.SetSelectedComponents(pSelectionService, one))
                            {
                                this.m_Control.Refresh();
                            }
                            return true;
                        }
                    }
                }
            }
            //
            if (pUICollectionItem is IBaseItemStackExItem)
            {
                IBaseItemStackExItem pBaseItemStackExItem = (IBaseItemStackExItem)pUICollectionItem;
                if (pBaseItemStackExItem.PreButtonVisible &&
                pBaseItemStackExItem.PreButtonRectangle.Contains(point))
                {
                    if (pBaseItemStackExItem.PreButtonIncreaseIndex) pBaseItemStackExItem.TopViewItemIndex++;
                    else pBaseItemStackExItem.TopViewItemIndex--;
                }
                if (pBaseItemStackExItem.NextButtonVisible &&
                pBaseItemStackExItem.NextButtonRectangle.Contains(point))
                {
                    if (pBaseItemStackExItem.PreButtonIncreaseIndex) pBaseItemStackExItem.TopViewItemIndex--;
                    else pBaseItemStackExItem.TopViewItemIndex++;
                }
            }
            else if (pUICollectionItem is IGalleryItem)
            {
                IGalleryItem pGalleryItem = (IGalleryItem)pUICollectionItem;
                if (pGalleryItem.ScrollUpButtonRectangle.Contains(point)) pGalleryItem.TopViewItemIndex++;
                if (pGalleryItem.ScrollDownButtonRectangle.Contains(point)) pGalleryItem.TopViewItemIndex--;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            IBaseItemHost pBaseItemHost = this.m_Control as IBaseItemHost;
            if (pBaseItemHost == null || pBaseItemHost.BaseItemObject == null) return false;
            //
            return this.SelectCompnentMouseUp_DG(pBaseItemHost.BaseItemObject as IUICollectionItem, point);
        }
        private bool SelectCompnentMouseUp_DG(IUICollectionItem pUICollectionItem, Point point)
        {
            if (pUICollectionItem == null) return false;
            if (!((IBaseItem)pUICollectionItem).Visible) return false;
            //
            bool bIsCanvasItem = pUICollectionItem is ICanvasItem;
            bool bIsTabButtonContainerItem = pUICollectionItem is ITabButtonContainerItem;
            //
            foreach (BaseItem one in pUICollectionItem.BaseItems)
            {
                if (this.SelectCompnentMouseUp_DG(one as IUICollectionItem, point)) return true;
                //
                if (!bIsCanvasItem)
                {
                    if (one.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            if (bIsTabButtonContainerItem && one is TabButtonItem)
                            {
                                TabButtonItem tabButtonItem = (TabButtonItem)one;
                                this.m_Item2 = tabButtonItem;
                                ITabButtonContainerItem pTabButtonContainerItem = (ITabButtonContainerItem)pUICollectionItem;
                                if (pTabButtonContainerItem.BaseItems.ExchangeItemT(this.m_Item1, this.m_Item2))
                                {
                                    RibbonPageTabButtonItem tabButton = this.m_Item1 as RibbonPageTabButtonItem;
                                    if (tabButton != null)
                                    {
                                        BaseItem tabPageItem = tabButtonItem.pTabPageItem as BaseItem;
                                        if (tabPageItem != null)
                                        {
                                            if (this.SetSelectedComponents(pSelectionService, tabPageItem))
                                            {
                                                this.m_Control.Refresh();
                                            }
                                        }
                                    }
                                    return true;
                                }
                                else
                                {
                                    RibbonPageItem ribbonPage = tabButtonItem.pTabPageItem as RibbonPageItem;
                                    if (ribbonPage != null)
                                    {
                                        if (this.SetSelectedComponents(pSelectionService, ribbonPage))
                                        {
                                            this.m_Control.Refresh();
                                        }
                                    }
                                    return true;
                                }
                            }
                            else
                            {
                                this.m_Item2 = one;
                                if (pUICollectionItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                                {
                                    if (this.SetSelectedComponents(pSelectionService, this.m_Item1))
                                    {
                                        this.m_Control.Refresh();
                                    }
                                    return true;
                                }
                                else
                                {

                                    this.m_Control.Refresh();
                                    if (this.SetSelectedComponents(pSelectionService, one))
                                    {
                                        this.m_Control.Refresh();
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            //
            if (bIsCanvasItem)
            {
                if (this.m_Item1 != null)
                {
                    if (this.m_MouseDownPoint != Point.Empty)
                    {
                        ISetBaseItemHelper pSetBaseItemHelper = this.m_Item1 as ISetBaseItemHelper;
                        if (pSetBaseItemHelper != null)
                        {
                            int iX = point.X - this.m_MouseDownPoint.X;
                            int iY = point.Y - this.m_MouseDownPoint.Y;
                            pSetBaseItemHelper.SetLocation(this.m_Item1.Location.X + iX, this.m_Item1.Location.Y + iY);
                            this.Translation_DG(this.m_Item1 as CanvasItem, iX, iY);
                            this.m_MouseDownPoint = Point.Empty;
                            this.m_Control.Refresh();
                        }
                    }
                    //
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.SetSelectedComponents(pSelectionService, this.m_Item1);
                    }
                    //
                    this.m_Item1 = null;
                    //
                    return true;
                }
            }
            //
            return false;
        }
        private void Translation_DG(CanvasItem canvasItem, int iX, int iY)
        {
            if (canvasItem == null) return;
            //
            foreach (BaseItem one in canvasItem.BaseItems)
            {
                one.Translation(iX, iY);
                //
                this.Translation_DG(one as CanvasItem, iX, iY);
            }
        }
        private bool SetSelectedComponents(ISelectionService pSelectionService, BaseItem baseItem) 
        {
            if (this.FilterType(baseItem)) return false;
            //
            pSelectionService.SetSelectedComponents(new Component[] { baseItem as Component }, SelectionTypes.Primary);
            return true;
        }
        private bool FilterType(BaseItem baseItem)
        {
            if (baseItem == null) return false;
            //
            string strType = baseItem.GetType().Name;
            return strType == "RibbonStartButtonItem2007Ex" ||
                strType == "RibbonStartButtonItem2010Ex" ||
                strType == "FormButtonStackItem" ||
                strType == "RibbonFormButtonStackItem" ||
                strType == "RibbonQuickAccessToolbarItemEx" ||
                strType == "RibbonPageContentContainerItem" ||
                strType == "MdiFormButtonStackItem" ||
                strType == "RibbonMdiFormButtonStackItem" ||
                strType == "TabButtonContainerItem" ||
                strType == "RibbonPageTabButtonContainerItem" ||
                strType == "TabButtonItem" ||
                strType == "RibbonPageTabButtonItem";
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_Control as IObjectDesignHelper);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}


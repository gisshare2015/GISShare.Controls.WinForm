using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonControlExDesigner : ControlDesigner
    {
        private RibbonControlEx m_RibbonControl = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonControl = base.Component as RibbonControlEx;
            if (this.m_RibbonControl == null)
            {
                this.DisplayError(new ArgumentException("RibbonControlEx == null"));
                return;
            }
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host != null)
            {
                System.Windows.Forms.Form form = host.RootComponent as System.Windows.Forms.Form;
                form.MainMenuStrip = this.m_RibbonControl.MenuStrip;
                //
                WFNew.RibbonForm ribbonForm = form as WFNew.RibbonForm;
                if (ribbonForm != null)
                {
                    this.m_RibbonControl.ParentForm = ribbonForm;
                    ribbonForm.RibbonControl = this.m_RibbonControl;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonControl.ToolbarItems)
                {
                    one.Dispose();
                }
                ((ICollectionItem)this.m_RibbonControl).BaseItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonControl.PageContents)
                {
                    one.Dispose();
                }
                this.m_RibbonControl.PageContents.Clear();
                //
                foreach (RibbonPage one in this.m_RibbonControl.RibbonPages)
                {
                    one.Dispose();
                }
                this.m_RibbonControl.RibbonPages.Clear();
                //
                //
                //
                foreach (BaseItem one in this.m_RibbonControl.ApplicationPopup.MenuItems)
                {
                    one.Dispose();
                }
                this.m_RibbonControl.ApplicationPopup.MenuItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonControl.ApplicationPopup.RecordItems)
                {
                    one.Dispose();
                }
                this.m_RibbonControl.ApplicationPopup.RecordItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonControl.ApplicationPopup.OperationItems)
                {
                    one.Dispose();
                }
                this.m_RibbonControl.ApplicationPopup.OperationItems.Clear();
            }
            base.Dispose(disposing);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                ////
                //verbs.Add(new DesignerVerb("添加 RibbonPage 到 RibbonPages", new EventHandler(AddRibbonPageToRibbonPages)));
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem 到 ToolbarItems", new EventHandler(AddBaseButtonItemToToolbarItems)));
                //verbs.Add(new DesignerVerb("添加 CheckButtonItem 到 ToolbarItems", new EventHandler(AddCheckButtonItemToToolbarItems)));
                //verbs.Add(new DesignerVerb("添加 DropDownButtonItem 到 ToolbarItems", new EventHandler(AddDropDownButtonItemToToolbarItems)));
                //verbs.Add(new DesignerVerb("添加 SplitButtonItem 到 ToolbarItems", new EventHandler(AddSplitButtonItemToToolbarItems)));
                //verbs.Add(new DesignerVerb("添加 ButtonItem 到 ToolbarItems", new EventHandler(AddButtonItemToToolbarItems)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem 到 ToolbarItems", new EventHandler(AddSeparatorItemToToolbarItems)));
                ////
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem 到 PageContents", new EventHandler(AddBaseButtonItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 DropDownButtonItem 到 PageContents", new EventHandler(AddDropDownButtonItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 SplitButtonItem 到 PageContents", new EventHandler(AddSplitButtonItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 ButtonItem 到 PageContents", new EventHandler(AddButtonItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 CheckButtonItem 到 PageContents", new EventHandler(AddCheckButtonItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 TextBoxItem 到 PageContents", new EventHandler(AddTextBoxItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 ComboBoxItem 到 PageContents", new EventHandler(AddComboBoxItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 ComboTreeItem 到 PageContents", new EventHandler(AddComboTreeItemToPageContents)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem 到 PageContents", new EventHandler(AddSeparatorItemToPageContents)));
                ////
                //verbs.Add(new DesignerVerb("添加 MenuButtonItem 到 ApplicationPopup.MenuItems", new EventHandler(AddMenuButtonItemToMenuItems)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem 到 ApplicationPopup.MenuItems", new EventHandler(AddSeparatorItemToMenuItems)));
                //verbs.Add(new DesignerVerb("添加 LabelSeparatorItem 到 ApplicationPopup.RecordItems", new EventHandler(AddLabelSeparatorItemToRecordItems)));
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem 到 ApplicationPopup.RecordItems", new EventHandler(AddBaseButtonItemToRecordItems)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem 到 ApplicationPopup.RecordItems", new EventHandler(AddSeparatorItemToRecordItems)));
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem 到 ApplicationPopup.OperationItems", new EventHandler(AddBaseButtonItemToOperationItems)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem 到 ApplicationPopup.OperationItems", new EventHandler(AddSeparatorItemToOperationItems)));
                verbs.Add(new DesignerVerb("展现 ApplicationPopup", new EventHandler(ShowApplicationPopup)));
                verbs.Add(new DesignerVerb("关闭 ApplicationPopup", new EventHandler(CloseApplicationPopup)));
                verbs.Add(new DesignerVerb("检测和修复RibbonControl", new EventHandler(CheckAndRepair)));
                verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
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
            point = this.m_RibbonControl.PointToClient(point);
            return new Rectangle(point, baseItem.DesignMouseSelectedRectangle.Size);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_RibbonControl.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_RibbonControl.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_RibbonControl.Handle != m.HWnd) break;
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
            foreach (BaseItem one in ((ICollectionItem)this.m_RibbonControl).BaseItems)
            {
                if (one.GetType().Name == "RibbonQuickAccessToolbarItemEx" || one.GetType().Name == "RibbonPageContentContainerItem")
                {
                    if (this.SelectCompnentMouseDown_DG(one as BaseItemStackItem, point)) return true;
                }
                if (one.GetType().Name == "RibbonPageTabButtonContainerItem")
                {
                    if (this.SelectCompnentMouseDownTC(one as TabButtonContainerItem, point)) return true;
                }                
                //
                //if (one.DesignMouseClickRectangleContainsEx(point))
                //{
                //    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                //    if (pSelectionService != null)
                //    {
                //        //this.m_Item1 = one;
                //        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                //        this.m_RibbonControl.Refresh();
                //        return true;
                //    }
                //}
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDownTC(TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null) return false;
            //
            foreach (BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                RibbonPageTabButtonItem tabButtonItem = one as RibbonPageTabButtonItem;
                if (tabButtonItem != null)
                {
                    if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item1 = tabButtonItem;
                            RibbonPage ribbonPage = tabButtonItem.pTabPageItem as RibbonPage;
                            if (ribbonPage != null)
                            {
                                pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                                this.m_RibbonControl.Refresh();
                            }
                            this.m_RibbonControl.RibbonPageSelectedIndex = ribbonTabButtonContainerItem.BaseItems.IndexOf(tabButtonItem);
                            //
                            return true;
                        }
                    }
                }
                else
                {
                    if (this.SelectCompnentMouseDown(one as RibbonGalleryItem, point)) return true;
                    if (this.SelectCompnentMouseDown(one as ButtonGroupItem, point)) return true;
                    if (this.SelectCompnentMouseDown_DG(one as BaseItemStackExItem, point)) return true;
                    if (this.SelectCompnentMouseDown_DG(one as BaseItemStackItem, point)) return true;
                    if (this.SelectCompnentMouseDown_DG(one as CanvasItem, point)) return true;
                    //
                    if (one.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item1 = one;
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            if (ribbonTabButtonContainerItem.PreButtonVisible &&
                ribbonTabButtonContainerItem.PreButtonRectangle.Contains(point))
            {
                if (ribbonTabButtonContainerItem.PreButtonIncreaseIndex) ribbonTabButtonContainerItem.TopViewItemIndex++;
                else ribbonTabButtonContainerItem.TopViewItemIndex--;
            }
            if (ribbonTabButtonContainerItem.NextButtonVisible &&
                ribbonTabButtonContainerItem.NextButtonRectangle.Contains(point))
            {
                if (ribbonTabButtonContainerItem.PreButtonIncreaseIndex) ribbonTabButtonContainerItem.TopViewItemIndex--;
                else ribbonTabButtonContainerItem.TopViewItemIndex++;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDown_DG(CanvasItem canvasItem, Point point)
        {
            if (canvasItem == null) return false;
            //
            foreach (BaseItem one in canvasItem.BaseItems)
            {
                if (this.SelectCompnentMouseDown(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseDown(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as CanvasItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        this.m_MouseDownPoint = point;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonControl.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDown_DG(BaseItemStackItem ribbonBaseItemStackItem, Point point)
        {
            if (ribbonBaseItemStackItem == null) return false;
            //
            foreach (BaseItem one in ribbonBaseItemStackItem.BaseItems)
            {
                if (this.SelectCompnentMouseDown(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseDown(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as CanvasItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonControl.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDown_DG(BaseItemStackExItem ribbonBaseItemStackExItem, Point point)
        {
            if (ribbonBaseItemStackExItem == null) return false;
            //
            foreach (BaseItem one in ribbonBaseItemStackExItem.BaseItems)
            {
                if (this.SelectCompnentMouseDown(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseDown(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseDown_DG(one as CanvasItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonControl.Refresh();
                        return true;
                    }
                }
            }
            //
            //switch (ribbonBaseItemStackExItem.eOrientation)
            //{
            //    case Orientation.Horizontal:
            //        if (ribbonBaseItemStackExItem.LeftButtonVisible &&
            //            ribbonBaseItemStackExItem.LeftButtonRectangle.Contains(point)) ribbonBaseItemStackExItem.TopViewItemIndex++;
            //        if (ribbonBaseItemStackExItem.RightButtonVisible &&
            //            ribbonBaseItemStackExItem.RightButtonRectangle.Contains(point)) ribbonBaseItemStackExItem.TopViewItemIndex--;
            //        break;
            //    case Orientation.Vertical:
            //        if (ribbonBaseItemStackExItem.TopButtonVisible &&
            //            ribbonBaseItemStackExItem.TopButtonRectangle.Contains(point)) ribbonBaseItemStackExItem.TopViewItemIndex++;
            //        if (ribbonBaseItemStackExItem.BottomButtonVisible &&
            //            ribbonBaseItemStackExItem.BottomButtonRectangle.Contains(point)) ribbonBaseItemStackExItem.TopViewItemIndex--;
            //        break;
            //}
            if (ribbonBaseItemStackExItem.PreButtonVisible &&
                ribbonBaseItemStackExItem.PreButtonRectangle.Contains(point))
            {
                if (ribbonBaseItemStackExItem.PreButtonIncreaseIndex) ribbonBaseItemStackExItem.TopViewItemIndex++;
                else ribbonBaseItemStackExItem.TopViewItemIndex--;
            }
            if (ribbonBaseItemStackExItem.NextButtonVisible &&
                ribbonBaseItemStackExItem.NextButtonRectangle.Contains(point))
            {
                if (ribbonBaseItemStackExItem.PreButtonIncreaseIndex) ribbonBaseItemStackExItem.TopViewItemIndex--;
                else ribbonBaseItemStackExItem.TopViewItemIndex++;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDown(RibbonGalleryItem ribbonGalleryItem, Point point)
        {
            if (ribbonGalleryItem == null) return false;
            //
            foreach (BaseItem one in ribbonGalleryItem.BaseItems)
            {
                if (this.SelectCompnentMouseDown(one as ButtonGroupItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonControl.Refresh();
                        return true;
                    }
                }
            }
            //
            if (ribbonGalleryItem.ScrollUpButtonRectangle.Contains(point)) ribbonGalleryItem.TopViewItemIndex++;
            if (ribbonGalleryItem.ScrollDownButtonRectangle.Contains(point)) ribbonGalleryItem.TopViewItemIndex--;
            //
            return false;
        }
        private bool SelectCompnentMouseDown(ButtonGroupItem ribbonButtonGroupItem, Point point)
        {
            if (ribbonButtonGroupItem == null) return false;
            //
            foreach (BaseItem one in ribbonButtonGroupItem.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonControl.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            foreach (BaseItem one in ((ICollectionItem)this.m_RibbonControl).BaseItems)
            {
                if (one.GetType().Name == "RibbonQuickAccessToolbarItemEx" || one.GetType().Name == "RibbonPageContentContainerItem")
                {
                    if (this.SelectCompnentMouseUp_DG(one as BaseItemStackItem, point)) return true;
                }
                if (one.GetType().Name == "RibbonPageTabButtonContainerItem")
                {
                    if (this.SelectCompnentMouseUpTC(one as TabButtonContainerItem, point)) return true;
                }       
                ////
                //if (one.DesignMouseClickRectangleContainsEx(point))
                //{
                //    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                //    if (pSelectionService != null)
                //    {
                //        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                //        this.m_RibbonControl.Refresh();
                //        return true;
                //        this.m_Item2 = one;
                //        if (this.m_RibbonControl.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                //        {
                //            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                //            this.m_RibbonControl.Refresh();
                //            return true;
                //        }
                //        else
                //        {
                //            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                //            this.m_RibbonControl.Refresh();
                //            return true;
                //        }
                //    }
                //}
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUpTC(TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null) return false;
            //
            foreach (BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                RibbonPageTabButtonItem tabButtonItem = one as RibbonPageTabButtonItem;
                if (tabButtonItem != null) 
                {
                    if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item2 = tabButtonItem;
                            //if (ribbonTabButtonContainerItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                            if (this.ExchangeRibbonPage(this.m_Item1 as RibbonPageTabButtonItem, this.m_Item2 as RibbonPageTabButtonItem))
                            {
                                RibbonPageTabButtonItem tabButton = this.m_Item1 as RibbonPageTabButtonItem;
                                if (tabButton != null)
                                {
                                    RibbonPage ribbonPage = tabButton.pTabPageItem as RibbonPage;
                                    if (ribbonPage != null)
                                    {
                                        pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                                        this.m_RibbonControl.Refresh();
                                    }
                                }
                                return true;
                            }
                            else
                            {
                                RibbonPage ribbonPage = tabButtonItem.pTabPageItem as RibbonPage;
                                if (ribbonPage != null)
                                {
                                    pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                                    this.m_RibbonControl.Refresh();
                                }
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (this.SelectCompnentMouseUp(one as RibbonGalleryItem, point)) return true;
                    if (this.SelectCompnentMouseUp(one as ButtonGroupItem, point)) return true;
                    if (this.SelectCompnentMouseUp_DG(one as BaseItemStackExItem, point)) return true;
                    if (this.SelectCompnentMouseUp_DG(one as BaseItemStackItem, point)) return true;
                    if (this.SelectCompnentMouseUp_DG(one as CanvasItem, point)) return true;
                    //
                    if (one.DesignMouseClickRectangleContainsEx(point))
                    {
                        ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                        if (pSelectionService != null)
                        {
                            this.m_Item2 = one;
                            if (ribbonTabButtonContainerItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                            {
                                pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                                this.m_RibbonControl.Refresh();
                                return true;
                            }
                            else
                            {
                                pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                                this.m_RibbonControl.Refresh();
                                return true;
                            }
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp_DG(CanvasItem canvasItem, Point point)
        {
            if (canvasItem == null) return false;
            //
            foreach (BaseItem one in canvasItem.BaseItems)
            {
                if (this.SelectCompnentMouseUp(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseUp(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as CanvasItem, point)) return true;
                ////
                //if (one.DesignMouseClickRectangleContainsEx(point))
                //{
                //    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                //    if (pSelectionService != null)
                //    {
                //        this.m_Item2 = one;
                //        if (canvasItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                //        {
                //            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                //            this.m_BaseBar.Refresh();
                //            return true;
                //        }
                //        else
                //        {
                //            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                //            this.m_BaseBar.Refresh();
                //            return true;
                //        }
                //    }
                //}
            }
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
                        this.m_RibbonControl.Refresh();
                    }
                }
                //
                ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                if (pSelectionService != null)
                {
                    pSelectionService.SetSelectedComponents(new Component[] { m_Item1 as Component }, SelectionTypes.Primary);
                }
                //
                this.m_Item1 = null;
                //
                return true;
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
        private bool SelectCompnentMouseUp_DG(BaseItemStackItem ribbonBaseItemStackItem, Point point)
        {
            if (ribbonBaseItemStackItem == null) return false;
            //
            foreach (BaseItem one in ribbonBaseItemStackItem.BaseItems)
            {
                if (this.SelectCompnentMouseUp(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseUp(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as CanvasItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (ribbonBaseItemStackItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp_DG(BaseItemStackExItem ribbonBaseItemStackExItem, Point point)
        {
            if (ribbonBaseItemStackExItem == null) return false;
            //
            foreach (BaseItem one in ribbonBaseItemStackExItem.BaseItems)
            {
                if (this.SelectCompnentMouseUp(one as RibbonGalleryItem, point)) return true;
                if (this.SelectCompnentMouseUp(one as ButtonGroupItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackExItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as BaseItemStackItem, point)) return true;
                if (this.SelectCompnentMouseUp_DG(one as CanvasItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (ribbonBaseItemStackExItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(RibbonGalleryItem ribbonGalleryItem, Point point)
        {
            if (ribbonGalleryItem == null) return false;
            //
            foreach (BaseItem one in ribbonGalleryItem.BaseItems)
            {
                if (this.SelectCompnentMouseUp(one as ButtonGroupItem, point)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (ribbonGalleryItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(ButtonGroupItem ribbonButtonGroupItem, Point point)
        {
            if (ribbonButtonGroupItem == null) return false;
            //
            foreach (BaseItem one in ribbonButtonGroupItem.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (ribbonButtonGroupItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonControl.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool ExchangeRibbonPage(RibbonPageTabButtonItem item1, RibbonPageTabButtonItem item2)
        {
            if (item1 == null || item2 == null) return false;
            return this.m_RibbonControl.RibbonPages.ExchangeItem(item1.pTabPageItem as RibbonPage, item2.pTabPageItem as RibbonPage);
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            //BaseItemCollectionEditerForm baseItemCollectionDesignerForm = new BaseItemCollectionEditerForm(this.m_RibbonControl);
            //baseItemCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            //baseItemCollectionDesignerForm.TopMost = true;
            //baseItemCollectionDesignerForm.Location = new Point(360, 150);
            //baseItemCollectionDesignerForm.Show();
            BaseItemCollectionEditerFormEx baseItemCollectionDesignerForm = new BaseItemCollectionEditerFormEx(this.m_RibbonControl);
            baseItemCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerForm.TopMost = true;
            baseItemCollectionDesignerForm.Location = new Point(360, 150);
            baseItemCollectionDesignerForm.Show();
        }

        //

        #region old
        //private void AddRibbonPageToRibbonPages(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        RibbonPage ribbonPage = host.CreateComponent(typeof(RibbonPage)) as RibbonPage;
        //        ribbonPage.Text = ribbonPage.Name;
        //        ribbonPage.Dock = DockStyle.Fill;
        //        this.m_RibbonControl.RibbonPages.Add(ribbonPage);
        //    }
        //}

        //private void AddBaseButtonItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }
        //}

        //private void AddDropDownButtonItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        DropDownButtonItem baseItem = host.CreateComponent(typeof(DropDownButtonItem)) as DropDownButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }
        //}

        //private void AddSplitButtonItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SplitButtonItem baseItem = host.CreateComponent(typeof(SplitButtonItem)) as SplitButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }
        //}

        //private void AddButtonItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }
        //}

        //private void AddCheckButtonItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }

        //}

        //private void AddSeparatorItemToToolbarItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonControl.ToolbarItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseButtonItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }
        //}

        //private void AddDropDownButtonItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        DropDownButtonItem baseItem = host.CreateComponent(typeof(DropDownButtonItem)) as DropDownButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }
        //}

        //private void AddSplitButtonItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SplitButtonItem baseItem = host.CreateComponent(typeof(SplitButtonItem)) as SplitButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }
        //}

        //private void AddButtonItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }
        //}

        //private void AddCheckButtonItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }

        //}

        //private void AddTextBoxItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        TextBoxItem baseItem = host.CreateComponent(typeof(TextBoxItem)) as TextBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }

        //}

        //private void AddComboBoxItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboBoxItem baseItem = host.CreateComponent(typeof(ComboBoxItem)) as ComboBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }

        //}

        //private void AddComboTreeItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboTreeItem baseItem = host.CreateComponent(typeof(ComboTreeItem)) as ComboTreeItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }

        //}

        //private void AddSeparatorItemToPageContents(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonControl.PageContents.Add(baseItem);
        //    }
        //}

        ////

        //private void AddMenuButtonItemToMenuItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        MenuButtonItem baseItem = host.CreateComponent(typeof(MenuButtonItem)) as MenuButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        baseItem.ShowNomalState = false;
        //        baseItem.ShowNomalSplitLine = false;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        this.m_RibbonControl.ApplicationPopup.MenuItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToMenuItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonControl.ApplicationPopup.MenuItems.Add(baseItem);
        //    }
        //}

        //private void AddLabelSeparatorItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LabelSeparatorItem baseItem = host.CreateComponent(typeof(LabelSeparatorItem)) as LabelSeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.ApplicationPopup.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseButtonItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.TextAlign = ContentAlignment.MiddleLeft;
        //        baseItem.Size = new Size(60, 21);
        //        baseItem.ShowNomalState = false;
        //        this.m_RibbonControl.ApplicationPopup.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonControl.ApplicationPopup.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseButtonItemToOperationItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.AutoPlanTextRectangle = true;
        //        baseItem.ImageAlign = ContentAlignment.MiddleLeft;
        //        baseItem.TextAlign = ContentAlignment.MiddleLeft;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonControl.ApplicationPopup.OperationItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToOperationItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonControl.ApplicationPopup.OperationItems.Add(baseItem);
        //    }
        //}
        #endregion

        private void ShowApplicationPopup(object sender, EventArgs ea)
        {
            this.m_RibbonControl.ShowApplicationPopup();
        }

        private void CloseApplicationPopup(object sender, EventArgs ea)
        {
            this.m_RibbonControl.CloseApplicationPopup();
        }

        private void CheckAndRepair(object sender, EventArgs ea)
        {
            this.m_RibbonControl.RibbonPages.CheckAndRepair();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        #region 已抛弃
        //private class BaseItemCollectionEditerForm : BaseItemCollectionDesignerForm
        //{
        //    private RibbonControlEx m_RibbonControl = null;

        //    public BaseItemCollectionEditerForm(RibbonControlEx ribbonControl)
        //        : base(ribbonControl)
        //    {
        //        this.m_RibbonControl = ribbonControl;
        //        //
        //        TreeNode node = new TreeNode();
        //        this.BuildTree_DG(this.m_RibbonControl.ApplicationPopup, node.Nodes);
        //        this.InsertTreeNode(new int[] { 0 }, 2, node.Nodes[0]);
        //        //
        //        WinForm.TitleTreeNodeItem node2 = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //        node2.Name = "RibbonPages";
        //        node2.Text = "功能区面板集合";
        //        node2.Tag = this.m_RibbonControl.RibbonPages;
        //        foreach (IBaseItem one in this.m_RibbonControl.RibbonPages)
        //        {
        //            this.BuildTree_DG(one, node2.Nodes);
        //        }
        //        this.InsertTreeNode(new int[] { 0 }, 3, node2);
        //    }

        //    protected override bool FiltrationShowPopup(object value)
        //    {
        //        if (value == null) return false;
        //        //
        //        if (value.GetType().Name == "RibbonControlEx") return false;
        //        if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
        //        if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
        //        if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
        //        //
        //        if (value.GetType().Name == "RibbonApplicationPopup") 
        //        {
        //            return false;
        //        }
        //        //
        //        if (value.GetType().Name == "RibbonPageCollection")
        //        {
        //            return true;
        //        }
        //        //
        //        return base.FiltrationShowPopup(value);
        //    }

        //    protected override bool FiltrationSelected(object value)
        //    {
        //        if (value == null) return false;
        //        //
        //        if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
        //        if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
        //        if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
        //        //
        //        if (value.GetType().Name == "RibbonApplicationPopup") return false;
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return false;
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return false;
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return false;
        //        //
        //        if (value.GetType().Name == "RibbonQuickAccessToolbarItemEx") return false;
        //        if (value.GetType().Name == "RibbonPageContentContainerItem") return false;
        //        //
        //        return base.FiltrationSelected(value);
        //    }

        //    protected override bool FiltrationBaseItem(object value)
        //    {
        //        if (value == null) return false;
        //        //
        //        if (value.GetType().Name == "RibbonStartButtonItem2007Ex") return false;
        //        if (value.GetType().Name == "RibbonStartButtonItem2010Ex") return false;
        //        if (value.GetType().Name == "RibbonFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonMdiFormButtonStackItem") return false;
        //        if (value.GetType().Name == "RibbonPageTabButtonContainerItem") return false;
        //        //
        //        return base.FiltrationBaseItem(value);
        //    }

        //    protected override string GetTypeDescription(object value)
        //    {
        //        if (value == null) return "null";
        //        //
        //        if (value.GetType().Name == "RibbonQuickAccessToolbarItemEx") return "快捷工具条";
        //        if (value.GetType().Name == "RibbonPageContentContainerItem") return "功能区面板右侧按钮列表";
        //        //
        //        if (value.GetType().Name == "RibbonApplicationPopup") return "应用程序快捷菜单";
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return "菜单栏";
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return "记录栏";
        //        if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return "操作栏";
        //        //
        //        return base.GetTypeDescription(value);
        //    }

        //    protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        //    {
        //        Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = base.CreateNewItemTypesDictionary();
        //        //
        //        typeCreateNewItemTypesDictionary.Add
        //            (
        //            "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonQuickAccessToolbarItemEx",
        //            new Type[] { typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(DropDownButtonItem), typeof(SplitButtonItem), typeof(ButtonItem), typeof(LabelItem), typeof(LinkLabelItem), typeof(SeparatorItem) }
        //            );
        //        typeCreateNewItemTypesDictionary.Add
        //            (
        //            "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonPageContentContainerItem",
        //            new Type[] { typeof(TextBoxItem), typeof(ComboBoxItem), typeof(ComboTreeItem), typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(DropDownButtonItem), typeof(SplitButtonItem), typeof(ButtonItem), typeof(LabelItem), typeof(LinkLabelItem), typeof(SeparatorItem) }
        //            );
        //        typeCreateNewItemTypesDictionary.Add
        //            (
        //            "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonPageCollection",
        //            new Type[] { typeof(RibbonPage) }
        //            );
        //        //
        //        return typeCreateNewItemTypesDictionary;
        //    }

        //    protected override TreeNode CreateNode(object value)
        //    {
        //        if (value is ICollectionItem)
        //        {
        //            WinForm.TitleTreeNodeItem node = new GISShare.Controls.WinForm.TitleTreeNodeItem();
        //            //node.UsingNodeRegionStyle = true;
        //            return node;
        //        }
        //        //
        //        return base.CreateNode(value);
        //    }

        //    protected internal override void SelectedComponent(Component component)
        //    {
        //        base.SelectedComponent(component);
        //        //
        //        if (component is RibbonPage) 
        //        {
        //            RibbonPage item = component as RibbonPage;
        //            int index = this.m_RibbonControl.RibbonPages.IndexOf(item);
        //            if (index >= 0 && index != this.m_RibbonControl.RibbonPageSelectedIndex)
        //            {
        //                this.m_RibbonControl.RibbonPageSelectedIndex = index;
        //            }
        //        }
        //    }

        //    protected override bool SetCreateTypeInfo(IComponent pComponent)
        //    {
        //        if (pComponent is IRibbonPageItem)
        //        {
        //            IRibbonPageItem pItem = pComponent as IRibbonPageItem;
        //            pItem.LineDistance = 2;
        //            pItem.ColumnDistance = 2;
        //            pItem.ShowBackgroudState = true;
        //            pItem.ShowNomalBackgroudState = true;
        //        }
        //        else if (pComponent is IRibbonBarItem)
        //        {
        //            IRibbonBarItem pItem = pComponent as IRibbonBarItem;
        //            pItem.Padding = new Padding(3, 3, 3, 2);
        //        }
        //        else if (pComponent is ITextBoxItem)
        //        {
        //            ITextBoxItem pItem = pComponent as ITextBoxItem;
        //            pItem.Size = new Size(100, 21);
        //        }
        //        //
        //        return base.SetCreateTypeInfo(pComponent);
        //    }
        //}
        #endregion

        private class BaseItemCollectionEditerFormEx : WFNew.Design.BaseItemCollectionDesignerForm
        {
            private RibbonControlEx m_RibbonControl = null;

            public BaseItemCollectionEditerFormEx(RibbonControlEx ribbonControl)
                : base(ribbonControl)
            {
                this.m_RibbonControl = ribbonControl;
                //
                View.NodeViewItem node = new View.NodeViewItem();
                this.BuildTree_DG(this.m_RibbonControl.ApplicationPopup as IObjectDesignHelper, node.NodeViewItems);
                this.InsertTreeNode(new int[] { 0 }, 2, node.NodeViewItems[0]);
                //
                View.NodeViewItem node2 = new View.NodeViewItem();
                node2.Name = "RibbonPages";
                node2.Text = "功能区面板集合";
                node2.ShowNomalState = true;
                node2.Tag = this.m_RibbonControl.RibbonPages;
                foreach (IObjectDesignHelper one in this.m_RibbonControl.RibbonPages)
                {
                    if (one != null) this.BuildTree_DG(one, node2.NodeViewItems);
                }
                this.InsertTreeNode(new int[] { 0 }, 3, node2);
            }

            protected override bool FiltrationShowPopup(object value)
            {
                if (value == null) return false;
                //
                if (value.GetType().Name == "RibbonControlEx") return false;
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
                    "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonQuickAccessToolbarItemEx",
                    new Type[] { typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(DropDownButtonItem), typeof(SplitButtonItem), typeof(ButtonItem), typeof(LabelItem), typeof(LinkLabelItem), typeof(SeparatorItem) }
                    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonPageContentContainerItem",
                    new Type[] { typeof(TextBoxItem), typeof(ComboBoxItem), typeof(ComboTreeItem), typeof(BaseButtonItem), typeof(CheckButtonItem), typeof(DropDownButtonItem), typeof(SplitButtonItem), typeof(ButtonItem), typeof(LabelItem), typeof(LinkLabelItem), typeof(SeparatorItem) }
                    );
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.RibbonControlEx+RibbonPageCollection",
                    new Type[] { typeof(RibbonPage) }
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
                if (component is RibbonPage)
                {
                    RibbonPage item = component as RibbonPage;
                    int index = this.m_RibbonControl.RibbonPages.IndexOf(item);
                    if (index >= 0 && index != this.m_RibbonControl.RibbonPageSelectedIndex)
                    {
                        this.m_RibbonControl.RibbonPageSelectedIndex = index;
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
}

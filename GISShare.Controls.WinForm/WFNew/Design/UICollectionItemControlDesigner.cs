using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class UICollectionItemControlDesigner : ControlDesigner
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
                IUICollectionItem pUICollectionItem = this.m_Control as IUICollectionItem;
                if (pUICollectionItem != null)
                {
                    foreach (BaseItem one in pUICollectionItem.BaseItems)
                    {
                        one.Dispose();
                    }
                    pUICollectionItem.BaseItems.Clear();
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
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
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
            IUICollectionItem pUICollectionItem = this.m_Control as IUICollectionItem;
            if (pUICollectionItem == null) return false;
            //
            foreach (BaseItem one in pUICollectionItem.BaseItems)
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
                        this.m_Control.Refresh();
                        return true;
                    }
                }
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
                        this.m_Control.Refresh();
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
                        this.m_Control.Refresh();
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
                        this.m_Control.Refresh();
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
                        this.m_Control.Refresh();
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
                        this.m_Control.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            IUICollectionItem pUICollectionItem = this.m_Control as IUICollectionItem;
            if (pUICollectionItem == null) return false;
            //
            foreach (BaseItem one in pUICollectionItem.BaseItems)
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
                        if (pUICollectionItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
                            return true;
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
                        this.m_Control.Refresh();
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
                            this.m_Control.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
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
                            this.m_Control.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
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
                            this.m_Control.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
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
                            this.m_Control.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_Control.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.Control as IObjectDesignHelper);
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


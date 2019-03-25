using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class BaseItemStackDesigner : ControlDesigner
    {
        private BaseItemStack m_BaseItemStack = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_BaseItemStack = base.Component as BaseItemStack;
            if (this.m_BaseItemStack == null)
            {
                this.DisplayError(new ArgumentException("BaseItemStack == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_BaseItemStack.BaseItems)
                {
                    one.Dispose();
                }
                this.m_BaseItemStack.BaseItems.Clear();
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
                //verbs.Add(new DesignerVerb("添加 LabelItem", new EventHandler(AddLabelItem)));
                //verbs.Add(new DesignerVerb("添加 LinkLabelItem", new EventHandler(AddLinkLabelItem)));
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                //verbs.Add(new DesignerVerb("添加 DropDownButtonItem", new EventHandler(AddDropDownButtonItem)));
                //verbs.Add(new DesignerVerb("添加 SplitButtonItem", new EventHandler(AddSplitButtonItem)));
                //verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                //verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                //verbs.Add(new DesignerVerb("添加 CheckBoxItem", new EventHandler(AddCheckBoxItem)));
                //verbs.Add(new DesignerVerb("添加 RadioButtonItem", new EventHandler(AddRadioButtonItem)));
                //verbs.Add(new DesignerVerb("添加 TextBoxItem", new EventHandler(AddTextBoxItem)));
                //verbs.Add(new DesignerVerb("添加 ComboBoxItem", new EventHandler(AddComboBoxItem)));
                //verbs.Add(new DesignerVerb("添加 ComboTreeItem", new EventHandler(AddComboTreeItem)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem", new EventHandler(AddSeparatorItem)));
                //verbs.Add(new DesignerVerb("添加 LabelSeparatorItem", new EventHandler(AddLabelSeparatorItem)));
                //verbs.Add(new DesignerVerb("添加 ButtonGroupItem", new EventHandler(AddButtonGroupItem)));
                //verbs.Add(new DesignerVerb("添加 RibbonGalleryItem", new EventHandler(AddRibbonGalleryItem)));
                //verbs.Add(new DesignerVerb("添加 BaseItemStackItem", new EventHandler(AddBaseItemStackItem)));
                //verbs.Add(new DesignerVerb("添加 BaseItemStackExItem", new EventHandler(AddBaseItemStackExItem)));
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
            point = this.m_BaseItemStack.PointToClient(point);
            return new Rectangle(point, baseItem.DesignMouseSelectedRectangle.Size);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_BaseItemStack.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_BaseItemStack.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_BaseItemStack.Handle != m.HWnd) break;
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
            foreach (BaseItem one in this.m_BaseItemStack.BaseItems)
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
                        this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            foreach (BaseItem one in this.m_BaseItemStack.BaseItems)
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
                        if (this.m_BaseItemStack.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
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
                        this.m_BaseItemStack.Refresh();
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
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
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
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
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
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
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
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_BaseItemStack.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        #region old
        //private void AddLabelItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LabelItem baseItem = host.CreateComponent(typeof(LabelItem)) as LabelItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddLinkLabelItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LinkLabelItem baseItem = host.CreateComponent(typeof(LinkLabelItem)) as LinkLabelItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddBaseButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddDropDownButtonItem(object sender, EventArgs ea)
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
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddSplitButtonItem(object sender, EventArgs ea)
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
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddCheckButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddCheckBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckBoxItem baseItem = host.CreateComponent(typeof(CheckBoxItem)) as CheckBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddRadioButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        RadioButtonItem baseItem = host.CreateComponent(typeof(RadioButtonItem)) as RadioButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddTextBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        TextBoxItem baseItem = host.CreateComponent(typeof(TextBoxItem)) as TextBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddComboBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboBoxItem baseItem = host.CreateComponent(typeof(ComboBoxItem)) as ComboBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddComboTreeItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboTreeItem baseItem = host.CreateComponent(typeof(ComboTreeItem)) as ComboTreeItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddSeparatorItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        switch (this.m_BaseItemStack.eOrientation) 
        //        {
        //            case Orientation.Vertical:
        //                baseItem.eOrientation = Orientation.Horizontal;
        //                baseItem.Size = new Size(23, 2);
        //                break;
        //            case Orientation.Horizontal:
        //                baseItem.eOrientation = Orientation.Vertical;
        //                baseItem.Size = new Size(2, 23);
        //                break;
        //        }
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddLabelSeparatorItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LabelSeparatorItem baseItem = host.CreateComponent(typeof(LabelSeparatorItem)) as LabelSeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddButtonGroupItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ButtonGroupItem baseItem = host.CreateComponent(typeof(ButtonGroupItem)) as ButtonGroupItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(23, 23);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddRibbonGalleryItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        RibbonGalleryItem baseItem = host.CreateComponent(typeof(RibbonGalleryItem)) as RibbonGalleryItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(70, 70);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseItemStackItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseItemStackItem baseItem = host.CreateComponent(typeof(BaseItemStackItem)) as BaseItemStackItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        switch (this.m_BaseItemStack.eOrientation) 
        //        {
        //            case Orientation.Horizontal:
        //                baseItem.eOrientation = Orientation.Vertical;
        //                baseItem.ColumnDistance = 0;
        //                break;
        //            case Orientation.Vertical:
        //                baseItem.eOrientation = Orientation.Horizontal;
        //                baseItem.LineDistance = 0;
        //                break;
        //        }
        //        baseItem.Size = new Size(60, 23);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseItemStackExItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseItemStackExItem baseItem = host.CreateComponent(typeof(BaseItemStackExItem)) as BaseItemStackExItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.IsStretchItems = true;
        //        baseItem.IsRestrictSize = false;
        //        switch (this.m_BaseItemStack.eOrientation)
        //        {
        //            case Orientation.Horizontal:
        //                baseItem.eOrientation = Orientation.Vertical;
        //                baseItem.ColumnDistance = 0;
        //                break;
        //            case Orientation.Vertical:
        //                baseItem.eOrientation = Orientation.Horizontal;
        //                baseItem.LineDistance = 0;
        //                break;
        //        }
        //        baseItem.Size = new Size(60, 23);
        //        this.m_BaseItemStack.BaseItems.Add(baseItem);
        //    }
        //}
        #endregion

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_BaseItemStack);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

    }
}

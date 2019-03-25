using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonGalleryDesigner : ControlDesigner
    {
        private RibbonGallery m_RibbonGallery = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonGallery = base.Component as RibbonGallery;
            if (this.m_RibbonGallery == null)
            {
                this.DisplayError(new ArgumentException("RibbonGallery == null"));
                return;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("添加 RibbonGalleryRowItem", new EventHandler(AddRibbonGalleryRowItem)));
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                //
                return verbs;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonGallery.BaseItems)
                {
                    one.Dispose();
                }
                this.m_RibbonGallery.BaseItems.Clear();
            }
            base.Dispose(disposing);
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
            point = this.m_RibbonGallery.PointToClient(point);
            return new Rectangle(point, baseItem.DesignMouseSelectedRectangle.Size);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_RibbonGallery.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_RibbonGallery.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_RibbonGallery.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseUp(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }
        private BaseItem m_Item1 = null;
        private BaseItem m_Item2 = null;
        private bool SelectCompnentMouseDown(Point point)
        {
            foreach (BaseItem one in this.m_RibbonGallery.BaseItems)
            {
                if (this.SelectCompnentMouseDownDG(point, one as RibbonGalleryRowItem)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonGallery.Refresh();
                        return true;
                    }
                }
            }
            //
            if (this.m_RibbonGallery.ScrollUpButtonRectangle.Contains(point)) this.m_RibbonGallery.TopViewItemIndex++;
            if (this.m_RibbonGallery.ScrollDownButtonRectangle.Contains(point)) this.m_RibbonGallery.TopViewItemIndex--;
            //
            return false;
        }
        private bool SelectCompnentMouseDownDG(Point point, RibbonGalleryRowItem ribbonGalleryRowItem)
        {
            if (ribbonGalleryRowItem == null) return false;
            //
            foreach (BaseItem one in ribbonGalleryRowItem.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_RibbonGallery.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            foreach (BaseItem one in this.m_RibbonGallery.BaseItems)
            {
                if (this.SelectCompnentMouseUpDG(point, one as RibbonGalleryRowItem)) return true;
                //
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (this.m_RibbonGallery.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonGallery.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonGallery.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUpDG(Point point, RibbonGalleryRowItem ribbonGalleryRowItem)
        {
            if (ribbonGalleryRowItem == null) return false;
            //
            foreach (BaseItem one in ribbonGalleryRowItem.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (ribbonGalleryRowItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_RibbonGallery.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_RibbonGallery.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        private void AddRibbonGalleryRowItem(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                RibbonGalleryRowItem baseItem = host.CreateComponent(typeof(RibbonGalleryRowItem)) as RibbonGalleryRowItem;
                baseItem.Name = baseItem.Site.Name;
                baseItem.Text = baseItem.Name;
                baseItem.LineDistance = 1;
                baseItem.Size = new Size(100, 21);
                this.m_RibbonGallery.BaseItems.Add(baseItem);
            }
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_RibbonGallery);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

    }
}

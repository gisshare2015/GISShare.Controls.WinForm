using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ButtonGroupDesigner : ControlDesigner
    {
        private ButtonGroup m_ButtonGroup = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ButtonGroup = base.Component as ButtonGroup;
            if (this.m_ButtonGroup == null)
            {
                this.DisplayError(new ArgumentException("ButtonGroup == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach(BaseItem one in this.m_ButtonGroup.BaseItems)
                {
                    one.Dispose();
                }
                this.m_ButtonGroup.BaseItems.Clear();
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
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                //verbs.Add(new DesignerVerb("添加 DropDownButtonItem", new EventHandler(AddDropDownButtonItem)));
                //verbs.Add(new DesignerVerb("添加 SplitButtonItem", new EventHandler(AddSplitButtonItem)));
                //verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                //verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
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
            point = this.m_ButtonGroup.PointToClient(point);
            return new Rectangle(point, baseItem.DesignMouseSelectedRectangle.Size);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_ButtonGroup.Created)
            {
                switch (m.Msg)
                {
                    //case (int)GISShare.Win32.Msgs.WM_KEYUP://0x101
                    //    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("WM_KEYUP" + m.WParam.ToString() + "  " + m.LParam.ToString()); 
                    //    break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_ButtonGroup.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_ButtonGroup.Handle != m.HWnd) break;
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
            foreach (BaseItem one in this.m_ButtonGroup.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = one;
                        pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                        this.m_ButtonGroup.Refresh();
                        return true;
                    }
                }
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUp(Point point)
        {
            foreach (BaseItem one in this.m_ButtonGroup.BaseItems)
            {
                if (one.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = one;
                        if (this.m_ButtonGroup.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { this.m_Item1 as Component }, SelectionTypes.Primary);
                            this.m_ButtonGroup.Refresh();
                            return true;
                        }
                        else
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { one as Component }, SelectionTypes.Primary);
                            this.m_ButtonGroup.Refresh();
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        #region old
        //private void AddBaseButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(23, 23);
        //        this.m_ButtonGroup.BaseItems.Add(baseItem);
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
        //        baseItem.Size = new Size(23, 23);
        //        this.m_ButtonGroup.BaseItems.Add(baseItem);
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
        //        baseItem.Size = new Size(23, 23);
        //        this.m_ButtonGroup.BaseItems.Add(baseItem);
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
        //        baseItem.Size = new Size(23, 23);
        //        this.m_ButtonGroup.BaseItems.Add(baseItem);
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
        //        baseItem.Size = new Size(23, 21);
        //        this.m_ButtonGroup.BaseItems.Add(baseItem);
        //    }
        //}
        #endregion

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            //BaseItemCollectionEditerForm baseItemCollectionDesignerForm = new BaseItemCollectionEditerForm(this.m_RibbonControl);
            //baseItemCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            //baseItemCollectionDesignerForm.TopMost = true;
            //baseItemCollectionDesignerForm.Location = new Point(360, 150);
            //baseItemCollectionDesignerForm.Show();
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_ButtonGroup);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}

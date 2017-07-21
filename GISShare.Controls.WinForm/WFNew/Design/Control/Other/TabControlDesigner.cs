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
    public class TabControlDesigner : ControlDesigner
    {
        private TabControl m_TabControl = null;

        private DesignerVerbCollection verbs;

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if( verbs == null )
				{
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("添加页面", new EventHandler(AddTabPage)));
                    verbs.Add(new DesignerVerb("移除页面", new EventHandler(RemoveTabPage)));
                    verbs.Add(new DesignerVerb("上一个", new EventHandler(BackTabPage)));
                    verbs.Add(new DesignerVerb("下一个", new EventHandler(NextTabPage)));
                    verbs.Add(new DesignerVerb("顶部停靠", new EventHandler(DockToTop)));
                    verbs.Add(new DesignerVerb("左边停靠", new EventHandler(DockToLeft)));
                    verbs.Add(new DesignerVerb("右边停靠", new EventHandler(DockToRight)));
                    verbs.Add(new DesignerVerb("底部停靠", new EventHandler(DockToBottom)));
                    verbs.Add(new DesignerVerb("填充", new EventHandler(DockToFill)));
                    verbs.Add(new DesignerVerb("不停靠", new EventHandler(DockToNone)));
                    verbs.Add(new DesignerVerb("检测和修复TabControl", new EventHandler(CheckAndRepair)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
				}
				return verbs;
			}
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                service.ComponentRemoved += new ComponentEventHandler(ComponentRemoved);
            }
            //
            this.m_TabControl = base.Component as TabControl;
            if (this.m_TabControl == null)
            {
                this.DisplayError(new ArgumentException("TabControl == null"));
                return;
            }
        }
        private void ComponentRemoved(object sender, ComponentEventArgs cea)
        {
            TabPage tabPage = cea.Component as TabPage;
            if (tabPage != null)
            {
                this.m_TabControl.TabPages.Remove(tabPage);
            }      
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_TabControl.Height < 16) return;
            //
            if (m_TabControl != null)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_TabControl.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_TabControl.Text, this.m_TabControl.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_TabControl.Text,
                        this.m_TabControl.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_TabControl.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_TabControl.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_TabControl.Handle != m.HWnd) break;
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
            foreach (BaseItem one in ((ICollectionItem)this.m_TabControl).BaseItems)
            {
                if(this.SelectCompnentMouseDownTC(one as TabButtonContainerItem, point)) return true;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDownTC(TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null || !ribbonTabButtonContainerItem.Visible) return false;
            //
            foreach (BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                TabButtonItem tabButtonItem = one as TabButtonItem;
                if (tabButtonItem == null) continue;
                if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = tabButtonItem;
                        TabPage ribbonPage = tabButtonItem.pTabPageItem as TabPage;
                        if (ribbonPage != null)
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                            this.m_TabControl.Refresh();
                        }
                        this.m_TabControl.TabPageSelectedIndex = ribbonTabButtonContainerItem.BaseItems.IndexOf(tabButtonItem);
                        //
                        return true;
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
        private bool SelectCompnentMouseUp(Point point)
        {
            foreach (BaseItem one in ((ICollectionItem)this.m_TabControl).BaseItems)
            {
                if(this.SelectCompnentMouseUpTC(one as TabButtonContainerItem, point)) return true;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUpTC(TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null || !ribbonTabButtonContainerItem.Visible) return false;
            //
            foreach (BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                TabButtonItem tabButtonItem = one as TabButtonItem;
                if (tabButtonItem == null) continue;
                if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = tabButtonItem;
                        if (ribbonTabButtonContainerItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            TabButtonItem tabButton = this.m_Item1 as TabButtonItem;
                            if (tabButton != null)
                            {
                                TabPage ribbonPage = tabButton.pTabPageItem as TabPage;
                                if (ribbonPage != null)
                                {
                                    pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                                    this.m_TabControl.Refresh();
                                }
                            }
                            return true;
                        }
                        else
                        {
                            TabPage ribbonPage = tabButtonItem.pTabPageItem as TabPage;
                            if (ribbonPage != null)
                            {
                                pSelectionService.SetSelectedComponents(new Component[] { ribbonPage as Component }, SelectionTypes.Primary);
                                this.m_TabControl.Refresh();
                            }
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            TabPageCollectionDesignerForm tabPageCollectionDesignerForm = new TabPageCollectionDesignerForm(this.m_TabControl);
            tabPageCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            tabPageCollectionDesignerForm.TopMost = true;
            tabPageCollectionDesignerForm.Location = new Point(360, 150);
            tabPageCollectionDesignerForm.Show();
        }
        
        private void AddTabPage(object sender, EventArgs ea)
		{
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
			{
                TabPage tabPage = host.CreateComponent(typeof(TabPage)) as TabPage;
                tabPage.Text = tabPage.Name;
                this.m_TabControl.TabPages.Add(tabPage);
			}
		}

        private void RemoveTabPage(object sender, EventArgs ea)
		{
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                TabPage tabPage = this.m_TabControl.SelectedTabPage as TabPage;//key
                if (tabPage == null) return;
                this.m_TabControl.TabPages.Remove(tabPage);
                host.DestroyComponent(tabPage);
            }
        }

        private void BackTabPage(object sender, EventArgs ea)
        {
            int index = this.m_TabControl.TabPageSelectedIndex - 1;
            if (index < 0 || index >= this.m_TabControl.TabPages.Count) return;
            this.m_TabControl.TabPageSelectedIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabPageSelectedIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabPageSelectedIndex"], this.m_TabControl.TabPageSelectedIndex, index);
        }

        private void NextTabPage(object sender, EventArgs ea)
        {
            int index = this.m_TabControl.TabPageSelectedIndex + 1;
            if (index < 0 || index >= this.m_TabControl.TabPages.Count) return;
            this.m_TabControl.TabPageSelectedIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabPageSelectedIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabPageSelectedIndex"], this.m_TabControl.TabPageSelectedIndex, index);
        }
        
        private void DockToTop(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.Top) return;
            //
            switch (this.m_TabControl.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_TabControl.Width;
                    this.m_TabControl.Dock = DockStyle.Top;
                    this.m_TabControl.Height = iWidth;
                    break;
                default:
                    this.m_TabControl.Dock = DockStyle.Top;
                    break;
            }
        }
        private void DockToLeft(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.Left) return;
            //
            switch (this.m_TabControl.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_TabControl.Height;
                    this.m_TabControl.Dock = DockStyle.Left;
                    this.m_TabControl.Width = iHeight;
                    break;
                default:
                    this.m_TabControl.Dock = DockStyle.Left;
                    break;
            }
        }
        private void DockToRight(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.Right) return;
            //
            switch (this.m_TabControl.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_TabControl.Height;
                    this.m_TabControl.Dock = DockStyle.Right;
                    this.m_TabControl.Width = iHeight;
                    break;
                default:
                    this.m_TabControl.Dock = DockStyle.Right;
                    break;
            }
        }
        private void DockToBottom(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.Bottom) return;
            //
            switch (this.m_TabControl.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_TabControl.Width;
                    this.m_TabControl.Dock = DockStyle.Bottom;
                    this.m_TabControl.Height = iWidth;
                    break;
                default:
                    this.m_TabControl.Dock = DockStyle.Bottom;
                    break;
            }
        }
        private void DockToFill(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.Fill) return;
            //
            this.m_TabControl.Dock = DockStyle.Fill;
        }
        private void DockToNone(object sender, EventArgs e)
        {
            if (this.m_TabControl.Dock == DockStyle.None) return;
            //
            this.m_TabControl.Dock = DockStyle.None;
        }

        private void CheckAndRepair(object sender, EventArgs ea)
        {
            this.m_TabControl.TabPages.CheckAndRepair();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        public class TabPageCollectionDesignerForm : Design.CollectionDesignerForm
        {
            public TabPageCollectionDesignerForm(IObjectDesignHelper pObjectDesignHelper)
                : base(pObjectDesignHelper) { }

            /// <summary>
            /// 默认的加载子项的类型数组
            /// </summary>
            /// <returns></returns>
            protected override Type[] CreateNewItemTypes()
            {
                return new Type[] { typeof(TabPage) };
            }

            /// <summary>
            /// 各类型 对应的加载子项的类型数组
            /// </summary>
            /// <returns></returns>
            protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
            {
                Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = new Dictionary<string, Type[]>();
                //
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.TabControl",
                    new Type[] { typeof(TabPage) }
                    );
                //
                return typeCreateNewItemTypesDictionary;
            }
        }
    }
}

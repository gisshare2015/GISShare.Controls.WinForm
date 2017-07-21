using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel.Design
{
    public class DockPanelDesigner : ParentControlDesigner
    {
        private DockPanel m_DockPanel = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("添加面板页面", new EventHandler(AddBasePanel)));
                    verbs.Add(new DesignerVerb("移除面板页面", new EventHandler(RemoveBasePanel)));
                    verbs.Add(new DesignerVerb("在顶部添加面板", new EventHandler(AddTopDockPanel)));
                    verbs.Add(new DesignerVerb("在左边添加面板", new EventHandler(AddLeftDockPanel)));
                    verbs.Add(new DesignerVerb("在右边添加面板", new EventHandler(AddRightDockPanel)));
                    verbs.Add(new DesignerVerb("在底部添加面板", new EventHandler(AddBottomDockPanel)));
                    verbs.Add(new DesignerVerb("上一个", new EventHandler(BackBasePanel)));
                    verbs.Add(new DesignerVerb("下一个", new EventHandler(NextBasePanel)));
                    verbs.Add(new DesignerVerb("检测和修复DockPanel", new EventHandler(CheckAndRepair)));
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
            this.m_DockPanel = base.Component as DockPanel;
            if (this.m_DockPanel == null)
            {
                this.DisplayError(new ArgumentException("DockPanel == null"));
                return;
            }
        }

        private void ComponentRemoved(object sender, ComponentEventArgs cea)
        {
            BasePanel basePanel = cea.Component as BasePanel;
            if (basePanel != null)
            {
                //this.m_DockPanel.RemoveTabPage(basePanel);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (this.m_DockPanel.Created)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONDOWN://0x204
                        if (this.m_DockPanel.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseDown(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONUP://0x202
                    case (int)GISShare.Win32.Msgs.WM_RBUTTONUP://0x205
                        if (this.m_DockPanel.Handle != m.HWnd) break;
                        if (this.SelectCompnentMouseUp(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam))) return;
                        break;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }
        private WFNew.BaseItem m_Item1 = null;
        private WFNew.BaseItem m_Item2 = null;
        private bool SelectCompnentMouseDown(Point point)
        {
            foreach (WFNew.BaseItem one in ((WFNew.ICollectionItem)this.m_DockPanel).BaseItems)
            {
                if(this.SelectCompnentMouseDownTC(one as WFNew.TabButtonContainerItem, point)) return true;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseDownTC(WFNew.TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null || !ribbonTabButtonContainerItem.Visible) return false;
            //
            foreach (WFNew.BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                WFNew.TabButtonItem tabButtonItem = one as WFNew.TabButtonItem;
                if (tabButtonItem == null) continue;
                if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item1 = tabButtonItem;
                        BasePanel basePanel = tabButtonItem.pTabPageItem as BasePanel;
                        if (basePanel != null)
                        {
                            pSelectionService.SetSelectedComponents(new Component[] { basePanel as Component }, SelectionTypes.Primary);
                            this.m_DockPanel.Refresh();
                        }
                        this.m_DockPanel.BasePanelSelectedIndex = ribbonTabButtonContainerItem.BaseItems.IndexOf(tabButtonItem);
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
            foreach (WFNew.BaseItem one in ((WFNew.ICollectionItem)this.m_DockPanel).BaseItems)
            {
                if(this.SelectCompnentMouseUpTC(one as WFNew.TabButtonContainerItem, point)) return true;
            }
            //
            return false;
        }
        private bool SelectCompnentMouseUpTC(WFNew.TabButtonContainerItem ribbonTabButtonContainerItem, Point point)
        {
            if (ribbonTabButtonContainerItem == null || !ribbonTabButtonContainerItem.Visible) return false;
            //
            foreach (WFNew.BaseItem one in ribbonTabButtonContainerItem.BaseItems)
            {
                WFNew.TabButtonItem tabButtonItem = one as WFNew.TabButtonItem;
                if (tabButtonItem == null) continue;
                if (tabButtonItem.DesignMouseClickRectangleContainsEx(point))
                {
                    ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    if (pSelectionService != null)
                    {
                        this.m_Item2 = tabButtonItem;
                        if (ribbonTabButtonContainerItem.BaseItems.ExchangeItem(this.m_Item1, this.m_Item2))
                        {
                            WFNew.TabButtonItem tabButton = this.m_Item1 as WFNew.TabButtonItem;
                            if (tabButton != null)
                            {
                                BasePanel basePanel = tabButton.pTabPageItem as BasePanel;
                                if (basePanel != null)
                                {
                                    pSelectionService.SetSelectedComponents(new Component[] { basePanel as Component }, SelectionTypes.Primary);
                                    this.m_DockPanel.Refresh();
                                }
                            }
                            return true;
                        }
                        else
                        {
                            BasePanel basePanel = tabButtonItem.pTabPageItem as BasePanel;
                            if (basePanel != null)
                            {
                                pSelectionService.SetSelectedComponents(new Component[] { basePanel as Component }, SelectionTypes.Primary);
                                this.m_DockPanel.Refresh();
                            }
                            return true;
                        }
                    }
                }
            }
            //
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this.m_DockPanel != null &&
                        !this.m_DockPanel.IsDisposed &&
                        this.m_DockPanel.DockPanelManager != null &&
                        this.m_DockPanel.DockPanelManager.DockPanels.Contains(this.m_DockPanel))
                    {
                        this.m_DockPanel.DockPanelManager.DockPanels.Remove(this.m_DockPanel);
                    }
                }
                catch { }
            }
            base.Dispose(disposing);
        }

        private void AddTopDockPanel(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                DockPanelContainerTopBottom dockPanelContainer = host.CreateComponent(typeof(DockPanelContainerTopBottom)) as DockPanelContainerTopBottom;
                basePanel.Dock = DockStyle.Fill;
                basePanel.Text = basePanel.Name;
                dockPanel.Dock = DockStyle.Fill;
                dockPanel.BasePanels.Add(basePanel);
                dockPanelContainer.Text = dockPanelContainer.Name;
                dockPanelContainer.Orientation = Orientation.Horizontal;
                dockPanelContainer.Panel1.Controls.Add(dockPanel);
                this.m_DockPanel.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanel.Parent.Controls.Remove(this.m_DockPanel);
                dockPanelContainer.Panel2.Controls.Add(this.m_DockPanel);
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanel.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanel.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
            }
        }

        private void AddBottomDockPanel(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                DockPanelContainerTopBottom dockPanelContainer = host.CreateComponent(typeof(DockPanelContainerTopBottom)) as DockPanelContainerTopBottom;
                basePanel.Dock = DockStyle.Fill;
                basePanel.Text = basePanel.Name;
                dockPanel.Dock = DockStyle.Fill;
                dockPanel.BasePanels.Add(basePanel);
                dockPanelContainer.Text = dockPanelContainer.Name;
                dockPanelContainer.Orientation = Orientation.Vertical;
                dockPanelContainer.Panel2.Controls.Add(dockPanel);
                this.m_DockPanel.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanel.Parent.Controls.Remove(this.m_DockPanel);
                dockPanelContainer.Panel1.Controls.Add(this.m_DockPanel);
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanel.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanel.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
            }
        }

        private void AddLeftDockPanel(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                DockPanelContainerLeftRight dockPanelContainer = host.CreateComponent(typeof(DockPanelContainerLeftRight)) as DockPanelContainerLeftRight;
                basePanel.Dock = DockStyle.Fill;
                basePanel.Text = basePanel.Name;
                dockPanel.Dock = DockStyle.Fill;
                dockPanel.BasePanels.Add(basePanel);
                dockPanelContainer.Text = dockPanelContainer.Name;
                dockPanelContainer.Orientation = Orientation.Vertical;
                dockPanelContainer.Panel1.Controls.Add(dockPanel);
                this.m_DockPanel.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanel.Parent.Controls.Remove(this.m_DockPanel);
                dockPanelContainer.Panel2.Controls.Add(this.m_DockPanel);
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanel.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanel.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
            }
        }

        private void AddRightDockPanel(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                DockPanelContainerLeftRight dockPanelContainer = host.CreateComponent(typeof(DockPanelContainerLeftRight)) as DockPanelContainerLeftRight;
                basePanel.Dock = DockStyle.Fill;
                basePanel.Text = basePanel.Name;
                dockPanel.Dock = DockStyle.Fill;
                dockPanel.BasePanels.Add(basePanel);
                dockPanelContainer.Text = dockPanelContainer.Name;
                dockPanelContainer.Orientation = Orientation.Vertical;
                dockPanelContainer.Panel2.Controls.Add(dockPanel);
                this.m_DockPanel.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanel.Parent.Controls.Remove(this.m_DockPanel);
                dockPanelContainer.Panel1.Controls.Add(this.m_DockPanel);
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanel.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanel.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
            }
        }

        private void AddBasePanel(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                basePanel.Text = basePanel.Name;
                this.m_DockPanel.BasePanels.Add(basePanel);
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
            }
        }

        private void RemoveBasePanel(object sender, EventArgs ea)
        {
            if (this.m_DockPanel.BasePanels.Count <= 1) 
            { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("一个停靠面板（DockPanel）中至少要有一个基础面板（BasePanel）项，否则可能会出现异常！"); return; }
            //
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BasePanel basePanel = this.m_DockPanel.SelectedBasePanel;//key
                if (basePanel == null) return;
                this.m_DockPanel.BasePanels.Remove(basePanel);
                host.DestroyComponent(basePanel);
            }
        }

        private void BackBasePanel(object sender, EventArgs ea)
        {
            int index = this.m_DockPanel.BasePanelSelectedIndex - 1;
            if (index < 0 || index >= this.m_DockPanel.BasePanels.Count) return;
            this.m_DockPanel.BasePanelSelectedIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["BasePanelSelectedIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["BasePanelSelectedIndex"], this.m_DockPanel.BasePanelSelectedIndex, index);
        }

        private void NextBasePanel(object sender, EventArgs ea)
        {
            int index = this.m_DockPanel.BasePanelSelectedIndex + 1;
            if (index < 0 || index >= this.m_DockPanel.BasePanels.Count) return;
            this.m_DockPanel.BasePanelSelectedIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["BasePanelSelectedIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["BasePanelSelectedIndex"], this.m_DockPanel.BasePanelSelectedIndex, index);
        }

        private void CheckAndRepair(object sender, EventArgs ea)
        {
            this.m_DockPanel.BasePanels.CheckAndRepair();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}

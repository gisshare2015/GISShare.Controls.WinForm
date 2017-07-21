using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel.Design
{
    public class DockPanelContainerDesigner : ParentControlDesigner
    {
        private DockPanelContainer m_DockPanelContainer = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("在顶部添加面板", new EventHandler(AddTopDockPanel)));
                    verbs.Add(new DesignerVerb("在左边添加面板", new EventHandler(AddLeftDockPanel)));
                    verbs.Add(new DesignerVerb("在右边添加面板", new EventHandler(AddRightDockPanel)));
                    verbs.Add(new DesignerVerb("在底部添加面板", new EventHandler(AddBottomDockPanel)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_DockPanelContainer = base.Component as DockPanelContainer;
            if (this.m_DockPanelContainer == null)
            {
                this.DisplayError(new ArgumentException("DockPanelContainer == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this.m_DockPanelContainer != null &&
                        !this.m_DockPanelContainer.IsDisposed &&
                        this.m_DockPanelContainer.DockPanelManager != null &&
                        this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Contains(this.m_DockPanelContainer))
                    {
                        this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Remove(this.m_DockPanelContainer);
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
                this.m_DockPanelContainer.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanelContainer.Parent.Controls.Remove(this.m_DockPanelContainer);
                dockPanelContainer.Panel2.Controls.Add(this.m_DockPanelContainer);
                this.m_DockPanelContainer.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
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
                this.m_DockPanelContainer.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanelContainer.Parent.Controls.Remove(this.m_DockPanelContainer);
                dockPanelContainer.Panel1.Controls.Add(this.m_DockPanelContainer);
                this.m_DockPanelContainer.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
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
                this.m_DockPanelContainer.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanelContainer.Parent.Controls.Remove(this.m_DockPanelContainer);
                dockPanelContainer.Panel2.Controls.Add(this.m_DockPanelContainer);
                this.m_DockPanelContainer.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
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
                this.m_DockPanelContainer.Parent.Controls.Add(dockPanelContainer);
                this.m_DockPanelContainer.Parent.Controls.Remove(this.m_DockPanelContainer);
                dockPanelContainer.Panel1.Controls.Add(this.m_DockPanelContainer);
                this.m_DockPanelContainer.DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanels.Add(dockPanel);
                this.m_DockPanelContainer.DockPanelManager.DockPanelContainers.Add(dockPanelContainer);
            }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}

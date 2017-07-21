using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel.Design
{
    public class DockPanelManagerDesigner : System.ComponentModel.Design.ComponentDesigner
    {
        private DockPanelManager m_DockPanelManager = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    if (this.m_DockPanelManager.DocumentArea == null)
                    {
                        verbs.Add(new DesignerVerb("创建文档区", new EventHandler(CreateDocumentArea)));
                        verbs.Add(new DesignerVerb("创建文档停靠区", new EventHandler(CreateDocumentDockArea)));
                    }
                    else if (this.m_DockPanelManager.DocumentArea is DocumentDockArea)
                    {
                        verbs.Add(new DesignerVerb("创建文档区面板", new EventHandler(CreateDocumentDockPanel))); 
                    }
                    verbs.Add(new DesignerVerb("创建顶部停靠面板", new EventHandler(CreateTopDockPanel)));
                    verbs.Add(new DesignerVerb("创建左边停靠面板", new EventHandler(CreateLeftDockPanel)));
                    verbs.Add(new DesignerVerb("创建右边停靠面板", new EventHandler(CreateRightDockPanel)));
                    verbs.Add(new DesignerVerb("创建底部停靠面板", new EventHandler(CreateBottomDockPanel)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            //if (service != null)
            //{
            //    service.ComponentRemoved += new ComponentEventHandler(ComponentRemoved);
            //}
            //
            this.m_DockPanelManager = base.Component as DockPanelManager;
            if (this.m_DockPanelManager == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("DockPanelManager == null");
                return;
            }
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host != null)
            {
                System.Windows.Forms.Form form = host.RootComponent as System.Windows.Forms.Form;
                this.m_DockPanelManager.ParentForm = form;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (this.m_DockPanelManager != null && host != null)
                    {
                        this.m_DockPanelManager.BasePanels.Clear();
                        this.m_DockPanelManager.DockPanels.Clear();
                        this.m_DockPanelManager.DockPanelContainers.Clear();
                        //
                        if (this.m_DockPanelManager.DocumentArea != null) host.DestroyComponent(this.m_DockPanelManager.DocumentArea);
                        for (int i = this.m_DockPanelManager.DockPanelDockAreas.Count - 1; i >= 0; i--)
                        {
                            host.DestroyComponent(this.m_DockPanelManager.DockPanelDockAreas[i]);
                        }
                    }
                }
                catch { }
            }
            base.Dispose(disposing);
        }

        //private void ComponentRemoved(object sender, ComponentEventArgs cea)
        //{
        //    //Doct ctr = cea.Component as Control;
        //    //if (ctr != null)
        //    //{
        //    //    IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
        //    //    if (host != null)
        //    //    {
        //    //        System.Windows.Forms.Form form = host.RootComponent as System.Windows.Forms.Form;
        //    //        this.m_DockPanelManager.ParentForm = form;
        //    //    }
        //    //    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(cea.Component.ToString() + " | " + ctr.Name);
        //    //}
        //}

        private void CreateDocumentArea(object sender, EventArgs e)
        {
            if (this.m_DockPanelManager.DocumentArea != null) return;
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host == null) return;
            DocumentArea documentAreaPanel = host.CreateComponent(typeof(DocumentArea)) as DocumentArea;
            if (documentAreaPanel == null) return;
            documentAreaPanel.Dock = DockStyle.Fill;
            documentAreaPanel.Text = documentAreaPanel.Name;
            this.m_DockPanelManager.DocumentArea = documentAreaPanel;
            this.m_DockPanelManager.ParentForm.Controls.Add(documentAreaPanel);
            if (this.m_DockPanelManager.ParentForm.Controls.Count > 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(documentAreaPanel, 0); }
        }

        private void CreateDocumentDockArea(object sender, EventArgs e)
        {
            if (this.m_DockPanelManager.DocumentArea != null) return;
            //
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host == null) return;
            DocumentDockArea documentAreaPanel = host.CreateComponent(typeof(DocumentDockArea)) as DocumentDockArea;
            if (documentAreaPanel == null) return;
            documentAreaPanel.Dock = DockStyle.Fill;
            documentAreaPanel.Text = documentAreaPanel.Name;
            this.m_DockPanelManager.DocumentArea = documentAreaPanel;
            this.m_DockPanelManager.ParentForm.Controls.Add(documentAreaPanel);
            if (this.m_DockPanelManager.ParentForm.Controls.Count > 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(documentAreaPanel, 0); }
        }

        private void CreateDocumentDockPanel(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            //
            if (host == null) return;
            //
            if (this.m_DockPanelManager.DocumentArea == null)
            {
                DocumentDockArea documentAreaPanel = host.CreateComponent(typeof(DocumentDockArea)) as DocumentDockArea;
                if (documentAreaPanel == null) return;
                documentAreaPanel.Dock = DockStyle.Fill;
                documentAreaPanel.Text = documentAreaPanel.Name;
                this.m_DockPanelManager.DocumentArea = documentAreaPanel;
                this.m_DockPanelManager.ParentForm.Controls.Add(documentAreaPanel);
                if (this.m_DockPanelManager.ParentForm.Controls.Count > 1) { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(documentAreaPanel, 0); }
                //
                BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                if (basePanel == null || dockPanel == null) return;
                basePanel.Dock = DockStyle.Fill;
                basePanel.Text = basePanel.Name;
                dockPanel.BasePanels.Add(basePanel);
                dockPanel.Dock = DockStyle.Fill;
                this.m_DockPanelManager.DocumentArea.Controls.Add(dockPanel);
                this.m_DockPanelManager.BasePanels.Add(basePanel);
                this.m_DockPanelManager.DockPanels.Add(dockPanel);
            }
            else if(this.m_DockPanelManager.DocumentArea is DocumentDockArea)
            {
                DockPanel dockPanel = ((DocumentDockArea)this.m_DockPanelManager.DocumentArea).GetDockPanel();
                if (dockPanel != null)
                {
                    BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                    if (basePanel == null) return;
                    basePanel.Dock = DockStyle.Fill;
                    basePanel.Text = basePanel.Name;
                    dockPanel.BasePanels.Add(basePanel);
                    this.m_DockPanelManager.BasePanels.Add(basePanel);
                }
                else
                {
                    BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
                    dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
                    if (basePanel == null || dockPanel == null) return;
                    basePanel.Dock = DockStyle.Fill;
                    basePanel.Text = basePanel.Name;
                    dockPanel.BasePanels.Add(basePanel);
                    dockPanel.Dock = DockStyle.Fill;
                    this.m_DockPanelManager.DocumentArea.Controls.Add(dockPanel);
                    this.m_DockPanelManager.BasePanels.Add(basePanel);
                    this.m_DockPanelManager.DockPanels.Add(dockPanel);
                }
            }
        }

        private void CreateTopDockPanel(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
            DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
            DockPanelDockAreaTop dockPanelDockAreaTop = host.CreateComponent(typeof(DockPanelDockAreaTop)) as DockPanelDockAreaTop;
            if (basePanel == null || dockPanel == null || dockPanelDockAreaTop == null) return;
            basePanel.Dock = DockStyle.Fill;
            basePanel.Text = basePanel.Name;
            dockPanel.BasePanels.Add(basePanel);
            dockPanel.Dock = DockStyle.Fill;
            dockPanelDockAreaTop.Controls.Add(dockPanel);
            dockPanelDockAreaTop.Width = 160;
            dockPanelDockAreaTop.Dock = DockStyle.Top;
            dockPanelDockAreaTop.Text = dockPanelDockAreaTop.Name;
            this.m_DockPanelManager.BasePanels.Add(basePanel);
            this.m_DockPanelManager.DockPanels.Add(dockPanel);
            this.m_DockPanelManager.DockPanelDockAreas.Add(dockPanelDockAreaTop);
            this.m_DockPanelManager.ParentForm.Controls.Add(dockPanelDockAreaTop);
            int iIndex = this.m_DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.m_DockPanelManager.ParentForm.Controls.Count - 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(dockPanelDockAreaTop, iIndex); }   
        }

        private void CreateLeftDockPanel(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
            DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
            DockPanelDockAreaLeft dockPanelDockAreaLeft = host.CreateComponent(typeof(DockPanelDockAreaLeft)) as DockPanelDockAreaLeft;
            if (basePanel == null || dockPanel == null || dockPanelDockAreaLeft == null) return;
            basePanel.Dock = DockStyle.Fill;
            basePanel.Text = basePanel.Name;
            dockPanel.BasePanels.Add(basePanel);
            dockPanel.Dock = DockStyle.Fill;
            dockPanelDockAreaLeft.Controls.Add(dockPanel);
            dockPanelDockAreaLeft.Height = 160;
            dockPanelDockAreaLeft.Dock = DockStyle.Left;
            dockPanelDockAreaLeft.Text = dockPanelDockAreaLeft.Name;
            this.m_DockPanelManager.BasePanels.Add(basePanel);
            this.m_DockPanelManager.DockPanels.Add(dockPanel);
            this.m_DockPanelManager.DockPanelDockAreas.Add(dockPanelDockAreaLeft);
            this.m_DockPanelManager.ParentForm.Controls.Add(dockPanelDockAreaLeft);
            int iIndex = this.m_DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.m_DockPanelManager.ParentForm.Controls.Count - 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(dockPanelDockAreaLeft, iIndex); }
        }

        private void CreateRightDockPanel(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
            DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
            DockPanelDockAreaRight dockPanelDockAreaRight = host.CreateComponent(typeof(DockPanelDockAreaRight)) as DockPanelDockAreaRight;
            if (basePanel == null || dockPanel == null || dockPanelDockAreaRight == null) return;
            basePanel.Dock = DockStyle.Fill;
            basePanel.Text = basePanel.Name;
            dockPanel.BasePanels.Add(basePanel);
            dockPanel.Dock = DockStyle.Fill;
            dockPanelDockAreaRight.Controls.Add(dockPanel);
            dockPanelDockAreaRight.Height = 160;
            dockPanelDockAreaRight.Dock = DockStyle.Right;
            dockPanelDockAreaRight.Text = dockPanelDockAreaRight.Name;
            this.m_DockPanelManager.BasePanels.Add(basePanel);
            this.m_DockPanelManager.DockPanels.Add(dockPanel);
            this.m_DockPanelManager.DockPanelDockAreas.Add(dockPanelDockAreaRight);
            this.m_DockPanelManager.ParentForm.Controls.Add(dockPanelDockAreaRight);
            int iIndex = this.m_DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.m_DockPanelManager.ParentForm.Controls.Count - 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(dockPanelDockAreaRight, iIndex); } 
        }

        private void CreateBottomDockPanel(object sender, EventArgs e)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (host == null) return;

            BasePanel basePanel = host.CreateComponent(typeof(BasePanel)) as BasePanel;
            DockPanel dockPanel = host.CreateComponent(typeof(DockPanel)) as DockPanel;
            DockPanelDockAreaBottom dockPanelDockAreaBottom = host.CreateComponent(typeof(DockPanelDockAreaBottom)) as DockPanelDockAreaBottom;
            if (basePanel == null || dockPanel == null || dockPanelDockAreaBottom == null) return;
            basePanel.Dock = DockStyle.Fill;
            basePanel.Text = basePanel.Name;
            dockPanel.BasePanels.Add(basePanel);
            dockPanel.Dock = DockStyle.Fill;
            dockPanelDockAreaBottom.Width = 160;
            dockPanelDockAreaBottom.Controls.Add(dockPanel);
            dockPanelDockAreaBottom.Dock = DockStyle.Bottom;
            dockPanelDockAreaBottom.Text = dockPanelDockAreaBottom.Name;
            this.m_DockPanelManager.BasePanels.Add(basePanel);
            this.m_DockPanelManager.DockPanels.Add(dockPanel);
            this.m_DockPanelManager.DockPanelDockAreas.Add(dockPanelDockAreaBottom);
            this.m_DockPanelManager.ParentForm.Controls.Add(dockPanelDockAreaBottom);
            int iIndex = this.m_DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.m_DockPanelManager.ParentForm.Controls.Count - 1)
            { this.m_DockPanelManager.ParentForm.Controls.SetChildIndex(dockPanelDockAreaBottom, iIndex); }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

    }
}

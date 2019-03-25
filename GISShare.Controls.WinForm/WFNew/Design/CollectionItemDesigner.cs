using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class CollectionItemDesigner : ComponentDesigner
    {
        private ICollectionItem m_pCollectionItem = null;
        private IRibbonApplicationObjectDesignHelper m_pRibbonApplicationObjectDesignHelper = null;
        private IObjectDesignHelper m_pObjectDesignHelper = null;
        private ICollectionObjectDesignHelper m_pCollectionObjectDesignHelper = null;
        private IPopupObjectDesignHelper m_pPopupObjectDesignHelper = null;
        //
        private IRibbonControl m_pRibbonControl = null;
        private IRibbonPageItem m_pRibbonPageItem = null;
        private IButtonGroupItem m_pButtonGroupItem = null;
        private IGalleryItem m_pGalleryItem = null;
        private RibbonGalleryRowItem m_RibbonGalleryRowItem = null;
        
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_pCollectionItem = base.Component as ICollectionItem;
            this.m_pRibbonApplicationObjectDesignHelper = base.Component as IRibbonApplicationObjectDesignHelper;
            this.m_pObjectDesignHelper = base.Component as IObjectDesignHelper;
            this.m_pCollectionObjectDesignHelper = base.Component as ICollectionObjectDesignHelper;
            this.m_pPopupObjectDesignHelper = base.Component as IPopupObjectDesignHelper;
            //
            this.m_pRibbonControl = base.Component as IRibbonControl;
            this.m_pRibbonPageItem = base.Component as IRibbonPageItem;
            this.m_pButtonGroupItem = base.Component as IButtonGroupItem;
            this.m_pGalleryItem = base.Component as IGalleryItem;
            this.m_RibbonGalleryRowItem = base.Component as RibbonGalleryRowItem;
            //
            if (this.m_pRibbonControl != null)
            {
                IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    if (host.RootComponent is System.Windows.Forms.Form)
                    {
                        System.Windows.Forms.Form form = (System.Windows.Forms.Form)host.RootComponent;
                        form.MainMenuStrip = this.m_pRibbonControl.MenuStrip;
                        this.m_pRibbonControl.ParentForm = form;
                        if (form is WFNew.RibbonForm)
                        {
                            ((WFNew.RibbonForm)form).RibbonControl = this.m_pRibbonControl;
                        }
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_pRibbonControl != null)
                {
                    foreach (BaseItem one in this.m_pRibbonControl.ToolbarItems)
                    {
                        one.Dispose();
                    }
                    ((ICollectionItem)this.m_pRibbonControl).BaseItems.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonControl.PageContents)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonControl.PageContents.Clear();
                    //
                    foreach (RibbonPageItem one in this.m_pRibbonControl.TabPages)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonControl.TabPages.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonControl.ApplicationPopup.MenuItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonControl.ApplicationPopup.MenuItems.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonControl.ApplicationPopup.RecordItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonControl.ApplicationPopup.RecordItems.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonControl.ApplicationPopup.OperationItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonControl.ApplicationPopup.OperationItems.Clear();
                }
                else if (this.m_pRibbonApplicationObjectDesignHelper != null)
                {
                    foreach (BaseItem one in this.m_pRibbonApplicationObjectDesignHelper.MenuItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonApplicationObjectDesignHelper.MenuItems.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonApplicationObjectDesignHelper.RecordItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonApplicationObjectDesignHelper.RecordItems.Clear();
                    //
                    foreach (BaseItem one in this.m_pRibbonApplicationObjectDesignHelper.OperationItems)
                    {
                        one.Dispose();
                    }
                    this.m_pRibbonApplicationObjectDesignHelper.OperationItems.Clear();
                }
                else if (this.m_pCollectionItem != null)
                {
                    foreach (BaseItem one in this.m_pCollectionItem.BaseItems)
                    {
                        one.Dispose();
                    }
                    this.m_pCollectionItem.BaseItems.Clear();
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
                if (this.m_pRibbonControl != null)
                {
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("检测和修复RibbonControl", new EventHandler(CheckAndRepair)));
                }
                else if (this.m_pRibbonPageItem != null)
                {
                    verbs.Add(new DesignerVerb("添加 RibbonBarItem", new EventHandler(AddRibbonBarItem)));
                } 
                else if (this.m_pButtonGroupItem != null)
                {
                    verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                    verbs.Add(new DesignerVerb("添加 DropDownButtonItem", new EventHandler(AddDropDownButtonItem)));
                    verbs.Add(new DesignerVerb("添加 SplitButtonItem", new EventHandler(AddSplitButtonItem)));
                    verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                    verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                } 
                else if(this.m_pGalleryItem != null)
                {
                    verbs.Add(new DesignerVerb("添加 RibbonGalleryRowItem", new EventHandler(AddRibbonGalleryRowItem)));
                }
                else if (this.m_RibbonGalleryRowItem != null)
                {
                    verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                    verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                    verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                }
                else if (this.m_pCollectionObjectDesignHelper != null)
                {
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                } 
                //
                if (this.m_pPopupObjectDesignHelper != null)
                {
                    verbs.Add(new DesignerVerb("展现 Popup", new EventHandler(ShowPopup)));
                    verbs.Add(new DesignerVerb("关闭 Popup", new EventHandler(ClosePopup)));
                }
                //
                verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                //
                return verbs;
            }
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            if (this.m_pObjectDesignHelper != null)
            {
                BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_pObjectDesignHelper);
                baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
                baseItemCollectionDesignerFormEx.TopMost = true;
                baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
                baseItemCollectionDesignerFormEx.Show();
            }
        }

        private void ShowPopup(object sender, EventArgs ea)
        {
            if (this.m_pPopupObjectDesignHelper != null)
            {
                this.m_pPopupObjectDesignHelper.ShowPopup();
            }
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            if (this.m_pPopupObjectDesignHelper != null)
            {
                this.m_pPopupObjectDesignHelper.ClosePopup();
            }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void CheckAndRepair(object sender, EventArgs ea)
        {
            if (this.m_pRibbonControl != null)
            {
                this.m_pRibbonControl.TabPages.CheckAndRepair();
            }
        }

        private void AddRibbonBarItem(object sender, EventArgs ea)
        {
            if (this.m_pRibbonPageItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    RibbonBarItem ribbonBarItem = host.CreateComponent(typeof(RibbonBarItem)) as RibbonBarItem;
                    ribbonBarItem.Name = ribbonBarItem.Site.Name;
                    ribbonBarItem.Padding = new Padding(3, 3, 3, 2);
                    ribbonBarItem.ShowNomalState = false;
                    ribbonBarItem.Size = new Size(60, 23);
                    ribbonBarItem.Text = ribbonBarItem.Name;
                    this.m_pRibbonPageItem.BaseItems.Add(ribbonBarItem);
                }
            }
        }

        private void AddBaseButtonItem(object sender, EventArgs ea)
        {
            if (this.m_pCollectionItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.ShowNomalState = false;
                    baseItem.Size = new Size(23, 23);
                    this.m_pCollectionItem.BaseItems.Add(baseItem);
                }
            }
        }

        private void AddCheckButtonItem(object sender, EventArgs ea)
        {
            if (this.m_pCollectionItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.ShowNomalState = false;
                    baseItem.Size = new Size(23, 21);
                    this.m_pCollectionItem.BaseItems.Add(baseItem);
                }
            }
        }

        private void AddDropDownButtonItem(object sender, EventArgs ea)
        {
            if (this.m_pCollectionItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    DropDownButtonItem baseItem = host.CreateComponent(typeof(DropDownButtonItem)) as DropDownButtonItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.eArrowDock = ArrowDock.eRight;
                    baseItem.ShowNomalState = false;
                    baseItem.Size = new Size(23, 23);
                    this.m_pCollectionItem.BaseItems.Add(baseItem);
                }
            }
        }

        private void AddSplitButtonItem(object sender, EventArgs ea)
        {
            if (this.m_pCollectionItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    SplitButtonItem baseItem = host.CreateComponent(typeof(SplitButtonItem)) as SplitButtonItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.eArrowDock = ArrowDock.eRight;
                    baseItem.ShowNomalState = false;
                    baseItem.Size = new Size(23, 23);
                    this.m_pCollectionItem.BaseItems.Add(baseItem);
                }
            }
        }

        private void AddButtonItem(object sender, EventArgs ea)
        {
            if (this.m_pCollectionItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.ShowNomalState = false;
                    baseItem.Size = new Size(23, 23);
                    this.m_pCollectionItem.BaseItems.Add(baseItem);
                }
            }
        }

        private void AddRibbonGalleryRowItem(object sender, EventArgs ea)
        {
            if (this.m_pButtonGroupItem != null)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    RibbonGalleryRowItem baseItem = host.CreateComponent(typeof(RibbonGalleryRowItem)) as RibbonGalleryRowItem;
                    baseItem.Name = baseItem.Site.Name;
                    baseItem.Text = baseItem.Name;
                    baseItem.LineDistance = 1;
                    baseItem.Size = new Size(100, 21);
                    this.m_pGalleryItem.BaseItems.Add(baseItem);
                }
            }
        }

    }
}

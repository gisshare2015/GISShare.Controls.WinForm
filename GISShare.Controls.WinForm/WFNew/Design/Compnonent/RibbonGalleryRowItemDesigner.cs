using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonGalleryRowItemDesigner : ComponentDesigner
    {
        private RibbonGalleryRowItem m_RibbonGalleryRowItem = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonGalleryRowItem = base.Component as RibbonGalleryRowItem;
            if (this.m_RibbonGalleryRowItem == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("RibbonGalleryRowItem == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonGalleryRowItem.BaseItems)
                {
                    one.Dispose();
                }
                this.m_RibbonGalleryRowItem.BaseItems.Clear();
            }
            base.Dispose(disposing);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                //
                return verbs;
            }
        }        

        private void AddBaseButtonItem(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
                baseItem.Name = baseItem.Site.Name;
                baseItem.Text = baseItem.Name;
                baseItem.ShowNomalState = false;
                baseItem.Size = new Size(21, 21);
                this.m_RibbonGalleryRowItem.BaseItems.Add(baseItem);
            }
        }

        private void AddButtonItem(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
                baseItem.Name = baseItem.Site.Name;
                baseItem.Text = baseItem.Name;
                baseItem.ShowNomalState = false;
                baseItem.Size = new Size(21, 21);
                this.m_RibbonGalleryRowItem.BaseItems.Add(baseItem);
            }
        }

        private void AddCheckButtonItem(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
                baseItem.Name = baseItem.Site.Name;
                baseItem.Text = baseItem.Name;
                baseItem.ShowNomalState = false;
                baseItem.Size = new Size(21, 21);
                this.m_RibbonGalleryRowItem.BaseItems.Add(baseItem);
            }
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            //BaseItemCollectionEditerForm baseItemCollectionDesignerForm = new BaseItemCollectionEditerForm(this.m_RibbonControl);
            //baseItemCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            //baseItemCollectionDesignerForm.TopMost = true;
            //baseItemCollectionDesignerForm.Location = new Point(360, 150);
            //baseItemCollectionDesignerForm.Show();
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_RibbonGalleryRowItem);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

    }
}



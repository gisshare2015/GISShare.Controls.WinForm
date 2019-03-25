using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonGalleryItemDesigner : ComponentDesigner
    {
        private RibbonGalleryItem m_RibbonGalleryItem = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonGalleryItem = base.Component as RibbonGalleryItem;
            if (this.m_RibbonGalleryItem == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("RibbonGalleryItem == null");
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
                foreach (BaseItem one in this.m_RibbonGalleryItem.BaseItems)
                {
                    one.Dispose();
                }
                this.m_RibbonGalleryItem.BaseItems.Clear();
            }
            base.Dispose(disposing);
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
                this.m_RibbonGalleryItem.BaseItems.Add(baseItem);
            }
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_RibbonGalleryItem);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}

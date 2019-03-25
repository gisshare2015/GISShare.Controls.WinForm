using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonPageItemDesigner : ComponentDesigner
    {
        private RibbonPageItem m_RibbonPageItem = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonPageItem = base.Component as RibbonPageItem;
            if (this.m_RibbonPageItem == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("RibbonPageItem == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonPageItem.BaseItems)
                {
                    one.Dispose();
                }
                this.m_RibbonPageItem.BaseItems.Clear();
            }
            base.Dispose(disposing);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("添加 RibbonBarItem", new EventHandler(AddRibbonBarItem)));
                verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                //
                return verbs;
            }
        }        

        private void AddRibbonBarItem(object sender, EventArgs ea)
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
                this.m_RibbonPageItem.BaseItems.Add(ribbonBarItem);
            }
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_RibbonPageItem);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}


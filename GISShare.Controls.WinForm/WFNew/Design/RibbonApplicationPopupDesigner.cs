using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonApplicationPopupDesigner : ComponentDesigner
    {
        private RibbonApplicationPopup m_RibbonApplicationPopup = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonApplicationPopup = base.Component as RibbonApplicationPopup;
            if (this.m_RibbonApplicationPopup == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("RibbonApplicationPopup == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonApplicationPopup.MenuItems)
                {
                    one.Dispose();
                }
                this.m_RibbonApplicationPopup.MenuItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonApplicationPopup.RecordItems)
                {
                    one.Dispose();
                }
                this.m_RibbonApplicationPopup.RecordItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonApplicationPopup.OperationItems)
                {
                    one.Dispose();
                }
                this.m_RibbonApplicationPopup.OperationItems.Clear();
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
                //
                verbs.Add(new DesignerVerb("展现 Popup", new EventHandler(ShowPopup)));
                verbs.Add(new DesignerVerb("关闭 Popup", new EventHandler(ClosePopup)));
                //
                return verbs;
            }
        }

        private void ShowPopup(object sender, EventArgs ea)
        {
            this.m_RibbonApplicationPopup.Show(3, 3);
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            this.m_RibbonApplicationPopup.Close();
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_RibbonApplicationPopup);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}

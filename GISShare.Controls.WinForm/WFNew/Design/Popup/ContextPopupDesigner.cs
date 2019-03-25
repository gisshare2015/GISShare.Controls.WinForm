using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ContextPopupDesigner : ComponentDesigner
    {
        private ContextPopup m_ContextPopup = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ContextPopup = base.Component as ContextPopup;
            if (this.m_ContextPopup == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("ContextPopup == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_ContextPopup.BaseItems)
                {
                    one.Dispose();
                }
                this.m_ContextPopup.BaseItems.Clear();
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
            this.m_ContextPopup.Show(3, 3);
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            this.m_ContextPopup.Close();
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerForm baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerForm(this.m_ContextPopup);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}



using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class DescriptionMenuPopupDesigner : ComponentDesigner
    {
        private DescriptionMenuPopup m_DescriptionMenuPopup = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_DescriptionMenuPopup = base.Component as DescriptionMenuPopup;
            if (this.m_DescriptionMenuPopup == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("DescriptionMenuPopup == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_DescriptionMenuPopup.BaseItems)
                {
                    one.Dispose();
                }
                this.m_DescriptionMenuPopup.BaseItems.Clear();
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
            this.m_DescriptionMenuPopup.Show(3, 3);
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            this.m_DescriptionMenuPopup.Close();
        }

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_DescriptionMenuPopup);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }
    }
}

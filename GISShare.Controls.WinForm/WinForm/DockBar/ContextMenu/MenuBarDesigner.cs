using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.DockBar
{
    public class ContextMenuDesigner : ComponentDesigner
    {
        private ContextMenu m_ContextMenu = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("展现 Popup", new EventHandler(ShowPopup)));
                    verbs.Add(new DesignerVerb("关闭 Popup", new EventHandler(ClosePopup)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ContextMenu = base.Component as ContextMenu;
            if (this.m_ContextMenu == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("ContextMenu == null");
                //this.DisplayError(new ArgumentException("ContextMenu == null"));
                return;
            }
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemDBCollectionDesignerForm baseItemDBCollectionDesignerForm = new BaseItemDBCollectionDesignerForm(this.m_ContextMenu);
            baseItemDBCollectionDesignerForm.GetServiceCallBackEx = new WFNew.GetServiceCallBack(this.GetService);
            baseItemDBCollectionDesignerForm.TopMost = true;
            baseItemDBCollectionDesignerForm.Location = new Point(360, 150);
            baseItemDBCollectionDesignerForm.Show();
        }

        private void ShowPopup(object sender, EventArgs ea)
        {
            this.m_ContextMenu.Show(3, 3);
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            this.m_ContextMenu.Close();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}

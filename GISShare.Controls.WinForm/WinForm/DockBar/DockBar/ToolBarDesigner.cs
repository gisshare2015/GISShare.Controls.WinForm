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
    public class ToolBarDesigner : ControlDesigner
    {
        private ToolBar m_ToolBar = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ToolBar = base.Component as ToolBar;
            if (this.m_ToolBar == null)
            {
                this.DisplayError(new ArgumentException("ToolBar == null"));
                return;
            }
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            BaseItemDBCollectionDesignerForm baseItemDBCollectionDesignerForm = new BaseItemDBCollectionDesignerForm(this.m_ToolBar);
            baseItemDBCollectionDesignerForm.GetServiceCallBackEx = new WFNew.GetServiceCallBack(this.GetService);
            baseItemDBCollectionDesignerForm.TopMost = true;
            baseItemDBCollectionDesignerForm.Location = new Point(360, 150);
            baseItemDBCollectionDesignerForm.Show();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}

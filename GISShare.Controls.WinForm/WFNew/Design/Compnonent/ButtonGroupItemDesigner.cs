using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ButtonGroupItemDesigner : ComponentDesigner
    {
        private ButtonGroupItem m_ButtonGroupItem = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ButtonGroupItem = base.Component as ButtonGroupItem;
            if (this.m_ButtonGroupItem == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("ButtonGroupItem == null");
                return;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                verbs.Add(new DesignerVerb("添加 DropDownButtonItem", new EventHandler(AddDropDownButtonItem)));
                verbs.Add(new DesignerVerb("添加 SplitButtonItem", new EventHandler(AddSplitButtonItem)));
                verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                //
                return verbs;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_ButtonGroupItem.BaseItems)
                {
                    one.Dispose();
                }
                this.m_ButtonGroupItem.BaseItems.Clear();
            }
            base.Dispose(disposing);
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
                baseItem.Size = new Size(23, 23);
                this.m_ButtonGroupItem.BaseItems.Add(baseItem);
            }
        }

        private void AddDropDownButtonItem(object sender, EventArgs ea)
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
                this.m_ButtonGroupItem.BaseItems.Add(baseItem);
            }
        }

        private void AddSplitButtonItem(object sender, EventArgs ea)
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
                this.m_ButtonGroupItem.BaseItems.Add(baseItem);
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
                baseItem.Size = new Size(23, 23);
                this.m_ButtonGroupItem.BaseItems.Add(baseItem);
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
                baseItem.Size = new Size(23, 21);
                this.m_ButtonGroupItem.BaseItems.Add(baseItem);
            }

        }
    }
}

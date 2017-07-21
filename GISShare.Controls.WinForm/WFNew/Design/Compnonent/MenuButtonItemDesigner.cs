using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class MenuButtonItemDesigner : ComponentDesigner
    {
        private MenuButtonItem m_MenuButtonItem = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_MenuButtonItem = base.Component as MenuButtonItem;
            if (this.m_MenuButtonItem == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("MenuButtonItem == null");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_MenuButtonItem.BaseItems)
                {
                    one.Dispose();
                }
                this.m_MenuButtonItem.BaseItems.Clear();
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
                ////
                //verbs.Add(new DesignerVerb("添加 DescriptionButtonItem", new EventHandler(AddDescriptionButtonItem)));
                //verbs.Add(new DesignerVerb("添加 BaseButtonItem", new EventHandler(AddBaseButtonItem)));
                //verbs.Add(new DesignerVerb("添加 DropDownButtonItem", new EventHandler(AddDropDownButtonItem)));
                //verbs.Add(new DesignerVerb("添加 SplitButtonItem", new EventHandler(AddSplitButtonItem)));
                //verbs.Add(new DesignerVerb("添加 ButtonItem", new EventHandler(AddButtonItem)));
                //verbs.Add(new DesignerVerb("添加 CheckButtonItem", new EventHandler(AddCheckButtonItem)));
                //verbs.Add(new DesignerVerb("添加 LabelItem", new EventHandler(AddLabelItem)));
                //verbs.Add(new DesignerVerb("添加 LinkLabelItem", new EventHandler(AddLinkLabelItem)));
                //verbs.Add(new DesignerVerb("添加 CheckBoxItem", new EventHandler(AddCheckBoxItem)));
                //verbs.Add(new DesignerVerb("添加 RadioButtonItem", new EventHandler(AddRadioButtonItem)));
                //verbs.Add(new DesignerVerb("添加 TextBoxItem", new EventHandler(AddTextBoxItem)));
                //verbs.Add(new DesignerVerb("添加 ComboBoxItem", new EventHandler(AddComboBoxItem)));
                //verbs.Add(new DesignerVerb("添加 ComboTreeItem", new EventHandler(AddComboTreeItem)));
                //verbs.Add(new DesignerVerb("添加 SeparatorItem", new EventHandler(AddSeparatorItem)));
                verbs.Add(new DesignerVerb("展现 Popup", new EventHandler(ShowPopup)));
                verbs.Add(new DesignerVerb("关闭 Popup", new EventHandler(ClosePopup)));
                //
                return verbs;
            }
        }

        #region old
        //private void AddDescriptionButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        DescriptionButtonItem baseItem = host.CreateComponent(typeof(DescriptionButtonItem)) as DescriptionButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        baseItem.ShowNomalState = false;
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddLabelItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LabelItem baseItem = host.CreateComponent(typeof(LabelItem)) as LabelItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddLinkLabelItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LinkLabelItem baseItem = host.CreateComponent(typeof(LinkLabelItem)) as LinkLabelItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddBaseButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddDropDownButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        DropDownButtonItem baseItem = host.CreateComponent(typeof(DropDownButtonItem)) as DropDownButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddSplitButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SplitButtonItem baseItem = host.CreateComponent(typeof(SplitButtonItem)) as SplitButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eArrowDock = ArrowDock.eRight;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ButtonItem baseItem = host.CreateComponent(typeof(ButtonItem)) as ButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 23);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }
        //}

        //private void AddCheckButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckButtonItem baseItem = host.CreateComponent(typeof(CheckButtonItem)) as CheckButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.ShowNomalState = false;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddCheckBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        CheckBoxItem baseItem = host.CreateComponent(typeof(CheckBoxItem)) as CheckBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddRadioButtonItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        RadioButtonItem baseItem = host.CreateComponent(typeof(RadioButtonItem)) as RadioButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddTextBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        TextBoxItem baseItem = host.CreateComponent(typeof(TextBoxItem)) as TextBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddComboBoxItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboBoxItem baseItem = host.CreateComponent(typeof(ComboBoxItem)) as ComboBoxItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddComboTreeItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        ComboTreeItem baseItem = host.CreateComponent(typeof(ComboTreeItem)) as ComboTreeItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        //baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }

        //}

        //private void AddSeparatorItem(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_MenuButtonItem.BaseItems.Add(baseItem);
        //    }
        //}
        #endregion

        //

        private void BuildTreeView(object sender, EventArgs ea)
        {
            //BaseItemCollectionEditerForm baseItemCollectionDesignerForm = new BaseItemCollectionEditerForm(this.m_RibbonControl);
            //baseItemCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            //baseItemCollectionDesignerForm.TopMost = true;
            //baseItemCollectionDesignerForm.Location = new Point(360, 150);
            //baseItemCollectionDesignerForm.Show();
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_MenuButtonItem);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

        private void ShowPopup(object sender, EventArgs ea)
        {
            this.m_MenuButtonItem.ShowPopup();
        }

        private void ClosePopup(object sender, EventArgs ea)
        {
            this.m_MenuButtonItem.ClosePopup();
        }

    }
}


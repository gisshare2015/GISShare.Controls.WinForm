using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class RibbonStartButton2007Designer : ControlDesigner
    {
        private RibbonStartButton2007 m_RibbonStartButton2007 = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_RibbonStartButton2007 = base.Component as RibbonStartButton2007;
            if (this.m_RibbonStartButton2007 == null)
            {
                this.DisplayError(new ArgumentException("RibbonStartButton2007 == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (BaseItem one in this.m_RibbonStartButton2007.MenuItems)
                {
                    one.Dispose();
                }
                this.m_RibbonStartButton2007.MenuItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonStartButton2007.RecordItems)
                {
                    one.Dispose();
                }
                this.m_RibbonStartButton2007.RecordItems.Clear();
                //
                foreach (BaseItem one in this.m_RibbonStartButton2007.OperationItems)
                {
                    one.Dispose();
                }
                this.m_RibbonStartButton2007.OperationItems.Clear();
            }
            base.Dispose(disposing);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("��ϵ�������", new EventHandler(BuildTreeView)));
                ////
                //verbs.Add(new DesignerVerb("���� MenuButtonItem �� MenuItems", new EventHandler(AddMenuButtonItemToMenuItems)));
                //verbs.Add(new DesignerVerb("���� SeparatorItem �� MenuItems", new EventHandler(AddSeparatorItemToMenuItems)));
                ////
                //verbs.Add(new DesignerVerb("���� LabelSeparatorItem �� RecordItems", new EventHandler(AddLabelSeparatorItemToRecordItems)));
                //verbs.Add(new DesignerVerb("���� BaseButtonItem �� RecordItems", new EventHandler(AddBaseButtonItemToRecordItems)));
                //verbs.Add(new DesignerVerb("���� SeparatorItem �� RecordItems", new EventHandler(AddSeparatorItemToRecordItems)));
                ////
                //verbs.Add(new DesignerVerb("���� BaseButtonItem �� OperationItems", new EventHandler(AddBaseButtonItemToOperationItems)));
                //verbs.Add(new DesignerVerb("���� SeparatorItem �� OperationItems", new EventHandler(AddSeparatorItemToOperationItems)));
                //
                verbs.Add(new DesignerVerb("չ�� ApplicationPopup", new EventHandler(ShowApplicationPopup)));
                verbs.Add(new DesignerVerb("�ر� ApplicationPopup", new EventHandler(CloseApplicationPopup)));
                //
                return verbs;
            }
        }

        //protected override void OnPaintAdornments(PaintEventArgs pe)
        //{
        //    using (Pen p = new Pen(Color.Black))
        //    {
        //        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        //        ISelectionService host = GetService(typeof(ISelectionService)) as ISelectionService;
        //        if (host != null)
        //        {
        //            foreach (IComponent one in host.GetSelectedComponents())
        //            {
        //                BaseItem item = one as BaseItem;
        //                if (item == null) continue;
        //                pe.Graphics.DrawRectangle(p, item.DesignMouseSelectedRectangle);
        //                break;
        //            }
        //        }
        //    }
        //    //
        //    base.OnPaintAdornments(pe);
        //}

        #region old
        //private void AddMenuButtonItemToMenuItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        MenuButtonItem baseItem = host.CreateComponent(typeof(MenuButtonItem)) as MenuButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonStartButton2007.MenuItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToMenuItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonStartButton2007.MenuItems.Add(baseItem);
        //    }
        //}

        //private void AddLabelSeparatorItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        LabelSeparatorItem baseItem = host.CreateComponent(typeof(LabelSeparatorItem)) as LabelSeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonStartButton2007.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseButtonItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.TextAlign = ContentAlignment.MiddleLeft;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonStartButton2007.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToRecordItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonStartButton2007.RecordItems.Add(baseItem);
        //    }
        //}

        //private void AddBaseButtonItemToOperationItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        BaseButtonItem baseItem = host.CreateComponent(typeof(BaseButtonItem)) as BaseButtonItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.AutoPlanTextRectangle = true;
        //        baseItem.ImageAlign = ContentAlignment.MiddleLeft;
        //        baseItem.TextAlign = ContentAlignment.MiddleLeft;
        //        baseItem.Size = new Size(60, 21);
        //        this.m_RibbonStartButton2007.OperationItems.Add(baseItem);
        //    }
        //}

        //private void AddSeparatorItemToOperationItems(object sender, EventArgs ea)
        //{
        //    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
        //    if (host != null)
        //    {
        //        SeparatorItem baseItem = host.CreateComponent(typeof(SeparatorItem)) as SeparatorItem;
        //        baseItem.Name = baseItem.Site.Name;
        //        baseItem.Text = baseItem.Name;
        //        baseItem.eOrientation = Orientation.Horizontal;
        //        baseItem.Size = new Size(23, 2);
        //        this.m_RibbonStartButton2007.OperationItems.Add(baseItem);
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
            BaseItemCollectionDesignerFormEx baseItemCollectionDesignerFormEx = new BaseItemCollectionDesignerFormEx(this.m_RibbonStartButton2007);
            baseItemCollectionDesignerFormEx.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            baseItemCollectionDesignerFormEx.TopMost = true;
            baseItemCollectionDesignerFormEx.Location = new Point(360, 150);
            baseItemCollectionDesignerFormEx.Show();
        }

        private void ShowApplicationPopup(object sender, EventArgs ea)
        {
            this.m_RibbonStartButton2007.ShowPopup();
        }

        private void CloseApplicationPopup(object sender, EventArgs ea)
        {
            this.m_RibbonStartButton2007.ClosePopup();
        }
    }
}
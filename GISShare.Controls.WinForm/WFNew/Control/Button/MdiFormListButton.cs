using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class MdiFormListButton : WFNew.BaseButtonN, ICollectionItem, IPopupOwner2, IPopupOwnerHelper, IPopupObjectDesignHelper
    {
        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "PopupOpened":
                    return this.PopupOpened != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "PopupClosed":
                    return this.PopupClosed != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "PopupOpened":
                    if (this.PopupOpened != null) { this.PopupOpened(this, e as PaintEventArgs); }
                    return true;
                case "PopupClosed":
                    if (this.PopupClosed != null) { this.PopupClosed(this, e as EventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        private ISimplyPopup m_DropDownPopup;

        public MdiFormListButton()
            : base()
        {
            base.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Image.RibbonMdiFormDefaultIcon.ico"));
            //
            this.m_DropDownPopup = new DropDownPopup();
            ((ISetOwnerHelper)this.m_DropDownPopup).SetOwner(this);
            //
            this.m_DropDownPopup.PopupOpened += new EventHandler(DropDownPopup_PopupOpened);
            this.m_DropDownPopup.PopupClosed += new EventHandler(DropDownPopup_PopupClosed);
        }
        void DropDownPopup_PopupOpened(object sender, EventArgs e)
        {
            this.OnPopupOpened(e);
        }
        void DropDownPopup_PopupClosed(object sender, EventArgs e)
        {
            this.Refresh();
            this.OnPopupClosed(e);
        }

        #region IPopupOwnerHelper
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_DropDownPopup;
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public BaseItemCollection BaseItems
        {
            get
            {
                if (this.m_DropDownPopup == null) return null;
                return this.m_DropDownPopup.BaseItems;
            }
        }
        #endregion

        #region IPopupOwner2
        [Browsable(true), Description("当弹出Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupOpened;
        [Browsable(true), Description("当关闭Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupClosed;

        public Padding GetPopupPadding()
        {
            return this.m_DropDownPopup.GetPadding();
        }

        public void SetPopupPadding(int iPadding)
        {
            this.m_DropDownPopup.SetPadding(iPadding);
        }

        public void SetPopupPadding(int left, int top, int right, int bottom)
        {
            this.m_DropDownPopup.SetPadding(left, top, right, bottom);
        }

        public void SetPopupRadius(int iRadius)
        {
            this.m_DropDownPopup.SetRadius(iRadius);
        }

        public void SetPopupRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius)
        {
            this.m_DropDownPopup.SetRadius(iLeftTopRadius, iLeftBottomRadius, iRightTopRadius, iRightBottomRadius);
        }

        private int m_PopupSpace = 0;
        [Browsable(true), DefaultValue(0), Description("弹出菜单与其携带者的间距"), Category("布局")]
        public int PopupSpace
        {
            get { return m_PopupSpace; }
            set { m_PopupSpace = value; }
        }

        [Browsable(false), Description("弹出菜单的坐标点（屏幕坐标）"), Category("布局")]
        public virtual Point PopupLoction
        {
            get
            {
                Size size = this.m_DropDownPopup.GetIdealSize();
                this.m_DropDownPopup.TrySetPopupPanelSize(size);
                Rectangle rectangle = this.DisplayRectangle;
                Point point;
                if (this.IsPopupItem)
                {
                    point = this.PointToScreen(new Point(rectangle.Right, rectangle.Top));
                    Padding padding = this.m_DropDownPopup.GetPadding();
                    point.X += PopupSpace + padding.Right;
                    if (System.Windows.Forms.SystemInformation.WorkingArea.Width - point.X >= size.Width) return point;
                    point = this.PointToScreen(new Point(rectangle.Left, rectangle.Top));
                    point.X = point.X - size.Width - PopupSpace - padding.Right;
                    return point;
                }
                else
                {
                    point = this.PointToScreen(new Point(rectangle.Left, rectangle.Bottom));
                    point.Y += PopupSpace;
                    return point;
                }
            }
        }

        [Browsable(false), Description("是否已展开弹出项"), Category("状态")]
        public bool IsOpened
        {
            get { return this.m_DropDownPopup.IsOpened; }
        }

        [Browsable(false), Description("是否有自动触发Popup的展现"), Category("行为")]
        public virtual bool IsAutoMouseTrigger
        {
            get { return this.IsPopupItem; }
        }

        [Browsable(false), Description("弹出菜单的激活区"), Category("布局")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get { return this.DisplayRectangle; }
        }

        /// <summary>
        /// 展开弹出项
        /// </summary>
        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_DropDownPopup.BaseItems.Clear();
            Form form = this.TryGetDependParentForm();
            if (form != null && form.IsMdiContainer && form.MdiChildren != null)
            {
                foreach (Form one in form.MdiChildren)
                {
                    BaseButtonItem ribbonBaseButtonItem = new BaseButtonItem();
                    ribbonBaseButtonItem.Name = one.Name;
                    ribbonBaseButtonItem.Text = one.Text;
                    if (one.Icon != null) ribbonBaseButtonItem.Image = one.Icon.ToBitmap();
                    ribbonBaseButtonItem.Checked = one.ContainsFocus;
                    ribbonBaseButtonItem.Tag = one;
                    ribbonBaseButtonItem.MouseClick += new MouseEventHandler(BaseButtonItem_MouseClick);
                    this.m_DropDownPopup.BaseItems.Add(ribbonBaseButtonItem);
                }
            }
            //
            this.m_DropDownPopup.Show(this.PopupLoction);
        }
        void BaseButtonItem_MouseClick(object sender, MouseEventArgs e)
        {
            BaseItem baseItem = sender as BaseItem;
            if (baseItem == null) return;
            Form form = baseItem.Tag as Form;
            if (form == null) return;
            form.Activate();
        }

        /// <summary>
        /// 关闭弹出项
        /// </summary>
        public void ClosePopup()
        {
            if (!this.IsOpened) return;
            //
            this.m_DropDownPopup.Close();
        }

        public void RefreshPopupPanel()
        {
            this.m_DropDownPopup.RefreshPopupPanel();
        }
        #endregion

        public override string Text
        {
            get
            {
                Form form = this.TryGetDependParentForm();
                if (form != null && form.IsMdiContainer && form.MdiChildren != null)
                {
                    foreach (Form one in form.MdiChildren)
                    {
                        if (one.ContainsFocus)
                        {
                            return one.Text;
                        }
                    }
                }
                return "";
            }
            set
            {
                base.Text = value;
            }
        }

        public override Image Image
        {
            get
            {
                Form form = this.TryGetDependParentForm();
                if (form != null && form.IsMdiContainer && form.MdiChildren != null)
                {
                    foreach (Form one in form.MdiChildren)
                    {
                        if (one.ContainsFocus)
                        {
                            return one.Icon != null ? one.Icon.ToBitmap() : base.Image;
                        }
                    }
                }
                return base.Image;
            }
            set
            {
                if (value == null) return;
                base.Image = value;
            }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return !this.IsOpened;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.IsOpened) return BaseItemState.ePressed;
                return base.eBaseItemState;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            //
            if (this.PopupTriggerRectangle.Contains(mevent.Location))
            {
                this.ShowPopup();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.IsOpened)
            {
                base.RelationEvent("MouseClick", e);
                return;
            }
            //
            base.OnMouseClick(e);
        }

        //

        protected virtual void OnPopupOpened(EventArgs e)
        {
            if (this.PopupOpened != null) this.PopupOpened(this, e);
        }

        protected virtual void OnPopupClosed(EventArgs e)
        {
            if (this.PopupClosed != null) this.PopupClosed(this, e);
        }
    }
}

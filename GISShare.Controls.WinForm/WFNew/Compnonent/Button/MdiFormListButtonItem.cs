using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class MdiFormListButtonItem : WFNew.BaseButtonItem, ICollectionItem, IPopupOwner2, IPopupOwnerHelper, IPopupObjectDesignHelper
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

        public MdiFormListButtonItem()
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
            this.Refresh();
            this.OnPopupOpened(e);
        }
        void DropDownPopup_PopupClosed(object sender, EventArgs e)
        {
            this.OnPopupClosed(e);
            this.Refresh();
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

        #region Clone
        public override object Clone()
        {
            MdiFormListButtonItem baseItem = new MdiFormListButtonItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.eDisplayStyle = this.eDisplayStyle;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.PopupSpace = this.PopupSpace;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            baseItem.TextLeftSpace = this.TextLeftSpace;
            baseItem.TextRightSpace = this.TextRightSpace;
            baseItem.Visible = this.Visible;
            foreach (BaseItem one in this.BaseItems)
            {
                baseItem.BaseItems.Add(one.Clone() as BaseItem);
            }
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        #endregion

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                if (this.IsOpened) return DismissPopupStyle.eNoDismiss;
                return base.eDismissPopupStyle;
            }
        }

        public override string Text
        {
            get
            {
                Form form = this.TryGetDependParentForm();
                if (form != null && form.IsMdiContainer && form.MdiChildren != null)
                {
                    foreach (Form one in form.MdiChildren)
                    {
                        if (one.ContainsFocus) return one.Text;
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
                        return one.Icon != null ? one.Icon.ToBitmap() : base.Image;
                    }
                }
                return base.Image;
            }
            set
            {
                if (value == null) return;
                //
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

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    if (this.IsOpened)
        //    {
        //        base.RelationEvent("MouseClick", e);
        //        return;
        //    }
        //    //
        //    base.OnMouseClick(e);
        //}

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class TabButtonItem : BaseButtonExItem, ITabButtonItem, ITabButtonItemEvent, ISetTabButtonItemHelper, IBaseItemProperty
    {
        private const int CTR_BUTTONSIZE = 14;

        public TabButtonItem(ITabPageItem tabPageItem)
        {
            this.m_pTabPageItem = tabPageItem;
            //
            if (this.m_pTabPageItem != null)
            {
                base.Text = this.m_pTabPageItem.Text;
            }
        }

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.pTabPageItem; }
        }
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "TabButtonActiveChanged":
                    return this.TabButtonActiveChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TabButtonMouseDown":
                    return this.TabButtonMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CloseButtonMouseDown":
                    return this.CloseButtonMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TabButtonMouseMove":
                    return this.TabButtonMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CloseButtonMouseMove":
                    return this.CloseButtonMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TabButtonMouseUp":
                    return this.TabButtonMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CloseButtonMouseUp":
                    return this.CloseButtonMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TabButtonMouseClick":
                    return this.TabButtonMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CloseButtonMouseClick":
                    return this.CloseButtonMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TabButtonMouseDoubleClick":
                    return this.TabButtonMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CloseButtonMouseDoubleClick":
                    return this.CloseButtonMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "TabButtonActiveChanged":
                    if (this.TabButtonActiveChanged != null) { this.TabButtonActiveChanged(this, e as BoolValueChangedEventArgs); }
                    return true;
                case "TabButtonMouseDown":
                    if (this.TabButtonMouseDown != null) { this.TabButtonMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "CloseButtonMouseDown":
                    if (this.CloseButtonMouseDown != null) { this.CloseButtonMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "TabButtonMouseMove":
                    if (this.TabButtonMouseMove != null) { this.TabButtonMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "CloseButtonMouseMove":
                    if (this.CloseButtonMouseMove != null) { this.CloseButtonMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "TabButtonMouseUp":
                    if (this.TabButtonMouseUp != null) { this.TabButtonMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "CloseButtonMouseUp":
                    if (this.CloseButtonMouseUp != null) { this.CloseButtonMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "TabButtonMouseClick":
                    if (this.TabButtonMouseClick != null) { this.TabButtonMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "CloseButtonMouseClick":
                    if (this.CloseButtonMouseClick != null) { this.CloseButtonMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "TabButtonMouseDoubleClick":
                    if (this.TabButtonMouseDoubleClick != null) { this.TabButtonMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                case "CloseButtonMouseDoubleClick":
                    if (this.CloseButtonMouseDoubleClick != null) { this.CloseButtonMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region ITabButtonItem
        bool m_IsSelected = false;
        [Browsable(false), Description("是否别选中"), Category("状态")]
        public bool IsSelected
        {
            get
            {
                return this.Visible ? m_IsSelected : false;
            }
        }

        [Browsable(false), Description("是否存在于TabButtonContainer里"), Category("状态")]
        public bool HaveTabButtonContainer
        {
            get { return this.pOwner is ITabButtonContainerItem; }
        }
        public virtual int OffsetValue
        {
            get { return 0; }
        }

        bool m_ShowCloseButton = false;
        public virtual bool ShowCloseButton
        {
            get
            {
                ITabButtonContainerItem pTabButtonContainerItem = this.pOwner as ITabButtonContainerItem;
                if (pTabButtonContainerItem != null)
                {
                    return pTabButtonContainerItem.UsingCloseTabButton;
                }
                //
                return m_ShowCloseButton;
            }
            set { m_ShowCloseButton = value; }
        }

        private BaseItemState m_eCloseButtonState = BaseItemState.eNormal;
        [Browsable(false), Description("关闭按钮的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eCloseButtonState
        {
            get
            {
                return m_eCloseButtonState;
            }
        }

        ITabPageItem m_pTabPageItem = null;
        public ITabPageItem pTabPageItem
        {
            get { return m_pTabPageItem; }
        }

        TabAlignment m_TabAlignment = TabAlignment.Top;
        public TabAlignment TabAlignment
        {
            get
            {
                ITabButtonContainerItem pTabButtonContainerItem = this.pOwner as ITabButtonContainerItem;
                if (pTabButtonContainerItem != null)
                {
                    return pTabButtonContainerItem.TabAlignment;
                }
                return m_TabAlignment;
            }
            set { m_TabAlignment = value; }
        }

        public Rectangle CloseButtonRectangle
        {
            get
            {
                Rectangle rectangle = base.ITDrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Rectangle(rectangle.Right - CTR_BUTTONSIZE, (rectangle.Top + rectangle.Bottom - CTR_BUTTONSIZE) / 2, CTR_BUTTONSIZE, CTR_BUTTONSIZE);
                    default:
                        return new Rectangle((rectangle.Left + rectangle.Right - CTR_BUTTONSIZE) / 2, rectangle.Bottom - CTR_BUTTONSIZE, CTR_BUTTONSIZE, CTR_BUTTONSIZE);
                }
            }
        }

        /// <summary>
        /// 选中该项
        /// </summary>
        public void Selected()
        {
            //ICollectionItem pCollectionItem = this.pOwner as ICollectionItem;
            //if (pCollectionItem == null) return;
            //foreach (ITabButtonItem one in pCollectionItem.BaseItems)
            //{
            //    if (one == null || !one.Visible) continue;
            //    //
            //    if (one == this) { ((ISetTabButtonItemHelper)one).SetIsSelected(true); }
            //    else { ((ISetTabButtonItemHelper)one).SetIsSelected(false); }
            //}
            ITabButtonContainerItem pTabButtonContainerItem = this.pOwner as ITabButtonContainerItem;
            if (pTabButtonContainerItem != null) pTabButtonContainerItem.TabButtonItemSelectedIndex = this.TryGetIndex();
        }
        #endregion

        #region ITabButtonItemEvent
        public event BoolValueChangedEventHandler TabButtonActiveChanged;
        public event MouseEventHandler TabButtonMouseDown;
        public event MouseEventHandler CloseButtonMouseDown;
        public event MouseEventHandler TabButtonMouseMove;
        public event MouseEventHandler CloseButtonMouseMove;
        public event MouseEventHandler TabButtonMouseUp;
        public event MouseEventHandler CloseButtonMouseUp;
        public event MouseEventHandler TabButtonMouseClick;
        public event MouseEventHandler CloseButtonMouseClick;
        public event MouseEventHandler TabButtonMouseDoubleClick;
        public event MouseEventHandler CloseButtonMouseDoubleClick;
        #endregion

        #region ISetTabButtonItemHelper
        void ISetTabButtonItemHelper.SetIsSelected(bool bIsSelected)
        {
            //Console.WriteLine(this.IsSelected.ToString() + " - " + bIsSelected + " - " + this.m_pTabPageItem.Visible);
            if (this.IsSelected == bIsSelected && this.IsSelected == this.m_pTabPageItem.Visible) return;
            //
            this.m_IsSelected = bIsSelected;
            if (this.pTabPageItem != null)
            {
                ISetTabPageItemHelper pSetTabPageItemHelper = this.pTabPageItem as ISetTabPageItemHelper;
                if (pSetTabPageItemHelper != null) pSetTabPageItemHelper.SetIsSelected(bIsSelected);
            }
            this.OnTabButtonActiveChanged(new BoolValueChangedEventArgs(bIsSelected));
            //
            this.Refresh();
        }
        #endregion

        protected override bool CancelPreEvent_MouseDown
        {
            get
            {
                return false;
            }
        }

        protected override bool CancelPreEvent_MouseMove
        {
            get
            {
                return false;
            }
        }

        protected override bool CancelPreEvent_MouseUp
        {
            get
            {
                return false;
            }
        }

        public override bool Enabled
        {
            get
            {
                if (this.pTabPageItem != null) return this.pTabPageItem.Enabled;
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public override string Text
        {
            get
            {
                if (this.pTabPageItem != null) return this.pTabPageItem.Text;
                return base.Text;
            }
            set
            {
                if (this.pTabPageItem != null) this.pTabPageItem.Text = value;
                base.Text = value;
            }
        }

        public override bool Checked
        {
            get
            {
                return this.IsSelected;
            }
            set
            {
                if (value) return;
                this.Selected();
            }
        }

        public override bool AutoPlanTextRectangle
        {
            get
            {
                return true;
            }
            set
            {
                base.AutoPlanTextRectangle = value;
            }
        }

        public override Padding Padding
        {
            get
            {
                switch (this.TabAlignment) 
                {
                    case TabAlignment.Top:
                        return new Padding(3, 3, this.ShowCloseButton ? 4 : 0, 2);
                    case TabAlignment.Bottom:
                        return new Padding(3, 2, this.ShowCloseButton ? 4 : 0, 3);
                    case TabAlignment.Left:
                        return new Padding(3, 3, 2, this.ShowCloseButton ? 4 : 0);
                    case TabAlignment.Right:
                        return new Padding(2, 3, 3, this.ShowCloseButton ? 4 : 0);
                    default:
                        return base.Padding;
                }
            }
            set
            {
                base.Padding = value;
            }
        }

        public override ImageSizeStyle eImageSizeStyle
        {
            get
            {
                return ImageSizeStyle.eSystem;
            }
            set
            {
                base.eImageSizeStyle = value;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.pTabPageItem != null && !this.pTabPageItem.Enabled) return BaseItemState.eDisabled;
                return this.eCloseButtonState == BaseItemState.ePressed ? BaseItemState.eHot : base.eBaseItemState;
            }
        }

        public override Rectangle ITDrawRectangle
        {
            get
            {
                if (this.ShowCloseButton)
                {
                    Rectangle rectangle = base.ITDrawRectangle;
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - CTR_BUTTONSIZE, rectangle.Bottom);
                        default:
                            return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - CTR_BUTTONSIZE);
                    }
                }
                return base.ITDrawRectangle;
            }
        }

        public override Orientation eOrientation
        {
            get
            {
                ITabButtonContainerItem pTabButtonContainerItem = this.pOwner as ITabButtonContainerItem;
                if (pTabButtonContainerItem != null)
                {
                    return pTabButtonContainerItem.eOrientation;
                }
                //
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        return Orientation.Horizontal;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return Orientation.Vertical;
                }
                return base.eOrientation;
            }
            set
            {
                base.eOrientation = value;
            }
        }

        public override ContentAlignment ImageAlign
        {
            get
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return ContentAlignment.MiddleLeft;
                    default:
                        return ContentAlignment.TopCenter;
                }
            }
            set
            {
                base.ImageAlign = value;
            }
        }

        public override ContentAlignment TextAlign
        {
            get
            {
                return ContentAlignment.MiddleCenter;
            }
            set
            {
                base.TextAlign = value;
            }
        }

        #region Clone
        public override object Clone()
        {
            TabButtonItem baseItem = new TabButtonItem(this.pTabPageItem);
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
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            //baseItem.TextLeftSpace = this.TextLeftSpace;
            //baseItem.TextRightSpace = this.TextRightSpace;
            baseItem.Visible = this.Visible;
            //
            baseItem.ShowCloseButton = this.ShowCloseButton;
            baseItem.TabAlignment = this.TabAlignment;
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
            if (this.GetEventState("TabButtonActiveChanged") == EventStateStyle.eUsed) baseItem.TabButtonActiveChanged += new BoolValueChangedEventHandler(baseItem_TabButtonActiveChanged);
            if (this.GetEventState("CloseButtonMouseUp") == EventStateStyle.eUsed) baseItem.CloseButtonMouseUp += new MouseEventHandler(baseItem_CloseButtonMouseUp);
            if (this.GetEventState("CloseButtonMouseMove") == EventStateStyle.eUsed) baseItem.CloseButtonMouseMove += new MouseEventHandler(baseItem_CloseButtonMouseMove);
            if (this.GetEventState("CloseButtonMouseDown") == EventStateStyle.eUsed) baseItem.CloseButtonMouseDown += new MouseEventHandler(baseItem_CloseButtonMouseDown);
            if (this.GetEventState("CloseButtonMouseDoubleClick") == EventStateStyle.eUsed) baseItem.CloseButtonMouseDoubleClick += new MouseEventHandler(baseItem_CloseButtonMouseDoubleClick);
            if (this.GetEventState("CloseButtonMouseClick") == EventStateStyle.eUsed) baseItem.CloseButtonMouseClick += new MouseEventHandler(baseItem_CloseButtonMouseClick);
            if (this.GetEventState("TabButtonMouseUp") == EventStateStyle.eUsed) baseItem.TabButtonMouseUp += new MouseEventHandler(baseItem_TabButtonMouseUp);
            if (this.GetEventState("TabButtonMouseMove") == EventStateStyle.eUsed) baseItem.TabButtonMouseMove += new MouseEventHandler(baseItem_TabButtonMouseMove);
            if (this.GetEventState("TabButtonMouseDown") == EventStateStyle.eUsed) baseItem.TabButtonMouseDown += new MouseEventHandler(baseItem_TabButtonMouseDown);
            if (this.GetEventState("TabButtonMouseDoubleClick") == EventStateStyle.eUsed) baseItem.TabButtonMouseDoubleClick += new MouseEventHandler(baseItem_TabButtonMouseDoubleClick);
            if (this.GetEventState("TabButtonMouseClick") == EventStateStyle.eUsed) baseItem.TabButtonMouseClick += new MouseEventHandler(baseItem_TabButtonMouseClick);
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
        void baseItem_TabButtonActiveChanged(object sender, BoolValueChangedEventArgs e)
        {
            this.RelationEvent("TabButtonActiveChanged", e);
        }
        void baseItem_TabButtonMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TabButtonMouseClick", e);
        }
        void baseItem_TabButtonMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TabButtonMouseDoubleClick", e);
        }
        void baseItem_TabButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TabButtonMouseDown", e);
        }
        void baseItem_TabButtonMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TabButtonMouseMove", e);
        }
        void baseItem_TabButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TabButtonMouseUp", e);
        }
        void baseItem_CloseButtonMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("CloseButtonMouseClick(", e);
        }
        void baseItem_CloseButtonMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("CloseButtonMouseDoubleClick", e);
        }
        void baseItem_CloseButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("CloseButtonMouseDown", e);
        }
        void baseItem_CloseButtonMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("CloseButtonMouseMove", e);
        }
        void baseItem_CloseButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("CloseButtonMouseUp", e);
        }
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            if (!this.ShowCloseButton) return base.MeasureSize(g);
            //
            Size size = base.MeasureSize(g);
            return this.eOrientation == Orientation.Horizontal ? new Size(size.Width + CTR_BUTTONSIZE, size.Height) : new Size(size.Width, size.Height + CTR_BUTTONSIZE);
        }

        public override bool RemoveSelf()
        {
             ITabButtonContainerItem pTabButtonContainerItem = this.pOwner as ITabButtonContainerItem;
             if (pTabButtonContainerItem == null) return base.RemoveSelf();
             ITabControl pTabControl = pTabButtonContainerItem.TryGetTabControl();
             if (pTabControl == null) return base.RemoveSelf();
             pTabControl.RemoveTabPage(this.pTabPageItem);
             return true;
        }

        public override void Refresh()
        {
            if (this.OffsetValue <= 0) { base.Refresh(); return; }
            //
            Rectangle rectangle = this.DisplayRectangle;
            switch (this.TabAlignment) 
            {
                case TabAlignment.Left:
                    this.Invalidate(Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top - 1, rectangle.Right + this.OffsetValue + 1, rectangle.Bottom + 1));
                    break;
                case TabAlignment.Right:
                    this.Invalidate(Rectangle.FromLTRB(rectangle.Left - 1 - this.OffsetValue, rectangle.Top - 1, rectangle.Right + 1, rectangle.Bottom + 1));
                    break;
                case TabAlignment.Top:
                    this.Invalidate(Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top - 1, rectangle.Right + 1, rectangle.Bottom + 1 + this.OffsetValue));
                    break;
                case TabAlignment.Bottom:
                default:
                    this.Invalidate(Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top - 1 - this.OffsetValue, rectangle.Right + 1, rectangle.Bottom + 1));
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (this.ShowCloseButton)
            {
                if (this.CloseButtonRectangle.Contains(mevent.Location))
                {
                    this.m_eCloseButtonState = BaseItemState.ePressed;
                    this.OnCloseButtonMouseDown(mevent);
                }
                else
                {
                    this.OnTabButtonMouseDown(mevent);
                }
            }
            else
            {
                this.OnTabButtonMouseDown(mevent);
            }
            //
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (this.ShowCloseButton)
            {
                if (this.CloseButtonRectangle.Contains(mevent.Location))
                {
                    if (this.m_eCloseButtonState != BaseItemState.ePressed &&
                        this.m_eCloseButtonState != BaseItemState.eHot) { this.m_eCloseButtonState = BaseItemState.eHot; this.Refresh(); }
                    this.OnCloseButtonMouseMove(mevent);
                }
                else
                {
                    if (this.m_eCloseButtonState != BaseItemState.ePressed &&
                        this.m_eCloseButtonState != BaseItemState.eNormal) { this.m_eCloseButtonState = BaseItemState.eNormal; this.Refresh(); }
                    this.OnTabButtonMouseMove(mevent);
                }
            }
            else
            {
                this.OnTabButtonMouseMove(mevent);
            }
            //
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.ShowCloseButton)
            {
                if (this.DisplayRectangle.Contains(mevent.Location))
                {
                    if (this.CloseButtonRectangle.Contains(mevent.Location))
                    {
                        this.m_eCloseButtonState = BaseItemState.eHot;
                        this.OnCloseButtonMouseUp(mevent);
                    }
                    else
                    {
                        this.OnTabButtonMouseUp(mevent);
                    }
                }
                else
                {
                    this.m_eCloseButtonState = BaseItemState.eNormal;
                }
            }
            else
            {
                this.OnTabButtonMouseUp(mevent);
            }
            //
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.ShowCloseButton)
            {
                if (this.CloseButtonRectangle.Contains(e.Location))
                {
                    this.OnCloseButtonMouseClick(e);
                }
                else
                {
                    this.OnTabButtonMouseClick(e);
                }
            }
            else
            {
                this.OnTabButtonMouseClick(e);
            }
            //
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.ShowCloseButton)
            {
                if (this.CloseButtonRectangle.Contains(e.Location))
                {
                    this.OnCloseButtonMouseClick(e);
                }
                else
                {
                    this.OnTabButtonMouseClick(e);
                }
            }
            else
            {
                this.OnTabButtonMouseClick(e);
            }
            //
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.m_eCloseButtonState = BaseItemState.eNormal;
            //
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.m_eCloseButtonState = BaseItemState.eHot;
            //
            base.OnMouseEnter(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTabButton(
                   new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eImage:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    break;
                case DisplayStyle.eText:
                    StringFormat stringFormat = new StringFormat();
                    if (this.eOrientation == Orientation.Vertical) stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                    stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle, stringFormat));
                    break;
                case DisplayStyle.eImageAndText:
                    StringFormat stringFormat2 = new StringFormat();
                    if (this.eOrientation == Orientation.Vertical) stringFormat2.FormatFlags = StringFormatFlags.DirectionVertical;
                    stringFormat2.Trimming = StringTrimming.EllipsisCharacter;
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle, stringFormat2));
                    break;
                default:
                    break;
            }
            //
            //base.OnDraw(e);
        }

        //

        protected virtual void OnTabButtonActiveChanged(BoolValueChangedEventArgs e)
        {
            if (this.TabButtonActiveChanged != null) { this.TabButtonActiveChanged(this, e); }
        }

        protected virtual void OnTabButtonMouseDown(MouseEventArgs e)
        {
            if (this.TabButtonMouseDown != null) { this.TabButtonMouseDown(this, e); }
        }

        protected virtual void OnCloseButtonMouseDown(MouseEventArgs e)
        {
            if (this.CloseButtonMouseDown != null) { this.CloseButtonMouseDown(this, e); }
        }

        protected virtual void OnTabButtonMouseMove(MouseEventArgs e)
        {
            if (this.TabButtonMouseMove != null) { this.TabButtonMouseMove(this, e); }
        }

        protected virtual void OnCloseButtonMouseMove(MouseEventArgs e)
        {
            if (this.CloseButtonMouseMove != null) { this.CloseButtonMouseMove(this, e); }
        }

        protected virtual void OnTabButtonMouseUp(MouseEventArgs e)
        {
            if (this.TabButtonMouseUp != null) { this.TabButtonMouseUp(this, e); }
        }

        protected virtual void OnCloseButtonMouseUp(MouseEventArgs e)
        {
            if (this.CloseButtonMouseUp != null) { this.CloseButtonMouseUp(this, e); }
        }

        protected virtual void OnTabButtonMouseClick(MouseEventArgs e)
        {
            if (this.TabButtonMouseClick != null) { this.TabButtonMouseClick(this, e); }
        }

        protected virtual void OnCloseButtonMouseClick(MouseEventArgs e)
        {
            if (this.CloseButtonMouseClick != null) { this.CloseButtonMouseClick(this, e); }
        }

        protected virtual void OnTabButtonMouseDoubleClick(MouseEventArgs e)
        {
            if (this.TabButtonMouseDoubleClick != null) { this.TabButtonMouseDoubleClick(this, e); }
        }

        protected virtual void OnCloseButtonMouseDoubleClick(MouseEventArgs e)
        {
            if (this.CloseButtonMouseDoubleClick != null) { this.CloseButtonMouseDoubleClick(this, e); }
        }
    }
}

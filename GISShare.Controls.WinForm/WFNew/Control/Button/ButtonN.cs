using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ButtonDesigner)), ToolboxItem(true)]
    public class ButtonN : ImageLabel, IButtonItem, IPopupOwnerHelper, ICollectionObjectDesignHelper, IPopupObjectDesignHelper//, IBaseItemOwner, IBaseItemOwner2
    {
        public event MouseEventHandler ButtonMouseDown;
        public event MouseEventHandler SplitMouseDown;
        public event MouseEventHandler ButtonMouseMove;
        public event MouseEventHandler SplitMouseMove;
        public event MouseEventHandler ButtonMouseUp;
        public event MouseEventHandler SplitMouseUp;
        public event MouseEventHandler ButtonMouseClick;
        public event MouseEventHandler SplitMouseClick;
        public event MouseEventHandler ButtonMouseDoubleClick;
        public event MouseEventHandler SplitMouseDoubleClick;

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "PopupOpened":
                    return this.PopupOpened != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "PopupClosed":
                    return this.PopupClosed != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseDown":
                    return this.ButtonMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseDown":
                    return this.SplitMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseMove":
                    return this.ButtonMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseMove":
                    return this.SplitMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseUp":
                    return this.ButtonMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseUp":
                    return this.SplitMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseClick":
                    return this.ButtonMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseClick":
                    return this.SplitMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseDoubleClick":
                    return this.ButtonMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseDoubleClick":
                    return this.SplitMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "ButtonMouseDown":
                    if (this.ButtonMouseDown != null) { this.ButtonMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseDown":
                    if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseMove":
                    if (this.ButtonMouseMove != null) { this.ButtonMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseMove":
                    if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseUp":
                    if (this.ButtonMouseUp != null) { this.ButtonMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseUp":
                    if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseClick":
                    if (this.ButtonMouseClick != null) { this.ButtonMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseClick":
                    if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseDoubleClick":
                    if (this.ButtonMouseDoubleClick != null) { this.ButtonMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseDoubleClick":
                    if (this.SplitMouseDoubleClick != null) { this.SplitMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        private ISimplyPopup m_DropDownPopup;

        public ButtonN(ISimplyPopup pSimplyPopup)
            : base()
        {
            base.BackColor = System.Drawing.Color.Transparent;
            //
            this.m_DropDownPopup = pSimplyPopup;
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
            this.OnPopupClosed(e);
            //
            if (!this.Contains(this.PointToClient(System.Windows.Forms.Form.MousePosition)))
            {
                this.Refresh();
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            }
        }

        public ButtonN()
            : this(new DropDownPopup()) { }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.BaseItems.ExchangeItem(item1, item2); }
        #endregion

        #region IPopupOwnerHelper
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_DropDownPopup;
        }
        #endregion
        
        #region IPopupOwner2
        public event EventHandler PopupOpened;
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
                    //point = this.PointToScreen(new Point(rectangle.Right, rectangle.Top));
                    //point.X += PopupSpace;
                    //if (System.Windows.Forms.SystemInformation.WorkingArea.Width - point.X >= size.Width) return point;
                    //point = this.PointToScreen(new Point(rectangle.Left, rectangle.Top));
                    //point.X = point.X - size.Width - PopupSpace;
                    //return point;
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
            get
            {
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eDropDownButton:
                        return this.IsPopupItem;
                    default:
                        return false;
                }
            }
        }

        [Browsable(false), Description("弹出菜单的激活区"), Category("布局")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get
            {
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eSplitButton:
                        return this.SplitRectangle;
                    default:
                        return this.DisplayRectangle;
                }
            }
        }

        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_DropDownPopup.Show(this.PopupLoction);
        }

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

        #region IBaseItemOwner
        public override Rectangle ItemsRectangle
        {
            get { return this.m_DropDownPopup.DisplayRectangle; }
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

        #region ICollectionItem2
        public IBaseItem GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem2 pCollectionItem2 = one as ICollectionItem2;
                    if (pCollectionItem2 != null)
                    {
                        pBaseItem = pCollectionItem2.GetBaseItem(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region ICollectionItem3
        public IBaseItem GetBaseItem2(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem3 pCollectionItem3 = one as ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region IButtonItem
        [Browsable(true), DefaultValue(typeof(ContextPopupStyle), "eNormal"), Description("Popup面板展现方式"), Category("外观")]
        public ContextPopupStyle eContextPopupStyle
        {
            get
            {
                IDropDownPopup pDropDownPopup = this.m_DropDownPopup as IDropDownPopup;
                if (pDropDownPopup == null) return ContextPopupStyle.eNormal;
                return pDropDownPopup.eContextPopupStyle;
            }
            set
            {
                IDropDownPopup pDropDownPopup = this.m_DropDownPopup as IDropDownPopup;
                if (pDropDownPopup == null) return;
                pDropDownPopup.eContextPopupStyle = value;
            }
        }

        private bool m_ShowNomalState = true;
        [Browsable(true), DefaultValue(true), Description("是否显示正常状态下的状态"), Category("状态")]
        public virtual bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        private bool m_ShowNomalSplitLine = false;
        [Browsable(true), DefaultValue(false), Description("是否显示分隔条"), Category("状态")]
        public bool ShowNomalSplitLine
        {
            get { return m_ShowNomalSplitLine; }
            set { m_ShowNomalSplitLine = value; }
        }

        [Browsable(false), Description("是否为正常的选中（当其为Popup的一级子项是使用）IDropDownPopup"), Category("状态")]
        public bool NomalChecked
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 虚拟的 popup 矩形区  （屏幕坐标）
        /// </summary>
        [Browsable(false), Description("虚拟的 popup 入口矩形区  （屏幕坐标）"), Category("布局")]
        public Rectangle VirtualPopupRectangle
        {
            get
            {
                Point point = this.m_DropDownPopup.PointToScreen(this.m_DropDownPopup.DisplayRectangle.Location);
                return new Rectangle(
                    point.X - this.PopupSpace - 3,
                    point.Y - PopupSpace - 3,
                    this.m_DropDownPopup.Width + 2 * this.PopupSpace + 6,
                    this.m_DropDownPopup.Height + 2 * this.PopupSpace + 6);
            }
        }

        #region Radius
        private int m_LeftTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左顶部圆角值"), Category("圆角")]
        public virtual int LeftTopRadius
        {
            get { return m_LeftTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftTopRadius = value;
            }
        }

        private int m_RightTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右顶部圆角值"), Category("圆角")]
        public virtual int RightTopRadius
        {
            get { return m_RightTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightTopRadius = value;
            }
        }

        private int m_LeftBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左底部圆角值"), Category("圆角")]
        public virtual int LeftBottomRadius
        {
            get { return m_LeftBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftBottomRadius = value;
            }
        }

        private int m_RightBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右底部圆角值"), Category("圆角")]
        public virtual int RightBottomRadius
        {
            get { return m_RightBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightBottomRadius = value;
            }
        }
        #endregion

        private ButtonStyle m_eButtonStyle = ButtonStyle.eButton;
        [Browsable(true), DefaultValue(typeof(ButtonStyle), "eButton"), Description("按钮类型"), Category("设计")]
        public virtual ButtonStyle eButtonStyle
        {
            get { return m_eButtonStyle; }
            set
            {
                m_eButtonStyle = value;
                //
                switch (value)
                {
                    case ButtonStyle.eDropDownButton:
                    case ButtonStyle.eSplitButton:
                        if (this.eArrowDock == ArrowDock.eNone) this.eArrowDock = ArrowDock.eRight;
                        break;
                    default:
                        this.eArrowDock = ArrowDock.eNone;
                        break;
                }
            }
        }

        private ArrowStyle m_eArrowStyle = ArrowStyle.eToDown;
        [Browsable(true), DefaultValue(typeof(ArrowStyle), "eToDown"), Description("箭头类型"), Category("布局")]
        public virtual ArrowStyle eArrowStyle
        {
            get { return m_eArrowStyle; }
            set { m_eArrowStyle = value; }
        }

        private ArrowDock m_eArrowDock = ArrowDock.eNone;
        [Browsable(true), DefaultValue(typeof(ArrowDock), "eNone"), Description("箭头停靠的位置"), Category("布局")]
        public virtual ArrowDock eArrowDock
        {
            get { return m_eArrowDock; }
            set { m_eArrowDock = value; }
        }

        private int m_DropDownDistance = 15;
        [Browsable(true), DefaultValue(15), Description("弹出区宽度"), Category("布局")]
        public virtual int DropDownDistance
        {
            get { return m_DropDownDistance; }
            set 
            {
                if (value <= 0) return;
                //switch (this.eArrowDock) 
                //{
                //    case ArrowDock.eUp:
                //    case ArrowDock.eDown:
                //        if (value >= this.Height - this.ITSpace) return;
                //        break;
                //    case ArrowDock.eLeft:
                //    case ArrowDock.eRight:
                //        if (value >= this.Width - this.ITSpace) return;
                //        break;
                //}
                switch (this.eArrowDock)
                {
                    case ArrowDock.eUp:
                        if (value >= this.Height - this.Padding.Top) return;
                        break;
                    case ArrowDock.eDown:
                        if (value >= this.Height - this.Padding.Bottom) return;
                        break;
                    case ArrowDock.eLeft:
                        if (value >= this.Width - this.Padding.Left) return;
                        break;
                    case ArrowDock.eRight:
                        if (value >= this.Width - this.Padding.Right) return;
                        break;
                }
                //
                m_DropDownDistance = value;
            }
        }

        private Size m_ArrowSize = new Size(3, 3);
        [Browsable(true), DefaultValue(typeof(Size), "3, 3"), Description("箭头尺寸"), Category("布局")]
        public virtual Size ArrowSize
        {
            get { return m_ArrowSize; }
            set
            {
                if (value.Width <= 0 || value.Height <= 0) return;
                //
                m_ArrowSize = value;
            }
        }

        [Browsable(false), Description("勾选区矩形框（的一级子项是使用）"), Category("布局")]
        public Rectangle CheckRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        [Browsable(false), Description("按钮实体矩形框"), Category("布局")]
        public virtual Rectangle ButtonRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                //
                if (this.eButtonStyle == ButtonStyle.eDropDownButton ||
                    this.eButtonStyle == ButtonStyle.eSplitButton)
                {
                    switch (this.eArrowDock)
                    {
                        case ArrowDock.eUp:
                            return new Rectangle(rectangle.X, rectangle.Y + this.DropDownDistance - 1, rectangle.Width, rectangle.Height - this.DropDownDistance);
                        case ArrowDock.eDown:
                            return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height - this.DropDownDistance + 1);// + 1
                        case ArrowDock.eLeft:
                            return new Rectangle(rectangle.X + this.DropDownDistance - 1, rectangle.Y, rectangle.Width - this.DropDownDistance, rectangle.Height);
                        case ArrowDock.eRight:
                            return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - this.DropDownDistance + 1, rectangle.Height);// + 1
                        default:
                            return rectangle;
                    }
                }
                //
                return rectangle;
            }
        }

        [Browsable(false), Description("弹出区矩形框"), Category("布局")]
        public virtual Rectangle DropDownRectangle
        {
            get
            {
                Rectangle rectangle = this.ITDrawRectangle;
                //
                switch (this.eArrowDock)
                {
                    case ArrowDock.eUp:
                        return new Rectangle(rectangle.Left - this.Padding.Left, rectangle.Top - this.Padding.Top - this.DropDownDistance, rectangle.Width + this.Padding.Left + this.Padding.Right, this.DropDownDistance);
                    case ArrowDock.eDown:
                        return new Rectangle(rectangle.Left - this.Padding.Left, rectangle.Bottom + this.Padding.Bottom, rectangle.Width + this.Padding.Left + this.Padding.Right, this.DropDownDistance);
                    case ArrowDock.eLeft:
                        return new Rectangle(rectangle.Left - this.Padding.Left - this.DropDownDistance, rectangle.Top - this.Padding.Top, this.DropDownDistance, rectangle.Height + this.Padding.Top + this.Padding.Bottom);
                    case ArrowDock.eRight:
                        return new Rectangle(rectangle.Right + this.Padding.Right, rectangle.Top - this.Padding.Top, this.DropDownDistance, rectangle.Height + this.Padding.Top + this.Padding.Bottom);
                    default:
                        return new Rectangle(rectangle.Left, rectangle.Top, 0, 0);
                }
                //switch (this.eArrowDock)
                //{
                //    case ArrowDock.eUp:
                //        return new Rectangle(rectangle.Left - this.Padding.Left, rectangle.Top - this.Padding.Top - this.DropDownDistance, rectangle.Width + this.Padding.Left + this.Padding.Right, this.DropDownDistance + this.Padding.Top);
                //    case ArrowDock.eDown:
                //        return new Rectangle(rectangle.Left - this.Padding.Left, rectangle.Bottom, rectangle.Width + this.Padding.Left + this.Padding.Right, this.DropDownDistance + this.Padding.Bottom);
                //    case ArrowDock.eLeft:
                //        return new Rectangle(rectangle.Left - this.Padding.Left - this.DropDownDistance, rectangle.Top - this.Padding.Top, this.DropDownDistance + this.Padding.Left, rectangle.Height + this.Padding.Top + this.Padding.Bottom);
                //    case ArrowDock.eRight:
                //        return new Rectangle(rectangle.Right, rectangle.Top - this.Padding.Top, this.DropDownDistance + this.Padding.Right, rectangle.Height + this.Padding.Top + this.Padding.Bottom);
                //    default:
                //        return new Rectangle(rectangle.Left, rectangle.Top, 0, 0);
                //}
            }
        }

        [Browsable(false), Description("分割区矩形框"), Category("布局")]
        public virtual Rectangle SplitRectangle
        {
            get
            {
                return this.DropDownRectangle;
            }
        }

        [Browsable(false), Description("按钮有效触发区"), Category("行为")]
        public virtual Rectangle ButtonTriggerRectangle
        {
            get
            {
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eSplitButton:
                        return this.ButtonRectangle;
                    default:
                        return this.DisplayRectangle;
                }
            }
        }

        [Browsable(false), Description("箭头区矩形框"), Category("布局")]
        public virtual Rectangle ArrowRectangle
        {
            get
            {
                Rectangle dropDownRectangle = this.DropDownRectangle;
                return new Rectangle(
                    new Point((dropDownRectangle.Left + dropDownRectangle.Right - this.ArrowSize.Width) / 2, (dropDownRectangle.Top + dropDownRectangle.Bottom - this.ArrowSize.Height) / 2),
                    this.ArrowSize);
            }
        }

        private BaseItemState m_eSplitState = BaseItemState.eNormal;
        [Browsable(false), Description("分割区的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eSplitState
        {
            get
            {
                if (this.IsOpened) return BaseItemState.ePressed;
                return m_eSplitState;
            }
        }
        #endregion

        public override Rectangle DrawRectangle
        {
            get
            {
                return this.ButtonRectangle;
            }
        }

        [Browsable(false), Description("图片和文本绘制矩形框"), Category("布局")]
        public override Rectangle ITDrawRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                //
                if (this.eButtonStyle == ButtonStyle.eDropDownButton ||
                    this.eButtonStyle == ButtonStyle.eSplitButton)
                {
                    switch (this.eArrowDock)
                    {
                        case ArrowDock.eUp:
                            return new Rectangle(rectangle.X + this.Padding.Left,
                                rectangle.Y + this.Padding.Top + this.DropDownDistance,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom - this.DropDownDistance);
                        case ArrowDock.eDown:
                            return new Rectangle(rectangle.X + this.Padding.Left,
                                rectangle.Y + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom - this.DropDownDistance);
                        case ArrowDock.eLeft:
                            return new Rectangle(rectangle.X + this.Padding.Left + this.DropDownDistance,
                                rectangle.Y + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right - this.DropDownDistance,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        case ArrowDock.eRight:
                            return new Rectangle(rectangle.X + this.Padding.Left,
                                rectangle.Y + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right - this.DropDownDistance,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        default:
                            return new Rectangle(rectangle.X + this.Padding.Left,
                                rectangle.Y + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                    }
                }
                //
                //return new Rectangle(rectangle.X + this.ITSpace, rectangle.Y + this.ITSpace, rectangle.Width - 2 * this.ITSpace, rectangle.Height - 2 * this.ITSpace);
                return new Rectangle(rectangle.X + this.Padding.Left, rectangle.Y + this.Padding.Top, rectangle.Width - this.Padding.Left - this.Padding.Right, rectangle.Height - this.Padding.Top - this.Padding.Bottom);
            }
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        #region Clone
        public override object Clone()
        {
            ButtonN baseItem = new ButtonN();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.ArrowSize = this.ArrowSize;
            baseItem.DropDownDistance = this.DropDownDistance;
            baseItem.eArrowDock = this.eArrowDock;
            baseItem.eArrowStyle = this.eArrowStyle;
            baseItem.eButtonStyle = this.eButtonStyle;
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
            baseItem.ShowNomalSplitLine = this.ShowNomalSplitLine;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            //baseItem.TextLeftSpace = this.TextLeftSpace;
            //baseItem.TextRightSpace = this.TextRightSpace;
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
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("ButtonMouseUp") == EventStateStyle.eUsed) baseItem.ButtonMouseUp += new MouseEventHandler(baseItem_ButtonMouseUp);
            if (this.GetEventState("ButtonMouseMove") == EventStateStyle.eUsed) baseItem.ButtonMouseMove += new MouseEventHandler(baseItem_ButtonMouseMove);
            if (this.GetEventState("ButtonMouseDown") == EventStateStyle.eUsed) baseItem.ButtonMouseDown += new MouseEventHandler(baseItem_ButtonMouseDown);
            if (this.GetEventState("ButtonMouseDoubleClick") == EventStateStyle.eUsed) baseItem.ButtonMouseDoubleClick += new MouseEventHandler(baseItem_ButtonMouseDoubleClick);
            if (this.GetEventState("ButtonMouseClick") == EventStateStyle.eUsed) baseItem.ButtonMouseClick += new MouseEventHandler(baseItem_ButtonMouseClick);
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
        void baseItem_ButtonMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseClick", e);
        }
        void baseItem_ButtonMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDoubleClick", e);
        }
        void baseItem_ButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDown", e);
        }
        void baseItem_ButtonMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseMove", e);
        }
        void baseItem_ButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseUp", e);
        }
        void baseItem_SplitMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseClick(", e);
        }
        void baseItem_SplitMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDoubleClick", e);
        }
        void baseItem_SplitMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDown", e);
        }
        void baseItem_SplitMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseMove", e);
        }
        void baseItem_SplitMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseUp", e);
        }
        #endregion

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
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    if (this.SplitRectangle.Contains(mevent.Location))
                    { 
                        this.OnSplitMouseDown(mevent);
                    }
                    else
                    {
                        this.OnButtonMouseDown(mevent);
                    } 
                    break;
                default:
                case ButtonStyle.eButton:
                case ButtonStyle.eCheckButton:
                case ButtonStyle.eDropDownButton:
                    break;
            }
            base.OnMouseDown(mevent);
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                case ButtonStyle.eSplitButton:
                    if (this.PopupTriggerRectangle.Contains(mevent.Location)) this.ShowPopup();
                    break;
                default:
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    if (this.SplitRectangle.Contains(mevent.Location))
                    {
                        if (this.eSplitState != BaseItemState.ePressed &&
                            this.eSplitState != BaseItemState.eHot) { this.m_eSplitState = BaseItemState.eHot; this.Refresh(); }
                        this.OnSplitMouseMove(mevent);
                    }
                    else
                    {
                        if (this.eSplitState != BaseItemState.ePressed &&
                            this.eSplitState != BaseItemState.eNormal) { this.m_eSplitState = BaseItemState.eNormal; this.Refresh(); }
                        this.OnButtonMouseMove(mevent);
                    }
                    break;
                default:
                    break;
            }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eCheckButton:
                    this.Checked = !this.Checked; 
                    break;
                case ButtonStyle.eSplitButton:
                    if (this.DisplayRectangle.Contains(mevent.Location))
                    {
                        if (this.SplitRectangle.Contains(mevent.Location)) { this.m_eSplitState = BaseItemState.eHot; this.OnSplitMouseUp(mevent); }
                        else { this.OnButtonMouseUp(mevent); }
                    }
                    else 
                    { 
                        this.m_eSplitState = BaseItemState.eNormal;
                    }
                    break;
                default:
                case ButtonStyle.eButton:
                case ButtonStyle.eDropDownButton:
                    break;
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    if (this.SplitRectangle.Contains(e.Location))
                    { this.OnSplitMouseClick(e); }
                    else
                    { this.OnButtonMouseClick(e); }
                    break;
                default:
                    break;
            }
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                    if (this.IsOpened)
                    {
                        base.RelationEvent("MouseClick", e);
                        return;
                    }
                    break;
                default:
                    if (this.ButtonTriggerRectangle.Contains(e.Location))
                    {
                        BasePopup basePopup = this.TryGetDependBasePopup();
                        if (basePopup != null) basePopup.DismissPopup();
                    }
                    break;
            }
            //
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    if (this.SplitRectangle.Contains(e.Location))
                    { this.OnSplitMouseDoubleClick(e); }
                    else
                    { this.OnButtonMouseDoubleClick(e); }
                    break;
                default:
                    break;
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    this.m_eSplitState = BaseItemState.eHot;
                    break;
                case ButtonStyle.eButton:
                case ButtonStyle.eCheckButton:
                case ButtonStyle.eDropDownButton:
                default:
                    break;
            }
            //
            base.OnMouseEnter(e);
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                    if (this.IsAutoMouseTrigger) //if (this.IsAutoMouseTrigger && this.IsPopupItem)
                    { 
                        this.ShowPopup();
                    }
                    break;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eSplitButton:
                    this.m_eSplitState = BaseItemState.eNormal; 
                    break;
                case ButtonStyle.eButton:
                case ButtonStyle.eCheckButton:
                case ButtonStyle.eDropDownButton:
                default:
                    break;
            }
            //
            base.OnMouseLeave(e);
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                    if (this.IsAutoMouseTrigger &&
                        !this.VirtualPopupRectangle.Contains(Form.MousePosition))//if (this.IsAutoMouseTrigger && this.IsPopupItem)
                    {
                        this.ClosePopup();
                    }
                    break;
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            switch (this.eButtonStyle) 
            {
                case ButtonStyle.eButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderBaseButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eCheckButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCheckButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eDropDownButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDropDownButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eSplitButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSplitButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
                    break;
                default:
                    break;
            }
            //
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eImage:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    break;
                case DisplayStyle.eText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    break;
                case DisplayStyle.eImageAndText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    break;
                default:
                    break;
            }
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                case ButtonStyle.eSplitButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                        new GISShare.Controls.WinForm.ArrowRenderEventArgs(e.Graphics, this, this.Enabled, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
                    break;
                default:
                    break;
            }
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

        protected virtual void OnButtonMouseDown(MouseEventArgs e)
        {
            if (this.ButtonMouseDown != null) { this.ButtonMouseDown(this, e); }
        }

        protected virtual void OnSplitMouseDown(MouseEventArgs e)
        {
            if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e); }
        }

        protected virtual void OnButtonMouseMove(MouseEventArgs e)
        {
            if (this.ButtonMouseMove != null) { this.ButtonMouseMove(this, e); }
        }

        protected virtual void OnSplitMouseMove(MouseEventArgs e)
        {
            if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e); }
        }

        protected virtual void OnButtonMouseUp(MouseEventArgs e)
        {
            if (this.ButtonMouseUp != null) { this.ButtonMouseUp(this, e); }
        }

        protected virtual void OnSplitMouseUp(MouseEventArgs e)
        {
            if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e); }
        }

        protected virtual void OnButtonMouseClick(MouseEventArgs e)
        {
            if (this.ButtonMouseClick != null) { this.ButtonMouseClick(this, e); }
        }

        protected virtual void OnSplitMouseClick(MouseEventArgs e)
        {
            if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e); }
        }

        protected virtual void OnButtonMouseDoubleClick(MouseEventArgs e)
        {
            if (this.ButtonMouseDoubleClick != null) { this.ButtonMouseDoubleClick(this, e); }
        }

        protected virtual void OnSplitMouseDoubleClick(MouseEventArgs e)
        {
            if (this.SplitMouseDoubleClick != null) { this.SplitMouseDoubleClick(this, e); }
        }
    }

}

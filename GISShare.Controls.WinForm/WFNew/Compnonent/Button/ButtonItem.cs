using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ButtonItemDesigner))]
    public class ButtonItem : ImageLabelItem, 
        IButtonItem, IDismissPopupObject, IPopupOwnerHelper, ICollectionObjectDesignHelper, IPopupObjectDesignHelper//, IMeasureHelper
    {
        private ISimplyPopup m_DropDownPopup;

        #region 构造函数
        public ButtonItem(ISimplyPopup pSimplyPopup)
            : base()
        {
            this.m_DropDownPopup = pSimplyPopup;
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
            //
            if (!this.Contains(this.PointToClient(System.Windows.Forms.Form.MousePosition)))
            {
                this.Refresh();
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            }
        }

        public ButtonItem()
            : this(new DropDownPopup()) { }

        public ButtonItem(string strText)
            : this(new DropDownPopup())
        {
            this.Text = strText;
        }

        public ButtonItem(string strName, string strText)
            : this(new DropDownPopup())
        {
            this.Name = strName;
            this.Text = strText;
        }

        public ButtonItem(string strText, Image image)
            : this(new DropDownPopup())
        {
            this.Text = strText;
            this.Image = image;
        }

        public ButtonItem(string strName, string strText, Image image)
            : this(new DropDownPopup())
        {
            this.Name = strName;
            this.Text = strText;
            this.Image = image;
        }

        //public ButtonItem(GISShare.Controls.Plugin.WFNew.IButtonItemP pBaseItemP)
        //    : this(new DropDownPopup())
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //    //IBaseButtonItemP
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.ShowNomalState = pBaseItemP.ShowNomalState;
        //    //IDropDownButtonItemP
        //    this.DropDownDistance = pBaseItemP.DropDownDistance;
        //    this.eArrowStyle = pBaseItemP.eArrowStyle;
        //    this.eArrowDock = pBaseItemP.eArrowDock;
        //    this.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
        //    this.ArrowSize = pBaseItemP.ArrowSize;
        //    this.PopupSpace = pBaseItemP.PopupSpace;
        //    //ISplitButtonItemP
        //    this.ShowNomalSplitLine = pBaseItemP.ShowNomalSplitLine;
        //    //IButtonItemP
        //    this.eButtonStyle = pBaseItemP.eButtonStyle;
        //}
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                //case "CheckedChanged":
                //    return this.CheckedChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                //case "CheckedChanged":
                //    if (this.CheckedChanged != null) { this.CheckedChanged(this, e as EventArgs); }
                //return true;
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

        public void RefreshPopupPanel()
        {
            this.m_DropDownPopup.RefreshPopupPanel();
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

        /// <summary>
        /// 展开弹出项
        /// </summary>
        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_DropDownPopup.Show(this.PopupLoction);
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

        private int m_TextLeftSpace = 5;
        [Browsable(true), DefaultValue(5), Description("文本左间距（当其为Popup的一级子项是使用）"), Category("布局")]
        public int TextLeftSpace
        {
            get { return m_TextLeftSpace; }
            set
            {
                if (value < 1) return;
                m_TextLeftSpace = value;
            }
        }

        private int m_TextRightSpace = 5;
        [Browsable(true), DefaultValue(5), Description("文本右间距（当其为Popup的一级子项是使用）"), Category("布局")]
        public int TextRightSpace
        {
            get { return m_TextRightSpace; }
            set
            {
                if (value < 1) return;
                m_TextRightSpace = value;
            }
        }

        #region IButtonItem
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseDown;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDown;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseMove;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseMove;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseUp;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseUp;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseClick;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseDoubleClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDoubleClick;

        [Browsable(false), Description("是否为正常的选中（当其为Popup的一级子项是使用）"), Category("状态")]
        public bool NomalChecked
        {
            get
            {
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null) return true;
                else 
                {
                    switch (pContextPopupPanelItem.eContextPopupStyle) 
                    {
                        case ContextPopupStyle.eSuper:
                        case ContextPopupStyle.eNormal:
                            return false;
                        default:
                            return true;
                    }
                }
            }
        }

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

        private bool m_ShowNomalState = false;
        [Browsable(true), DefaultValue(false), Description("是否显示正常状态下的状态"), Category("状态")]
        public virtual bool ShowNomalState
        {
            get
            {
                if (this.IsPopupItem) return false;
                return m_ShowNomalState;
            }
            set { m_ShowNomalState = value; }
        }

        private bool m_ShowNomalSplitLine = false;
        [Browsable(true), DefaultValue(false), Description("是否显示分隔条"), Category("状态")]
        public virtual bool ShowNomalSplitLine
        {
            get { return m_ShowNomalSplitLine; }
            set { m_ShowNomalSplitLine = value; }
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
        public virtual ArrowStyle eArrowStyle//BH
        {
            get 
            {
                if (this.IsBaseBarItem) return ArrowStyle.eToDown;
                return m_eArrowStyle;
            }
            set { m_eArrowStyle = value; }
        }

        private ArrowDock m_eArrowDock = ArrowDock.eNone;
        [Browsable(true), DefaultValue(typeof(ArrowDock), "eNone"), Description("箭头停靠的位置"), Category("布局")]
        public virtual ArrowDock eArrowDock//BH
        {
            get
            {
                if (this.IsBaseBarItem) return ArrowDock.eRight;
                return m_eArrowDock;
            }
            set { m_eArrowDock = value; }
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

        private int m_DropDownDistance = 15;
        [Browsable(true), DefaultValue(15), Description("弹出区宽度"), Category("布局")]
        public virtual  int DropDownDistance
        {
            get
            {
                if (this.IsPopupItem) return 16; 
                return m_DropDownDistance;
            }
            set
            {
                if (value <= 0) return;
                //
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
        public virtual  Size ArrowSize
        {
            get
            {
                if (this.IsPopupItem) return new Size(3, 4);
                return m_ArrowSize;
            }
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
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
                    return this.DisplayRectangle;
                }
                else
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            return new Rectangle(rectangle.X, rectangle.Y, pContextPopupPanelItem.CheckRegionWidth, pContextPopupPanelItem.CheckRegionWidth);
                        case ContextPopupStyle.eNormal:
                            return new Rectangle(rectangle.X, rectangle.Y, pContextPopupPanelItem.ImageRegionWidth, pContextPopupPanelItem.ImageRegionWidth);
                        case ContextPopupStyle.eSimply:
                        default:
                            return rectangle;
                    }
                }
            }
        }

        [Browsable(false), Description("按钮实体矩形框"), Category("布局")]
        public virtual Rectangle ButtonRectangle//IT + DropDown
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
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
                    //switch (this.eArrowDock)
                    //{
                    //    case ArrowDock.eUp:
                    //        return new Rectangle(rectangle.X, rectangle.Y + this.DropDownDistance + this.Padding.Top, rectangle.Width, rectangle.Height - this.DropDownDistance - this.Padding.Top);
                    //    case ArrowDock.eDown:
                    //        return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height - this.DropDownDistance - this.Padding.Bottom + 1);
                    //    case ArrowDock.eLeft:
                    //        return new Rectangle(rectangle.X + this.DropDownDistance + this.Padding.Left, rectangle.Y, rectangle.Width - this.DropDownDistance - this.Padding.Left, rectangle.Height);
                    //    case ArrowDock.eRight:
                    //        return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - this.DropDownDistance - this.Padding.Right + 1, rectangle.Height);
                    //    default:
                    //        return rectangle;
                    //}
                }
                //
                return rectangle;
            }
        }

        [Browsable(false), Description("弹出区矩形框"), Category("布局")]
        public virtual Rectangle DropDownRectangle//BH
        {
            get
            {
                Rectangle rectangle;
                //
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
                    rectangle = this.ITDrawRectangle;
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
                else
                {
                    rectangle = this.DisplayRectangle;
                    return new Rectangle(rectangle.Right - this.Padding.Right - this.DropDownDistance,
                        rectangle.Y + this.Padding.Top,
                        this.DropDownDistance,
                        this.Height - this.Padding.Top - this.Padding.Bottom);
                }
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
        #endregion

        #region IDismissPopupObject
        [Browsable(false), DefaultValue(typeof(DismissPopupStyle), "eIsDependBasePopup"), Description("解散popup的类型"), Category("状态")]
        public virtual DismissPopupStyle eDismissPopupStyle
        {
            get {
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eLabel:
                        return DismissPopupStyle.eNoDismiss;
                    case ButtonStyle.eDropDownButton:
                        if (this.IsOpened) return DismissPopupStyle.eNoDismiss;
                        return DismissPopupStyle.eIsDependBasePopup;
                    default:
                        return DismissPopupStyle.eIsDependBasePopup;
                }
            }
        }

        [Browsable(false), Description("解散popup的触发区"), Category("属性")]
        public virtual Rectangle DismissTriggerRectangle
        {
            get { return this.ButtonRectangle; }
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
                Rectangle rectangle = this.DisplayRectangle;
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
                return new Rectangle(rectangle.X + this.Padding.Left, rectangle.Y + this.Padding.Top, rectangle.Width - this.Padding.Left - this.Padding.Right, rectangle.Height - this.Padding.Top - this.Padding.Bottom);
            }
        }

        private ImageSizeStyle m_eImageSizeStyle = ImageSizeStyle.eDefault;
        [Browsable(true), DefaultValue(typeof(ImageSizeStyle), "eDefault"), Description("图片尺寸的展现方式"), Category("布局")]
        public override ImageSizeStyle eImageSizeStyle//BH
        {
            get
            {
                if (this.IsBaseBarItem) return ImageSizeStyle.eSystem;
                return m_eImageSizeStyle;
            }
            set { m_eImageSizeStyle = value; }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return !this.IsOpened;
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

        public override Padding Padding
        {
            get
            {
                if (this.IsPopupItem) return new Padding(1);
                return base.Padding;
            }
            set { base.Padding = value; }
        }

        #region Clone
        public override object Clone()
        {
            ButtonItem baseItem = new ButtonItem();
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

        public override Size MeasureSize(Graphics g)//有待完善
        {
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    break;
                case DisplayStyle.eImage:
                    if (this.Image != null)
                    {
                        rectangle = this.ImageRectangle;
                        iWidth += rectangle.Width;
                        iHeight += rectangle.Height;
                    }
                    break;
                case DisplayStyle.eImageAndText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    if (this.Image != null)
                    {
                        switch (this.ImageAlign)
                        {
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopRight:
                                rectangle = this.ImageRectangle;
                                iWidth += rectangle.Width + this.ITSpace;
                                iHeight += rectangle.Height;
                                break;
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.TopCenter:
                                iHeight += this.ImageRectangle.Height + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleRight:
                                iWidth += this.ImageRectangle.Width + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleCenter:
                                rectangle = this.ImageRectangle;
                                if (iWidth < rectangle.Width) iWidth = rectangle.Width;
                                if (iHeight < rectangle.Height) iHeight = rectangle.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                case ButtonStyle.eSplitButton:
                    switch (this.eArrowDock)
                    {
                        case ArrowDock.eUp:
                        case ArrowDock.eDown:
                            iHeight += this.DropDownDistance;
                            break;
                        case ArrowDock.eLeft:
                        case ArrowDock.eRight:
                            iWidth += this.DropDownDistance;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            iWidth += this.Padding.Left + this.Padding.Right;
            iHeight += this.Padding.Top + this.Padding.Bottom;
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                        iHeight = pContextPopupPanelItem.CheckRegionWidth > pContextPopupPanelItem.ImageRegionWidth ? pContextPopupPanelItem.CheckRegionWidth : pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eNormal:
                        iHeight = pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eSimply:
                    default:
                        break;
                }
                iWidth += this.TextLeftSpace + this.TextRightSpace;
            }
            //
            if (iWidth < 16) iWidth = 16;
            if (iHeight < 16) iHeight = 16;
            //
            return new Size(iWidth, iHeight);
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
                        this.m_eSplitState = BaseItemState.ePressed; 
                        this.OnSplitMouseDown(mevent);
                    }
                    else
                    { 
                        this.OnButtonMouseDown(mevent); 
                    }
                    break;
                case ButtonStyle.eButton:
                case ButtonStyle.eCheckButton:
                case ButtonStyle.eDropDownButton:
                default:
                    break;
            }
            base.OnMouseDown(mevent);
            //
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                case ButtonStyle.eSplitButton:
                    if (this.PopupTriggerRectangle.Contains(mevent.Location))
                        this.ShowPopup();
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
                    if (this.DisplayRectangle.Contains(mevent.Location))
                    {
                        this.Checked = !this.Checked;
                    }
                    break;
                case ButtonStyle.eSplitButton:
                    if (this.DisplayRectangle.Contains(mevent.Location))
                    {
                        if (this.SplitRectangle.Contains(mevent.Location))
                        {
                            this.m_eSplitState = BaseItemState.eHot;
                            this.OnSplitMouseUp(mevent);
                        }
                        else
                        {
                            this.OnButtonMouseUp(mevent);
                        }
                    }
                    else
                    {
                        this.m_eSplitState = BaseItemState.eNormal;
                    }
                    break;
                case ButtonStyle.eButton:
                case ButtonStyle.eDropDownButton:
                default:
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
            ////
            //switch (this.eButtonStyle)
            //{
            //    case ButtonStyle.eDropDownButton:
            //        if (this.IsOpened) 
            //        {
            //            base.RelationEvent("MouseClick", e); 
            //            return;
            //        }
            //        break;
            //    default:
            //        if (this.ButtonTriggerRectangle.Contains(e.Location))
            //        {
            //            BasePopup basePopup = this.TryGetDependBasePopup();
            //            if (basePopup != null) basePopup.DismissPopup();
            //        }
            //        break;
            //}
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
                default:
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

        protected override void OnDraw(PaintEventArgs pevent)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderBaseButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eCheckButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCheckButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eDropDownButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDropDownButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eSplitButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSplitButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eLabel:
                default:
                    break;
            }
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle) 
                {
                    case ContextPopupStyle.eSuper:
                    case ContextPopupStyle.eNormal:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderContextPopupItemButtonChecked(
                            new GISShare.Controls.WinForm.CheckedRenderEventArgs(pevent.Graphics, this, this.Enabled, this.ForeColor, this.Checked, this.CheckRectangle));
                        break;
                    default:
                        break;
                }
                //
                //switch (this.eDisplayStyle) 
                //{
                //    case DisplayStyle.eText:
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                //            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                //        break;
                //    default:
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                //            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                //            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                //        break;
                //}
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eImage:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        break;
                    case DisplayStyle.eText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        break;
                }
                //
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eDropDownButton:
                        if (this.HaveVisibleBaseItem)
                        {
                            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                               new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
                        }
                        break;
                    case ButtonStyle.eSplitButton:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                            new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eImage:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        break;
                    case DisplayStyle.eText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
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
                            new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
                        break;
                    default:
                        break;
                }
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

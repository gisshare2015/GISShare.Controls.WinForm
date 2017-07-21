using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.DropDownButtonItemDesigner))]
    public class DropDownButtonItem : BaseButtonItem, IDropDownButtonItem, IPopupOwnerHelper, ICollectionObjectDesignHelper, IPopupObjectDesignHelper
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

        #region 构造函数
        public DropDownButtonItem(ISimplyPopup pSimplyPopup)
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

        public DropDownButtonItem()
            : this(new DropDownPopup()) { }

        public DropDownButtonItem(string strText)
            : this(new DropDownPopup())
        {
            this.Text = strText;
        }

        public DropDownButtonItem(string strName, string strText)
            : this(new DropDownPopup())
        {
            this.Name = strName;
            this.Text = strText;
        }

        public DropDownButtonItem(string strText, Image image)
            : this(new DropDownPopup())
        {
            this.Text = strText;
            this.Image = image;
        }

        public DropDownButtonItem(string strName, string strText, Image image)
            : this(new DropDownPopup())
        {
            this.Name = strName;
            this.Text = strText;
            this.Image = image;
        }

        //public DropDownButtonItem(GISShare.Controls.Plugin.WFNew.IDropDownButtonItemP pBaseItemP)
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
        //}
        #endregion

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
            this.m_DropDownPopup.Show(this.PopupLoction);
            this.RefreshPopupPanel();
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

        #region IDropDownButtonItem
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

        private ArrowDock m_eArrowDock = ArrowDock.eRight;
        [Browsable(true), DefaultValue(typeof(ArrowDock), "eRight"), Description("箭头停靠的位置"), Category("布局")]
        public virtual ArrowDock eArrowDock//BH
        {
            get
            {
                if (this.m_eArrowDock != ArrowDock.eNone && this.IsBaseBarItem) return ArrowDock.eRight;
                return m_eArrowDock;
            }
            set { m_eArrowDock = value; }
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

        private int m_DropDownDistance = 15;
        [Browsable(true), DefaultValue(15), Description("弹出区宽度"), Category("布局")]
        public virtual int DropDownDistance
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
        public virtual Size ArrowSize
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

        [Browsable(false), Description("弹出区矩形框"), Category("布局")]
        public virtual Rectangle DropDownRectangle//BH
        {
            get
            {
                Rectangle rectangle = this.ITDrawRectangle;
                //
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
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
                    return new Rectangle(rectangle.Right, rectangle.Top - this.Padding.Top, this.DropDownDistance + this.Padding.Top, rectangle.Height + this.Padding.Top + this.Padding.Bottom);
                }
            }
        }

        [Browsable(false), Description("按钮有效触发区"), Category("行为")]
        public virtual Rectangle ButtonTriggerRectangle
        {
            get { return this.DisplayRectangle; }
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

        public override Rectangle ITDrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                //
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
        }

        public override Rectangle ButtonRectangle//IT + DropDown
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                //
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
        }

        #region Clone
        public override object Clone()
        {
            DropDownButtonItem baseItem = new DropDownButtonItem();
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

        public override Size MeasureSize(Graphics g)//有待完善
        {
            Size size = base.MeasureSize(g);
            switch (this.eArrowDock)
            {
                case ArrowDock.eUp:
                case ArrowDock.eDown:
                    size.Height += this.DropDownDistance;
                    break;
                case ArrowDock.eLeft:
                case ArrowDock.eRight:
                    size.Width += this.DropDownDistance;
                    break;
                default:
                    break;
            }
            return size;
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

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //
            if (this.IsAutoMouseTrigger) //if (this.IsAutoMouseTrigger && this.IsPopupItem)
            {
                this.ShowPopup();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //
            if (this.IsAutoMouseTrigger && 
                !this.VirtualPopupRectangle.Contains(Form.MousePosition))//if (this.IsAutoMouseTrigger && this.IsPopupItem)
            {
                this.ClosePopup();
            }
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDropDownButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
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
                if (this.HaveVisibleBaseItem)
                {
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                       new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
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
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                    new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
                //if (this.HaveVisibleBaseItem)
                //{
                //    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                //        new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
                //}
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
    }
}
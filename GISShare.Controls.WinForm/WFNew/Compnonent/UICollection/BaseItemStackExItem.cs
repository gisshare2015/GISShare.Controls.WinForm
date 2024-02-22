using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class BaseItemStackExItem : BaseItemStackItem, IBaseItemStackExItem, IBaseItemStackExItemEvent
    {
        private const int TBLR_BUTTONSPACE = 1;
        private int TBLR_BUTTONSIZE = 10;
        public void SET_TBLR_BUTTONSIZE(int i_TBLR_BUTTONSIZE)
        {
            this.TBLR_BUTTONSIZE = i_TBLR_BUTTONSIZE;
        }

        private PreButtonItem m_PreButton;
        private NextButtonItem m_NextButton;

        #region 构造函数
        public BaseItemStackExItem()
        {
            this.m_PreButton = new PreButtonItem(this);
            this.m_NextButton = new NextButtonItem(this);
        }

        //public BaseItemStackExItem(GISShare.Controls.Plugin.WFNew.IBaseItemStackExItemP pBaseItemP)
        //    : this()
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
        //    //IBaseItemStackItemP
        //    this.eOrientation = pBaseItemP.eOrientation;
        //    this.CanExchangeItem = pBaseItemP.CanExchangeItem;
        //    this.ReverseLayout = pBaseItemP.ReverseLayout;
        //    this.IsStretchItems = pBaseItemP.IsStretchItems;
        //    this.IsRestrictItems = pBaseItemP.IsRestrictItems;
        //    this.RestrictItemsWidth = pBaseItemP.RestrictItemsWidth;
        //    this.RestrictItemsHeight = pBaseItemP.RestrictItemsHeight;
        //    this.LineDistance = pBaseItemP.LineDistance;
        //    this.ColumnDistance = pBaseItemP.ColumnDistance;
        //    this.MinSize = pBaseItemP.MinSize;
        //    this.MaxSize = pBaseItemP.MaxSize;
        //    //IBaseItemStackExItemP
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.TopViewItemIndex = pBaseItemP.TopViewItemIndex;
        //    this.PreButtonIncreaseIndex = pBaseItemP.PreButtonIncreaseIndex;
        //}
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "TopViewItemIndexChanged":
                    return this.TopViewItemIndexChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "TopViewItemIndexChanged":
                    if (this.TopViewItemIndexChanged != null) { this.TopViewItemIndexChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }
        
        #region IBaseItemStackExItemEvent
        [Browsable(true), Description("顶部子项索引改变后触发"), Category("属性已更改")]
        public event IntValueChangedHandler TopViewItemIndexChanged;
        #endregion

        #region IBaseItemStackExItem
        #region Radius
        private int m_LeftTopRadius = 3;
        [Browsable(true), DefaultValue(3), Description("左顶部圆角值"), Category("圆角")]
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

        private int m_RightTopRadius = 3;
        [Browsable(true), DefaultValue(3), Description("右顶部圆角值"), Category("圆角")]
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

        private int m_LeftBottomRadius = 3;
        [Browsable(true), DefaultValue(3), Description("左底部圆角值"), Category("圆角")]
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

        private int m_RightBottomRadius = 3;
        [Browsable(true), DefaultValue(3), Description("右底部圆角值"), Category("圆角")]
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

        [Browsable(false), Description("其绘制矩形框"), Category("布局")]
        public virtual Rectangle DrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom);
            }
        }

        private PNLayoutStyle m_ePNLayoutStyle = PNLayoutStyle.eBothEnds;
        [Browsable(true), DefaultValue(typeof(PNLayoutStyle), "eBothEnds"), Description("存在溢出项时PN调节按钮的布局方式"), Category("布局")]
        public virtual PNLayoutStyle ePNLayoutStyle
        {
            get { return m_ePNLayoutStyle; }
            set { m_ePNLayoutStyle = value; }
        }

        [Browsable(false), Description("PreButton是否可见"), Category("状态")]
        public bool PreButtonVisible
        {
            get
            {
                if (this.m_PreButton == null) return false;
                return this.m_PreButton.Visible;
            }
        }

        [Browsable(false), Description("PreButton矩形框"), Category("布局")]
        public Rectangle PreButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                Rectangle rectangleOut;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        switch (this.ePNLayoutStyle)
                        {
                            case PNLayoutStyle.eHead:
                            case PNLayoutStyle.eBothEnds:
                                rectangleOut = new Rectangle(rectangle.X, rectangle.Y, TBLR_BUTTONSIZE, rectangle.Height);
                                break;
                            case PNLayoutStyle.eTail:
                            default:
                                rectangleOut = new Rectangle(rectangle.Right - 2 * TBLR_BUTTONSIZE, rectangle.Y, TBLR_BUTTONSIZE, rectangle.Height);
                                break;
                        }
                        break;
                    case Orientation.Vertical:
                    default:
                        switch (this.ePNLayoutStyle)
                        {
                            case PNLayoutStyle.eHead:
                            case PNLayoutStyle.eBothEnds:
                                rectangleOut = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, TBLR_BUTTONSIZE);
                                break;
                            case PNLayoutStyle.eTail:
                            default:
                                rectangleOut = new Rectangle(rectangle.X, rectangle.Bottom - 2 * TBLR_BUTTONSIZE, rectangle.Width, TBLR_BUTTONSIZE);
                                break;
                        }
                        break;
                }
                return rectangleOut;
            }
        }

        [Browsable(false), Description("NextButton是否可见"), Category("状态")]
        public bool NextButtonVisible
        {
            get
            {
                if (this.m_NextButton == null) return false;
                return this.m_NextButton.Visible;
            }
        }

        [Browsable(false), Description("NextButton矩形框"), Category("布局")]
        public Rectangle NextButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                Rectangle rectangleOut;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        switch (this.ePNLayoutStyle)
                        {
                            case PNLayoutStyle.eHead:
                                rectangleOut = new Rectangle(rectangle.X + TBLR_BUTTONSIZE, rectangle.Y, TBLR_BUTTONSIZE, rectangle.Height);
                                break;
                            case PNLayoutStyle.eTail:
                            case PNLayoutStyle.eBothEnds:
                            default:
                                rectangleOut = new Rectangle(rectangle.Right - TBLR_BUTTONSIZE, rectangle.Y, TBLR_BUTTONSIZE, rectangle.Height);
                                break;
                        }
                        break;
                    case Orientation.Vertical:
                    default:
                        switch (this.ePNLayoutStyle)
                        {
                            case PNLayoutStyle.eHead:
                                rectangleOut = new Rectangle(rectangle.X, rectangle.Y + TBLR_BUTTONSIZE, rectangle.Width, TBLR_BUTTONSIZE);
                                break;
                            case PNLayoutStyle.eTail:
                            case PNLayoutStyle.eBothEnds:
                            default:
                                rectangleOut = new Rectangle(rectangle.X, rectangle.Bottom - TBLR_BUTTONSIZE, rectangle.Width, TBLR_BUTTONSIZE);
                                break;
                        }
                        break;
                }
                return rectangleOut;
            }
        }

        private int m_TopViewItemIndex = -1;
        [Browsable(true), DefaultValue(-1), Description("子项视图索引"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return m_TopViewItemIndex; }
            set
            {
                //if (this.BaseItems.Count == 0) value = -1;
                //if (value < 0 ||
                //    value >= this.BaseItems.Count) value = 0;
                if (this.m_TopViewItemIndex < value) { value = this.GetEnableIndexIncrease(value); }
                else { value = this.GetEnableIndexDecrease(value); }
                //
                if (this.m_TopViewItemIndex == value) return;
                //
                IntValueChangedEventArgs e = new IntValueChangedEventArgs(this.m_TopViewItemIndex, value);
                this.m_TopViewItemIndex = value;
                this.Refresh();
                this.OnTopViewItemIndexChanged(e);
            }
        }
        private int GetEnableIndexIncrease(int index)//先向后寻找匹配项
        {
            if (this.BaseItems.Count == 0) return -1;
            //
            if (index < 0 || index >= this.BaseItems.Count) index = 0;
            //
            if (this.BaseItems[index].Visible) { return index; }
            //
            for (int i = index + 1; i < this.BaseItems.Count; i++)
            {
                if (this.BaseItems[i].Visible) { return i; }
            }
            for (int i = 0; i < index; i++)
            {
                if (this.BaseItems[i].Visible) { return i; }
            }
            //
            return -1;
        }
        private int GetEnableIndexDecrease(int index)//先向前寻找匹配项
        {
            if (this.BaseItems.Count == 0) return -1;
            //
            if (index < 0 || index >= this.BaseItems.Count) index = 0;
            //
            if (this.BaseItems[index].Visible) { return index; }
            //
            for (int i = index - 1; i >= 0; i--)
            {
                if (this.BaseItems[i].Visible) { return i; }
            }
            for (int i = index + 1; i < this.BaseItems.Count; i++)
            {
                if (this.BaseItems[i].Visible) { return i; }
            }
            //
            return -1;
        }

        private bool m_PreButtonIncreaseIndex = true;
        [Browsable(true), DefaultValue(true), Description("PreButton 增加 子项视图索引"), Category("行为")]
        public virtual bool PreButtonIncreaseIndex
        {
            get { return m_PreButtonIncreaseIndex; }
            set { m_PreButtonIncreaseIndex = value; }
        }
        #endregion

        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        if (this.OverflowItemsCount > 0)
                        {
                            switch (this.ePNLayoutStyle)
                            {
                                case PNLayoutStyle.eHead:
                                    return new Rectangle(rectangle.X + 2 * TBLR_BUTTONSIZE + TBLR_BUTTONSPACE,
                                        rectangle.Y,
                                        rectangle.Width - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE,
                                        rectangle.Height);
                                case PNLayoutStyle.eTail:
                                    return new Rectangle(rectangle.X + TBLR_BUTTONSPACE,
                                        rectangle.Y,
                                        rectangle.Width - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE,
                                        rectangle.Height);
                                case PNLayoutStyle.eBothEnds:
                                default:
                                    return new Rectangle(rectangle.X + TBLR_BUTTONSIZE + TBLR_BUTTONSPACE,
                                        rectangle.Y,
                                        rectangle.Width - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE,
                                        rectangle.Height);
                            }
                        }
                        else this.m_TopViewItemIndex = 0;
                        break;
                    case Orientation.Vertical:
                        if (this.OverflowItemsCount > 0)
                        {
                            switch (this.ePNLayoutStyle)
                            {
                                case PNLayoutStyle.eHead:
                                    return new Rectangle(rectangle.X,
                                        rectangle.Y + 2 * TBLR_BUTTONSIZE + TBLR_BUTTONSPACE,
                                        rectangle.Width,
                                        rectangle.Height - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE);
                                case PNLayoutStyle.eTail:
                                    return new Rectangle(rectangle.X,
                                        rectangle.Y + TBLR_BUTTONSPACE,
                                        rectangle.Width,
                                        rectangle.Height - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE);
                                case PNLayoutStyle.eBothEnds:
                                default:
                                    return new Rectangle(rectangle.X,
                                        rectangle.Y + TBLR_BUTTONSIZE + TBLR_BUTTONSPACE,
                                        rectangle.Width,
                                        rectangle.Height - 2 * TBLR_BUTTONSIZE - 2 * TBLR_BUTTONSPACE);
                            }
                        }
                        else this.m_TopViewItemIndex = 0;
                        break;
                }
                return rectangle;
            }
        }

        #region Clone
        public override object Clone()
        {
            BaseItemStackExItem baseItem = new BaseItemStackExItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.Visible = this.Visible;
            //
            baseItem.MinSize = this.MinSize;
            baseItem.MaxSize = this.MaxSize;
            baseItem.IsStretchItems = this.IsStretchItems;
            baseItem.IsRestrictItems = this.IsRestrictItems;
            baseItem.LineDistance = this.LineDistance;
            baseItem.ColumnDistance = this.ColumnDistance;
            baseItem.eOrientation = this.eOrientation;
            //
            //baseItem.ShowNomalOutLineState = this.ShowNomalOutLineState;
            //baseItem.ShowNomalBackgroundState = this.ShowNomalBackgroundState;
            //baseItem.ShowOutLineState = this.ShowOutLineState;
            //baseItem.ShowBackgroundState = this.ShowBackgroundState;
            baseItem.PreButtonIncreaseIndex = this.PreButtonIncreaseIndex;
            //
            baseItem.ShowBackground = this.ShowBackground;
            baseItem.ShowOutLine = this.ShowOutLine;
            foreach (BaseItem one in this.BaseItems)
            {
                baseItem.BaseItems.Add(one.Clone() as BaseItem);
            }
            baseItem.TopViewItemIndex = this.TopViewItemIndex;
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
            if (this.GetEventState("TopViewItemIndexChanged") == EventStateStyle.eUsed) baseItem.TopViewItemIndexChanged += new IntValueChangedHandler(baseItem_TopViewItemIndexChanged);
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
        void baseItem_TopViewItemIndexChanged(object sender, IntValueChangedEventArgs e)
        {
            this.RelationEvent("TopViewItemIndexChanged", e);
        }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_PreButton).SendMessage(messageInfo);
            ((IMessageChain)this.m_NextButton).SendMessage(messageInfo);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderBaseItemStackEx(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
        protected override Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)
        {
            if (this.BaseItems.Count <= 0) return this.DisplayRectangle.Size;
            //
            Rectangle rectangle = this.DisplayRectangle;
            Rectangle itemsRectangle = this.ItemsRectangle;
            //
            Size size = Size.Empty;
            if (this.ReverseLayout)
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_RB(g, this, this.TopViewItemIndex, this.IsStretchItems, this.IsRestrictItems,
                            this.ColumnDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    case Orientation.Vertical:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_RB(g, this, this.TopViewItemIndex, this.IsStretchItems, this.IsRestrictItems,
                            this.LineDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_LT(g, this, this.TopViewItemIndex, this.IsStretchItems, this.IsRestrictItems,
                            this.ColumnDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    case Orientation.Vertical:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_LT(g, this, this.TopViewItemIndex, this.IsStretchItems, this.IsRestrictItems,
                            this.LineDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    default:
                        break;
                }
            }
            //
            if (!bSetSize) return size;
            //
            if (!size.IsEmpty)
            {
                this.Size = new Size(this.LockWith ? size.Width : this.Size.Width, this.LockHeight ? size.Height : this.Size.Height);
            }
            //
            return size;
        }

        //
        protected virtual void OnTopViewItemIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.TopViewItemIndexChanged != null) { this.TopViewItemIndexChanged(this, e); }
        }
    }
}

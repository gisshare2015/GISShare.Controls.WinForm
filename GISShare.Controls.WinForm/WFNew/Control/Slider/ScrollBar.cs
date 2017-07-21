using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), DefaultEvent("ValueChanged")]
    public class ScrollBar : BaseItemControl, IMessagePermeate,
        IScrollBarItem, IScrollBarItemEvent, ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem
    {
        private const int CONST_MINUSPLUSBUTTONSIZE = 16;
        private const int CONST_MINUSSLIDERPLUSSPACE = 1;
        private const int CONST_SLIDERBUTTONMINSIZE = 23;

        ScrollBarButtonItem m_MinusButtonItem;
        ScrollBarButtonItem m_PlusButtonItem;
        ScrollBarButtonItem m_ScrollButtonItem;
        //
        private BaseItemCollection m_BaseItemCollection;

        public ScrollBar()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_MinusButtonItem = new ScrollBarButtonItem(ScrollBarButtonStyle.eMinusButton);
            this.m_BaseItemCollection.Add(this.m_MinusButtonItem);
            this.m_PlusButtonItem = new ScrollBarButtonItem(ScrollBarButtonStyle.ePlusButton);
            this.m_BaseItemCollection.Add(this.m_PlusButtonItem);
            this.m_ScrollButtonItem = new ScrollBarButtonItem(ScrollBarButtonStyle.eScrollButton);
            this.m_BaseItemCollection.Add(this.m_ScrollButtonItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
        }

        public override bool LockWith
        {
            get { return this.eOrientation == Orientation.Vertical; }
        }

        public override bool LockHeight
        {
            get { return this.eOrientation == Orientation.Horizontal; }
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "ValueChanged":
                    return this.ValueChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "ValueChanged":
                    if (this.ValueChanged != null) { this.ValueChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
        bool ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in ((ICollectionItem)this).BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        /// <summary>
        /// 一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况
        /// </summary>
        [Browsable(false), Description("其携带的子项（一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况）"), Category("子项")]
        BaseItemCollection ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem WFNew.ICollectionItem2.GetBaseItem(string strName)
        {
            WFNew.IBaseItem pBaseItem = null;
            foreach (WFNew.IBaseItem one in ((WFNew.ICollectionItem)this).BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem2 pCollectionItem2 = one as WFNew.ICollectionItem2;
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
        IBaseItem WFNew.ICollectionItem3.GetBaseItem2(string strName)
        {
            return null;
        }
        #endregion

        #region IUICollectionItem
        Size IUICollectionItem.GetIdealSize(Graphics g)
        {
            return this.Size;
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ScrollBar baseItem = new ScrollBar();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            //
            baseItem.eOrientation = this.eOrientation;
            baseItem.Step = this.Step;
            baseItem.Value = this.Value;
            baseItem.Minimum = this.Minimum;
            baseItem.Maximum = this.Maximum;
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
            if (this.GetEventState("ValueChanged") == EventStateStyle.eUsed) baseItem.ValueChanged += new IntValueChangedHandler(baseItem_ValueChanged);
            return baseItem;
        }
        void baseItem_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            this.RelationEvent("ValueChanged", e);
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
        #endregion

        #region IScrollBarItemEvent
        public event IntValueChangedHandler ValueChanged;
        #endregion

        #region IScrollBarItem
        System.Windows.Forms.Orientation m_eOrientation = System.Windows.Forms.Orientation.Horizontal;
        [Browsable(true), DefaultValue(typeof(Orientation), "Horizontal"), Description("进度条的布局方式"), Category("布局")]
        public System.Windows.Forms.Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        int m_Step = 1;
        [Browsable(true), DefaultValue(1), Description("增量"), Category("属性")]
        public int Step
        {
            get { return m_Step; }
            set { m_Step = value; }
        }

        int m_Value = 0;
        [Browsable(true), DefaultValue(0), Description("值"), Category("属性")]
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (value < Minimum) value = Minimum;
                else if (value > Maximum) value = Maximum;
                if (m_Value == value) return;
                IntValueChangedEventArgs e = new IntValueChangedEventArgs(m_Value, value);
                m_Value = value;
                this.OnValueChanged(e);
                this.Invalidate(this.ScrollAreaRectangle);
            }
        }

        int m_Minimum = 0;
        [Browsable(true), DefaultValue(0), Description("最小值"), Category("属性")]
        public int Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (value > Maximum) value = Maximum - Step;
                m_Minimum = value;
                if (this.Value < this.Minimum) this.Value = this.Minimum;
            }
        }

        int m_Maximum = 100;
        [Browsable(true), DefaultValue(100), Description("最大值"), Category("属性")]
        public int Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (value < Minimum) value = Minimum + Step;
                m_Maximum = value;
                if (this.Value > this.Maximum) this.Value = this.Maximum;
            }
        }

        [Browsable(false), Description("当前百分比"), Category("属性")]
        public int Percentage
        {
            get
            {
                if (this.Maximum == this.Minimum) return 100;
                return this.Value * 100 / (this.Maximum - this.Minimum);
            }
        }

        [Browsable(false), Description("获取滑动按钮的尺寸"), Category("布局")]
        public int ScrollButtonSize
        {
            get 
            {
                int iSize = this.Maximum - this.Minimum;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        iSize = this.ScrollAreaRectangle.Height - iSize;
                        break;
                    case Orientation.Horizontal:
                    default:
                        iSize = this.ScrollAreaRectangle.Width - iSize;
                        break;
                }
                return iSize > CONST_SLIDERBUTTONMINSIZE ? iSize : CONST_SLIDERBUTTONMINSIZE;
            }
        }

        [Browsable(false), Description("加减按钮的尺寸"), Category("布局")]
        public int MinusPlusButtonSize
        {
            get
            {
                return CONST_MINUSPLUSBUTTONSIZE;
            }
        }

        [Browsable(false), Description("绘制矩形"), Category("布局")]
        public Rectangle DrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left + this.Padding.Left, rectangle.Top + this.Padding.Top, rectangle.Right - this.Padding.Right, rectangle.Bottom - this.Padding.Bottom);
            }
        }

        [Browsable(false), Description("减少按钮矩形"), Category("布局")]
        public Rectangle MinusButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return new Rectangle(rectangle.Left, 
                            rectangle.Top,
                            rectangle.Width,
                            CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return new Rectangle(rectangle.Left,
                            rectangle.Top,
                            CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Height);
                }
            }
        }

        [Browsable(false), Description("增加按钮矩形"), Category("布局")]
        public Rectangle PlusButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return new Rectangle(rectangle.Left,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Width,
                            CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return new Rectangle(rectangle.Right - CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Top,
                            CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Height);
                }
            }
        }

        [Browsable(false), Description("滑动区矩形"), Category("布局")]
        public Rectangle ScrollAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return Rectangle.FromLTRB(
                            rectangle.Left,
                            rectangle.Top + CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Right,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return Rectangle.FromLTRB
                            (
                            rectangle.Left + CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Top,
                            rectangle.Right - CONST_MINUSPLUSBUTTONSIZE,
                            rectangle.Bottom
                            );
                }
            }
        }

        [Browsable(false), Description("有效值滑动区矩形"), Category("布局")]
        public Rectangle ScrollValueAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return Rectangle.FromLTRB(
                            rectangle.Left,
                            rectangle.Top + CONST_MINUSPLUSBUTTONSIZE + CONST_MINUSSLIDERPLUSSPACE + (int)(this.ScrollButtonSize * this.m_ScrollButtonSizeSplitOffset),
                            rectangle.Right,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE - CONST_MINUSSLIDERPLUSSPACE - (int)(this.ScrollButtonSize * (1 - this.m_ScrollButtonSizeSplitOffset)));
                    case Orientation.Horizontal:
                    default:
                        return Rectangle.FromLTRB
                            (
                            rectangle.Left + CONST_MINUSPLUSBUTTONSIZE + CONST_MINUSSLIDERPLUSSPACE + (int)(this.ScrollButtonSize * this.m_ScrollButtonSizeSplitOffset),
                            rectangle.Top,
                            rectangle.Right - CONST_MINUSPLUSBUTTONSIZE - CONST_MINUSSLIDERPLUSSPACE - (int)(this.ScrollButtonSize * (1 - this.m_ScrollButtonSizeSplitOffset)),
                            rectangle.Bottom
                            );
                }
            }
        }

        [Browsable(false), Description("滑动按钮矩形"), Category("布局")]
        public Rectangle ScrollButtonRectangle
        {
            get
            {
                int iOffset = (int)(this.ScrollButtonSize * this.m_ScrollButtonSizeSplitOffset);
                //
                Rectangle rectangle = this.ScrollValueAreaRectangle;
                switch (this.eOrientation) 
                {
                    case Orientation.Vertical:
                        double dH = rectangle.Height;
                        dH = this.Percentage * dH / 100;
                        if (dH >= rectangle.Height)
                        {
                            return new Rectangle(
                                rectangle.Left,
                                rectangle.Bottom - iOffset,
                                rectangle.Width,
                                this.ScrollButtonSize);
                        }
                        else if (dH <= 0)
                        {
                            return new Rectangle(
                                rectangle.Left,
                                rectangle.Top - iOffset,
                                rectangle.Width,
                                this.ScrollButtonSize);
                        }
                        else
                        {
                            return new Rectangle(
                                rectangle.Left,
                                rectangle.Top + (int)dH - iOffset,
                                rectangle.Width,
                                this.ScrollButtonSize);
                        } 
                    case Orientation.Horizontal:
                    default:
                        double dW = rectangle.Width;
                        dW = this.Percentage * dW / 100;
                        if (dW >= rectangle.Width)
                        {
                            return new Rectangle(
                                rectangle.Right - iOffset,
                                rectangle.Top,
                                this.ScrollButtonSize,
                                rectangle.Height);
                        }
                        else if (dW <= 0)
                        {
                            return new Rectangle(
                                rectangle.Left - iOffset,
                                rectangle.Top,
                                this.ScrollButtonSize,
                                rectangle.Height);
                        }
                        else
                        {
                            return new Rectangle(
                                  rectangle.Left + (int)dW - iOffset,
                                  rectangle.Top,
                                  this.ScrollButtonSize,
                                  rectangle.Height);
                        }
                }
            }
        }

        public int GetEffectiveValue()
        {
            if (this.Maximum <= this.Minimum) return 0;
            if (this.Value < this.Minimum) return this.Minimum;
            if (this.Value > this.Maximum) return this.Maximum;
            return this.Value;
        }

        public int GetValueFromPoint(Point point)
        {
            Rectangle rectangle = this.ScrollValueAreaRectangle;
            switch (this.eOrientation)
            {
                case Orientation.Vertical:
                    if (point.Y == rectangle.Top) return this.Minimum;
                    double dV = (double)(this.Maximum - this.Minimum) / rectangle.Height * (point.Y - rectangle.Top);
                    if (dV < this.Minimum) return this.Minimum;
                    else if (dV > this.Maximum) return this.Maximum;
                    else return (int)dV;
                case Orientation.Horizontal:
                    if (point.X == rectangle.Left) return this.Minimum;
                    double dV2 = (double)(this.Maximum - this.Minimum) / rectangle.Width * (point.X - rectangle.Left);
                    if (dV2 < this.Minimum) return this.Minimum;
                    else if (dV2 > this.Maximum) return this.Maximum;
                    else return (int)dV2;
                default:
                    return this.Minimum;
            }
        }
        #endregion

        #region IMessagePermeate
        bool IMessagePermeate.PermeateCancelEvent(MessageStyle eMessageStyle)
        {
            return eMessageStyle == MessageStyle.eMSMouseDown ||
                eMessageStyle == MessageStyle.eMSMouseMove ||
                eMessageStyle == MessageStyle.eMSMouseUp;
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            switch(messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSPaint:
                    this.OnDraw(messageInfo.MessageParameter as PaintEventArgs);
                    break;
            }
            //
            if (this.m_MinusButtonItem.Visible) ((IMessageChain)this.m_MinusButtonItem).SendMessage(messageInfo);
            if (this.m_PlusButtonItem.Visible) ((IMessageChain)this.m_PlusButtonItem).SendMessage(messageInfo);
            if (this.m_ScrollButtonItem.Visible) ((IMessageChain)this.m_ScrollButtonItem).SendMessage(messageInfo);
        }

        protected virtual void OnDraw(PaintEventArgs pevent)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderScrollBar(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));//
        }

        double m_ScrollButtonSizeSplitOffset = 0.5;//记录按下点在滑动按钮的分割因子
        bool m_IsMouseDown = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.ScrollAreaRectangle.Contains(e.Location)) 
            {
                this.m_IsMouseDown = this.ScrollButtonRectangle.Contains(e.Location);
                //
                if (this.m_IsMouseDown)
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Vertical:
                            this.m_ScrollButtonSizeSplitOffset = (double)(e.Location.Y - this.ScrollButtonRectangle.Top) / this.ScrollButtonSize;
                            break;
                        case Orientation.Horizontal:
                            this.m_ScrollButtonSizeSplitOffset = (double)(e.Location.X - this.ScrollButtonRectangle.Left) / this.ScrollButtonSize;
                            break;
                        default:
                            m_ScrollButtonSizeSplitOffset = 0.5;
                            break;
                    }
                }
                else
                {
                    this.Value = this.GetValueFromPoint(e.Location);
                }
            }
            //
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.m_IsMouseDown)
            {
                this.Value = this.GetValueFromPoint(e.Location);
            }
            //
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            this.m_IsMouseDown = false;
        }

        //
        protected virtual void OnValueChanged(IntValueChangedEventArgs e)
        {
            if (this.ValueChanged != null) { this.ValueChanged(this, e); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("ValueChanged")]
    public class SliderItem : BaseItem,
        IMessagePermeate,
        ISliderItem, 
        ISliderItemEvent, ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem
    {
        private const int CONST_MINUSPLUSBUTTONSIZE = 16;
        private const int CONST_MINUSSLIDERPLUSSPACE = 2;
        private const int CONST_SLIDERBUTTONWIDTH_D = 10;
        private const int CONST_SLIDERBUTTONHEIGHT_L = 16;

        SliderButtonItem m_MinusButtonItem;
        SliderButtonItem m_PlusButtonItem;
        SliderButtonItem m_SliderButtonItem;
        private BaseItemCollection m_BaseItemCollection;

        #region 构造函数
        public SliderItem()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_MinusButtonItem = new SliderButtonItem(SliderButtonStyle.eMinusButton);
            this.m_BaseItemCollection.Add(this.m_MinusButtonItem);
            this.m_PlusButtonItem = new SliderButtonItem(SliderButtonStyle.ePlusButton);
            this.m_BaseItemCollection.Add(this.m_PlusButtonItem);
            this.m_SliderButtonItem = new SliderButtonItem(SliderButtonStyle.eSliderButton);
            this.m_BaseItemCollection.Add(this.m_SliderButtonItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
        }

        //public SliderItem(GISShare.Controls.Plugin.WFNew.ISliderItemP pBaseItemP)
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
        //    //ISliderItemP
        //    this.eOrientation = pBaseItemP.eOrientation;
        //    this.Value = pBaseItemP.Value;
        //    this.Maximum = pBaseItemP.Maximum;
        //    this.Minimum = pBaseItemP.Minimum;
        //    this.Step = pBaseItemP.Step;
        //}
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override bool LockWith
        {
            get { return this.eOrientation == Orientation.Vertical; }
        }

        public override bool LockHeight
        {
            get { return this.eOrientation == Orientation.Horizontal; }
        }

        public override Size MeasureSize(Graphics g)
        {
            return this.DisplayRectangle.Size;
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
                    if (this.ValueChanged != null) { this.ValueChanged(this, e as DoubleValueChangedEventArgs); }
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
            SliderItem baseItem = new SliderItem();
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
            if (this.GetEventState("ValueChanged") == EventStateStyle.eUsed) baseItem.ValueChanged += new DoubleValueChangedHandler(baseItem_ValueChanged);
            return baseItem;
        }
        void baseItem_ValueChanged(object sender, DoubleValueChangedEventArgs e)
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

        #region ISliderItemEvent
        public event DoubleValueChangedHandler ValueChanged;
        #endregion

        #region ISliderItem
        System.Windows.Forms.Orientation m_eOrientation = System.Windows.Forms.Orientation.Horizontal;
        [Browsable(true), DefaultValue(typeof(Orientation), "Horizontal"), Description("进度条的布局方式"), Category("布局")]
        public System.Windows.Forms.Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        double m_Step = 1;
        [Browsable(true), DefaultValue(1), Description("增量"), Category("属性")]
        public double Step
        {
            get { return m_Step; }
            set { m_Step = value; }
        }

        double m_Value = 0;
        [Browsable(true), DefaultValue(0), Description("值"), Category("属性")]
        public double Value
        {
            get { return m_Value; }
            set
            {
                if (value < Minimum) value = Minimum;
                else if (value > Maximum) value = Maximum;
                if (m_Value == value) return;
                DoubleValueChangedEventArgs e = new DoubleValueChangedEventArgs(m_Value, value);
                m_Value = value;
                this.OnValueChanged(e);
                this.Invalidate(this.SliderAreaRectangle);
            }
        }

        double m_Minimum = 0;
        [Browsable(true), DefaultValue(0), Description("最小值"), Category("属性")]
        public double Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (value > Maximum) value = Maximum - Step;
                m_Minimum = value;
            }
        }

        double m_Maximum = 100;
        [Browsable(true), DefaultValue(100), Description("最大值"), Category("属性")]
        public double Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (value < Minimum) value = Minimum + Step;
                m_Maximum = value;
            }
        }

        [Browsable(false), Description("当前百分比"), Category("属性")]
        public double Percentage
        {
            get
            {
                if (this.Maximum == this.Minimum) return 1;
                return this.Value / (this.Maximum - this.Minimum);
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
                        return new Rectangle((rectangle.Left + rectangle.Right - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Top,
                            CONST_MINUSPLUSBUTTONSIZE,
                            CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return new Rectangle(rectangle.Left,
                            (rectangle.Top + rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            CONST_MINUSPLUSBUTTONSIZE,
                            CONST_MINUSPLUSBUTTONSIZE);
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
                        return new Rectangle((rectangle.Left + rectangle.Right - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE,
                            CONST_MINUSPLUSBUTTONSIZE,
                            CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return new Rectangle(rectangle.Right - CONST_MINUSPLUSBUTTONSIZE,
                            (rectangle.Top + rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            CONST_MINUSPLUSBUTTONSIZE,
                            CONST_MINUSPLUSBUTTONSIZE);
                }
            }
        }

        [Browsable(false), Description("滑动区矩形"), Category("布局")]
        public Rectangle SliderAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return Rectangle.FromLTRB(
                            (rectangle.Left + rectangle.Right - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Top + CONST_MINUSPLUSBUTTONSIZE,
                            (rectangle.Left + rectangle.Right + CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE);
                    case Orientation.Horizontal:
                    default:
                        return Rectangle.FromLTRB
                            (
                            rectangle.Left + CONST_MINUSPLUSBUTTONSIZE,
                            (rectangle.Top + rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Right - CONST_MINUSPLUSBUTTONSIZE,
                            (rectangle.Top + rectangle.Bottom + CONST_MINUSPLUSBUTTONSIZE) / 2
                            );
                }
            }
        }

        [Browsable(false), Description("有效值滑动区矩形"), Category("布局")]
        public Rectangle SliderValueAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return Rectangle.FromLTRB(
                            (rectangle.Left + rectangle.Right - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Top + CONST_MINUSPLUSBUTTONSIZE + CONST_MINUSSLIDERPLUSSPACE + CONST_SLIDERBUTTONWIDTH_D / 2,
                            (rectangle.Left + rectangle.Right + CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE - CONST_MINUSSLIDERPLUSSPACE - CONST_SLIDERBUTTONWIDTH_D / 2);
                    case Orientation.Horizontal:
                    default:
                        return Rectangle.FromLTRB
                            (
                            rectangle.Left + CONST_MINUSPLUSBUTTONSIZE + CONST_MINUSSLIDERPLUSSPACE + CONST_SLIDERBUTTONWIDTH_D / 2,
                            (rectangle.Top + rectangle.Bottom - CONST_MINUSPLUSBUTTONSIZE) / 2,
                            rectangle.Right - CONST_MINUSPLUSBUTTONSIZE - CONST_MINUSSLIDERPLUSSPACE - CONST_SLIDERBUTTONWIDTH_D / 2,
                            (rectangle.Top + rectangle.Bottom + CONST_MINUSPLUSBUTTONSIZE) / 2
                            );
                }
            }
        }

        [Browsable(false), Description("滑动按钮矩形"), Category("布局")]
        public Rectangle SliderButtonRectangle
        {
            get
            {
                int iOffset = CONST_SLIDERBUTTONWIDTH_D / 2;
                //
                Rectangle rectangle = this.SliderValueAreaRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        double dH = rectangle.Height;
                        dH = this.Percentage * dH;
                        if (dH >= rectangle.Height)
                        {
                            return new Rectangle(
                                (rectangle.Left + rectangle.Right - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                rectangle.Bottom - iOffset,
                                CONST_SLIDERBUTTONHEIGHT_L,
                                CONST_SLIDERBUTTONWIDTH_D);
                        }
                        else if (dH <= 0)
                        {
                            return new Rectangle(
                                (rectangle.Left + rectangle.Right - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                rectangle.Top - iOffset,
                                CONST_SLIDERBUTTONHEIGHT_L,
                                CONST_SLIDERBUTTONWIDTH_D);
                        }
                        else
                        {
                            return new Rectangle(
                                (rectangle.Left + rectangle.Right - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                rectangle.Top + (int)dH - iOffset,
                                CONST_SLIDERBUTTONHEIGHT_L,
                                CONST_SLIDERBUTTONWIDTH_D);
                        }
                    case Orientation.Horizontal:
                    default:
                        double dW = rectangle.Width;
                        dW = this.Percentage * dW;
                        if (dW >= rectangle.Width)
                        {
                            return new Rectangle(
                                rectangle.Right - iOffset,
                                (rectangle.Top + rectangle.Bottom - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                CONST_SLIDERBUTTONWIDTH_D,
                                CONST_SLIDERBUTTONHEIGHT_L);
                        }
                        else if (dW <= 0)
                        {
                            return new Rectangle(
                                rectangle.Left - iOffset,
                                (rectangle.Top + rectangle.Bottom - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                CONST_SLIDERBUTTONWIDTH_D,
                                CONST_SLIDERBUTTONHEIGHT_L);
                        }
                        else
                        {
                            return new Rectangle(
                                  rectangle.Left + (int)dW - iOffset,
                                  (rectangle.Top + rectangle.Bottom - CONST_SLIDERBUTTONHEIGHT_L) / 2,
                                  CONST_SLIDERBUTTONWIDTH_D,
                                  CONST_SLIDERBUTTONHEIGHT_L);
                        }
                }
            }
        }

        public double GetValueFromPoint(Point point)
        {
            Rectangle rectangle = this.SliderValueAreaRectangle;
            switch (this.eOrientation)
            {
                case Orientation.Vertical:
                    if (point.Y == rectangle.Top) return this.Minimum;
                    double dV = (this.Maximum - this.Minimum) / rectangle.Height * (point.Y - rectangle.Top);
                    if (dV < this.Minimum) return this.Minimum;
                    else if (dV > this.Maximum) return this.Maximum;
                    else return dV;
                case Orientation.Horizontal:
                    if (point.X == rectangle.Left) return this.Minimum;
                    double dV2 = (this.Maximum - this.Minimum) / rectangle.Width * (point.X - rectangle.Left);
                    if (dV2 < this.Minimum) return this.Minimum;
                    else if (dV2 > this.Maximum) return this.Maximum;
                    else return dV2;
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

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            if (this.m_MinusButtonItem.Visible) ((IMessageChain)this.m_MinusButtonItem).SendMessage(messageInfo);
            if (this.m_PlusButtonItem.Visible) ((IMessageChain)this.m_PlusButtonItem).SendMessage(messageInfo);
            if (this.m_SliderButtonItem.Visible) ((IMessageChain)this.m_SliderButtonItem).SendMessage(messageInfo);
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSlider(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));//
        }

        bool m_IsMouseDown = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.SliderAreaRectangle.Contains(e.Location))
            {
                this.Value = this.GetValueFromPoint(e.Location);
                //
                this.m_IsMouseDown = this.SliderButtonRectangle.Contains(e.Location);
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
        protected virtual void OnValueChanged(DoubleValueChangedEventArgs e)
        {
            if (this.ValueChanged != null) { this.ValueChanged(this, e); }
        }
    }
}

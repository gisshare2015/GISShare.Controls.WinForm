using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class ScrollBarButtonItem : BaseButtonItem, IScrollBarButton, IBaseItemProperty
    {
        private readonly int READONLY_TimerInterval;
        private System.Windows.Forms.Timer m_Timer;

        ScrollBarButtonStyle m_eScrollBarButtonStyle = ScrollBarButtonStyle.eScrollButton;
        public ScrollBarButtonStyle eScrollBarButtonStyle
        {
            get { return m_eScrollBarButtonStyle; }
        }

        public  System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
                if (pScrollBarItem == null) return System.Windows.Forms.Orientation.Horizontal;
                return pScrollBarItem.eOrientation;
            }
        }

        public ScrollBarButtonItem(ScrollBarButtonStyle scrollBarButtonStyle)
        {
            READONLY_TimerInterval = (int)(1.5 * System.Windows.Forms.SystemInformation.DoubleClickTime);
            //
            base.Name = "GISShare.Controls.WinForm.WFNew.ScrollBarButtonItem";
            base.Text = "滚动条按钮[减少//滚动//增加]";
            base.LeftTopRadius = 3;
            base.RightTopRadius = 3;
            base.LeftBottomRadius = 3;
            base.RightBottomRadius = 3;
            base.eDisplayStyle = DisplayStyle.eNone;
            base.AutoGetFocus = true;
            //
            this.m_eScrollBarButtonStyle = scrollBarButtonStyle;
            //
            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = READONLY_TimerInterval;
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (this.m_Timer.Interval > 80) this.m_Timer.Interval = 80;
            //
            IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
            if (pScrollBarItem == null) return;
            switch (this.eScrollBarButtonStyle)
            {
                case ScrollBarButtonStyle.eMinusButton:
                    if (pScrollBarItem.Value > pScrollBarItem.Minimum)
                    {
                        pScrollBarItem.Value -= pScrollBarItem.Step;
                    }
                    else
                    {
                        this.TimerStop();
                    }
                    break;
                case ScrollBarButtonStyle.ePlusButton:
                    if (pScrollBarItem.Value < pScrollBarItem.Maximum)
                    {
                        pScrollBarItem.Value += pScrollBarItem.Step;
                    }
                    else
                    {
                        this.TimerStop();
                    }
                    break;
            }
        }
        private void TimerStart()
        {
            this.m_Timer.Start();
        }
        private void TimerStop()
        {
            this.m_Timer.Stop();
            this.m_Timer.Interval = READONLY_TimerInterval;
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
            get { return this.pOwner as IBaseItem3; }
        }
        #endregion

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                return DismissPopupStyle.eNoDismiss;
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
                if (pScrollBarItem == null) return base.ShowNomalState;
                return (pScrollBarItem.eBaseItemState == BaseItemState.eHot || pScrollBarItem.eBaseItemState == BaseItemState.ePressed) ? true : base.ShowNomalState;
            }
            set
            {
                base.ShowNomalState = value;
            }
        }

        public override bool Visible
        {
            get
            {
                IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
                if (pScrollBarItem == null) return base.Visible;
                switch (this.eScrollBarButtonStyle)
                {
                    case ScrollBarButtonStyle.eScrollButton:
                        return pScrollBarItem.ScrollButtonSize < (pScrollBarItem.eOrientation == System.Windows.Forms.Orientation.Horizontal ? pScrollBarItem.ScrollAreaRectangle.Width : pScrollBarItem.ScrollAreaRectangle.Height);
                    case ScrollBarButtonStyle.eMinusButton:
                    case ScrollBarButtonStyle.ePlusButton:
                        return 2 * pScrollBarItem.MinusPlusButtonSize < (pScrollBarItem.eOrientation == System.Windows.Forms.Orientation.Horizontal ? pScrollBarItem.DrawRectangle.Width : pScrollBarItem.DrawRectangle.Height);
                    default:
                        return base.Visible;
                }
            }
            set
            {
                base.Visible = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
                if (pScrollBarItem == null) return base.Enabled;
                return pScrollBarItem.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                return this.Enabled ? base.eBaseItemState : BaseItemState.eDisabled;
            }
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
                if(pScrollBarItem == null) return base.DisplayRectangle;
                switch (this.eScrollBarButtonStyle) 
                {
                    case ScrollBarButtonStyle.eMinusButton:
                        return pScrollBarItem.MinusButtonRectangle;
                    case ScrollBarButtonStyle.ePlusButton:
                        return pScrollBarItem.PlusButtonRectangle;
                    case ScrollBarButtonStyle.eScrollButton:
                        return pScrollBarItem.ScrollButtonRectangle;
                    default:
                        return base.DisplayRectangle;
                }
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
            if (pScrollBarItem == null) return;
            switch (this.eScrollBarButtonStyle)
            {
                case ScrollBarButtonStyle.eMinusButton:
                    pScrollBarItem.Value -= pScrollBarItem.Step;
                    this.TimerStart();
                    break;
                case ScrollBarButtonStyle.ePlusButton:
                    pScrollBarItem.Value += pScrollBarItem.Step;
                    this.TimerStart();
                    break;
            }
            //base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            IScrollBarItem pScrollBarItem = this.pOwner as IScrollBarItem;
            if (pScrollBarItem == null) return;
            switch (this.eScrollBarButtonStyle)
            {
                case ScrollBarButtonStyle.eMinusButton:
                case ScrollBarButtonStyle.ePlusButton:
                    this.TimerStop();
                    break;
                default:
                    break;
            }
            //base.OnMouseUp(e);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs pevent)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderScrollBarButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));//
        }
    }
}


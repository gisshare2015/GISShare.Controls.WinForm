using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class SliderButtonItem : BaseButtonItem, ISliderButtonItem, IBaseItemProperty
    {
        private readonly int READONLY_TimerInterval;
        private System.Windows.Forms.Timer m_Timer;

        SliderButtonStyle m_eSliderButtonStyle = SliderButtonStyle.eSliderButton;
        public SliderButtonStyle eSliderButtonStyle
        {
            get { return m_eSliderButtonStyle; }
        }

        public System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                ISliderItem pSliderItem = this.pOwner as ISliderItem;
                if (pSliderItem == null) return System.Windows.Forms.Orientation.Horizontal;
                return pSliderItem.eOrientation;
            }
        }

        public SliderButtonItem(SliderButtonStyle sliderButtonStyle)
        {
            READONLY_TimerInterval = (int)(1.5 * System.Windows.Forms.SystemInformation.DoubleClickTime);
            //
            base.Name = "GISShare.Controls.WinForm.WFNew.SliderButtonItem";
            base.Text = "滑动条按钮[减少//滑动//增加]";
            base.ShowNomalState = true;
            base.eDisplayStyle = DisplayStyle.eNone;
            //
            this.m_eSliderButtonStyle = sliderButtonStyle;
            switch(this.eSliderButtonStyle)
            {
                case SliderButtonStyle.eMinusButton:
                case SliderButtonStyle.ePlusButton:
                    base.LeftBottomRadius = 14;
                    base.LeftTopRadius = 14;
                    base.RightBottomRadius = 14;
                    base.RightTopRadius = 14;
                    break;
            }
            //
            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = READONLY_TimerInterval;
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (this.m_Timer.Interval > 80) this.m_Timer.Interval = 80;
            //
            ISliderItem pSliderItem = this.pOwner as ISliderItem;
            if (pSliderItem == null) return;
            switch (this.eSliderButtonStyle)
            {
                case SliderButtonStyle.eMinusButton:
                    if (pSliderItem.Value > pSliderItem.Minimum)
                    {
                        pSliderItem.Value -= pSliderItem.Step;
                    }
                    else
                    {
                        this.TimerStop();
                    }
                    break;
                case SliderButtonStyle.ePlusButton:
                    if (pSliderItem.Value < pSliderItem.Maximum)
                    {
                        pSliderItem.Value += pSliderItem.Step;
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

        public override bool Enabled
        {
            get
            {
                ISliderItem pSliderItem = this.pOwner as ISliderItem;
                if (pSliderItem == null) return base.Enabled;
                return pSliderItem.Enabled;
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
                ISliderItem pSliderItem = this.pOwner as ISliderItem;
                if(pSliderItem == null) return base.DisplayRectangle;
                switch (this.eSliderButtonStyle) 
                {
                    case SliderButtonStyle.eMinusButton:
                        return pSliderItem.MinusButtonRectangle;
                    case SliderButtonStyle.ePlusButton:
                        return pSliderItem.PlusButtonRectangle;
                    case SliderButtonStyle.eSliderButton:
                        return pSliderItem.SliderButtonRectangle;
                    default:
                        return base.DisplayRectangle;
                }
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            ISliderItem pSliderItem = this.pOwner as ISliderItem;
            if (pSliderItem == null) return;
            switch (this.eSliderButtonStyle)
            {
                case SliderButtonStyle.eMinusButton:
                    pSliderItem.Value -= pSliderItem.Step;
                    this.TimerStart();
                    break;
                case SliderButtonStyle.ePlusButton:
                    pSliderItem.Value += pSliderItem.Step;
                    this.TimerStart();
                    break;
            }
            //base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            ISliderItem pSliderItem = this.pOwner as ISliderItem;
            if (pSliderItem == null) return;
            switch (this.eSliderButtonStyle)
            {
                case SliderButtonStyle.eMinusButton:
                case SliderButtonStyle.ePlusButton:
                    this.TimerStop();
                    break;
                default:
                    break;
            }
            //base.OnMouseUp(e);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs pevent)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSliderButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));//
        }
    }
}

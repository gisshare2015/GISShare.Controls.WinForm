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
    class NextButtonTBCItem : LTBRButtonItem, IBaseItemProperty
    {
        private readonly int READONLY_TimerInterval;
        private System.Windows.Forms.Timer m_Timer;

        private ITabButtonContainerItem owner = null;

        public bool AutoIncreaseIndex
        {
            get { return !this.owner.PreButtonIncreaseIndex; }
        }

        public NextButtonTBCItem(ITabButtonContainerItem pTabButtonContainerItem)
            : base(LTBRButtonStyle.eBottomButton)
        {
            READONLY_TimerInterval = (int)(1.5 * System.Windows.Forms.SystemInformation.DoubleClickTime);
            //
            base.Name = "GISShare.Controls.WinForm.WFNew.NextButtonTBCItem";
            base.Text = "下一个表按钮";
            //
            this.owner = pTabButtonContainerItem;
            ((ISetOwnerHelper)this).SetOwner(owner as IOwner);
            //
            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = READONLY_TimerInterval;
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (this.m_Timer.Interval > 150) this.m_Timer.Interval = 150;
            //
            if (this.Enabled)
            {
                if (AutoIncreaseIndex)
                {
                    this.owner.TopViewItemIndex++;
                }
                else
                {
                    this.owner.TopViewItemIndex--;
                }
            }
            else
            {
                this.TimerStop();
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
            get { return this.owner; }
        }
        #endregion

        public override LTBRButtonStyle eLTBRButtonStyle
        {
            get
            {
                switch (this.owner.eOrientation)
                {
                    case Orientation.Horizontal:
                        return LTBRButtonStyle.eRightButton;
                    case Orientation.Vertical:
                    default:
                        return LTBRButtonStyle.eBottomButton;
                }
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.NextButtonRectangle;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.owner.DrawRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override bool Enabled
        {
            get
            {
                if (!this.owner.Enabled) return false;
                //
                if (this.AutoIncreaseIndex)
                {
                    base.Enabled = this.owner.TopViewItemIndex - this.owner.BaseItems.GetItemCount(false, 0, this.owner.TopViewItemIndex) < this.owner.OverflowItemsCount;
                }
                else
                {
                    base.Enabled = this.owner.TopViewItemIndex - this.owner.BaseItems.GetItemCount(false, 0, this.owner.TopViewItemIndex) > 0;
                }
                //
                return base.Enabled;
            }
        }

        public override bool Visible
        {
            get
            {
                base.Visible = this.owner.eTabButtonContainerStyle == TabButtonContainerStyle.ePreButtonAndNextButton && this.owner.OverflowItemsCount > 0;
                //
                return base.Visible;
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                switch (this.eLTBRButtonStyle)
                {
                    case LTBRButtonStyle.eTopButton:
                    case LTBRButtonStyle.eLeftButton:
                        return this.owner.LeftTopRadius;
                    default:
                        return 0;
                }
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                switch (this.eLTBRButtonStyle)
                {
                    case LTBRButtonStyle.eTopButton:
                    case LTBRButtonStyle.eRightButton:
                        return this.owner.RightTopRadius;
                    default:
                        return 0;
                }
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                switch (this.eLTBRButtonStyle)
                {
                    case LTBRButtonStyle.eLeftButton:
                    case LTBRButtonStyle.eBottomButton:
                        return this.owner.LeftBottomRadius;
                    default:
                        return 0;
                }
            }
            set { }
        }

        public override int RightBottomRadius
        {
            get
            {
                switch (this.eLTBRButtonStyle)
                {
                    case LTBRButtonStyle.eRightButton:
                    case LTBRButtonStyle.eBottomButton:
                        return this.owner.RightBottomRadius;
                    default:
                        return 0;
                }
            }
            set { }
        }

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);
        //    if (AutoIncreaseIndex)
        //    {
        //        this.owner.TopViewItemIndex++;
        //    }
        //    else
        //    {
        //        this.owner.TopViewItemIndex--;
        //    }
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (AutoIncreaseIndex)
            {
                this.owner.TopViewItemIndex++;
            }
            else
            {
                this.owner.TopViewItemIndex--;
            }
            this.TimerStart();
            ////
            //base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.TimerStop();
            //base.OnMouseUp(e);
        }
    }
}

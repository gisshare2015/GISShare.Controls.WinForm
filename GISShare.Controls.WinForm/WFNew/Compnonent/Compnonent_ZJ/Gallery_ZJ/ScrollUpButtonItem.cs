using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    internal class ScrollUpButtonItem : GalleryScrollButtonItem, IBaseItemProperty
    {
        private readonly int READONLY_TimerInterval;
        private System.Windows.Forms.Timer m_Timer;

        private IGalleryItem owner = null;

        public ScrollUpButtonItem(IGalleryItem pGalleryItem)
            : base(GalleryScrollButtonStyle.eScrollUpButton)
        {
            READONLY_TimerInterval = (int)(1.5 * System.Windows.Forms.SystemInformation.DoubleClickTime);
            //
            base.Name = "GISShare.Controls.WinForm.WFNew.ScrollUpButtonItem";
            base.Text = "向上翻动";
            //
            this.owner = pGalleryItem;
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
                if (UpButtonIncreaseIndex)
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

        public bool UpButtonIncreaseIndex
        {
            get { return this.owner.UpButtonIncreaseIndex; }
        }

        public override bool Enabled
        {
            get
            {
                if (!this.owner.Enabled || this.owner.IsOpened) return false;
                //
                if (this.UpButtonIncreaseIndex)
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
            set
            {
                base.Enabled = value;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.owner.ScrollRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                return 0;
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                return this.owner.RightTopRadius;
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                return 0;
            }
            set { }
        }

        public override int RightBottomRadius
        {
            get
            {
                return 0;
            }
            set { }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.ScrollUpButtonRectangle;
            }
        }

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    //base.OnMouseClick(e);
        //    this.RelationEvent("MouseClick", e);
        //    if (this.UpButtonIncreaseIndex)
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
            if (UpButtonIncreaseIndex)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true)]
    public class PanelControl : AreaControlCC,
        ICollectionItem, 
        IScrollableObjectHelper
    {
        private const int CONST_SCROLBARBARSIZE = 13;
        //
        private VScrollBarItem m_VScrollBarItem;
        private HScrollBarItem m_HScrollBarItem;
        private BaseItemCollection m_BaseItemCollection;

        public PanelControl()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_VScrollBarItem = new VScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_VScrollBarItem);
            this.m_HScrollBarItem = new HScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_HScrollBarItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);
            //
            this.m_HScrollBarItem.ValueChanged += new IntValueChangedHandler(m_HScrollBarItem_ValueChanged);
        }

        void m_HScrollBarItem_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            this.HorizontalScroll.Value = this.m_HScrollBarItem.Value;
            Console.WriteLine(this.m_HScrollBarItem.Value);
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

        #region IScrollableObjectHelper
        [Browsable(false), Description("水平滚动条最小值"), Category("外观")]
        public int HScrollBarMinimum
        {
            get { return 0; }
        }

        int m_HScrollBarMaximum = 0;
        [Browsable(false), Description("水平滚动条最大值"), Category("外观")]
        public int HScrollBarMaximum
        {
            get { return this.m_HScrollBarMaximum; }
        }

        private bool m_HScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("水平滚动条是否可见"), Category("外观")]
        public bool HScrollBarVisible
        {
            get
            {
                return this.AutoScroll && this.m_HScrollBarVisible;
            }
        }

        [Browsable(false), Description("水平滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.HScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.FrameRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Bottom
                    );
            }
        }

        //

        [Browsable(false), Description("竖直滚动条最小值"), Category("外观")]
        public int VScrollBarMinimum
        {
            get { return 0; }
        }

        private int m_VScrollBarMaximum = 0;
        [Browsable(false), Description("竖直滚动条最大值"), Category("外观")]
        public int VScrollBarMaximum
        {
            get { return this.m_VScrollBarMaximum; }
        }

        private bool m_VScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("竖直滚动条是否可见"), Category("外观")]
        public bool VScrollBarVisible
        {
            get
            {
                return this.AutoScroll && this.m_VScrollBarVisible;
            }
        }

        [Browsable(false), Description("竖直滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.VScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.FrameRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Top,
                    rectangle.Right,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0)
                    );
            }
        }

        //

        void IScrollableObjectHelper.ScrollValueRefresh()
        {
            this.Invalidate(this.DisplayRectangle);
        }
        #endregion

        private bool m_AutoScroll = false;
        [Browsable(true), Description("自动开启滚动条"), Category("外观")]
        public new bool AutoScroll
        {
            get { return m_AutoScroll; }
            set { m_AutoScroll = value; }
        }
        
        //public override System.Drawing.Rectangle DisplayRectangle
        //{
        //    get
        //    {
        //        Rectangle rectangle = this.FrameRectangle;
        //        return Rectangle.FromLTRB
        //            (
        //            rectangle.Left - this.m_HScrollBarItem.Value,
        //            rectangle.Top - this.m_VScrollBarItem.Value,
        //            rectangle.Right,
        //            rectangle.Bottom
        //            );
        //    }
        //}

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            BaseItem baseItem;
            for (int i = 0; i < this.m_BaseItemCollection.Count; i++)
            {
                baseItem = this.m_BaseItemCollection[i];
                if (baseItem.pOwner != this) continue;
                //
                IMessageChain pMessageChain = baseItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            SetHVScrollBarVisible();
            base.OnPaint(pevent);
        }
        private void SetHVScrollBarVisible()
        {
            if (this.AutoScroll)
            {
                int iW = 0;
                int iH = 0;
                int iTmp = 0;
                foreach (Control one in this.Controls)
                {
                    iTmp = one.Location.X + one.Width;
                    if (iTmp > iW) iW = iTmp;
                    iTmp = one.Location.X + one.Height;
                    if (iTmp > iH) iH = iTmp;
                }
                //
                iW = iW - this.Width;
                if (iW > 0) iW += CONST_SCROLBARBARSIZE;
                iH = iH - this.Height;
                if (iH > 0) iH += CONST_SCROLBARBARSIZE;
                this.m_HScrollBarVisible = iW > 0;
                this.m_HScrollBarMaximum = this.m_HScrollBarVisible ? iW : 0;
                this.m_VScrollBarVisible = iH > 0;
                this.m_VScrollBarMaximum = this.m_VScrollBarVisible ? iH : 0;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.m_VScrollBarVisible &&
                this.m_VScrollBarMaximum > this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value >= this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value <= this.m_VScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_VScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_VScrollBarItem.Value--;
            }
            else if (this.m_HScrollBarVisible &&
                this.m_HScrollBarMaximum > this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value >= this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value <= this.m_HScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_HScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_HScrollBarItem.Value--;
            }
            //
            base.OnMouseWheel(e);
        }

        #region Clone
        public override object Clone()
        {
            PanelControl baseItem = new PanelControl();
            return baseItem;
        }
        #endregion
    }
}

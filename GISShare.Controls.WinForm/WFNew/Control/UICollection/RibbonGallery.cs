using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.RibbonGalleryDesigner)), DefaultEvent("TopViewItemIndexChanged"), ToolboxItem(true)]
    public class RibbonGallery : BaseItemStack, IGalleryItem, IGalleryItemEvent, IPopupOwner, IPopupOwnerHelper
    {
        private const int CTR_MINHEIGHT = 62;
        private const int CTR_SCROLLAREAWIDTH = 15;
        private const int CTR_UPBUTTONHEIGHT = 20;
        private const int CTR_DOWNBUTTONHEIGHT = 20;
        private const int CTR_DROPDOWNBUTTONHEIGHT = 22;

        private RibbonGalleryPopup m_RibbonGalleryPopup;

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

        private ScrollUpButtonItem m_ScrollUpButton = null;
        private ScrollDownButtonItem m_ScrollDownButton = null;
        private ScrollDropDownButtonItem m_ScrollDropDownButton = null;

        public RibbonGallery()
            : base()
        {
            base.BackColor = System.Drawing.Color.Transparent;
            //
            this.m_ScrollUpButton = new ScrollUpButtonItem(this);
            this.m_ScrollDownButton = new ScrollDownButtonItem(this);
            this.m_ScrollDropDownButton = new ScrollDropDownButtonItem(this);
            //
            this.m_RibbonGalleryPopup = new RibbonGalleryPopup(this);
            ((ISetOwnerHelper)this.m_RibbonGalleryPopup).SetOwner(this);
            this.m_RibbonGalleryPopup.PopupOpened += new EventHandler(RibbonGalleryPopup_PopupOpened);
            this.m_RibbonGalleryPopup.PopupClosed += new EventHandler(RibbonGalleryPopup_PopupClosed);
        }
        void RibbonGalleryPopup_PopupOpened(object sender, EventArgs e)
        {
            this.OnPopupOpened(e);
        }
        void RibbonGalleryPopup_PopupClosed(object sender, EventArgs e)
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

        #region IPopupOwner
        [Browsable(true), Description("当弹出Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupOpened;
        [Browsable(true), Description("当关闭Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupClosed;

        private int m_PopupSpace = 0;
        [Browsable(false), DefaultValue(0), Description("弹出菜单与其携带者的间距(与此类无关)"), Category("布局")]
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
                this.m_RibbonGalleryPopup.GetPopupPanel().TrySetPopupPanelSize(this.m_RibbonGalleryPopup.GetIdealSize());
                return this.PointToScreen(this.DisplayRectangle.Location);
            }
        }

        [Browsable(false), Description("是否已展开弹出项"), Category("状态")]
        public bool IsOpened
        {
            get { return this.m_RibbonGalleryPopup.IsOpened; }
        }

        [Browsable(false), Description("是否有自动触发Popup的展现"), Category("行为")]
        public bool IsAutoMouseTrigger
        {
            get { return false; }
        }

        [Browsable(false), Description("弹出菜单的激活区"), Category("布局")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get { return this.m_ScrollDropDownButton.DisplayRectangle; }
        }

        /// <summary>
        /// 展开弹出项
        /// </summary>
        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_RibbonGalleryPopup.Show(this.PopupLoction);
        }

        /// <summary>
        /// 关闭弹出项
        /// </summary>
        public void ClosePopup()
        {
            if (!this.IsOpened) return;
            //
            this.m_RibbonGalleryPopup.Close();
        }
        #endregion

        #region IPopupOwnerHelper
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_RibbonGalleryPopup;
        }
        #endregion

        #region IGalleryItemEvent
        public event IntValueChangedHandler TopViewItemIndexChanged;
        #endregion

        [Browsable(false), DefaultValue(typeof(Orientation), "Vertical"), Description("子项的布局方式（与此类无关）"), Category("布局")]
        public override Orientation eOrientation
        {
            get { return Orientation.Vertical; }
        }
        
        #region IGalleryItem
        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.GalleryRectangle;
                if (this.OverflowItemsCount <= 0) this.m_TopViewItemIndex = 0;
                return rectangle;
            }
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

        [Browsable(false), Description("其绘制矩形框"), Category("布局")]
        public Rectangle DrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("其子项存放区绘制矩形框"), Category("布局")]
        public Rectangle GalleryRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.Left + 1, rectangle.Top + 1, rectangle.Width - CTR_SCROLLAREAWIDTH, rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("其绘制矩形框"), Category("布局")]
        public Rectangle ScrollRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.Right - CTR_SCROLLAREAWIDTH + 1, rectangle.Top, CTR_SCROLLAREAWIDTH, rectangle.Height);
            }
        }

        [Browsable(false), Description("ScrollUpButton 绘制矩形框"), Category("布局")]
        public Rectangle ScrollUpButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.ScrollRectangle;
                return new Rectangle(rectangle.Left, rectangle.Top, CTR_SCROLLAREAWIDTH, CTR_UPBUTTONHEIGHT);
            }
        }

        [Browsable(false), Description("ScrollDownButton 其绘制矩形框"), Category("布局")]
        public Rectangle ScrollDownButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.ScrollRectangle;
                return new Rectangle(rectangle.Left, rectangle.Bottom - CTR_DROPDOWNBUTTONHEIGHT - CTR_DOWNBUTTONHEIGHT, CTR_SCROLLAREAWIDTH, CTR_DOWNBUTTONHEIGHT);
            }
        }

        [Browsable(false), Description("ScrollDropDownButton 其绘制矩形框"), Category("布局")]
        public Rectangle ScrollDropDownButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.ScrollRectangle;
                return new Rectangle(rectangle.Left, rectangle.Bottom - CTR_DROPDOWNBUTTONHEIGHT, CTR_SCROLLAREAWIDTH, CTR_DROPDOWNBUTTONHEIGHT);
            }
        }

        private bool m_UpButtonIncreaseIndex = true;
        [Browsable(true), DefaultValue(true), Description("UpButton 增加 子项视图索引"), Category("行为")]
        public bool UpButtonIncreaseIndex
        {
            get { return m_UpButtonIncreaseIndex; }
            set { m_UpButtonIncreaseIndex = value; }
        }

        private int m_TopViewItemIndex = 0;
        [Browsable(true), DefaultValue(0), Description("子项视图索引"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return m_TopViewItemIndex; }
            set
            {
                if (this.m_TopViewItemIndex == value) return;
                //
                //if (this.BaseItems.Count == 0) value = -1;
                //if (value < 0 ||
                //    value >= this.BaseItems.Count) value = 0;
                if (this.m_TopViewItemIndex < value) { value = this.GetEnableIndexIncrease(value); }
                else { value = this.GetEnableIndexDecrease(value); }
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
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        [Browsable(false)]
        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        #region Clone
        public override object Clone()
        {
            RibbonGallery baseItem = new RibbonGallery();
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
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.Visible = this.Visible;
            //
            baseItem.UpButtonIncreaseIndex = this.UpButtonIncreaseIndex;
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
            if (!this.IsOpened)
            {
                base.MessageMonitor(messageInfo);
            }
            //
            ((IMessageChain)this.m_ScrollUpButton).SendMessage(messageInfo);
            ((IMessageChain)this.m_ScrollDownButton).SendMessage(messageInfo);
            ((IMessageChain)this.m_ScrollDropDownButton).SendMessage(messageInfo);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            if (this.IsOpened) this.KeepRelayoutSize();
            else this.Relayout(e.Graphics, this.TopViewItemIndex);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonGallery(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
        private void KeepRelayoutSize()
        {
            int iMaxWidth = 0;
            foreach (BaseItem one in this.BaseItems)
            {
                if (!one.Visible) continue;
                if (iMaxWidth < one.Width) iMaxWidth = one.Width;
            }
            this.Width = iMaxWidth + CTR_SCROLLAREAWIDTH + 1;
        }
        private void Relayout(Graphics g, int topViewItemIndex)
        {
            if (this.BaseItems.Count < 0) return;
            //
            if (!this.BaseItems.OwnerEquals(this)) ((ISetOwnerHelper)this.BaseItems).SetOwner(this);//key
            //
            Rectangle rectangle = this.DisplayRectangle;
            Rectangle itemsRectangle = this.ItemsRectangle;
            //
            Size size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_LT(g, this, topViewItemIndex, true, false,
                0, -1, -1,
                itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                //this.Padding.Left + 1, this.Padding.Top + 1, this.Padding.Right + CTR_SCROLLAREAWIDTH, this.Padding.Bottom + 0,
                CTR_MINHEIGHT, -1, LayoutStyle.eLayoutAuto, ref  this._OverflowItemsCount, ref this._DrawItemsCount);
            //
            if (!size.IsEmpty)
            {
                this.Width = size.Width;
                this.Height = this.Height;
                //if (this.LockWith && this.Dock != DockStyle.Top && this.Dock != DockStyle.Bottom && this.Dock != DockStyle.Fill)
                //{
                //    this.Width = size.Width;
                //}
                //if (this.LockHeight && this.Dock != DockStyle.Left && this.Dock != DockStyle.Right && this.Dock != DockStyle.Fill)
                //{
                //    this.Height = this.Height;
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

        protected virtual void OnTopViewItemIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.TopViewItemIndexChanged != null) { this.TopViewItemIndexChanged(this, e); }
        }
    }
}

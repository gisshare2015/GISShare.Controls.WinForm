using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    class NCQuickAccessToolbarItem : WFNew.BaseItemStackItem,
        WFNew.IBaseBarItem, WFNew.IQuickAccessToolbarItem, INCBaseItem, IViewDepend
    {
        private const int CUSTOMIZEBUTTONWIDTH = 11;//自定义项宽度
        private const int OVERFLOWBUTTONWIDTH = 11;//溢出项宽度
        private const int FRAMELINESPACE = 1;

        private NCOverflowButtonItem m_OverflowButton;
        private NCCustomizeButtonItem m_CustomizeButton;

        public NCQuickAccessToolbarItem()
        {
            this.m_OverflowButton = new NCOverflowButtonItem(this);
            this.m_CustomizeButton = new NCCustomizeButtonItem(this);
            this.UsingViewOverflow = false;
            this.ShowBackground = true;
        }

        #region IOffsetNC
        [Browsable(false), Description("X轴偏移"), Category("布局")]
        public int NCOffsetX
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetX;
            }
        }

        [Browsable(false), Description("Y轴偏移"), Category("布局")]
        public int NCOffsetY
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetY;
            }
        }
        #endregion

        #region INCBaseItem
        public IBaseItemOwnerNC GetTopBaseItemOwnerNC()
        {
            return this.TryGetDependControl() as IBaseItemOwnerNC;
        }
        #endregion
        
        #region IBaseBarItem
        private bool m_ShowNomalState = false;
        [Browsable(true), DefaultValue(false), Description("绘制正常状态"), Category("状态")]
        public virtual bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        #region Radius
        private int m_LeftTopRadius = 2;
        [Browsable(true), DefaultValue(2), Description("左顶部圆角值"), Category("圆角")]
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

        private int m_RightTopRadius = 2;
        [Browsable(true), DefaultValue(2), Description("右顶部圆角值"), Category("圆角")]
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

        private int m_LeftBottomRadius = 2;
        [Browsable(true), DefaultValue(2), Description("左底部圆角值"), Category("圆角")]
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

        private int m_RightBottomRadius = 2;
        [Browsable(true), DefaultValue(2), Description("右底部圆角值"), Category("圆角")]
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

        private bool m_ShowOverflowButton = true;
        [Browsable(true), DefaultValue(true), Description("溢出按钮在右侧显示"), Category("布局")]
        public virtual bool ShowOverflowButton
        {
            get { return m_ShowOverflowButton; }
            set { m_ShowOverflowButton = value; }
        }

        [Browsable(false), Description("溢出按钮存在的矩形框"), Category("布局")]
        public virtual Rectangle OverflowButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ShowOverflowButton)
                {
                    switch (this.eQuickAccessToolbarStyle)
                    {
                        case WFNew.QuickAccessToolbarStyle.eAllRound:
                            return new Rectangle(rectangle.Right - this.Padding.Right - OVERFLOWBUTTONWIDTH - this.RoundWidth / 4 - CUSTOMIZEBUTTONWIDTH,
                                rectangle.Y + this.Padding.Top + 1,
                                OVERFLOWBUTTONWIDTH,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom - 2);
                        default:
                            return new Rectangle(rectangle.Right - this.Padding.Right - OVERFLOWBUTTONWIDTH - CUSTOMIZEBUTTONWIDTH,
                                rectangle.Y + this.Padding.Top,
                                OVERFLOWBUTTONWIDTH,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                    }
                }
                else
                {
                    return new Rectangle(rectangle.X + this.Padding.Left,
                        rectangle.Y + this.Padding.Top + 1,
                        OVERFLOWBUTTONWIDTH,
                        rectangle.Height - this.Padding.Top - this.Padding.Bottom - 2);
                }
            }
        }
        #endregion

        #region IQuickAccessToolbarItem
        private int m_RoundWidth = 16;
        [Browsable(true), DefaultValue(16), Description("圆弧区宽"), Category("布局")]
        public int RoundWidth
        {
            get { return m_RoundWidth; }
            set 
            {
                if (value < 10) return;
                m_RoundWidth = value;
            }
        }

        //private bool m_ShowBackground = true;
        //[Browsable(true), DefaultValue(true), Description("展现背景"), Category("状态")]
        //public override bool ShowBackground
        //{
        //    get { return m_ShowBackground; }
        //    set { m_ShowBackground = value; }
        //}

        private WFNew.QuickAccessToolbarStyle m_eQuickAccessToolbarStyle = WFNew.QuickAccessToolbarStyle.eNormal;
        [Browsable(true), DefaultValue(typeof(WFNew.QuickAccessToolbarStyle), "eNormal"), Description("快捷工具条的展现方式"), Category("外观")]
        public virtual WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle
        {
            get 
            {
                return m_eQuickAccessToolbarStyle;
            }
            set 
            {
                if (this.m_eQuickAccessToolbarStyle == value) return;
                m_eQuickAccessToolbarStyle = value;
                this.Refresh();
            }
        }

        [Browsable(false), Description("CustomizeButton绘制矩形框"), Category("布局")]
        public virtual Rectangle CustomizeButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.Right - this.Padding.Right - CUSTOMIZEBUTTONWIDTH,
                    rectangle.Y + this.Padding.Top,
                    CUSTOMIZEBUTTONWIDTH,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom);
            }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public override Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                rectangle.X += this.Padding.Left;
                rectangle.Y += this.Padding.Top;
                rectangle.Width = rectangle.Width - this.Padding.Left - this.Padding.Right - CUSTOMIZEBUTTONWIDTH;
                rectangle.Height = rectangle.Height - this.Padding.Top - this.Padding.Bottom - 1;
                return rectangle;
            }
        }
        #endregion

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override bool Overflow
        {
            get
            {
                IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
                if (pBaseItemOwnerNC == null) return base.Overflow;
                return !pBaseItemOwnerNC.ItemsRectangleNC.Contains(this.DisplayRectangle);
            }
        }

        [Browsable(false)]
        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = true;
            }
        }

        [Browsable(false)]
        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return System.Windows.Forms.Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            }
        }

        public override void Refresh()
        {
            IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
            if (pBaseItemOwnerNC != null) pBaseItemOwnerNC.RefreshNC();
            else base.Refresh();
        }

        public override void Invalidate(Rectangle rectangle)
        {
            this.Refresh();
        }

        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                rectangle.X += this.Padding.Left + FRAMELINESPACE;
                rectangle.Y += this.Padding.Top + FRAMELINESPACE;
                rectangle.Width = rectangle.Width - this.Padding.Left - this.Padding.Right - 2 * FRAMELINESPACE - CUSTOMIZEBUTTONWIDTH;
                rectangle.Height = rectangle.Height - this.Padding.Top - this.Padding.Bottom - 2 * FRAMELINESPACE;
                //
                if (this.OverflowItemsCount > 0)
                {
                    if (this.ShowOverflowButton)
                    {
                        rectangle.Width -= OVERFLOWBUTTONWIDTH;
                    }
                    else
                    {
                        rectangle.X += OVERFLOWBUTTONWIDTH;
                        rectangle.Width -= OVERFLOWBUTTONWIDTH;
                    }
                }
                //
                switch (this.eQuickAccessToolbarStyle)
                {
                    case WFNew.QuickAccessToolbarStyle.eAllRound:
                        rectangle.X += this.RoundWidth / 4;
                        rectangle.Width -= this.RoundWidth / 2;
                        break;
                    default:
                        break;
                }
                return rectangle;
            }
        }

        public override Rectangle ItemsViewRectangle
        {
            get
            {
                return this.ItemsRectangle;
            }
        }

        #region Clone
        public override object Clone()
        {
            NCQuickAccessToolbarItem baseItem = new NCQuickAccessToolbarItem();
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
            baseItem.ShowNomalState = this.ShowNomalState;
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
            baseItem.ShowOverflowButton = this.ShowOverflowButton;
            //
            baseItem.RoundWidth = this.RoundWidth;
            baseItem.ShowBackground = this.ShowBackground;
            baseItem.eQuickAccessToolbarStyle = this.eQuickAccessToolbarStyle;
            foreach (WFNew.BaseItem one in this.BaseItems)
            {
                baseItem.BaseItems.Add(one.Clone() as WFNew.BaseItem);
            }
            if (this.GetEventState("VisibleChanged") == WFNew.EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == WFNew.EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == WFNew.EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == WFNew.EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == WFNew.EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == WFNew.EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == WFNew.EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == WFNew.EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == WFNew.EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == WFNew.EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == WFNew.EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == WFNew.EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == WFNew.EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
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
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_OverflowButton).SendMessage(messageInfo);
            ((IMessageChain)this.m_CustomizeButton).SendMessage(messageInfo);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonQuickAccessToolbar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }

}

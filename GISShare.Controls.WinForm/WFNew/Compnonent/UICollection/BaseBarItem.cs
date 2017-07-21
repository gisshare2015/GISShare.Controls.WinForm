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
    /// <summary>
    /// 绘制型 基础工具条
    /// </summary>
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseBarItemDesigner))]
    public class BaseBarItem : BaseItemStackItem, IBaseBarItem, IGetPartItemHelper
    {
        private const int OVERFLOWBUTTONSIZE = 11;//溢出项宽度

        private OverflowButtonItem m_OverflowButton;

        public BaseBarItem()
        {
            base.eOrientation = Orientation.Horizontal;
            //
            this.m_OverflowButton = new OverflowButtonItem(this);
        }

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
                if (this.ReverseLayout)
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Top + this.Padding.Top,
                                OVERFLOWBUTTONSIZE,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        case Orientation.Vertical:
                        default:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Top + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                OVERFLOWBUTTONSIZE);
                    }
                }
                else
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            return new Rectangle(rectangle.Right - this.Padding.Right - OVERFLOWBUTTONSIZE,
                                rectangle.Top + this.Padding.Top,
                                OVERFLOWBUTTONSIZE,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        case Orientation.Vertical:
                        default:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Bottom - this.Padding.Bottom - OVERFLOWBUTTONSIZE,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                OVERFLOWBUTTONSIZE);
                    }
                }
            }
        }
        #endregion

        #region IGetPartItemHelper
        BaseItem[] IGetPartItemHelper.GetPartItems()
        {
            return new BaseItem[1] { this.m_OverflowButton };
        }
        #endregion

        [Browsable(false)]
        public override Rectangle ItemsRectangle
        {
            get
            {
                if (this.OverflowItemsCount > 0)
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    if (this.ReverseLayout)
                    {
                        switch (this.eOrientation)
                        {
                            case Orientation.Horizontal:
                                if (this.ShowOverflowButton)
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left + OVERFLOWBUTTONSIZE,
                                        rectangle.Y + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right - OVERFLOWBUTTONSIZE,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                                else
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Y + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                            case Orientation.Vertical:
                            default:
                                if (this.ShowOverflowButton)
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top + OVERFLOWBUTTONSIZE,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom - OVERFLOWBUTTONSIZE);
                                }
                                else
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                        }
                    }
                    else
                    {
                        switch (this.eOrientation)
                        {
                            case Orientation.Horizontal:
                                if (this.ShowOverflowButton)
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right - OVERFLOWBUTTONSIZE,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                                else
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                            case Orientation.Vertical:
                            default:
                                if (this.ShowOverflowButton)
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom - OVERFLOWBUTTONSIZE);
                                }
                                else
                                {
                                    return new Rectangle(rectangle.Left + this.Padding.Left,
                                        rectangle.Top + this.Padding.Top,
                                        rectangle.Width - this.Padding.Left - this.Padding.Right,
                                        rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                                }
                        }
                    }
                }
                return base.ItemsRectangle;
            }
        }

        #region Clone
        public override object Clone()
        {
            BaseBarItem baseItem = new BaseBarItem();
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
            foreach (BaseItem one in this.BaseItems) 
            {
                baseItem.BaseItems.Add(one.Clone() as BaseItem);
            }
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
            //发送消息
            ((IMessageChain)this.m_OverflowButton).SendMessage(messageInfo);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonBaseBar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

    }
}

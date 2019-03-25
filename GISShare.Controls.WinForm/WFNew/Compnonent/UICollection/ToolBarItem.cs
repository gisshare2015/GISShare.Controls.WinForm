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
    public class ToolBarItem : BaseBarItem, IToolBarItem
    {
        private const int GRIPREGIONSIZE = 10;//手柄宽度

        #region IToolBarItem
        private bool m_ShowGripRegion = true;
        [Browsable(true), DefaultValue(true), Description("是否显示手柄区"), Category("布局")]
        public bool ShowGripRegion
        {
            get { return m_ShowGripRegion; }
            set { m_ShowGripRegion = value; }
        }

        ToolBarStyle m_eToolBarStyle = ToolBarStyle.eToolBar;
        [Browsable(true), DefaultValue(typeof(ToolBarStyle), "eToolBar"), Description("工具条类型"), Category("外观")]
        public ToolBarStyle eToolBarStyle
        {
            get { return m_eToolBarStyle; }
            set { m_eToolBarStyle = value; }
        }

        [Browsable(false), Description("手柄区矩形框"), Category("布局")]
        public Rectangle GripRegionRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ReverseLayout)
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            return new Rectangle(rectangle.Right - this.Padding.Right - GRIPREGIONSIZE,
                                rectangle.Top + this.Padding.Top,
                                GRIPREGIONSIZE,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        case Orientation.Vertical:
                        default:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Bottom - this.Padding.Bottom - GRIPREGIONSIZE,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                GRIPREGIONSIZE);
                    }
                }
                else
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Top + this.Padding.Top,
                                GRIPREGIONSIZE,
                                rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                        case Orientation.Vertical:
                        default:
                            return new Rectangle(rectangle.Left + this.Padding.Left,
                                rectangle.Top + this.Padding.Top,
                                rectangle.Width - this.Padding.Left - this.Padding.Right,
                                GRIPREGIONSIZE);
                    }
                }
            }
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ToolBarItem baseItem = new ToolBarItem();
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
            baseItem.ShowGripRegion = this.ShowGripRegion;
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

        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = base.ItemsRectangle;
                if (this.ReverseLayout)
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            if (this.ShowGripRegion)
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - GRIPREGIONSIZE, rectangle.Bottom);
                            }
                            else
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                        case Orientation.Vertical:
                            if (this.ShowGripRegion)
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - GRIPREGIONSIZE);
                            }
                            else
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                    }
                }
                else
                {
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            if (this.ShowGripRegion)
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left + GRIPREGIONSIZE, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            else
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                        case Orientation.Vertical:
                            if (this.ShowGripRegion)
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top + GRIPREGIONSIZE, rectangle.Right, rectangle.Bottom);
                            }
                            else
                            {
                                rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                    }
                }
                return rectangle;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonToolBar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}

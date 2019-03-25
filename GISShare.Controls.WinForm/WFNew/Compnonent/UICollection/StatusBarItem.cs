using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    //[ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseBarDesigner))]
    public class StatusBarItem : BaseBarItem, IStatusBarItem
    {
        private WFNew.StatusBarRightBaseItemStackItem m_BaseItemStackItem;

        public StatusBarItem()
        {
            //base.Dock = DockStyle.Bottom;
            base.Padding = new System.Windows.Forms.Padding(1);
            //
            this.m_BaseItemStackItem = new StatusBarRightBaseItemStackItem(this);
        }

        #region IStatusBarItem
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的右侧子项集合"), Category("子项")]
        public BaseItemCollection RightBaseItems
        {
            get { return this.m_BaseItemStackItem.BaseItems; }
        }

        [Browsable(false)]
        public Rectangle RightItemsRectangle 
        {
            get { return this.m_BaseItemStackItem.ItemsRectangle; }
        }
        #endregion

        [Browsable(false)]
        public override Rectangle ItemsRectangle
        {
            get
            {
                if (this.m_BaseItemStackItem.HaveVisibleBaseItem)
                {
                    Rectangle rectangle = base.ItemsRectangle;
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            if (this.ReverseLayout)
                            {
                                return Rectangle.FromLTRB(rectangle.Left + this.m_BaseItemStackItem.Width, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            else
                            {
                                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - this.m_BaseItemStackItem.Width, rectangle.Bottom);
                            }
                        case Orientation.Vertical:
                            if (this.ReverseLayout)
                            {
                                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top + this.m_BaseItemStackItem.Height, rectangle.Right, rectangle.Bottom);
                            }
                            else
                            {
                                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - this.m_BaseItemStackItem.Height);
                            }
                        default:
                            break;
                    }
                }
                return base.ItemsRectangle;
            }
        }

        [Browsable(false)]
        public override Rectangle OverflowButtonRectangle
        {
            get
            {
                if (this.m_BaseItemStackItem.HaveVisibleBaseItem)
                {
                    Rectangle rectangle = base.OverflowButtonRectangle;
                    Rectangle rectangle2 = this.m_BaseItemStackItem.DisplayRectangle;
                    switch (this.eOrientation)
                    {
                        case Orientation.Horizontal:
                            if (this.ReverseLayout)
                            {
                                return Rectangle.FromLTRB(rectangle2.Right, rectangle.Top, rectangle2.Right + rectangle.Width, rectangle.Bottom);
                            }
                            else
                            {
                                return Rectangle.FromLTRB(rectangle2.Left - rectangle.Width, rectangle.Top, rectangle2.Left, rectangle.Bottom);
                            }
                        case Orientation.Vertical:
                            if (this.ReverseLayout)
                            {
                                return Rectangle.FromLTRB(rectangle.Left, rectangle2.Bottom, rectangle.Right, rectangle2.Bottom + rectangle.Height);
                            }
                            else
                            {
                                return Rectangle.FromLTRB(rectangle.Left, rectangle2.Top - rectangle.Height, rectangle.Right, rectangle2.Top);
                            }
                        default:
                            break;
                    }
                }
                return base.OverflowButtonRectangle;
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

        [Browsable(false)]
        public override bool ReverseLayout
        {
            get
            {
                return false;
            }
            set
            {
                base.ReverseLayout = false;
            }
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_BaseItemStackItem).SendMessage(messageInfo);
        }

        #region Clone
        public override object Clone()
        {
            StatusBarItem baseItem = new StatusBarItem();
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

        //protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        //{
        //    this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
        //    this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
        //    //
        //    //base.OnDraw(e);
        //    //
        //    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonStatusBar(
        //        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        //}
    }
}

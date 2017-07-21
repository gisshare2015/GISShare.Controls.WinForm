using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true)]
    public class Separator : BaseItemControl, ISeparatorItem
    {
        private const int CTR_WHSIZE = 3;
        private const int CTR_DARKLINESPACE = 1;
        private const int CTR_LIGHTLINESPACE = 2;

        public Separator()
            : base()
        {
            base.BackColor = System.Drawing.Color.Transparent;
        }

        #region ISeparatorItem
        private int m_LineSpace = 6;
        [Browsable(true), DefaultValue(6), Description("分割线的两端间距"), Category("布局")]
        public int LineSpace
        {
            get { return m_LineSpace; }
            set { m_LineSpace = value; }
        }

        private bool m_BaseItemStateEnable = false;
        [Browsable(true), DefaultValue(false), Description("自身状态是否可用"), Category("状态")]
        public bool BaseItemStateEnable
        {
            get { return m_BaseItemStateEnable; }
            set { m_BaseItemStateEnable = value; }
        }

        private bool m_AutoLayout = true;
        [Browsable(false), Description("自动布局（与此类无关）"), Category("布局")]
        public bool AutoLayout
        {
            get { return m_AutoLayout; }
            set { m_AutoLayout = value; }
        }

        private Orientation m_eOrientation = Orientation.Vertical;
        [Browsable(true), DefaultValue(typeof(Orientation), "Vertical"), Description("分割线的布局方式"), Category("布局")]
        public Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        [Browsable(false), Description("DarkLine起点"), Category("布局")]
        public Point StartPointLightLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Point(rectangle.Left + this.LineSpace, rectangle.Top + CTR_LIGHTLINESPACE);
                    case Orientation.Vertical:
                    default:
                        return new Point(rectangle.Left + CTR_LIGHTLINESPACE, rectangle.Top + this.LineSpace);
                }
            }
        }

        [Browsable(false), Description("DarkLine终点"), Category("布局")]
        public Point EndPointLightLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Point(rectangle.Right - this.LineSpace, rectangle.Top + CTR_LIGHTLINESPACE);
                    case Orientation.Vertical:
                    default:
                        return new Point(rectangle.Left + CTR_LIGHTLINESPACE, rectangle.Bottom - this.LineSpace);
                }
            }
        }

        [Browsable(false), Description("LightLine起点"), Category("布局")]
        public Point StartPointDarkLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Point(rectangle.Left + this.LineSpace, rectangle.Top + CTR_DARKLINESPACE);
                    case Orientation.Vertical:
                    default:
                        return new Point(rectangle.Left + CTR_DARKLINESPACE, rectangle.Top + this.LineSpace);
                }
            }
        }

        [Browsable(false), Description("LightLine终点"), Category("布局")]
        public Point EndPointDarkLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Point(rectangle.Right - this.LineSpace, rectangle.Top + CTR_DARKLINESPACE);
                    case Orientation.Vertical:
                    default:
                        return new Point(rectangle.Left + CTR_DARKLINESPACE, rectangle.Bottom - this.LineSpace);
                }
            }
        }
        #endregion

        public override bool LockHeight
        {
            get
            {
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return false;
                    case Orientation.Horizontal:
                        return true;
                }
                return true;
            }
        }

        public override bool LockWith
        {
            get
            {
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        return true;
                    case Orientation.Horizontal:
                        return false;
                }
                return true;
            }
        }

        #region Clone
        public override object Clone()
        {
            Separator baseItem = new Separator();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            //
            baseItem.LineSpace = this.LineSpace;
            baseItem.BaseItemStateEnable = this.BaseItemStateEnable;
            baseItem.eOrientation = this.eOrientation;
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

        //public override Size MeasureSize(Graphics g) { return DisplayRectangle.Size; }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            //
            switch (this.eOrientation) 
            {
                case Orientation.Horizontal:
                    this.Height = CTR_WHSIZE;
                    break;
                case Orientation.Vertical:
                    this.Width = CTR_WHSIZE;
                    break;
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
            //
            //base.OnPaint(e);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSeparator(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return this.BaseItemStateEnable;
            }
        }

    }
}

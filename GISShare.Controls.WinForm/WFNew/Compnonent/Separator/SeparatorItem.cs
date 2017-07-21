using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class SeparatorItem : BaseItem, ISeparatorItem
    {
        private const int CTR_WHSIZE = 3;
        private const int CTR_DARKLINESPACE = 1;
        private const int CTR_LIGHTLINESPACE = 2;

        #region 构造函数
        public SeparatorItem()
        {
            this.Text = "———分隔条———";
        }

        //public SeparatorItem(GISShare.Controls.Plugin.WFNew.ISeparatorItemP pBaseItemP)
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ISeparatorItemP
        //    this.eOrientation = pBaseItemP.eOrientation;
        //    this.LineSpace = pBaseItemP.LineSpace;
        //    this.BaseItemStateEnable = pBaseItemP.BaseItemStateEnable;
        //}
        #endregion

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
        [Browsable(true), Description("自动布局"), Category("布局")]
        public bool AutoLayout
        {
            get { return m_AutoLayout; }
            set { m_AutoLayout = value; }
        }

        private Orientation m_eOrientation = Orientation.Vertical;
        [Browsable(false), Description("分割线的布局方式"), Category("布局")]
        public Orientation eOrientation
        {
            get
            {
                if (this.AutoLayout)
                {
                    IBaseItemStackItem pBaseItemStackItem = this.pOwner as IBaseItemStackItem;
                    if (pBaseItemStackItem != null)
                    {
                        switch (pBaseItemStackItem.eOrientation)
                        {
                            case Orientation.Horizontal:
                                return Orientation.Vertical;
                            case Orientation.Vertical:
                                return Orientation.Horizontal;
                        }
                    }
                }
                return m_eOrientation;
            }
            set { m_eOrientation = value; }
        }

        [Browsable(false), Description("DarkLine起点"), Category("布局")]
        public Point StartPointDarkLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem != null)
                {
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            return new Point(rectangle.Left + this.LineSpace + pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth, rectangle.Top + CTR_DARKLINESPACE);
                        case ContextPopupStyle.eNormal:
                            return new Point(rectangle.Left + this.LineSpace + pContextPopupPanelItem.ImageRegionWidth, rectangle.Top + CTR_DARKLINESPACE);
                        case ContextPopupStyle.eSimply:
                        default:
                            return new Point(rectangle.Left + this.LineSpace, rectangle.Top + CTR_DARKLINESPACE);
                    }
                }
                else
                {
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
        }

        [Browsable(false), Description("DarkLine终点"), Category("布局")]
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

        [Browsable(false), Description("LightLine起点"), Category("布局")]
        public Point StartPointLightLine
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem != null)
                {
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            return new Point(rectangle.Left + this.LineSpace + pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth, rectangle.Top + CTR_LIGHTLINESPACE);
                        case ContextPopupStyle.eNormal:
                            return new Point(rectangle.Left + this.LineSpace + pContextPopupPanelItem.ImageRegionWidth, rectangle.Top + CTR_LIGHTLINESPACE);
                        case ContextPopupStyle.eSimply:
                        default:
                            return new Point(rectangle.Left + this.LineSpace, rectangle.Top + CTR_LIGHTLINESPACE);
                    }
                }
                else
                {
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
        }

        [Browsable(false), Description("LightLine终点"), Category("布局")]
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
        #endregion

        public override bool Visible
        {
            get
            {
                if (this.AutoLayout)
                {
                    IBaseItemStackItem pBaseItemStackItem = this.pOwner as IBaseItemStackItem;
                    if (pBaseItemStackItem != null)
                    {
                        int iIndex = 0;
                        int iIndexM = 0;
                        foreach (BaseItem one in pBaseItemStackItem.BaseItems)
                        {
                            if (one == this)
                            {
                                iIndexM = iIndex;
                            }
                            else
                            {
                                if (one is ISeparatorItem) continue;
                                //
                                if (one.Visible)
                                {
                                    iIndex++;
                                }
                            }
                        }
                        return base.Visible ? iIndexM != 0 && iIndexM != iIndex : false;
                    }
                }
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return new Size(CTR_WHSIZE, CTR_WHSIZE);
            }
            set
            {
                base.MinimumSize = value;
            }
        }

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

        public override Rectangle DesignMouseClickRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Rectangle(this.Location, new Size(base.Size.Width, CTR_WHSIZE));
                    case Orientation.Vertical:
                        return new Rectangle(this.Location, new Size(CTR_WHSIZE, base.Size.Height));
                }
                return base.DisplayRectangle;
            }
        }

        #region Clone
        public override object Clone()
        {
            SeparatorItem baseItem = new SeparatorItem();
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

        protected override bool RefreshBaseItemState
        {
            get
            {
                return this.BaseItemStateEnable;
            }
        }

        public override Size MeasureSize(Graphics g)//有待完善
        {
            return this.Size;
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            //switch (this.eOrientation)
            //{
            //    case Orientation.Horizontal:
            //        ((ISetBaseItemHelper)this).SetSize(this.Width, CTR_WHSIZE);
            //        break;
            //    case Orientation.Vertical:
            //        ((ISetBaseItemHelper)this).SetSize(CTR_WHSIZE, this.Height);
            //        break;
            //}
            ////
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSeparator(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}

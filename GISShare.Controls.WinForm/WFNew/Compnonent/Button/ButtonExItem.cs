using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ButtonExItem : ButtonItem, IBaseButtonExItem
    {
        #region 构造函数
        public ButtonExItem(ISimplyPopup pSimplyPopup)
            : base(pSimplyPopup) { }

        public ButtonExItem()
            : base(new DropDownPopup()) { }

        public ButtonExItem(string strText)
            : base(strText) { }

        public ButtonExItem(string strName, string strText)
            : base(strName, strText) { }

        public ButtonExItem(string strText, Image image)
            : base(strText, image) { }

        public ButtonExItem(string strName, string strText, Image image)
            : base(strName, strText, image) { }

        //public ButtonExItem(GISShare.Controls.Plugin.WFNew.IButtonExItemP pBaseItemP)
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
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //    //IBaseButtonItemP
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.ShowNomalState = pBaseItemP.ShowNomalState;
        //    //IDropDownButtonItemP
        //    this.DropDownDistance = pBaseItemP.DropDownDistance;
        //    this.eArrowStyle = pBaseItemP.eArrowStyle;
        //    this.eArrowDock = pBaseItemP.eArrowDock;
        //    this.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
        //    this.ArrowSize = pBaseItemP.ArrowSize;
        //    this.PopupSpace = pBaseItemP.PopupSpace;
        //    //ISplitButtonItemP
        //    this.ShowNomalSplitLine = pBaseItemP.ShowNomalSplitLine;
        //    //IButtonItemP
        //    this.eButtonStyle = pBaseItemP.eButtonStyle;
        //    //IButtonExItemP
        //    this.eOrientation = pBaseItemP.eOrientation;
        //}
        #endregion

        System.Windows.Forms.Orientation m_eOrientation = Orientation.Horizontal;
        [Browsable(true), DefaultValue(typeof(Orientation), "Horizontal"), Description("文本布局方式"), Category("布局")]
        public virtual System.Windows.Forms.Orientation eOrientation
        {
            get { return this.IsPopupItem ? System.Windows.Forms.Orientation.Horizontal : m_eOrientation; }
            set { m_eOrientation = value; }
        }

        private Rectangle m_TextRectangle;
        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public override Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = m_TextRectangle;
                Rectangle displayRectangle = this.ButtonRectangle;
                int iTop = rectangle.Top;
                int iLeft = rectangle.Left;
                int iRight = rectangle.Right;
                int iBottom = rectangle.Bottom;
                if (displayRectangle.Top > iTop) iTop = displayRectangle.Top;
                if (displayRectangle.Left > iLeft) iLeft = displayRectangle.Left;
                if (displayRectangle.Right < iRight) iRight = displayRectangle.Right;
                if (displayRectangle.Bottom < iBottom) iBottom = displayRectangle.Bottom;
                return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
            }
        }

        private Rectangle GetTextRectangle(Graphics g)
        {
            if (!this.AutoPlanTextRectangle ||
                this.Image == null ||
                this.eDisplayStyle == DisplayStyle.eText ||
                this.eImageSizeStyle == ImageSizeStyle.eStretch)
            {
                return this.GetTextRectangleAutoF(g);
            }
            else
            {
                return this.GetTextRectangleAutoT(g);
            }
        }
        private Rectangle GetTextRectangleAutoF(Graphics g)
        {
            Rectangle rectangle = this.ITDrawRectangle;
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            if (this.eOrientation == Orientation.Vertical)
            {
                iWidth = (int)(size.Height + 1);
                iHeight = (int)(size.Width + 1);
            }
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                //
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(rectangle.Left,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                case ContentAlignment.MiddleCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                case ContentAlignment.MiddleRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                //
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle GetTextRectangleAutoT(Graphics g)
        {
            Rectangle rectangle = this.ITDrawRectangle;
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            if (this.eOrientation == Orientation.Vertical)
            {
                iWidth = (int)(size.Height + 1);
                iHeight = (int)(size.Width + 1);
            }
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                    if (this.ImageAlign == ContentAlignment.TopLeft)
                    {
                        return new Rectangle(this.ImageRectangle.Right + this.ITSpace,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Left,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.TopCenter:
                    if (this.ImageAlign == ContentAlignment.TopLeft)
                    {
                        return new Rectangle((this.ImageRectangle.Right + this.ITSpace + rectangle.Right - iWidth) / 2,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.TopCenter)
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            this.ImageRectangle.Bottom + this.ITSpace,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.TopRight)
                    {
                        return new Rectangle((rectangle.Left + this.ImageRectangle.Left - this.ITSpace - iWidth) / 2,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.TopRight:
                    if (this.ImageAlign == ContentAlignment.TopRight)
                    {
                        return new Rectangle(this.ImageRectangle.Left - this.ITSpace - iWidth,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Right - iWidth,
                            rectangle.Top,
                            iWidth,
                            iHeight);
                    }
                //
                case ContentAlignment.MiddleLeft:
                    if (this.ImageAlign == ContentAlignment.MiddleLeft)
                    {
                        return new Rectangle(this.ImageRectangle.Right + this.ITSpace,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Left,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.MiddleCenter:
                    if (this.ImageAlign == ContentAlignment.MiddleLeft)
                    {
                        return new Rectangle((this.ImageRectangle.Right + this.ITSpace + rectangle.Right - iWidth) / 2,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.TopRight)
                    {
                        return new Rectangle((rectangle.Left + this.ImageRectangle.Left - this.ITSpace - iWidth) / 2,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.TopCenter)
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            (this.ImageRectangle.Bottom + this.ITSpace + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomCenter)
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            (rectangle.Top + this.ImageRectangle.Top - this.ITSpace - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.MiddleRight:
                    if (this.ImageAlign == ContentAlignment.TopRight)
                    {
                        return new Rectangle(this.ImageRectangle.Left - this.ITSpace - iWidth,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Right - iWidth,
                            (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                            iWidth,
                            iHeight);
                    }
                //
                case ContentAlignment.BottomLeft:
                    if (this.ImageAlign == ContentAlignment.BottomLeft)
                    {
                        return new Rectangle(this.ImageRectangle.Right + this.ITSpace,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Left,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.BottomCenter:
                    if (this.ImageAlign == ContentAlignment.BottomLeft)
                    {
                        return new Rectangle((this.ImageRectangle.Right + this.ITSpace + rectangle.Right - iWidth) / 2,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomCenter)
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                           this.ImageRectangle.Top - this.ITSpace - iHeight,
                            iWidth,
                            iHeight);
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomRight)
                    {
                        return new Rectangle((rectangle.Left + this.ImageRectangle.Left - this.ITSpace - iWidth) / 2,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                case ContentAlignment.BottomRight:
                    if (this.ImageAlign == ContentAlignment.BottomRight)
                    {
                        return new Rectangle(this.ImageRectangle.Left - this.ITSpace - iWidth,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                    else
                    {
                        return new Rectangle(rectangle.Right - iWidth,
                            rectangle.Bottom - iHeight,
                            iWidth,
                            iHeight);
                    }
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }

        #region Clone
        public override object Clone()
        {
            ButtonExItem baseItem = new ButtonExItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.ArrowSize = this.ArrowSize;
            baseItem.DropDownDistance = this.DropDownDistance;
            baseItem.eArrowDock = this.eArrowDock;
            baseItem.eArrowStyle = this.eArrowStyle;
            baseItem.eButtonStyle = this.eButtonStyle;
            baseItem.eDisplayStyle = this.eDisplayStyle;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.PopupSpace = this.PopupSpace;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalSplitLine = this.ShowNomalSplitLine;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            baseItem.TextLeftSpace = this.TextLeftSpace;
            baseItem.TextRightSpace = this.TextRightSpace;
            baseItem.eOrientation = this.eOrientation;
            baseItem.Visible = this.Visible;
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
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("ButtonMouseUp") == EventStateStyle.eUsed) baseItem.ButtonMouseUp += new MouseEventHandler(baseItem_ButtonMouseUp);
            if (this.GetEventState("ButtonMouseMove") == EventStateStyle.eUsed) baseItem.ButtonMouseMove += new MouseEventHandler(baseItem_ButtonMouseMove);
            if (this.GetEventState("ButtonMouseDown") == EventStateStyle.eUsed) baseItem.ButtonMouseDown += new MouseEventHandler(baseItem_ButtonMouseDown);
            if (this.GetEventState("ButtonMouseDoubleClick") == EventStateStyle.eUsed) baseItem.ButtonMouseDoubleClick += new MouseEventHandler(baseItem_ButtonMouseDoubleClick);
            if (this.GetEventState("ButtonMouseClick") == EventStateStyle.eUsed) baseItem.ButtonMouseClick += new MouseEventHandler(baseItem_ButtonMouseClick);
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
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        void baseItem_ButtonMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseClick", e);
        }
        void baseItem_ButtonMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDoubleClick", e);
        }
        void baseItem_ButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDown", e);
        }
        void baseItem_ButtonMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseMove", e);
        }
        void baseItem_ButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseUp", e);
        }
        void baseItem_SplitMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseClick(", e);
        }
        void baseItem_SplitMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDoubleClick", e);
        }
        void baseItem_SplitMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDown", e);
        }
        void baseItem_SplitMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseMove", e);
        }
        void baseItem_SplitMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseUp", e);
        }
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            if (this.eOrientation == Orientation.Horizontal) return base.MeasureSize(g);
            //
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Height + 1);
                    iHeight = (int)(size.Width + 1);
                    break;
                case DisplayStyle.eImage:
                    if (this.Image != null)
                    {
                        rectangle = this.ImageRectangle;
                        iWidth += rectangle.Width;
                        iHeight += rectangle.Height;
                    }
                    break;
                case DisplayStyle.eImageAndText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Height + 1);
                    iHeight = (int)(size.Width + 1);
                    if (this.Image != null)
                    {
                        switch (this.ImageAlign)
                        {
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopRight:
                                rectangle = this.ImageRectangle;
                                iWidth += rectangle.Width + this.ITSpace;
                                iHeight += rectangle.Height;
                                break;
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.TopCenter:
                                iHeight += this.ImageRectangle.Height + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleRight:
                                iWidth += this.ImageRectangle.Width + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleCenter:
                                rectangle = this.ImageRectangle;
                                if (iWidth < rectangle.Width) iWidth = rectangle.Width;
                                if (iHeight < rectangle.Height) iHeight = rectangle.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eDropDownButton:
                case ButtonStyle.eSplitButton:
                    switch (this.eArrowDock)
                    {
                        case ArrowDock.eUp:
                        case ArrowDock.eDown:
                            iHeight += this.DropDownDistance;
                            break;
                        case ArrowDock.eLeft:
                        case ArrowDock.eRight:
                            iWidth += this.DropDownDistance;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            iWidth += this.Padding.Left + this.Padding.Right;
            iHeight += this.Padding.Top + this.Padding.Bottom;
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                        iHeight = pContextPopupPanelItem.CheckRegionWidth > pContextPopupPanelItem.ImageRegionWidth ? pContextPopupPanelItem.CheckRegionWidth : pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eNormal:
                        iHeight = pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eSimply:
                    default:
                        break;
                }
                iWidth += this.TextLeftSpace + this.TextRightSpace;
            }
            //
            if (iWidth < 16) iWidth = 16;
            if (iHeight < 16) iHeight = 16;
            //
            return new Size(iWidth, iHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.m_TextRectangle = this.GetTextRectangle(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            switch (this.eButtonStyle)
            {
                case ButtonStyle.eButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderBaseButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eCheckButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCheckButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eDropDownButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDropDownButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eSplitButton:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSplitButton(
                        new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
                    break;
                case ButtonStyle.eLabel:
                default:
                    break;
            }
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                    case ContextPopupStyle.eNormal:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderContextPopupItemButtonChecked(
                            new GISShare.Controls.WinForm.CheckedRenderEventArgs(pevent.Graphics, this, this.Enabled, this.ForeColor, this.Checked, this.CheckRectangle));
                        break;
                    default:
                        break;
                }
                //
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                }
                //
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eDropDownButton:
                        if (this.HaveVisibleBaseItem)
                        {
                            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                               new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
                        }
                        break;
                    case ButtonStyle.eSplitButton:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                            new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eImage:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        break;
                    case DisplayStyle.eText:
                        StringFormat stringFormat = new StringFormat();
                        if (this.eOrientation == Orientation.Vertical) stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                        stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, true, this.Text, this.ForeColor, this.Font, this.TextRectangle, stringFormat));
                        break;
                    case DisplayStyle.eImageAndText:
                        StringFormat stringFormat2 = new StringFormat();
                        if (this.eOrientation == Orientation.Vertical) stringFormat2.FormatFlags = StringFormatFlags.DirectionVertical;
                        stringFormat2.Trimming = StringTrimming.EllipsisCharacter;
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, true, this.Text, this.ForeColor, this.Font, this.TextRectangle, stringFormat2));
                        break;
                    default:
                        break;
                }
                //
                switch (this.eButtonStyle)
                {
                    case ButtonStyle.eDropDownButton:
                    case ButtonStyle.eSplitButton:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                            new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

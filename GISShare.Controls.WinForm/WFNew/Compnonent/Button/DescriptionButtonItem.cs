using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class DescriptionButtonItem : BaseButtonItem, IDescriptionButtonItem
    {
        #region 构造函数
        public DescriptionButtonItem()
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        public DescriptionButtonItem(string strText)
            : base(strText)
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        public DescriptionButtonItem(string strName, string strText)
            : base(strName, strText)
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        public DescriptionButtonItem(string strText, Image image)
            : base(strText, image)
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        public DescriptionButtonItem(string strName, string strText, Image image)
            : base(strName, strText, image)
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        //public DescriptionButtonItem(GISShare.Controls.Plugin.WFNew.IDescriptionButtonItemP pBaseItemP)
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
        //    //IDescriptionButtonItemP
        //    this.DescriptionForeColor = pBaseItemP.DescriptionForeColor;
        //    this.DescriptionFont = pBaseItemP.DescriptionFont;
        //    this.Description = pBaseItemP.Description;
        //    this.TDSpace = pBaseItemP.TDSpace;
        //}
        #endregion

        #region IDescriptionButtonItem
        private Color m_DescriptionForeColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("描述文本字体颜色"), Category("外观")]
        public Color DescriptionForeColor
        {
            get { return m_DescriptionForeColor; }
            set { m_DescriptionForeColor = value; }
        }

        private Font m_DescriptionFont = new Font("宋体", 9f);
        [Browsable(true), DefaultValue(typeof(Font), "\"宋体\", 9f"), Description("描述文本字体"), Category("外观")]
        public virtual Font DescriptionFont
        {
            get { return m_DescriptionFont; }
            set { m_DescriptionFont = value; }
        }

        private string m_Description = "";
        [Browsable(true), Description("描述文本"), Category("外观")]
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private int m_TDSpace = 2;
        [Browsable(true), DefaultValue(2), Description("文本和描述文本的间距"), Category("布局")]
        public int TDSpace
        {
            get { return m_TDSpace; }
            set { if (value < 0) return; m_TDSpace = value; }
        }

        [Browsable(false), Description("文本和描述文本的矩形框"), Category("布局")]
        public virtual Rectangle TDRectangle
        {
            get
            {
                Rectangle rectangle = this.ITDrawRectangle;
                if (rectangle.Width < rectangle.Height)
                {
                    switch (this.ImageAlign)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.TopRight:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top + this.ImageRectangle.Height + this.ITSpace,
                                rectangle.Right,
                                rectangle.Bottom);
                        //
                        case ContentAlignment.MiddleLeft:
                            return Rectangle.FromLTRB(rectangle.Left + this.ImageRectangle.Height + this.ITSpace,
                                rectangle.Top,
                                rectangle.Right,
                                rectangle.Bottom);
                        case ContentAlignment.MiddleCenter:
                            return rectangle;
                        case ContentAlignment.MiddleRight:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top,
                                rectangle.Right - this.ImageRectangle.Height - this.ITSpace,
                                rectangle.Bottom);
                        //
                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomCenter:
                        case ContentAlignment.BottomRight:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top,
                                rectangle.Right,
                                rectangle.Bottom - this.ImageRectangle.Height - this.ITSpace);
                        default:
                            return new Rectangle(0, 0, 0, 0);
                    }
                }
                else
                {
                    switch (this.ImageAlign)
                    {
                        case ContentAlignment.TopCenter:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top + this.ImageRectangle.Height + this.ITSpace,
                                rectangle.Right,
                                rectangle.Bottom);
                        //
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.BottomLeft:
                            return Rectangle.FromLTRB(rectangle.Left + this.ImageRectangle.Height + this.ITSpace,
                                rectangle.Top,
                                rectangle.Right,
                                rectangle.Bottom);
                        case ContentAlignment.MiddleCenter:
                            return rectangle;
                        case ContentAlignment.TopRight:
                        case ContentAlignment.MiddleRight:
                        case ContentAlignment.BottomRight:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top,
                                rectangle.Right - this.ImageRectangle.Height - this.ITSpace,
                                rectangle.Bottom);
                        //
                        case ContentAlignment.BottomCenter:
                            return Rectangle.FromLTRB(rectangle.Left,
                                rectangle.Top,
                                rectangle.Right,
                                rectangle.Bottom - this.ImageRectangle.Height - this.ITSpace);
                        default:
                            return new Rectangle(0, 0, 0, 0);
                    }
                }
            }
        }

        [Browsable(false), Description("描述文本的矩形框"), Category("布局")]
        public virtual Rectangle DescriptionRectangle
        {
            get
            {
                Rectangle rectangle = this.TDRectangle;
                rectangle = Rectangle.FromLTRB(rectangle.Left,
                    this.TextRectangle.Bottom + this.TDSpace,
                    rectangle.Right,
                    rectangle.Bottom);
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
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            SizeF size2;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    size2 = g.MeasureString(this.Description, this.DescriptionFont);
                    iWidth = (int)(size.Width > size2.Width ? size.Width : size2.Width + 1);
                    iHeight = (int)(size.Height + this.TDSpace + size2.Height + 1);
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
                    size2 = g.MeasureString(this.Description, this.DescriptionFont);
                    iWidth = (int)(size.Width > size2.Width ? size.Width : size2.Width + 1);
                    iHeight = (int)(size.Height + this.TDSpace + size2.Height + 1);
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

        private Rectangle m_TextRectangle;
        public override Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.m_TextRectangle;
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
        private Rectangle GetTextRectangleEx(Graphics g)
        {
            Rectangle rectangle = this.TDRectangle;
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                default:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        rectangle.Width,
                        iHeight);
            }
        }

        public override bool AutoPlanTextRectangle
        {
            get
            {
                return true;
            }
            set
            {
                base.AutoPlanTextRectangle = true;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.m_TextRectangle = this.GetTextRectangleEx(pevent.Graphics);

            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDescriptionButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eImage:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    break;
                case DisplayStyle.eText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Description, this.ForeCustomize, this.DescriptionForeColor, this.ShadowColor, this.DescriptionFont, this.DescriptionRectangle));
                    break;
                case DisplayStyle.eImageAndText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Description, this.ForeCustomize, this.DescriptionForeColor, this.ShadowColor, this.DescriptionFont, this.DescriptionRectangle));
                    break;
                default:
                    break;
            }
        }

    }
}

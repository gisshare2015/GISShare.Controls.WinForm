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
    public class DescriptionButton : GISShare.Controls.WinForm.WFNew.BaseButtonN, IDescriptionButtonItem
    {
        public DescriptionButton()
        {
            this.Padding = new Padding(3);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
        }

        #region IDescriptionButtonItem
        private Color m_DescriptionForeColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体颜色"), Category("外观")]
        public Color DescriptionForeColor
        {
            get { return m_DescriptionForeColor; }
            set { m_DescriptionForeColor = value; }
        }

        private Font m_DescriptionFont = new Font("宋体", 9f);
        [Browsable(true), DefaultValue(typeof(Font), "\"宋体\", 9f"), Description("字体"), Category("外观")]
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
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Description, this.DescriptionForeColor, this.DescriptionFont, this.DescriptionRectangle));
                    break;
                case DisplayStyle.eImageAndText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Description, this.DescriptionForeColor, this.DescriptionFont, this.DescriptionRectangle));
                    break;
                default:
                    break;
            }
        }

    }
}

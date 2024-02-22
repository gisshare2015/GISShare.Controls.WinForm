using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace GISShare.Controls.WinForm.WFNew
{
    public class WFNewRenderer2007 : WFNewRenderer
    {
        private WFNewColorTable m_WFNewColorTable;
        public override WFNewColorTable WFNewColorTable
        {
            get { return m_WFNewColorTable; }
        }

        public WFNewRenderer2007(WFNewColorTable ribbonColorTable)
        {
            this.m_WFNewColorTable = ribbonColorTable;
        }

        public WFNewRenderer2007()
            : this(new WFNewColorTable2007())
        { }

        #region RibbonArea
        public override void OnRenderRibbonArea(ObjectRenderEventArgs e)
        {
            WFNew.IArea2 pRibbonArea = e.Object as WFNew.IArea2;
            if (pRibbonArea.ShowBackground)
            {
                using (SolidBrush b = new SolidBrush(pRibbonArea.AreaCustomize ? pRibbonArea.BackgroundColor : this.WFNewColorTable.RibbonAreaDisabledBackground))
                {
                    e.Graphics.FillRectangle(b, e.Bounds);
                }
            }
            if (pRibbonArea.BackgroundImage != null && !(pRibbonArea is Control))
            {
                Rectangle rectangle = pRibbonArea.FrameRectangle;
                switch (pRibbonArea.BackgroundImageLayout)
                {
                    case ImageLayout.Center:
                        rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        rectangle = new Rectangle(rectangle.X + (rectangle.Width - pRibbonArea.BackgroundImage.Width), rectangle.Y + (rectangle.Height - pRibbonArea.BackgroundImage.Height), pRibbonArea.BackgroundImage.Width, pRibbonArea.BackgroundImage.Height);
                        this.OnRenderRibbonImage(new ImageRenderEventArgs(e.Graphics, this, ((IBaseItem)pRibbonArea).Enabled, pRibbonArea.BackgroundImage, rectangle));
                        break;
                    case ImageLayout.Stretch:
                        rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        this.OnRenderRibbonImage(new ImageRenderEventArgs(e.Graphics, this, ((IBaseItem)pRibbonArea).Enabled, pRibbonArea.BackgroundImage, rectangle));
                        break;
                    case ImageLayout.Tile:
                        rectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, pRibbonArea.BackgroundImage.Width, pRibbonArea.BackgroundImage.Height);
                        this.OnRenderRibbonImage(new ImageRenderEventArgs(e.Graphics, this, ((IBaseItem)pRibbonArea).Enabled, pRibbonArea.BackgroundImage, rectangle));
                        break;
                    case ImageLayout.Zoom:
                        rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        this.OnRenderRibbonImage(new ImageRenderEventArgs(e.Graphics, this, ((IBaseItem)pRibbonArea).Enabled, pRibbonArea.BackgroundImage, rectangle));
                        break;
                    case ImageLayout.None:
                    default:
                        rectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, pRibbonArea.BackgroundImage.Width, pRibbonArea.BackgroundImage.Height);
                        this.OnRenderRibbonImage(new ImageRenderEventArgs(e.Graphics, this, ((IBaseItem)pRibbonArea).Enabled, pRibbonArea.BackgroundImage, rectangle));
                        break;
                }
            }
            if (pRibbonArea.ShowOutLine)
            {
                using (Pen p = new Pen(pRibbonArea.AreaCustomize ? pRibbonArea.OutLineColor : this.WFNewColorTable.RibbonAreaOutLine))
                {
                    e.Graphics.DrawRectangle(p, pRibbonArea.FrameRectangle);
                }
            }
        }
        #endregion

        #region Form
        public override void OnRenderFormNC(ObjectRenderEventArgs e)
        {
            IForm pForm = e.Object as IForm;
            if (pForm == null) return;
            //return;
            //
            if (pForm.IsActive) this.DrawRenderFormActive(e.Graphics, pForm, e.Bounds);
            else this.DrawRenderFormUnActive(e.Graphics, pForm, e.Bounds);
        }
        private void DrawRenderFormActive(Graphics g, WFNew.IForm pForm, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            // 内部渲染
            //
            int iLeftTopRadius;
            int iRightTopRadius;
            int iLeftBottomRadius;
            int iRightBottomRadius;
            pForm.GetRadiusInfo(out iLeftTopRadius, out iRightTopRadius, out iLeftBottomRadius, out  iRightBottomRadius);
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                      this.WFNewColorTable.FormActiveCaptionBegin, this.WFNewColorTable.FormActiveCaptionEnd, LinearGradientMode.Vertical))//Color.FromArgb(228, 235, 246)  Color.FromArgb(217, 231, 249)
                {
                    g.FillPath(b, path);
                }
                //
                Rectangle rectangleCaption = pForm.CaptionRectangle;
                if (rectangleCaption.Width > 0 && rectangleCaption.Height > 0)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangleCaption,
                        this.WFNewColorTable.FormActiveCaptionBegin, this.WFNewColorTable.FormActiveCaptionEnd, LinearGradientMode.Vertical))//Color.FromArgb(202, 222, 247) Color.FromArgb(227, 240, 253)
                    {
                        g.FillRectangle(b, rectangleCaption);
                    }
                }
            }
            //
            //边线
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.FormActiveOutLine))//Color.FromArgb(59, 90, 130)
                {
                    p.Width = 2;
                    g.DrawPath(p, path);
                }
            }
            Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            if (rectangle2.Width > 0 && rectangle2.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle2, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.FormActiveIntLine))//Color.FromArgb(177, 198, 225)
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            //Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            //if (rectangle2.Width > 0 && rectangle2.Height > 0)
            //{
            //    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle2, 9, 9, 9, 9))
            //    {
            //        using (Pen p = new Pen(this.WFNewColorTable.FormActiveMiddleLine))//Color.FromArgb(177, 198, 225)
            //        {
            //            g.DrawPath(p, path);
            //        }
            //    }
            //}
            //Rectangle rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y + 1, rectangle2.Width - 1, rectangle2.Height - 1);
            //if (rectangle3.Width > 0 && rectangle3.Height > 0)
            //{
            //    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle3, 9, 9, 9, 9))
            //    {
            //        using (Pen p = new Pen(this.WFNewColorTable.FormActiveIntLine))//Color.FromArgb(194, 217, 247)
            //        {
            //            p.Width = 2;
            //            g.DrawPath(p, path);
            //        }
            //    }
            //}
        }
        private void DrawRenderFormUnActive(Graphics g, WFNew.IForm pForm, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            // 内部渲染
            //
            int iLeftTopRadius;
            int iRightTopRadius;
            int iLeftBottomRadius;
            int iRightBottomRadius;
            pForm.GetRadiusInfo(out iLeftTopRadius, out iRightTopRadius, out iLeftBottomRadius, out  iRightBottomRadius);
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                       this.WFNewColorTable.FormUnActiveCaptionBegin, this.WFNewColorTable.FormUnActiveCaptionEnd, LinearGradientMode.Vertical))//Color.FromArgb(227, 231, 236) Color.FromArgb(222, 229, 237)
                {
                    g.FillPath(b, path);
                }
                //
                Rectangle rectangleCaption = pForm.CaptionRectangle;
                if (rectangleCaption.Width > 0 && rectangleCaption.Height > 0)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangleCaption,
                            this.WFNewColorTable.FormUnActiveCaptionBegin, this.WFNewColorTable.FormUnActiveCaptionEnd, LinearGradientMode.Vertical))//Color.FromArgb(217, 226, 236) Color.FromArgb(226, 232, 239)
                    {
                        g.FillRectangle(b, rectangleCaption);
                    }
                }
            }
            //
            //边线
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.FormUnActiveOutLine))//Color.FromArgb(151, 165, 183)
                {
                    p.Width = 2;
                    g.DrawPath(p, path);
                }
            }
            Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            if (rectangle2.Width > 0 && rectangle2.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle2, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.FormUnActiveIntLine))//Color.FromArgb(204, 214, 226)
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            //Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            //if (rectangle2.Width > 0 && rectangle2.Height > 0)
            //{
            //    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle2, 9, 9, 9, 9))
            //    {
            //        using (Pen p = new Pen(this.WFNewColorTable.FormUnActiveMiddleLine))//Color.FromArgb(204, 214, 226)
            //        {
            //            g.DrawPath(p, path);
            //        }
            //    }
            //}
            //Rectangle rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y + 1, rectangle2.Width - 1, rectangle2.Height - 1);
            //if (rectangle3.Width > 0 && rectangle3.Height > 0)
            //{
            //    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle3, 9, 9, 9, 9))
            //    {
            //        using (Pen p = new Pen(this.WFNewColorTable.FormUnActiveIntLine))//Color.FromArgb(212, 222, 236)
            //        {
            //            p.Width = 2;
            //            g.DrawPath(p, path);
            //        }
            //    }
            //}
        }

        public override void OnRenderFormNCIcon(IconRenderEventArgs e)
        {
            if (e.Icon == null) return;
            //
            e.Graphics.DrawIcon(e.Icon, e.IconBounds);
        }

        public override void OnRenderFormNCCaptionText(TextRenderEventArgs e)
        {
            //IForm pForm = e.Object as IForm;
            //if (pForm == null || e.Text == null) return;
            //StringFormat stringFormat = new StringFormat();
            //stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            //Font font = new Font(SystemFonts.CaptionFont, FontStyle.Regular);
            //Rectangle rectangle = Rectangle.FromLTRB
            //    (
            //    e.TextBounds.Left,
            //    (e.TextBounds.Top + e.TextBounds.Bottom - (int)e.Graphics.MeasureString(e.Text, font).Height + 1) / 2,
            //    e.TextBounds.Right, 
            //    e.TextBounds.Bottom
            //    );
            //Rectangle rectangle = e.TextBounds;
            if (e.Enabled)
            {
                if (e.IsMiddle)
                {
                    TextRenderer.DrawText
                        (
                        e.Graphics,
                        e.Text,
                        e.ForeCustomize ? e.Font : new Font(SystemFonts.CaptionFont, FontStyle.Regular),
                        e.TextBounds,
                        e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.FormCaptionText
                        );
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.FormCaptionText))
                    {
                        e.Graphics.DrawString
                            (
                            e.Text,
                            e.ForeCustomize ? e.Font : new Font(SystemFonts.CaptionFont, FontStyle.Regular), 
                            b, 
                            e.TextBounds,
                            e.StringFormat
                            );
                    }
                }
            }
            else
            {
                if (e.IsMiddle)
                {
                    TextRenderer.DrawText
                        (
                        e.Graphics,
                        e.Text,
                        e.ForeCustomize ? e.Font : new Font(SystemFonts.CaptionFont, FontStyle.Regular),
                        e.TextBounds,
                        this.WFNewColorTable.FormDisabledCaptionText
                        );
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.FormDisabledCaptionText))
                    {
                        e.Graphics.DrawString
                            (
                            e.Text,
                            e.ForeCustomize ? e.Font : new Font(SystemFonts.CaptionFont, FontStyle.Regular), 
                            b,
                            e.TextBounds, 
                            e.StringFormat
                            );
                    }
                }
            }
        }

        public override void OnRenderForm(ObjectRenderEventArgs e)
        {
            //using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaDisabledBackground))
            //{
            //    e.Graphics.FillRectangle(b, e.Bounds);
            //}
        }
        #endregion

        #region RibbonControl
        public override void OnRenderRibbonControl(ObjectRenderEventArgs e)
        {
            IRibbonControl pRibbonControl = e.Object as IRibbonControl;
            if (pRibbonControl == null) return;
            //
            if (pRibbonControl.Enabled) { this.DrawRenderRibbonControl(e.Graphics, pRibbonControl, e.Bounds); }
            else { this.DrawRenderRibbonControlDisabled(e.Graphics, pRibbonControl, e.Bounds); }
        }
        private void DrawRenderRibbonControl(Graphics g, WFNew.IRibbonControl pRibbonControl, Rectangle rectangle)
        {
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaBackground))
            {
                g.FillRectangle(b, rectangle);
            }
            //
            rectangle = pRibbonControl.CaptionRectangle;
            if ((rectangle.Width > 0) && (rectangle.Height > 0))
            {
                if (pRibbonControl.IsActive)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                        this.WFNewColorTable.FormActiveCaptionBegin, this.WFNewColorTable.FormActiveCaptionEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                }
                else
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                        this.WFNewColorTable.FormUnActiveCaptionBegin, this.WFNewColorTable.FormUnActiveCaptionEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                }
            }
            //
            if (!pRibbonControl.HideRibbonPage)
            {
                rectangle = pRibbonControl.MiddleCompnonentRectangle;
                if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
                rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height - 2);
                Rectangle intRectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, (int)(intRectangle.Height / 4.5));
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle,
                       pRibbonControl.LeftTopRadius, pRibbonControl.RightTopRadius, pRibbonControl.LeftBottomRadius, pRibbonControl.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, rectangle.Top + 30), new Point(0, rectangle.Bottom - 10),
                        this.WFNewColorTable.RibbonControlPagesBackgroundBegin, this.WFNewColorTable.RibbonControlPagesBackgroundEnd))
                    {
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path);
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonControlOutLine))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            //
            if (pRibbonControl.IsTopToolbar) return;
            //
            rectangle = pRibbonControl.BottomCompnonentRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            rectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, rectangle.Height - 2);
            if ((rectangle.Width > 0) && (rectangle.Height > 0))
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle,
                    pRibbonControl.LeftTopRadius, pRibbonControl.RightTopRadius, pRibbonControl.LeftBottomRadius, pRibbonControl.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonControlOutLine))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawRenderRibbonControlDisabled(Graphics g, WFNew.IRibbonControl pRibbonControl, Rectangle rectangle)
        {
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaDisabledBackground))
            {
                g.FillRectangle(b, rectangle);
            }
            //
            rectangle = pRibbonControl.CaptionRectangle;
            if ((rectangle.Width > 0) && (rectangle.Height > 0))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                    this.WFNewColorTable.FormUnActiveCaptionBegin, this.WFNewColorTable.FormUnActiveCaptionEnd, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(b, rectangle);
                }
            }
            //
            if (!pRibbonControl.HideRibbonPage)
            {
                rectangle = pRibbonControl.MiddleCompnonentRectangle;
                if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
                rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height - 2);
                Rectangle intRectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, (int)(intRectangle.Height / 4.5));
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle,
                       pRibbonControl.LeftTopRadius, pRibbonControl.RightTopRadius, pRibbonControl.LeftBottomRadius, pRibbonControl.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, rectangle.Top + 30), new Point(0, rectangle.Bottom - 10),
                        this.WFNewColorTable.RibbonControlPagesDisabledBackgroundBegin, this.WFNewColorTable.RibbonControlPagesDisabledBackgroundEnd))
                    {
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path);
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonControlDisabledOutLine))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            //
            if (pRibbonControl.IsTopToolbar) return;
            //
            rectangle = pRibbonControl.BottomCompnonentRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            rectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, rectangle.Height - 2);
            if ((rectangle.Width > 0) && (rectangle.Height > 0))
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle,
                    pRibbonControl.LeftTopRadius, pRibbonControl.RightTopRadius, pRibbonControl.LeftBottomRadius, pRibbonControl.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonControlDisabledOutLine))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }

        //public override void OnRenderRibbonControlCaptionIcon(IconRenderEventArgs e)
        //{
        //    if (e.Icon == null) return;
        //    //
        //    e.Graphics.DrawIcon(e.Icon, e.IconBounds);
        //}

        //public override void OnRenderRibbonControlCaptionText(TextRenderEventArgs e)
        //{
        //    if (e.Text == null) return;
        //    if (e.Enabled)
        //    {
        //        TextRenderer.DrawText
        //            (
        //            e.Graphics,
        //            e.Text,
        //            new Font(SystemFonts.CaptionFont, FontStyle.Regular),
        //            e.TextBounds,
        //            this.WFNewColorTable.FormCaptionText
        //            );
        //    }
        //    else
        //    {
        //        TextRenderer.DrawText
        //            (
        //            e.Graphics,
        //            e.Text,
        //            new Font(SystemFonts.CaptionFont, FontStyle.Regular),
        //            e.TextBounds,
        //            this.WFNewColorTable.FormCaptionText
        //            );
        //    }
        //}
        #endregion

        #region RibbonPageContainerPopup
        public override void OnRenderRibbonPageContainerPopup(ObjectRenderEventArgs e)
        {
            WFNew.IBaseItem pBaseItem = e.Object as WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            //
            if (pBaseItem.Enabled)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                    this.WFNewColorTable.RibbonControlPagesBackgroundBegin, this.WFNewColorTable.RibbonControlPagesBackgroundEnd))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.FillRectangle(b, outRectangle);
                }
                Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                {
                    e.Graphics.FillRectangle(b, glossy);
                }
            }
            else
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                    this.WFNewColorTable.RibbonControlPagesDisabledBackgroundBegin, this.WFNewColorTable.RibbonControlPagesDisabledBackgroundEnd))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.FillRectangle(b, outRectangle);
                }
                Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                using (Brush b = new SolidBrush(Color.FromArgb(120, Color.White)))
                {
                    e.Graphics.FillRectangle(b, glossy);
                }
            }
        }
        #endregion

        #region RibbonStartButton2007
        public override void OnRenderRibbonStartButton2007(ObjectRenderEventArgs e)
        {
            WFNew.IStartButtonItem pStartButtonItem = e.Object as WFNew.IStartButtonItem;
            if (pStartButtonItem == null) return;
            //
            //Rectangle rectangle = pStartButtonItem.DisplayRectangle;
            Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 2);
            int sweep, start;
            Point p1, p2, p3;
            Color bgdark, bgmed, bglight, light;
            Rectangle rinner = rectangle;
            rinner.Inflate(-1, -1);
            Rectangle shadow = rectangle;
            shadow.Offset(1, 1);
            shadow.Inflate(1, 1);

            #region Color Selection
            switch (pStartButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    bgdark = this.WFNewColorTable.StartButton2007DisabledBackgroundDark;
                    bgmed = this.WFNewColorTable.StartButton2007DisabledBackgroundCenter;
                    bglight = this.WFNewColorTable.StartButton2007DisabledBackgroundLight;
                    light = this.WFNewColorTable.StartButton2007DisabledLight;
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    bgdark = this.WFNewColorTable.StartButton2007SelectedBackgroundDark;
                    bgmed = this.WFNewColorTable.StartButton2007SelectedBackgroundDark;
                    bglight = this.WFNewColorTable.StartButton2007SelectedBackgroundLight;
                    light = this.WFNewColorTable.StartButton2007SelectedLight;
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    bgdark = this.WFNewColorTable.StartButton2007PressedBackgroundDark;
                    bgmed = this.WFNewColorTable.StartButton2007PressedBackgroundCenter;
                    bglight = this.WFNewColorTable.StartButton2007PressedBackgroundLight;
                    light = this.WFNewColorTable.StartButton2007PressedLight;
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (pStartButtonItem.Checked)
                    {
                        bgdark = this.WFNewColorTable.StartButton2007CheckedBackgroundDark;
                        bgmed = this.WFNewColorTable.StartButton2007CheckedBackgroundCenter;
                        bglight = this.WFNewColorTable.StartButton2007CheckedBackgroundLight;
                        light = this.WFNewColorTable.StartButton2007CheckedLight;
                    }
                    else
                    {
                        bgdark = this.WFNewColorTable.StartButton2007NomalBackgroundDark;
                        bgmed = this.WFNewColorTable.StartButton2007NomalBackgroundCenter;
                        bglight = this.WFNewColorTable.StartButton2007NomalBackgroundLight;
                        light = this.WFNewColorTable.StartButton2007NomalLight;
                    }
                    break;
            }
            #endregion

            #region Shadow
            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(shadow);
                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(shadow.Left + shadow.Width / 2, shadow.Top + shadow.Height / 2);
                    gradient.CenterColor = Color.FromArgb(180, Color.Black);
                    gradient.SurroundColors = new Color[] { Color.Transparent };

                    Blend blend = new Blend(3);
                    blend.Factors = new float[] { 0f, 1f, 1f };
                    blend.Positions = new float[] { 0, 0.2f, 1f };
                    gradient.Blend = blend;

                    e.Graphics.FillPath(gradient, p);
                }
            }
            #endregion

            #region Orb Background
            using (Pen p = new Pen(bgdark, 1))
            {
                e.Graphics.DrawEllipse(p, rectangle);
            }

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(rectangle);
                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rectangle.Left + rectangle.Width / 2), Convert.ToSingle(rectangle.Bottom));
                    gradient.CenterColor = bglight;
                    gradient.SurroundColors = new Color[] { bgmed };

                    Blend blend = new Blend(3);
                    blend.Factors = new float[] { 0f, .8f, 1f };
                    blend.Positions = new float[] { 0, 0.50f, 1f };
                    gradient.Blend = blend;

                    e.Graphics.FillPath(gradient, p);
                }
            }
            #endregion

            #region Bottom round shine
            Rectangle bshine = new Rectangle(0, 0, rectangle.Width / 2, rectangle.Height / 2);
            bshine.X = rectangle.X + (rectangle.Width - bshine.Width) / 2;
            bshine.Y = rectangle.Y + rectangle.Height / 2;

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(bshine);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rectangle.Left + rectangle.Width / 2), Convert.ToSingle(rectangle.Bottom));
                    gradient.CenterColor = Color.White;
                    gradient.SurroundColors = new Color[] { Color.Transparent };

                    e.Graphics.FillPath(gradient, p);
                }
            }
            #endregion

            #region Upper Glossy
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                p1 = Point.Round(p.PathData.Points[0]);
                p2 = Point.Round(p.PathData.Points[p.PathData.Points.Length - 1]);
                p3 = new Point(rinner.Left + rinner.Width / 2, p2.Y - 3);
                p.AddCurve(new Point[] { p2, p3, p1 });

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = p3;
                    gradient.CenterColor = Color.Transparent;
                    gradient.SurroundColors = new Color[] { light };

                    Blend blend = new Blend(3);
                    blend.Factors = new float[] { .3f, .8f, 1f };
                    blend.Positions = new float[] { 0, 0.50f, 1f };
                    gradient.Blend = blend;

                    e.Graphics.FillPath(gradient, p);
                }

                using (LinearGradientBrush b = new LinearGradientBrush(new Point(rectangle.Left, rectangle.Top), new Point(rectangle.Left, p1.Y), Color.White, Color.Transparent))
                {
                    Blend blend = new Blend(4);
                    blend.Factors = new float[] { 0f, .4f, .8f, 1f };
                    blend.Positions = new float[] { 0f, .3f, .4f, 1f };
                    b.Blend = blend;
                    e.Graphics.FillPath(b, p);
                }
            }
            #endregion

            #region Upper Shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                using (Pen pen = new Pen(Color.White))
                {
                    e.Graphics.DrawPath(pen, p);
                }
            }
            #endregion

            #region Lower Shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);
                Point pt = Point.Round(p.PathData.Points[0]);

                Rectangle rrinner = rinner; rrinner.Inflate(-1, -1);
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rrinner, start, sweep);

                using (LinearGradientBrush b = new LinearGradientBrush(new Point(rinner.Left, rinner.Bottom), new Point(rinner.Left, pt.Y - 1), light, Color.FromArgb(50, light)))
                {
                    e.Graphics.FillPath(b, p);
                }
            }

            #endregion
        }
        #endregion

        #region RibbonStartButton2010
        public override void OnRenderRibbonStartButton2010(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
            //            
            Rectangle rectangle = new Rectangle((e.Bounds.Left + e.Bounds.Right - 22) / 2, (e.Bounds.Top + e.Bounds.Bottom - 11) / 2, 20, 10);
            using (SolidBrush b = new SolidBrush(Color.FromArgb(220, this.WFNewColorTable.StartButton2010GraphicBackground)))
            {
                e.Graphics.FillRectangle(b, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 6, 11));
            }
            using (Pen p = new Pen(this.WFNewColorTable.StartButton2010GraphicDarkLine))
            {
                e.Graphics.DrawLine(p, rectangle.X + 6, rectangle.Y + 2, rectangle.X + 11, rectangle.Y + 2);
                e.Graphics.DrawLine(p, rectangle.X + 6, rectangle.Y + 4, rectangle.X + 11, rectangle.Y + 4);
                e.Graphics.DrawLine(p, rectangle.X + 6, rectangle.Y + 6, rectangle.X + 11, rectangle.Y + 6);
                e.Graphics.DrawLine(p, rectangle.X + 6, rectangle.Y + 8, rectangle.X + 11, rectangle.Y + 8);
            }
            using (Pen p = new Pen(this.WFNewColorTable.StartButton2010GraphicArrow))
            {
                e.Graphics.DrawLine(p, rectangle.X + 18, rectangle.Y + 4, rectangle.X + 24, rectangle.Y + 4);
                e.Graphics.DrawLine(p, rectangle.X + 19, rectangle.Y + 5, rectangle.X + 23, rectangle.Y + 5);
                e.Graphics.DrawLine(p, rectangle.X + 20, rectangle.Y + 6, rectangle.X + 22, rectangle.Y + 6);
                e.Graphics.DrawLine(p, rectangle.X + 21, rectangle.Y + 4, rectangle.X + 21, rectangle.Y + 7);
            }
            using (Pen p = new Pen(Color.FromArgb(160, this.WFNewColorTable.StartButton2010GraphicDarkLine)))
            {
                e.Graphics.DrawLine(p, rectangle.X + 4, rectangle.Top + 1, rectangle.X + 4, rectangle.Bottom - 1);
            }
        }
        #endregion

        #region RibbonApplicationPopupPanel
        public override void OnRenderRibbonApplicationPopupPanel(ObjectRenderEventArgs e)
        {
            Controls.WinForm.WFNew.IRibbonApplicationPopupPanelItem pRibbonApplicationPopupPanel = e.Object as Controls.WinForm.WFNew.IRibbonApplicationPopupPanelItem;
            if (pRibbonApplicationPopupPanel == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonApplicationPopupPanelBackground))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            Rectangle rectangle = pRibbonApplicationPopupPanel.MenuItemsRectangle;
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonApplicationPopupPanelMenuItemsBackground))
            {
                e.Graphics.FillRectangle(b, rectangle);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(pRibbonApplicationPopupPanel.TopRectangle,
                this.WFNewColorTable.RibbonApplicationPopupPanelBackgroundBegin, this.WFNewColorTable.RibbonApplicationPopupPanelBackgroundEnd, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, pRibbonApplicationPopupPanel.TopRectangle);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(pRibbonApplicationPopupPanel.BottomRectangle,
                this.WFNewColorTable.RibbonApplicationPopupPanelBackgroundEnd, this.WFNewColorTable.RibbonApplicationPopupPanelBackgroundBegin, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, pRibbonApplicationPopupPanel.BottomRectangle);
            }
            Rectangle rectangle2 = pRibbonApplicationPopupPanel.FrameRectangle;
            using (Pen p = new Pen(this.WFNewColorTable.RibbonApplicationPopupPanelSeparatorDark))
            {
                e.Graphics.DrawLine(p, new Point(rectangle2.Left, rectangle.Top - 1), new Point(rectangle2.Right, rectangle.Top - 1));
                e.Graphics.DrawLine(p, new Point(rectangle.Right + 1, rectangle.Top), new Point(rectangle.Right + 1, rectangle.Bottom));
                e.Graphics.DrawLine(p, new Point(rectangle2.Left, rectangle.Bottom + 1), new Point(rectangle2.Right, rectangle.Bottom + 1));
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonApplicationPopupPanelSeparatorLight))
            {
                e.Graphics.DrawLine(p, new Point(rectangle.Right + 2, rectangle.Top), new Point(rectangle.Right + 2, rectangle.Bottom));
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle2, pRibbonApplicationPopupPanel.LeftTopRadius, pRibbonApplicationPopupPanel.RightTopRadius, pRibbonApplicationPopupPanel.LeftBottomRadius, pRibbonApplicationPopupPanel.RightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonApplicationPopupPanelBorber))//
                {
                    e.Graphics.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region DescriptionMenuPopupPanel
        public override void OnRenderDescriptionMenuPopupPanel(ObjectRenderEventArgs e)
        {
            WFNew.IDescriptionPopupPanelItem pDescriptionPopupPanelItem = e.Object as WFNew.IDescriptionPopupPanelItem;
            if (pDescriptionPopupPanelItem == null) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pDescriptionPopupPanelItem.FrameRectangle,
                pDescriptionPopupPanelItem.LeftTopRadius, pDescriptionPopupPanelItem.RightTopRadius, pDescriptionPopupPanelItem.LeftBottomRadius, pDescriptionPopupPanelItem.RightBottomRadius))
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.DescriptionMenuPopupPanelBackground))
                {
                    e.Graphics.FillPath(b, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.DescriptionMenuPopupPanelBorber))//
                {
                    e.Graphics.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region FormButton RibbonControl MinButton MaxButton HelpButton CloseButton
        public override void OnRenderFormButton(ObjectRenderEventArgs e)
        {
            WFNew.IFormButton pFormButton = e.Object as WFNew.IFormButton;
            if (pFormButton == null) return;
            //
            switch (pFormButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pFormButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pFormButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pFormButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pFormButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pFormButton, e.Bounds, 2); }
                    else if (pFormButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pFormButton, e.Bounds, 2); }
                    break;
            }
            //
            Form form;
            switch (pFormButton.eFormButtonStyle)
            {
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMinButton:
                    form = pFormButton.OperationForm;
                    if (form != null && form.IsMdiChild && form.WindowState == FormWindowState.Minimized)
                    {
                        this.DrawNormalButton(e.Graphics, pFormButton.GlyphRectangle);
                    }
                    else
                    {
                        this.DrawMinButton(e.Graphics, pFormButton.GlyphRectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMaxButton:
                    form = pFormButton.OperationForm;
                    if (form != null && form.WindowState == FormWindowState.Maximized)
                    {
                        this.DrawNormalButton(e.Graphics, pFormButton.GlyphRectangle);
                    }
                    else
                    {
                        this.DrawMaxButton(e.Graphics, pFormButton.GlyphRectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eHelpButton:
                    this.DrawHelpButton(e.Graphics, pFormButton.GlyphRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eCloseButton:
                    this.DrawCloseButton(e.Graphics, pFormButton.GlyphRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiMinButton:
                    this.DrawMinButton(e.Graphics, pFormButton.GlyphRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiMaxButton:
                    this.DrawNormalButton(e.Graphics, pFormButton.GlyphRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiCloseButton:
                    this.DrawCloseButton(e.Graphics, pFormButton.GlyphRectangle);
                    break;
                default:
                    break;
            }
        }
        private void DrawMinButton(Graphics g, Rectangle rectangle)
        {
            int iW = (int)(2 * ((double)rectangle.Width / 3));
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 2, rectangle.Left + iW, rectangle.Bottom - 2);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Left + iW, rectangle.Bottom - 1);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Left + iW, rectangle.Bottom);
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 3, rectangle.Left + iW, rectangle.Bottom - 3);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 2, rectangle.Left + iW, rectangle.Bottom - 2);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Left + iW, rectangle.Bottom - 1);
            }
        }
        private void DrawNormalButton(Graphics g, Rectangle rectangle)
        {
            int iS = (int)((double)rectangle.Width / 3);
            Rectangle rectangle2 = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + iS + 1, rectangle.Right - iS + 1, rectangle.Bottom + 1);
            rectangle.Offset(1, 1);
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Left + iS, rectangle2.Top);
                g.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle2.Right, rectangle2.Top + iS, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                //
                g.DrawRectangle(p, rectangle2);
                g.DrawLine(p, rectangle2.Left, rectangle2.Top + 1, rectangle2.Right, rectangle2.Top + 1);
            }
            //
            rectangle2.Offset(-1, -1);
            rectangle.Offset(-1, -1);
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Left + iS, rectangle2.Top);
                g.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle2.Right, rectangle2.Top + iS, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                //
                g.DrawRectangle(p, rectangle2);
                g.DrawLine(p, rectangle2.Left, rectangle2.Top + 1, rectangle2.Right, rectangle2.Top + 1);
            }
        }
        private void DrawMaxButton(Graphics g, Rectangle rectangle)
        {
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left, rectangle.Top + 3, rectangle.Right, rectangle.Top + 3);
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 2, rectangle.Right, rectangle.Top + 2);
            }
        }
        private void DrawHelpButton(Graphics g, Rectangle rectangle)
        {
            Font f = new Font("Marlett", rectangle.Height, FontStyle.Bold);
            SizeF size = g.MeasureString("s", f);
            rectangle = new Rectangle(
                (int)(rectangle.Left + rectangle.Right - size.Width) / 2,
                (int)(rectangle.Top + rectangle.Bottom - size.Height) / 2 + 1,
                (int)size.Width,
                (int)size.Height);
            rectangle.Offset(1, 1);
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
            {
                g.DrawString("s", f, b, rectangle, new StringFormat());
            }
            rectangle.Offset(-1, -1);
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
            {
                g.DrawString("s", f, b, rectangle, new StringFormat());
            }
        }
        private void DrawCloseButton(Graphics g, Rectangle rectangle)
        {
            rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right + 1, rectangle.Bottom + 1);
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                g.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom);
                //
                g.DrawLine(p, rectangle.Left + 1, rectangle.Bottom, rectangle.Right, rectangle.Top + 1);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Top);
            }
            rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top - 1, rectangle.Right - 1, rectangle.Bottom - 1);
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(p, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                g.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom);
                //
                g.DrawLine(p, rectangle.Left + 1, rectangle.Bottom, rectangle.Right, rectangle.Top + 1);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Top);
            }

        }
        #region old
        //public override void OnRenderFormButton(ObjectRenderEventArgs e)
        //{
        //    WFNew.IFormButton pFormButton = e.Object as WFNew.IFormButton;
        //    if (pFormButton == null) return;
        //    //
        //    switch (pFormButton.eBaseItemState)
        //    {
        //        case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
        //            this.DrawBaseButtonSelected(e.Graphics, pFormButton, e.Bounds, 2);
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
        //            this.DrawBaseButtonPressed(e.Graphics, pFormButton, e.Bounds, 2);
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
        //            this.DrawBaseButtonDisabled(e.Graphics, pFormButton, e.Bounds, 2);
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
        //        default:
        //            if (!pFormButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pFormButton, e.Bounds, 2); }
        //            else if (pFormButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pFormButton, e.Bounds, 2); }
        //            break;
        //    }
        //    //
        //    Font f;
        //    Rectangle r;
        //    Rectangle cbr;
        //    Form form;
        //    switch (pFormButton.eFormButtonStyle)
        //    {
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMinButton:
        //            form = pFormButton.OperationForm;
        //            if (form != null && form.IsMdiChild && form.WindowState == FormWindowState.Minimized)
        //            {
        //                f = new Font("Marlett", 7);
        //                Rectangle r1 = new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 5, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //                Rectangle cbr1 = r1;
        //                cbr1.Y++;
        //                Rectangle r2 = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 8, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //                Rectangle cbr2 = r2;
        //                cbr2.Y++;
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))//((WFNewColorTable)ColorTable).ArrowLight
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr1, new StringFormat());
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr2, new StringFormat());
        //                }
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r1, new StringFormat());
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r2, new StringFormat());
        //                }
        //            }
        //            else
        //            {
        //                f = new Font("Marlett", 10);
        //                r = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height);
        //                cbr = r;
        //                cbr.Y++;
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
        //                {
        //                    e.Graphics.DrawString("0", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //                }
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //                {
        //                    e.Graphics.DrawString("0", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //                }
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMaxButton:
        //            form = pFormButton.OperationForm;
        //            if (form != null && form.WindowState == FormWindowState.Maximized)
        //            {
        //                f = new Font("Marlett", 7);
        //                Rectangle r1 = new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 5, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //                Rectangle cbr1 = r1;
        //                cbr1.Y++;
        //                Rectangle r2 = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 8, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //                Rectangle cbr2 = r2;
        //                cbr2.Y++;
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))//((WFNewColorTable)ColorTable).ArrowLight
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr1, new StringFormat());
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr2, new StringFormat());
        //                }
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r1, new StringFormat());
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r2, new StringFormat());
        //                }
        //            }
        //            else
        //            {
        //                f = new Font("Marlett", 10);
        //                r = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height);
        //                cbr = r;
        //                cbr.Y++;
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))//((WFNewColorTable)ColorTable).ArrowLight
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //                }
        //                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //                {
        //                    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //                }
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eHelpButton:
        //            f = new Font("Marlett", 10);
        //            r = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height);
        //            cbr = r;
        //            cbr.Y++;
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
        //            {
        //                e.Graphics.DrawString("s", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //            }
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            {
        //                e.Graphics.DrawString("s", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eCloseButton:
        //            f = new Font("Marlett", 11);
        //            r = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 3, e.Bounds.Width, e.Bounds.Height);
        //            cbr = r;
        //            cbr.Y++;
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
        //            {
        //                e.Graphics.DrawString("r", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //            }
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            {
        //                e.Graphics.DrawString("r", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiMinButton:
        //            f = new Font("Marlett", 8);
        //            r = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height);
        //            cbr = r;
        //            cbr.Y++;
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
        //            {
        //                e.Graphics.DrawString("0", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //            }
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            {
        //                e.Graphics.DrawString("0", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiMaxButton:
        //            //f = new Font("Marlett", 8);
        //            //r = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height);
        //            //cbr = r;
        //            //cbr.Y++;
        //            //using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))//((WFNewColorTable)ColorTable).ArrowLight
        //            //{
        //            //    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //            //}
        //            //using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            //{
        //            //    e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //            //}
        //            f = new Font("Marlett", 6);
        //            Rectangle r11 = new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 5, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //            Rectangle cbr11 = r11;
        //            cbr11.Y++;
        //            Rectangle r22 = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 8, e.Bounds.Width / 2, e.Bounds.Height / 2);
        //            Rectangle cbr22 = r22;
        //            cbr22.Y++;
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))//((WFNewColorTable)ColorTable).ArrowLight
        //            {
        //                e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr11, new StringFormat());
        //                e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, cbr22, new StringFormat());
        //            }
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            {
        //                e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r11, new StringFormat());
        //                e.Graphics.DrawString("1", new Font(f, FontStyle.Bold), b, r22, new StringFormat());
        //            }
        //            break;
        //        case GISShare.Controls.WinForm.WFNew.FormButtonStyle.eMdiCloseButton:
        //            f = new Font("Marlett", 9);
        //            r = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height);
        //            cbr = r;
        //            cbr.Y++;
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
        //            {
        //                e.Graphics.DrawString("r", new Font(f, FontStyle.Bold), b, cbr, new StringFormat());
        //            }
        //            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
        //            {
        //                e.Graphics.DrawString("r", new Font(f, FontStyle.Bold), b, r, new StringFormat());
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
        #endregion
        #endregion

        #region RibbonQuickAccessToolbar
        public override void OnRenderRibbonQuickAccessToolbar(ObjectRenderEventArgs e)
        {
            WFNew.IQuickAccessToolbarItem pQuickAccessToolbarItem = e.Object as WFNew.IQuickAccessToolbarItem;
            if (pQuickAccessToolbarItem == null ||
                pQuickAccessToolbarItem.BaseItems.GetItemCount(true) <= 0 ||
                pQuickAccessToolbarItem.eQuickAccessToolbarStyle == GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eNone) return;
            Rectangle rectangleOut = pQuickAccessToolbarItem.FrameRectangle;
            if (rectangleOut.Width <= 0 || rectangleOut.Height <= 0) return;
            //rectangleOut = new Rectangle(rectangleOut.X, rectangleOut.Y, rectangleOut.Width - 1, rectangleOut.Height);
            Rectangle rectangleIn = new Rectangle(rectangleOut.X + 1, rectangleOut.Y + 1, rectangleOut.Width - 2, rectangleOut.Height - 2);
            if (rectangleIn.Width <= 0 || rectangleIn.Height <= 0) return;
            switch (pQuickAccessToolbarItem.eQuickAccessToolbarStyle)
            {
                case GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eAllRound:
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangleOut,
                        pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberLight))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangleIn,
                        pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth))
                    {
                        if (pQuickAccessToolbarItem.ShowBackground)
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangleIn,
                                this.WFNewColorTable.QuickAccessToolbarGripBegin, this.WFNewColorTable.QuickAccessToolbarGripEnd, 90))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberDark))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eHalfRound:
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddArc(rectangleOut.Left - pQuickAccessToolbarItem.RoundWidth, rectangleOut.Top, 2 * pQuickAccessToolbarItem.RoundWidth, 2 * pQuickAccessToolbarItem.RoundWidth, 270, 90);
                        path.Reverse();
                        path.AddArc(rectangleOut.Right - pQuickAccessToolbarItem.RoundWidth, rectangleOut.Top, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, 270, 90);
                        path.AddArc(rectangleOut.Right - pQuickAccessToolbarItem.RoundWidth, rectangleOut.Bottom - pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, 0, 90);
                        path.AddLine(rectangleOut.Right - pQuickAccessToolbarItem.RoundWidth, rectangleOut.Bottom, rectangleOut.Left + pQuickAccessToolbarItem.RoundWidth, rectangleOut.Bottom);
                        path.CloseFigure();
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberLight))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddArc(rectangleIn.Left - pQuickAccessToolbarItem.RoundWidth, rectangleIn.Top, 2 * pQuickAccessToolbarItem.RoundWidth, 2 * pQuickAccessToolbarItem.RoundWidth, 270, 90);
                        path.Reverse();
                        path.AddArc(rectangleIn.Right - pQuickAccessToolbarItem.RoundWidth, rectangleIn.Top, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, 270, 90);
                        path.AddArc(rectangleIn.Right - pQuickAccessToolbarItem.RoundWidth, rectangleIn.Bottom - pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, pQuickAccessToolbarItem.RoundWidth, 0, 90);
                        path.AddLine(rectangleIn.Right - pQuickAccessToolbarItem.RoundWidth, rectangleIn.Bottom, rectangleIn.Left + pQuickAccessToolbarItem.RoundWidth, rectangleIn.Bottom);
                        path.CloseFigure();
                        if (pQuickAccessToolbarItem.ShowBackground)
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangleIn,
                                this.WFNewColorTable.QuickAccessToolbarGripBegin, this.WFNewColorTable.QuickAccessToolbarGripEnd, 90))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberDark))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eNormal:
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangleOut, 10, 10, 10, 10))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberLight))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangleIn, 10, 10, 10, 10))
                    {
                        if (pQuickAccessToolbarItem.ShowBackground)
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangleIn,
                                this.WFNewColorTable.QuickAccessToolbarGripBegin, this.WFNewColorTable.QuickAccessToolbarGripEnd, 90))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.QuickAccessToolbarBorberDark))
                        {
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.QuickAccessToolbarStyle.eLineSeparator:
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
                    {
                        e.Graphics.DrawLine(p, rectangleOut.Left - 1, rectangleOut.Top + 1, rectangleOut.Left - 1, rectangleOut.Bottom - 1);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
                    {
                        e.Graphics.DrawLine(p, rectangleOut.Left, rectangleOut.Top + 1, rectangleOut.Left, rectangleOut.Bottom - 1);
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
                    {
                        e.Graphics.DrawLine(p, rectangleOut.Right - 1, rectangleOut.Top + 1, rectangleOut.Right - 1, rectangleOut.Bottom - 1);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
                    {
                        e.Graphics.DrawLine(p, rectangleOut.Right, rectangleOut.Top + 1, rectangleOut.Right, rectangleOut.Bottom - 1);
                    }
                    break;
                default:
                    break;
            }
        }

        public override void OnRenderCustomizeButton(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
            //
            Rectangle rectangle = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + (e.Bounds.Height - 2) / 2 + 3, 3, 3);
            Rectangle rectangle2 = new Rectangle(rectangle.X - 1, rectangle.Y - 4, 5, 2);
            Point[] points = null;
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
            {
                rectangle.X -= 1;
                rectangle.Width += 2;
                points = new Point[3];
                points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                points[2] = new Point(rectangle.Left, rectangle.Top);
                points[1] = new Point(rectangle.Right, rectangle.Top);
                e.Graphics.FillPolygon(b, points);
                //
                e.Graphics.FillRectangle(b, rectangle2);
            }
            rectangle = new Rectangle(rectangle.X + 1, rectangle.Y - 1, 3, 3);
            rectangle2 = new Rectangle(rectangle2.X, rectangle2.Y - 1, 5, 2);
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
            {
                rectangle.X -= 1;
                rectangle.Width += 2;
                points = new Point[3];
                points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                points[2] = new Point(rectangle.Left, rectangle.Top);
                points[1] = new Point(rectangle.Right, rectangle.Top);
                e.Graphics.FillPolygon(b, points);
                //
                e.Graphics.FillRectangle(b, rectangle2);
            }
        }
        #endregion

        #region RibbonStatusBar
        public override void OnRenderRibbonStatusBar(ObjectRenderEventArgs e)
        {
            WFNew.IBaseBarItem pBaseBarItem = e.Object as WFNew.IBaseBarItem;
            if (pBaseBarItem == null || !pBaseBarItem.ShowNomalState) return;
            //
            RectangleF backRect = new RectangleF(0, 1.5f, pBaseBarItem.Width, pBaseBarItem.Height - 2);
            if ((e.Bounds.Width > 0) && (e.Bounds.Height > 0))
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(e.Bounds, pBaseBarItem.LeftTopRadius, pBaseBarItem.RightTopRadius, pBaseBarItem.LeftBottomRadius, pBaseBarItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd, 90f))
                    {
                        Blend baseBarBlend = new Blend();
                        baseBarBlend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f };
                        baseBarBlend.Factors = new float[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f };
                        b.Blend = baseBarBlend;
                        e.Graphics.FillPath(b, path);
                    }
                }
            }
        }
        #endregion

        #region BaseBar
        public override void OnRenderRibbonBaseBar(ObjectRenderEventArgs e)
        {
            WFNew.IBaseBarItem pBaseBarItem = e.Object as WFNew.IBaseBarItem;
            if (pBaseBarItem == null || !pBaseBarItem.ShowNomalState) return;
            //
            RectangleF backRect = new RectangleF(0, 1.5f, pBaseBarItem.Width, pBaseBarItem.Height - 2);
            if ((e.Bounds.Width > 0) && (e.Bounds.Height > 0))
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(e.Bounds, pBaseBarItem.LeftTopRadius, pBaseBarItem.RightTopRadius, pBaseBarItem.LeftBottomRadius, pBaseBarItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd, 90f))
                    {
                        //Blend baseBarBlend = new Blend();
                        //baseBarBlend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f };
                        //baseBarBlend.Factors = new float[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f };
                        //b.Blend = baseBarBlend;
                        e.Graphics.FillPath(b, path);
                    }
                }
            }
        }

        public override void OnRenderOverflowButton(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IOverflowButton pOverflowButton = e.Object as GISShare.Controls.WinForm.WFNew.IOverflowButton;
            if (pOverflowButton == null) return;
            //
            switch (pOverflowButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pOverflowButton, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pOverflowButton, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pOverflowButton, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pOverflowButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pOverflowButton, e.Bounds, 3); }
                    else if (pOverflowButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pOverflowButton, e.Bounds, 3); }
                    break;
            }
            //
            Rectangle rectangle;
            Rectangle rectangle2;
            Point[] points = null;
            if (pOverflowButton.ReverseLayout)
            {
                rectangle = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - 3) / 2 + 1, 3, 3);
                rectangle2 = new Rectangle(rectangle.X + 4, rectangle.Y - 1, 1, 5);
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
                {
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    points[2] = new Point(rectangle.Right, rectangle.Bottom);
                    e.Graphics.FillPolygon(b, points);
                    //
                    e.Graphics.FillRectangle(b, rectangle2);
                }
                rectangle = new Rectangle(rectangle.X + 1, rectangle.Y, 3, 4);
                rectangle2 = new Rectangle(rectangle2.X + 1, rectangle2.Y, 1, 5);
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    points[2] = new Point(rectangle.Right, rectangle.Bottom);
                    e.Graphics.FillPolygon(b, points);
                    //
                    e.Graphics.FillRectangle(b, rectangle2);
                }
            }
            else
            {
                rectangle = new Rectangle(e.Bounds.Right - 5, e.Bounds.Y + (e.Bounds.Height - 3) / 2 + 1, 3, 3);
                rectangle2 = new Rectangle(rectangle.X - 2, rectangle.Y - 1, 1, 5);
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
                {
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    points[1] = new Point(rectangle.Left, rectangle.Top);
                    e.Graphics.FillPolygon(b, points);
                    //
                    e.Graphics.FillRectangle(b, rectangle2);
                }
                rectangle = new Rectangle(rectangle.X - 1, rectangle.Y, 3, 4);
                rectangle2 = new Rectangle(rectangle2.X - 1, rectangle2.Y, 1, 5);
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    points[1] = new Point(rectangle.Left, rectangle.Top);
                    e.Graphics.FillPolygon(b, points);
                    //
                    e.Graphics.FillRectangle(b, rectangle2);
                }
            }
        }
        #endregion

        public override void OnRenderRibbonToolBar(ObjectRenderEventArgs e)
        {
            WFNew.IToolBarItem pToolBarItem = e.Object as WFNew.IToolBarItem;
            if (pToolBarItem == null || !pToolBarItem.ShowNomalState) return;
            //
            if (pToolBarItem.eToolBarStyle == ToolBarStyle.eMenuBar)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(e.Bounds, pToolBarItem.LeftTopRadius, pToolBarItem.RightTopRadius, pToolBarItem.LeftBottomRadius, pToolBarItem.RightBottomRadius))
                {
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.BaseBarBackgroundBegin))
                    {
                        e.Graphics.FillPath(b, path);
                    }
                }
            }
            else if (pToolBarItem.eToolBarStyle == ToolBarStyle.eToolBar)
            {
                RectangleF backRect = new RectangleF(0, 1.5f, pToolBarItem.Width, pToolBarItem.Height - 2);
                if ((e.Bounds.Width > 0) && (e.Bounds.Height > 0))
                {
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(e.Bounds, pToolBarItem.LeftTopRadius, pToolBarItem.RightTopRadius, pToolBarItem.LeftBottomRadius, pToolBarItem.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd, 90f))
                        {
                            //Blend baseBarBlend = new Blend();
                            //baseBarBlend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f };
                            //baseBarBlend.Factors = new float[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f };
                            //b.Blend = baseBarBlend;
                            e.Graphics.FillPath(b, path);
                        }
                    }
                }
            }
            else if (pToolBarItem.eToolBarStyle == ToolBarStyle.eStatusBar)
            {
                RectangleF backRect = new RectangleF(0, 1.5f, pToolBarItem.Width, pToolBarItem.Height - 2);
                if ((e.Bounds.Width > 0) && (e.Bounds.Height > 0))
                {
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(e.Bounds, pToolBarItem.LeftTopRadius, pToolBarItem.RightTopRadius, pToolBarItem.LeftBottomRadius, pToolBarItem.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd, 90f))
                        {
                            Blend baseBarBlend = new Blend();
                            baseBarBlend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f };
                            baseBarBlend.Factors = new float[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f };
                            b.Blend = baseBarBlend;
                            e.Graphics.FillPath(b, path);
                        }
                    }
                }
            }
            //
            if (pToolBarItem.ShowGripRegion)
            {
                Rectangle rectangle = pToolBarItem.GripRegionRectangle;
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    switch (pToolBarItem.eOrientation)
                    {
                        case Orientation.Horizontal:
                            int iX = (rectangle.Left + rectangle.Right) / 2 - 1;
                            for (int i = 4; i < rectangle.Height - 3; i += 5)
                            {
                                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
                                {
                                    e.Graphics.FillRectangle(b, new Rectangle(iX + 1, rectangle.Top + i + 1, 2, 2));
                                } 
                                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                                {
                                    e.Graphics.FillRectangle(b, new Rectangle(iX, rectangle.Top + i, 2, 2));
                                }
                            }
                            break;
                        case Orientation.Vertical:
                        default:
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            for (int i = 4; i < rectangle.Width - 3; i += 5)
                            {
                                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowLight))
                                {
                                    e.Graphics.FillRectangle(b, new Rectangle(rectangle.Left + i + 1, iY + 1, 2, 2));
                                }
                                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                                {
                                    e.Graphics.FillRectangle(b, new Rectangle(rectangle.Left + i, iY, 2, 2));
                                }
                            }
                            break;
                    }
                }
            }
        }

        #region RibbonPage
        public override void OnRenderRibbonPage(ObjectRenderEventArgs e)
        {
            WFNew.IRibbonPageItem pRibbonPage = e.Object as WFNew.IRibbonPageItem;
            if (pRibbonPage == null) return;
            if (pRibbonPage.Enabled)
            {
                if (!pRibbonPage.ShowBackground) return;
                Rectangle rectangle = pRibbonPage.DisplayRectangle;
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    //Background gradient
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pRibbonPage.LeftTopRadius, pRibbonPage.RightTopRadius, pRibbonPage.LeftBottomRadius, pRibbonPage.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, e.Bounds.Top + 30), new Point(0, e.Bounds.Bottom - 10),
                            this.WFNewColorTable.RibbonControlPagesBackgroundBegin, this.WFNewColorTable.RibbonControlPagesBackgroundEnd))
                        {
                            b.WrapMode = WrapMode.TileFlipXY;
                            e.Graphics.FillPath(b, path);
                        }
                    }
                    Rectangle glossy = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top + 0, e.Bounds.Right, e.Bounds.Top + 18);
                    if (glossy.Width > 0 && glossy.Height > 0)
                    {
                        using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonPage.LeftTopRadius, pRibbonPage.RightTopRadius, 0, 0))
                        {
                            using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                    }
                }
            }
            else
            {
                Rectangle rectangle = pRibbonPage.DisplayRectangle;
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    //Background gradient
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pRibbonPage.LeftTopRadius, pRibbonPage.RightTopRadius, pRibbonPage.LeftBottomRadius, pRibbonPage.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                            this.WFNewColorTable.RibbonControlPagesDisabledBackgroundBegin, this.WFNewColorTable.RibbonControlPagesDisabledBackgroundEnd, LinearGradientMode.Vertical))
                        {
                            b.WrapMode = WrapMode.TileFlipXY;
                            e.Graphics.FillPath(b, path);
                        }
                    }
                    Rectangle glossy = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top + 0, e.Bounds.Right, e.Bounds.Top + 18);
                    if (glossy.Width > 0 && glossy.Height > 0)
                    {
                        using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonPage.LeftTopRadius, pRibbonPage.RightTopRadius, 0, 0))
                        {
                            using (Brush b = new SolidBrush(Color.FromArgb(120, Color.White)))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region TabButtonContainerButton ContextButton CloseButton
        public override void OnRenderTabButtonContainerButton(ObjectRenderEventArgs e)
        {
            ITabButtonContainerButton pTabButtonContainerButton = e.Object as ITabButtonContainerButton;
            if (pTabButtonContainerButton == null) return;
            //
            switch (pTabButtonContainerButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pTabButtonContainerButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pTabButtonContainerButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pTabButtonContainerButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pTabButtonContainerButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pTabButtonContainerButton, e.Bounds, 2); }
                    else if (pTabButtonContainerButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pTabButtonContainerButton, e.Bounds, 2); }
                    break;
            }
            //
            Rectangle rectangle;
            switch (pTabButtonContainerButton.eTabButtonContainerButtonStyle)
            {
                case TabButtonContainerButtonStyle.eContextButton:
                    rectangle = Util.UtilTX.CreateRectangle(pTabButtonContainerButton.GlyphRectangle, 15, 15);
                    int x1 = rectangle.Left + rectangle.Width / 2;
                    int y1 = rectangle.Bottom - 3;
                    int y2 = y1 - 4;
                    int x2 = x1 - 4;
                    int y3 = y2;
                    int x3 = x1 + 4;
                    using (GraphicsPath arrowHeadPath = new GraphicsPath())
                    {
                        arrowHeadPath.AddLine(x1, y1, x2, y2);
                        arrowHeadPath.AddLine(x2, y2, x3, y3);
                        arrowHeadPath.CloseFigure();
                        e.Graphics.FillPath(new SolidBrush(this.WFNewColorTable.Arrow), arrowHeadPath);
                    }
                    break;
                case TabButtonContainerButtonStyle.eCloseButton:
                    rectangle = Util.UtilTX.CreateRectangle(pTabButtonContainerButton.GlyphRectangle, 15, 15);
                    using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
                    {
                        rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
                        e.Graphics.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region TabButton
        public override void OnRenderTabButton(ObjectRenderEventArgs e)
        {
            WFNew.ITabButtonItem pTabButtonItem = e.Object as WFNew.ITabButtonItem;
            if (pTabButtonItem == null) return;
            //
            switch (pTabButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawTabButtonSelected(e.Graphics, pTabButtonItem, e.Bounds);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawTabButtonPressed(e.Graphics, pTabButtonItem, e.Bounds);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    if (pTabButtonItem.Checked) { this.DrawTabButtonChecked(e.Graphics, pTabButtonItem, e.Bounds); }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                default:
                    this.DrawTabButtonDisabled(e.Graphics, pTabButtonItem, e.Bounds);
                    break;
            }
            //
            if(pTabButtonItem.ShowCloseButton) this.DrawCloseRegion(e.Graphics, pTabButtonItem, pTabButtonItem.CloseButtonRectangle);
        }
        private void DrawTabButtonChecked(Graphics g, WFNew.ITabButtonItem pTabButtonItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            int iAngle = 90;
            int iLeftTopRadius = pTabButtonItem.LeftTopRadius;
            int iRightTopRadius = pTabButtonItem.RightTopRadius;
            int iLeftBottomRadius = 0;
            int iRightBottomRadius = 0;
            Rectangle glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height / 3);
            switch (pTabButtonItem.TabAlignment)
            {
                case TabAlignment.Left:
                    iAngle = 270;
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = 0;
                    outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right + pTabButtonItem.OffsetValue, outerR.Bottom - 1);
                    glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width / 3, rectangle.Height);
                    break;
                case TabAlignment.Right:
                    iAngle = 270;
                    iLeftTopRadius = 0;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    innerR = Rectangle.FromLTRB(outerR.Left - pTabButtonItem.OffsetValue, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
                    glossy = new Rectangle(rectangle.Right - rectangle.Width / 3, rectangle.Top, rectangle.Width / 3, rectangle.Height);
                    break;
                case TabAlignment.Top:
                    iAngle = 90;
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = 0;
                    outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom + pTabButtonItem.OffsetValue);
                    glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height / 3);
                    break;
                case TabAlignment.Bottom:
                default:
                    iAngle = 90;
                    iLeftTopRadius = 0;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top - pTabButtonItem.OffsetValue, outerR.Right - 1, outerR.Bottom - 1);
                    glossy = new Rectangle(rectangle.Left, rectangle.Bottom - rectangle.Height / 3, rectangle.Width, rectangle.Height / 3);
                    break;
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.TabButtonCheckedBegin, this.WFNewColorTable.TabButtonCheckedEnd, iAngle))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Brush b = new SolidBrush(Color.FromArgb(180, Color.White)))
                {
                    g.FillPath(b, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, outerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonControlOutLine))
                {
                    g.DrawPath(p, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, innerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(Color.FromArgb(200, this.WFNewColorTable.TabButtonCheckedBorderIn)))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawTabButtonDisabled(Graphics g, WFNew.ITabButtonItem pTabButtonItem, Rectangle rectangle)
        {
            Rectangle outRectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle intRectangle = Rectangle.FromLTRB(outRectangle.Left + 1, outRectangle.Top + 1, outRectangle.Right - 1, outRectangle.Bottom - 1);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            int iAngle = 90;
            int iLeftTopRadius = pTabButtonItem.LeftTopRadius;
            int iRightTopRadius = pTabButtonItem.RightTopRadius;
            int iLeftBottomRadius = 0;
            int iRightBottomRadius = 0;
            Rectangle glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height / 3);
            switch (pTabButtonItem.TabAlignment)
            {
                case TabAlignment.Left:
                    iAngle = 270;
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = 0;
                    outRectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    intRectangle = Rectangle.FromLTRB(outRectangle.Left + 1, outRectangle.Top + 1, outRectangle.Right + pTabButtonItem.OffsetValue, outRectangle.Bottom - 1);
                    glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width / 3, rectangle.Height);
                    break;
                case TabAlignment.Right:
                    iAngle = 270;
                    iLeftTopRadius = 0;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    intRectangle = Rectangle.FromLTRB(outRectangle.Left - pTabButtonItem.OffsetValue, outRectangle.Top + 1, outRectangle.Right - 1, outRectangle.Bottom - 1);
                    glossy = new Rectangle(rectangle.Right - rectangle.Width / 3, rectangle.Top, rectangle.Width / 3, rectangle.Height);
                    break;
                case TabAlignment.Top:
                    iAngle = 90;
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = 0;
                    outRectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    intRectangle = Rectangle.FromLTRB(outRectangle.Left + 1, outRectangle.Top + 1, outRectangle.Right - 1, outRectangle.Bottom + pTabButtonItem.OffsetValue);
                    glossy = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height / 3);
                    break;
                case TabAlignment.Bottom:
                default:
                    iAngle = 90;
                    iLeftTopRadius = 0;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    intRectangle = Rectangle.FromLTRB(outRectangle.Left + 1, outRectangle.Top - pTabButtonItem.OffsetValue, outRectangle.Right - 1, outRectangle.Bottom - 1);
                    glossy = new Rectangle(rectangle.Left, rectangle.Bottom - rectangle.Height / 3, rectangle.Width, rectangle.Height / 3);
                    break;
            }
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(intRectangle, this.WFNewColorTable.TabButtonDisabledBegin, this.WFNewColorTable.TabButtonDisabledEnd, iAngle))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    g.FillPath(b, path);
                }
            }
            if (pTabButtonItem.Checked)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(180, Color.White)))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, outRectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.TabButtonDisabledBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, intRectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.TabButtonDisabledBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawTabButtonPressed(Graphics g, WFNew.ITabButtonItem pTabButtonItem, Rectangle rectangle)
        {
            int iLeftTopRadius = pTabButtonItem.LeftTopRadius;
            int iRightTopRadius = pTabButtonItem.RightTopRadius;
            int iLeftBottomRadius = 0;
            int iRightBottomRadius = 0;
            switch (pTabButtonItem.TabAlignment)
            {
                case TabAlignment.Left:
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = 0;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right + pTabButtonItem.OffsetValue, rectangle.Bottom - 1);
                    break;
                case TabAlignment.Right:
                    iLeftTopRadius = 0;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    rectangle = Rectangle.FromLTRB(rectangle.Left - pTabButtonItem.OffsetValue, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    break;
                case TabAlignment.Top:
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = 0;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom + pTabButtonItem.OffsetValue);
                    break;
                case TabAlignment.Bottom:
                default:
                    iLeftTopRadius = 0;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top - pTabButtonItem.OffsetValue, rectangle.Right - 1, rectangle.Bottom);
                    break;
            }
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, rectangle.Top + 30), new Point(0, rectangle.Bottom - 10),
                    this.WFNewColorTable.TabButtonPressedBegin, this.WFNewColorTable.TabButtonPressedEnd))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    g.FillPath(b, path);
                }
            }
            //
            using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, rectangle, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.TabButtonPressedBorderOut))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawPath(p, path2);
                }
            }
        }
        private void DrawTabButtonSelected(Graphics g, WFNew.ITabButtonItem pTabButtonItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(innerR.Left + 1, innerR.Top + 1, innerR.Right - 1, innerR.Top + rectangle.Height / 2);
            //
            int iLeftTopRadius = pTabButtonItem.LeftTopRadius;
            int iRightTopRadius = pTabButtonItem.RightTopRadius;
            int iLeftBottomRadius = 0;
            int iRightBottomRadius = 0;
            switch (pTabButtonItem.TabAlignment)
            {
                case TabAlignment.Left:
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = 0;
                    outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right + pTabButtonItem.OffsetValue, outerR.Bottom - 1);
                    break;
                case TabAlignment.Right:
                    iLeftTopRadius = 0;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    innerR = Rectangle.FromLTRB(outerR.Left - pTabButtonItem.OffsetValue, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
                    break;
                case TabAlignment.Top:
                    iLeftTopRadius = pTabButtonItem.LeftTopRadius;
                    iRightTopRadius = pTabButtonItem.RightTopRadius;
                    iLeftBottomRadius = 0;
                    iRightBottomRadius = 0;
                    outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom + pTabButtonItem.OffsetValue);
                    break;
                case TabAlignment.Bottom:
                default:
                    iLeftTopRadius = 0;
                    iRightTopRadius = 0;
                    iLeftBottomRadius = pTabButtonItem.LeftBottomRadius;
                    iRightBottomRadius = pTabButtonItem.RightBottomRadius;
                    innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top - pTabButtonItem.OffsetValue, outerR.Right - 1, outerR.Bottom - 1);
                    break;
            }
            //
            GraphicsPath outer = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius);
            GraphicsPath inner = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius);
            GraphicsPath glossy = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius);
            //
            if (pTabButtonItem.Checked)
            {
                this.DrawTabButtonChecked(g, pTabButtonItem, rectangle);
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, outerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(Color.FromArgb(150, this.WFNewColorTable.TabButtonCheckedSelectedBorderOut)))
                    {
                        p.Width = 1.5f;
                        g.DrawPath(p, path);
                    }
                }
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, innerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(Color.FromArgb(200, this.WFNewColorTable.TabButtonCheckedSelectedBorderIn)))
                    {
                        g.DrawPath(p, path);
                    }
                }
                return;
            }
            //
            using (GraphicsPath radialPath = new GraphicsPath())
            {
                radialPath.AddRectangle(innerR);
                radialPath.CloseFigure();
                PathGradientBrush gr = new PathGradientBrush(radialPath);
                gr.CenterPoint = new PointF(
                    Convert.ToSingle(innerR.Left + innerR.Width / 2),
                    Convert.ToSingle(innerR.Top - 5));
                gr.CenterColor = this.WFNewColorTable.TabButtonSelectedCenter;
                gr.SurroundColors = new Color[] { this.WFNewColorTable.TabButtonSelectedGlow };
                Blend blend = new Blend(3);
                blend.Factors = new float[] { 0.0f, 0.9f, 0.0f };
                blend.Positions = new float[] { 0.0f, 0.8f, 1.0f };
                gr.Blend = blend;
                g.FillPath(gr, radialPath);
                gr.Dispose();
            }
            using (SolidBrush b = new SolidBrush(Color.FromArgb(100, Color.White)))
            {
                g.FillPath(b, glossy);
            }
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, outerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.TabButtonSelectedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateTabButtonContour(pTabButtonItem.TabAlignment, innerR, iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(Color.FromArgb(200, this.WFNewColorTable.TabButtonSelectedBorderIn)))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            outer.Dispose();
            inner.Dispose();
            glossy.Dispose();

        }
        private void DrawCloseRegion(Graphics g, WFNew.ITabButtonItem pTabButtonItem, Rectangle rectangle)
        {
            rectangle = Util.UtilTX.CreateRectangle(rectangle, 14, 14);
            //
            if (pTabButtonItem.eCloseButtonState == BaseItemState.eHot)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonSelected)) 
                {
                    g.FillRectangle(b, rectangle);
                }
                using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
                { 
                    g.DrawRectangle(p, rectangle);
                }
            }
            else if (pTabButtonItem.eCloseButtonState == BaseItemState.ePressed) 
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonPressed))
                {
                    g.FillRectangle(b, rectangle);
                }
                using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
                {
                    g.DrawRectangle(p, rectangle);
                }
            }
            else 
            {

            }
            //
            if (pTabButtonItem.ShowCloseButton)
            {
                using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
                {
                    rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
                    g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                    g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
                }
            }
        }
        #endregion

        #region RibbonBar
        public override void OnRenderRibbonBar(ObjectRenderEventArgs e)
        {
            WFNew.IRibbonBarItem pRibbonBarItem = e.Object as WFNew.IRibbonBarItem;
            if (pRibbonBarItem == null) return;
            //
            switch (pRibbonBarItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawRibbonBarSelected(e.Graphics, pRibbonBarItem);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawRibbonBarPressed(e.Graphics, pRibbonBarItem);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawRibbonBarDisabled(e.Graphics, pRibbonBarItem);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (pRibbonBarItem.Checked) this.DrawRibbonBarChecked(e.Graphics, pRibbonBarItem);
                    else this.DrawRibbonBarNomal(e.Graphics, pRibbonBarItem);
                    break;
            }
        }
        private void DrawRibbonBarNomal(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            if (pRibbonBarItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(pRibbonBarItem.DrawRectangle.X, pRibbonBarItem.DrawRectangle.Y, pRibbonBarItem.DrawRectangle.Width - 1, pRibbonBarItem.DrawRectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                if (pRibbonBarItem.ShowNomalState)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                        this.WFNewColorTable.RibbonControlPagesBackgroundBegin, this.WFNewColorTable.RibbonControlPagesBackgroundEnd))
                    {
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path);
                    }
                    Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, 0, 0))
                    {
                        using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                        {
                            g.FillPath(b, path2);
                        }
                    }
                }
                //
                if (pRibbonBarItem.IsMinState)
                {
                    this.DrawRibbonBarMinState(g, pRibbonBarItem);
                }
                else
                {
                    using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitleBackground))
                        {
                            g.FillPath(b, path3);
                        }
                    }
                    //
                    if (pRibbonBarItem.GlyphVisible)
                    {
                        switch (pRibbonBarItem.eGlyphState)
                        {
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                break;
                        }
                        this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                    }
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarNomalBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarNomalBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonBarChecked(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            if (pRibbonBarItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(pRibbonBarItem.DrawRectangle.X, pRibbonBarItem.DrawRectangle.Y, pRibbonBarItem.DrawRectangle.Width - 1, pRibbonBarItem.DrawRectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                    this.WFNewColorTable.RibbonBarCheckedBegin, this.WFNewColorTable.RibbonBarCheckedEnd))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    g.FillPath(b, path);
                }
                Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, 0, 0))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                    {
                        g.FillPath(b, path2);
                    }
                }
                //
                if (pRibbonBarItem.IsMinState)
                {
                    this.DrawRibbonBarMinState(g, pRibbonBarItem);
                }
                else
                {
                    using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitleCheckedBackground))
                        {
                            g.FillPath(b, path3);
                        }
                    }
                    //
                    if (pRibbonBarItem.GlyphVisible)
                    {
                        switch (pRibbonBarItem.eGlyphState)
                        {
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                break;
                        }
                        this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                    }
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarCheckedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarCheckedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonBarDisabled(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            if (pRibbonBarItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(pRibbonBarItem.DrawRectangle.X, pRibbonBarItem.DrawRectangle.Y, pRibbonBarItem.DrawRectangle.Width - 1, pRibbonBarItem.DrawRectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                    this.WFNewColorTable.RibbonControlPagesDisabledBackgroundBegin, this.WFNewColorTable.RibbonControlPagesDisabledBackgroundEnd))
                {
                    b.WrapMode = WrapMode.TileFlipXY;
                    g.FillPath(b, path);
                }
                Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, 0, 0))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                    {
                        g.FillPath(b, path2);
                    }
                }
                //
                if (pRibbonBarItem.IsMinState)
                {
                    this.DrawRibbonBarMinState(g, pRibbonBarItem);
                }
                else
                {
                    using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitleDisabledBackground))
                        {
                            g.FillPath(b, path3);
                        }
                    }
                    //
                    if (pRibbonBarItem.GlyphVisible)
                    {
                        switch (pRibbonBarItem.eGlyphState)
                        {
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                break;
                        }
                        this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                    }
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarDisabledBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarDisabledBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonBarPressed(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            if (pRibbonBarItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(pRibbonBarItem.DrawRectangle.X, pRibbonBarItem.DrawRectangle.Y, pRibbonBarItem.DrawRectangle.Width - 1, pRibbonBarItem.DrawRectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            if (pRibbonBarItem.IsMinState)
            {
                #region Main Gradient
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                        this.WFNewColorTable.RibbonBarPressedBegin, this.WFNewColorTable.RibbonBarPressedEnd))
                    {
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path);
                    }
                    Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossy, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, 0, 0))
                    {
                        using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
                        {
                            g.FillPath(b, path2);
                        }
                    }
                    //
                    if (pRibbonBarItem.IsMinState)
                    {
                        this.DrawRibbonBarMinState(g, pRibbonBarItem);
                    }
                    else
                    {
                        using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                        {
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitlePressedBackground))
                            {
                                g.FillPath(b, path3);
                            }
                        }
                        //
                        if (pRibbonBarItem.GlyphVisible)
                        {
                            switch (pRibbonBarItem.eGlyphState)
                            {
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                    this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                    this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                    this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                            }
                            this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                        }
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonBarPressedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
                #endregion
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle, pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonBarPressedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            else
            {
                #region Main Gradient
                if (pRibbonBarItem.IsMinState)
                {
                    intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                        pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(intRectangle,
                            this.WFNewColorTable.RibbonBarSelectedBegin, this.WFNewColorTable.RibbonBarSelectedEnd, 90))
                        {
                            g.FillPath(b, path);
                        }
                        this.DrawRibbonBarMinState(g, pRibbonBarItem);
                    }
                }
                else
                {
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                        pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(intRectangle,
                            this.WFNewColorTable.RibbonBarSelectedBegin, this.WFNewColorTable.RibbonBarSelectedEnd, 90))
                        {
                            g.FillPath(b, path);
                        }
                        //
                        using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                        {
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitleSelectedBackground))
                            {
                                g.FillPath(b, path3);
                            }
                        }
                        if (pRibbonBarItem.GlyphVisible)
                        {
                            switch (pRibbonBarItem.eGlyphState)
                            {
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                    this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                    this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                    this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                    break;
                            }
                            this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                        }
                        //
                        using (Pen p = new Pen(this.WFNewColorTable.RibbonBarSelectedBorderIn))
                        {
                            g.DrawPath(p, path);
                        }
                    }
                }
                #endregion
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                    pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonBarSelectedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawRibbonBarSelected(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            if (pRibbonBarItem == null) return;
            //
            Rectangle outRectangle = new Rectangle(pRibbonBarItem.DrawRectangle.X, pRibbonBarItem.DrawRectangle.Y, pRibbonBarItem.DrawRectangle.Width - 1, pRibbonBarItem.DrawRectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            //Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, (int)(intRectangle.Height / 4.5));
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0) return;
            //
            #region Main Gradient
            if (pRibbonBarItem.IsMinState)
            {
                intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(intRectangle,
                        this.WFNewColorTable.RibbonBarSelectedBegin, this.WFNewColorTable.RibbonBarSelectedEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                    this.DrawRibbonBarMinState(g, pRibbonBarItem);
                }
            }
            else
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(intRectangle,
                        this.WFNewColorTable.RibbonBarSelectedBegin, this.WFNewColorTable.RibbonBarSelectedEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                    //
                    using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonBarItem.TitleRectangle, 0, 0, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonBarTitleSelectedBackground))
                        {
                            g.FillPath(b, path3);
                        }
                    }
                    if (pRibbonBarItem.GlyphVisible)
                    {
                        switch (pRibbonBarItem.eGlyphState)
                        {
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                                this.DrawRibbonBarGlyphSelected(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                                this.DrawRibbonBarGlyphPressed(g, pRibbonBarItem.GlyphRectangle);
                                break;
                            case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                                this.DrawRibbonBarGlyphDisabled(g, pRibbonBarItem.GlyphRectangle);
                                break;
                        }
                        this.DrawRibbonBarGlyph(g, pRibbonBarItem.GlyphRectangle, pRibbonBarItem.GlyphEnabled);
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.RibbonBarSelectedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            #endregion
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                pRibbonBarItem.LeftTopRadius, pRibbonBarItem.RightTopRadius, pRibbonBarItem.LeftBottomRadius, pRibbonBarItem.RightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarSelectedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonBarGlyph(Graphics g, Rectangle b, bool enabled)
        {
            Size moreSize = new Size(7, 7);

            Color dark = enabled ? this.WFNewColorTable.Glyph : this.WFNewColorTable.GlyphDisabled;
            Color light = this.WFNewColorTable.GlyphLight;

            Rectangle bounds = this.CenterOn(b, new Rectangle(Point.Empty, moreSize));
            Rectangle boundsLight = bounds;
            boundsLight.Offset(1, 1);

            DrawGlyph(g, boundsLight.Location, light, moreSize);
            DrawGlyph(g, bounds.Location, dark, moreSize);
        }
        private Rectangle CenterOn(Rectangle container, Rectangle r)
        {
            Rectangle result = new Rectangle(
                container.Left + ((container.Width - r.Width) / 2),
                container.Top + ((container.Height - r.Height) / 2),
                r.Width, r.Height);

            return result;
        }
        private void DrawRibbonBarGlyphChecked(Graphics g, Rectangle bounds)
        {
            Rectangle outerR = Rectangle.FromLTRB(
                bounds.Left,
                bounds.Top,
                bounds.Right - 1,
                bounds.Bottom - 1);

            Rectangle innerR = Rectangle.FromLTRB(
                bounds.Left + 1,
                bounds.Top + 1,
                bounds.Right - 2,
                bounds.Bottom - 2);

            using (SolidBrush brus = new SolidBrush(this.WFNewColorTable.RibbonBarGlyphCheckedBackground))
            {
                g.FillRectangle(brus, outerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphCheckedBorderOut))
            {
                g.DrawRectangle(p, innerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphCheckedBorderIn))
            {
                g.DrawRectangle(p, outerR);
            }
        }
        private void DrawRibbonBarGlyphDisabled(Graphics g, Rectangle bounds)
        {
            Rectangle outerR = Rectangle.FromLTRB(
                bounds.Left,
                bounds.Top,
                bounds.Right - 1,
                bounds.Bottom - 1);

            Rectangle innerR = Rectangle.FromLTRB(
                bounds.Left + 1,
                bounds.Top + 1,
                bounds.Right - 2,
                bounds.Bottom - 2);

            using (SolidBrush brus = new SolidBrush(this.WFNewColorTable.RibbonBarGlyphDisabledBackground))
            {
                g.FillRectangle(brus, outerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphDisabledBorderOut))
            {
                g.DrawRectangle(p, innerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphDisabledBorderIn))
            {
                g.DrawRectangle(p, outerR);
            }
        }
        private void DrawRibbonBarGlyphPressed(Graphics g, Rectangle bounds)
        {
            Rectangle outerR = Rectangle.FromLTRB(
                bounds.Left,
                bounds.Top,
                bounds.Right - 1,
                bounds.Bottom - 1);

            Rectangle innerR = Rectangle.FromLTRB(
                bounds.Left + 1,
                bounds.Top + 1,
                bounds.Right - 2,
                bounds.Bottom - 2);

            using (SolidBrush brus = new SolidBrush(this.WFNewColorTable.RibbonBarGlyphPressedBackground))
            {
                g.FillRectangle(brus, outerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphPressedBorderOut))
            {
                g.DrawRectangle(p, innerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphPressedBorderIn))
            {
                g.DrawRectangle(p, outerR);
            }
        }
        private void DrawRibbonBarGlyphSelected(Graphics g, Rectangle bounds)
        {
            Rectangle outerR = Rectangle.FromLTRB(
                bounds.Left,
                bounds.Top,
                bounds.Right - 1,
                bounds.Bottom - 1);

            Rectangle innerR = Rectangle.FromLTRB(
                bounds.Left + 1,
                bounds.Top + 1,
                bounds.Right - 2,
                bounds.Bottom - 2);

            using (SolidBrush brus = new SolidBrush(this.WFNewColorTable.RibbonBarGlyphSelectedBackground))
            {
                g.FillRectangle(brus, outerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphPressedBorderOut))
            {
                g.DrawRectangle(p, innerR);
            }
            using (Pen p = new Pen(this.WFNewColorTable.RibbonBarGlyphPressedBorderIn))
            {
                g.DrawRectangle(p, outerR);
            }
        }
        private void DrawGlyph(Graphics gr, Point p, Color color, Size moreSize)
        {
            /*
             
             a-------b-+
             |         |
             |         d
             c     g   |
             |         |
             +----e----f
             
             */
            Point a = p;
            Point b = new Point(p.X + moreSize.Width - 1, p.Y);
            Point c = new Point(p.X, p.Y + moreSize.Height - 1);
            Point f = new Point(p.X + moreSize.Width, p.Y + moreSize.Height);
            Point d = new Point(f.X, f.Y - 3);
            Point e = new Point(f.X - 3, f.Y);
            Point g = new Point(f.X - 3, f.Y - 3);

            SmoothingMode lastMode = gr.SmoothingMode;

            gr.SmoothingMode = SmoothingMode.None;

            using (Pen pen = new Pen(color))
            {
                gr.DrawLine(pen, a, b);
                gr.DrawLine(pen, a, c);
                gr.DrawLine(pen, e, f);
                gr.DrawLine(pen, d, f);
                gr.DrawLine(pen, e, d);
                gr.DrawLine(pen, g, f);
            }

            gr.SmoothingMode = lastMode;
        }
        private void DrawRibbonBarMinState(Graphics g, WFNew.IRibbonBarItem pRibbonBarItem)
        {
            Size imgSquareSize = new Size(32, 32);

            Rectangle imageRectangle = new Rectangle(new Point(
                pRibbonBarItem.DrawRectangle.Left + (pRibbonBarItem.DrawRectangle.Width - imgSquareSize.Width) / 2,
                pRibbonBarItem.DrawRectangle.Top + 7), imgSquareSize);

            Rectangle imageRectangleBottom = Rectangle.FromLTRB(
                imageRectangle.Left, imageRectangle.Bottom - 10, imageRectangle.Right, imageRectangle.Bottom);

            int margin = 9;
            Rectangle textR = Rectangle.FromLTRB(
                pRibbonBarItem.DrawRectangle.Left + margin,
                imageRectangle.Bottom + margin,
                pRibbonBarItem.DrawRectangle.Right - margin,
                pRibbonBarItem.DrawRectangle.Bottom - margin);

            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(imageRectangle, 6, 6, 6, 6))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(imageRectangle, this.WFNewColorTable.RibbonBarNomalBegin, this.WFNewColorTable.RibbonBarNomalEnd, 90))
                {
                    g.FillPath(b, path);
                }
                //
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(imageRectangleBottom, 0, 0, 6, 6))
                {
                    using (SolidBrush b2 = new SolidBrush(this.WFNewColorTable.RibbonBarTitleBackground))
                    {
                        g.FillPath(b2, path2);
                    }
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonBarNomalBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            if (pRibbonBarItem.Image != null)
            {
                g.DrawImage
                    (
                    pRibbonBarItem.Image,
                    imageRectangle.Left + (imageRectangle.Width - pRibbonBarItem.Image.Width) / 2,
                    imageRectangle.Top + ((imageRectangle.Height - imageRectangleBottom.Height) - pRibbonBarItem.Image.Height) / 2,
                    pRibbonBarItem.Image.Width, pRibbonBarItem.Image.Height
                    );
            }
        }

        public override void OnRenderRibbonBarPopup(ObjectRenderEventArgs e)
        {
            Rectangle outRectangle = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            //
            using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, outRectangle.Top + 30), new Point(0, outRectangle.Bottom - 10),
                this.WFNewColorTable.RibbonControlPagesBackgroundBegin, this.WFNewColorTable.RibbonControlPagesBackgroundEnd))
            {
                b.WrapMode = WrapMode.TileFlipXY;
                e.Graphics.FillRectangle(b, outRectangle);
            }
            //
            Rectangle glossy = Rectangle.FromLTRB(outRectangle.Left, outRectangle.Top + 0, outRectangle.Right, outRectangle.Top + 18);
            using (Brush b = new SolidBrush(Color.FromArgb(30, Color.White)))
            {
                e.Graphics.FillRectangle(b, glossy);
            }
        }
        #endregion

        #region Label
        public override void OnRenderLabel(ObjectRenderEventArgs e)
        {
            this.OnRenderRibbonArea(e);
        }
        #endregion

        #region LinkLabel
        public override void OnRenderLinkLabel(ObjectRenderEventArgs e)
        {
            this.OnRenderRibbonArea(e);
        }
        #endregion

        #region LabelSeparator
        public override void OnRenderLabelSeparator(ObjectRenderEventArgs e)
        {
            WFNew.ILabelSeparatorItem pLabelSeparatorItem = e.Object as WFNew.ILabelSeparatorItem;
            if (pLabelSeparatorItem == null || !pLabelSeparatorItem.ShowNomalState) return;
            Rectangle rectangle = e.Bounds;
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pLabelSeparatorItem.LeftTopRadius, pLabelSeparatorItem.RightTopRadius, pLabelSeparatorItem.LeftBottomRadius, pLabelSeparatorItem.RightBottomRadius))
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorBackground))
                {
                    e.Graphics.FillPath(b, path);
                }
            }
            using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
            {
                e.Graphics.DrawLine(p, new Point(rectangle.Left, rectangle.Bottom), new Point(rectangle.Right - 1, rectangle.Bottom));
            }
            using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
            {
                e.Graphics.DrawLine(p, new Point(rectangle.Left, rectangle.Bottom + 1), new Point(rectangle.Right - 1, rectangle.Bottom + 1));
            }
        }
        #endregion

        #region RadioButton
        public override void OnRenderRadioButton(ObjectRenderEventArgs e)
        {
            WFNew.IRadioButtonItem pRadioButtonItem = e.Object as WFNew.IRadioButtonItem;
            if (pRadioButtonItem == null) return;
            switch (pRadioButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawRadioButtonSelected(e.Graphics, pRadioButtonItem, pRadioButtonItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawRadioButtonPressed(e.Graphics, pRadioButtonItem, pRadioButtonItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawRadioButtonDisabled(e.Graphics, pRadioButtonItem, pRadioButtonItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    this.DrawRadioButtonNomal(e.Graphics, pRadioButtonItem, pRadioButtonItem.CheckRectangle);
                    break;
            }
        }
        private void DrawRadioButtonNomal(Graphics g, WFNew.IRadioButtonItem pRadioButtonItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxomalBackgroundOutBegin, this.WFNewColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxomalBackgroundIntBegin, this.WFNewColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonItem.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonDisabled(Graphics g, WFNew.IRadioButtonItem pRadioButtonItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxDisabledBackgroundOutBegin, this.WFNewColorTable.CheckBoxDisabledBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxDisabledBackgroundIntBegin, this.WFNewColorTable.CheckBoxDisabledBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonItem.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxDisabledOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonPressed(Graphics g, WFNew.IRadioButtonItem pRadioButtonItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxPressedBackgroundOutBegin, this.WFNewColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxPressedBackgroundIntBegin, this.WFNewColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonItem.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonSelected(Graphics g, WFNew.IRadioButtonItem pRadioButtonItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxSelectedBackgroundOutBegin, this.WFNewColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxSelectedBackgroundIntBegin, this.WFNewColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonItem.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        #endregion

        #region CheckBox
        public override void OnRenderCheckBox(ObjectRenderEventArgs e)
        {
            WFNew.ICheckBoxItem pCheckBoxItem = e.Object as WFNew.ICheckBoxItem;
            if (pCheckBoxItem == null) return;
            switch (pCheckBoxItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawCheckBoxSelected(e.Graphics, pCheckBoxItem, pCheckBoxItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawCheckBoxPressed(e.Graphics, pCheckBoxItem, pCheckBoxItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawCheckBoxDisabled(e.Graphics, pCheckBoxItem, pCheckBoxItem.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    this.DrawCheckBoxNomal(e.Graphics, pCheckBoxItem, pCheckBoxItem.CheckRectangle);
                    break;
            }
        }
        private void DrawCheckBoxNomal(Graphics g, WFNew.ICheckBoxItem pCheckBoxItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxomalBackgroundOutBegin, this.WFNewColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxomalBackgroundMiddleBegin, this.WFNewColorTable.CheckBoxomalBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WFNewColorTable.CheckBoxomalBackgroundIntBegin, this.WFNewColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxItem.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxDisabled(Graphics g, WFNew.ICheckBoxItem pCheckBoxItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1); 
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxDisabledBackgroundOutBegin, this.WFNewColorTable.CheckBoxDisabledBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxDisabledBackgroundMiddleBegin, this.WFNewColorTable.CheckBoxDisabledBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WFNewColorTable.CheckBoxDisabledBackgroundIntBegin, this.WFNewColorTable.CheckBoxDisabledBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxItem.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowDisabled))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ArrowDisabled))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxDisabledOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxPressed(Graphics g, WFNew.ICheckBoxItem pCheckBoxItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxPressedBackgroundOutBegin, this.WFNewColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxPressedBackgroundMiddleBegin, this.WFNewColorTable.CheckBoxPressedBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WFNewColorTable.CheckBoxPressedBackgroundIntBegin, this.WFNewColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxItem.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxSelected(Graphics g, WFNew.ICheckBoxItem pCheckBoxItem, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WFNewColorTable.CheckBoxSelectedBackgroundOutBegin, this.WFNewColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WFNewColorTable.CheckBoxSelectedBackgroundMiddleBegin, this.WFNewColorTable.CheckBoxSelectedBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WFNewColorTable.CheckBoxSelectedBackgroundIntBegin, this.WFNewColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxItem.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WFNewColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WFNewColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        #endregion

        #region BaseButton
        public override void OnRenderBaseButton(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
        }
        private void DrawBaseButtonNomal(Graphics g, WFNew.IBaseButtonItem pBaseButtonItem, Rectangle rectangle, int iSpilt)
        {
            if (!pBaseButtonItem.ShowNomalState) return;

            int iLeftTopRadius = pBaseButtonItem.LeftTopRadius;
            int iRightTopRadius = pBaseButtonItem.RightTopRadius;
            int iLeftBottomRadius = pBaseButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pBaseButtonItem.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0 ||
                glossyRectangle.Width <= 0 ||
                glossyRectangle.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pBaseButtonItem.pOwner as WFNew.IButtonGroupItem,
                pBaseButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                float[] pos = new float[4];
                pos[0] = 0.0F;
                pos[1] = 0.3F;
                pos[2] = 0.35F;
                pos[3] = 1.0F;
                Color[] colors = new Color[4];
                colors[0] = this.GetColor(this.WFNewColorTable.ButtonNomal, 0, 35, 24, 9);
                colors[1] = this.GetColor(this.WFNewColorTable.ButtonNomal, 0, 13, 8, 3);
                colors[2] = this.WFNewColorTable.ButtonNomal;
                colors[3] = this.GetColor(this.WFNewColorTable.ButtonNomal, 0, 28, 29, 14);
                ColorBlend mix = new ColorBlend();
                mix.Colors = colors;
                mix.Positions = pos;
                LinearGradientBrush lgbrush = new LinearGradientBrush(intRectangle, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                lgbrush.InterpolationColors = mix;
                g.FillPath(lgbrush, path);
                //
                #region Glossy
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                        this.WFNewColorTable.ButtonNomalGlossyBegin, this.WFNewColorTable.ButtonNomalGlossyEnd, 90))
                    {
                        //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path2);
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
        }
        private void DrawBaseButtonChecked(Graphics g, WFNew.IBaseButtonItem pBaseButtonItem, Rectangle rectangle, int iSpilt)
        {
            int iLeftTopRadius = pBaseButtonItem.LeftTopRadius;
            int iRightTopRadius = pBaseButtonItem.RightTopRadius;
            int iLeftBottomRadius = pBaseButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pBaseButtonItem.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0 ||
                glossyRectangle.Width <= 0 ||
                glossyRectangle.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pBaseButtonItem.pOwner as WFNew.IButtonGroupItem,
                pBaseButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                float[] pos = new float[4];
                pos[0] = 0.0F;
                pos[1] = 0.3F;
                pos[2] = 0.35F;
                pos[3] = 1.0F;
                Color[] colors = new Color[4];
                colors[0] = this.GetColor(this.WFNewColorTable.ButtonChecked, 0, 35, 24, 9);
                colors[1] = this.GetColor(this.WFNewColorTable.ButtonChecked, 0, 13, 8, 3);
                colors[2] = this.WFNewColorTable.ButtonChecked;
                colors[3] = this.GetColor(this.WFNewColorTable.ButtonChecked, 0, 28, 29, 14);
                ColorBlend mix = new ColorBlend();
                mix.Colors = colors;
                mix.Positions = pos;
                LinearGradientBrush lgbrush = new LinearGradientBrush(intRectangle, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                lgbrush.InterpolationColors = mix;
                g.FillPath(lgbrush, path);
                //
                #region Glossy
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                        this.WFNewColorTable.ButtonCheckedGlossyBegin, this.WFNewColorTable.ButtonCheckedGlossyEnd, 90))
                    {
                        //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path2);
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
        }
        private void DrawBaseButtonDisabled(Graphics g, WFNew.IBaseButtonItem pBaseButtonItem, Rectangle rectangle, int iSpilt)
        {
            if (!pBaseButtonItem.ShowNomalState) return;

            int iLeftTopRadius = pBaseButtonItem.LeftTopRadius;
            int iRightTopRadius = pBaseButtonItem.RightTopRadius;
            int iLeftBottomRadius = pBaseButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pBaseButtonItem.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0 ||
                glossyRectangle.Width <= 0 ||
                glossyRectangle.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pBaseButtonItem.pOwner as WFNew.IButtonGroupItem,
                pBaseButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                #region Main Bg
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                    //path2.CloseFigure();
                    using (PathGradientBrush gradient = new PathGradientBrush(path))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(
                            Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                            Convert.ToSingle(intRectangle.Bottom));
                        gradient.CenterColor = this.WFNewColorTable.ButtonDisabledCenter;
                        gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonDisabledOut };

                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0f, 0.8f, 0f };
                        blend.Positions = new float[] { 0f, 0.30f, 1f };

                        Region lastClip = g.Clip;
                        Region newClip = new Region(path);
                        newClip.Intersect(lastClip);
                        g.SetClip(newClip.GetBounds(g));
                        g.FillPath(gradient, path);
                        g.Clip = lastClip;
                    }
                }
                #endregion
                //
                #region Glossy
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                        this.WFNewColorTable.ButtonDisabledGlossyBegin, this.WFNewColorTable.ButtonDisabledGlossyEnd, 90))
                    {
                        //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path2);
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
        }
        private void DrawBaseButtonPressed(Graphics g, WFNew.IBaseButtonItem pBaseButtonItem, Rectangle rectangle, int iSpilt)
        {
            int iLeftTopRadius = pBaseButtonItem.LeftTopRadius;
            int iRightTopRadius = pBaseButtonItem.RightTopRadius;
            int iLeftBottomRadius = pBaseButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pBaseButtonItem.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0 ||
                glossyRectangle.Width <= 0 ||
                glossyRectangle.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pBaseButtonItem.pOwner as WFNew.IButtonGroupItem,
                pBaseButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                #region Main Bg
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                    //path2.CloseFigure();
                    using (PathGradientBrush gradient = new PathGradientBrush(path))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(
                            Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                            Convert.ToSingle(intRectangle.Bottom));
                        gradient.CenterColor = this.WFNewColorTable.ButtonPressedCenter;
                        gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonPressedOut };

                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0f, 0.8f, 0f };
                        blend.Positions = new float[] { 0f, 0.30f, 1f };

                        Region lastClip = g.Clip;
                        Region newClip = new Region(path);
                        newClip.Intersect(lastClip);
                        g.SetClip(newClip.GetBounds(g));
                        g.FillPath(gradient, path);
                        g.Clip = lastClip;
                    }
                }
                #endregion
                //
                #region Glossy
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                        this.WFNewColorTable.ButtonPressedGlossyBegin, this.WFNewColorTable.ButtonPressedGlossyEnd, 90))
                    {
                        //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path2);
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
        }
        private void DrawBaseButtonSelected(Graphics g, WFNew.IBaseButtonItem pBaseButtonItem, Rectangle rectangle, int iSpilt)
        {
            int iLeftTopRadius = pBaseButtonItem.LeftTopRadius;
            int iRightTopRadius = pBaseButtonItem.RightTopRadius;
            int iLeftBottomRadius = pBaseButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pBaseButtonItem.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
            Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
            //
            if (outRectangle.Width <= 0 ||
                outRectangle.Height <= 0 ||
                intRectangle.Width <= 0 ||
                intRectangle.Height <= 0 ||
                glossyRectangle.Width <= 0 ||
                glossyRectangle.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pBaseButtonItem.pOwner as WFNew.IButtonGroupItem,
                pBaseButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            #region Main Gradient
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                #region Main Bg
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                    //path2.CloseFigure();
                    using (PathGradientBrush gradient = new PathGradientBrush(path))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(
                            Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                            Convert.ToSingle(intRectangle.Bottom));
                        gradient.CenterColor = this.WFNewColorTable.ButtonSelectedCenter;
                        gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonSelectedOut };

                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0f, 0.8f, 0f };
                        blend.Positions = new float[] { 0f, 0.30f, 1f };

                        Region lastClip = g.Clip;
                        Region newClip = new Region(path);
                        newClip.Intersect(lastClip);
                        g.SetClip(newClip.GetBounds(g));
                        g.FillPath(gradient, path);
                        g.Clip = lastClip;
                    }
                }
                #endregion
                //
                #region Glossy
                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                        this.WFNewColorTable.ButtonSelectedGlossyBegin, this.WFNewColorTable.ButtonSelectedGlossyEnd, 90))
                    {
                        //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                        b.WrapMode = WrapMode.TileFlipXY;
                        g.FillPath(b, path2);
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            #endregion
        }
        private Color GetColor(Color color, int A, int R, int G, int B)
        {
            if (A + color.A > 255) { A = 255; } else { A = A + color.A; }
            if (R + color.R > 255) { R = 255; } else { R = R + color.R; }
            if (G + color.G > 255) { G = 255; } else { G = G + color.G; }
            if (B + color.B > 255) { B = 255; } else { B = B + color.B; }
            return Color.FromArgb(A, R, G, B);
        }
        #endregion

        #region GlyphButton
        public override void OnRenderGlyphButton(ObjectRenderEventArgs e)
        {
            WFNew.IGlyphButtonItem pGlyphButtonItem = e.Object as WFNew.IGlyphButtonItem;
            if (pGlyphButtonItem == null) return;
            switch (pGlyphButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pGlyphButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pGlyphButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pGlyphButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pGlyphButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pGlyphButtonItem, e.Bounds, 3); }
                    else if (pGlyphButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pGlyphButtonItem, e.Bounds, 3); }
                    break;
            }
            //
            Rectangle rectangle = pGlyphButtonItem.GlyphRectangle;
            if (rectangle.Width < 1 || rectangle.Height < 1) return;
            //
            Color arrowColor = pGlyphButtonItem.Enabled ? this.WFNewColorTable.Arrow : this.WFNewColorTable.ArrowDisabled;
            switch (pGlyphButtonItem.eGlyphStyle)
            {
                case GlyphStyle.eArrowUp:
                    this.DrawArrow(e.Graphics, rectangle, arrowColor, ArrowStyle.eToUp);
                    break;
                case GlyphStyle.eArrowDown:
                    this.DrawArrow(e.Graphics, rectangle, arrowColor, ArrowStyle.eToDown);
                    break;
                case GlyphStyle.eArrowLeft:
                    this.DrawArrow(e.Graphics, rectangle, arrowColor, ArrowStyle.eToLeft);
                    break;
                case GlyphStyle.eArrowRight:
                    this.DrawArrow(e.Graphics, rectangle, arrowColor, ArrowStyle.eToRight);
                    break;
                case GlyphStyle.eDirectionUp:
                    if (rectangle.Width > 6 && rectangle.Height > 6)
                    {
                        using (Pen p = new Pen(arrowColor))
                        {
                            int iX = (rectangle.Left + rectangle.Right) / 2 - 1;
                            int iNum = rectangle.Height / 5 - 1;
                            for (int i = 0; i < iNum; i++)
                            {
                                int iY = rectangle.Top + 2 + i * 5;
                                e.Graphics.DrawLine(p, iX, iY, iX - 5, iY + 5);
                                e.Graphics.DrawLine(p, iX, iY + 1, iX - 4, iY + 5);
                                e.Graphics.DrawLine(p, iX, iY, iX + 5, iY + 5);
                                e.Graphics.DrawLine(p, iX, iY + 1, iX + 4, iY + 5);
                            }
                        }
                    }
                    break;
                case GlyphStyle.eDirectionDown:
                    if (rectangle.Width > 6 && rectangle.Height > 6)
                    {
                        using (Pen p = new Pen(arrowColor))
                        {
                            int iX = (rectangle.Left + rectangle.Right) / 2 - 1;
                            int iNum = rectangle.Height / 5 - 1;
                            for (int i = iNum - 1; i >= 0; i--)
                            {
                                int iY = rectangle.Bottom - 3 - i * 5;
                                e.Graphics.DrawLine(p, iX, iY, iX - 5, iY - 5);
                                e.Graphics.DrawLine(p, iX, iY - 1, iX - 4, iY - 5);
                                e.Graphics.DrawLine(p, iX, iY, iX + 5, iY - 5);
                                e.Graphics.DrawLine(p, iX, iY - 1, iX + 4, iY - 5);
                            }
                        }
                    }
                    break;
                case GlyphStyle.eDirectionLeft:
                    if (rectangle.Width > 6 && rectangle.Height > 6)
                    {
                        using (Pen p = new Pen(arrowColor))
                        {
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            int iNum = rectangle.Width / 5 - 1;
                            for (int i = 0; i < iNum; i++)
                            {
                                int iX = rectangle.Left + 2 + i * 5;
                                e.Graphics.DrawLine(p, iX, iY, iX + 5, iY + 5);
                                e.Graphics.DrawLine(p, iX + 1, iY, iX + 5, iY + 4);
                                e.Graphics.DrawLine(p, iX, iY, iX + 5, iY - 5);
                                e.Graphics.DrawLine(p, iX + 1, iY, iX + 5, iY - 4);
                            }
                        }
                    }
                    break;
                case GlyphStyle.eDirectionRight:
                    if (rectangle.Width > 6 && rectangle.Height > 6)
                    {
                        using (Pen p = new Pen(arrowColor))
                        {
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            int iNum = rectangle.Width / 5 - 1;
                            for (int i = iNum - 1; i >= 0; i--)
                            {
                                int iX = rectangle.Right - 3 - i * 5;
                                e.Graphics.DrawLine(p, iX, iY, iX - 5, iY + 5);
                                e.Graphics.DrawLine(p, iX - 1, iY, iX - 5, iY + 4);
                                e.Graphics.DrawLine(p, iX, iY, iX - 5, iY - 5);
                                e.Graphics.DrawLine(p, iX - 1, iY, iX - 5, iY - 4);
                            }
                        }
                    }
                    break;
                case GlyphStyle.eMinus:
                    if (rectangle.Width >= 2)
                    {
                        using (SolidBrush b = new SolidBrush(arrowColor))
                        {
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            e.Graphics.FillRectangle(b, new Rectangle(rectangle.Left, iY, rectangle.Width, 2));
                        }
                    }
                    break;
                case GlyphStyle.ePlus:
                    if (rectangle.Width >= 2 && rectangle.Height >= 2)
                    {
                        using (SolidBrush b = new SolidBrush(arrowColor))
                        {
                            int iX = (rectangle.Left + rectangle.Right) / 2 - 1;
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            e.Graphics.FillRectangle(b, new Rectangle(iX, rectangle.Top, 2, rectangle.Height));
                            e.Graphics.FillRectangle(b, new Rectangle(rectangle.Left, iY, rectangle.Width, 2));
                        }
                    }
                    break;
                case GlyphStyle.ePointsVertical:
                    if (rectangle.Height > 4)
                    {
                        using (SolidBrush b = new SolidBrush(arrowColor))
                        {
                            int iX = (rectangle.Left + rectangle.Right) / 2 - 1;
                            int iNum = rectangle.Height / 4;
                            for (int i = 0; i < iNum; i++)
                            {
                                e.Graphics.FillRectangle(b, new Rectangle(iX, rectangle.Top + 1 + i * 4, 2, 2));
                            }
                        }
                    }
                    break;
                case GlyphStyle.ePointsHorizontal:
                    if (rectangle.Width > 4)
                    {
                        using(SolidBrush b = new SolidBrush(arrowColor ))
                        {
                            int iY = (rectangle.Top + rectangle.Bottom) / 2 - 1;
                            int iNum = rectangle.Width / 4;
                            for (int i = 0; i < iNum; i++)
                            {
                                e.Graphics.FillRectangle(b, new Rectangle(rectangle.Left + 1 + i * 4, iY, 2, 2));
                            }
                        }
                    }
                    break;
                case GlyphStyle.eCross:
                    if (rectangle.Width < rectangle.Height) 
                    {
                        rectangle = new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - rectangle.Width) / 2, rectangle.Width, rectangle.Width);
                    }
                    else if (rectangle.Width > rectangle.Height)
                    {
                        rectangle = new Rectangle((rectangle.Left + rectangle.Right - rectangle.Height) / 2, rectangle.Top, rectangle.Height, rectangle.Height);
                    }
                    using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region DescriptionButton
        public override void OnRenderDescriptionButton(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
        }
        #endregion

        #region CheckButton
        public override void OnRenderCheckButton(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
        }
        #endregion

        #region DropDownButton
        public override void OnRenderDropDownButton(ObjectRenderEventArgs e)
        {
            WFNew.IBaseButtonItem pBaseButtonItem = e.Object as WFNew.IBaseButtonItem;
            if (pBaseButtonItem == null) return;
            switch (pBaseButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pBaseButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pBaseButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    else if (pBaseButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pBaseButtonItem, e.Bounds, 3); }
                    break;
            }
        }
        #endregion

        #region SplitButton
        public override void OnRenderSplitButton(ObjectRenderEventArgs e)
        {
            WFNew.ISplitButtonItem pButtonItem = e.Object as WFNew.ISplitButtonItem;
            if (pButtonItem == null) return;
            switch (pButtonItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawSplitButtonSelected(e.Graphics, pButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawSplitButtonPressed(e.Graphics, pButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawSplitButtonDisabled(e.Graphics, pButtonItem, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pButtonItem.Checked) { this.DrawSplitButtonNomal(e.Graphics, pButtonItem, e.Bounds, 3); }
                    else if (pButtonItem.NomalChecked) { this.DrawSplitButtonChecked(e.Graphics, pButtonItem, e.Bounds, 3); }
                    break;
            }
        }
        private void DrawSplitButtonNomal(Graphics g, WFNew.ISplitButtonItem pButtonItem, Rectangle rectangle, int iSpilt)
        {
            if (pButtonItem.ShowNomalState)
            {
                this.DrawBaseButtonNomal(g, pButtonItem, rectangle, iSpilt);
                //
                if (pButtonItem.ShowNomalSplitLine)
                {
                    int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
                    Rectangle splitRectangle = pButtonItem.SplitRectangle;
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                    {
                        this.GetSplitLine1(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderIn))
                    {
                        this.GetSplitLine2(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                }
            }
            else 
            {
                if (pButtonItem.ShowNomalSplitLine)
                {
                    int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
                    Rectangle splitRectangle = pButtonItem.SplitRectangle;
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                    {
                        this.GetSplitLine3(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                }
            }
        }
        private void DrawSplitButtonChecked(Graphics g, WFNew.ISplitButtonItem pButtonItem, Rectangle rectangle, int iSpilt)
        {
            this.DrawBaseButtonChecked(g, pButtonItem, rectangle, iSpilt);
            //
            if (pButtonItem.ShowNomalSplitLine)
            {
                int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
                Rectangle splitRectangle = pButtonItem.SplitRectangle;
                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedBorderOut))
                {
                    this.GetSplitLine1(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                    g.DrawLine(p, oneX, oneY, twoX, twoY);
                }
                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedBorderIn))
                {
                    this.GetSplitLine2(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                    g.DrawLine(p, oneX, oneY, twoX, twoY);
                }
            }
        }
        private void DrawSplitButtonDisabled(Graphics g, WFNew.ISplitButtonItem pButtonItem, Rectangle rectangle, int iSpilt)
        {
            if (pButtonItem.ShowNomalState)
            {
                this.DrawBaseButtonDisabled(g, pButtonItem, rectangle, iSpilt);
                //
                if (pButtonItem.ShowNomalSplitLine)
                {
                    int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
                    Rectangle splitRectangle = pButtonItem.SplitRectangle;
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderOut))
                    {
                        this.GetSplitLine1(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderIn))
                    {
                        this.GetSplitLine2(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                }
            }
            else
            {
                if (pButtonItem.ShowNomalSplitLine)
                {
                    int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
                    Rectangle splitRectangle = pButtonItem.SplitRectangle;
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderOut))
                    {
                        this.GetSplitLine3(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
                        g.DrawLine(p, oneX, oneY, twoX, twoY);
                    }
                }
            }
        }
        private void DrawSplitButtonPressed(Graphics g, WFNew.ISplitButtonItem pButtonItem, Rectangle rectangle, int iSpilt)
        {
            int iLeftTopRadius = pButtonItem.LeftTopRadius;
            int iRightTopRadius = pButtonItem.RightTopRadius;
            int iLeftBottomRadius = pButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pButtonItem.RightBottomRadius;

            Rectangle outBounds = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intBounds = new Rectangle(outBounds.X + 1, outBounds.Y + 1, outBounds.Width - 2, outBounds.Height - 2);
            //
            if (outBounds.Width <= 0 ||
                outBounds.Height <= 0 ||
                intBounds.Width <= 0 ||
                intBounds.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pButtonItem.pOwner as WFNew.IButtonGroupItem,
                pButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outBounds,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intBounds,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                #region Main Bg
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    //path2.AddEllipse(new Rectangle(intBounds.Left, intBounds.Top, intBounds.Width, intBounds.Height * 2));
                    //path2.CloseFigure();
                    using (PathGradientBrush gradient = new PathGradientBrush(path))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(
                            Convert.ToSingle(intBounds.Left + intBounds.Width / 2),
                            Convert.ToSingle(intBounds.Bottom));
                        gradient.CenterColor = this.WFNewColorTable.ButtonPressedOut;
                        gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonPressedCenter };

                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0f, 0.8f, 0f };
                        blend.Positions = new float[] { 0f, 0.30f, 1f };

                        Region lastClip = g.Clip;
                        Region newClip = new Region(path);
                        newClip.Intersect(lastClip);
                        g.SetClip(newClip.GetBounds(g));
                        g.FillPath(gradient, path);
                        g.Clip = lastClip;
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            //
            //
            this.DrawSplitRegion_Pressed(g, pButtonItem, iSpilt);
        }
        private void DrawSplitButtonSelected(Graphics g, WFNew.ISplitButtonItem pButtonItem, Rectangle rectangle, int iSpilt)
        {
            int iLeftTopRadius = pButtonItem.LeftTopRadius;
            int iRightTopRadius = pButtonItem.RightTopRadius;
            int iLeftBottomRadius = pButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pButtonItem.RightBottomRadius;

            Rectangle outBounds = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            Rectangle intBounds = new Rectangle(outBounds.X + 1, outBounds.Y + 1, outBounds.Width - 2, outBounds.Height - 2);
            //
            if (outBounds.Width <= 0 ||
                outBounds.Height <= 0 ||
                intBounds.Width <= 0 ||
                intBounds.Height <= 0) return;
            //
            this.GetButtonItemRadiusInfo(pButtonItem.pOwner as WFNew.IButtonGroupItem,
                pButtonItem as WFNew.BaseItem,
                ref iLeftTopRadius, ref iRightTopRadius, ref iLeftBottomRadius, ref iRightBottomRadius);
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outBounds,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
                {
                    g.DrawPath(p, path);
                }
            }
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intBounds,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                #region Main Bg
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    //path2.AddEllipse(new Rectangle(intBounds.Left, intBounds.Top, intBounds.Width, intBounds.Height * 2));
                    //path2.CloseFigure();
                    using (PathGradientBrush gradient = new PathGradientBrush(path))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(
                            Convert.ToSingle(intBounds.Left + intBounds.Width / 2),
                            Convert.ToSingle(intBounds.Bottom));
                        gradient.CenterColor = this.WFNewColorTable.ButtonSelectedOut;
                        gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonSelectedCenter };

                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0f, 0.8f, 0f };
                        blend.Positions = new float[] { 0f, 0.30f, 1f };

                        Region lastClip = g.Clip;
                        Region newClip = new Region(path);
                        newClip.Intersect(lastClip);
                        g.SetClip(newClip.GetBounds(g));
                        g.FillPath(gradient, path);
                        g.Clip = lastClip;
                    }
                }
                #endregion
                //
                using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
                {
                    g.DrawPath(p, path);
                }
            }
            //
            //
            //
            this.DrawSplitRegion_Selected(g, pButtonItem, iSpilt);
        }
        private void DrawSplitRegion_Pressed(Graphics g, WFNew.ISplitButtonItem pButtonItem, int iSpilt)
        {
            int iLeftTopRadius = pButtonItem.LeftTopRadius;
            int iRightTopRadius = pButtonItem.RightTopRadius;
            int iLeftBottomRadius = pButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pButtonItem.RightBottomRadius;

            if (pButtonItem.eSplitState == GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed)
            {
                switch (pButtonItem.eArrowDock)
                {
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                        iLeftTopRadius = 0;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                        iLeftTopRadius = 0;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                }
                //
                #region Main Gradient
                Rectangle rectangle = pButtonItem.SplitRectangle;
                Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
                Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
                //
                if (outRectangle.Width <= 0 ||
                    outRectangle.Height <= 0 ||
                    intRectangle.Width <= 0 ||
                    intRectangle.Height <= 0 ||
                    glossyRectangle.Width <= 0 ||
                    glossyRectangle.Height <= 0) return;
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    #region Main Bg
                    using (GraphicsPath path2 = new GraphicsPath())
                    {
                        //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                        //path2.CloseFigure();
                        using (PathGradientBrush gradient = new PathGradientBrush(path))
                        {
                            gradient.WrapMode = WrapMode.Clamp;
                            gradient.CenterPoint = new PointF(
                                Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                                Convert.ToSingle(intRectangle.Bottom));
                            gradient.CenterColor = this.WFNewColorTable.ButtonPressedCenter;
                            gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonPressedOut };

                            Blend blend = new Blend(3);
                            blend.Factors = new float[] { 0f, 0.8f, 0f };
                            blend.Positions = new float[] { 0f, 0.30f, 1f };

                            Region lastClip = g.Clip;
                            Region newClip = new Region(path);
                            newClip.Intersect(lastClip);
                            g.SetClip(newClip.GetBounds(g));
                            g.FillPath(gradient, path);
                            g.Clip = lastClip;
                        }
                    }
                    #endregion
                    //
                    #region Glossy
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                            this.WFNewColorTable.ButtonPressedGlossyBegin, this.WFNewColorTable.ButtonPressedGlossyEnd, 90))
                        {
                            //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                            b.WrapMode = WrapMode.TileFlipXY;
                            g.FillPath(b, path2);
                        }
                    }
                    #endregion
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
                #endregion
            }
            else //if (pButtonItem.eSplitState == GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal)
            {
                switch (pButtonItem.eArrowDock)
                {
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                        iLeftTopRadius = 0;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                        iLeftTopRadius = 0;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        iRightBottomRadius = 0;
                        break;
                }
                //
                #region Main Gradient
                Rectangle rectangle = pButtonItem.ButtonRectangle;
                Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
                Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
                //
                if (outRectangle.Width <= 0 ||
                    outRectangle.Height <= 0 ||
                    intRectangle.Width <= 0 ||
                    intRectangle.Height <= 0 ||
                    glossyRectangle.Width <= 0 ||
                    glossyRectangle.Height <= 0) return;
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    #region Main Bg
                    using (GraphicsPath path2 = new GraphicsPath())
                    {
                        //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                        //path2.CloseFigure();
                        using (PathGradientBrush gradient = new PathGradientBrush(path))
                        {
                            gradient.WrapMode = WrapMode.Clamp;
                            gradient.CenterPoint = new PointF(
                                Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                                Convert.ToSingle(intRectangle.Bottom));
                            gradient.CenterColor = this.WFNewColorTable.ButtonPressedCenter;
                            gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonPressedOut };

                            Blend blend = new Blend(3);
                            blend.Factors = new float[] { 0f, 0.8f, 0f };
                            blend.Positions = new float[] { 0f, 0.30f, 1f };

                            Region lastClip = g.Clip;
                            Region newClip = new Region(path);
                            newClip.Intersect(lastClip);
                            g.SetClip(newClip.GetBounds(g));
                            g.FillPath(gradient, path);
                            g.Clip = lastClip;
                        }
                    }
                    #endregion
                    //
                    #region Glossy
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                            this.WFNewColorTable.ButtonPressedGlossyBegin, this.WFNewColorTable.ButtonPressedGlossyEnd, 90))
                        {
                            //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                            b.WrapMode = WrapMode.TileFlipXY;
                            g.FillPath(b, path2);
                        }
                    }
                    #endregion
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
                #endregion
            }
            ////
            //int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
            //Rectangle splitRectangle = pButtonItem.SplitRectangle;
            //using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderOut))
            //{
            //    this.GetSplitLine1(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
            //    g.DrawLine(p, oneX, oneY, twoX, twoY);
            //}
            //using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
            //{
            //    this.GetSplitLine2(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
            //    g.DrawLine(p, oneX, oneY, twoX, twoY);
            //}
        }
        private void DrawSplitRegion_Selected(Graphics g, WFNew.ISplitButtonItem pButtonItem, int iSpilt)
        {
            int iLeftTopRadius = pButtonItem.LeftTopRadius;
            int iRightTopRadius = pButtonItem.RightTopRadius;
            int iLeftBottomRadius = pButtonItem.LeftBottomRadius;
            int iRightBottomRadius = pButtonItem.RightBottomRadius;

            if (pButtonItem.eSplitState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
            {
                switch (pButtonItem.eArrowDock)
                {
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                        iLeftTopRadius = 0;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                        iLeftTopRadius = 0;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                }
                //
                #region Main Gradient
                Rectangle rectangle = pButtonItem.SplitRectangle;
                Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
                Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
                //
                if (outRectangle.Width <= 0 ||
                    outRectangle.Height <= 0 ||
                    intRectangle.Width <= 0 ||
                    intRectangle.Height <= 0 ||
                    glossyRectangle.Width <= 0 ||
                    glossyRectangle.Height <= 0) return;
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    #region Main Bg
                    using (GraphicsPath path2 = new GraphicsPath())
                    {
                        //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                        //path2.CloseFigure();
                        using (PathGradientBrush gradient = new PathGradientBrush(path))
                        {
                            gradient.WrapMode = WrapMode.Clamp;
                            gradient.CenterPoint = new PointF(
                                Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                                Convert.ToSingle(intRectangle.Bottom));
                            gradient.CenterColor = this.WFNewColorTable.ButtonSelectedCenter;
                            gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonSelectedOut };

                            Blend blend = new Blend(3);
                            blend.Factors = new float[] { 0f, 0.8f, 0f };
                            blend.Positions = new float[] { 0f, 0.30f, 1f };

                            Region lastClip = g.Clip;
                            Region newClip = new Region(path);
                            newClip.Intersect(lastClip);
                            g.SetClip(newClip.GetBounds(g));
                            g.FillPath(gradient, path);
                            g.Clip = lastClip;
                        }
                    }
                    #endregion
                    //
                    #region Glossy
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                            this.WFNewColorTable.ButtonSelectedGlossyBegin, this.WFNewColorTable.ButtonSelectedGlossyEnd, 90))
                        {
                            //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                            b.WrapMode = WrapMode.TileFlipXY;
                            g.FillPath(b, path2);
                        }
                    }
                    #endregion
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
                #endregion
            }
            else //if (pButtonItem.eSplitState == GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal)
            {
                switch (pButtonItem.eArrowDock)
                {
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                        iLeftTopRadius = 0;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = 0;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                        iLeftTopRadius = 0;
                        //iRightTopRadius = pButtonItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        //iRightBottomRadius = pButtonItem.RightBottomRadius;
                        break;
                    case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                        //iLeftTopRadius = pButtonItem.LeftTopRadius;
                        iRightTopRadius = 0;
                        //iLeftBottomRadius = pButtonItem.LeftBottomRadius;
                        iRightBottomRadius = 0;
                        break;
                }
                //
                #region Main Gradient
                Rectangle rectangle = pButtonItem.ButtonRectangle;
                Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
                Rectangle intRectangle = new Rectangle(outRectangle.X + 1, outRectangle.Y + 1, outRectangle.Width - 2, outRectangle.Height - 2);
                Rectangle glossyRectangle = new Rectangle(intRectangle.X, intRectangle.Y, intRectangle.Width, intRectangle.Height / iSpilt);
                //
                if (outRectangle.Width <= 0 ||
                    outRectangle.Height <= 0 ||
                    intRectangle.Width <= 0 ||
                    intRectangle.Height <= 0 ||
                    glossyRectangle.Width <= 0 ||
                    glossyRectangle.Height <= 0) return;
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
                //
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(intRectangle,
                    iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
                {
                    #region Main Bg
                    using (GraphicsPath path2 = new GraphicsPath())
                    {
                        //path2.AddEllipse(new Rectangle(intRectangle.Left, intRectangle.Top, intRectangle.Width, intRectangle.Height * 2));
                        //path2.CloseFigure();
                        using (PathGradientBrush gradient = new PathGradientBrush(path))
                        {
                            gradient.WrapMode = WrapMode.Clamp;
                            gradient.CenterPoint = new PointF(
                                Convert.ToSingle(intRectangle.Left + intRectangle.Width / 2),
                                Convert.ToSingle(intRectangle.Bottom));
                            gradient.CenterColor = this.WFNewColorTable.ButtonSelectedCenter;
                            gradient.SurroundColors = new Color[] { this.WFNewColorTable.ButtonSelectedOut };

                            Blend blend = new Blend(3);
                            blend.Factors = new float[] { 0f, 0.8f, 0f };
                            blend.Positions = new float[] { 0f, 0.30f, 1f };

                            Region lastClip = g.Clip;
                            Region newClip = new Region(path);
                            newClip.Intersect(lastClip);
                            g.SetClip(newClip.GetBounds(g));
                            g.FillPath(gradient, path);
                            g.Clip = lastClip;
                        }
                    }
                    #endregion
                    //
                    #region Glossy
                    using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyRectangle, iLeftTopRadius, iRightTopRadius, 0, 0))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(glossyRectangle,
                            this.WFNewColorTable.ButtonSelectedGlossyBegin, this.WFNewColorTable.ButtonSelectedGlossyEnd, 90))
                        {
                            //g.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), path);
                            b.WrapMode = WrapMode.TileFlipXY;
                            g.FillPath(b, path2);
                        }
                    }
                    #endregion
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
                #endregion
            }
            ////
            //int oneX = 0, oneY = 0, twoX = 0, twoY = 0;
            //Rectangle splitRectangle = pButtonItem.SplitRectangle;
            //using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderOut))
            //{
            //    this.GetSplitLine1(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
            //    g.DrawLine(p, oneX, oneY, twoX, twoY);
            //}
            //using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
            //{
            //    this.GetSplitLine2(pButtonItem.eArrowDock, splitRectangle, ref oneX, ref oneY, ref twoX, ref twoY);
            //    g.DrawLine(p, oneX, oneY, twoX, twoY);
            //}
        }
        private void GetSplitLine1(GISShare.Controls.WinForm.WFNew.ArrowDock eArrowDock, Rectangle splitRectangle,
            ref int oneX, ref int oneY, ref int twoX, ref int twoY)
        {
            switch (eArrowDock)
            {
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                    oneX = splitRectangle.Left + 3;
                    oneY = splitRectangle.Bottom;
                    twoX = splitRectangle.Right - 3;
                    twoY = splitRectangle.Bottom;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                    oneX = splitRectangle.Right;
                    oneY = splitRectangle.Top + 3;
                    twoX = splitRectangle.Right;
                    twoY = splitRectangle.Bottom - 3;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                    oneX = splitRectangle.Left;
                    oneY = splitRectangle.Top + 3;
                    twoX = splitRectangle.Left;
                    twoY = splitRectangle.Bottom - 3;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                    oneX = splitRectangle.Left + 3;
                    oneY = splitRectangle.Top;
                    twoX = splitRectangle.Right - 3;
                    twoY = splitRectangle.Top;
                    break;
                default:
                    break;
            }
        }
        private void GetSplitLine2(GISShare.Controls.WinForm.WFNew.ArrowDock eArrowDock, Rectangle splitRectangle,
            ref int oneX, ref int oneY, ref int twoX, ref int twoY)
        {
            switch (eArrowDock)
            {
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                    oneX = splitRectangle.Left + 3;
                    oneY = splitRectangle.Bottom - 1;
                    twoX = splitRectangle.Right - 3;
                    twoY = splitRectangle.Bottom - 1;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                    oneX = splitRectangle.Right - 1;
                    oneY = splitRectangle.Top + 3;
                    twoX = splitRectangle.Right - 1;
                    twoY = splitRectangle.Bottom - 3;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                    oneX = splitRectangle.Left + 1;
                    oneY = splitRectangle.Top + 3;
                    twoX = splitRectangle.Left + 1;
                    twoY = splitRectangle.Bottom - 3;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                    oneX = splitRectangle.Left + 3;
                    oneY = splitRectangle.Top + 1;
                    twoX = splitRectangle.Right - 3;
                    twoY = splitRectangle.Top + 1;
                    break;
                default:
                    break;
            }
        }
        private void GetSplitLine3(GISShare.Controls.WinForm.WFNew.ArrowDock eArrowDock, Rectangle splitRectangle,
            ref int oneX, ref int oneY, ref int twoX, ref int twoY)
        {
            switch (eArrowDock)
            {
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eUp:
                    oneX = splitRectangle.Left + 5;
                    oneY = splitRectangle.Bottom - 1;
                    twoX = splitRectangle.Right - 5;
                    twoY = splitRectangle.Bottom - 1;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eLeft:
                    oneX = splitRectangle.Right - 1;
                    oneY = splitRectangle.Top + 5;
                    twoX = splitRectangle.Right - 1;
                    twoY = splitRectangle.Bottom - 5;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eRight:
                    oneX = splitRectangle.Left + 1;
                    oneY = splitRectangle.Top + 5;
                    twoX = splitRectangle.Left + 1;
                    twoY = splitRectangle.Bottom - 5;
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowDock.eDown:
                    oneX = splitRectangle.Left + 5;
                    oneY = splitRectangle.Top + 1;
                    twoX = splitRectangle.Right - 5;
                    twoY = splitRectangle.Top + 1;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ProcessBar
        public override void OnRenderProcessBar(ObjectRenderEventArgs e) 
        {
            IProcessBarItem pProcessBarItem = e.Object as IProcessBarItem;
            if (pProcessBarItem == null) return;
            Rectangle rectangle = pProcessBarItem.FrameRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pProcessBarItem.LeftTopRadius, pProcessBarItem.RightTopRadius, pProcessBarItem.LeftBottomRadius, pProcessBarItem.RightBottomRadius))
            {
                if (pProcessBarItem.Percentage >= 100)
                {
                    switch (pProcessBarItem.eOrientation)
                    {
                        case Orientation.Vertical:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ProcessBarValueEnd, this.WFNewColorTable.ProcessBarValueBegin, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                        case Orientation.Horizontal:
                        default:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ProcessBarValueBegin, this.WFNewColorTable.ProcessBarValueEnd, LinearGradientMode.Horizontal))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                    }
                }
                else if (pProcessBarItem.Percentage > 0)
                {
                    switch (pProcessBarItem.eOrientation)
                    {
                        case Orientation.Vertical:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                                   this.WFNewColorTable.ProcessBarBackgroundBegin, this.WFNewColorTable.ProcessBarBackgroundEnd,
                                   pProcessBarItem.eOrientation == Orientation.Horizontal ? LinearGradientMode.Horizontal : LinearGradientMode.Horizontal))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            int iH = rectangle.Height * pProcessBarItem.Percentage / 100;
                            if (iH > 0)
                            {
                                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(new Rectangle(rectangle.Left, rectangle.Bottom - iH + 1, rectangle.Width, iH),
                                    0, 0, pProcessBarItem.LeftBottomRadius, pProcessBarItem.RightBottomRadius))
                                {
                                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ProcessBarValueEnd, this.WFNewColorTable.ProcessBarValueBegin, LinearGradientMode.Vertical))
                                    {
                                        e.Graphics.FillPath(b, path2);
                                    }
                                }
                            }
                            break;
                        case Orientation.Horizontal:
                        default:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                                   this.WFNewColorTable.ProcessBarBackgroundEnd, this.WFNewColorTable.ProcessBarBackgroundBegin,
                                   pProcessBarItem.eOrientation == Orientation.Horizontal ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            int iW = rectangle.Width * pProcessBarItem.Percentage / 100;
                            if (iW > 0)
                            {
                                using (GraphicsPath path2 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(new Rectangle(rectangle.Left, rectangle.Top, iW, rectangle.Height),
                                    pProcessBarItem.LeftTopRadius, 0, pProcessBarItem.LeftBottomRadius, 0))
                                {
                                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ProcessBarValueBegin, this.WFNewColorTable.ProcessBarValueEnd, LinearGradientMode.Horizontal))
                                    {
                                        e.Graphics.FillPath(b, path2);
                                    }
                                }
                            }
                            break;
                    }
                }
                //
                rectangle.Inflate(-1, -1);
                using (GraphicsPath path3 = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pProcessBarItem.LeftTopRadius, pProcessBarItem.RightTopRadius, pProcessBarItem.LeftBottomRadius, pProcessBarItem.RightBottomRadius))
                {
                    using (Pen p3 = new Pen(this.WFNewColorTable.ProcessBarBorderIn))
                    {
                        e.Graphics.DrawPath(p3, path3);
                    }
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.ProcessBarBorderOut))
                {
                    e.Graphics.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region PlayProcessBar
        public override void OnRenderPlayProcessBar(ObjectRenderEventArgs e)
        {
            this.OnRenderProcessBar(e);
        }
        #endregion

        #region PlayProcessBarButton
        public override void OnRenderPlayProcessBarButton(ObjectRenderEventArgs e)
        {
            IPlayProcessBarButton pPlayProcessBarButton = e.Object as IPlayProcessBarButton;
            if (pPlayProcessBarButton == null) return;
            //
            switch (pPlayProcessBarButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pPlayProcessBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pPlayProcessBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pPlayProcessBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pPlayProcessBarButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pPlayProcessBarButton, e.Bounds, 2); }
                    else if (pPlayProcessBarButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pPlayProcessBarButton, e.Bounds, 2); }
                    break;
            }
        }
        #endregion

        #region Slider
        public override void OnRenderSlider(ObjectRenderEventArgs e)
        {
            ISliderItem pSliderItem = e.Object as ISliderItem;
            if (pSliderItem == null) return;
            //
            Rectangle rectangle = pSliderItem.SliderAreaRectangle;
            switch (pSliderItem.eOrientation)
            {
                case Orientation.Vertical:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 5, rectangle.Top + 1, rectangle.Right - 5, rectangle.Bottom - 1);
                    using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                    {
                        e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2 + 1, rectangle.Top + 3, (rectangle.Left + rectangle.Right) / 2 + 1, rectangle.Bottom - 3);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2, rectangle.Top + 3, (rectangle.Left + rectangle.Right) / 2, rectangle.Bottom - 3);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, (rectangle.Top + rectangle.Bottom) / 2, rectangle.Right, (rectangle.Top + rectangle.Bottom) / 2);
                    }
                    break;
                case Orientation.Horizontal:
                default:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 5, rectangle.Right - 1, rectangle.Bottom - 5);
                    using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2 + 1, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2 + 1);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2, rectangle.Top, (rectangle.Left + rectangle.Right) / 2, rectangle.Bottom);
                    }
                    break;
            }
        }
        #endregion

        #region SliderButton
        public override void OnRenderSliderButton(ObjectRenderEventArgs e)
        {
            ISliderButton pSliderButton = e.Object as ISliderButton;
            if (pSliderButton == null) return;
            //
            switch (pSliderButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pSliderButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pSliderButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pSliderButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pSliderButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pSliderButton, e.Bounds, 2); }
                    else if (pSliderButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pSliderButton, e.Bounds, 2); }
                    break;
            }
            //
            Rectangle rectangle = pSliderButton.DisplayRectangle;
            switch (pSliderButton.eSliderButtonStyle) 
            {
                case SliderButtonStyle.eMinusButton:
                    using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                    {
                        p.Width = 2;
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2 + 1, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2 + 1);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        p.Width = 2;
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2);
                    }
                    break;
                case SliderButtonStyle.ePlusButton:
                    using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                    {
                        p.Width = 2;
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2 + 1, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2 + 1);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        p.Width = 2;
                        e.Graphics.DrawLine(p, rectangle.Left + 3, (rectangle.Top + rectangle.Bottom) / 2, rectangle.Right - 3, (rectangle.Top + rectangle.Bottom) / 2);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        p.Width = 2;
                        e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2, rectangle.Top + 3, (rectangle.Left + rectangle.Right) / 2, rectangle.Bottom - 3);
                    }
                    break;
                case SliderButtonStyle.eSliderButton:
                    if (pSliderButton.eOrientation == Orientation.Horizontal)
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2, rectangle.Top + 5, (rectangle.Left + rectangle.Right) / 2, rectangle.Bottom - 4);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, (rectangle.Left + rectangle.Right) / 2 - 1, rectangle.Top + 4, (rectangle.Left + rectangle.Right) / 2 - 1, rectangle.Bottom - 5);
                        }
                    }
                    else
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, rectangle.Left + 4, (rectangle.Top + rectangle.Bottom) / 2, rectangle.Right - 5, (rectangle.Top + rectangle.Bottom) / 2);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, rectangle.Left + 4, (rectangle.Top + rectangle.Bottom) / 2 - 1, rectangle.Right - 5, (rectangle.Top + rectangle.Bottom) / 2 - 1);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ScrollBar
        public override void OnRenderScrollBar(ObjectRenderEventArgs e)
        {
            if (e.Bounds.Width <= 0 || e.Bounds.Height <= 0) return;
            //
            IScrollBarItem pScrollBarItem = e.Object as IScrollBarItem;
            if (pScrollBarItem == null) return;
            //
            switch (pScrollBarItem.eOrientation)
            {
                case Orientation.Vertical:
                    using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.ScrollBarBackgroundBegin, this.WFNewColorTable.ScrollBarBackgroundEnd, LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillRectangle(b, e.Bounds);
                    }
                    break;
                case Orientation.Horizontal:
                default:
                    using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WFNewColorTable.ScrollBarBackgroundBegin, this.WFNewColorTable.ScrollBarBackgroundEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, e.Bounds);
                    }
                    break;
            }
        }
        #endregion

        #region Star
        public override void OnRenderStar(ObjectRenderEventArgs e)
        {
            if (e.Bounds.Width <= 0 || e.Bounds.Height <= 0) return;
            //
            IRatingStarItem pRatingStarItem = e.Object as IRatingStarItem;
            if (pRatingStarItem == null) return;
            //
            for (int i = 0; i < pRatingStarItem.StarCount; i++)
            {
                Rectangle rectangle = new Rectangle
                                (
                                e.Bounds.Left + i * (pRatingStarItem.StarSize + pRatingStarItem.StarSpace),
                                (e.Bounds.Top + e.Bounds.Bottom - pRatingStarItem.StarSize) / 2,
                                pRatingStarItem.StarSize,
                                pRatingStarItem.StarSize
                                );
                //
                if (e.Bounds.Right < rectangle.Right) break;
                //
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    PointF[] pointArray = GISShare.Controls.WinForm.Util.UtilTX.CreateStar(rectangle);
                    //
                    if (pRatingStarItem.Value > i)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.RatingStarSelectedBegin, this.WFNewColorTable.RatingStarSelectedEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillPolygon(b, pointArray);
                        }
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.RatingStarBorder))
                    {
                        e.Graphics.DrawPolygon(p, pointArray);
                    }
                }
            }
        }
        #endregion

        #region ScrollBarButton
        public override void OnRenderScrollBarButton(ObjectRenderEventArgs e)
        {
            IScrollBarButton pScrollBarButton = e.Object as IScrollBarButton;
            if (pScrollBarButton == null) return;
            //
            switch (pScrollBarButton.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawBaseButtonSelected(e.Graphics, pScrollBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawBaseButtonPressed(e.Graphics, pScrollBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawBaseButtonDisabled(e.Graphics, pScrollBarButton, e.Bounds, 2);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pScrollBarButton.Checked) { this.DrawBaseButtonNomal(e.Graphics, pScrollBarButton, e.Bounds, 2); }
                    else if (pScrollBarButton.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pScrollBarButton, e.Bounds, 2); }
                    break;
            }
            //
            Rectangle rectangle = pScrollBarButton.DisplayRectangle;
            switch (pScrollBarButton.eScrollBarButtonStyle) 
            {
                case ScrollBarButtonStyle.eMinusButton:
                    if (pScrollBarButton.eOrientation == Orientation.Vertical)
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X - 2, pointCenter.Y + 1, pointCenter.X + 2, pointCenter.Y + 1);
                        }
                        pointCenter.Offset(0, 1);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X - 2, pointCenter.Y + 1, pointCenter.X + 2, pointCenter.Y + 1);
                        }
                    }
                    else
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2 - 1, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X + 1, pointCenter.Y - 2, pointCenter.X + 1, pointCenter.Y + 2);
                        }
                        pointCenter.Offset(1, 0);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X + 1, pointCenter.Y - 2, pointCenter.X + 1, pointCenter.Y + 2);
                        }
                    }
                    break;
                case ScrollBarButtonStyle.ePlusButton:
                    if (pScrollBarButton.eOrientation == Orientation.Vertical)
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y + 1, pointCenter.X, pointCenter.Y - 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X - 2, pointCenter.Y - 1, pointCenter.X + 2, pointCenter.Y - 1);
                        }
                        pointCenter.Offset(0, -1);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y + 1, pointCenter.X, pointCenter.Y - 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y, pointCenter.X + 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X - 2, pointCenter.Y - 1, pointCenter.X + 2, pointCenter.Y - 1);
                        }
                    }
                    else
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2 + 1, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X + 1, pointCenter.Y, pointCenter.X - 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y - 2, pointCenter.X - 1, pointCenter.Y + 2);
                        }
                        pointCenter.Offset(-1, 0);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            e.Graphics.DrawLine(p, pointCenter.X + 1, pointCenter.Y, pointCenter.X - 1, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 1, pointCenter.X, pointCenter.Y + 1);
                            e.Graphics.DrawLine(p, pointCenter.X - 1, pointCenter.Y - 2, pointCenter.X - 1, pointCenter.Y + 2);
                        }
                    }
                    break;
                case ScrollBarButtonStyle.eScrollButton:
                    if (pScrollBarButton.eOrientation == Orientation.Vertical)
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            p.Width = 1.5f;
                            e.Graphics.DrawLine(p, pointCenter.X - 3, pointCenter.Y - 3, pointCenter.X + 3, pointCenter.Y - 3);
                            e.Graphics.DrawLine(p, pointCenter.X - 3, pointCenter.Y, pointCenter.X + 3, pointCenter.Y);
                            e.Graphics.DrawLine(p, pointCenter.X - 3, pointCenter.Y + 3, pointCenter.X + 3, pointCenter.Y + 3);
                        }
                    }
                    else
                    {
                        Point pointCenter = new Point((rectangle.Left + rectangle.Right) / 2 + 1, (rectangle.Top + rectangle.Bottom) / 2);
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            p.Width = 1.5f;
                            e.Graphics.DrawLine(p, pointCenter.X - 3, pointCenter.Y - 3, pointCenter.X - 3, pointCenter.Y + 3);
                            e.Graphics.DrawLine(p, pointCenter.X, pointCenter.Y - 3, pointCenter.X, pointCenter.Y + 3);
                            e.Graphics.DrawLine(p, pointCenter.X + 3, pointCenter.Y - 3, pointCenter.X + 3, pointCenter.Y + 3);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ButtonGroup
        public override void OnRenderButtonGroup(ObjectRenderEventArgs e)
        {
            WFNew.IButtonGroupItem pButtonGroupItem = e.Object as WFNew.IButtonGroupItem;
            //
            if (pButtonGroupItem == null) return;
            //if (pButtonGroupItem == null || 
            //    pButtonGroupItem.eButtonGroupStyle == GISShare.Controls.WinForm.WFNew.ButtonGroupStyle.eButtonList) return;
            //
            //switch (pButtonGroupItem.eBaseItemState)
            //{
            //    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
            //        this.DrawButtonGroupSelected(e.Graphics, pButtonGroupItem, e.Bounds);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
            //        this.DrawButtonGroupPressed(e.Graphics, pButtonGroupItem, e.Bounds);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
            //        this.DrawButtonGroupDisabled(e.Graphics, pButtonGroupItem, e.Bounds);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
            //    default:
            //        if (pButtonGroupItem.Checked) { this.DrawButtonGroupChecked(e.Graphics, pButtonGroupItem, e.Bounds); }
            //        else { this.DrawButtonGroupNomal(e.Graphics, pButtonGroupItem, e.Bounds); }
            //        break;
            //}
            this.DrawButtonGroupNomal(e.Graphics, pButtonGroupItem, e.Bounds);
            this.DrawButtonGroupSeparator(e.Graphics, pButtonGroupItem);
        }
        private void DrawButtonGroupNomal(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem, Rectangle rectangle)
        {
            if (!pButtonGroupItem.ShowNomalState) return;
            //
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1);
            //
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(innerR, this.WFNewColorTable.ButtonGroupNomalBegin, this.WFNewColorTable.ButtonGroupNomalEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (glossyR.Width > 0 && glossyR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, 0, 0, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyR, this.WFNewColorTable.ButtonGroupNomalGlossyBegin, this.WFNewColorTable.ButtonGroupNomalGlossyEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupNomalBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            if (innerR.Width > 0 && innerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupNomalBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawButtonGroupChecked(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1);
            //
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(innerR, this.WFNewColorTable.ButtonGroupCheckedBegin, this.WFNewColorTable.ButtonGroupCheckedEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (glossyR.Width > 0 && glossyR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, 0, 0, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyR, this.WFNewColorTable.ButtonGroupCheckedGlossyBegin, this.WFNewColorTable.ButtonGroupCheckedGlossyEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupCheckedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            if (innerR.Width > 0 && innerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupCheckedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawButtonGroupDisabled(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1);
            //
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(innerR, this.WFNewColorTable.ButtonGroupDisabledBegin, this.WFNewColorTable.ButtonGroupDisabledEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (glossyR.Width > 0 && glossyR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, 0, 0, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyR, this.WFNewColorTable.ButtonGroupDisabledGlossyBegin, this.WFNewColorTable.ButtonGroupDisabledGlossyEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupDisabledBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            if (innerR.Width > 0 && innerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupDisabledBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawButtonGroupPressed(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1);
            //
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(innerR, this.WFNewColorTable.ButtonGroupPressedBegin, this.WFNewColorTable.ButtonGroupPressedEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (glossyR.Width > 0 && glossyR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, 0, 0, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyR, this.WFNewColorTable.ButtonGroupPressedGlossyBegin, this.WFNewColorTable.ButtonGroupPressedGlossyEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupPressedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            if (innerR.Width > 0 && innerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupPressedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawButtonGroupSelected(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem, Rectangle rectangle)
        {
            Rectangle outerR = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle innerR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1);
            Rectangle glossyR = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1);
            //
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(innerR, this.WFNewColorTable.ButtonGroupSelectedBegin, this.WFNewColorTable.ButtonGroupSelectedEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (glossyR.Width > 0 && glossyR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(glossyR, 0, 0, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(glossyR, this.WFNewColorTable.ButtonGroupSelectedGlossyBegin, this.WFNewColorTable.ButtonGroupSelectedGlossyEnd, 90))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
            if (outerR.Width > 0 && outerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupSelectedBorderOut))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
            if (innerR.Width > 0 && innerR.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(innerR, pButtonGroupItem.LeftTopRadius, pButtonGroupItem.RightTopRadius, pButtonGroupItem.LeftBottomRadius, pButtonGroupItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupSelectedBorderIn))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        private void DrawButtonGroupSeparator(Graphics g, WFNew.IButtonGroupItem pButtonGroupItem)
        {
            switch (pButtonGroupItem.eOrientation)
            {
                case Orientation.Horizontal:
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
                    {
                        Rectangle rectangle;
                        for (int i = 0; i < pButtonGroupItem.BaseItems.Count - 1; i++)
                        {
                            rectangle = pButtonGroupItem.BaseItems[i].DisplayRectangle;
                            g.DrawLine(p, rectangle.Right - 1, rectangle.Top + 2, rectangle.Right - 1, rectangle.Bottom - 3);
                        }
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
                    {
                        Rectangle rectangle;
                        for (int i = 1; i < pButtonGroupItem.BaseItems.Count; i++)
                        {
                            rectangle = pButtonGroupItem.BaseItems[i].DisplayRectangle;
                            g.DrawLine(p, rectangle.Left, rectangle.Top + 2, rectangle.Left, rectangle.Bottom - 3);
                        }
                    }
                    break;
                case Orientation.Vertical:
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
                    {
                        Rectangle rectangle;
                        for (int i = 0; i < pButtonGroupItem.BaseItems.Count - 1; i++)
                        {
                            rectangle = pButtonGroupItem.BaseItems[i].DisplayRectangle;
                            g.DrawLine(p, rectangle.Left + 2, rectangle.Bottom - 1, rectangle.Right - 3, rectangle.Bottom - 1);
                        }
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
                    {
                        Rectangle rectangle;
                        for (int i = 1; i < pButtonGroupItem.BaseItems.Count; i++)
                        {
                            rectangle = pButtonGroupItem.BaseItems[i].DisplayRectangle;
                            g.DrawLine(p, rectangle.Left + 2, rectangle.Top, rectangle.Right - 3, rectangle.Top);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Separator
        public override void OnRenderSeparator(ObjectRenderEventArgs e)
        {
            WFNew.ISeparatorItem pSeparator = e.Object as WFNew.ISeparatorItem;
            if (pSeparator == null) return;
            //
            using (Pen p = new Pen(this.WFNewColorTable.SeparatorDark))
            {
                e.Graphics.DrawLine(p, pSeparator.StartPointDarkLine, pSeparator.EndPointDarkLine);
            }
            using (Pen p = new Pen(this.WFNewColorTable.SeparatorLight))
            {
                e.Graphics.DrawLine(p, pSeparator.StartPointLightLine, pSeparator.EndPointLightLine);
            }
        }
        #endregion

        #region RibbonGallery
        public override void OnRenderRibbonGallery(ObjectRenderEventArgs e)
        {
            WFNew.IGalleryItem pRibbonGallery = e.Object as WFNew.IGalleryItem;
            if (pRibbonGallery == null) return;
            switch (pRibbonGallery.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawRibbonGallerySelected(e.Graphics, pRibbonGallery, e.Bounds);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawRibbonGalleryPressed(e.Graphics, pRibbonGallery, e.Bounds);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawRibbonGalleryDisabled(e.Graphics, pRibbonGallery, e.Bounds);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (pRibbonGallery.Checked) { this.DrawRibbonGalleryChecked(e.Graphics, pRibbonGallery, e.Bounds); }
                    else { this.DrawRibbonGalleryNomal(e.Graphics, pRibbonGallery, e.Bounds); }
                    break;
            }
        }
        private void DrawRibbonGalleryNomal(Graphics g, WFNew.IGalleryItem pRibbonGallery, Rectangle rectangle)
        {
            int iLeftTopRadius = pRibbonGallery.LeftTopRadius;
            int iRightTopRadius = pRibbonGallery.RightTopRadius;
            int iLeftBottomRadius = pRibbonGallery.LeftBottomRadius;
            int iRightBottomRadius = pRibbonGallery.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonGallery.DrawRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.GalleryNomalBackground))
                {
                    g.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.GalleryNomalBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonGalleryChecked(Graphics g, WFNew.IGalleryItem pRibbonGallery, Rectangle rectangle)
        {
            int iLeftTopRadius = pRibbonGallery.LeftTopRadius;
            int iRightTopRadius = pRibbonGallery.RightTopRadius;
            int iLeftBottomRadius = pRibbonGallery.LeftBottomRadius;
            int iRightBottomRadius = pRibbonGallery.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonGallery.DrawRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.GalleryCheckedBackground))
                {
                    g.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.GalleryCheckedBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonGalleryDisabled(Graphics g, WFNew.IGalleryItem pRibbonGallery, Rectangle rectangle)
        {
            int iLeftTopRadius = pRibbonGallery.LeftTopRadius;
            int iRightTopRadius = pRibbonGallery.RightTopRadius;
            int iLeftBottomRadius = pRibbonGallery.LeftBottomRadius;
            int iRightBottomRadius = pRibbonGallery.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonGallery.DrawRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.GalleryDisabledBackground))
                {
                    g.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.GalleryDisabledBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonGalleryPressed(Graphics g, WFNew.IGalleryItem pRibbonGallery, Rectangle rectangle)
        {
            int iLeftTopRadius = pRibbonGallery.LeftTopRadius;
            int iRightTopRadius = pRibbonGallery.RightTopRadius;
            int iLeftBottomRadius = pRibbonGallery.LeftBottomRadius;
            int iRightBottomRadius = pRibbonGallery.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonGallery.DrawRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.GalleryPressedBackground))
                {
                    g.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.GalleryPressedBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawRibbonGallerySelected(Graphics g, WFNew.IGalleryItem pRibbonGallery, Rectangle rectangle)
        {
            int iLeftTopRadius = pRibbonGallery.LeftTopRadius;
            int iRightTopRadius = pRibbonGallery.RightTopRadius;
            int iLeftBottomRadius = pRibbonGallery.LeftBottomRadius;
            int iRightBottomRadius = pRibbonGallery.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pRibbonGallery.DrawRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.GallerySelectedBackground))
                {
                    g.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.GallerySelectedBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region RibbonGalleryPopupPanel
        public override void OnRenderRibbonGalleryPopupPanel(ObjectRenderEventArgs e)
        {
            WFNew.IGalleryMirrorPopupPanelItem pIGalleryMirrorPopupPanelItem = e.Object as WFNew.IGalleryMirrorPopupPanelItem;
            //
            if (pIGalleryMirrorPopupPanelItem == null) return;
            int iLeftTopRadius = pIGalleryMirrorPopupPanelItem.LeftTopRadius;
            int iRightTopRadius = pIGalleryMirrorPopupPanelItem.RightTopRadius;
            int iLeftBottomRadius = pIGalleryMirrorPopupPanelItem.LeftBottomRadius;
            int iRightBottomRadius = pIGalleryMirrorPopupPanelItem.RightBottomRadius;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pIGalleryMirrorPopupPanelItem.FrameRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.RibbonGalleryPopupPanelBackground))
                {
                    e.Graphics.FillPath(brush, path);
                }
                using (Pen p = new Pen(this.WFNewColorTable.RibbonGalleryPopupPanelBorber))
                {
                    e.Graphics.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region GalleryScrollButton RibbonGallery ScrollUpButton ScrollDownButton ScrollDropDownButton
        public override void OnRenderGalleryScrollButton(ObjectRenderEventArgs e)
        {
            this.OnRenderBaseButton(e);
            //
            WFNew.IGalleryScrollButton pGalleryScrollButton = e.Object as WFNew.IGalleryScrollButton;
            if (pGalleryScrollButton == null) return;
            //
            Rectangle rectangle = e.Bounds;
            Point[] points = new Point[3];
            switch (pGalleryScrollButton.eGalleryScrollButtonStyle)
            {
                case GISShare.Controls.WinForm.WFNew.GalleryScrollButtonStyle.eScrollUpButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 4) / 2, (rectangle.Top + rectangle.Bottom - 4) / 2, 4, 4);
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pGalleryScrollButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top - 1);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom - 1);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom - 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.GalleryScrollButtonStyle.eScrollDownButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 3) / 2, (rectangle.Top + rectangle.Bottom - 3) / 2, 3, 3);
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pGalleryScrollButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom + 1);
                        points[2] = new Point(rectangle.Left, rectangle.Top + 1);
                        points[1] = new Point(rectangle.Right, rectangle.Top + 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.GalleryScrollButtonStyle.eScrollDropDownButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 3) / 2, (rectangle.Top + rectangle.Bottom - 3) / 2, 3, 3);
                    rectangle.X -= 1;
                    rectangle.Y += 2;
                    rectangle.Width += 2;
                    if (pGalleryScrollButton.Enabled)
                    {
                        e.Graphics.DrawLine(new Pen(this.WFNewColorTable.Arrow), rectangle.Left - 1, rectangle.Top - 3, rectangle.Right, rectangle.Top - 3);
                        e.Graphics.DrawLine(new Pen(this.WFNewColorTable.ArrowLight), rectangle.Left - 1, rectangle.Top - 2, rectangle.Right, rectangle.Top - 2);
                        //
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom + 1);
                        points[2] = new Point(rectangle.Left, rectangle.Top + 1);
                        points[1] = new Point(rectangle.Right, rectangle.Top + 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        e.Graphics.DrawLine(new Pen(this.WFNewColorTable.ArrowDisabled), rectangle.Left - 1, rectangle.Top - 3, rectangle.Right, rectangle.Top - 3);
                        //
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
            }
        }
        #endregion

        #region TextBox
        public override void OnRenderTextBox(ObjectRenderEventArgs e)
        {
            WFNew.ITextBoxItem pTextBox = e.Object as WFNew.ITextBoxItem;
            if (pTextBox == null) return;
            //
            switch (pTextBox.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxSelectedBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    //using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxSelectedText))
                    //{
                    //    e.Graphics.DrawString(pTextBox.Text, pTextBox.Font, brush, pTextBox.TextRectangle);
                    //}
                    switch (pTextBox.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxSelectedBorder))
                            {
                                e.Graphics.DrawRectangle(p, pTextBox.FrameRectangle);
                            }
                            break;
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxDisabledBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    //using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxDisabledText))
                    //{
                    //    e.Graphics.DrawString(pTextBox.Text, pTextBox.Font, brush, pTextBox.TextRectangle);
                    //}
                    switch (pTextBox.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxDisabledBorder))
                            {
                                e.Graphics.DrawRectangle(p, pTextBox.FrameRectangle);
                            }
                            break;
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxomalBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    //using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxomalText))
                    //{
                    //    e.Graphics.DrawString(pTextBox.Text, pTextBox.Font, brush, pTextBox.TextRectangle);
                    //}
                    switch (pTextBox.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxomalBorder))
                            {
                                e.Graphics.DrawRectangle(p, pTextBox.FrameRectangle);
                            }
                            break;
                    }
                    break;
            }
        }
        #endregion
        
        #region ImageAreaBox
        public override void OnRenderImageAreaBox(ObjectRenderEventArgs e)
        {
            WFNew.IImageBoxItem pImageBoxItem = e.Object as WFNew.IImageBoxItem;
            if (pImageBoxItem == null) return;
            //

        }
        #endregion

        public override void OnRenderUpDownButton(ObjectRenderEventArgs e)
        {
            this.OnRenderBaseButton(e);
            //
            WFNew.IUpDownButton pUpDownButton = e.Object as WFNew.IUpDownButton;
            if (pUpDownButton == null) return;
            //
            Rectangle rectangle = e.Bounds;
            Point[] points = new Point[3];
            switch (pUpDownButton.eUpDownButtonStyle)
            {
                case UpDownButtonStyle.eUpButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 4) / 2, (rectangle.Top + rectangle.Bottom - 4) / 2, 4, 4);
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pUpDownButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top - 1);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom - 1);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom - 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case UpDownButtonStyle.eDownButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 3) / 2, (rectangle.Top + rectangle.Bottom - 3) / 2, 3, 3);
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pUpDownButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom + 1);
                        points[2] = new Point(rectangle.Left, rectangle.Top + 1);
                        points[1] = new Point(rectangle.Right, rectangle.Top + 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                default:
                    break;
            }
        }

        #region CustomizeComboBox
        public override void OnRenderCustomizeComboBox(ObjectRenderEventArgs e)
        {
            WFNew.ICustomizeComboBoxItem pCustomizeComboBoxItem = e.Object as WFNew.ICustomizeComboBoxItem;
            if (pCustomizeComboBoxItem == null) return;
            //
            switch (pCustomizeComboBoxItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxSelectedBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    switch (pCustomizeComboBoxItem.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxSelectedBorder))
                            {
                                e.Graphics.DrawRectangle(p, pCustomizeComboBoxItem.FrameRectangle);
                            }
                            break;
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxDisabledBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    switch (pCustomizeComboBoxItem.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxDisabledBorder))
                            {
                                e.Graphics.DrawRectangle(p, pCustomizeComboBoxItem.FrameRectangle);
                            }
                            break;
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.TextBoxomalBackground))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                    switch (pCustomizeComboBoxItem.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            break;
                        case BorderStyle.eSingle:
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.TextBoxomalBorder))
                            {
                                e.Graphics.DrawRectangle(p, pCustomizeComboBoxItem.FrameRectangle);
                            }
                            break;
                    }
                    break;
            }
            //
            switch (pCustomizeComboBoxItem.eSplitState)
            {
                case BaseItemState.eHot:
                    this.DrawRenderCustomizeComboBoxSpiltHot(e.Graphics, pCustomizeComboBoxItem.SplitRectangle);
                    break;
                case BaseItemState.ePressed:
                    this.DrawRenderCustomizeComboBoxSpiltPressed(e.Graphics, pCustomizeComboBoxItem.SplitRectangle);
                    break;
                case BaseItemState.eNormal:
                default:
                    break;
            }
        }
        private void DrawRenderCustomizeComboBoxSpiltHot(Graphics g, Rectangle rectangle)
        {
            Rectangle intRectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 3);
            //
            if (intRectangle.Width <= 0 || intRectangle.Height <= 0) return;
            //
            using (LinearGradientBrush b = new LinearGradientBrush(intRectangle, this.WFNewColorTable.ButtonSelectedCenter, this.WFNewColorTable.ButtonSelectedOut, LinearGradientMode.Vertical))
            {
                g.FillRectangle(b, intRectangle);
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedBorderIn))
            {
                g.DrawRectangle(p, intRectangle);
            }
        }
        private void DrawRenderCustomizeComboBoxSpiltPressed(Graphics g, Rectangle rectangle)
        {
            Rectangle intRectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 3);
            //
            if (intRectangle.Width <= 0 || intRectangle.Height <= 0) return;
            //
            using (LinearGradientBrush b = new LinearGradientBrush(intRectangle, this.WFNewColorTable.ButtonPressedCenter, this.WFNewColorTable.ButtonPressedOut, LinearGradientMode.Vertical))
            {
                g.FillRectangle(b, intRectangle);
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.ButtonPressedBorderIn))
            {
                g.DrawRectangle(p, intRectangle);
            }
        }
        #endregion

        #region CustomizePopup
        public override void OnRenderCustomizePopup(ObjectRenderEventArgs e)
        {
            WFNew.ICustomizePopup pCustomizePopup = e.Object as WFNew.ICustomizePopup;
            if (pCustomizePopup == null) return;
            //
            Rectangle rectangle = pCustomizePopup.ModifySizeRegionRectangle;
            switch (pCustomizePopup.eModifySizeStyle)
            {
                case ModifySizeStyle.eHorizontal:
                    if (rectangle.Width > 0 && rectangle.Height > 0)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.PopupPanelCheckGripBegin, this.WFNewColorTable.PopupPanelCheckGripEnd, LinearGradientMode.Horizontal))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //
                        rectangle = new Rectangle(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2, 2, 2);
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorLight))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(0, -6);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(0, 12);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorDark))
                        {
                            rectangle.Offset(-1, -13);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(0, 6);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(0, 6);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    break;
                case ModifySizeStyle.eVertical:
                    if (rectangle.Width > 0 && rectangle.Height > 0)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.PopupPanelCheckGripBegin, this.WFNewColorTable.PopupPanelCheckGripEnd, LinearGradientMode.Vertical))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //
                        rectangle = new Rectangle(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2, 2, 2);
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorLight))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-6, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(12, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorDark))
                        {
                            rectangle.Offset(-13, -1);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(6, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(6, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    break;
                case ModifySizeStyle.eAll:
                    if (rectangle.Width > 0 && rectangle.Height > 0)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.PopupPanelCheckGripBegin, this.WFNewColorTable.PopupPanelCheckGripEnd, LinearGradientMode.Vertical))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //
                        rectangle = new Rectangle(rectangle.Right - 4, rectangle.Bottom - 4, 2, 2);
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorLight))//
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(5, -5);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 5);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(10, -10);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.SeparatorDark))
                        {
                            rectangle.Offset(-1, 9);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(5, -5);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 0);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(-5, 5);
                            e.Graphics.FillRectangle(b, rectangle);
                            rectangle.Offset(10, -10);
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    break;
            }
            //
            if (pCustomizePopup.UseRadius)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pCustomizePopup.FrameRectangle, pCustomizePopup.LeftTopRadius, pCustomizePopup.RightTopRadius, pCustomizePopup.LeftBottomRadius, pCustomizePopup.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.PopupPanelBorder))//
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
            else
            {
                using (Pen p = new Pen(this.WFNewColorTable.PopupPanelBorder))//
                {
                    e.Graphics.DrawRectangle(p, pCustomizePopup.FrameRectangle);
                }
            }
        }
        #endregion

        //#region BaseItemStack
        //public override void OnRenderBaseItemStack(ObjectRenderEventArgs e)
        //{

        //}
        //#endregion

        #region BaseItemStackEx
        public override void OnRenderBaseItemStackEx(ObjectRenderEventArgs e)
        {
            WFNew.IBaseItemStackExItem pBaseItemStackEx = e.Object as WFNew.IBaseItemStackExItem;
            if (pBaseItemStackEx == null) return;
            if (!pBaseItemStackEx.ShowBackground && !pBaseItemStackEx.ShowOutLine) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pBaseItemStackEx.FrameRectangle,
                    pBaseItemStackEx.LeftTopRadius, pBaseItemStackEx.RightTopRadius, pBaseItemStackEx.LeftBottomRadius, pBaseItemStackEx.RightBottomRadius))
            {
                if (pBaseItemStackEx.ShowBackground)
                {
                    using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.BaseItemStackExNomalBackground))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
                //
                if (pBaseItemStackEx.ShowOutLine)
                {
                    using (Pen p = new Pen(this.WFNewColorTable.BaseItemStackExNomalBorder))
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
        }
        #endregion

        #region LTBRButton BaseItemStackEx Top Bottom Left Right Button
        public override void OnRenderLTBRButton(ObjectRenderEventArgs e)
        {
            this.OnRenderBaseButton(e);
            //
            WFNew.ILTBRButton pLTBRButton = e.Object as WFNew.ILTBRButton;
            if (pLTBRButton == null) return;
            //
            Rectangle rectangle = e.Bounds;
            Point[] points = new Point[3];
            switch (pLTBRButton.eLTBRButtonStyle)
            {
                case GISShare.Controls.WinForm.WFNew.LTBRButtonStyle.eTopButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 4) / 2, (rectangle.Top + rectangle.Bottom - 4) / 2, 4, 4);
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pLTBRButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top - 1);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom - 1);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom - 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.LTBRButtonStyle.eBottomButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 3) / 2, (rectangle.Top + rectangle.Bottom - 3) / 2, 3, 3);
                    //rectangle.X -= 1;
                    rectangle.Width += 2;
                    if (pLTBRButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom + 1);
                        points[2] = new Point(rectangle.Left, rectangle.Top + 1);
                        points[1] = new Point(rectangle.Right, rectangle.Top + 1);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.LTBRButtonStyle.eLeftButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 4) / 2, (rectangle.Top + rectangle.Bottom - 4) / 2, 4, 4);
                    rectangle.X += 1;
                    rectangle.Y -= 1;
                    rectangle.Width -= 1;
                    rectangle.Height += 2;
                    if (pLTBRButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Left - 1, rectangle.Top + rectangle.Height / 2);
                        points[1] = new Point(rectangle.Right - 1, rectangle.Bottom);
                        points[2] = new Point(rectangle.Right - 1, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                        points[1] = new Point(rectangle.Right, rectangle.Bottom);
                        points[2] = new Point(rectangle.Right, rectangle.Top);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.LTBRButtonStyle.eRightButton:
                    rectangle = new Rectangle((rectangle.Left + rectangle.Right - 3) / 2, (rectangle.Top + rectangle.Bottom - 3) / 2, 3, 3);
                    rectangle.Y -= 2;
                    //rectangle.Width -= 1;
                    rectangle.Height += 3;
                    if (pLTBRButton.Enabled)
                    {
                        points[0] = new Point(rectangle.Right + 1, rectangle.Top + rectangle.Height / 2);
                        points[2] = new Point(rectangle.Left + 1, rectangle.Top);
                        points[1] = new Point(rectangle.Left + 1, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowLight), points);
                        points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), points);
                    }
                    else
                    {
                        points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                        points[2] = new Point(rectangle.Left, rectangle.Top);
                        points[1] = new Point(rectangle.Left, rectangle.Bottom);
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.ArrowDisabled), points);
                    }
                    break;
            }
        }
        #endregion

        #region ContextPopupPanel
        public override void OnRenderContextPopupPanel(ObjectRenderEventArgs e)
        {
            WFNew.IContextPopupPanelItem pContextPopupPanelItem = e.Object as WFNew.IContextPopupPanelItem;
            if (pContextPopupPanelItem == null) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pContextPopupPanelItem.EntityRectangle, 0, pContextPopupPanelItem.RightTopRadius, 0, pContextPopupPanelItem.RightBottomRadius))
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.PopupPanelBackground))
                {
                    e.Graphics.FillPath(b, path);
                }
            }
            //
            Rectangle rectangle = pContextPopupPanelItem.ImageRectangle;
            switch (pContextPopupPanelItem.eContextPopupStyle)
            {
                case GISShare.Controls.WinForm.WFNew.ContextPopupStyle.eNormal:
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pContextPopupPanelItem.LeftTopRadius, 0, pContextPopupPanelItem.LeftBottomRadius, 0))
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.PopupPanelImageGripBackground))
                        {
                            e.Graphics.FillPath(b, path);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.PopupPanelImageGripSeparator))
                        {
                            e.Graphics.DrawLine(p, new Point(rectangle.Right - 1, rectangle.Top), new Point(rectangle.Right - 1, rectangle.Bottom));
                        }
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.ContextPopupStyle.eSuper:
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pContextPopupPanelItem.CheckRectangle, pContextPopupPanelItem.LeftTopRadius, 0, pContextPopupPanelItem.LeftBottomRadius, 0))
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(pContextPopupPanelItem.CheckRectangle, this.WFNewColorTable.PopupPanelCheckGripBegin, this.WFNewColorTable.PopupPanelCheckGripEnd, LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillPath(b, path);
                        }
                        using (SolidBrush b2 = new SolidBrush(this.WFNewColorTable.PopupPanelImageGripBackground))
                        {
                            e.Graphics.FillRectangle(b2, pContextPopupPanelItem.ImageRectangle);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.PopupPanelImageGripSeparator))
                        {
                            e.Graphics.DrawLine(p, new Point(rectangle.Right - 1, rectangle.Top), new Point(rectangle.Right - 1, rectangle.Bottom));
                        }
                    }
                    break;
            }
            //
            if (pContextPopupPanelItem.ShowOutLine)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pContextPopupPanelItem.FrameRectangle, pContextPopupPanelItem.LeftTopRadius, pContextPopupPanelItem.RightTopRadius, pContextPopupPanelItem.LeftBottomRadius, pContextPopupPanelItem.RightBottomRadius))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.PopupPanelBorder))//
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
        }
        #endregion

        public override void OnRenderToolTipPopupPanel(ObjectRenderEventArgs e)
        {
            WFNew.IToolTipPopupPanelItem pToolTipPopupPanelItem = e.Object as WFNew.IToolTipPopupPanelItem;
            if (pToolTipPopupPanelItem == null) return;
            //
            ////Rectangle rectangle = pToolTipPopupPanelItem.DisplayRectangle;
            ////rectangle.Inflate(-1, -1);
            ////if (rectangle.Width > 0 && rectangle.Height > 0)
            ////{
            ////    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ToolTipPopupPanelBegin, this.WFNewColorTable.ToolTipPopupPanelEnd, LinearGradientMode.Vertical))
            ////    {
            ////        e.Graphics.FillRectangle(b, rectangle);
            ////    }
            ////}
            Rectangle rectangle = pToolTipPopupPanelItem.FrameRectangle;
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle,
                        pToolTipPopupPanelItem.LeftTopRadius, pToolTipPopupPanelItem.RightTopRadius, pToolTipPopupPanelItem.LeftBottomRadius, pToolTipPopupPanelItem.RightBottomRadius))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ToolTipPopupPanelBegin, this.WFNewColorTable.ToolTipPopupPanelEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillPath(b, path);
                    }
                    //
                    using (Pen p = new Pen(this.WFNewColorTable.PopupPanelBorder))
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
        }

        #region RibbonText
        public override void OnRenderRibbonText(TextRenderEventArgs e)
        {
            if (e.Text == null) return;
            //
            string strText = e.Text.Length > 1024 ? e.Text.Substring(0, 1024) : e.Text;
            //
            int iOffset = 4;
            if (e.StringFormat.FormatFlags == StringFormatFlags.DirectionVertical)
            {
                iOffset = 0;
            }
            else
            {
                if (System.Text.Encoding.Default.GetByteCount(strText) == strText.Length) { iOffset = 2; }
            }
            //
            Rectangle cbr = e.TextBounds;
            if (e.Enabled)
            {
                cbr.Y += iOffset;
                if (e.HaveShadow)
                {
                    using (SolidBrush b = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.ItemTextLight))
                    {
                        e.Graphics.DrawString(strText, e.Font, b, cbr, e.StringFormat);//stringFormat
                    }
                }
                cbr.Y -= 1;
                using (SolidBrush b = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.ItemText))
                {
                    e.Graphics.DrawString(strText, e.Font, b, cbr, e.StringFormat);//stringFormat
                }
            }
            else
            {
                cbr.Y += --iOffset;
                using (SolidBrush b = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.ItemTextDisabled))
                {
                    e.Graphics.DrawString(strText, e.Font, b, cbr, e.StringFormat);//stringFormat
                }
            }
        }

        public override void OnRenderLinkLabelText(TextRenderEventArgs e)
        {
            WFNew.ILinkLabelItem pLinkLabelItem = e.Object as WFNew.ILinkLabelItem;
            if (pLinkLabelItem == null) return;
            //
            Color color = this.WFNewColorTable.LinkLabelNomal;
            if (pLinkLabelItem.LinkVisited)
            {
                color = this.WFNewColorTable.LinkLabelVisited;
            }
            else
            {
                switch (pLinkLabelItem.eBaseItemState)
                {
                    case BaseItemState.eHot:
                        color = this.WFNewColorTable.LinkLabelSelected;
                        break;
                    case BaseItemState.ePressed:
                        color = this.WFNewColorTable.LinkLabelPressed;
                        break;
                    case BaseItemState.eDisabled:
                        color = this.WFNewColorTable.LinkLabelDisabled;
                        break;
                    case BaseItemState.eNormal:
                    default:
                        break;
                }
            }
            //
            bool haveUnderline = true;
            switch (pLinkLabelItem.LinkBehavior) 
            {
                case LinkBehavior.SystemDefault:
                case LinkBehavior.AlwaysUnderline:
                    haveUnderline = true;
                    break;
                case LinkBehavior.HoverUnderline:
                    haveUnderline = (pLinkLabelItem.eBaseItemState == BaseItemState.eHot || pLinkLabelItem.eBaseItemState == BaseItemState.ePressed);
                    break;
                case LinkBehavior.NeverUnderline: 
                    haveUnderline = false;
                    break;
            }
            //
            using (SolidBrush brush = new SolidBrush(e.ForeCustomize ? e.ForeColor : color))
            {
                e.Graphics.DrawString(e.Text, e.ForeCustomize ? e.Font : (haveUnderline ? new Font(e.Font, FontStyle.Underline) : e.Font), brush, e.TextBounds);
            }
        }

        public override void OnRenderTextBoxText(TextRenderEventArgs e)
        {
            WFNew.ITextBoxItem pTextBoxItem = e.Object as WFNew.ITextBoxItem;
            if (pTextBoxItem == null) return;
            //
            switch (pTextBoxItem.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (SolidBrush brush = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.TextBoxSelectedText))
                    {
                        e.Graphics.DrawString(e.Text, e.Font, brush, e.TextBounds);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    using (SolidBrush brush = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.TextBoxDisabledText))
                    {
                        e.Graphics.DrawString(e.Text, e.Font, brush, e.TextBounds);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    using (SolidBrush brush = new SolidBrush(e.ForeCustomize ? e.ForeColor : this.WFNewColorTable.TextBoxomalText))
                    {
                        e.Graphics.DrawString(e.Text, e.Font, brush, e.TextBounds);
                    }
                    break;
            }
        }
        #endregion

        #region RibbonImage
        public override void OnRenderRibbonImage(ImageRenderEventArgs e)
        {
            if (e.Image == null) return;
            //
            if (e.Object == null)
            {
                e.Graphics.DrawImage(e.Image, e.ImageBounds); return;
            }
            //
            if (e.Enabled) { e.Graphics.DrawImage(e.Image, e.ImageBounds); }
            else { e.Graphics.DrawImage(GISShare.Controls.WinForm.Util.UtilTX.CreateDisabledImage(e.Image), e.ImageBounds); }
        }
        public override void OnRenderRibbonImage(ImageRenderEventArgsF e)
        {
            if (e.Image == null) return;
            //
            if (e.Object == null)
            {
                e.Graphics.DrawImage(e.Image, e.ImageBounds); return;
            }
            //
            if (e.Enabled) { e.Graphics.DrawImage(e.Image, e.ImageBounds); }
            else { e.Graphics.DrawImage(GISShare.Controls.WinForm.Util.UtilTX.CreateDisabledImage(e.Image), e.ImageBounds); }
        }
        #endregion

        #region RibbonArrow
        public override void OnRenderRibbonArrow(ArrowRenderEventArgs e)
        {
            //Color arrowColor = e.ArrowColor;
            //if (e.Enabled) { arrowColor = this.WFNewColorTable.Arrow; }
            //else { arrowColor = this.WFNewColorTable.ArrowDisabled; }
            //this.DrawArrow(e.Graphics, e.ArrowBounds, arrowColor, e.eArrowStyle);

            this.DrawArrow(e.Graphics, e.ArrowBounds, e.Enabled ? this.WFNewColorTable.Arrow : this.WFNewColorTable.ArrowDisabled, e.eArrowStyle);

            //Rectangle rectangle = e.ArrowBounds;
            //Point[] points = null;
            //switch (e.eArrowStyle)
            //{
            //    case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToUp:
            //        rectangle.X -= 1;
            //        rectangle.Width += 2;
            //        points = new Point[3];
            //        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
            //        points[1] = new Point(rectangle.Right, rectangle.Bottom);
            //        points[2] = new Point(rectangle.Left, rectangle.Bottom);
            //        e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToDown:
            //        rectangle.X -= 1;
            //        rectangle.Width += 2;
            //        points = new Point[3];
            //        points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
            //        points[2] = new Point(rectangle.Left, rectangle.Top);
            //        points[1] = new Point(rectangle.Right, rectangle.Top);
            //        e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToLeft:
            //        rectangle.Y -= 1;
            //        rectangle.Height += 2;
            //        points = new Point[3];
            //        points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
            //        points[1] = new Point(rectangle.Right, rectangle.Top);
            //        points[2] = new Point(rectangle.Right, rectangle.Bottom);
            //        e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
            //        break;
            //    case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToRight:
            //        rectangle.Y -= 1;
            //        rectangle.Height += 2;
            //        points = new Point[3];
            //        points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
            //        points[2] = new Point(rectangle.Left, rectangle.Bottom);
            //        points[1] = new Point(rectangle.Left, rectangle.Top);
            //        e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
            //        break;
            //    default:
            //        break;
            //}
        }
        private void DrawArrow(System.Drawing.Graphics graphics, Rectangle rectangle, Color arrowColor, GISShare.Controls.WinForm.WFNew.ArrowStyle eArrowStyle)
        {
            Point[] points = null;
            switch (eArrowStyle)
            {
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToUp:
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                    points[1] = new Point(rectangle.Right, rectangle.Bottom);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToDown:
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                    points[2] = new Point(rectangle.Left, rectangle.Top);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToLeft:
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    points[2] = new Point(rectangle.Right, rectangle.Bottom);
                    graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToRight:
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    points[1] = new Point(rectangle.Left, rectangle.Top);
                    graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region RibbonCheck
        public override void OnRenderContextPopupItemButtonChecked(CheckedRenderEventArgs e)
        {
            if (e.CheckState != CheckState.Checked) return;
            //
            Rectangle rectangle = new Rectangle(e.CheckBounds.X + 1, e.CheckBounds.Y + 1, e.CheckBounds.Width - 3, e.CheckBounds.Height - 3);
            if (e.Enabled)
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.PopupItemButtonCheckedBackgound))
                {
                    e.Graphics.FillRectangle(brush, rectangle);
                }
                using (Pen p = new Pen(this.WFNewColorTable.PopupItemButtonCheckedBorder))
                {
                    e.Graphics.DrawRectangle(p, rectangle);
                }
                using (GraphicsPath path = this.CreateCheckPath(rectangle))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        p.Width = 2;
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(this.WFNewColorTable.PopupItemButtonCheckedDisabledBorder))
                {
                    e.Graphics.FillRectangle(brush, rectangle);
                }
                using (Pen p = new Pen(this.WFNewColorTable.PopupItemButtonCheckedDisabledBorder))
                {
                    e.Graphics.DrawRectangle(p, rectangle);
                }
                using (GraphicsPath path = this.CreateCheckPath(rectangle))
                {
                    using (Pen p = new Pen(this.WFNewColorTable.ArrowDisabled))
                    {
                        p.Width = 2;
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
        }
        #endregion

        //用来优化ButtonGroupItem里的 ButtonItem 圆角信息
        private void GetButtonItemRadiusInfo(WFNew.IButtonGroupItem pButtonGroupItem, 
            WFNew.BaseItem baseItem, ref int iLeftTopRadius, ref int iRightTopRadius, ref int iLeftBottomRadius, ref int iRightBottomRadius)
        {
            if (pButtonGroupItem == null || baseItem == null) return;
            //if (pButtonGroupItem == null ||
            //   baseItem == null ||
            //   pButtonGroupItem.eButtonGroupStyle == GISShare.Controls.WinForm.WFNew.ButtonGroupStyle.eButtonList) return;
            //
            int fristDrawBaseItemIndex = pButtonGroupItem.FristDrawBaseItemIndex;
            if (fristDrawBaseItemIndex < 0) return;
            int lastDrawBaseItemIndex = pButtonGroupItem.LastDrawBaseItemIndex;
            if (fristDrawBaseItemIndex == lastDrawBaseItemIndex)
            {
                iLeftTopRadius = pButtonGroupItem.LeftTopRadius;
                iRightTopRadius = pButtonGroupItem.RightTopRadius;
                iLeftBottomRadius = pButtonGroupItem.LeftBottomRadius;
                iRightBottomRadius = pButtonGroupItem.RightBottomRadius;
                return;
            }
            int id = pButtonGroupItem.BaseItems.IndexOf(baseItem);
            if (id < 0) return;
            if (id == fristDrawBaseItemIndex)
            {
                switch (pButtonGroupItem.eOrientation)
                {
                    case Orientation.Horizontal:
                        iLeftTopRadius = pButtonGroupItem.LeftTopRadius;
                        iRightTopRadius = 0;
                        iLeftBottomRadius = pButtonGroupItem.LeftBottomRadius;
                        iRightBottomRadius = 0;
                        break;
                    case Orientation.Vertical:
                        iLeftTopRadius = pButtonGroupItem.LeftTopRadius;
                        iRightTopRadius = pButtonGroupItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = 0;
                        break;
                }
            }
            else if (id == lastDrawBaseItemIndex)
            {
                switch (pButtonGroupItem.eOrientation)
                {
                    case Orientation.Horizontal:
                        iLeftTopRadius = 0;
                        iRightTopRadius = pButtonGroupItem.RightTopRadius;
                        iLeftBottomRadius = 0;
                        iRightBottomRadius = pButtonGroupItem.RightBottomRadius;
                        break;
                    case Orientation.Vertical:
                        iLeftTopRadius = 0;
                        iRightTopRadius = 0;
                        iLeftBottomRadius = pButtonGroupItem.LeftBottomRadius;
                        iRightBottomRadius = pButtonGroupItem.RightBottomRadius;
                        break;
                }
            }
            else
            {
                iLeftTopRadius = 0;
                iRightTopRadius = 0;
                iLeftBottomRadius = 0;
                iRightBottomRadius = 0;
            }
        }

        public virtual GraphicsPath CreateCheckPath(Rectangle rectangle)
        {
            int x = rectangle.X + rectangle.Width / 2;
            int y = rectangle.Y + rectangle.Height / 2;
            //
            GraphicsPath path = new GraphicsPath();
            path.AddLine(x - 4, y, x - 2, y + 4);
            path.AddLine(x - 2, y + 4, x + 3, y - 5);
            return path;
        }

        //
        //
        //

        //#region ViewItemList
        //public override void OnRenderViewItemList(ObjectRenderEventArgs e)
        //{
        //    View.IViewItemList pViewItemList = e.Object as View.IViewItemList;
        //    if (pViewItemList == null) return;
        //    if (pViewItemList.ShowOutLine)
        //    {
        //        using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaOutLine))
        //        {
        //            e.Graphics.DrawRectangle(p, pViewItemList.FrameRectangle);
        //        }
        //    }
        //}
        //#endregion

        #region ViewItem
        public override void OnRenderViewItem(ObjectRenderEventArgs e)
        {
            View.IViewItem pViewItem = e.Object as View.IViewItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            this.DrawViewItem(e.Graphics, pViewItem, rectangle);
        }
        private void DrawViewItem(Graphics g, View.IViewItem pViewItem, Rectangle rectangle)
        {
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupDisabledBorderOut))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonCheckedCenter))
                            {
                                g.FillRectangle(b, rectangle);
                            }
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                            {
                                g.DrawRectangle(p, rectangle);
                            }
                            break;
                        //case ViewParameterStyle.eSelected:
                        //    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                        //    {
                        //        g.FillRectangle(b, rectangle);
                        //    }
                        //    using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                        //    {
                        //        g.DrawRectangle(p, rectangle);
                        //    }
                        //    break;
                    }
                    break;
            }
        }
        #endregion

        #region ColorViewItem
        public override void OnRenderColorViewItem(ObjectRenderEventArgs e)
        {
            View.IColorViewItem pColorViewItem = e.Object as View.IColorViewItem;
            if (pColorViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            this.DrawViewItem(e.Graphics, pColorViewItem, rectangle);
            //switch (pColorViewItem.eBaseItemState)
            //{
            //    case BaseItemState.eDisabled:
            //        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
            //        {
            //            e.Graphics.FillRectangle(b, rectangle);
            //        }
            //        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
            //        {
            //            e.Graphics.DrawRectangle(p, rectangle);
            //        }
            //        break;
            //    case BaseItemState.eHot:
            //        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
            //        {
            //            e.Graphics.FillRectangle(b, rectangle);
            //        }
            //        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
            //        {
            //            e.Graphics.DrawRectangle(p, rectangle);
            //        }
            //        break;
            //    case BaseItemState.ePressed:
            //        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
            //        {
            //            e.Graphics.FillRectangle(b, rectangle);
            //        }
            //        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
            //        {
            //            e.Graphics.DrawRectangle(p, rectangle);
            //        }
            //        break;
            //    default:
            //        switch (pColorViewItem.eViewParameterStyle)
            //        {
            //            case ViewParameterStyle.eSelected:
            //                using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
            //                {
            //                    e.Graphics.FillRectangle(b, rectangle);
            //                }
            //                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
            //                {
            //                    e.Graphics.DrawRectangle(p, rectangle);
            //                }
            //                break;
            //        }
            //        break;
            //}
            //
            if (pColorViewItem.Color.IsEmpty) return;
            rectangle = pColorViewItem.ColorRectangle;
            if (pColorViewItem.Width >= 0 && pColorViewItem.Width < rectangle.Width) return;
            using (SolidBrush b = new SolidBrush(pColorViewItem.Color))
            {
                e.Graphics.FillRectangle(b, rectangle);
                e.Graphics.DrawRectangle(Pens.Black, rectangle);
            }
        }
        #endregion

        #region ImageViewItem
        public override void OnRenderImageViewItem(ObjectRenderEventArgs e)
        {
            View.IImageViewItem pImageViewItem = e.Object as View.IImageViewItem;
            if (pImageViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            this.DrawViewItem(e.Graphics, pImageViewItem, rectangle);
            //
            if (pImageViewItem.Image == null) return;
            if (pImageViewItem.Width >= 0 && pImageViewItem.Width < pImageViewItem.ImageRectangle.Right) return;
            e.Graphics.DrawImage(pImageViewItem.Image, pImageViewItem.ImageRectangle);
        }
        #endregion

        #region NodeViewItem
        public override void OnRenderNodeViewItem(ObjectRenderEventArgs e)
        {
            View.INodeViewItem2 pNodeViewItem = e.Object as View.INodeViewItem2;
            if (pNodeViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            if (pNodeViewItem.ShowBaseItemState)
            {
                switch (pNodeViewItem.eBaseItemState)
                {
                    case BaseItemState.eDisabled:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    case BaseItemState.eHot:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    case BaseItemState.ePressed:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    default:
                        switch (pNodeViewItem.eViewParameterStyle)
                        {
                            case ViewParameterStyle.eFocused:
                            case ViewParameterStyle.eSelected:
                                using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                                {
                                    e.Graphics.FillRectangle(b, rectangle);
                                }
                                using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                                {
                                    e.Graphics.DrawRectangle(p, rectangle);
                                }
                                break;
                            case ViewParameterStyle.eNone:
                            default:
                                if (pNodeViewItem.eNodeViewStyle == View.NodeViewStyle.eTitleNodeView)
                                {
                                    if (pNodeViewItem.SystemColor)
                                    {
                                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonNomalOut, this.WFNewColorTable.ButtonNomalCenter, LinearGradientMode.Vertical))
                                        {
                                            e.Graphics.FillRectangle(b, rectangle);
                                        }
                                        using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderIn))
                                        {
                                            e.Graphics.DrawRectangle(p, rectangle);
                                        }
                                    }
                                    else
                                    {
                                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, pNodeViewItem.TitleBackgroundBegin, pNodeViewItem.TitleBackgroundEnd, LinearGradientMode.Vertical))
                                        {
                                            e.Graphics.FillRectangle(b, rectangle);
                                        }
                                        using (Pen p = new Pen(pNodeViewItem.TitleBorder))
                                        {
                                            e.Graphics.DrawRectangle(p, rectangle);
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            //
            this.DrawNodeViewItem(e.Graphics, pNodeViewItem, rectangle);
        }
        private void DrawNodeViewItem(Graphics g, View.INodeViewItem pNodeViewItem, Rectangle rectangle)
        {
            if (pNodeViewItem.ShowPlusMinus && pNodeViewItem.HaveVisibleNodeView)
            {
                rectangle = pNodeViewItem.PlusMinusRectangle;
                using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
                {
                    int iX = (rectangle.Left + rectangle.Right) / 2;
                    int iY = (rectangle.Top + rectangle.Bottom) / 2;
                    if (pNodeViewItem.IsExpanded)
                    {
                        g.DrawLine(pen, iX - 2, iY, iX + 2, iY);
                    }
                    else
                    {
                        g.DrawLine(pen, iX - 2, iY, iX + 2, iY);
                        g.DrawLine(pen, iX, iY - 2, iX, iY + 2);
                    }
                    //
                    g.DrawRectangle(pen, Rectangle.FromLTRB(iX - 4, iY - 4, iX + 4, iY + 4));
                }
            }
        }
        #endregion

        //

        #region TitleViewItem
        public override void OnRenderTitleViewItem(ObjectRenderEventArgs e)
        {
            View.ISizeViewItem pViewItem = e.Object as View.ISizeViewItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupDisabledBorderOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, rectangle);
                            }
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                            {
                                e.Graphics.DrawRectangle(p, rectangle);
                            }
                            break;
                        default:
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonNomalCenter))
                            {
                                e.Graphics.FillRectangle(b, rectangle);
                            }
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                            {
                                e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                                e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region ColumnViewItem
        public override void OnRenderColumnViewItem(ObjectRenderEventArgs e)
        {
            View.IColumnViewItem pViewItem = e.Object as View.IColumnViewItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonGroupDisabledBorderOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, rectangle);
                            }
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                            {
                                e.Graphics.DrawRectangle(p, rectangle);
                            }
                            break;
                        default:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonNomal, this.WFNewColorTable.ButtonNomalCenter, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, rectangle);
                            }
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                            {
                                e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                                e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region RowHeaderItem
        public override void OnRenderRowHeaderItem(ObjectRenderEventArgs e)
        {
            View.IRowHeaderItem pViewItem = e.Object as View.IRowHeaderItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            if (pViewItem.RowIndex < 0)
            {
                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonNomalCenter))
                {
                    e.Graphics.FillRectangle(b, rectangle);
                }
            }
            else
            {
                switch (pViewItem.eViewParameterStyle)
                {
                    case ViewParameterStyle.eFocused:
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonCheckedCenter))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                        {
                            int iM = (rectangle.Top + rectangle.Bottom) / 2;
                            e.Graphics.DrawLine(p, rectangle.Left + 2, iM - 3, rectangle.Left + 2, iM + 3);
                            e.Graphics.DrawLine(p, rectangle.Left + 3, iM - 2, rectangle.Left + 3, iM + 2);
                            e.Graphics.DrawLine(p, rectangle.Left + 4, iM - 1, rectangle.Left + 4, iM + 1);
                            e.Graphics.DrawLine(p, rectangle.Left + 2, iM, rectangle.Left + 5, iM);
                        }
                        break;
                    case ViewParameterStyle.eSelected:
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonCheckedCenter))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                    default:
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonNomalCenter))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                }
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
            {
                e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }
        }
        #endregion

        #region RowCellViewItem
        public override void OnRenderRowCellViewItem(ObjectRenderEventArgs e)
        {
            View.IRowCellViewItem pViewItem = e.Object as View.IRowCellViewItem;
            if (pViewItem == null) return;
            if (!pViewItem.ShowBaseItemState) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonCheckedCenter))
                            {
                                e.Graphics.FillRectangle(b, rectangle);
                            }
                            break;
                        //case ViewParameterStyle.eSelected:
                        //    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                        //    {
                        //        e.Graphics.FillRectangle(b, rectangle);
                        //    }
                        //    break;
                    }
                    break;
            }
        }
        #endregion

        #region RowNodeCellViewItem
        public override void OnRenderRowNodeCellViewItem(ObjectRenderEventArgs e)
        {
            View.INodeViewItem2 pNodeViewItem = e.Object as View.INodeViewItem2;
            if (pNodeViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            if (pNodeViewItem.ShowBaseItemState)
            {
                switch (pNodeViewItem.eBaseItemState)
                {
                    case BaseItemState.eDisabled:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                        break;
                    case BaseItemState.eHot:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                        break;
                    case BaseItemState.ePressed:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        //using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                        break;
                    default:
                        switch (pNodeViewItem.eViewParameterStyle)
                        {
                            case ViewParameterStyle.eFocused:
                            case ViewParameterStyle.eSelected:
                                using (SolidBrush b = new SolidBrush(this.WFNewColorTable.ButtonCheckedCenter))
                                {
                                    e.Graphics.FillRectangle(b, rectangle);
                                }
                                //using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonChecked, this.WFNewColorTable.ButtonCheckedCenter, LinearGradientMode.Vertical))
                                //{
                                //    e.Graphics.FillRectangle(b, rectangle);
                                //}
                                //using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                                //{
                                //    e.Graphics.DrawRectangle(p, rectangle);
                                //}
                                break;
                            case ViewParameterStyle.eNone:
                            default:
                                //if (pNodeViewItem.eNodeViewStyle == View.NodeViewStyle.eTitleNodeView)
                                //{
                                //    if (pNodeViewItem.SystemColor)
                                //    {
                                //        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonNomalOut, this.WFNewColorTable.ButtonNomalCenter, LinearGradientMode.Vertical))
                                //        {
                                //            e.Graphics.FillRectangle(b, rectangle);
                                //        }
                                //        using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderIn))
                                //        {
                                //            e.Graphics.DrawRectangle(p, rectangle);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, pNodeViewItem.TitleBackgroundBegin, pNodeViewItem.TitleBackgroundEnd, LinearGradientMode.Vertical))
                                //        {
                                //            e.Graphics.FillRectangle(b, rectangle);
                                //        }
                                //        using (Pen p = new Pen(pNodeViewItem.TitleBorder))
                                //        {
                                //            e.Graphics.DrawRectangle(p, rectangle);
                                //        }
                                //    }
                                //}
                                break;
                        }
                        break;
                }
            }
            //
            this.DrawNodeViewItem(e.Graphics, pNodeViewItem, rectangle);
        }
        #endregion

        #region CellViewItem
        public override void OnRenderCellViewItem(ObjectRenderEventArgs e)
        {
            View.ICellViewItem pViewItem = e.Object as View.ICellViewItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                            {
                                e.Graphics.DrawRectangle(p, rectangle);
                            }
                            break;
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                            {
                                e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                                e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                            }
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region NodeCellViewItem
        public override void OnRenderNodeCellViewItem(ObjectRenderEventArgs e)
        {
            View.ICellViewItem pViewItem = e.Object as View.ICellViewItem;
            if (pViewItem == null) return;
            Rectangle rectangle = e.Bounds;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pViewItem.eBaseItemState)
            {
                case BaseItemState.eDisabled:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonDisabled, this.WFNewColorTable.ButtonDisabledCenter, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonDisabledBorderOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.eHot:
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case BaseItemState.ePressed:
                    using (Pen p = new Pen(this.WFNewColorTable.ButtonSelectedOut))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    switch (pViewItem.eViewParameterStyle)
                    {
                        case ViewParameterStyle.eFocused:
                        case ViewParameterStyle.eSelected:
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonCheckedOut))
                            {
                                e.Graphics.DrawRectangle(p, rectangle);
                            }
                            break;
                        default:
                            using (Pen p = new Pen(this.WFNewColorTable.ButtonNomalBorderOut))
                            {
                                e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                                e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                                //
                                e.Graphics.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Left - 1, rectangle.Bottom);
                                e.Graphics.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Right, rectangle.Top - 1);
                            }
                            break;
                    }
                    break;
            }
        }
        #endregion

        //
        //
        //

        #region DockPanelButton
        public override void OnRenderDockPanelButton(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelButtonItem pDockPanelButtonItem = e.Object as GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelButtonItem;
            if (pDockPanelButtonItem != null)
            {
                Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                //
                switch (pDockPanelButtonItem.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        this.DrawBaseButtonSelected(e.Graphics, pDockPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        this.DrawBaseButtonPressed(e.Graphics, pDockPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        this.DrawBaseButtonDisabled(e.Graphics, pDockPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (!pDockPanelButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pDockPanelButtonItem, e.Bounds, 2); }
                        else if (pDockPanelButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pDockPanelButtonItem, e.Bounds, 2); }
                        break;
                }
                //
                switch (pDockPanelButtonItem.eDockPanelButtonItemStyle)
                {
                    case GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelButtonItemStyle.eContextButton:
                        this.DrawContextButtonDP(e.Graphics, pDockPanelButtonItem.GlyphRectangle);
                        break;
                    case GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelButtonItemStyle.eHideButton:
                        this.DrawHideButtonDP(e.Graphics, pDockPanelButtonItem.GlyphRectangle, pDockPanelButtonItem.IsHideState);
                        break;
                    case GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelButtonItemStyle.eCloseButton:
                        this.DrawCloseButtonDP(e.Graphics, pDockPanelButtonItem.GlyphRectangle);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawContextButtonDP(Graphics g, Rectangle rectangle)
        {
            int x1 = rectangle.Left + rectangle.Width / 2;
            int y1 = rectangle.Bottom - 3;
            int y2 = y1 - 4;
            int x2 = x1 - 4;
            int y3 = y2;
            int x3 = x1 + 4;
            using (GraphicsPath arrowHeadPath = new GraphicsPath())
            {
                arrowHeadPath.AddLine(x1, y1, x2, y2);
                arrowHeadPath.AddLine(x2, y2, x3, y3);
                arrowHeadPath.CloseFigure();

                g.FillPath(new SolidBrush(this.WFNewColorTable.Arrow), arrowHeadPath);
            }
        }
        private void DrawHideButtonDP(Graphics g, Rectangle rectangle, bool IsHideState)
        {
            using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
            {
                if (IsHideState)
                {
                    int left = rectangle.Left + 1;
                    int top = rectangle.Top + 1;

                    g.DrawLine(pen, left + 4, top + 9, left + 4, top + 3);
                    g.DrawLine(pen, left + 1, top + 6, left + 4, top + 6);
                    g.DrawLine(pen, left + 10, top + 8, left + 10, top + 4);
                    g.DrawLine(pen, left + 4, top + 4, left + 10, top + 4);
                    g.DrawLine(pen, left + 4, top + 8, left + 10, top + 8);
                    g.DrawLine(pen, left + 4, top + 7, left + 10, top + 7);
                }
                else
                {
                    int left = rectangle.Left + 1;
                    int top = rectangle.Top;

                    g.DrawLine(pen, left + 3, top + 8, left + 9, top + 8);
                    g.DrawLine(pen, left + 6, top + 8, left + 6, top + 11);
                    g.DrawLine(pen, left + 4, top + 2, left + 8, top + 2);
                    g.DrawLine(pen, left + 4, top + 2, left + 4, top + 8);
                    g.DrawLine(pen, left + 8, top + 2, left + 8, top + 8);
                    g.DrawLine(pen, left + 7, top + 2, left + 7, top + 8);
                }
            }
        }
        private void DrawCloseButtonDP(Graphics g, Rectangle rectangle)
        {
            rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
            using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
            }
        }
        #endregion

        #region DockPanel
        public override void OnRenderDockPanel(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanel2 pDockPanel = e.Object as GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanel2;
            if (pDockPanel == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaBackground))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            Rectangle rectangle = pDockPanel.CaptionRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (pDockPanel.bActive)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                          this.WFNewColorTable.FormActiveCaptionEnd, this.WFNewColorTable.FormActiveCaptionBegin, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(b, rectangle);
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaOutLine))
                {
                    e.Graphics.DrawRectangle(p, pDockPanel.FrameRectangle);
                }
            }
            else
            {
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                    this.WFNewColorTable.FormUnActiveCaptionEnd, this.WFNewColorTable.FormUnActiveCaptionBegin, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(b, rectangle);
                }
                //
                using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaDisabledOutLine))
                {
                    e.Graphics.DrawRectangle(p, pDockPanel.FrameRectangle);
                }
            }
        }
        #endregion

        #region DockPanelFloatFormButton
        public override void OnRenderDockPanelFloatFormButton(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelFloatFormButtonItem pDockPanelFloatFormButtonItem = e.Object as GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelFloatFormButtonItem;
            if (pDockPanelFloatFormButtonItem != null)
            {
                Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                //
                switch (pDockPanelFloatFormButtonItem.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        this.DrawBaseButtonSelected(e.Graphics, pDockPanelFloatFormButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        this.DrawBaseButtonPressed(e.Graphics, pDockPanelFloatFormButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        this.DrawBaseButtonDisabled(e.Graphics, pDockPanelFloatFormButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (!pDockPanelFloatFormButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pDockPanelFloatFormButtonItem, e.Bounds, 2); }
                        else if (pDockPanelFloatFormButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pDockPanelFloatFormButtonItem, e.Bounds, 2); }
                        break;
                }
                //
                switch (pDockPanelFloatFormButtonItem.eDockPanelFloatFormButtonItemStyle)
                {
                    case GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelFloatFormButtonItemStyle.eMaxButton:
                        if (pDockPanelFloatFormButtonItem.OperationForm != null)
                        {
                            switch (pDockPanelFloatFormButtonItem.OperationForm.WindowState)
                            {
                                case FormWindowState.Minimized:
                                case FormWindowState.Maximized:
                                    this.DrawNormalButtonDPF(e.Graphics, pDockPanelFloatFormButtonItem.GlyphRectangle);
                                    break;
                                case FormWindowState.Normal:
                                    this.DrawMaxButtonDPF(e.Graphics, pDockPanelFloatFormButtonItem.GlyphRectangle);
                                    break;
                            }
                        }
                        else
                        {
                            this.DrawMaxButtonDPF(e.Graphics, pDockPanelFloatFormButtonItem.GlyphRectangle);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelFloatFormButtonItemStyle.eCloseButton:
                        this.DrawCloseButtonDPF(e.Graphics, pDockPanelFloatFormButtonItem.GlyphRectangle);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawNormalButtonDPF(Graphics g, Rectangle rectangle)
        {
            int iS = (int)((double)rectangle.Width / 3);
            Rectangle rectangle2 = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + iS + 1, rectangle.Right - iS + 1, rectangle.Bottom + 1);
            rectangle.Offset(1, 1);
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Left + iS, rectangle2.Top);
                g.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle2.Right, rectangle2.Top + iS, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                //
                g.DrawRectangle(p, rectangle2);
                g.DrawLine(p, rectangle2.Left, rectangle2.Top + 1, rectangle2.Right, rectangle2.Top + 1);
            }
            //
            rectangle2.Offset(-1, -1);
            rectangle.Offset(-1, -1);
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Right, rectangle.Top);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Left + iS, rectangle2.Top);
                g.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle2.Right, rectangle2.Top + iS, rectangle.Right, rectangle2.Top + iS);
                g.DrawLine(p, rectangle.Left + iS, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                //
                g.DrawRectangle(p, rectangle2);
                g.DrawLine(p, rectangle2.Left, rectangle2.Top + 1, rectangle2.Right, rectangle2.Top + 1);
            }
        }
        private void DrawMaxButtonDPF(Graphics g, Rectangle rectangle)
        {
            using (Pen p = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(p, rectangle.Left, rectangle.Top + 3, rectangle.Right, rectangle.Top + 3);
            }
            //
            using (Pen p = new Pen(this.WFNewColorTable.Arrow))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                g.DrawLine(p, rectangle.Left, rectangle.Top + 2, rectangle.Right, rectangle.Top + 2);
            }
        }
        private void DrawCloseButtonDPF(Graphics g, Rectangle rectangle)
        {
            rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
            rectangle.Offset(1, 1);
            using (Pen pen = new Pen(this.WFNewColorTable.ArrowLight))
            {
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
            }
            rectangle.Offset(-1, -1);
            using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
            }
        }
        #endregion

        #region DockPanelFloatForm
        public override void OnRenderDockPanelFloatForm(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelFloatForm pDockPanelFloatForm = e.Object as GISShare.Controls.WinForm.WFNew.DockPanel.IDockPanelFloatForm;
            if (pDockPanelFloatForm == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaBackground))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            //
            Rectangle rectangle = pDockPanelFloatForm.CaptionRectangle;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                          this.WFNewColorTable.FormActiveCaptionBegin, this.WFNewColorTable.FormActiveCaptionEnd, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, rectangle);
            }
            using (Pen p = new Pen(this.WFNewColorTable.FormUnActiveOutLine))
            {
                rectangle = pDockPanelFloatForm.FrameRectangle;
                e.Graphics.DrawRectangle(p, rectangle);
                rectangle.Inflate(-1, -1);
                e.Graphics.DrawRectangle(p, rectangle);
            }
        }
        #endregion

        #region SplitLine
        public override void OnRenderSplitLine(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.ISplitLine pSplitLine = e.Object as GISShare.Controls.WinForm.WFNew.ISplitLine;
            if (pSplitLine == null) return;
            //
            using (Pen p = new Pen(this.WFNewColorTable.Arrow, 1))
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                switch (pSplitLine.SplitPanelDock)
                {
                    case DockStyle.Top:
                        for (int i = 0; i <= pSplitLine.Height; i++)
                        {
                            if (i % 2 == 0) { e.Graphics.DrawLine(p, 1, pSplitLine.Height - i, pSplitLine.Width - 1, pSplitLine.Height - i); }
                            else { e.Graphics.DrawLine(p, 0, pSplitLine.Height - i, pSplitLine.Width, pSplitLine.Height - i); }
                        }
                        break;
                    case DockStyle.Bottom:
                        for (int i = 0; i <= pSplitLine.Height; i++)
                        {
                            if (i % 2 == 0) { e.Graphics.DrawLine(p, 1, i, pSplitLine.Width - 1, i); }
                            else { e.Graphics.DrawLine(p, 0, i, pSplitLine.Width, i); }
                        }
                        break;
                    case DockStyle.Left:
                        for (int i = 0; i <= pSplitLine.Width; i++)
                        {
                            if (i % 2 == 0) { e.Graphics.DrawLine(p, pSplitLine.Width - i, 1, pSplitLine.Width - i, pSplitLine.Height - 1); }
                            else { e.Graphics.DrawLine(p, pSplitLine.Width - i, 0, pSplitLine.Width - i, pSplitLine.Height); }
                        }
                        break;
                    case DockStyle.Right:
                        for (int i = 0; i <= pSplitLine.Width; i++)
                        {
                            if (i % 2 == 0) { e.Graphics.DrawLine(p, i, 1, i, pSplitLine.Height - 1); }
                            else { e.Graphics.DrawLine(p, i, 0, i, pSplitLine.Height); }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region DockButton
        public override void OnRenderDockButton(ObjectRenderEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.IDockButtonItem pDockButtonItem = e.Object as GISShare.Controls.WinForm.WFNew.DockPanel.IDockButtonItem;
            if (pDockButtonItem == null) return;
            //
            if (pDockButtonItem.Image == null) return;
            //
            e.Graphics.DrawImage(pDockButtonItem.Image, e.Bounds);
            //
            if (pDockButtonItem.eBaseItemState != BaseItemState.eHot) return;
            //
            Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
            switch (pDockButtonItem.eDockButtonStyle)
            {
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDockFill:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top + 6, rectangle.Left + 6, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Left + 6, rectangle.Top, rectangle.Right - 6, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Right - 6, rectangle.Top, rectangle.Right, rectangle.Top + 6);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top + 6, rectangle.Right, rectangle.Bottom - 6);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Bottom - 6, rectangle.Right - 6, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Right - 6, rectangle.Bottom, rectangle.Left + 6, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top + 6, rectangle.Left, rectangle.Bottom - 6);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom - 6, rectangle.Left + 6, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDocumentUp:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom - 6);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - 6);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDocumentLeft:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right - 6, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right - 6, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDocumentRight:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left + 6, rectangle.Top, rectangle.Right, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left + 6, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDocumentBottom:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top + 6, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top + 6, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDockUp:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom - 6);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - 6);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDockLeft:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right - 6, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right - 6, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDockRight:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left + 6, rectangle.Top, rectangle.Right, rectangle.Top);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left + 6, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eCenterToDockBottom:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Top + 6, rectangle.Left, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                        e.Graphics.DrawLine(p, rectangle.Right, rectangle.Top + 6, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eToDockUp:
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eToDockLeft:
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eToDockRight:
                case GISShare.Controls.WinForm.WFNew.DockPanel.DockButtonStyle.eToDockBottom:
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        //
        //
        //

        #region CollapsableSplitPanel
        public override void OnRenderCollapsableSplitPanel(ObjectRenderEventArgs e)
        {
            WFNew.ICollapsableSplitPanel pCollapsableSplitPanel = e.Object as WFNew.ICollapsableSplitPanel;
            if (pCollapsableSplitPanel == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaBackground))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            //
            Rectangle rectangle = pCollapsableSplitPanel.SplitterRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (pCollapsableSplitPanel.SplitPanelDock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                           this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd,
                           LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                        //
                        if (pCollapsableSplitPanel.eCollapseSplitPanelStyles == GISShare.Controls.WinForm.WFNew.CollapseSplitPanelStyles.SplitPanel) return;
                        //
                        rectangle = pCollapsableSplitPanel.CollapseButtonRectangle;
                        if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
                        //
                        switch (pCollapsableSplitPanel.eCollapseButtonState)
                        {
                            case BaseItemState.eHot:
                                e.Graphics.FillRectangle(new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Horizontal), rectangle);
                                break;
                            case BaseItemState.ePressed:
                                e.Graphics.FillRectangle(new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonPressed, this.WFNewColorTable.ButtonPressedCenter, LinearGradientMode.Horizontal), rectangle);
                                break;
                            default:
                                break;
                        }
                        //
                        int x = rectangle.X + 15;
                        int y = rectangle.Y + 2;
                        for (int i = 0; i < 7; i++)
                        {
                            e.Graphics.DrawRectangle(new Pen(this.WFNewColorTable.ArrowLight), x + (i * 5), y, 2, 2);
                            e.Graphics.DrawRectangle(new Pen(this.WFNewColorTable.Arrow), x + (i * 5), y, 1, 1);
                        }
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), this.ArrowPointArray(pCollapsableSplitPanel, rectangle.X, rectangle.Y));
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), this.ArrowPointArray(pCollapsableSplitPanel, rectangle.X + rectangle.Width - 9, rectangle.Y));
                    }
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                           this.WFNewColorTable.BaseBarBackgroundBegin, this.WFNewColorTable.BaseBarBackgroundEnd,
                           LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                        //
                        if (pCollapsableSplitPanel.eCollapseSplitPanelStyles == GISShare.Controls.WinForm.WFNew.CollapseSplitPanelStyles.SplitPanel) return;
                        //
                        rectangle = pCollapsableSplitPanel.CollapseButtonRectangle;
                        if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
                        //
                        switch (pCollapsableSplitPanel.eCollapseButtonState)
                        {
                            case BaseItemState.eHot:
                                e.Graphics.FillRectangle(new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonSelected, this.WFNewColorTable.ButtonSelectedCenter, LinearGradientMode.Vertical), rectangle);
                                break;
                            case BaseItemState.ePressed:
                                e.Graphics.FillRectangle(new LinearGradientBrush(rectangle, this.WFNewColorTable.ButtonPressed, this.WFNewColorTable.ButtonPressedCenter, LinearGradientMode.Vertical), rectangle);
                                break;
                            default:
                                break;
                        }
                        //
                        int x = rectangle.X + 2;
                        int y = rectangle.Y + 15;
                        for (int i = 0; i < 7; i++)
                        {
                            e.Graphics.DrawRectangle(new Pen(this.WFNewColorTable.ArrowLight), x, y + (i * 5), 2, 2);
                            e.Graphics.DrawRectangle(new Pen(this.WFNewColorTable.Arrow), x, y + (i * 5), 1, 1);
                        }
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), this.ArrowPointArray(pCollapsableSplitPanel, rectangle.X, rectangle.Y));
                        e.Graphics.FillPolygon(new SolidBrush(this.WFNewColorTable.Arrow), this.ArrowPointArray(pCollapsableSplitPanel, rectangle.X, rectangle.Y + rectangle.Height - 11));
                    }
                    break;
                default:
                    break;
            }
        }
        private Point[] ArrowPointArray(WFNew.ICollapsableSplitPanel pCollapsableSplitPanel, int x, int y)
        {
            Point[] point = new Point[3];
            //
            if ((pCollapsableSplitPanel.SplitPanelDock == DockStyle.Left && pCollapsableSplitPanel.Collapse) ||
                   (pCollapsableSplitPanel.SplitPanelDock == DockStyle.Right && !pCollapsableSplitPanel.Collapse))
            {
                // right arrow
                point[0] = new Point(x + 1, y + 1);
                point[1] = new Point(x + 5, y + 5);
                point[2] = new Point(x, y + 10);
            }
            else if ((pCollapsableSplitPanel.SplitPanelDock == DockStyle.Left && !pCollapsableSplitPanel.Collapse) ||
                (pCollapsableSplitPanel.SplitPanelDock == DockStyle.Right && pCollapsableSplitPanel.Collapse))
            {
                // left arrow
                point[0] = new Point(x + 5, y + 1);
                point[1] = new Point(x + 1, y + 6);
                point[2] = new Point(x + 5, y + 10);
            }
            else if ((pCollapsableSplitPanel.SplitPanelDock == DockStyle.Bottom && pCollapsableSplitPanel.Collapse) ||
                (pCollapsableSplitPanel.SplitPanelDock == DockStyle.Top && !pCollapsableSplitPanel.Collapse))
            {
                // up arrow
                point[0] = new Point(x + 5, y);
                point[1] = new Point(x + 10, y + 5);
                point[2] = new Point(x, y + 5);
            }
            else if ((pCollapsableSplitPanel.SplitPanelDock == DockStyle.Bottom && !pCollapsableSplitPanel.Collapse) ||
                (pCollapsableSplitPanel.SplitPanelDock == DockStyle.Top && pCollapsableSplitPanel.Collapse))
            {
                // down arrow
                point[0] = new Point(x + 1, y + 1);
                point[1] = new Point(x + 10, y);
                point[2] = new Point(x + 5, y + 5);
            }
            //
            return point;
        }
        #endregion

        //
        //
        //

        #region ExpandableCaptionPanel
        public override void OnRenderExpandableCaptionPanel(ObjectRenderEventArgs e)
        {
            IExpandableCaptionPanel pExpandableCaptionPanel = e.Object as IExpandableCaptionPanel;
            if (pExpandableCaptionPanel == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.RibbonAreaBackground))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            //
            if (!pExpandableCaptionPanel.ShowCaption) return;
            //
            LinearGradientMode eLinearGradientMode = LinearGradientMode.Vertical;
            switch (pExpandableCaptionPanel.eCaptionAlignment)
            {
                case TabAlignment.Left:
                case TabAlignment.Right:
                default:
                    eLinearGradientMode = LinearGradientMode.Horizontal;
                    break;
            }
            //
            Rectangle rectangle = pExpandableCaptionPanel.CaptionRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (pExpandableCaptionPanel.UseRadius)
            {
                if (pExpandableCaptionPanel.bActive)
                {
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pExpandableCaptionPanel.LeftTopRadius, pExpandableCaptionPanel.RightTopRadius, pExpandableCaptionPanel.LeftBottomRadius, pExpandableCaptionPanel.RightBottomRadius))
                    {
                        if (pExpandableCaptionPanel.IsSimplyDrawCaption)
                        {
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.FormActiveCaptionBegin))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                        else
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                                   this.WFNewColorTable.FormActiveCaptionEnd, this.WFNewColorTable.FormActiveCaptionBegin, eLinearGradientMode))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                    }
                    //
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pExpandableCaptionPanel.FrameRectangle, pExpandableCaptionPanel.LeftTopRadius, pExpandableCaptionPanel.RightTopRadius, pExpandableCaptionPanel.LeftBottomRadius, pExpandableCaptionPanel.RightBottomRadius))
                    {
                        if (pExpandableCaptionPanel.ShowOutLine)
                        {
                            using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaOutLine))
                            {
                                e.Graphics.DrawPath(p, path);
                            }
                        }
                    }
                }
                else
                {
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(rectangle, pExpandableCaptionPanel.LeftTopRadius, pExpandableCaptionPanel.RightTopRadius, pExpandableCaptionPanel.LeftBottomRadius, pExpandableCaptionPanel.RightBottomRadius))
                    {
                        if (pExpandableCaptionPanel.IsSimplyDrawCaption)
                        {
                            using (SolidBrush b = new SolidBrush(this.WFNewColorTable.FormUnActiveCaptionBegin))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                        else
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                                this.WFNewColorTable.FormUnActiveCaptionEnd, this.WFNewColorTable.FormUnActiveCaptionBegin, eLinearGradientMode))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                        }
                    }
                    //
                    using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(pExpandableCaptionPanel.FrameRectangle, pExpandableCaptionPanel.LeftTopRadius, pExpandableCaptionPanel.RightTopRadius, pExpandableCaptionPanel.LeftBottomRadius, pExpandableCaptionPanel.RightBottomRadius))
                    {
                        if (pExpandableCaptionPanel.ShowOutLine)
                        {
                            using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaDisabledOutLine))
                            {
                                e.Graphics.DrawPath(p, path);
                            }
                        }
                    }
                }
            }
            else
            {
                if (pExpandableCaptionPanel.bActive)
                {
                    if (pExpandableCaptionPanel.IsSimplyDrawCaption)
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.FormActiveCaptionBegin))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    else
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                               this.WFNewColorTable.FormActiveCaptionEnd, this.WFNewColorTable.FormActiveCaptionBegin, eLinearGradientMode))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    //
                    if (pExpandableCaptionPanel.ShowOutLine)
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaOutLine))
                        {
                            e.Graphics.DrawRectangle(p, pExpandableCaptionPanel.FrameRectangle);
                        }
                    }
                }
                else
                {
                    if (pExpandableCaptionPanel.IsSimplyDrawCaption)
                    {
                        using (SolidBrush b = new SolidBrush(this.WFNewColorTable.FormUnActiveCaptionBegin))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    else
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle,
                            this.WFNewColorTable.FormUnActiveCaptionEnd, this.WFNewColorTable.FormUnActiveCaptionBegin, eLinearGradientMode))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    //
                    if (pExpandableCaptionPanel.ShowOutLine)
                    {
                        using (Pen p = new Pen(this.WFNewColorTable.RibbonAreaDisabledOutLine))
                        {
                            e.Graphics.DrawRectangle(p, pExpandableCaptionPanel.FrameRectangle);
                        }
                    }
                }
            }
        }
        #endregion

        #region ExpandableCaptionPanelButton
        public override void OnRenderExpandableCaptionPanelButton(ObjectRenderEventArgs e)
        {
            IExpandableCaptionPanelButtonItem pExpandableCaptionPanelButtonItem = e.Object as IExpandableCaptionPanelButtonItem;
            if (pExpandableCaptionPanelButtonItem != null)
            {
                Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                //
                switch (pExpandableCaptionPanelButtonItem.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        this.DrawBaseButtonSelected(e.Graphics, pExpandableCaptionPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        this.DrawBaseButtonPressed(e.Graphics, pExpandableCaptionPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        this.DrawBaseButtonDisabled(e.Graphics, pExpandableCaptionPanelButtonItem, e.Bounds, 2);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (!pExpandableCaptionPanelButtonItem.Checked) { this.DrawBaseButtonNomal(e.Graphics, pExpandableCaptionPanelButtonItem, e.Bounds, 2); }
                        else if (pExpandableCaptionPanelButtonItem.NomalChecked) { this.DrawBaseButtonChecked(e.Graphics, pExpandableCaptionPanelButtonItem, e.Bounds, 2); }
                        break;
                }
                //
                switch (pExpandableCaptionPanelButtonItem.eExpandableCaptionPanelButtonItemStyle)
                {
                    case GISShare.Controls.WinForm.WFNew.ExpandableCaptionPanelButtonItemStyle.eTreeNodeButton:
                        this.DrawTreeNodeButtonECP(e.Graphics, pExpandableCaptionPanelButtonItem.GlyphRectangle, pExpandableCaptionPanelButtonItem.IsExpand);
                        break;
                    case GISShare.Controls.WinForm.WFNew.ExpandableCaptionPanelButtonItemStyle.eExpandButton:
                        this.DrawExpandButtonECP(e.Graphics, pExpandableCaptionPanelButtonItem.GlyphRectangle, pExpandableCaptionPanelButtonItem.IsExpand, pExpandableCaptionPanelButtonItem.UseMaxMinStyle, pExpandableCaptionPanelButtonItem.eExpandButtonStyle);
                        break;
                    case GISShare.Controls.WinForm.WFNew.ExpandableCaptionPanelButtonItemStyle.eCloseButton:
                        this.DrawCloseButtonECP(e.Graphics, pExpandableCaptionPanelButtonItem.GlyphRectangle);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawTreeNodeButtonECP(Graphics g, Rectangle rectangle, bool bIsExpand)
        {
            using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
            {
                int iX = (rectangle.Left + rectangle.Right) / 2;
                int iY = (rectangle.Top + rectangle.Bottom) / 2;
                if (bIsExpand)
                {
                    g.DrawLine(pen, iX - 3, iY, iX + 3, iY);
                }
                else
                {
                    g.DrawLine(pen, iX - 3, iY, iX + 3, iY);
                    g.DrawLine(pen, iX, iY - 3, iX, iY + 3);
                }
                //
                g.DrawRectangle(pen, Rectangle.FromLTRB(iX - 5, iY - 5, iX + 5, iY + 5));
            }
        }
        private void DrawExpandButtonECP(Graphics g, Rectangle rectangle, bool bIsExpand, bool bUseMaxMinStyle, ExpandButtonStyle eExpandButtonStyle)
        {
            if (bUseMaxMinStyle)
            {
                if (bIsExpand)
                {
                    int iS = (int)((double)rectangle.Width / 3);
                    Rectangle rectangle2 = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + iS + 1, rectangle.Right - iS + 1, rectangle.Bottom + 1);
                    rectangle2.Offset(-1, -1);
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Right, rectangle.Top);
                        g.DrawLine(p, rectangle.Left + iS, rectangle.Top, rectangle.Left + iS, rectangle2.Top);
                        g.DrawLine(p, rectangle.Right, rectangle.Top, rectangle.Right, rectangle2.Top + iS);
                        g.DrawLine(p, rectangle2.Right, rectangle2.Top + iS, rectangle.Right, rectangle2.Top + iS);
                        g.DrawLine(p, rectangle.Left + iS, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                        //
                        g.DrawRectangle(p, rectangle2);
                        g.DrawLine(p, rectangle2.Left, rectangle2.Top + 1, rectangle2.Right, rectangle2.Top + 1);
                    }
                }
                else
                {
                    using (Pen p = new Pen(this.WFNewColorTable.Arrow))
                    {
                        p.Width = 1.6f;
                        g.DrawRectangle(p, rectangle);
                        g.DrawLine(p, rectangle.Left, rectangle.Top + 1, rectangle.Right, rectangle.Top + 1);
                        g.DrawLine(p, rectangle.Left, rectangle.Top + 2, rectangle.Right, rectangle.Top + 2);
                    }
                }
            }
            else
            {
                using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
                {
                    switch (eExpandButtonStyle)
                    {
                        case ExpandButtonStyle.eTopToBottom:
                            if (bIsExpand)
                            {
                                // /\
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 4, rectangle.X + 4, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 5, rectangle.X + 5, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 5, rectangle.X + 9, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 4, rectangle.X + 10, rectangle.Y + 7);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 8, rectangle.X + 4, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 9, rectangle.X + 5, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 9, rectangle.X + 9, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 8, rectangle.X + 10, rectangle.Y + 11);
                            }
                            else
                            {
                                // v
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 6, rectangle.X + 5, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 6, rectangle.X + 9, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 4);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 11, rectangle.X + 4, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 10, rectangle.X + 5, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 10, rectangle.X + 9, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 11, rectangle.X + 10, rectangle.Y + 8);
                            }
                            break;
                        case ExpandButtonStyle.eBottomToTop:
                            if (bIsExpand)
                            {
                                // v
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 6, rectangle.X + 5, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 6, rectangle.X + 9, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 4);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 11, rectangle.X + 4, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 10, rectangle.X + 5, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 10, rectangle.X + 9, rectangle.Y + 8);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 11, rectangle.X + 10, rectangle.Y + 8);
                            }
                            else
                            {
                                // /\
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 4, rectangle.X + 4, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 5, rectangle.X + 5, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 5, rectangle.X + 9, rectangle.Y + 7);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 4, rectangle.X + 10, rectangle.Y + 7);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 8, rectangle.X + 4, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 9, rectangle.X + 5, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 9, rectangle.X + 9, rectangle.Y + 11);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 8, rectangle.X + 10, rectangle.Y + 11);
                            }
                            break;
                        case ExpandButtonStyle.eLeftToRight:
                            if (bIsExpand)
                            {
                                // <
                                g.DrawLine(pen, rectangle.X + 3, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 4, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 4, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 3, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 10);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 8, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 8, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 10);
                            }
                            else
                            {
                                // >
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 6, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 6, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 10);
                                //
                                g.DrawLine(pen, rectangle.X + 11, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 10, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 10, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 11, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 10);
                            }
                            break;
                        case ExpandButtonStyle.eRightToLeft:
                            if (bIsExpand)
                            {
                                // >
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 6, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 6, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 4, rectangle.Y + 10);
                                //
                                g.DrawLine(pen, rectangle.X + 11, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 10, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 10, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 11, rectangle.Y + 7, rectangle.X + 8, rectangle.Y + 10);
                            }
                            else
                            {
                                // <
                                g.DrawLine(pen, rectangle.X + 3, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 4, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 4, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 3, rectangle.Y + 7, rectangle.X + 6, rectangle.Y + 10);
                                //
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 4);
                                g.DrawLine(pen, rectangle.X + 8, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 5);
                                g.DrawLine(pen, rectangle.X + 8, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 9);
                                g.DrawLine(pen, rectangle.X + 7, rectangle.Y + 7, rectangle.X + 10, rectangle.Y + 10);
                            }
                            break;
                    }
                }
            }
        }
        private void DrawCloseButtonECP(Graphics g, Rectangle rectangle)
        {
            rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
            using (Pen pen = new Pen(this.WFNewColorTable.Arrow))
            {
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
            }
        }
        #endregion
    }
}
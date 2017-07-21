using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace GISShare.Controls.WinForm
{
    public class WinFormRenderer : ToolStripProfessionalRenderer 
    {
        public static WinFormRenderer WinFormRendererStrategy = new WinFormRenderer();

        private WinFormColorTable m_WinFormColorTable = new WinFormColorTable();
        public WinFormColorTable WinFormColorTable
        {
            get { return m_WinFormColorTable; }
        }       

        public WinFormRenderer()
             : this(new WinFormColorTable())
        { }

        public WinFormRenderer(WinFormColorTable colorTable)
            : base(colorTable)
        {
            this.m_WinFormColorTable = colorTable;
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle arrowRectangle = e.ArrowRectangle;
            using (Brush brush = new SolidBrush(this.WinFormColorTable.Arrow))
            {
                Point point = new Point(arrowRectangle.Left + (arrowRectangle.Width / 2), arrowRectangle.Top + (arrowRectangle.Height / 2));
                Point[] points = null;
                switch (e.Direction)
                {
                    case ArrowDirection.Left:
                        points = new Point[] { new Point(point.X + 2, point.Y - 4), new Point(point.X + 2, point.Y + 4), new Point(point.X - 2, point.Y) };
                        break;
                    case ArrowDirection.Up:
                        points = new Point[] { new Point(point.X - 2, point.Y + 1), new Point(point.X + 3, point.Y + 1), new Point(point.X, point.Y - 2) };
                        break;
                    case ArrowDirection.Right:
                        points = new Point[] { new Point(point.X - 2, point.Y - 4), new Point(point.X - 2, point.Y + 4), new Point(point.X + 2, point.Y) };
                        break;
                    default:
                        points = new Point[] { new Point(point.X - 2, point.Y - 1), new Point(point.X + 3, point.Y - 1), new Point(point.X, point.Y + 2) };
                        break;
                }
                graphics.FillPolygon(brush, points);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            ToolStripItem item = e.Item;
            Graphics dc = e.Graphics;
            Color textColor = e.TextColor;
            Font textFont = e.TextFont;
            string text = e.Text;
            Rectangle textRectangle = e.TextRectangle;
            TextFormatFlags textFormat = e.TextFormat;
            textColor = item.Enabled ? this.WinFormColorTable.ItemText : this.WinFormColorTable.ItemTextDisabled;
            if (((e.TextDirection != ToolStripTextDirection.Horizontal) && (textRectangle.Width > 0)) && (textRectangle.Height > 0))
            {
                Size size = this.FlipSize(textRectangle.Size);
                using (Bitmap bitmap = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                {
                    using (Graphics graphics2 = Graphics.FromImage(bitmap))
                    {
                        graphics2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        TextRenderer.DrawText(graphics2, text, textFont, new Rectangle(Point.Empty, size), textColor, textFormat);
                        bitmap.RotateFlip((e.TextDirection == ToolStripTextDirection.Vertical90) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);
                        dc.DrawImage(bitmap, textRectangle);
                    }
                    return;
                }
            }
            TextRenderer.DrawText(dc, text, textFont, textRectangle, textColor, textFormat);
        }
        private Size FlipSize(Size size)
        {
            int width = size.Width;
            size.Width = size.Height;
            size.Height = width;
            return size;
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderOverflowButtonBackground(e);
            //
            //
            //
            DockBar.IDockBar pDockBar = e.ToolStrip as DockBar.IDockBar;
            if (pDockBar == null || !pDockBar.DrawMoreArrow) return;
            using (Pen pen1 = new Pen(e.Item.ForeColor))
            {
                Pen pen2 = new Pen(Color.White);
                switch (pDockBar.LayoutStyle)
                {
                    case ToolStripLayoutStyle.HorizontalStackWithOverflow:
                        e.Graphics.DrawLine(pen1, e.Item.Width - 9, 4, e.Item.Width - 9, 6);
                        e.Graphics.DrawLine(pen1, e.Item.Width - 9, 5, e.Item.Width - 8, 5);
                        e.Graphics.DrawLine(pen2, e.Item.Width - 8, 6, e.Item.Width - 7, 6);
                        e.Graphics.DrawLine(pen2, e.Item.Width - 9, 7, e.Item.Width - 8, 7);
                        //
                        e.Graphics.DrawLine(pen1, e.Item.Width - 5, 4, e.Item.Width - 5, 6);
                        e.Graphics.DrawLine(pen1, e.Item.Width - 5, 5, e.Item.Width - 4, 5);
                        e.Graphics.DrawLine(pen2, e.Item.Width - 4, 6, e.Item.Width - 3, 6);
                        e.Graphics.DrawLine(pen2, e.Item.Width - 5, 7, e.Item.Width - 4, 7);
                        break;
                    case ToolStripLayoutStyle.VerticalStackWithOverflow:
                        e.Graphics.DrawLine(pen1, 3, e.Item.Height - 5, 5, e.Item.Height - 5);
                        e.Graphics.DrawLine(pen1, 4, e.Item.Height - 3, 4, e.Item.Height - 4);
                        e.Graphics.DrawLine(pen2, 4, e.Item.Height - 3, 5, e.Item.Height - 3);
                        e.Graphics.DrawLine(pen2, 5, e.Item.Height - 4, 6, e.Item.Height - 4);
                        //
                        e.Graphics.DrawLine(pen1, 3, e.Item.Height - 9, 5, e.Item.Height - 9);
                        e.Graphics.DrawLine(pen1, 4, e.Item.Height - 7, 4, e.Item.Height - 8);
                        e.Graphics.DrawLine(pen2, 4, e.Item.Height - 7, 5, e.Item.Height - 7);
                        e.Graphics.DrawLine(pen2, 5, e.Item.Height - 8, 6, e.Item.Height - 8);
                        break;
                }
            }
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            base.OnRenderToolStripContentPanelBackground(e);
            if (ColorTable.UseSystemColors == false)
            {
                if ((e.ToolStripContentPanel.Width > 0) &&
                    (e.ToolStripContentPanel.Height > 0))
                {
                    using (LinearGradientBrush backBrush = new LinearGradientBrush(e.ToolStripContentPanel.ClientRectangle,
                                                                                   ColorTable.ToolStripContentPanelGradientBegin,
                                                                                   ColorTable.ToolStripContentPanelGradientEnd,
                                                                                   LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(backBrush, e.ToolStripContentPanel.ClientRectangle);
                    }
                }
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == true)
            {
                base.OnRenderToolStripBackground(e);
            }
            else
            {
                if (e.ToolStrip is StatusStrip)
                {
                    RectangleF backRectangle = new RectangleF(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
                    //
                    if ((backRectangle.Width > 0) && (backRectangle.Height > 0))
                    {
                        using (LinearGradientBrush backBrush = new LinearGradientBrush(backRectangle,
                                                                                       ColorTable.StatusStripGradientBegin,
                                                                                       ColorTable.StatusStripGradientEnd,
                                                                                       LinearGradientMode.Vertical))
                        {
                            backBrush.Blend = new Blend();
                            backBrush.Blend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.4F, 0.8F, 1.0F };
                            backBrush.Blend.Factors = new float[] { 0.3F, 0.4F, 0.5F, 1.0F, 0.8F, 0.7F };
                            e.Graphics.FillRectangle(backBrush, backRectangle);
                        }
                    }
                }
                else
                {
                    base.OnRenderToolStripBackground(e);
                }
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == true)
            {
                base.OnRenderToolStripBackground(e);
            }
            else
            {
                if ((e.ToolStrip is ContextMenuStrip) || (e.ToolStrip is ToolStripDropDownMenu))
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, this.WinFormColorTable.ImageMarginGradientBegin, this.WinFormColorTable.ImageMarginGradientEnd, LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillRectangle(b, e.AffectedBounds); 
                    }
                }
                else
                {
                    base.OnRenderImageMargin(e);
                }
            }
        }

        #region FloatForm
        public virtual void OnRenderFloatForm(ObjectRenderEventArgs e)
        {
            DockBar.IDockBarFloatForm pDockBarFloatForm = e.Object as DockBar.IDockBarFloatForm;
            if (pDockBarFloatForm == null) return;
            //
            using (SolidBrush b = new SolidBrush(this.ColorTable.ToolStripGradientEnd))
            {
                e.Graphics.FillRectangle(b, pDockBarFloatForm.FrameRectangle);
                e.Graphics.DrawRectangle(Pens.White, new Rectangle(2, 2, pDockBarFloatForm.FrameRectangle.Width - 5, pDockBarFloatForm.FrameRectangle.Height - 5));
            }
        }
        #endregion

        #region FormButton ContextButton CloseButton
        public virtual void OnRenderFloatFormButton(ObjectRenderEventArgs e)
        {
            DockBar.IDockBarFloatFormButton pDockBarFloatFormButton = e.Object as DockBar.IDockBarFloatFormButton;
            if (pDockBarFloatFormButton != null)
            {
                Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width -1 , e.Bounds.Height - 1);
                //
                switch (pDockBarFloatFormButton.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        e.Graphics.FillRectangle(new SolidBrush(this.ColorTable.ButtonSelectedGradientEnd), rectangle);
                        e.Graphics.DrawRectangle(new Pen(this.ColorTable.ButtonSelectedBorder), rectangle);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        e.Graphics.FillRectangle(new SolidBrush(this.ColorTable.ButtonPressedGradientEnd), rectangle);
                        e.Graphics.DrawRectangle(new Pen(this.ColorTable.ButtonPressedBorder), rectangle);
                        break;
                    default:
                        e.Graphics.FillRectangle(new SolidBrush(this.ColorTable.ToolStripGradientEnd), rectangle);
                        break;
                }
                //
                switch (pDockBarFloatFormButton.eDockBarFloatFormButtonStyle) 
                {
                    case DockBar.DockBarFloatFormButtonStyle.eContextButton:
                        this.DrawContextButton(e.Graphics, pDockBarFloatFormButton.GlyphRectangle);
                        break;
                    case DockBar.DockBarFloatFormButtonStyle.eCloseButton:
                        this.DrawCloseButton(e.Graphics, pDockBarFloatFormButton.GlyphRectangle);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawContextButton(Graphics g, Rectangle rectangle)
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

                g.FillPath(new SolidBrush(this.ColorTable.GripDark), arrowHeadPath);
            }
        }
        private void DrawCloseButton(Graphics g, Rectangle rectangle)
        {
            rectangle = new Rectangle(rectangle.X + 3, rectangle.Y + 4, 8, 7);
            Pen pen = new Pen(this.ColorTable.GripDark);
            g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
            g.DrawLine(pen, rectangle.Left + 1, rectangle.Top, rectangle.Right, rectangle.Bottom);
            g.DrawLine(pen, rectangle.Right - 1, rectangle.Top, rectangle.Left, rectangle.Bottom);
            g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Left + 1, rectangle.Bottom);
        }
        #endregion

        #region TextBox
        public virtual void OnRenderTextBoxC(ObjectRenderEventArgs e)
        {
            DockBar.ITextBoxItemDB pTextBoxX = e.Object as DockBar.ITextBoxItemDB;
            if (pTextBoxX == null || pTextBoxX.BorderStyle == BorderStyle.None) return;
            //
            Rectangle rectangle = pTextBoxX.FrameRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            if (pTextBoxX.BorderStyle == BorderStyle.Fixed3D)
            {
                switch (pTextBoxX.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        rectangle.Inflate(-1, -1);
                        using (Pen p = new Pen(pTextBoxX.BackColor))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonDisabledBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        rectangle.Inflate(-1, -1);
                        using (Pen p = new Pen(pTextBoxX.BackColor))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    default:
                        using (Pen p = new Pen(pTextBoxX.BackColor))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        rectangle.Inflate(-1, -1);
                        using (Pen p = new Pen(pTextBoxX.BackColor))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                }
            }
            else if (pTextBoxX.BorderStyle == BorderStyle.FixedSingle)
            {
                switch (pTextBoxX.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonDisabledBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                    default:
                        using (Pen p = new Pen(pTextBoxX.BackColor))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        break;
                }
            }
        }
        #endregion

        #region UpDownButtons
        public virtual void OnRenderUpDownButtons(ObjectRenderEventArgs e)
        {
            DockBar.IUpDownButtons pUpDownButtonsX = e.Object as DockBar.IUpDownButtons;
            if (pUpDownButtonsX == null) return;
            //
            switch (pUpDownButtonsX.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (SolidBrush brush = new SolidBrush(this.WinFormColorTable.ButtonSelectedGradientMiddle))
                    {
                        e.Graphics.FillRectangle(brush, pUpDownButtonsX.DisplayRectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    using (SolidBrush brush = new SolidBrush(this.WinFormColorTable.ButtonDisabledGradientMiddle))
                    {
                        e.Graphics.FillRectangle(brush, pUpDownButtonsX.DisplayRectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.StatusStripGradientBegin, this.WinFormColorTable.StatusStripGradientEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, pUpDownButtonsX.DisplayRectangle);
                    }
                    break;
            }
            //
            this.DrawUpDownButtons(e.Graphics, pUpDownButtonsX.eUpButtonState, pUpDownButtonsX.UpButtonRectangle);
            this.DrawUpDownButtons(e.Graphics, pUpDownButtonsX.eDownButtonState, pUpDownButtonsX.DownButtonRectangle);
        }
        private void DrawUpDownButtons(Graphics g, WFNew.BaseItemState eBaseItemState, Rectangle rectangle)
        {
            switch (eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                default:
                    break;
            }
        }
        #endregion

        //
        //
        //

        #region CheckBox
        public virtual void OnRenderCheckBox(ObjectRenderEventArgs e)
        {
            ICheckBoxX pCheckBoxX = e.Object as ICheckBoxX;
            if (pCheckBoxX == null) return;
            switch (pCheckBoxX.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawCheckBoxSelected(e.Graphics, pCheckBoxX, pCheckBoxX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawCheckBoxPressed(e.Graphics, pCheckBoxX, pCheckBoxX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawCheckBoxDisabled(e.Graphics, pCheckBoxX, pCheckBoxX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    this.DrawCheckBoxomal(e.Graphics, pCheckBoxX, pCheckBoxX.CheckRectangle);
                    break;
            }
        }
        private void DrawCheckBoxomal(Graphics g, ICheckBoxX pCheckBoxX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxomalBackgroundOutBegin, this.WinFormColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxomalBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxomalBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxomalBackgroundIntBegin, this.WinFormColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxX.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxDisabled(Graphics g, ICheckBoxX pCheckBoxX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxDisabledBackgroundOutBegin, this.WinFormColorTable.CheckBoxDisabledBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxDisabledBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxDisabledBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxDisabledBackgroundIntBegin, this.WinFormColorTable.CheckBoxDisabledBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxX.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.ArrowDisabled))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ArrowDisabled))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxDisabledOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxPressed(Graphics g, ICheckBoxX pCheckBoxX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxPressedBackgroundOutBegin, this.WinFormColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxPressedBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxPressedBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxPressedBackgroundIntBegin, this.WinFormColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxX.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        private void DrawCheckBoxSelected(Graphics g, ICheckBoxX pCheckBoxX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxSelectedBackgroundOutBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                g.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxSelectedBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundMiddleEnd, 135))
            {
                g.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxSelectedBackgroundIntBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                g.FillRectangle(b, rectangleInt3);
            }
            switch (pCheckBoxX.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            g.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        g.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.6f;
                g.DrawRectangle(p, rectangle);
            }
        }
        #endregion

        #region RadioButton
        public virtual void OnRenderRadioButton(ObjectRenderEventArgs e)
        {
            IRadioButtonX pRadioButtonX = e.Object as IRadioButtonX;
            if (pRadioButtonX == null) return;
            switch (pRadioButtonX.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawRadioButtonSelected(e.Graphics, pRadioButtonX, pRadioButtonX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawRadioButtonPressed(e.Graphics, pRadioButtonX, pRadioButtonX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawRadioButtonDisabled(e.Graphics, pRadioButtonX, pRadioButtonX.CheckRectangle);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    this.DrawRadioButtonNomal(e.Graphics, pRadioButtonX, pRadioButtonX.CheckRectangle);
                    break;
            }
        }
        private void DrawRadioButtonNomal(Graphics g, IRadioButtonX pRadioButtonX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxomalBackgroundOutBegin, this.WinFormColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxomalBackgroundIntBegin, this.WinFormColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonX.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonDisabled(Graphics g, IRadioButtonX pRadioButtonX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxDisabledBackgroundOutBegin, this.WinFormColorTable.CheckBoxDisabledBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxDisabledBackgroundIntBegin, this.WinFormColorTable.CheckBoxDisabledBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonX.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxDisabledOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonPressed(Graphics g, IRadioButtonX pRadioButtonX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxPressedBackgroundOutBegin, this.WinFormColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxPressedBackgroundIntBegin, this.WinFormColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonX.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        private void DrawRadioButtonSelected(Graphics g, IRadioButtonX pRadioButtonX, Rectangle rectangle)
        {
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxSelectedBackgroundOutBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                g.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxSelectedBackgroundIntBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                g.FillEllipse(b, rectangleInt2);
            }
            if (pRadioButtonX.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    g.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.5f;
                g.DrawEllipse(p, rectangle);
            }
        }
        #endregion

        #region Button
        public virtual void OnRenderButton(ObjectRenderEventArgs e)
        {
            IButtonX pButtonX = e.Object as IButtonX;
            if (pButtonX == null) return;
            switch (pButtonX.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawButtonSelected(e.Graphics, pButtonX, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawButtonPressed(e.Graphics, pButtonX, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                    this.DrawButtonDisabled(e.Graphics, pButtonX, e.Bounds, 3);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (!pButtonX.Checked) { this.DrawButtonomal(e.Graphics, pButtonX, e.Bounds, 3); }
                    else { this.DrawButtonChecked(e.Graphics, pButtonX, e.Bounds, 3); }
                    break;
            }
        }
        private void DrawButtonomal(Graphics g, IButtonX pButtonX, Rectangle rectangle, int iSpilt)
        {
            if (!pButtonX.ShowNomalState) return;

            int iLeftTopRadius = pButtonX.LeftTopRadius;
            int iRightTopRadius = pButtonX.RightTopRadius;
            int iLeftBottomRadius = pButtonX.LeftBottomRadius;
            int iRightBottomRadius = pButtonX.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            //
            if (outRectangle.Width <= 0 ||
                 outRectangle.Height <= 0 ) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(outRectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                {
                    g.FillPath(b, path);
                }
                //
                using (Pen p = new Pen(this.WinFormColorTable.ButtonomalBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawButtonChecked(Graphics g, IButtonX pButtonX, Rectangle rectangle, int iSpilt)
        {
            if (!pButtonX.ShowNomalState) return;

            int iLeftTopRadius = pButtonX.LeftTopRadius;
            int iRightTopRadius = pButtonX.RightTopRadius;
            int iLeftBottomRadius = pButtonX.LeftBottomRadius;
            int iRightBottomRadius = pButtonX.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            //
            if (outRectangle.Width <= 0 ||
                 outRectangle.Height <= 0) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(outRectangle, this.WinFormColorTable.ToolStripGradientBegin, this.WinFormColorTable.ToolStripGradientEnd, LinearGradientMode.Vertical))
                {
                    g.FillPath(b, path);
                }
                //
                using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawButtonDisabled(Graphics g, IButtonX pButtonX, Rectangle rectangle, int iSpilt)
        {
            if (!pButtonX.ShowNomalState) return;

            int iLeftTopRadius = pButtonX.LeftTopRadius;
            int iRightTopRadius = pButtonX.RightTopRadius;
            int iLeftBottomRadius = pButtonX.LeftBottomRadius;
            int iRightBottomRadius = pButtonX.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            //
            if (outRectangle.Width <= 0 ||
                 outRectangle.Height <= 0) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(outRectangle, this.WinFormColorTable.ButtonDisabledGradientBegin, this.WinFormColorTable.ButtonDisabledGradientEnd, LinearGradientMode.Vertical))
                {
                    g.FillPath(b, path);
                }
                //
                using (Pen p = new Pen(this.WinFormColorTable.ButtonDisabledBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawButtonPressed(Graphics g, IButtonX pButtonX, Rectangle rectangle, int iSpilt)
        {
            if (!pButtonX.ShowNomalState) return;

            int iLeftTopRadius = pButtonX.LeftTopRadius;
            int iRightTopRadius = pButtonX.RightTopRadius;
            int iLeftBottomRadius = pButtonX.LeftBottomRadius;
            int iRightBottomRadius = pButtonX.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            //
            if (outRectangle.Width <= 0 ||
                 outRectangle.Height <= 0) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(outRectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                {
                    g.FillPath(b, path);
                }
                //
                using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        private void DrawButtonSelected(Graphics g, IButtonX pButtonX, Rectangle rectangle, int iSpilt)
        {
            if (!pButtonX.ShowNomalState) return;

            int iLeftTopRadius = pButtonX.LeftTopRadius;
            int iRightTopRadius = pButtonX.RightTopRadius;
            int iLeftBottomRadius = pButtonX.LeftBottomRadius;
            int iRightBottomRadius = pButtonX.RightBottomRadius;

            Rectangle outRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            //
            if (outRectangle.Width <= 0 ||
                 outRectangle.Height <= 0) return;
            //
            using (GraphicsPath path = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(outRectangle,
                iLeftTopRadius, iRightTopRadius, iLeftBottomRadius, iRightBottomRadius))
            {
                using (LinearGradientBrush b = new LinearGradientBrush(outRectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                {
                    g.FillPath(b, path);
                }
                //
                using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                {
                    g.DrawPath(p, path);
                }
            }
        }
        #endregion

        #region ListBox
        public virtual void OnRenderListBoxNC(ObjectRenderEventArgs e)
        {
            IListBoxX pListBoxX = e.Object as IListBoxX;
            if (pListBoxX == null || pListBoxX.BorderStyle == BorderStyle.None) return;
            //
            using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
            {
                e.Graphics.DrawRectangle(p, pListBoxX.FrameRectangle);
            }
        }
        #endregion

        #region DataGridViewCellItem
        public virtual void OnRenderDataGridViewCellItem(ItemRenderEventArgs e)
        {
            DataGridViewCellPaintingEventArgs cellPaintingInfo = e.Object as DataGridViewCellPaintingEventArgs;
            if (cellPaintingInfo == null) return;
            //
            if (cellPaintingInfo.ColumnIndex == -1 && cellPaintingInfo.RowIndex == -1)
            {
                #region 左上角部分
                Rectangle rectangle = e.Bounds;
                switch (e.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (SolidBrush brush = new SolidBrush(this.WinFormColorTable.ButtonSelectedGradientMiddle))
                        {
                            e.Graphics.FillRectangle(brush, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        //rectangle.Inflate(-1, -1);
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                        break;
                    default: 
                        using (SolidBrush brush = new SolidBrush(this.WinFormColorTable.ButtonomalGradientMiddle))
                        {
                            e.Graphics.FillRectangle(brush, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        //rectangle.Inflate(-1, -1);
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemNomalBorderIn))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                        break;
                }
                //
                rectangle = e.LeftGripBounds;
                int iSize = rectangle.Width > rectangle.Height ? rectangle.Height : rectangle.Width;
                GraphicsPath path = new GraphicsPath();
                iSize = rectangle.Width > rectangle.Height ? rectangle.Height : rectangle.Width;
                path.AddLine(rectangle.Right, rectangle.Bottom - iSize, rectangle.Right, rectangle.Bottom);
                path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Right - iSize, rectangle.Bottom);
                path.AddLine(rectangle.Right - iSize, rectangle.Bottom, rectangle.Right - iSize, rectangle.Bottom);
                path.CloseFigure();
                using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillPath(b, path);
                }
                #endregion
                //
                cellPaintingInfo.PaintContent(cellPaintingInfo.CellBounds);
                cellPaintingInfo.Handled = true;
            }
            else if (cellPaintingInfo.RowIndex == -1)
            {
                #region 列标题
                switch (e.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (e.CheckState == CheckState.Checked)
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        else
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        break;
                }
                #endregion
                //
                cellPaintingInfo.PaintContent(cellPaintingInfo.CellBounds);
                cellPaintingInfo.Handled = true;
            }
            else if (cellPaintingInfo.ColumnIndex == -1)
            {
                #region 行标题
                switch (e.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonSelectedGradientMiddle))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonPressedGradientMiddle))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (e.CheckState == CheckState.Checked)
                        {
                            using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonCheckedGradientMiddle))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        else
                        {
                            using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonomalGradientMiddle))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        break;
                }
                #endregion
                //
                cellPaintingInfo.PaintContent(cellPaintingInfo.CellBounds);
                cellPaintingInfo.Handled = true;
            }
            else
            {
                #region 普通单元格
                switch (e.eBaseItemState) 
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, e.Bounds);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                        {
                            e.Graphics.DrawRectangle(p, e.Bounds);
                        }
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (e.CheckState == CheckState.Checked)
                        {
                            using (LinearGradientBrush b = new LinearGradientBrush(e.Bounds, this.WinFormColorTable.ButtonCheckedGradientEnd, this.WinFormColorTable.ButtonCheckedGradientBegin, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        else
                        {
                            using (SolidBrush b = new SolidBrush(Color.White))
                            {
                                e.Graphics.FillRectangle(b, e.Bounds);
                            }
                            using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
                            {
                                e.Graphics.DrawRectangle(p, e.Bounds);
                            }
                        }
                        break;
                }
                #endregion
                //
                cellPaintingInfo.PaintContent(cellPaintingInfo.CellBounds);
                cellPaintingInfo.Handled = true;
            }
        }
        #endregion

        #region Item
        public virtual void OnRenderItem(ItemRenderEventArgs e)
        {
            if (e.Owner == null) return;
            //
            if (e.Owner.ShowGripRegion)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(e.GripBounds, this.WinFormColorTable.ImageMarginGradientBegin, this.WinFormColorTable.ImageMarginGradientEnd, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(b, e.GripBounds);
                }
                //using (Pen p = new Pen(this.WinFormColorTable.SeparatorDark))
                //{
                //    e.Graphics.DrawLine(p, e.GripBounds.Right - 1, e.GripBounds.Top, e.GripBounds.Right - 1, e.GripBounds.Bottom - 1);
                //}
            }
            //
            this.DrawItemSimply(e);
            //
            switch (e.Owner.eItemDrawStyle)
            {
                case WinForm.ItemDrawStyle.eRadioButton:
                    this.DrawItemRadioButton(e);
                    break;
                case WinForm.ItemDrawStyle.eCheckBox:
                    this.DrawItemCheckBox(e);
                    break;
                //case WinForm.ItemDrawStyle.eSimply:
                //default:
                //    break;
            }
            //
            WinForm.IColorItem pColorItem = e.Object as WinForm.IColorItem;
            if (pColorItem != null)
            {
                using (SolidBrush b = new SolidBrush(pColorItem.Color))
                {
                    if (e.Owner.ShowGripRegion)
                    {
                        e.Graphics.FillRectangle
                            (
                            b,
                            Rectangle.FromLTRB(e.LeftGripBounds.Right + 2, e.GripBounds.Top + 2, e.GripBounds.Right - 3, e.GripBounds.Bottom - 3)
                            );
                    }
                    else
                    {
                        Rectangle rectangle = new Rectangle
                            (
                            e.LeftGripBounds.Right + 3,
                            (e.GripBounds.Top + e.GripBounds.Bottom - 9) / 2,
                            e.GripBounds.Width - e.LeftGripBounds.Width - 5,
                            9
                            );
                        e.Graphics.FillRectangle(b, rectangle);
                        using (Pen p = new Pen(Color.Black))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                    }
                }
            }
        }
        private void DrawItemSimply(ItemRenderEventArgs e)
        {
            Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
            //
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (e.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    //Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                    //using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonSelectedGradientMiddle))
                    //{
                    //    e.Graphics.FillRectangle(b, rectangle);
                    //}
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorder))
                    //{
                    //    e.Graphics.DrawRectangle(p, rectangle);
                    //}
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    e.Graphics.DrawRectangle(p, rectangle);
                    //}
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    e.Graphics.DrawRectangle(p, rectangle);
                    //}
                    break;
                default:
                    if (e.CheckState == CheckState.Checked)
                    {
                        if (e.Owner.eItemDrawStyle == GISShare.Controls.WinForm.ItemDrawStyle.eRadioButton ||
                            e.Owner.eItemDrawStyle == GISShare.Controls.WinForm.ItemDrawStyle.eCheckBox) return;
                        //
                        //Rectangle rectangle2 = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                        //using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ButtonCheckedGradientMiddle))
                        //{
                        //    e.Graphics.FillRectangle(b, rectangle2);
                        //}
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemCheckedBorder))
                        //{
                        //    e.Graphics.DrawRectangle(p, rectangle2);
                        //}
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemCheckedBorderIn))
                        //{
                        //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        //    e.Graphics.DrawRectangle(p, rectangle);
                        //}
                    }
                    else
                    {
                        WinForm.IItem pItem = e.Object as WinForm.IItem;
                        if (pItem != null && pItem.ShowBackColor)
                        {
                            using (SolidBrush b = new SolidBrush(pItem.BackColor))
                            {
                                e.Graphics.FillRectangle(b, e.ItemBounds);
                            }
                        }
                    }
                    break;
            }
        }
        private void DrawItemRadioButton(ItemRenderEventArgs e)
        {
            switch (e.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawItemRadioButtonSelected(e);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawItemRadioButtonPressed(e);
                    break;
                default:
                    this.DrawItemRadioButtonomal(e);
                    break;
            }
        }
        private void DrawItemRadioButtonomal(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.LeftGripBounds.Left + e.LeftGripBounds.Right - iSize) / 2,
                (e.LeftGripBounds.Top + e.LeftGripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxomalBackgroundOutBegin, this.WinFormColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxomalBackgroundIntBegin, this.WinFormColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt2);
            }
            if (e.CheckState == CheckState.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    e.Graphics.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.5f;
                e.Graphics.DrawEllipse(p, rectangle);
            }
        }
        private void DrawItemRadioButtonPressed(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.GripBounds.Left + e.GripBounds.Right - iSize) / 2,
                (e.GripBounds.Top + e.GripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxPressedBackgroundOutBegin, this.WinFormColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxPressedBackgroundIntBegin, this.WinFormColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt2);
            }
            if (e.CheckState == CheckState.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    e.Graphics.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.5f;
                e.Graphics.DrawEllipse(p, rectangle);
            }
        }
        private void DrawItemRadioButtonSelected(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.LeftGripBounds.Left + e.LeftGripBounds.Right - iSize) / 2,
                (e.LeftGripBounds.Top + e.LeftGripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxSelectedBackgroundOutBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxSelectedBackgroundIntBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                e.Graphics.FillEllipse(b, rectangleInt2);
            }
            if (e.CheckState == CheckState.Checked)
            {
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                {
                    e.Graphics.FillEllipse(b, rectangleInt2);
                }
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.5f;
                e.Graphics.DrawEllipse(p, rectangle);
            }
        }
        private void DrawItemCheckBox(ItemRenderEventArgs e)
        {
            switch (e.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    this.DrawItemCheckBoxSelected(e);
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    this.DrawItemCheckBoxPressed(e);
                    break;
                default:
                    this.DrawItemCheckBoxomal(e);
                    break;
            }
        }
        private void DrawItemCheckBoxomal(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.LeftGripBounds.Left + e.LeftGripBounds.Right - iSize) / 2,
                (e.LeftGripBounds.Top + e.LeftGripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            if (rectangleInt3.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxomalBackgroundOutBegin, this.WinFormColorTable.CheckBoxomalBackgroundOutEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxomalBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxomalBackgroundMiddleEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxomalBackgroundIntBegin, this.WinFormColorTable.CheckBoxomalBackgroundIntEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt3);
            }
            switch (e.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        e.Graphics.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxomalOutLine))
            {
                p.Width = 1.6f;
                e.Graphics.DrawRectangle(p, rectangle);
            }
        }
        private void DrawItemCheckBoxPressed(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.LeftGripBounds.Left + e.LeftGripBounds.Right - iSize) / 2,
                (e.LeftGripBounds.Top + e.LeftGripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            if (rectangleInt3.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxomalBackgroundOutBegin, this.WinFormColorTable.CheckBoxPressedBackgroundOutEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxPressedBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxPressedBackgroundMiddleEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxPressedBackgroundIntBegin, this.WinFormColorTable.CheckBoxPressedBackgroundIntEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt3);
            }
            switch (e.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        e.Graphics.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxPressedOutLine))
            {
                p.Width = 1.6f;
                e.Graphics.DrawRectangle(p, rectangle);
            }
        }
        private void DrawItemCheckBoxSelected(ItemRenderEventArgs e)
        {
            int iSize = 11;
            Rectangle rectangle = new Rectangle(
                (e.LeftGripBounds.Left + e.LeftGripBounds.Right - iSize) / 2,
                (e.LeftGripBounds.Top + e.LeftGripBounds.Bottom - iSize) / 2,
                iSize,
                iSize);
            Rectangle rectangleInt = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right, rectangle.Bottom);
            Rectangle rectangleInt2 = Rectangle.FromLTRB(rectangleInt.Left + 1, rectangleInt.Top + 1, rectangleInt.Right - 1, rectangleInt.Bottom - 1);
            Rectangle rectangleInt3 = Rectangle.FromLTRB(rectangleInt2.Left + 1, rectangleInt2.Top + 1, rectangleInt2.Right - 1, rectangleInt2.Bottom - 1);
            if (rectangleInt.Width <= 0 || rectangleInt.Height <= 0) return;
            if (rectangleInt2.Width <= 0 || rectangleInt2.Height <= 0) return;
            if (rectangleInt3.Width <= 0 || rectangleInt2.Height <= 0) return;
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt, this.WinFormColorTable.CheckBoxSelectedBackgroundOutBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundOutEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt2, this.WinFormColorTable.CheckBoxSelectedBackgroundMiddleBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundMiddleEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt2);
            }
            using (LinearGradientBrush b = new LinearGradientBrush(rectangleInt3, this.WinFormColorTable.CheckBoxSelectedBackgroundIntBegin, this.WinFormColorTable.CheckBoxSelectedBackgroundIntEnd, 135))
            {
                e.Graphics.FillRectangle(b, rectangleInt3);
            }
            switch (e.CheckState)
            {
                case CheckState.Checked:
                    using (GraphicsPath path = this.CreateCheckPath(new Rectangle(rectangleInt.X + 1, rectangleInt.Y, rectangleInt.Width, rectangleInt.Height)))
                    {
                        using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                        {
                            p.Width = 2;
                            e.Graphics.DrawPath(p, path);
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.Arrow))
                    {
                        e.Graphics.FillRectangle(b, rectangleInt3);
                    }
                    break;
                default:
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.CheckBoxSelectedOutLine))
            {
                p.Width = 1.6f;
                e.Graphics.DrawRectangle(p, rectangle);
            }
        }
        #endregion

        #region TreeView
        public virtual void OnRenderTreeViewNC(ObjectRenderEventArgs e)
        {
            ITreeViewX pTreeViewX = e.Object as ITreeViewX;
            if (pTreeViewX == null || pTreeViewX.BorderStyle == BorderStyle.None) return;
            //
            using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
            {
                e.Graphics.DrawRectangle(p, pTreeViewX.FrameRectangle);
            }
        }
        #endregion

        #region TreeNodeItem
        public virtual void OnRenderTreeNodeItem(TreeNodeItemRenderEventArgs e)
        {
            ITreeViewX pTreeViewX = e.Owner as ITreeViewX;
            if (pTreeViewX == null) return;
            //
            TreeNode node = e.Object as TreeNode;
            if (node == null) return;
            //
            TitleTreeNodeItem titleTreeNode = node as TitleTreeNodeItem;
            if (titleTreeNode == null)
            {
                if (!node.BackColor.IsEmpty)
                {
                    using (SolidBrush b = new SolidBrush(node.BackColor))
                    {
                        e.Graphics.FillRectangle(b, e.ItemBounds);
                    }
                }
                //
                if (e.Owner.ShowGripRegion)
                {
                    if (e.GripBounds.Width > 0 && e.GripBounds.Height > 0)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(e.GripBounds, this.WinFormColorTable.ImageMarginGradientBegin, this.WinFormColorTable.ImageMarginGradientEnd, LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(b, e.GripBounds);
                        }
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.SeparatorDark))
                    //{
                    //    e.Graphics.DrawLine(p, e.GripBounds.Right - 1, e.GripBounds.Top, e.GripBounds.Right - 1, e.GripBounds.Bottom - 1);
                    //}
                }
                //
                this.DrawTreeNodeItem(e.Graphics, node, e.eBaseItemState, Rectangle.FromLTRB(e.ItemBounds.Left, e.ItemBounds.Top, e.ItemBounds.Right - 1, e.ItemBounds.Bottom - 1));
            }
            else
            {
                Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                if (titleTreeNode.UsingNodeRegionStyle) { rectangle = Rectangle.FromLTRB(e.ItemBounds.Left, e.ItemBounds.Top, e.ItemBounds.Right - 1, e.ItemBounds.Bottom - 1); }                
                Color titleBorder = this.WinFormColorTable.ToolStripBorder;
                Color titleBackgroundBegin = this.WinFormColorTable.ToolStripGradientBegin;
                Color titleBackgroundEnd = this.WinFormColorTable.ToolStripGradientEnd;
                if (titleTreeNode.SystemColor)
                {
                    titleBorder = titleTreeNode.TitleBorder;
                    titleBackgroundBegin = titleTreeNode.TitleBackgroundBegin;
                    titleBackgroundEnd = titleTreeNode.TitleBackgroundEnd;
                }
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, titleBackgroundBegin, titleBackgroundEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                }
                using (Pen p = new Pen(titleBorder))
                {
                    e.Graphics.DrawRectangle(p, rectangle);
                }
                //
                this.DrawTreeNodeItem(e.Graphics, node, e.eBaseItemState, rectangle);
            }
            //
            //
            //
            if (node != null && e.PlusMinusBounds.Left > 0)
            {
                int iSize = 8;
                Rectangle rectangle = new Rectangle(e.PlusMinusBounds.Left + 2, (e.PlusMinusBounds.Top + e.PlusMinusBounds.Bottom - iSize) / 2, iSize, iSize);
                if (pTreeViewX.ShowLines && this.ShowLines(titleTreeNode))
                {
                    using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                    {
                        int iCX = rectangle.Left + rectangle.Width / 2;
                        int iCY = rectangle.Top + rectangle.Height / 2;
                        p.DashStyle = DashStyle.Dot;
                        e.Graphics.DrawLine(p, iCX, rectangle.Top + rectangle.Height / 2, e.LeftGripBounds.Left, iCY);
                        List<int> iNumList = new List<int>();
                        this.GetVLineNum_DG(node, 0, ref iNumList);
                        foreach (int one in iNumList)
                        {
                            e.Graphics.DrawLine(p, iCX - one * pTreeViewX.PlusMinusRegionWidth, node.Bounds.Top + 1, iCX - one * pTreeViewX.PlusMinusRegionWidth, node.Bounds.Bottom);
                        }
                        if (node.Parent != null || node.PrevNode != null) e.Graphics.DrawLine(p, iCX, iCY, iCX, node.Bounds.Top - (pTreeViewX.ItemHeight % 2 == 0 ? 2 : 0));
                        if (node.NextNode != null) e.Graphics.DrawLine(p, iCX, iCY, iCX, node.Bounds.Bottom);
                    }
                }
                //
                if (pTreeViewX.ShowPlusMinus && node.Nodes.Count > 0 && this.ShowPlusMinus(titleTreeNode))
                {
                    if (rectangle.Width > 0 && rectangle.Height > 0)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.GripLight, this.WinFormColorTable.GripDark, 135))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.Arrow))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                        if (node.Nodes.Count > 0)
                        {
                            e.Graphics.DrawLine(p, rectangle.Left + 2, rectangle.Top + rectangle.Height / 2, rectangle.Right - 2, rectangle.Top + rectangle.Height / 2);
                            if (!node.IsExpanded) e.Graphics.DrawLine(p, rectangle.Left + rectangle.Width / 2, rectangle.Top + 2, rectangle.Left + rectangle.Width / 2, rectangle.Bottom - 2);
                        }
                    }
                }
            }
            //
            //
            //
            if (this.ShowStateImageOrCheckBox(titleTreeNode))
            {
                switch (e.Owner.eItemDrawStyle)
                {
                    case WinForm.ItemDrawStyle.eRadioButton:
                        this.DrawItemRadioButton(e);
                        break;
                    case WinForm.ItemDrawStyle.eCheckBox:
                        this.DrawItemCheckBox(e);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawTreeNodeItem(Graphics g, TreeNode node, WFNew.BaseItemState eBaseItemState, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:              
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                default:
                    if (node.IsSelected || node.Checked)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                        {
                            g.DrawRectangle(p, rectangle);
                        }
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemCheckedBorderIn))
                        //{
                        //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        //    g.DrawRectangle(p, rectangle);
                        //}
                    }
                    break;
            }
        }
        private void GetVLineNum_DG(TreeNode node, int iNum, ref List<int> iNumList)
        {
            if (node == null || node.Parent == null) return;
            iNum++;
            if (node.Parent.NextNode != null) iNumList.Add(iNum);
            this.GetVLineNum_DG(node.Parent, iNum, ref iNumList);
        }
        private bool ShowLines(TitleTreeNodeItem titleTreeNode)
        {
            return titleTreeNode == null || titleTreeNode.ShowLines;
        }
        private bool ShowPlusMinus(TitleTreeNodeItem titleTreeNode)
        {
            return titleTreeNode == null || titleTreeNode.ShowPlusMinus;
        }
        private bool ShowStateImageOrCheckBox(TitleTreeNodeItem titleTreeNode)
        {
            return titleTreeNode == null || titleTreeNode.ShowStateImageOrCheckBox;
        }
        #endregion

        #region ListView
        public virtual void OnRenderListViewNC(ObjectRenderEventArgs e)
        {
            IListViewX pListViewX = e.Object as IListViewX;
            if (pListViewX == null || pListViewX.BorderStyle == BorderStyle.None) return;
            //
            using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
            {
                e.Graphics.DrawRectangle(p, pListViewX.FrameRectangle);
            }
        }
        #endregion

        #region ColumnHeaderItem
        public virtual void OnRenderColumnHeaderItem(ItemRenderEventArgs e)
        {
            Rectangle rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom - 1);
            //
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            switch (e.eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                default:
                    if (e.CheckState == CheckState.Checked)
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                    }
                    else
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
                        {
                            e.Graphics.DrawRectangle(p, rectangle);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region ListViewItem
        public virtual void OnRenderListViewItem(ItemRenderEventArgs e)
        {
            ListViewItem item = e.Object as ListViewItem;
            if (item == null) return;
            //
            if (e.Owner.ShowGripRegion)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(e.GripBounds, this.WinFormColorTable.ImageMarginGradientBegin, this.WinFormColorTable.ImageMarginGradientEnd, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(b, e.GripBounds);
                }
                //using (Pen p = new Pen(this.WinFormColorTable.SeparatorDark))
                //{
                //    e.Graphics.DrawLine(p, e.GripBounds.Right - 1, e.GripBounds.Top, e.GripBounds.Right - 1, e.GripBounds.Bottom - 1);
                //}
            }
            //
            if (!item.BackColor.IsEmpty)
            {
                using (SolidBrush b = new SolidBrush(item.BackColor))
                {
                    e.Graphics.FillRectangle(b, e.ItemBounds);
                }
            }
            //
            if (e.Owner.ShowGripRegion)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(e.GripBounds, this.WinFormColorTable.ImageMarginGradientBegin, this.WinFormColorTable.ImageMarginGradientEnd, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(b, e.GripBounds);
                }
                //using (Pen p = new Pen(this.WinFormColorTable.SeparatorDark))
                //{
                //    e.Graphics.DrawLine(p, e.GripBounds.Right - 1, e.GripBounds.Top, e.GripBounds.Right - 1, e.GripBounds.Bottom - 1);
                //}
            }
            //
            this.DrawListViewItem(e.Graphics, item, e.eBaseItemState, Rectangle.FromLTRB(e.ItemBounds.Left, e.ItemBounds.Top, e.ItemBounds.Right - 1, e.ItemBounds.Bottom - 1));
            //
            //
            //
            switch (e.Owner.eItemDrawStyle)
            {
                case WinForm.ItemDrawStyle.eRadioButton:
                    this.DrawItemRadioButton(e);
                    break;
                case WinForm.ItemDrawStyle.eCheckBox:
                    this.DrawItemCheckBox(e);
                    break;
                default:
                    break;
            }
        }
        private void DrawListViewItem(Graphics g, ListViewItem listViewItem, WFNew.BaseItemState eBaseItemState, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (eBaseItemState)
            {
                case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
                    {
                        g.DrawRectangle(p, rectangle);
                    }
                    //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
                    //{
                    //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //    g.DrawRectangle(p, rectangle);
                    //}
                    break;
                default:
                    if (listViewItem.Selected)// || listViewItem.Checked
                    {
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(b, rectangle);
                        }
                        using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
                        {
                            g.DrawRectangle(p, rectangle);
                        }
                        //using (Pen p = new Pen(this.WinFormColorTable.ItemCheckedBorderIn))
                        //{
                        //    rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                        //    g.DrawRectangle(p, rectangle);
                        //}
                    }
                    break;
            }
        }
        #endregion

        #region ListViewSubItem
        public virtual void OnRenderListViewSubItem(ItemRenderEventArgs e)
        {
            ListViewItem.ListViewSubItem item = e.Object as ListViewItem.ListViewSubItem;
            if (item == null) return;
            //
            if (!item.BackColor.IsEmpty)
            {
                using (SolidBrush b = new SolidBrush(item.BackColor))
                {
                    e.Graphics.FillRectangle(b, e.ItemBounds);
                }
            }
        }
        #endregion

        #region TabControl
        public virtual void OnRenderTabControl(ObjectRenderEventArgs e)
        {
            ITabControlX pTabControlX = e.Object as ITabControlX;
            if (pTabControlX == null) return;
            //
            Rectangle rectangle = pTabControlX.FrameRectangle;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            //
            if (pTabControlX.Appearance == TabAppearance.Normal)
            {
                using (GraphicsPath path = pTabControlX.GetOutLineRegionPath())
                {
                    switch (pTabControlX.Alignment)
                    {
                        case TabAlignment.Top:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                        case TabAlignment.Bottom:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                        case TabAlignment.Left:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Horizontal))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                        case TabAlignment.Right:
                            using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Horizontal))
                            {
                                e.Graphics.FillPath(b, path);
                            }
                            break;
                    }
                    using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                }
            }
            else
            {
                switch (pTabControlX.Alignment)
                {
                    case TabAlignment.Top:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                    case TabAlignment.Bottom:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                    case TabAlignment.Left:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                    case TabAlignment.Right:
                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                        }
                        break;
                }
                using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
                {
                    e.Graphics.DrawRectangle(p, rectangle);
                }
            }
        }
        #endregion

        #region TabItem
        public virtual void OnRenderTabItem(ItemRenderEventArgs e)//Normal
        {
            ITabControlX pTabControlX = e.Owner as ITabControlX;
            if (pTabControlX == null) return;
            //
            if (pTabControlX.Appearance == TabAppearance.Normal)
            {
                if (e.CheckState == CheckState.Checked)
                {
                    switch (e.eBaseItemState)
                    {
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                            this.DrawTabItemChecked_N(pTabControlX, e.Graphics, e.Bounds, this.WinFormColorTable.ButtonSelectedBorder); 
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                            this.DrawTabItemChecked_N(pTabControlX, e.Graphics, e.Bounds, this.WinFormColorTable.ButtonPressedBorder); 
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                            this.DrawTabItemChecked_N(pTabControlX, e.Graphics, e.Bounds, this.WinFormColorTable.ButtonDisabledBorder); 
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                        default:
                            this.DrawTabItemChecked_N(pTabControlX, e.Graphics, e.Bounds, this.WinFormColorTable.ToolStripPanelGradientBorder); 
                            break;
                    }
                }
                else
                {
                    switch (e.eBaseItemState)
                    {
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                            this.DrawTabItemSelected_N(pTabControlX, e.Graphics, e.Bounds);
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                            this.DrawTabItemPressed_N(pTabControlX, e.Graphics, e.Bounds);
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                            this.DrawTabItemDisabled_N(pTabControlX, e.Graphics, e.Bounds);
                            break;
                        case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                        default:
                            this.DrawTabItemNormal_N(pTabControlX, e.Graphics, e.Bounds);
                            break;
                    }
                }
            }
            else if (pTabControlX.Appearance == TabAppearance.Buttons)
            {
                switch (e.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        this.DrawTabItemSelected_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        this.DrawTabItemPressed_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        this.DrawTabItemDisabled_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (e.CheckState == CheckState.Checked) this.DrawTabItemChecked_B(pTabControlX, e.Graphics, e.Bounds);
                        else this.DrawTabItemNormal_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                }
            }
            else 
            {
                switch (e.eBaseItemState)
                {
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eHot:
                        this.DrawTabItemSelected_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed:
                        this.DrawTabItemPressed_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled:
                        this.DrawTabItemDisabled_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                    case GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal:
                    default:
                        if (e.CheckState == CheckState.Checked) this.DrawTabItemChecked_B(pTabControlX, e.Graphics, e.Bounds);
                        break;
                }
            }
        }
        public virtual void DrawTabItemChecked_N(ITabControlX pTabControlX, Graphics g, Rectangle rectangle, Color outLineColor)
        {
            Rectangle rectangle2 = pTabControlX.FrameRectangle;
            if (rectangle2.Width <= 0 || rectangle2.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle2, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(outLineColor))
                    {
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Bottom, rectangle.Left - 1, rectangle.Top - 1);
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Right, rectangle.Top - 1);
                        g.DrawLine(p, rectangle.Right, rectangle.Top - 1, rectangle.Right, rectangle.Bottom);
                    }
                    //if (!intLineColor.IsEmpty)
                    //{
                    //    using (Pen p = new Pen(intLineColor))
                    //    {
                    //        g.DrawLine(p, rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    //        g.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Top);
                    //        g.DrawLine(p, rectangle.Right - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    //    }
                    //}
                    break;
                case TabAlignment.Bottom:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle2, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(outLineColor))
                    {
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Left - 1, rectangle.Bottom);
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                        g.DrawLine(p, rectangle.Right, rectangle.Bottom, rectangle.Right, rectangle.Top - 1);
                    }
                    //if (!intLineColor.IsEmpty)
                    //{
                    //    using (Pen p = new Pen(intLineColor))
                    //    {
                    //        g.DrawLine(p, rectangle.Left, rectangle.Top - 1, rectangle.Left, rectangle.Bottom - 1);
                    //        g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    //        g.DrawLine(p, rectangle.Right - 1, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Top - 1);
                    //    }
                    //}
                    break;
                case TabAlignment.Left:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle2, this.WinFormColorTable.ToolStripPanelGradientBegin, this.WinFormColorTable.ToolStripPanelGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(outLineColor))
                    {
                        g.DrawLine(p, rectangle.Right, rectangle.Top - 1, rectangle.Left - 1, rectangle.Top - 1);
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Left - 1, rectangle.Bottom);
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                    }
                    //if (!intLineColor.IsEmpty)
                    //{
                    //    using (Pen p = new Pen(intLineColor))
                    //    {
                    //        g.DrawLine(p, rectangle.Right, rectangle.Top , rectangle.Left, rectangle.Top);
                    //        g.DrawLine(p, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom - 1);
                    //        g.DrawLine(p, rectangle.Left, rectangle.Bottom - 1, rectangle.Right, rectangle.Bottom - 1);
                    //    }
                    //}
                    break;
                case TabAlignment.Right:
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle2, this.WinFormColorTable.ToolStripPanelGradientEnd, this.WinFormColorTable.ToolStripPanelGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    using (Pen p = new Pen(outLineColor))
                    {
                        g.DrawLine(p, rectangle.Left - 1, rectangle.Top - 1, rectangle.Right, rectangle.Top - 1);
                        g.DrawLine(p, rectangle.Right, rectangle.Top - 1, rectangle.Right, rectangle.Bottom);
                        g.DrawLine(p, rectangle.Right, rectangle.Bottom, rectangle.Left - 1, rectangle.Bottom);
                    }
                    //if (!intLineColor.IsEmpty)
                    //{
                    //    using (Pen p = new Pen(intLineColor))
                    //    {
                    //        g.DrawLine(p, rectangle.Left - 1, rectangle.Top, rectangle.Right - 1, rectangle.Top);
                    //        g.DrawLine(p, rectangle.Right - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    //        g.DrawLine(p, rectangle.Right - 1, rectangle.Bottom - 1, rectangle.Left - 1, rectangle.Bottom - 1);
                    //    }
                    //}
                    break;
            }
        }
        public virtual void DrawTabItemNormal_N(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientEnd, this.WinFormColorTable.ButtonomalGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientEnd, this.WinFormColorTable.ButtonomalGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
        }
        public virtual void DrawTabItemDisabled_N(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientBegin, this.WinFormColorTable.ButtonDisabledGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientEnd, this.WinFormColorTable.ButtonDisabledGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientBegin, this.WinFormColorTable.ButtonDisabledGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientEnd, this.WinFormColorTable.ButtonDisabledGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
        }
        public virtual void DrawTabItemSelected_N(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientEnd, this.WinFormColorTable.ButtonSelectedGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientEnd, this.WinFormColorTable.ButtonSelectedGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            //using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientEnd))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
            //rectangle.Inflate(-1, -1);
            using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            rectangle.Inflate(-1, -1);
            using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
        }
        public virtual void DrawTabItemPressed_N(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 1, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientEnd, this.WinFormColorTable.ButtonPressedGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientEnd, this.WinFormColorTable.ButtonPressedGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            //using (Pen p = new Pen(this.WinFormColorTable.ToolStripPanelGradientEnd))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
            //rectangle.Inflate(-1, -1);
            using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        //
        public virtual void DrawTabItemChecked_B(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top - 1, rectangle.Right, rectangle.Bottom - 3);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientEnd, this.WinFormColorTable.ButtonCheckedGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientBegin, this.WinFormColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 2, rectangle.Right - 3, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonCheckedGradientEnd, this.WinFormColorTable.ButtonCheckedGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ButtonCheckedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemCheckedBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        public virtual void DrawTabItemNormal_B(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top - 1, rectangle.Right, rectangle.Bottom - 3);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientEnd, this.WinFormColorTable.ButtonomalGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientBegin, this.WinFormColorTable.ButtonomalGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 2, rectangle.Right - 3, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonomalGradientEnd, this.WinFormColorTable.ButtonomalGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ButtonomalGradientEnd))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemNomalBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        public virtual void DrawTabItemDisabled_B(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientBegin, this.WinFormColorTable.ButtonDisabledGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top - 1, rectangle.Right, rectangle.Bottom - 3);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientEnd, this.WinFormColorTable.ButtonDisabledGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientBegin, this.WinFormColorTable.ButtonDisabledGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 2, rectangle.Right - 3, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonDisabledGradientEnd, this.WinFormColorTable.ButtonDisabledGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ButtonDisabledBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemDisabledBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        public virtual void DrawTabItemSelected_B(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top - 1, rectangle.Right, rectangle.Bottom - 3);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientEnd, this.WinFormColorTable.ButtonSelectedGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientBegin, this.WinFormColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 2, rectangle.Right - 3, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonSelectedGradientEnd, this.WinFormColorTable.ButtonSelectedGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ButtonSelectedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemSelectedBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        public virtual void DrawTabItemPressed_B(ITabControlX pTabControlX, Graphics g, Rectangle rectangle)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            switch (pTabControlX.Alignment)
            {
                case TabAlignment.Top:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Bottom:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top - 1, rectangle.Right, rectangle.Bottom - 3);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientEnd, this.WinFormColorTable.ButtonPressedGradientBegin, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Left:
                    rectangle = Rectangle.FromLTRB(rectangle.Left + 2, rectangle.Top + 2, rectangle.Right, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientBegin, this.WinFormColorTable.ButtonPressedGradientEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
                case TabAlignment.Right:
                    rectangle = Rectangle.FromLTRB(rectangle.Left - 1, rectangle.Top + 2, rectangle.Right - 3, rectangle.Bottom);
                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, this.WinFormColorTable.ButtonPressedGradientEnd, this.WinFormColorTable.ButtonPressedGradientBegin, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(b, rectangle);
                    }
                    break;
            }
            using (Pen p = new Pen(this.WinFormColorTable.ButtonPressedBorder))
            {
                g.DrawRectangle(p, rectangle);
            }
            //rectangle.Inflate(-1, -1);
            //using (Pen p = new Pen(this.WinFormColorTable.ItemPressedBorderIn))
            //{
            //    g.DrawRectangle(p, rectangle);
            //}
        }
        #endregion

        #region Arrow
        public virtual void OnRenderArrow(ArrowRenderEventArgs e)
        {
            Color arrowColor = e.ArrowColor;
            if (e.Enabled) { arrowColor = this.WinFormColorTable.Arrow; }
            else { arrowColor = this.WinFormColorTable.ArrowDisabled; }
            Rectangle rectangle = e.ArrowBounds;
            Point[] points = null;
            switch (e.eArrowStyle)
            {
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToUp:
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top);
                    points[1] = new Point(rectangle.Right, rectangle.Bottom);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToDown:
                    rectangle.X -= 1;
                    rectangle.Width += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
                    points[2] = new Point(rectangle.Left, rectangle.Top);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToLeft:
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Left, rectangle.Top + rectangle.Height / 2);
                    points[1] = new Point(rectangle.Right, rectangle.Top);
                    points[2] = new Point(rectangle.Right, rectangle.Bottom);
                    e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                case GISShare.Controls.WinForm.WFNew.ArrowStyle.eToRight:
                    rectangle.Y -= 1;
                    rectangle.Height += 2;
                    points = new Point[3];
                    points[0] = new Point(rectangle.Right, rectangle.Top + rectangle.Height / 2);
                    points[2] = new Point(rectangle.Left, rectangle.Bottom);
                    points[1] = new Point(rectangle.Left, rectangle.Top);
                    e.Graphics.FillPolygon(new SolidBrush(arrowColor), points);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Text
        public virtual void OnRenderText(TextRenderEventArgs e)
        {
            if (e.Text == null) return;
            //StringFormat stringFormat = new StringFormat();
            //stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            //
            //bool bHaveChinese = System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[\u4e00-\u9fa5]+$ ");
            int iOffset = 2;
            if (System.Text.Encoding.Default.GetByteCount(e.Text) == e.Text.Length) { iOffset = 0; }
            //
            Rectangle cbr = e.TextBounds;
            if (e.Enabled)
            {
                //if (e.HaveShadow)
                //{
                //    cbr.Y += iOffset;
                //    using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ItemTextLight))
                //    {
                //        e.Graphics.DrawString(e.Text, e.Font, b, cbr, e.StringFormat);
                //    }
                //    //
                //    cbr.Y -= 1;
                //}
                cbr.Y += iOffset;
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ItemText))
                {
                    e.Graphics.DrawString(e.Text, e.Font, b, cbr, e.StringFormat);
                }
            }
            else
            {
                cbr.Y += 1;
                using (SolidBrush b = new SolidBrush(this.WinFormColorTable.ItemTextDisabled))
                {
                    e.Graphics.DrawString(e.Text, e.Font, b, cbr, e.StringFormat);
                }
            }
        }

        public virtual void OnRenderColumnHeaderText(TextRenderEventArgs e)
        {
            if (e.Text == null) return;
            //
            using (SolidBrush b = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(e.Text, e.Font, b, e.TextBounds, e.StringFormat);
            }
        }

        public virtual void OnRenderTabText(TextRenderEventArgs e)
        {
            if (e.Text == null) return;
            //
            using (SolidBrush b = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(e.Text, e.Font, b, e.TextBounds, e.StringFormat);
            }
        }
        #endregion

        #region Image
        public virtual void OnRenderImage(ImageRenderEventArgs e)
        {
            if (e.Image == null) return;
            //
            if (e.Enabled) { e.Graphics.DrawImage(e.Image, e.ImageBounds); }
            else { e.Graphics.DrawImage(GISShare.Controls.WinForm.Util.UtilTX.CreateDisabledImage(e.Image), e.ImageBounds); }
        }
        #endregion

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
    }

    //

    public class ItemRenderEventArgs
        : ObjectRenderEventArgs
    {
        private WinForm.IItemOwner m_Owner = null;
        /// <summary>
        /// 其拥有者对象
        /// </summary>
        public WinForm.IItemOwner Owner
        {
            get { return m_Owner; }
        }

        CheckState m_CheckState = CheckState.Unchecked;
        /// <summary>
        /// item 选中状态
        /// </summary>
        public CheckState CheckState
        {
            get { return m_CheckState; }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        /// <summary>
        /// item 的状态
        /// </summary>
        public WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }

        private System.Drawing.Rectangle m_LeftGripBounds;
        /// <summary>
        /// 图片、颜色等 绘制区矩形
        /// </summary>
        public System.Drawing.Rectangle LeftGripBounds
        {
            get { return m_LeftGripBounds; }
        }

        private System.Drawing.Rectangle m_GripBounds;
        /// <summary>
        /// 图片、颜色等 绘制区矩形
        /// </summary>
        public System.Drawing.Rectangle GripBounds
        {
            get { return m_GripBounds; }
        }

        private System.Drawing.Rectangle m_ItemBounds;
        /// <summary>
        /// 除去 GripBounds 的文本部分
        /// </summary>
        public System.Drawing.Rectangle ItemBounds
        {
            get { return m_ItemBounds; }
        }

        public ItemRenderEventArgs(Graphics graphics, object item,
            WinForm.IItemOwner owner, CheckState checkState, WFNew.BaseItemState baseItemState,
            Rectangle leftGripBounds, Rectangle gripBounds, Rectangle itemBounds, Rectangle itemBoundsEx)
            : base(graphics, item, itemBoundsEx)
        {
            this.m_Owner = owner;
            this.m_CheckState = checkState;
            this.m_eBaseItemState = baseItemState;
            this.m_LeftGripBounds = leftGripBounds;
            this.m_GripBounds = gripBounds;
            this.m_ItemBounds = itemBounds;
        }
    }

    public class TreeNodeItemRenderEventArgs
        : ItemRenderEventArgs
    {
        public TreeNodeItemRenderEventArgs(Graphics graphics, object item,
            WinForm.IItemOwner owner, CheckState checkState, WFNew.BaseItemState baseItemState,
            Rectangle plusMinusBounds, Rectangle leftGripBounds, Rectangle gripBounds, Rectangle itemBounds, Rectangle itemBoundsEx)
            : base(graphics, item, owner, checkState, baseItemState, leftGripBounds, gripBounds, itemBounds, itemBoundsEx)
        {
            this.m_PlusMinusBounds = plusMinusBounds;
        }

        Rectangle m_PlusMinusBounds;
        public Rectangle PlusMinusBounds
        {
            get { return m_PlusMinusBounds; }
        }
    }

}

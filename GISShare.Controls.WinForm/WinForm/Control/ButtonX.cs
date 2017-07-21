using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class ButtonX : Button, IButtonX
    {
        [Browsable(true), Description("选中属性改变后触发"), Category("属性已更改")]
        public event EventHandler CheckedChanged;

        public ButtonX()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            //
            base.BackColor = System.Drawing.Color.Transparent;
        }

        #region ILabelX
        private Rectangle m_TextRectangle;
        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public virtual Rectangle TextRectangle
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
        #endregion

        #region IButtonX
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private bool m_ShowNomalState = true;
        [Browsable(true), DefaultValue(true), Description("是否显示正常状态下的状态"), Category("状态")]
        public virtual bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        bool m_Checked = false;
        [Browsable(true), DefaultValue(false), Description("选中"), Category("状态")]
        public virtual bool Checked
        {
            get { return m_Checked; }
            set
            {
                if (m_Checked == value) return;
                m_Checked = value;
                this.OnCheckedChanged(new EventArgs());
                this.Refresh();
            }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            return true;
        }
        protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            return true;
        }
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }

        private bool m_AutoPlanTextRectangle = false;
        [Browsable(true), DefaultValue(true), Description("是否自动规划文本的布局"), Category("布局")]
        public virtual bool AutoPlanTextRectangle
        {
            get { return m_AutoPlanTextRectangle; }
            set { m_AutoPlanTextRectangle = value; }
        }

        private int m_ITSpace = 1;
        [Browsable(true), DefaultValue(1), Description("文本域图片的间距"), Category("布局")]
        public int ITSpace
        {
            get { return m_ITSpace; }
            set { if (value < 0) return; m_ITSpace = value; }
        }

        #region Radius
        private int m_LeftTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左顶部圆角值"), Category("圆角")]
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

        private int m_RightTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右顶部圆角值"), Category("圆角")]
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

        private int m_LeftBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左底部圆角值"), Category("圆角")]
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

        private int m_RightBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右底部圆角值"), Category("圆角")]
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

        private WFNew.ImageSizeStyle m_eImageSizeStyle = WFNew.ImageSizeStyle.eDefault;
        [Browsable(true), DefaultValue(typeof(WFNew.ImageSizeStyle), "eDefault"), Description("图片尺寸的展现方式"), Category("布局")]
        public virtual WFNew.ImageSizeStyle eImageSizeStyle
        {
            get { return m_eImageSizeStyle; }
            set { m_eImageSizeStyle = value; }
        }

        private Size m_ImageSize = new Size(30, 30);
        [Browsable(true), DefaultValue(typeof(Size), "30, 30"), Description("图片尺寸"), Category("布局")]
        public virtual Size ImageSize
        {
            get { return m_ImageSize; }
            set
            {
                if (value.Width <= 0 || value.Height <= 0) return;
                //
                m_ImageSize = value;
            }
        }

        [Browsable(false), Description("图片和文本绘制矩形框"), Category("布局")]
        public virtual Rectangle ITDrawRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                return new Rectangle(
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom
                    );
            }
        }

        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public virtual Rectangle ImageRectangle
        {
            get
            {
                if (this.Image == null) return new Rectangle(0, 0, 0, 0);
                //
                Rectangle rectangle;
                switch (this.eImageSizeStyle)
                {
                    case WFNew.ImageSizeStyle.eStretch:
                        rectangle = this.ITDrawRectangle;
                        break;
                    case WFNew.ImageSizeStyle.eCustomize:
                        rectangle = this.GetImageRectangleCustomize();
                        break;
                    case WFNew.ImageSizeStyle.eSystem:
                        rectangle = this.GetImageRectangleSystem();
                        break;
                    default:
                        rectangle = this.GetImageRectangleDefault();
                        break;
                }
                //
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
        private Rectangle GetImageRectangleDefault()
        {
            Rectangle rectangle = this.ITDrawRectangle;
            //
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.TopCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.Image.Width) / 2,
                        rectangle.Top,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.TopRight:
                    return new Rectangle(rectangle.Right - this.Image.Width,
                        rectangle.Top,
                        this.Image.Width,
                        this.Image.Height);
                //
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(rectangle.Left,
                        (rectangle.Top + rectangle.Bottom - this.Image.Height) / 2,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.MiddleCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.Image.Width) / 2,
                        (rectangle.Top + rectangle.Bottom - this.Image.Height) / 2,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.MiddleRight:
                    return new Rectangle(rectangle.Right - this.Image.Width,
                        (rectangle.Top + rectangle.Bottom - this.Image.Height) / 2,
                        this.Image.Width,
                        this.Image.Height);
                //
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Bottom - this.Image.Height,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.Image.Width) / 2,
                        rectangle.Bottom - this.Image.Height,
                        this.Image.Width,
                        this.Image.Height);
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - this.Image.Width,
                        rectangle.Bottom - this.Image.Height,
                        this.Image.Width,
                        this.Image.Height);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle GetImageRectangleCustomize()
        {
            Rectangle rectangle = this.ITDrawRectangle;
            //
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.TopCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.ImageSize.Width) / 2,
                        rectangle.Top,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.TopRight:
                    return new Rectangle(rectangle.Right - this.ImageSize.Width,
                        rectangle.Top,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                //
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(rectangle.Left,
                        (rectangle.Top + rectangle.Bottom - this.ImageSize.Height) / 2,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.MiddleCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.ImageSize.Width) / 2,
                        (rectangle.Top + rectangle.Bottom - this.ImageSize.Height) / 2,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.MiddleRight:
                    return new Rectangle(rectangle.Right - this.ImageSize.Width,
                        (rectangle.Top + rectangle.Bottom - this.ImageSize.Height) / 2,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                //
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Bottom - this.ImageSize.Height,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - this.ImageSize.Width) / 2,
                        rectangle.Bottom - this.ImageSize.Height,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - this.ImageSize.Width,
                        rectangle.Bottom - this.ImageSize.Height,
                        this.ImageSize.Width,
                        this.ImageSize.Height);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }
        //private Rectangle GetImageRectangleSystem()
        //{
        //    Rectangle rectangle = this.ITDrawRectangle;
        //    //
        //    switch (this.ImageAlign)
        //    {
        //        case ContentAlignment.TopCenter:
        //        case ContentAlignment.TopLeft:
        //        case ContentAlignment.TopRight:
        //            return new Rectangle(rectangle.Left,
        //                rectangle.Top,
        //                rectangle.Width,
        //                rectangle.Width);
        //        //
        //        case ContentAlignment.MiddleLeft:
        //            return new Rectangle(rectangle.Left,
        //                rectangle.Top,
        //                rectangle.Height,
        //                rectangle.Height);
        //        //
        //        case ContentAlignment.MiddleCenter:
        //            return new Rectangle(rectangle.Left,
        //                rectangle.Top,
        //                rectangle.Width,
        //                rectangle.Height);
        //        //
        //        case ContentAlignment.MiddleRight:
        //            return new Rectangle(rectangle.Right - rectangle.Height,
        //                rectangle.Top,
        //                rectangle.Height,
        //                rectangle.Height);
        //        //
        //        case ContentAlignment.BottomCenter:
        //        case ContentAlignment.BottomLeft:
        //        case ContentAlignment.BottomRight:
        //            return new Rectangle(rectangle.Left,
        //                rectangle.Bottom - rectangle.Width,
        //                rectangle.Width,
        //                rectangle.Width);
        //        default:
        //            return new Rectangle(0, 0, 0, 0);
        //    }
        //}
        private Rectangle GetImageRectangleSystem()
        {
            Rectangle rectangle = this.ITDrawRectangle;
            //
            int iWH = rectangle.Width < rectangle.Height ? rectangle.Width : rectangle.Height;
            //
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Top,
                        iWH,
                        iWH);
                case ContentAlignment.TopCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWH) / 2,
                        rectangle.Top,
                        iWH,
                        iWH);
                case ContentAlignment.TopRight:
                    return new Rectangle(rectangle.Right - iWH,
                        rectangle.Top,
                        iWH,
                        iWH);
                //
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(rectangle.Left,
                        (rectangle.Top + rectangle.Bottom - iWH) / 2,
                        iWH,
                        iWH);
                case ContentAlignment.MiddleCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWH) / 2,
                        (rectangle.Top + rectangle.Bottom - iWH) / 2,
                        iWH,
                        iWH);
                case ContentAlignment.MiddleRight:
                    return new Rectangle(rectangle.Right - iWH,
                        (rectangle.Top + rectangle.Bottom - iWH) / 2,
                        iWH,
                        iWH);
                //
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left,
                        rectangle.Bottom - iWH,
                        iWH,
                        iWH);
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + rectangle.Right - iWH) / 2,
                        rectangle.Bottom - iWH,
                        iWH,
                        iWH);
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - iWH,
                        rectangle.Bottom - iWH,
                        iWH,
                        iWH);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }

        public virtual Rectangle ButtonRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        [Browsable(false), Description("按钮实体矩形框"), Category("布局")]
        private Rectangle GetTextRectangle(Graphics g)
        {
            if (!this.AutoPlanTextRectangle ||
                this.Image == null ||
                //this.eDisplayStyle == WFNew.DisplayStyle.eText ||
                this.eImageSizeStyle == WFNew.ImageSizeStyle.eStretch)
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
        #endregion

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.DisplayRectangle.Contains(mevent.Location)) { this.SetBaseItemStateEx(WFNew.BaseItemState.eHot); }
            else { this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal); }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetBaseItemState(WFNew.BaseItemState.eNormal);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetBaseItemState(WFNew.BaseItemState.eHot);
            base.OnMouseEnter(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.m_TextRectangle = this.GetTextRectangle(pevent.Graphics);
            //
            //base.OnPaint(pevent);
            //
            this.OnDraw(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            if (this.Image != null)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage(
                    new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
            }
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, true, this.Text, this.ForeColor, this.Font, this.TextRectangle));
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }

    }
}

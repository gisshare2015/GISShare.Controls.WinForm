using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class ButtonXSkinHelper : ControlSkinHelper, IButtonX
    {
        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        private Button m_HostButton;

        public ButtonXSkinHelper(Button hostButton)
            : base(hostButton)
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            this.m_HostButton = hostButton;
        }

        #region зЂВс
        protected override void RegisterEventHandlers(Control hostControl)
        {
            base.RegisterEventHandlers(hostControl);
            //
            hostControl.MouseDown += new MouseEventHandler(HostControl_MouseDown);
            hostControl.MouseUp += new MouseEventHandler(HostControl_MouseUp);
            hostControl.MouseEnter += new EventHandler(HostControl_MouseEnter);
            hostControl.MouseLeave += new EventHandler(HostControl_MouseLeave);
            hostControl.TextChanged += new EventHandler(HostControl_TextChanged);
        }
        protected override void UnregisterEventHandlers(Control hostControl)
        {
            base.UnregisterEventHandlers(hostControl);
            //
            hostControl.MouseDown -= new MouseEventHandler(HostControl_MouseDown);
            hostControl.MouseUp -= new MouseEventHandler(HostControl_MouseUp);
            hostControl.MouseEnter -= new EventHandler(HostControl_MouseEnter);
            hostControl.MouseLeave -= new EventHandler(HostControl_MouseLeave);
            hostControl.TextChanged -= new EventHandler(HostControl_TextChanged);
        }
        void HostControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
        }
        void HostControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.DisplayRectangle.Contains(e.Location)) { this.SetBaseItemStateEx(WFNew.BaseItemState.eHot); }
            else { this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal); }
        }
        void HostControl_MouseLeave(object sender, EventArgs e)
        {
            this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
        }
        void HostControl_MouseEnter(object sender, EventArgs e)
        {
            this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
        }
        void HostControl_TextChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        #endregion

        #region IButtonX
        public event EventHandler CheckedChanged;

        public Image Image
        {
            get
            {
                if (this.m_HostButton == null || this.m_HostButton.IsDisposed) return null;
                return this.m_HostButton.Image;
            }
        }

        public ContentAlignment ImageAlign
        {
            get
            {
                if (this.m_HostButton == null || this.m_HostButton.IsDisposed) return  ContentAlignment.MiddleLeft;
                return this.m_HostButton.ImageAlign;
            }
        }

        public ContentAlignment TextAlign
        {
            get
            {
                if (this.m_HostButton == null || this.m_HostButton.IsDisposed) return  ContentAlignment.MiddleCenter;
                return this.m_HostButton.TextAlign;
            }
        }

        private bool m_ShowNomalState = true;
        public virtual bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        bool m_Checked = false;
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
        
        private bool m_AutoPlanTextRectangle = false;
        public virtual bool AutoPlanTextRectangle
        {
            get { return m_AutoPlanTextRectangle; }
            set { m_AutoPlanTextRectangle = value; }
        }

        private int m_ITSpace = 1;
        public int ITSpace
        {
            get { return m_ITSpace; }
            set { if (value < 0) return; m_ITSpace = value; }
        }

        #region Radius
        private int m_LeftTopRadius = 6;
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
        public virtual WFNew.ImageSizeStyle eImageSizeStyle
        {
            get { return m_eImageSizeStyle; }
            set { m_eImageSizeStyle = value; }
        }

        private Size m_ImageSize = new Size(30, 30);
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

        private Rectangle m_TextRectangle;
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

        public virtual Rectangle ITDrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom
                    );
            }
        }

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
                return this.DisplayRectangle;
            }
        }

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

        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_PAINT:
                    if (!this.IsUserRefresh) base.WndProc(ref m);
                    //this.WndPaint(true);
                    this.WndPaint(ref m);
                    return;
                    //if (this.WndPaint(true)) return;
                    //break;
            }
            //
            base.WndProc(ref m);
        }
        //private bool WndPaint2(bool bInvalidateBuffer)
        //{
        //    if (this.m_HostButton == null || this.m_HostButton.IsDisposed) return false;

        //    bool bResult = false;

        //    try
        //    {
        //        GISShare.Win32.RECT rectScreen = new GISShare.Win32.RECT();
        //        GISShare.Win32.API.GetWindowRect(this.DependObjectHandle, ref rectScreen);
        //        //
        //        Rectangle rectBounds = Rectangle.FromLTRB(rectScreen.Left, rectScreen.Top, rectScreen.Right, rectScreen.Bottom);

        //        // create graphics handle
        //        IntPtr hdc = GISShare.Win32.API.GetDCEx
        //            (
        //            this.DependObjectHandle,
        //            (IntPtr)0,
        //            (GISShare.Win32.DCXFlags.DCX_CACHE | GISShare.Win32.DCXFlags.DCX_CLIPSIBLINGS | GISShare.Win32.DCXFlags.DCX_WINDOW)
        //            );
        //        Graphics g = Graphics.FromHdc(hdc);

        //        // create new buffered graphics if needed
        //        if (m_BufferedGraphics == null || m_CurrentCacheSize != rectBounds.Size)
        //        {
        //            if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
        //            m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate
        //                (
        //                g, new Rectangle(0, 0, rectBounds.Width, rectBounds.Height)
        //                );
        //            m_CurrentCacheSize = rectBounds.Size;
        //            bInvalidateBuffer = true;
        //        }
        //        //
        //        if (bInvalidateBuffer)
        //        {
        //            m_BufferedGraphics.Graphics.Clear(this.TryGetBackColor());
        //            this.OnPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, rectBounds));
        //            //
        //            bResult = true;
        //        }

        //        // render buffered graphics 
        //        if (m_BufferedGraphics != null) m_BufferedGraphics.Render(g);

        //        // cleanup data
        //        if (hdc != (IntPtr)0)
        //        {
        //            GISShare.Win32.API.ReleaseDC(this.DependObjectHandle, hdc);
        //        }
        //        if (g != null) g.Dispose();
        //    }
        //    catch (Exception)
        //    {
        //        // error drawing
        //        bResult = false;
        //    }

        //    return bResult;
        //}
        private bool WndPaint(ref Message m)
        {
            if (this.m_HostButton == null || this.m_HostButton.IsDisposed) return false;

            bool bResult = false;

            IntPtr iHandle = GISShare.Win32.API.GetDC(m.HWnd);
            try
            {
                Graphics g = Graphics.FromHdc(iHandle);
                //
                if (m_BufferedGraphics == null || this.m_CurrentCacheSize != this.Size)
                {
                    if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate(g, this.DisplayRectangle);
                    m_CurrentCacheSize = this.Size;
                }
                //
                m_BufferedGraphics.Graphics.Clear(this.TryGetBackColor());
                this.OnPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, this.DisplayRectangle));
                bResult = true;
                //
                if (m_BufferedGraphics != null) m_BufferedGraphics.Render(g);
                //
                g.Dispose();
            }
            catch { bResult = false; }
            finally
            {
                GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
            }

            return bResult;
        }
        private void SetRegion(Size size)
        {
            this.m_HostButton.Region = new Region
                (
                GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle
                     (
                     new Rectangle(0, 0, size.Width, size.Height),
                     this.LeftTopRadius,
                     this.RightTopRadius,
                     this.LeftBottomRadius,
                     this.RightBottomRadius
                     )
                );
        }

        //
        protected virtual void OnPaint(PaintEventArgs pevent)
        {
            this.m_TextRectangle = this.GetTextRectangle(pevent.Graphics);
            //
            SolidBrush b = new SolidBrush(Color.Transparent);
            pevent.Graphics.FillRectangle(b, pevent.ClipRectangle);
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
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }
    }
}

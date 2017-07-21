using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GISShare.Controls.WinForm.WFNew
{
    public class RibbonForm : Form, IRibbonForm, IFormEvent
    {
        private const int CTR_NONCLIENT = 7;
        private const int CTR_MINHEIGHT = 300;
        private const int CTR_MINWIDTH = 300;

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        public RibbonForm()
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            base.FormBorderStyle = FormBorderStyle.None;
            //this.MaximizedBounds = SystemInformation.WorkingArea;
            this.MaximizedBounds = new Rectangle
                (
                -SystemInformation.FrameBorderSize.Width,
                -SystemInformation.FrameBorderSize.Height,
                SystemInformation.WorkingArea.Width + 2 * SystemInformation.FrameBorderSize.Width,
                SystemInformation.WorkingArea.Height + 2 * SystemInformation.FrameBorderSize.Height
                );
        }

        #region IForm
        [Browsable(false), Description("标题是否居中显示"), Category("布局")]
        public bool IsMiddleCaptionText
        {
            get { return true; } 
        }

        private bool m_IsActive = false;
        [Browsable(false), Description("是否激活"), Category("状态")]
        public bool IsActive
        {
            get { return m_IsActive; }
        }
        protected bool SetIsActive(bool bIsActive, IntPtr iHWnd)
        {
            if (this.m_IsActive == bIsActive) return false;
            this.m_IsActive = bIsActive;
            if (this.RibbonControl != null) this.RibbonControl.Invalidate(this.RibbonControl.CaptionRectangle);
            this.WndNCPaint(iHWnd);
            return true;
        }

        [Browsable(false), Description("标题区高"), Category("布局")]
        public int CaptionHeight
        {
            get
            {
                return 0;
            }
        }

        [Browsable(false), Description("是否绘制标题ICON"), Category("状态")]
        public bool IsDrawIcon
        {
            get { return this.ShowIcon && this.Icon != null; }
        }

        [Browsable(false), Description("取消非客户区的绘制"), Category("状态")]
        public bool CancelDrawNC
        {
            get
            {
                return this.IsDisposed || (this.IsMdiChild && this.WindowState == FormWindowState.Maximized);
            }
        }

        [Browsable(false), Description("携带系统菜单"), Category("状态")]
        public bool HasMenu
        {
            get { return false; }
        }

        [Browsable(false), Description("窗体外轮廓的外观类型"), Category("状态")]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
        }

        [Browsable(false), Description("框架边缘区的宽度"), Category("布局")]
        public Size FrameBorderSize
        {
            get 
            {
                Size size = SystemInformation.FrameBorderSize;
                //size.Width = size.Width + SystemInformation.HorizontalResizeBorderThickness;
                //size.Height = size.Height + SystemInformation.VerticalResizeBorderThickness;
                return size;
            }
        }

        [Browsable(false), Description("标题区矩形"), Category("布局")]
        public Rectangle CaptionRectangle
        {
            get
            {
                return Rectangle.Empty;
            }
        }

        [Browsable(false), Description("标题区ICON矩形"), Category("布局")]
        public Rectangle CaptionIconRectangle
        {
            get
            {
                return Rectangle.Empty;
            }
        }

        [Browsable(false), Description("标题区文本布局矩形"), Category("布局")]
        public Rectangle CaptionTextRectangle
        {
            get
            {
                return Rectangle.Empty;
            }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.m_CurrentCacheSize.Width - 1, this.m_CurrentCacheSize.Height - 1);
            }
        }

        public virtual void GetRadiusInfo(out int iLeftTopRadius, out  int iRightTopRadius, out int iLeftBottomRadius, out int iRightBottomRadius)
        {
            iLeftTopRadius = 9;
            iRightTopRadius = 9;
            iLeftBottomRadius = 0;
            iRightBottomRadius = 0;
        }
        #endregion

        #region IFormEvent
        [Browsable(true), Description("绘制非客户区触发"), Category("外观")]
        public event PaintEventHandler NCPaint;
        #endregion

        #region IRibbonForm
        private IRibbonControl m_RibbonControl;
        [Browsable(true), Description("依附它的功能区控件"), Category("关联")]
        public IRibbonControl RibbonControl
        {
            get { return m_RibbonControl; }
            set { m_RibbonControl = value; }
        }
        #endregion

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            if (this.Width < CTR_MINWIDTH) this.Width = CTR_MINWIDTH;
            if (this.Height < CTR_MINHEIGHT) this.Height = CTR_MINHEIGHT;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0xC0000;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //case (int)GISShare.Win32.Msgs.WM_SHOWWINDOW:
                //    this.NCPaint(m.HWnd);
                //    break;
                //case (int)GISShare.Win32.Msgs.WM_WINDOWPOSCHANGING:
                //case (int)GISShare.Win32.Msgs.WM_WINDOWPOSCHANGED:
                //case (int)GISShare.Win32.Msgs.WM_SIZE:
                //    base.WndProc(ref m);
                //    this.WndNCPaint(m.HWnd);
                //    return;
                case (int)GISShare.Win32.Msgs.WM_NCPAINT:
                    if (!this.WndNCPaint(m.HWnd)) base.WndProc(ref m);
                    return;
                //case (int)GISShare.Win32.Msgs.WM_ACTIVATEAPP:
                //    base.WndProc(ref m);
                //    this.SetIsActive((int)m.WParam != 0, m.HWnd);
                //    return;//key
                case (int)GISShare.Win32.Msgs.WM_ACTIVATE://
                    if (!this.SetIsActive((int)GISShare.Win32.WindowActiveStyles.WA_ACTIVE == (int)m.WParam || (int)GISShare.Win32.WindowActiveStyles.WA_CLICKACTIVE == (int)m.WParam, m.HWnd)) 
                        base.WndProc(ref m);
                    return;
                case (int)GISShare.Win32.Msgs.WM_NCACTIVATE://
                    base.WndProc(ref m);
                    this.SetIsActive
                        (
                        (int)GISShare.Win32.WindowActiveStyles.WA_ACTIVE == (int)m.WParam || (int)GISShare.Win32.WindowActiveStyles.WA_CLICKACTIVE == (int)m.WParam,
                        m.HWnd
                        );
                    return;//key
                case (int)GISShare.Win32.Msgs.WM_MDIACTIVATE:
                    base.WndProc(ref m);
                    if (m.WParam == this.Handle) this.SetIsActive(false, m.HWnd);
                    else if (m.LParam == this.Handle) this.SetIsActive(true, m.HWnd);
                    return;//key
                default:
                    break;
            }
            //
            base.WndProc(ref m);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Region = new Region(GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle(new Rectangle(0, 0, this.Width, this.Height), 9, 9, 0, 0));
            base.OnSizeChanged(e);
        }

        protected virtual bool WndNCPaint(IntPtr iHWnd)
        {
            try
            {
                Size size = this.FrameBorderSize;
                //
                GISShare.Win32.RECT rectScreen = new GISShare.Win32.RECT();
                GISShare.Win32.API.GetWindowRect(iHWnd, ref rectScreen);
                //
                Rectangle rectBounds = Rectangle.FromLTRB(rectScreen.Left, rectScreen.Top, rectScreen.Right, rectScreen.Bottom);
                rectBounds.Offset(-rectBounds.X, -rectBounds.Y);
                //
                Rectangle rectClip = rectBounds;
                Region region = new Region(rectClip);
                rectClip.Inflate(-size.Width + 1, -size.Height + 1);
                //
                IntPtr iHandle = GISShare.Win32.API.GetDCEx
                    (
                    iHWnd,
                    (IntPtr)0,
                    (GISShare.Win32.DCXFlags.DCX_CACHE | GISShare.Win32.DCXFlags.DCX_CLIPSIBLINGS | GISShare.Win32.DCXFlags.DCX_WINDOW)
                    );
                Graphics g = Graphics.FromHdc(iHandle);
                //
                region.Exclude(rectClip);
                IntPtr hrgn = region.GetHrgn(g);
                GISShare.Win32.API.SelectClipRgn(iHandle, hrgn);
                //
                if (this.m_BufferedGraphics == null || this.m_CurrentCacheSize != rectBounds.Size)
                {
                    if (this.m_BufferedGraphics != null) this.m_BufferedGraphics.Dispose();
                    //
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate
                        (
                        g,
                        new Rectangle(0, 0, rectBounds.Width, rectBounds.Height)
                        );
                    this.m_CurrentCacheSize = rectBounds.Size;
                }
                //
                //
                //
                this.OnNCPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, this.FrameRectangle));
                //
                //
                //
                if (this.m_BufferedGraphics != null) m_BufferedGraphics.Render(g);
                //
                if (iHandle != (IntPtr)0)
                {
                    GISShare.Win32.API.SelectClipRgn(iHandle, (IntPtr)0);
                    GISShare.Win32.API.ReleaseDC(iHWnd, iHandle);
                }
                if (region != null && hrgn != (IntPtr)0) region.ReleaseHrgn(hrgn);
                if (region != null) region.Dispose();
                if (g != null) g.Dispose();
                //
                #region old
                //IntPtr iHandle = GISShare.Win32.API.GetWindowDC(iHWnd);
                //Graphics g = Graphics.FromHdc(iHandle);
                ////
                //GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonFormNC(
                //    new GISShare.Controls.WinForm.ObjectRenderEventArgs(g, this, this.FrameRectangle));
                ////
                //g.Dispose();
                //GISShare.Win32.API.ReleaseDC(iHWnd, iHandle);
                #endregion
            }
            catch { return false; }
            //
            return true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.RibbonControl != null) { this.RibbonControl.Invalidate(this.RibbonControl.CaptionTextRectangle); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //
            this.OnDraw(e);
        }

        //
        protected virtual void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderForm
                (
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, base.DisplayRectangle)
                );
        }

        protected virtual void OnNCPaint(PaintEventArgs e)
        {
            this.OnNCDraw(e);
            //
            if (this.NCPaint != null) { this.NCPaint(this, e); }
        }

        protected virtual void OnNCDraw(System.Windows.Forms.PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNC
                (
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, e.ClipRectangle)
                );
        }

    }
}
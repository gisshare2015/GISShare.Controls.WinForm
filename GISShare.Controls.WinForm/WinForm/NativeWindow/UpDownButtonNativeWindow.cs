using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class UpDownButtonativeWindow : NativeWindow, IDisposable, DockBar.IUpDownButtons
    {
        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        public UpDownButtonativeWindow()
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
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
            //
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            //
            return true;
        }
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }

        public void Invalidate(Rectangle rectangle)
        {
            GISShare.Win32.RECT rect = new GISShare.Win32.RECT();
            rect.Left = rectangle.Left;
            rect.Right = rectangle.Right;
            rect.Top = rectangle.Top;
            rect.Bottom = rectangle.Bottom;
            GISShare.Win32.API.InvalidateRect(this.Handle, rect, true);
        }

        private Size m_UpButtonArrowSize = new Size(3, 3);
        public virtual Size UpButtonArrowSize
        {
            get { return m_UpButtonArrowSize; }
            set
            {
                if (value.Width <= 0 || value.Height <= 0) return;
                //
                m_UpButtonArrowSize = value;
            }
        }

        private Size m_DownButtonArrowSize = new Size(3, 3);
        public virtual Size DownButtonArrowSize
        {
            get { return m_DownButtonArrowSize; }
            set
            {
                if (value.Width <= 0 || value.Height <= 0) return;
                //
                m_DownButtonArrowSize = value;
            }
        }

        #region WinForm.IItem
        public string Name 
        {
            get { return "UpDownButtonativeWindow"; }
            set { }
        }

        public string Text
        {
            get { return "UpDownButtonativeWindow"; }
            set { }
        }
        #endregion

        #region WFNew.IBaseItem
        bool WFNew.IBaseItem.Enabled
        {
            get { return true; }
            set { }
        }
        bool WFNew.IBaseItem.Visible
        {
            get { return true; }
            set { }
        }
        Color WFNew.IBaseItem.ForeColor
        {
            get { return Color.Black; }
            set { }
        }
        Font WFNew.IBaseItem.Font
        {
            get { return new Font("宋体", 9f); }
            set { }
        }
        Padding WFNew.IBaseItem.Padding 
        {
            get { return new Padding(0); }
            set { }
        }
        Size WFNew.IBaseItem.Size
        {
            get { return this.DisplayRectangle.Size; }
            //set { }
        }
        WFNew.BaseItemState WFNew.IBaseItem.eBaseItemState
        {
            get { return GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal; }
        }
        int WFNew.IBaseItem.Width
        {
            get { return this.DisplayRectangle.Width; }
        }
        int WFNew.IBaseItem.Height
        {
            get { return this.DisplayRectangle.Height; }
        }
        Point WFNew.IBaseItem.Location
        {
            get { return this.DisplayRectangle.Location; }
        }
        Point WFNew.IBaseItem.PointToClient(Point point)
        {
            return new Point();
        }
        Point WFNew.IBaseItem.PointToScreen(Point point)
        {
            return new Point();
        }

        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        //[Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private bool m_HaveShadow = true;
        //[Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        //[Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        //[Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
        }

        public Rectangle DisplayRectangle
        {
            get
            {
                GISShare.Win32.RECT rect = new GISShare.Win32.RECT();
                GISShare.Win32.API.GetWindowRect(this.Handle, ref rect);
                return Rectangle.FromLTRB(0, 0, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
        }

        public void Refresh()
        {
            GISShare.Win32.RECT rect = new GISShare.Win32.RECT();
            GISShare.Win32.API.GetWindowRect(this.Handle, ref rect);
            GISShare.Win32.API.InvalidateRect(this.Handle, rect, true);
        }
        #endregion

        #region DockBar.IUpDownButtons
        Orientation m_eOrientation = Orientation.Horizontal;
        public Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        private WFNew.BaseItemState m_eUpButtonState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetUpButtonState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eUpButtonState == baseItemState) return false;
            this.m_eUpButtonState = baseItemState;
            return true;
        }
        protected virtual bool SetUpButtonStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eUpButtonState == baseItemState) return false;
            //
            this.m_eUpButtonState = baseItemState;
            this.Invalidate(this.UpButtonRectangle);
            //
            return true;
        }
        public WFNew.BaseItemState eUpButtonState
        {
            get
            {
                return m_eUpButtonState;
            }
        }

        private WFNew.BaseItemState m_eDownButtonState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetDownButtonState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eDownButtonState == baseItemState) return false;
            this.m_eDownButtonState = baseItemState;
            return true;
        }
        protected virtual bool SetDownButtonStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eDownButtonState == baseItemState) return false;
            //
            this.m_eDownButtonState = baseItemState;
            this.Invalidate(this.DownButtonRectangle);
            //
            return true;
        }
        public WFNew.BaseItemState eDownButtonState
        {
            get
            {
                return m_eDownButtonState;
            }
        }

        public Rectangle UpButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 2 - 1, rectangle.Height - 1);
                    case Orientation.Vertical:
                    default:
                        return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height / 2 - 1);
                }
            }
        }

        public Rectangle DownButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return new Rectangle(rectangle.X + rectangle.Width / 2, rectangle.Y, rectangle.Width / 2 - 1, rectangle.Height - 1);
                    case Orientation.Vertical:
                    default:
                        return new Rectangle(rectangle.X, rectangle.Y + rectangle.Height / 2, rectangle.Width - 1, rectangle.Height / 2 - 1);
                }
            }
        }

        public Rectangle UpButtonArrowRectangle
        {
            get
            {
                Rectangle rectangle = this.UpButtonRectangle;
                return new Rectangle
                    (
                    (rectangle.Left + rectangle.Right - UpButtonArrowSize.Width) / 2 + 1,
                    (rectangle.Top + rectangle.Bottom - UpButtonArrowSize.Height) / 2 + 1,
                    UpButtonArrowSize.Width,
                    UpButtonArrowSize.Height
                    );
            }
        }

        public Rectangle DownButtonArrowRectangle
        {
            get
            {
                Rectangle rectangle = this.DownButtonRectangle;
                return new Rectangle
                    (
                    (rectangle.Left + rectangle.Right - DownButtonArrowSize.Width) / 2,
                    (rectangle.Top + rectangle.Bottom - DownButtonArrowSize.Height) / 2,
                    DownButtonArrowSize.Width,
                    DownButtonArrowSize.Height
                    );
            }
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                    this.WmMouseDown(m);
                    break;
                case (int)GISShare.Win32.Msgs.WM_MOUSEMOVE:
                    this.WmMouseMove(m);
                    break;
                case (int)GISShare.Win32.Msgs.WM_LBUTTONUP:
                    this.WmMouseUp(m);
                    break;
                case (int)GISShare.Win32.Msgs.WM_MOUSELEAVE:
                    this.WmMouseLeave();
                    break;
                case (int)GISShare.Win32.Msgs.WM_PAINT:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCPAINT"));
                    base.WndProc(ref m);
                    this.WmPaint(ref m);
                    return;

            }
            //
            base.WndProc(ref m);
        }
        private void WmMouseDown(Message m)
        {
            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
            //
            if (this.UpButtonRectangle.Contains(point))
            {
                this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
                this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
            }
            else if (this.DownButtonRectangle.Contains(point))
            {
                this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
            }
            else
            {
                this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
            }
            //
            this.SetBaseItemStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
        }
        private void WmMouseMove(Message m)
        {
            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
            //
            if (this.UpButtonRectangle.Contains(point))
            {
                this.SetUpButtonStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
                this.SetDownButtonStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
            }
            else if (this.DownButtonRectangle.Contains(point))
            {
                this.SetUpButtonStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                this.SetDownButtonStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
            }
            else
            {
                this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                //
                this.SetBaseItemStateEx(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
            }
        }
        private void WmMouseUp(Message m)
        {
            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
            //
            if (this.DisplayRectangle.Contains(point))
            {
                if (this.UpButtonRectangle.Contains(point))
                {
                    this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
                    this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                }
                else if (this.DownButtonRectangle.Contains(point))
                {
                    this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                    this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
                }
                //
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            }
            else
            {
                this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                //
                this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
            }
        }
        private void WmMouseLeave()
        {
            this.SetUpButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
            this.SetDownButtonState(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
            this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
        }
        private void WmPaint(ref Message m)
        {
            IntPtr iHandle = GISShare.Win32.API.GetDC(m.HWnd);
            try
            {
                Graphics g = Graphics.FromHdc(iHandle);

                Rectangle rectangle = this.DisplayRectangle;
                if (m_BufferedGraphics == null || this.m_CurrentCacheSize != rectangle.Size)
                {
                    if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate(g, rectangle);
                    this.m_CurrentCacheSize = rectangle.Size;
                }

                this.OnPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, rectangle));

                if (m_BufferedGraphics != null) m_BufferedGraphics.Render(g);

                g.Dispose();
            }
            catch { }
            finally
            {
                GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
            }
        }

        protected virtual void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderUpDownButtons
                   (
                   new ObjectRenderEventArgs(e.Graphics, this, e.ClipRectangle)
                   );
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderArrow
                (
                new GISShare.Controls.WinForm.ArrowRenderEventArgs
                    (
                    e.Graphics,
                    null, true,
                    this.eOrientation == Orientation.Vertical ? WFNew.ArrowStyle.eToUp : GISShare.Controls.WinForm.WFNew.ArrowStyle.eToLeft,
                    Color.Black,
                    this.UpButtonArrowRectangle
                    )
                );
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderArrow
                (
                new GISShare.Controls.WinForm.ArrowRenderEventArgs
                    (
                    e.Graphics,
                    null, true,
                    this.eOrientation == Orientation.Vertical ? WFNew.ArrowStyle.eToDown : GISShare.Controls.WinForm.WFNew.ArrowStyle.eToRight,
                    Color.Black,
                    this.DownButtonArrowRectangle
                    )
                );
        }

        #region IDisposable 成员

        public void Dispose()
        {
            base.ReleaseHandle();
        }

        #endregion
    }
}

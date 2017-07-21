using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class UpDownButtonsSkinHelper : ControlSkinHelper, IUpDownButtons
    {
        private const int CTR_ARROWWIDTH = 3;
        private const int CTR_ARROWHEIGHT = 3;

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        public UpDownButtonsSkinHelper(Control hostControl)
            : base(hostControl)
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
        }

        public override GISShare.Controls.WinForm.WFNew.BaseItemState eBaseItemState
        {
            get
            {
                if (this.DependObject != null)
                {
                    WFNew.IBaseItem pBaseItem = this.DependObject.Parent as WFNew.IBaseItem;
                    if (pBaseItem != null) return pBaseItem.eBaseItemState;
                }
                return base.eBaseItemState;
            }
        }

        #region IUpDownButtons
        public System.Windows.Forms.Orientation eOrientation
        {
            get { return Orientation.Vertical; }
        }

        private WFNew.BaseItemState m_eUpButtonState = WFNew.BaseItemState.eNormal;
        public WFNew.BaseItemState eUpButtonState
        {
            get
            {
                return m_eUpButtonState;
            }
        }

        private WFNew.BaseItemState m_eDownButtonState = WFNew.BaseItemState.eNormal;
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
                return new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, (rectangle.Height - 1) / 2 - 1);
            }
        }

        public Rectangle DownButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X + 1, rectangle.Y + (rectangle.Height - 1) / 2, rectangle.Width - 3, (rectangle.Height - 1) / 2 - 1);
            }
        }

        public Rectangle UpButtonArrowRectangle
        {
            get
            {
                Rectangle rectangle = this.UpButtonRectangle;
                return new Rectangle
                    (
                    (rectangle.Left + rectangle.Right - CTR_ARROWWIDTH - 1) / 2 + 1,
                    (rectangle.Top + rectangle.Bottom - CTR_ARROWHEIGHT - 1) / 2, 
                    CTR_ARROWWIDTH + 1, 
                    CTR_ARROWHEIGHT + 1
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
                    (rectangle.Left + rectangle.Right - CTR_ARROWWIDTH) / 2 + 1,
                    (rectangle.Top + rectangle.Bottom - CTR_ARROWHEIGHT) / 2 + 1,
                    CTR_ARROWWIDTH,
                    CTR_ARROWHEIGHT
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
                    if (!this.IsUserRefresh) base.WndProc(ref m);
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
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
            }
            else if (this.DownButtonRectangle.Contains(point))
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed;
                this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
            }
            //else if (this.DisplayRectangle.Contains(point))
            //{
            //    this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            //    this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            //    this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
            //}
            else
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
            }
        }
        private void WmMouseMove(Message m)
        {
            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam); 
            //
            if (this.UpButtonRectangle.Contains(point))
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            }
            else if (this.DownButtonRectangle.Contains(point))
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            }
            //else if (this.DisplayRectangle.Contains(point))
            //{
            //    this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            //    this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            //    this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            //}
            else
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
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
                    this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                    this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                }
                else if (this.DownButtonRectangle.Contains(point))
                {
                    this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                    this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                }
                //
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            }
            else
            {
                this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
            }
        }
        private void WmMouseLeave()
        {
            this.m_eUpButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            this.m_eDownButtonState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
        }
        private void WmPaint(ref Message m)
        {
            //IntPtr iHandle = GISShare.Win32.API.GetDC(m.HWnd);
            //try
            //{
            //    Graphics g = Graphics.FromHdc(iHandle);
            //    //
            //    this.OnPaint(new PaintEventArgs(g, this.DisplayRectangle));
            //    //
            //    g.Dispose();
            //}
            //catch { }
            //finally
            //{
            //    GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
            //}

            IntPtr iHandle = GISShare.Win32.API.GetDC(m.HWnd);
            try
            {
                Graphics g = Graphics.FromHdc(iHandle);

                Rectangle rectangle = this.DisplayRectangle;
                if (m_BufferedGraphics == null || this.m_CurrentCacheSize != this.Size)
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
                   new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                   ); 
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderArrow(
                 new GISShare.Controls.WinForm.ArrowRenderEventArgs(e.Graphics, this, this.Enabled, WFNew.ArrowStyle.eToUp, this.ForeColor, this.UpButtonArrowRectangle));
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderArrow(
                 new GISShare.Controls.WinForm.ArrowRenderEventArgs(e.Graphics, this, this.Enabled, WFNew.ArrowStyle.eToDown, this.ForeColor, this.DownButtonArrowRectangle));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class RadioButtonXSkinHelper : ControlSkinHelper, IRadioButtonX
    {
        private const int CRT_CHECKSIZE = 11;
        private const int CRT_CHECKSPACE = 3;

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        private RadioButton m_HostRadioButton;

        public RadioButtonXSkinHelper(RadioButton hostRadioButton)
            : base(hostRadioButton)
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            this.m_HostRadioButton = hostRadioButton;
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

        #region IRadioButtonX
        int m_VOffset = 0;
        public int VOffset
        {
            get { return m_VOffset; }
            set { m_VOffset = value; }
        }

        public bool Checked
        {
            get
            {
                if (this.m_HostRadioButton == null || this.m_HostRadioButton.IsDisposed) return false;
                return this.m_HostRadioButton.Checked;
            }
        }

        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(this.CheckRectangle.Right + CRT_CHECKSPACE, rectangle.Top + this.VOffset, rectangle.Right, rectangle.Bottom);
            }
        }

        public virtual Rectangle CheckRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top + (rectangle.Height - this.Padding.Top - this.Padding.Bottom - CRT_CHECKSIZE) / 2,
                    CRT_CHECKSIZE,
                    CRT_CHECKSIZE
                    );
            }
        }
        #endregion

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
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
        //private bool WndPaint(bool bInvalidateBuffer)
        //{
        //    if (this.m_HostRadioButton == null || this.m_HostRadioButton.IsDisposed) return false;

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
            if (this.m_HostRadioButton == null || this.m_HostRadioButton.IsDisposed) return false;

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

        protected virtual void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderRadioButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, true, this.Text, this.ForeColor, this.Font, this.TextRectangle));
        }
    }
}

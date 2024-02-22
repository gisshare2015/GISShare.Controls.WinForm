using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class TabControlXSkinHelper : ControlSkinHelper, ITabControlX
    {
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_OVERFLOWREGIONWIDTH = 28;
        private const string UPDOWN32CLASSNAME = "msctls_updown32";

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        UpDownButtonativeWindow m_UpDownButtonativeWindow;

        private System.Windows.Forms.TabControl m_HostTabControl;

        public TabControlXSkinHelper(System.Windows.Forms.TabControl hostTabControl)
            : base(hostTabControl)
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            this.m_HostTabControl = hostTabControl;
            //
            this.m_HostTabControl.ControlAdded += new ControlEventHandler(m_HostTabControl_ControlAdded);
            this.m_HostTabControl.SizeChanged += new EventHandler(m_HostTabControl_SizeChanged);
            this.m_HostTabControl.Disposed += new EventHandler(m_HostTabControl_Disposed);
            //
            this.m_HostTabControl.MouseDown += new MouseEventHandler(m_HostTabControl_MouseDown);
            this.m_HostTabControl.MouseUp += new MouseEventHandler(m_HostTabControl_MouseUp);
            this.m_HostTabControl.MouseMove += new MouseEventHandler(m_HostTabControl_MouseMove);
            this.m_HostTabControl.MouseLeave += new EventHandler(m_HostTabControl_MouseLeave);
        }
        void m_HostTabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            this.SetUpDownButtonativeWindow();
        }
        void m_HostTabControl_SizeChanged(object sender, EventArgs e)
        {
            this.SetUpDownButtonativeWindow();
        }
        void m_HostTabControl_Disposed(object sender, EventArgs e)
        {
            this.ReleaseUpDownButtonativeWindow();
        }
        private void SetUpDownButtonativeWindow()
        {
            if (this.m_UpDownButtonativeWindow == null)
            {
                IntPtr upDownButtonHandle = this.GetUpDownButtonIntPtr();
                if (upDownButtonHandle != IntPtr.Zero)
                {
                    this.m_UpDownButtonativeWindow = new UpDownButtonativeWindow();
                    this.m_UpDownButtonativeWindow.UpButtonArrowSize = new Size(4, 6);
                    this.m_UpDownButtonativeWindow.DownButtonArrowSize = new Size(4, 6);
                    this.m_UpDownButtonativeWindow.AssignHandle(upDownButtonHandle);
                }
            }
        }
        private void ReleaseUpDownButtonativeWindow()
        {
            if (this.m_UpDownButtonativeWindow != null)
            {
                IntPtr upDownButtonHandle = this.GetUpDownButtonIntPtr();
                if (upDownButtonHandle != IntPtr.Zero)
                {
                    this.m_UpDownButtonativeWindow.Dispose();
                    this.m_UpDownButtonativeWindow = null;
                }
            }
        }
        private IntPtr GetUpDownButtonIntPtr()
        {
            if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return IntPtr.Zero;
            return GISShare.Win32.API.FindWindowEx(this.m_HostTabControl.Handle, IntPtr.Zero, UPDOWN32CLASSNAME, null);
        }
        //
        private bool m_IsMouseDown = false;
        private void SetIsMouseDown(bool bValue)
        {
            if (this.m_IsMouseDown == bValue) return;
            //
            this.m_IsMouseDown = bValue;
        }
        void m_HostTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetIsMouseDown(true);
        }
        void m_HostTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetIsMouseDown(false);
        }
        private int m_MouseLoctionTabItemIndex = -1;
        void m_HostTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AutoMouseMoveSeleced)
            {
                if (this.m_MouseLoctionTabItemIndex >= 0)
                {
                    Rectangle rectangle = this.GetTabRect(this.m_MouseLoctionTabItemIndex);
                    if (!rectangle.Contains(e.Location))
                    {
                        rectangle.Inflate(1, 1);
                        this.Invalidate(rectangle);
                        for (int i = 0; i < this.TabPages.Count; i++)
                        {
                            rectangle = this.GetTabRect(i);
                            if (rectangle.Contains(e.Location))
                            {
                                this.m_MouseLoctionTabItemIndex = i;
                                rectangle.Inflate(1, 1);
                                this.Invalidate(rectangle);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Rectangle rectangle;
                    for (int i = 0; i < this.TabPages.Count; i++)
                    {
                        rectangle = this.GetTabRect(i);
                        if (rectangle.Contains(e.Location))
                        {
                            this.m_MouseLoctionTabItemIndex = i;
                            rectangle.Inflate(1, 1);
                            this.Invalidate(rectangle);
                            break;
                        }
                    }
                }
            }
        }
        void m_HostTabControl_MouseLeave(object sender, EventArgs e)
        {
            if (this.AutoMouseMoveSeleced && this.m_MouseLoctionTabItemIndex >= 0)
            {
                Rectangle rectangle = this.GetTabRect(this.m_MouseLoctionTabItemIndex);
                rectangle.Inflate(1, 1);
                this.Invalidate(rectangle);
            }
        }

        #region зЂВс
        protected override void UnregisterEventHandlers(Control hostControl)
        {
            base.UnregisterEventHandlers(hostControl);
            //
            if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return;
            //
            this.m_HostTabControl.ControlAdded -= new ControlEventHandler(m_HostTabControl_ControlAdded);
            this.m_HostTabControl.SizeChanged -= new EventHandler(m_HostTabControl_SizeChanged);
            this.m_HostTabControl.Disposed -= new EventHandler(m_HostTabControl_Disposed);
            //
            this.m_HostTabControl.MouseDown -= new MouseEventHandler(m_HostTabControl_MouseDown);
            this.m_HostTabControl.MouseUp -= new MouseEventHandler(m_HostTabControl_MouseUp);
            this.m_HostTabControl.MouseMove -= new MouseEventHandler(m_HostTabControl_MouseMove);
            this.m_HostTabControl.MouseLeave -= new EventHandler(m_HostTabControl_MouseLeave);
        }
        #endregion

        #region IOwner
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        public bool ShowGripRegion
        {
            get
            {
                return false;
            }
        }

        public int ColorRegionWidth
        {
            get { return CTR_COLORREGIONWIDTH; }
        }

        public int ImageGripRegionWidth
        {
            get
            {
                if (this.ImageList != null) return this.ImageList.ImageSize.Width;
                return 0;
            }
        }

        public int LeftGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get
            {
                if (this.ImageList != null) return GISShare.Controls.WinForm.ItemDrawStyle.eImageLabel;
                return GISShare.Controls.WinForm.ItemDrawStyle.eSimply;
            }
        }
        #endregion

        #region IFrameControlItem
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        public BorderStyle BorderStyle
        {
            get
            {
                return BorderStyle.None;
            }
        }
        #endregion

        #region ITabControlX
        int m_ImageSpace = 0;
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        public bool AutoMouseMoveSeleced
        {
            get
            {
                return (this.Appearance == TabAppearance.Normal && (this.Alignment == TabAlignment.Top || this.Alignment == TabAlignment.Bottom)) ? false : true;
            }
        }

        public GraphicsPath GetOutLineRegionPath()
        {
            GraphicsPath p = new GraphicsPath();
            //
            if (this.Appearance != TabAppearance.Normal) { p.AddRectangle(this.FrameRectangle); return p; }
            //
            Rectangle rectangle = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
            //
            if (this.SelectedIndex < 0) { p.AddRectangle(rectangle); return p; }
            //
            Rectangle rectangle2 = this.GetTabRect(this.SelectedIndex);
            //
            switch (this.Alignment)
            {
                case TabAlignment.Bottom:
                    p.AddLine(rectangle.Left, rectangle2.Top - 1, rectangle2.Left - 1, rectangle2.Top - 1);
                    //
                    p.AddLine(rectangle2.Left - 1, rectangle2.Top - 1, rectangle2.Left - 1, rectangle2.Bottom);
                    p.AddLine(rectangle2.Left - 1, rectangle2.Bottom, rectangle2.Right, rectangle2.Bottom);
                    p.AddLine(rectangle2.Right, rectangle2.Bottom, rectangle2.Right, rectangle2.Top - 1);
                    //
                    p.AddLine(rectangle2.Right, rectangle2.Top - 1, rectangle.Right, rectangle2.Top - 1);
                    //
                    p.AddLine(rectangle.Right, rectangle2.Top - 1, rectangle.Right, rectangle.Top);
                    p.AddLine(rectangle.Right, rectangle.Top, rectangle.Left, rectangle.Top);
                    p.AddLine(rectangle.Left, rectangle.Top, rectangle.Left, rectangle2.Top - 1);
                    p.CloseFigure();
                    break;
                case TabAlignment.Left:
                    p.AddLine(rectangle2.Right, rectangle.Top, rectangle2.Right, rectangle2.Top - 1);
                    //
                    p.AddLine(rectangle2.Right, rectangle2.Top - 1, rectangle2.Left - 1, rectangle2.Top - 1);
                    p.AddLine(rectangle2.Left - 1, rectangle2.Top - 1, rectangle2.Left - 1, rectangle2.Bottom);
                    p.AddLine(rectangle2.Left - 1, rectangle2.Bottom, rectangle2.Right, rectangle2.Bottom);
                    //
                    p.AddLine(rectangle2.Right, rectangle2.Bottom, rectangle2.Right, rectangle.Bottom);
                    //
                    p.AddLine(rectangle2.Right, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                    p.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Right, rectangle.Top);
                    p.AddLine(rectangle.Right, rectangle.Top, rectangle2.Right, rectangle.Top);
                    p.CloseFigure();
                    break;
                case TabAlignment.Right:
                    p.AddLine(rectangle2.Left - 1, rectangle.Top, rectangle2.Left - 1, rectangle2.Top - 1);
                    //
                    p.AddLine(rectangle2.Left - 1, rectangle2.Top - 1, rectangle2.Right, rectangle2.Top - 1);
                    p.AddLine(rectangle2.Right, rectangle2.Top - 1, rectangle2.Right, rectangle2.Bottom);
                    p.AddLine(rectangle2.Right, rectangle2.Bottom, rectangle2.Left - 1, rectangle2.Bottom);
                    //
                    p.AddLine(rectangle2.Left - 1, rectangle2.Bottom, rectangle2.Left - 1, rectangle.Bottom);
                    //
                    p.AddLine(rectangle2.Left - 1, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    p.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    p.AddLine(rectangle.Left, rectangle.Top, rectangle2.Left - 1, rectangle.Top);
                    p.CloseFigure();
                    break;
                case TabAlignment.Top:
                default:
                    p.AddLine(rectangle.Left, rectangle2.Bottom, rectangle2.Left - 1, rectangle2.Bottom);
                    //
                    p.AddLine(rectangle2.Left - 1, rectangle2.Bottom, rectangle2.Left - 1, rectangle2.Top - 1);
                    p.AddLine(rectangle2.Left - 1, rectangle2.Top - 1, rectangle2.Right, rectangle2.Top - 1);
                    p.AddLine(rectangle2.Right, rectangle2.Top - 1, rectangle2.Right, rectangle2.Bottom);
                    //
                    p.AddLine(rectangle2.Right, rectangle2.Bottom, rectangle.Right, rectangle2.Bottom);
                    //
                    p.AddLine(rectangle.Right, rectangle2.Bottom, rectangle.Right, rectangle.Bottom);
                    p.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    p.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle2.Bottom);
                    p.CloseFigure();
                    break;
            }

            return p;
        }

        //

        public TabAlignment Alignment
        {
            get
            {
                if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return TabAlignment.Top;
                return this.m_HostTabControl.Alignment;
            }
        }

        public TabAppearance Appearance
        {
            get
            {
                if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return TabAppearance.Normal;
                return this.m_HostTabControl.Appearance;
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return -1;
                return this.m_HostTabControl.SelectedIndex;
            }
        }

        public ImageList ImageList
        {
            get
            {
                if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return null;
                return this.m_HostTabControl.ImageList;
            }
        }

        public Rectangle GetTabRect(int index)
        {
            if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return new Rectangle();
            return this.m_HostTabControl.GetTabRect(index);
        }
        #endregion

        public System.Windows.Forms.TabControl.TabPageCollection TabPages
        {
            get
            {
                if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return null;
                return this.m_HostTabControl.TabPages;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_PAINT:
                    if (!this.IsUserRefresh) base.WndProc(ref m);
                    this.WndPaint(ref m);
                    return;
            }
            //
            base.WndProc(ref m);
        }
        private bool WndPaint(ref Message m)
        {
            if (this.m_HostTabControl == null || this.m_HostTabControl.IsDisposed) return false;

            bool bResult = false;

            IntPtr iHandle = GISShare.Win32.API.GetDC(m.HWnd);
            try
            {
                Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
                //
                Graphics g = Graphics.FromHdc(iHandle);
                Region region = new Region(rectangle);
                region.Exclude(this.DisplayRectangle);
                g.Clip = region;
                //
                if (m_BufferedGraphics == null || this.m_CurrentCacheSize != this.Size)
                {
                    if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate(g, rectangle);
                    m_CurrentCacheSize = this.Size;
                }
                //
                m_BufferedGraphics.Graphics.Clear(this.TryGetBackColor());
                this.OnPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, rectangle));
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
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTabControl(new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            if (this.TabPages == null) return;
            //
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                if (this.SelectedIndex != i) this.DrawTabPage(this.TabPages[i], CheckState.Unchecked, this.GetTabRect(i), e.Graphics);
            }
            //
            if (this.SelectedIndex >= 0) this.DrawTabPage(this.TabPages[this.SelectedIndex], CheckState.Checked, this.GetTabRect(this.SelectedIndex), e.Graphics);
        }
        private void DrawTabPage(TabPage tabPage, CheckState eCheckState, Rectangle rectangle, Graphics g)
        {
            WFNew.BaseItemState eBaseItemState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            if (rectangle.Contains(this.PointToClient(Form.MousePosition)))
            {
                eBaseItemState = this.m_IsMouseDown ? WFNew.BaseItemState.ePressed : GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
            }
            //
            Image image = this.GetTabItemImage(tabPage);
            //
            Size size = g.MeasureString(tabPage.Text, tabPage.Font == null ? this.Font : tabPage.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            //
            Size imageSize = image == null ? new Size(0, 0) : this.ImageList.ImageSize;
            //
            Rectangle rectangleIT = new Rectangle
                (
                (rectangle.Left + rectangle.Right - imageSize.Width - CTR_IMAGEGRIPSEPARATORWIDTH - size.Width) / 2,
                (rectangle.Top + rectangle.Bottom - size.Height) / 2 + 2,
                imageSize.Width + CTR_IMAGEGRIPSEPARATORWIDTH + size.Width,
                size.Height
                );
            Rectangle rectangleI = new Rectangle(rectangleIT.Left, (rectangle.Top + rectangle.Bottom - imageSize.Height) / 2, imageSize.Width, imageSize.Height);
            Rectangle rectangleT = Rectangle.FromLTRB(rectangleIT.Right - size.Width, rectangleIT.Top, rectangleIT.Right, rectangleIT.Bottom);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            switch (this.Alignment)
            {
                case TabAlignment.Bottom:
                    rectangleIT = new Rectangle
                        (
                        (rectangle.Left + rectangle.Right - imageSize.Width - CTR_IMAGEGRIPSEPARATORWIDTH - size.Width) / 2,
                        (rectangle.Top + rectangle.Bottom - size.Height) / 2 + 1,
                        imageSize.Width + CTR_IMAGEGRIPSEPARATORWIDTH + size.Width,
                        size.Height
                        );
                    rectangleI = new Rectangle(rectangleIT.Left, (rectangle.Top + rectangle.Bottom - imageSize.Height) / 2, imageSize.Width, imageSize.Height);
                    rectangleT = Rectangle.FromLTRB(rectangleIT.Right - size.Width, rectangleIT.Top, rectangleIT.Right, rectangleIT.Bottom);
                    break;
                //
                case TabAlignment.Left:
                    rectangleIT = new Rectangle
                        (
                        (rectangle.Left + rectangle.Right - size.Height) / 2 + 1,
                        (rectangle.Top + rectangle.Bottom - imageSize.Height - CTR_IMAGEGRIPSEPARATORWIDTH - size.Width) / 2,
                        size.Height,
                        imageSize.Height + CTR_IMAGEGRIPSEPARATORWIDTH + size.Width
                        );
                    rectangleI = new Rectangle((rectangle.Left + rectangle.Right - imageSize.Width) / 2, rectangleIT.Top, imageSize.Width, imageSize.Height);
                    rectangleT = Rectangle.FromLTRB(rectangleIT.Left, rectangleIT.Bottom - size.Width, rectangleIT.Right, rectangleIT.Bottom);
                    stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                    break;
                case TabAlignment.Right:
                    rectangleIT = new Rectangle
                        (
                        (rectangle.Left + rectangle.Right - size.Height) / 2 - 1,
                        (rectangle.Top + rectangle.Bottom - imageSize.Height - CTR_IMAGEGRIPSEPARATORWIDTH - size.Width) / 2,
                        size.Height,
                        imageSize.Height + CTR_IMAGEGRIPSEPARATORWIDTH + size.Width
                        );
                    rectangleI = new Rectangle((rectangle.Left + rectangle.Right - imageSize.Width) / 2, rectangleIT.Top, imageSize.Width, imageSize.Width);
                    rectangleT = Rectangle.FromLTRB(rectangleIT.Left, rectangleIT.Bottom - size.Width, rectangleIT.Right, rectangleIT.Bottom);
                    stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                    break;
            }
            //
            //
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTabItem
                (
                new ItemRenderEventArgs
                    (
                    g,
                    tabPage,
                    this,
                    eCheckState,
                    eBaseItemState,
                    rectangle,
                    rectangle,
                    rectangle,
                    rectangle
                    )
                );
            //
            //
            //
            if (image != null)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                    (
                    new GISShare.Controls.WinForm.ImageRenderEventArgs
                        (
                        g,
                        this,
                        this.Enabled,
                        image,
                        Rectangle.FromLTRB(rectangleI.Left + this.ImageSpace, rectangleI.Top + this.ImageSpace, rectangleI.Right - this.ImageSpace, rectangleI.Bottom - this.ImageSpace)
                        )
                     );
            }
            //
            //
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTabText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    g,
                    this,
                    this.Enabled,
                    false,
                    tabPage.Text,
                    false,
                    tabPage.ForeColor,
                    tabPage.ForeColor,
                    tabPage.Font,
                    rectangleT,
                    stringFormat
                    )
                );
        }
        private Image GetTabItemImage(TabPage item)//ImageList
        {
            if (this.ImageList == null) return null;
            //
            if (item.ImageIndex >= 0 && item.ImageIndex < this.ImageList.Images.Count) return this.ImageList.Images[item.ImageIndex];
            if (this.ImageList.Images.Keys.Contains(item.ImageKey)) return this.ImageList.Images[item.ImageKey];
            //
            return null;
        }


    }
}

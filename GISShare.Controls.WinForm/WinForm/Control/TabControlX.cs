using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm
{
    public class TabControlX : System.Windows.Forms.TabControl, ITabControlX
    {
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_OVERFLOWREGIONWIDTH = 28;
        private const string UPDOWN32CLASSNAME = "msctls_updown32";

        UpDownButtonativeWindow m_UpDownButtonativeWindow;

        public TabControlX()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            this.SetUpDownButtonativeWindow();
            //
            base.OnControlAdded(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.SetUpDownButtonativeWindow();
            //
            base.OnSizeChanged(e);
        }

        protected override void DestroyHandle()
        {
            this.ReleaseUpDownButtonativeWindow();
            //
            base.DestroyHandle();
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
            return GISShare.Win32.API.FindWindowEx(base.Handle, IntPtr.Zero, UPDOWN32CLASSNAME, null);
        }

        #region IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
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

        private bool m_HaveShadow = true;
        [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
        }
        #endregion

        #region IOwner
        [Browsable(false), Description("获取其拥有者"), Category("关联")]
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        [Browsable(false), Description("是否显示绘制区"), Category("外观")]
        public bool ShowGripRegion
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("值为颜色项时，绘制颜色区所需的宽度"), Category("布局")]
        public int ColorRegionWidth
        {
            get { return CTR_COLORREGIONWIDTH; }
        }

        [Browsable(false), Description("图片绘制区所需的宽度"), Category("布局")]
        public int ImageGripRegionWidth
        {
            get
            {
                if (this.ImageList != null) return this.ImageList.ImageSize.Width;
                return 0;
            }
        }

        [Browsable(false), Description("最左端绘制区所需的宽度"), Category("布局")]
        public int LeftGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        [Browsable(false), Description("记录它的绘制状态"), Category("外观")]
        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get
            {
                if (this.ImageList != null) return GISShare.Controls.WinForm.ItemDrawStyle.eImageLabel;
                return GISShare.Controls.WinForm.ItemDrawStyle.eSimply;
            }
        }
        #endregion

        #region ITabControlX
        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("其外框的显示状态"), Category("外观")]
        public BorderStyle BorderStyle
        {
            get 
            {
                return BorderStyle.None;
            }
        }

        int m_ImageSpace = 0;
        [Browsable(true), DefaultValue(0), Description("图片四周的间距"), Category("布局")]
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        [Browsable(false), Description("鼠标移动遂即选中所在项"), Category("状态")]
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
        #endregion

        private bool m_IsMouseDown = false;
        private void SetIsMouseDown(bool bValue)
        {
            if (this.m_IsMouseDown == bValue) return;
            //
            this.m_IsMouseDown = bValue;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.SetIsMouseDown(true);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.SetIsMouseDown(false);
            base.OnMouseUp(e);
        }

        private int m_MouseLoctionTabItemIndex = -1;
        protected override void OnMouseMove(MouseEventArgs e)
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
            //
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.AutoMouseMoveSeleced && this.m_MouseLoctionTabItemIndex >= 0)
            {
                Rectangle rectangle = this.GetTabRect(this.m_MouseLoctionTabItemIndex);
                rectangle.Inflate(1, 1);
                this.Invalidate(rectangle);
            }
            //
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTabControl(new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                if (this.SelectedIndex != i) this.DrawTabPage(this.TabPages[i], CheckState.Unchecked, this.GetTabRect(i), e.Graphics);
            }
            //
            if (this.SelectedIndex >= 0) this.DrawTabPage(this.TabPages[this.SelectedIndex], CheckState.Checked,  this.GetTabRect(this.SelectedIndex), e.Graphics);
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

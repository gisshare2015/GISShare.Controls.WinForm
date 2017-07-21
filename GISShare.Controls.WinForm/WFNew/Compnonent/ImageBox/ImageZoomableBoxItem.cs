using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ImageZoomableBoxItem : AreaItem, IImageZoomableBoxItem
    {
        const float CONST_MINWidth = 30;
        const float CONST_MINHeight = 30;

        #region 构造函数
        public ImageZoomableBoxItem()
        {
            this.ShowOutLine = true;
        }

        public ImageZoomableBoxItem(Image image)
            : this()
        {
            this.Image = image;
        }

        public ImageZoomableBoxItem(string strName, Image image)
            : this()
        {
            this.Name = strName;
            this.Image = image;
        }

        //public ImageZoomableBoxItem(GISShare.Controls.Plugin.WFNew.IImageBoxItemP pBaseItemP)
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //IImageBoxItemP
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //}
        #endregion

        #region IImageZoomableBoxItem
        private Image m_Image = null;
        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image
        {
            get { return m_Image; }
            set
            {
                m_Image = value;
                this.DefaultExtent(false);
                if (this.ImageRectangle.Width >= this.Width ||
                this.ImageRectangle.Height >= this.Height)
                {
                    this.FullExtent(false);
                }
                if (this.Image != null) this.Refresh();
            }
        }

        private RectangleF m_ImageRectangle = RectangleF.Empty;
        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public RectangleF ImageRectangle
        {
            get { return m_ImageRectangle; }
        }

        double m_ScaleFactor = 0.1;
        [Browsable(true), DefaultValue(0.1), Description("缩放因子（0到1之间）"), Category("属性")]
        public double ScaleFactor
        {
            get { return m_ScaleFactor; }
            set { if (value <= 0 || value >= 1) return; m_ScaleFactor = value; }
        }

        /// <summary>
        /// 缩放到全尺寸
        /// </summary>
        /// <param name="bRefresh"></param>
        public void DefaultExtent(bool bRefresh)
        {
            if (this.Image == null)
            {
                this.m_ImageRectangle = Rectangle.Empty;
            }
            else
            {
                Size size = this.Image.Size;
                Rectangle displayRectangle = this.DisplayRectangle;
                if (this.ShowOutLine) displayRectangle = Rectangle.FromLTRB(displayRectangle.Left + 1, displayRectangle.Top + 1, displayRectangle.Right - 1, displayRectangle.Bottom - 1);
                this.m_ImageRectangle = new RectangleF((float)((double)(displayRectangle.Left + displayRectangle.Right - size.Width) / 2D),
                    (float)((double)(displayRectangle.Top + displayRectangle.Bottom - size.Height) / 2D),
                    (float)size.Width,
                    (float)size.Height);
                if (bRefresh) this.Refresh();
            }
        }

        /// <summary>
        /// 全图
        /// </summary>
        /// <param name="bRefresh"></param>
        public void FullExtent(bool bRefresh)
        {
            if (this.Image == null)
            {
                this.m_ImageRectangle = Rectangle.Empty;
            }
            else
            {
                Size size = this.Image.Size;
                Rectangle displayRectangle = this.DisplayRectangle;
                if (this.ShowOutLine) displayRectangle = Rectangle.FromLTRB(displayRectangle.Left + 1, displayRectangle.Top + 1, displayRectangle.Right - 1, displayRectangle.Bottom - 1);
                this.m_ImageRectangle = GISShare.Controls.WinForm.Util.UtilTX.CreateRectangle(displayRectangle, size.Width, size.Height);
                if (bRefresh) this.Refresh();
            }
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ImageZoomableBoxItem baseItem = new ImageZoomableBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Image = this.Image;
            baseItem.Padding = this.Padding;
            baseItem.Visible = this.Visible;
            baseItem.ScaleFactor = this.ScaleFactor;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        #endregion

        float m_fTop;
        float m_fLeft;
        MouseEventArgs m_MouseEventArgs = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            this.m_MouseEventArgs = e;
            this.m_fTop = this.ImageRectangle.Top;
            this.m_fLeft = this.ImageRectangle.Left;
        }
        int m_iX_MV = -1;
        int m_iY_MV = -1;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.m_MouseEventArgs != null)
                {
                    this.m_ImageRectangle = new RectangleF(this.m_fLeft + e.X - this.m_MouseEventArgs.X, this.m_fTop + e.Y - this.m_MouseEventArgs.Y, this.ImageRectangle.Width, this.ImageRectangle.Height);
                    this.Refresh();
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                double dScale = 1;
                int iZoomFactor = (this.m_iX_MV < 0 || this.m_iX_MV < 0) ? e.X - this.m_MouseEventArgs.X + e.Y - this.m_MouseEventArgs.Y : e.X - this.m_iX_MV + e.Y - this.m_iY_MV;
                if (iZoomFactor > 0)
                {
                    dScale = 1D + this.ScaleFactor;
                }
                else if (iZoomFactor < 0)
                {
                    dScale = 1D - this.ScaleFactor;
                }
                //
                this.m_ImageRectangle = GISShare.Controls.WinForm.Util.UtilTX.CreateRectangle(this.ImageRectangle, this.m_MouseEventArgs.Location, dScale);
                this.Refresh();
                //
                this.m_iX_MV = e.X;
                this.m_iY_MV = e.Y;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            this.m_MouseEventArgs = null;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            //
            if (e.Delta > 0)
            {
                this.m_ImageRectangle = GISShare.Controls.WinForm.Util.UtilTX.CreateRectangle(this.ImageRectangle, e.Location, 1D + this.ScaleFactor);
                this.Refresh();

            }
            else if (e.Delta < 0)
            {
                this.m_ImageRectangle = GISShare.Controls.WinForm.Util.UtilTX.CreateRectangle(this.ImageRectangle, e.Location, 1D - this.ScaleFactor);
                this.Refresh();
            }
        }

        //protected override void OnMouseDoubleClick(MouseEventArgs e)
        //{
        //    base.OnMouseDoubleClick(e);
        //    //
        //    this.FullExtent(true);
        //}

        public override Size MeasureSize(Graphics g)//有待完善
        {
            return this.DisplayRectangle.Size;
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            base.OnDraw(e);
            //
            if (this.Image == null) return;
            //
            if (this.ImageRectangle.Width < CONST_MINWidth || this.ImageRectangle.Height < CONST_MINHeight)
            {
                double dW = CONST_MINWidth;
                double dH = CONST_MINHeight;
                double dWH = this.ImageRectangle.Width / this.ImageRectangle.Height;
                if (this.ImageRectangle.Width > this.ImageRectangle.Height)
                {
                    dW = this.ImageRectangle.Width / this.ImageRectangle.Height * CONST_MINHeight;
                }
                else if (this.ImageRectangle.Width < this.ImageRectangle.Height)
                {
                    dH = this.ImageRectangle.Height / this.ImageRectangle.Width * CONST_MINWidth;
                }
                //
                this.m_ImageRectangle = new RectangleF(
                    this.ImageRectangle.X + (float)((double)(this.ImageRectangle.Width - CONST_MINWidth) / 2D),
                    this.ImageRectangle.Y + (float)((double)(this.ImageRectangle.Height - CONST_MINHeight) / 2D),
                    (float)dW,
                    (float)dH);
            }
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                new GISShare.Controls.WinForm.ImageRenderEventArgsF(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
        }
    }
}

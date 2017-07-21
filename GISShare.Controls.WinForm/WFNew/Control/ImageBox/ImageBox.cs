using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true)]
    public class ImageBox : BaseItemControl, IImageBoxItem
    {
        #region IImageBoxItem
        private Image m_Image = null;
        [Browsable(true), Description("图片"), Category("外观")]
        public virtual Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        private ContentAlignment m_ImageAlign = ContentAlignment.MiddleCenter;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter"), Description("文本布局的方式"), Category("外观")]
        public virtual ContentAlignment ImageAlign//BH
        {
            get
            {
                return m_ImageAlign;
            }
            set { m_ImageAlign = value; }
        }

        private ImageSizeStyle m_eImageSizeStyle = ImageSizeStyle.eDefault;
        [Browsable(true), DefaultValue(typeof(ImageSizeStyle), "eDefault"), Description("图片尺寸的展现方式"), Category("布局")]
        public virtual ImageSizeStyle eImageSizeStyle
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

        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public virtual Rectangle ImageRectangle
        {
            get
            {
                if (this.Image == null) return new Rectangle(0, 0, 0, 0);
                //
                Rectangle rectangle;
                Rectangle displayRectangle = this.DisplayRectangle;
                switch (this.eImageSizeStyle)
                {
                    case ImageSizeStyle.eStretch:
                        rectangle = this.DisplayRectangle;
                        break;
                    case ImageSizeStyle.eCustomize:
                        rectangle = this.GetImageRectangleCustomize(displayRectangle);
                        break;
                    case ImageSizeStyle.eSystem:
                        rectangle = this.GetImageRectangleSystem(displayRectangle);
                        break;
                    default:
                        rectangle = this.GetImageRectangleDefault(displayRectangle);
                        break;
                }
                //
                int iTop = rectangle.Top;
                int iLeft = rectangle.Left;
                int iRight = rectangle.Right;
                int iBottom = rectangle.Bottom;
                if (displayRectangle.Top + this.Padding.Top > iTop) iTop = displayRectangle.Top + this.Padding.Top;
                if (displayRectangle.Left + this.Padding.Left > iLeft) iLeft = displayRectangle.Left + this.Padding.Left;
                if (displayRectangle.Right - this.Padding.Right < iRight) iRight = displayRectangle.Right - this.Padding.Right;
                if (displayRectangle.Bottom - this.Padding.Bottom < iBottom) iBottom = displayRectangle.Bottom - this.Padding.Bottom;
                return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
            }
        }
        private Rectangle GetImageRectangleDefault(Rectangle rectangle)
        {
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
        private Rectangle GetImageRectangleCustomize(Rectangle rectangle)
        {
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
        private Rectangle GetImageRectangleSystem(Rectangle rectangle)
        {
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
        #endregion

        #region Clone
        public override object Clone()
        {
            ImageBoxItem baseItem = new ImageBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.Padding = this.Padding;
            baseItem.Visible = this.Visible;
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ImageBoxItem : BaseItem, IImageBoxItem
    {
        #region ���캯��
        public ImageBoxItem() { }

        public ImageBoxItem(Image image)
        {
            this.Image = image;
        }

        public ImageBoxItem(string strName, Image image)
        {
            this.Name = strName;
            this.Image = image;
        }

        //public ImageBoxItem(GISShare.Controls.Plugin.WFNew.IImageBoxItemP pBaseItemP)
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

        #region IImageBoxItem
        private Image m_Image = null;
        [Browsable(true), Description("ͼƬ"), Category("���")]
        public virtual Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        private ContentAlignment m_ImageAlign = ContentAlignment.MiddleCenter;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter"), Description("�ı����ֵķ�ʽ"), Category("���")]
        public virtual ContentAlignment ImageAlign//BH
        {
            get
            {
                return m_ImageAlign;
            }
            set { m_ImageAlign = value; }
        }

        private ImageSizeStyle m_eImageSizeStyle = ImageSizeStyle.eDefault;
        [Browsable(true), DefaultValue(typeof(ImageSizeStyle), "eDefault"), Description("ͼƬ�ߴ��չ�ַ�ʽ"), Category("����")]
        public virtual ImageSizeStyle eImageSizeStyle
        {
            get { return m_eImageSizeStyle; }
            set { m_eImageSizeStyle = value; }
        }

        private Size m_ImageSize = new Size(30, 30);
        [Browsable(true), DefaultValue(typeof(Size), "30, 30"), Description("ͼƬ�ߴ�"), Category("����")]
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

        [Browsable(false), Description("ͼƬ���ƾ��ο�"), Category("����")]
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

        public override Size MeasureSize(Graphics g)//�д�����
        {
            return this.DisplayRectangle.Size;
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
        }
    }
}

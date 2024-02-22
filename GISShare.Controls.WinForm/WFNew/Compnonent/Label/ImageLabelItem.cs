using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ImageLabelItem : AreaItem, IImageLabelItem
    {
        #region ���캯��
        public ImageLabelItem() { }

        public ImageLabelItem(string strText)
        {
            this.Text = strText;
        }

        public ImageLabelItem(string strName, string strText)
        {
            this.Name = strName;
            this.Text = strText;
        }

        public ImageLabelItem(string strText, Image image)
        {
            this.Text = strText;
            this.Image = image;
        }

        public ImageLabelItem(string strName, string strText, Image image)
        {
            this.Name = strName;
            this.Text = strText;
            this.Image = image;
        }

        //public ImageLabelItem(GISShare.Controls.Plugin.WFNew.IImageLabelItemP pBaseItemP)
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
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //}
        #endregion

        //~ImageLabelItem()
        //{
        //    if (this.m_Image != null)
        //    {
        //        this.m_Image.Dispose();
        //        this.m_Image = null;
        //    }
        //}

        #region ILabelItem
        private ContentAlignment m_TextAlign = ContentAlignment.MiddleCenter;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter"), Description("�ı����ֵķ�ʽ"), Category("���")]
        public virtual ContentAlignment TextAlign//BH
        {
            get
            {
                //if (this.IsBaseBarItem) return ContentAlignment.MiddleRight;
                return m_TextAlign;
            }
            set { m_TextAlign = value; }
        }

        private Rectangle m_TextRectangle;
        [Browsable(false), Description("�ı����ƾ��ο�"), Category("����")]
        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = m_TextRectangle;
                Rectangle displayRectangle = this.DrawRectangle;
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
        private Rectangle GetTextRectangle(Graphics g)
        {
            if (!this.AutoPlanTextRectangle ||
                this.Image == null ||
                this.eDisplayStyle == DisplayStyle.eText ||
                this.eImageSizeStyle == ImageSizeStyle.eStretch)
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

        #region IImageItem
        private Image m_Image = null;
        [Browsable(true), Description("ͼƬ"), Category("���")]
        public virtual Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        private ContentAlignment m_ImageAlign = ContentAlignment.MiddleCenter;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter"), Description("ͼƬ���ֵķ�ʽ"), Category("���")]
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
        public virtual ImageSizeStyle eImageSizeStyle//BH
        {
            get
            {
                return m_eImageSizeStyle;
            }
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
                switch (this.eImageSizeStyle)
                {
                    case ImageSizeStyle.eStretch:
                        rectangle = this.ITDrawRectangle;
                        break;
                    case ImageSizeStyle.eCustomize:
                        rectangle = this.GetImageRectangleCustomize();
                        break;
                    case ImageSizeStyle.eSystem:
                        rectangle = this.GetImageRectangleSystem();
                        break;
                    default:
                        rectangle = this.GetImageRectangleDefault();
                        break;
                }
                //
                Rectangle displayRectangle = this.DrawRectangle;
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
        #endregion

        #region IImageLabelItem
        private bool m_AutoPlanTextRectangle = true;
        [Browsable(true), DefaultValue(true), Description("�Ƿ��Զ��滮�ı��Ĳ���"), Category("����")]
        public virtual bool AutoPlanTextRectangle
        {
            get { return m_AutoPlanTextRectangle; }
            set { m_AutoPlanTextRectangle = value; }
        }

        private int m_ITSpace = 1;
        [Browsable(true), DefaultValue(1), Description("�ı���ͼƬ�ļ��"), Category("����")]
        public int ITSpace
        {
            get { return m_ITSpace; }
            set { if (value < 0) return; m_ITSpace = value; }
        }

        private DisplayStyle m_eDisplayStyle = DisplayStyle.eImageAndText;
        [Browsable(true), DefaultValue(typeof(DisplayStyle), "eImageAndText"), Description("�ı���ͼƬ��չ�ֵķ�ʽ"), Category("���")]
        public virtual DisplayStyle eDisplayStyle
        {
            get { return m_eDisplayStyle; }
            set { m_eDisplayStyle = value; }
        }

        [Browsable(false), Description("���ƾ��ο�"), Category("����")]
        public virtual Rectangle DrawRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        [Browsable(false), Description("ͼƬ���ı����ƾ��ο�"), Category("����")]
        public virtual Rectangle ITDrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                //
                return new Rectangle(
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom
                    );
            }
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ImageLabelItem baseItem = new ImageLabelItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.eDisplayStyle = this.eDisplayStyle;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.Padding = this.Padding;
            baseItem.TextAlign = this.TextAlign;
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
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    break;
                case DisplayStyle.eImage:
                    if (this.Image != null)
                    {
                        rectangle = this.ImageRectangle;
                        iWidth += rectangle.Width;
                        iHeight += rectangle.Height;
                    }
                    break;
                case DisplayStyle.eImageAndText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    if (this.Image != null)
                    {
                        switch (this.ImageAlign)
                        {
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopRight:
                                rectangle = this.ImageRectangle;
                                iWidth += rectangle.Width + this.ITSpace;
                                iHeight += rectangle.Height;
                                break;
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.TopCenter:
                                iHeight += this.ImageRectangle.Height + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleRight:
                                iWidth += this.ImageRectangle.Width + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleCenter:
                                rectangle = this.ImageRectangle;
                                if (iWidth < rectangle.Width) iWidth = rectangle.Width;
                                if (iHeight < rectangle.Height) iHeight = rectangle.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
            iWidth += this.Padding.Left + this.Padding.Right;
            iHeight += this.Padding.Top + this.Padding.Bottom;
            //
            if (iWidth < 16) iWidth = 16;
            if (iHeight < 16) iHeight = 16;
            //
            return new Size(iWidth, iHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                case DisplayStyle.eImageAndText:
                    this.m_TextRectangle = this.GetTextRectangle(e.Graphics);
                    break;
            }
            //
            base.OnPaint(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderLabel(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eImage:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    break;
                case DisplayStyle.eText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                    break;
                case DisplayStyle.eImageAndText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                    break;
                default:
                    break;
            }
        }
    }
}

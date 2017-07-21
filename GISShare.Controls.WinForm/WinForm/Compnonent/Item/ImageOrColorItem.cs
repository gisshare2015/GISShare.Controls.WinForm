using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class ImageOrColorItem : FontItem, IImageOrColorItem
    {
        public ImageOrColorItem(bool isColorItem)
        { this.m_IsColorItem = isColorItem; }

        public ImageOrColorItem(IImageItem item)
            : base(item.Name, item.Text, item.Font, item.ForeColor, item.ShowBackColor, item.BackColor)
        {
            this.m_IsColorItem = true;
        }

        public ImageOrColorItem(IColorItem item)
            : base(item.Name, item.Text, item.Font, item.ForeColor, item.ShowBackColor, item.BackColor)
        {
            this.m_IsColorItem = false;
        }

        public ImageOrColorItem(bool isColorItem, string name, string text)
            : base(name, text)
        { this.m_IsColorItem = isColorItem; }

        public ImageOrColorItem(bool isColorItem, string name, string text, Font font, Color foreColor)
            : base(name, text, font, foreColor)
        { this.m_IsColorItem = isColorItem; }

        public ImageOrColorItem(bool isColorItem, string name, string text, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor)
        { this.m_IsColorItem = isColorItem; }

        public ImageOrColorItem(bool isColorItem, string name, string text, Image image, Color color, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor)
        {
            this.m_IsColorItem = isColorItem;
            this.m_Color = color;
            this.m_Image = image;
        }

        bool m_IsColorItem = false;
        public bool IsColorItem
        {
            get { return m_IsColorItem; }
        }

        Color m_Color = Color.Red;
        [Browsable(false), Description("颜色"), Category("外观")]
        public Color Color
        {
            get { return m_Color; }
        }

        private Image m_Image = null;
        [Browsable(false), Description("图片"), Category("外观")]
        public virtual Image Image
        {
            get { return m_Image; }
        }

        public void SetIsColorItem(bool isColorItem)
        {
            this.m_IsColorItem = isColorItem;
        }

        public void SetColor(Color color)
        {
            this.m_Color = color;
            this.m_IsColorItem = true;
        }

        public void SetImage(Image image)
        {
            this.m_Image = image;
            this.m_IsColorItem = false;
        }

        public void SetColorAndImage(bool isColorItem, Color color, Image image)
        {
            this.m_IsColorItem = isColorItem;
            this.m_Color = color;
            this.m_Image = image;
        }
    }

    
}

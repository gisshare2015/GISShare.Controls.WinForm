using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class ImageItem : FontItem, IImageItem
    {
        public ImageItem() { }

        public ImageItem(string name, string text)
            : base(name, text) { }

        public ImageItem(string name, string text, Font font, Color foreColor)
            : base(name, text, font, foreColor) { }

        public ImageItem(string name, string text, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor) { }

        public ImageItem(string name, string text, Image image, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor)
        {
            this.m_Image = image;
        }

        #region IImageItem
        private Image m_Image = null;
        [Browsable(true), Description("Õº∆¨"), Category("Õ‚π€")]
        public virtual Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }
        #endregion
    }
}

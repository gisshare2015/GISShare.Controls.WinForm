using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class ColorItem : FontItem, IColorItem
    {
        public ColorItem() { }

        public ColorItem(string name, string text)
            : base(name, text) { }

        public ColorItem(string name, string text, Font font, Color foreColor)
            : base(name, text, font, foreColor) { }

        public ColorItem(string name, string text, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor) { }

        public ColorItem(string name, string text, Color color, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, font, foreColor, showBackColor, backColor)
        {
            this.m_Color = color;
        }

        #region IColorItem
        Color m_Color = Color.Red;
        [Browsable(true), Description("ÑÕÉ«"), Category("Íâ¹Û")]
        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class FontItem : Item, IFontItem
    {
        public FontItem()
        { }

        public FontItem(string name, string text)
            : base(name, text) { }

        public FontItem(string name, string text, Font font, Color foreColor)
            : base(name, text)
        {
            this.m_Font = font;
            this.m_ForeColor = foreColor;
        }

        public FontItem(string name, string text, Font font, Color foreColor, bool showBackColor, Color backColor)
            : base(name, text, showBackColor, backColor)
        {
            this.m_Font = font;
            this.m_ForeColor = foreColor;
        }

        #region IFontItem
        private Font m_Font = new Font("����", 9f);
        [Browsable(true), DefaultValue(typeof(Font), "\"����\", 9f"), Description("����"), Category("���")]
        public virtual Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        private Color m_ForeColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "SystemColors.ControlText"), Description("������ɫ"), Category("���")]
        public virtual Color ForeColor
        {
            get { return m_ForeColor; }
            set { m_ForeColor = value; }
        }
        #endregion
    }
}

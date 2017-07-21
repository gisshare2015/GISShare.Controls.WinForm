using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class ObjectRenderEventArgs
        : EventArgs
    {
        public ObjectRenderEventArgs(Graphics graphics, object obj, Rectangle bounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Bounds = bounds;
        }

        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object;
        public object Object
        {
            get { return m_Object; }
        }

        private System.Drawing.Rectangle m_Bounds;
        public System.Drawing.Rectangle Bounds
        {
            get { return m_Bounds; }
        }
    }

    public class IconRenderEventArgs
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object;
        public object Object
        {
            get { return m_Object; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private Icon m_Icon = null;
        public Icon Icon
        {
            get { return m_Icon; }
        }

        private System.Drawing.Rectangle m_IconBounds;
        public System.Drawing.Rectangle IconBounds
        {
            get { return m_IconBounds; }
        }

        public IconRenderEventArgs(Graphics graphics, object obj, bool enabled, Icon icon, Rectangle iconBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_Icon = icon;
            this.m_IconBounds = iconBounds;
        }
    }

    public class ImageRenderEventArgs
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object = null;
        public object Object
        {
            get { return m_Object; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private Image m_Image = null;
        public Image Image
        {
            get { return m_Image; }
        }

        private System.Drawing.Rectangle m_ImageBounds;
        public System.Drawing.Rectangle ImageBounds
        {
            get { return m_ImageBounds; }
        }

        public ImageRenderEventArgs(Graphics graphics, object obj, bool enabled, Image image, Rectangle imageBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_Image = image;
            this.m_ImageBounds = imageBounds;
        }
    }
    public class ImageRenderEventArgsF
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object = null;
        public object Object
        {
            get { return m_Object; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private Image m_Image = null;
        public Image Image
        {
            get { return m_Image; }
        }

        private System.Drawing.RectangleF m_ImageBounds;
        public System.Drawing.RectangleF ImageBounds
        {
            get { return m_ImageBounds; }
        }

        public ImageRenderEventArgsF(Graphics graphics, object obj, bool enabled, Image image, RectangleF imageBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_Image = image;
            this.m_ImageBounds = imageBounds;
        }
    }

    public class TextRenderEventArgs
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object = null;
        public object Object
        {
            get { return m_Object; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private bool m_HaveShadow = true;
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
        }

        private bool m_IsMiddle = false;
        /// <summary>
        /// 该属性仅对窗体有效
        /// </summary>
        public bool IsMiddle
        {
            get { return m_IsMiddle; }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
        }

        private Color m_ForeColor;
        public Color ForeColor
        {
            get { return m_ForeColor; }
        }

        private Font m_Font;
        public Font Font
        {
            get { return m_Font; }
        }

        private System.Drawing.Rectangle m_TextBounds;
        public System.Drawing.Rectangle TextBounds
        {
            get { return m_TextBounds; }
        }

        StringFormat m_StringFormat = new StringFormat();
        public StringFormat StringFormat
        {
            get { return m_StringFormat; }
        }

        public TextRenderEventArgs(Graphics graphics, object obj, bool enabled, bool haveShadow, bool isMiddle,
            string text, Color foreColor, Font font, Rectangle textBounds, StringFormat stringFormat)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_HaveShadow = haveShadow;
            this.m_IsMiddle = isMiddle;
            this.m_Text = text;
            this.m_ForeColor = foreColor;
            this.m_Font = font;
            this.m_TextBounds = textBounds;
            this.m_StringFormat = stringFormat;
        }

        public TextRenderEventArgs(Graphics graphics, object obj, bool enabled, bool haveShadow,
            string text, Color foreColor, Font font, Rectangle textBounds, StringFormat stringFormat)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_HaveShadow = haveShadow;
            this.m_Text = text;
            this.m_ForeColor = foreColor;
            this.m_Font = font;
            this.m_TextBounds = textBounds;
            this.m_StringFormat = stringFormat;
        }

        public TextRenderEventArgs(Graphics graphics, object obj, bool enabled, bool haveShadow,
            string text, Color foreColor, Font font, Rectangle textBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_HaveShadow = haveShadow;
            this.m_Text = text;
            this.m_ForeColor = foreColor;
            this.m_Font = font;
            this.m_TextBounds = textBounds;
            //
            this.m_StringFormat.Trimming = StringTrimming.EllipsisCharacter;
        }

        public TextRenderEventArgs(Graphics graphics, object obj, bool enabled,
            string text, Color foreColor, Font font, Rectangle textBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_Text = text;
            this.m_ForeColor = foreColor;
            this.m_Font = font;
            this.m_TextBounds = textBounds;
            //
            this.m_HaveShadow = true;
            this.m_StringFormat.Trimming = StringTrimming.EllipsisCharacter;
        }
    }

    public class ArrowRenderEventArgs
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object = null;
        public object Object
        {
            get { return m_Object; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private System.Drawing.Rectangle m_ArrowBounds;
        public System.Drawing.Rectangle ArrowBounds
        {
            get { return m_ArrowBounds; }
        }

        private Color m_ArrowColor;
        public Color ArrowColor
        {
            get { return m_ArrowColor; }
        }

        private WFNew.ArrowStyle m_eArrowStyle = GISShare.Controls.WinForm.WFNew.ArrowStyle.eToDown;
        public WFNew.ArrowStyle eArrowStyle
        {
            get { return m_eArrowStyle; }
        }

        public ArrowRenderEventArgs(Graphics graphics, object obj, bool enabled,
            WFNew.ArrowStyle arrowStyle, Color arrowColor, Rectangle arrowBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_ArrowColor = arrowColor;
            this.m_eArrowStyle = arrowStyle;
            this.m_ArrowBounds = arrowBounds;
        }
    }

    public class CheckedRenderEventArgs
        : EventArgs
    {
        private System.Drawing.Graphics m_Graphics;
        public System.Drawing.Graphics Graphics
        {
            get { return m_Graphics; }
        }

        object m_Object = null;
        public object Object
        {
            get { return m_Object; }
        }

        private System.Drawing.Rectangle m_CheckBounds;
        public System.Drawing.Rectangle CheckBounds
        {
            get { return m_CheckBounds; }
        }

        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        private Color m_CheckColor;
        public Color CheckColor
        {
            get { return m_CheckColor; }
        }

        private CheckState m_CheckState;
        public CheckState CheckState
        {
            get { return m_CheckState; }
        }

        public CheckedRenderEventArgs(Graphics graphics, object obj, bool enabled,
            Color checkColor, CheckState checkState, Rectangle checkBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_CheckColor = checkColor;
            this.m_CheckState = checkState;
            this.m_CheckBounds = checkBounds;
        }

        public CheckedRenderEventArgs(Graphics graphics, object obj, bool enabled,
            Color checkColor, bool bChecked, Rectangle checkBounds)
        {
            this.m_Graphics = graphics;
            this.m_Object = obj;
            this.m_Enabled = enabled;
            this.m_CheckColor = checkColor;
            this.m_CheckState = bChecked ? CheckState.Checked : CheckState.Unchecked;
            this.m_CheckBounds = checkBounds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IButtonTextBoxItem : ITextBoxItem
    {
        GlyphStyle eGlyphStyle { get; set; }

        Padding ButtonPadding { get; set; }

        Rectangle GlyphButtonRectangle { get; }

        int OffsetX { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IButtonTextBoxItem : ITextBoxItem
    {
        GlyphStyle eGlyphStyle { get; set; }

        Rectangle GlyphButtonRectangle { get; }
    }
}

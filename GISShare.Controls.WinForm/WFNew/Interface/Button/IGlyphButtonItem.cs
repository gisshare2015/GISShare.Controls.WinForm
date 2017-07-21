using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IGlyphButtonItem : IBaseButtonItem
    {
        GlyphStyle eGlyphStyle { get; set; }

        Rectangle GlyphRectangle { get; }
    }
}

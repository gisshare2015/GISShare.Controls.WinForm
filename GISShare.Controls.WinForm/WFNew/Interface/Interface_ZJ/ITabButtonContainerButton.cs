using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabButtonContainerButton : IBaseButtonItem
    {
        Rectangle GlyphRectangle { get; }

        TabButtonContainerButtonStyle eTabButtonContainerButtonStyle { get; }
    }    
}

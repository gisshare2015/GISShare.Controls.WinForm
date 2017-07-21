using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IColorItem : IFontItem
    {
        Color Color { get; set; }
    }
}

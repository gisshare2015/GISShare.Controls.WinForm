using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IFontItem : IItem
    {
        Font Font { get; set; }

        Color ForeColor { get; set; }
    }
}

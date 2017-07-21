using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IImageItem : IFontItem
    {
        Image Image { get; set; }
    }
}

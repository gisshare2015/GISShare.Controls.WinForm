using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GISShare.Controls.WinForm.WFNew;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IImageBoxItemP : IBaseItemP_
    {
        Image Image { get; }

        ContentAlignment ImageAlign { get; }

        ImageSizeStyle eImageSizeStyle { get; }

        Size ImageSize { get; }
    }
}

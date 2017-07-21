using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IImageBoxItem : IBaseItem2
    {
        Image Image { get;set; }

        ContentAlignment ImageAlign { get;set; }

        ImageSizeStyle eImageSizeStyle { get;set; }

        Size ImageSize { get;set; }

        Rectangle ImageRectangle { get;}
    }
}

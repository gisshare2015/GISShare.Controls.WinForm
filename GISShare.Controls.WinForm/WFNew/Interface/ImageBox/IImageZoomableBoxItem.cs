using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IImageZoomableBoxItem
    {
        Image Image { get; set; }

        double ScaleFactor { get; set; }

        RectangleF ImageRectangle { get; }

        void DefaultExtent(bool bRefresh);
        void FullExtent(bool bRefresh);
    }
}

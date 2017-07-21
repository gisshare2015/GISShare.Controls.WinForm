using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IImageOrColorItem : IFontItem
    {
        bool IsColorItem { get; }

        Color Color { get; }

        Image Image { get; }

        void SetIsColorItem(bool isColorItem);

        void SetColor(Color color);

        void SetImage(Image image);

        void SetColorAndImage(bool isColorItem, Color color, Image image);
    }
}

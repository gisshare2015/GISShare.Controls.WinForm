using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IGalleryScrollButton : IBaseButtonItem
    {
        GalleryScrollButtonStyle eGalleryScrollButtonStyle { get; }
    }
}

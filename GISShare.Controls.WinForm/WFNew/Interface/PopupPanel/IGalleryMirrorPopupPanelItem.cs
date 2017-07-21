using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IGalleryMirrorPopupPanelItem : IBaseItemStackExItem, IPopupPanel
    {
        IGalleryItem pGalleryItem { get; }
    }
}

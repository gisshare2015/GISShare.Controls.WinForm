using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBasePopupEvent2 : IBasePopupEvent
    {
        event EventHandler PopupOpened;
        event EventHandler PopupClosed;

    }
}

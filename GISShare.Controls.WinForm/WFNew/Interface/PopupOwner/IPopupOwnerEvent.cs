using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPopupOwnerEvent
    {
        event EventHandler PopupOpened;
        event EventHandler PopupClosed;

    }
}

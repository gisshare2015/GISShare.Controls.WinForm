using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPopupOwner : IPopupOwnerEvent
    {
        int PopupSpace { get; set; }

        bool IsAutoMouseTrigger { get; }

        Rectangle PopupTriggerRectangle { get; }

        Point PopupLoction { get; }

        bool IsOpened { get; }

        void ShowPopup();

        void ClosePopup();
    }
}

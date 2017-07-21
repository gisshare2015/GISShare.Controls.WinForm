using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPanelPopup : IBasePopup2
    {
        Size GetIdealSize();

        IPopupPanel GetPopupPanel();
    }
}

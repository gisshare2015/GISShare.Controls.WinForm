using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    interface IToolTipPopup : IPanelPopup
    {
        bool SetTipInfo(ITipInfo pTipInfo);
    }
}

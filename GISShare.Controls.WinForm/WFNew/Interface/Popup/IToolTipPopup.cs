using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IToolTipPopup : IPanelPopup
    {
        bool SetTipInfo(ITipInfo pTipInfo);
    }
}

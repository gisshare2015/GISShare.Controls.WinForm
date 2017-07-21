using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IToolTipPopupPanelItem : IBaseItemStackExItem, IPopupPanel
    {
        ITipInfo TipInfo { get; }

        bool SetTipInfo(ITipInfo pTipInfo);
    }
}

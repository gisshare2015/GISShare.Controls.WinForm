using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDropDownPopup : ISimplyPopup
    {
        ContextPopupStyle eContextPopupStyle { get; set; }
    }
}

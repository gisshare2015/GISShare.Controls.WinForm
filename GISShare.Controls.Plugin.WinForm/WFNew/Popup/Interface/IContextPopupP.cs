using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IContextPopupP : IBaseItemP_, ISubItem
    {
        GISShare.Controls.WinForm.WFNew.ContextPopupStyle eContextPopupStyle { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IFlexibleRowViewItem : IRowViewItem, IResizeViewItem
    {
        bool IsFilled { get; set; }

        bool CanExchangeItem { get; set; }
    }
}

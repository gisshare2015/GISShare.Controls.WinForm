using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IRowViewObject
    {
        int DefaultRowHeight { get; set; }
        bool CanResizeRowHeight { get; set; }
        bool CanExchangeRow { get; set; }
        int RowWidth { get; }
        Rectangle RowViewItemsRectangle { get; }
    }
}

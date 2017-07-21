using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IColumnViewObject
    {
        int DefaultColumnWidth { get; set; }

        bool ShowColumn { get; set; }

        int ColumnHeight { get; set; }

        bool CanResizeColumnWidth { get; set; }

        bool CanExchangeColumn { get; set; }

        ColumnViewItemCollection ColumnViewItems { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    interface IRowColumnViewItem
    {
        ColumnViewItemCollection ViewItems { get; }

        int SelectedIndex { get; set; }

        bool ShowBaseItemState { get; set; }
    }
}

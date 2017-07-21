using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface ICellViewItem : ISuperViewItem, IVisibleViewItem
    {
        CellViewStyle eCellViewStyle { get; }

        object Value { get; set; }
    }
}

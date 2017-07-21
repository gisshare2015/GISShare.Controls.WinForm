using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    interface IColumnViewObjectHelper : IOwner
    {
        void InsertColumnViewItem(int index);

        void RemoveColumnViewItem(int index);

        void ExchangeColumnViewItem(int index1, int index2);

        Rectangle ColumnViewItemsRectangle { get; }
    }
}

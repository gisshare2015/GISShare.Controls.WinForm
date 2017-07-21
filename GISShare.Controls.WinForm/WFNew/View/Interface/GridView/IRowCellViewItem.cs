using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IRowCellViewItem : IRowViewItem
    {
        RowCellViewStyle eRowCellViewStyle { get; }
    }
}

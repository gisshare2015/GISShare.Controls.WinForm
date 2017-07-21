using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IColumnViewItem : ISuperViewItem, ITextViewItem, IVisibleViewItem
    {
        string FieldName { get; set; }
    }
}

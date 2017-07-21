using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IComboDateItem : ICustomizeComboBoxItem
    {
        event PropertyChangedEventHandler SelectedDateChanged;

        DateTime SelectedDate { get; set; }
    }
}

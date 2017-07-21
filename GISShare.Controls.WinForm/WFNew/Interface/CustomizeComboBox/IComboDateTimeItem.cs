using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IComboDateTimeItem : ICustomizeComboBoxItem
    {
        event PropertyChangedEventHandler SelectedDateTimeChanged;

        DateTime SelectedDateTime { get; set; }
    }
}

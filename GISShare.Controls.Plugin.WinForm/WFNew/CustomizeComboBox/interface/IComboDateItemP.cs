using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IComboDateItemP : ICustomizeComboBoxItemP_
    {
        DateTime SelectedDate { get; }
    }
}

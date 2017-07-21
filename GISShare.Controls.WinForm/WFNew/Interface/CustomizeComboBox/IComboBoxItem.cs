using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IComboBoxItem : ICheckedComboBoxItem
    {
        event IntValueChangedHandler SelectedIndexChanged;

        int SelectedIndex { get;  set; }

        View.ViewItem SelectedItem { get; }
    }
}

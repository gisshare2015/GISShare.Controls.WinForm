using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IComboBoxItemP : ICheckedComboBoxItemP_
    {
        int SelectedIndex { get; }

        GISShare.Controls.WinForm.WFNew.View.ViewItem[] Items { get; }
    }
}

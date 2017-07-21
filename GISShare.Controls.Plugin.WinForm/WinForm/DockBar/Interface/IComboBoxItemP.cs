using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IComboBoxItemP : IBaseItemDBP_
    {
        int DropDownHeight { get; }

        int DropDownWidth { get; }

        int SelectedIndex { get; }

        System.Windows.Forms.ComboBoxStyle DropDownStyle { get; }

        object[] Items { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IComboTreeItem : ICheckedComboBoxItem
    {
        event PropertyChangedEventHandler SelectedNodeChanged;

        View.NodeViewItem SelectedNode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IComboTreeItemP : ICheckedComboBoxItemP_
    {
        GISShare.Controls.WinForm.WFNew.View.NodeViewItem SelectedNode { get; }

        GISShare.Controls.WinForm.WFNew.View.NodeViewItem[] NodeViewItems { get; }
    }
}

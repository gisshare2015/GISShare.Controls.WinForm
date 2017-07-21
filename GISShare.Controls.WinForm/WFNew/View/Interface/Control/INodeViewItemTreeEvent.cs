using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface INodeViewItemTreeEvent
    {
        event PropertyChangedEventHandler SelectedNodeChanged;
    }
}

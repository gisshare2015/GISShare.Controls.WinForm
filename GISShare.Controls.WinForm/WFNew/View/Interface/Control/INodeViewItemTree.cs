using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface INodeViewItemTree : IViewItemList, INodeViewList
    {
        bool CanEdit { get; set; }

        NodeViewItem SelectedNode { get; set; }

        NodeViewItem TryGetNodeViewItemFromPoint(Point point);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface INodeViewList : IViewList
    {
        NodeViewItemCollection NodeViewItems { get; }

        void Expand();

        void Collapse();

        void ExpandAll();

        void CollapseAll();
    }
}

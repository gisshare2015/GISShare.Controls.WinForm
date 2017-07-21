using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IMultipleNode : IBaseNode, IPanelNode
    {
        IBaseNode ParentNode { get; }

        int ChildNodeCount { get; }

        IBaseNode[] ChildNodes { get; }
    }
}

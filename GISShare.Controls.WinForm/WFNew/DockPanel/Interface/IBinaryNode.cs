using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IBinaryNode : IBaseNode
    {
        IBaseNode ParentNode { get; }

        IBaseNode LeftNode { get; }

        IBaseNode RightNode { get; }
    }
}

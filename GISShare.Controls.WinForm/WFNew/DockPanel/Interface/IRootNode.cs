using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IRootNode : IBaseNode
    {
        IBaseNode ChildNode { get; }
    }
}

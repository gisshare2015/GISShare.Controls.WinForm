using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IBasePanel2 : WFNew.IBaseItem, IBasePanel, IBottomNode
    {
        DockPanel TryGetDependDockPanel();
    }
}

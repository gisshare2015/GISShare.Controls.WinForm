using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IPanelNode
    {
        event PanelNodeStateChangedEventHandler PanelNodeStateChanged;

        PanelNodeState ePanelNodeState { get; }

        PanelNodeStyle ePanelNodeStyle { get; }
    }
}

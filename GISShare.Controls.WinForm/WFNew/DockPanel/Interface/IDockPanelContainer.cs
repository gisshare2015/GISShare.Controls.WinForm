using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanelContainer
    {
        //int RecordID { get; }

        string Name { get; }

        string Describe { get; }

        DockPanelManager DockPanelManager { get; }

        DockPanelContainerStyle eDockPanelContainerStyle { get; }

        DockPanel[] GetDockPanels();
    }

}

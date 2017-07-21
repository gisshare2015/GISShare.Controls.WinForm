using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    internal interface ISetDockPanelHelper
    {
        void SetActiveState(IDockPanel pDockPanel);

        bool SetHideState(bool bIsHideState);
    }
}

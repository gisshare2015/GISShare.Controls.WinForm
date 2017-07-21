using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICollapsableSplitPanel : GISShare.Controls.WinForm.WFNew.ISplitPanel2
    {
        bool Collapse { get;}

        WFNew.BaseItemState eCollapseButtonState { get; }

        CollapseSplitPanelStyles eCollapseSplitPanelStyles { get;}

        Rectangle CollapseButtonRectangle { get;}
    }
}

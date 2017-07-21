using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISplitPanel2 : ISplitPanel
    {
        int InternalMinWidth { get; set; }

        int OuterMinWidth { get; set; }

        int SplitLineWidth { get; set; }

        DockStyle SplitPanelDock { get; set; }
    }
}

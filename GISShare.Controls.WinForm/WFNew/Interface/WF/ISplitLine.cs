using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISplitLine : WFNew.IBaseItem
    {
        System.Windows.Forms.DockStyle SplitPanelDock { get; }
    }
}

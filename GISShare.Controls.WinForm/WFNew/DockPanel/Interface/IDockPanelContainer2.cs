using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanelContainer2 : WFNew.IBaseItem, GISShare.Controls.WinForm.WFNew.ISplitPanel, IDockPanelContainer, IBinaryNode, IDockPanel
    {
        IDockPanel GetIDockPanelFromPanel1();

        IDockPanel GetIDockPanelFromPanel2();
    }
}

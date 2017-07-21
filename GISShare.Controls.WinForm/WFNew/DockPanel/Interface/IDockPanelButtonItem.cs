using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanelButtonItem : WFNew.IBaseButtonItem
    {
        bool IsHideState { get; }

        DockPanelButtonItemStyle eDockPanelButtonItemStyle { get;  }

        Rectangle GlyphRectangle { get; }
    }
}

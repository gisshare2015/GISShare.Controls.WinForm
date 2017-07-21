using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanelFloatFormButtonItem : WFNew.IBaseButtonItem
    {
        DockPanelFloatFormButtonItemStyle eDockPanelFloatFormButtonItemStyle { get;  }

        System.Windows.Forms.Form OperationForm { get; }

        Rectangle GlyphRectangle { get; }
    }
}

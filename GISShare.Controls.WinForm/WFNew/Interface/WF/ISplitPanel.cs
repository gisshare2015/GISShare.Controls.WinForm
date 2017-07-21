using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISplitPanel : WFNew.IBaseItem, WFNew.IArea
    {
        Rectangle PanelRectangle { get; }

        Rectangle SplitterRectangle { get; }

        Rectangle SplitterScreenRectangle { get; }
    }
}

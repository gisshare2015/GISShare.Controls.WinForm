using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISplitButtonItem : IDropDownButtonItem
    {
        bool ShowNomalSplitLine { get; set; }

        BaseItemState eSplitState { get; }

        Rectangle SplitRectangle { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICustomizeComboBoxItemEvent
    {
        event MouseEventHandler TextBoxMouseDown;
        event MouseEventHandler SplitMouseDown;
        event MouseEventHandler TextBoxMouseMove;
        event MouseEventHandler SplitMouseMove;
        event MouseEventHandler TextBoxMouseUp;
        event MouseEventHandler SplitMouseUp;
        event MouseEventHandler TextBoxMouseClick;
        event MouseEventHandler SplitMouseClick;
        event MouseEventHandler TextBoxMouseDoubleClick;
        event MouseEventHandler SplitMouseDoubleClick;

    }
}

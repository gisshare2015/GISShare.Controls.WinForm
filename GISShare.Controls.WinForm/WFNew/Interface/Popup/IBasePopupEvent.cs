using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBasePopupEvent
    {
        event EventHandler SizeChanged;
        event EventHandler LocationChanged;
        event EventHandler MouseEnter;
        event EventHandler MouseLeave;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseUp;
        event MouseEventHandler MouseClick;
        event MouseEventHandler MouseDoubleClick;

        event EventHandler Opened;
        event CancelEventHandler Opening;
        event ToolStripDropDownClosedEventHandler Closed;
        event ToolStripDropDownClosingEventHandler Closing;
    }
}

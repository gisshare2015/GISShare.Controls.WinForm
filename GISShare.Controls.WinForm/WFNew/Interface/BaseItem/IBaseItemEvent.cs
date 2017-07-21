using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItemEvent
    {
        event PaintEventHandler Paint;
        event EventHandler SizeChanged;
        event EventHandler LocationChanged;
        event EventHandler CheckedChanged;
        event EventHandler EnabledChanged;
        event EventHandler VisibleChanged;
        event EventHandler TextChanged;
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        event KeyPressEventHandler KeyPress;
        event EventHandler MouseEnter;
        event EventHandler MouseLeave;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseUp;
        event MouseEventHandler MouseClick;
        event MouseEventHandler MouseDoubleClick;

    }
}

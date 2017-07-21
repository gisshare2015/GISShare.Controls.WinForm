using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISplitButtonItemEvent
    {
        event MouseEventHandler ButtonMouseDown;
        event MouseEventHandler SplitMouseDown;
        event MouseEventHandler ButtonMouseMove;
        event MouseEventHandler SplitMouseMove;
        event MouseEventHandler ButtonMouseUp;
        event MouseEventHandler SplitMouseUp;
        event MouseEventHandler ButtonMouseClick;
        event MouseEventHandler SplitMouseClick;
        event MouseEventHandler ButtonMouseDoubleClick;
        event MouseEventHandler SplitMouseDoubleClick;
        ////
        //event EventHandler SplitClick;
        //event EventHandler ButtonClick;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IInputRegionEvent
    {
        event CancelEventHandler InputEnd;
    }
}

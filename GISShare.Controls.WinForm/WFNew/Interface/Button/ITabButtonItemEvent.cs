using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabButtonItemEvent
    {
        event BoolValueChangedEventHandler TabButtonActiveChanged;
        event MouseEventHandler TabButtonMouseDown;
        event MouseEventHandler CloseButtonMouseDown;
        event MouseEventHandler TabButtonMouseMove;
        event MouseEventHandler CloseButtonMouseMove;
        event MouseEventHandler TabButtonMouseUp;
        event MouseEventHandler CloseButtonMouseUp;
        event MouseEventHandler TabButtonMouseClick;
        event MouseEventHandler CloseButtonMouseClick;
        event MouseEventHandler TabButtonMouseDoubleClick;
        event MouseEventHandler CloseButtonMouseDoubleClick;

    }
}

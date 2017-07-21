using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemListEvent
    {
        event CancelEventHandler InputEnd;

        event ViewItemEventHandler ViewItemEdited;
    }
}

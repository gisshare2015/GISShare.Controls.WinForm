using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IEventHelper
    {
        EventStateStyle GetEventState(string strEventName);

        bool RelationEvent(string strEventName, EventArgs e);
    }
}

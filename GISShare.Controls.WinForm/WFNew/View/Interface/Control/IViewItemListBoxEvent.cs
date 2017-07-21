using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemListBoxEvent
    {
        event IntValueChangedHandler SelectedIndexChanged;
    }
}

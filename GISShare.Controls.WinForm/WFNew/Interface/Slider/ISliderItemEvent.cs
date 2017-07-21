using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISliderItemEvent
    {
        event DoubleValueChangedHandler ValueChanged;

    }
}

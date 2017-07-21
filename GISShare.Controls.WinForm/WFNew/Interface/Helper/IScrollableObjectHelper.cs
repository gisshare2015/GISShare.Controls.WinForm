using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IScrollableObjectHelper
    {
        int HScrollBarMinimum { get; }

        int HScrollBarMaximum { get; }

        bool HScrollBarVisible { get; }

        Rectangle HScrollBarDisplayRectangle { get; }

        //

        int VScrollBarMinimum { get; }

        int VScrollBarMaximum { get; }

        bool VScrollBarVisible { get; }

        Rectangle VScrollBarDisplayRectangle { get; }

        //

        void ScrollValueRefresh();
    }
}

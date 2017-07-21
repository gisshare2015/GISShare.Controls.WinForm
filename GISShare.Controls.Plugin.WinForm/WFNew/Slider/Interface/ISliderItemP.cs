using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ISliderItemP : IBaseItemP_
    {
        System.Windows.Forms.Orientation eOrientation { get; }

        double Step { get; }

        double Value { get; }

        double Maximum { get; }

        double Minimum { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface INumericUpDownItemP : IBaseItemDBP_
    {
        decimal Maximum { get; }

        decimal Minimum { get;}

        decimal Value { get;}

        decimal Increment { get;  }
    }
}

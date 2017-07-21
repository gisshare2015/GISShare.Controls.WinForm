using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IProgressBarItemP : IBaseItemDBP_
    {
        int Maximum { get; }

        int Minimum { get; }

        int Value { get; }

        int Step { get; }
    }
}

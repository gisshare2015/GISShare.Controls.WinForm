using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IScrollBarItemP : IBaseItemP_
    {
        System.Windows.Forms.Orientation eOrientation { get;  }

        int Step { get; }

        int Value { get;}

        int Maximum { get; }

        int Minimum { get; }
    }
}

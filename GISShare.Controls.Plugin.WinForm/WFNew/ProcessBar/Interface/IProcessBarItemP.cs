using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IProcessBarItemP : IBaseItemP_
    {
        System.Windows.Forms.Orientation eOrientation { get; }

        int LeftTopRadius { get; }

        int RightTopRadius { get; }

        int LeftBottomRadius { get; }

        int RightBottomRadius { get; }

        int Value { get; }

        int Maximum { get; }

        int Minimum { get; }
    }
}

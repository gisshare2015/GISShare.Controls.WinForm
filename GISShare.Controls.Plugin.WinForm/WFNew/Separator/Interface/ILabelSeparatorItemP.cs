using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ILabelSeparatorItemP : ILabelItemP
    {
        bool ShowNomalState { get;  }

        int LeftTopRadius { get; }

        int RightTopRadius { get;  }

        int LeftBottomRadius { get;}

        int RightBottomRadius { get; }
    }
}

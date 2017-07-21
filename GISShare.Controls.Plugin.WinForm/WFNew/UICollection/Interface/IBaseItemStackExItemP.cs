using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IBaseItemStackExItemP : IBaseItemStackItemP, ISubItem
    {
        int LeftTopRadius { get; }

        int RightTopRadius { get; }

        int LeftBottomRadius { get; }

        int RightBottomRadius { get; }

        int TopViewItemIndex { get; }

        bool PreButtonIncreaseIndex { get; }
    }
}

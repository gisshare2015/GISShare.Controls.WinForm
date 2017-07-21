using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBasePopup2 : IBasePopup, IBasePopupEvent2
    {
        bool IsOpened { get; }

        bool UseRadius { get; }
        int LeftTopRadius { get; }
        int RightTopRadius { get; }
        int LeftBottomRadius { get; }
        int RightBottomRadius { get; }
    }
}

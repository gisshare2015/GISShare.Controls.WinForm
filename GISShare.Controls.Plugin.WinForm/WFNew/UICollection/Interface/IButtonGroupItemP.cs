using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IButtonGroupItemP : IBaseItemStackItemP, ISubItem
    {
        bool ShowNomalState { get; }

        bool UseButtonGroupRadius { get;  }

        int LeftTopRadius { get; }

        int RightTopRadius { get;  }

        int LeftBottomRadius { get;  }

        int RightBottomRadius { get; }
    }
}

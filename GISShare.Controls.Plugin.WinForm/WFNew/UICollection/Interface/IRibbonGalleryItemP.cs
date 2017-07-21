using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IRibbonGalleryItemP : IBaseItemStackItemP, ISubItem
    {
        int TopViewItemIndex { get;  }

        int LeftTopRadius { get; }

        int RightTopRadius { get;  }

        int LeftBottomRadius { get; }

        int RightBottomRadius { get;  }

        bool UpButtonIncreaseIndex { get;  }
    }
}

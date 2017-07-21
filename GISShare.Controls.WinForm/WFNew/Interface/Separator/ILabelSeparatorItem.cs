using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ILabelSeparatorItem : ILabelItem
    {
        bool ShowNomalState { get; set; }

        int LeftTopRadius { get; set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }
    }
}

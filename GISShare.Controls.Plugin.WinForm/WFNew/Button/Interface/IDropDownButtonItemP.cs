using System;
using System.Collections.Generic;
using System.Text;
using GISShare.Controls.WinForm.WFNew;
using System.Drawing;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IDropDownButtonItemP : IBaseButtonItemP, ISubItem
    {
        int DropDownDistance { get; }

        ArrowStyle eArrowStyle { get; }

        ArrowDock eArrowDock { get;  }

        ContextPopupStyle eContextPopupStyle { get; }

        Size ArrowSize { get;}

        int PopupSpace { get; }
    }
}

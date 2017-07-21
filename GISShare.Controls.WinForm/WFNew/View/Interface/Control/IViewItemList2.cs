using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemList2
    {
        string TryGetInputingText();

        ViewItem TryGetFocusViewItem();
    }
}

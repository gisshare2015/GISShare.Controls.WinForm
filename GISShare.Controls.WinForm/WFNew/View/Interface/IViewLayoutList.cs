using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewLayoutList : IViewList
    {
        int TopViewItemIndex { get; }

        int BottomViewItemIndex { get; }

        ViewItem TryGetViewItemFromPoint(Point point);
    }
}

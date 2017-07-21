using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IRowViewItem : ISizeViewItem, IViewList, IViewLayoutList
    {
        int SelectedIndex { get; set; }

        bool ShowBaseItemState { get; set; }

        IFlexibleList Items { get; }
    }
}

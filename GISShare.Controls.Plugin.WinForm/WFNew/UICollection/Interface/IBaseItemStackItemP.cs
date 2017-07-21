using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IBaseItemStackItemP : IBaseItemP_, ISubItem
    {
        bool CanExchangeItem { get; }

        bool ReverseLayout { get; }

        bool IsStretchItems { get; }

        bool IsRestrictItems { get; }

        int RestrictItemsWidth { get; }

        int RestrictItemsHeight { get; }

        int LineDistance { get; }

        int ColumnDistance { get; }

        int MinSize { get; }

        int MaxSize { get; }

        System.Windows.Forms.Orientation eOrientation { get; }
    }
}

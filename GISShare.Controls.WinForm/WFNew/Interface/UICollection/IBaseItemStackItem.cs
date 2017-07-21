using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItemStackItem : IBaseItem3, IArea, IUICollectionItem
    {
        bool CanExchangeItem { get; set; }

        bool ReverseLayout { get; set; }

        bool IsStretchItems { get; set; }

        bool IsRestrictItems { get; set; }

        int RestrictItemsWidth { get; set; }

        int RestrictItemsHeight { get; set; }

        int LineDistance { get; set; }

        int ColumnDistance { get; set; }

        int MinSize { get; set; }

        int MaxSize { get; set; }

        System.Windows.Forms.Orientation eOrientation { get; set; }

        int OverflowItemsCount { get; }
        int DrawItemsCount { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IColumnTitleViewObject
    {
        bool ShowColumnTitle { get; set; }

        bool CanResizeColumnTitleHeight { get; set; }

        int ColumnTitleHeight { get; }

        Rectangle ColumnTitleViewItemsRectangle { get; }

        TitleViewItemCollection TitleViewItems { get; }

        TitleViewItem GetTitleViewItemByText(string strText);

        TitleViewItem GetTitleViewItemByName(string strText);
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemListBox : IViewItemList
    {
        bool CanEdit { get; set; }
        bool CanSelect { get; set; }

        int SelectedIndex { get; set; }

        bool MultipleSelect { get; set; }

        bool ShowHScrollBar { get; set; }
    }
}

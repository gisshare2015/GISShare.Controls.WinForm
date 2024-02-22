using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemSearchListBox : IViewItemListBox
    {
        bool ShowSearchBox { get; set; }
        bool SearchBoxIsTop { get; set; }
        Rectangle SearchBoxRectangle { get; }
        List<ViewItem> ViewItems { get; }
        void UpdateViewItems();
    }
}

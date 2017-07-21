using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemListBox2 : IViewItemListBox
    {
        ViewItem SelectedItem { get; }

        ViewItemCollection ViewItems { get; }
    }
}

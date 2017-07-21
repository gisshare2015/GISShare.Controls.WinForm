using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewListEnumerator
    {
        IViewItem GetViewItem(int index);

        int GetViewItemCount();
    }
}

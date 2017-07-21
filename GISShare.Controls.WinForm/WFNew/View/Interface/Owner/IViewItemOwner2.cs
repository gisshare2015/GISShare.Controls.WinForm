using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemOwner2 : IViewItemOwner
    {
        IViewItemOwner2 GetTopViewItemOwner();
    }
}

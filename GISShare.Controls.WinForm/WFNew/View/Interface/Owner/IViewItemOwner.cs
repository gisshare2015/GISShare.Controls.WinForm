using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemOwner : IOwner//IBaseItemOwner
    {
        Rectangle ViewItemsRectangle { get; }
    }
}

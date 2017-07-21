using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface IUICollectionItemNC : IOffsetNC, ICollectionItem2, ICollectionItem3
    {
        bool HaveVisibleBaseItemNC { get; }

        System.Drawing.Rectangle ItemsRectangleNC { get; }
    }
}

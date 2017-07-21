using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IUICollectionItem : ICollectionItem, ICollectionItem2, ICollectionItem3
    {
        System.Drawing.Rectangle ItemsRectangle { get;  }

        Size GetIdealSize(Graphics g);
    }
}

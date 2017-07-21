using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItemList : IBaseItem, IViewLayoutList
    {
        bool ShowOutLine { get; set; }

        Color BackColor { get; set; }

        int LeftOffset { get;  }

        Rectangle FrameRectangle { get; }
    }
}

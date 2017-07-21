using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IResizeViewItem : ITextViewItem
    {
        bool CanResizeItemWidth { get; set; }

        bool CanResizeItemHeight { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface ITextViewItem : ISizeViewItem
    {
        Font Font { get; set; }

        Color ForeColor { get; set; }
    }
}

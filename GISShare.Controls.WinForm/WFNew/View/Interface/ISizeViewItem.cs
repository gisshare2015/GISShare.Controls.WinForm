using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface ISizeViewItem : IViewItem
    {
        int Width { get; set; }

        int Height { get; set; }
    }
}

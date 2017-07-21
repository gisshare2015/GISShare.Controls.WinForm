using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonPageItem : IBaseItemStackExItem, ITabPageItem
    {
        bool VisibleEx { get; set; }
 
        System.Drawing.Image Image { get; set; }
    }
}

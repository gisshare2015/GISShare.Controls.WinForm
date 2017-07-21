using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IRibbonPageItemP : IBaseItemStackExItemP, ISubItem
    {
        System.Drawing.Image Image { get; }
    }
}

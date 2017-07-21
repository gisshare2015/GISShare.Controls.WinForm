using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IDropDownButtonItemP : IBaseItemDBP_, ISubItem
    {
        bool ShowDropDownArrow { get; }
    }
}

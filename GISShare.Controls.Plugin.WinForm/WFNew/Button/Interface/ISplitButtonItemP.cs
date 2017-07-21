using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ISplitButtonItemP : IDropDownButtonItemP, ISubItem
    {
        bool ShowNomalSplitLine { get; }
    }
}

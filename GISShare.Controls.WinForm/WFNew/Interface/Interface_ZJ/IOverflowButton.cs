using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IOverflowButton : IDropDownButtonItem
    {
        bool ReverseLayout { get; }

        Orientation eOrientation { get; }
    }
}

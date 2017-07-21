using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IStatusBarItem
    {
        BaseItemCollection RightBaseItems { get; }
    }
}

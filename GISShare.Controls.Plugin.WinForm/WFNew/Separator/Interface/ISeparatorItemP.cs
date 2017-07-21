using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ISeparatorItemP : IBaseItemP_
    {
        int LineSpace { get;  }

        bool BaseItemStateEnable { get; }

        bool AutoLayout { get; }

        Orientation eOrientation { get;  }
    }
}

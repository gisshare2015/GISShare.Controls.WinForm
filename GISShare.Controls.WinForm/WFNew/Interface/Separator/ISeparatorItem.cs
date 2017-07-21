using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISeparatorItem : IBaseItem2
    {
        int LineSpace { get; set; }

        bool BaseItemStateEnable { get; set; }

        bool AutoLayout { get; set; }

        Orientation eOrientation { get; set; }

        Point StartPointLightLine { get; }

        Point EndPointLightLine { get; }

        Point StartPointDarkLine { get; }

        Point EndPointDarkLine { get; }

    }
}

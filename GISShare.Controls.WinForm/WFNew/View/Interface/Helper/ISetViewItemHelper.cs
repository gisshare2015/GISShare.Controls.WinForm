using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    internal interface ISetViewItemHelper
    {
        bool SetViewParameterStyle(ViewParameterStyle viewParameterStyle);

        bool SetViewItemDisplayRectangle(Rectangle rectangle);
    }
}

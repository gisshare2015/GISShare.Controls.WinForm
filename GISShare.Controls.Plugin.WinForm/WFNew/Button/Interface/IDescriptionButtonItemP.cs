using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IDescriptionButtonItemP : IBaseButtonItemP
    {
        Color DescriptionForeColor { get; }

        Font DescriptionFont { get; }

        string Description { get; }

        int TDSpace { get; }
    }
}

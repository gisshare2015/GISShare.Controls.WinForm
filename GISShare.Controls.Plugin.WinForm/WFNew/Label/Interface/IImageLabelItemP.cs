using System;
using System.Collections.Generic;
using System.Text;
using GISShare.Controls.WinForm.WFNew;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IImageLabelItemP : ILabelItemP, IImageBoxItemP
    {
        bool AutoPlanTextRectangle { get; }

        int ITSpace { get; }

        DisplayStyle eDisplayStyle { get; }
    }
}

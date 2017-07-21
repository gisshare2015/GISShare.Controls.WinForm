using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IRatingStarItemP : IBaseItemP_
    {
        int Value { get; }

        int StarCount { get; }

        int StarSize { get; }

        int StarSpace { get; }
    }
}

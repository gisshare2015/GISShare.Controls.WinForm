using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRatingStarItem : IBaseItem
    {
        int Value { get; set; }

        int StarCount { get; set; }

        int StarSize { get; set; }

        int StarSpace { get; set; }
    }
}

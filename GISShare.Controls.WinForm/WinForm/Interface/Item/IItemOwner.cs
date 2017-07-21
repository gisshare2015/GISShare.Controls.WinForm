using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IItemOwner : WFNew.IOwner
    {
        bool ShowGripRegion { get; }

        int LeftGripRegionWidth { get; }

        int ImageGripRegionWidth { get; }

        int ColorRegionWidth { get; }

        ItemDrawStyle eItemDrawStyle { get; }
    }
}

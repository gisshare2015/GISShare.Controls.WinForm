using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IRadioButtonItemP : IBaseItemDBP_
    {
        int VOffset { get; }

        bool Checked { get; }
    }
}

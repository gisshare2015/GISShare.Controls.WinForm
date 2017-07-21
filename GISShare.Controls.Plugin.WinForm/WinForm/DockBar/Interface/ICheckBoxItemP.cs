using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface ICheckBoxItemP : IBaseItemDBP_
    {
        int VOffset { get; }

        System.Windows.Forms.CheckState CheckState { get; }

        bool Checked { get; }
    }
}

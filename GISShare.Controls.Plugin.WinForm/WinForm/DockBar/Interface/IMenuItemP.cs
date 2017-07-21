using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IMenuItemP : IBaseItemDBP_, ISubItem
    {
        bool Checked { get;  }

        System.Windows.Forms.CheckState CheckState { get; }

        bool CheckOnClick { get; }

        string ShortcutKeyDisplayString { get;  }

        System.Windows.Forms.Keys ShortcutKeys { get; }

        bool ShowShortcutKeys { get;  }
    }
}

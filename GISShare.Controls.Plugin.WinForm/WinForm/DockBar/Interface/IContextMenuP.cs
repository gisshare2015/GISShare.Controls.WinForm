using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IContextMenuP : IPlugin, IPlugin2, ISubItem
    {
        string Text { get; }

        bool Enabled { get; }

        bool Visible { get; }

        bool DropShadowEnabled { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.DockPanel
{
    public interface IDockPanelP : IBaseItemP_, ISubItem
    {
        bool Interal { get; }

        System.Windows.Forms.DockStyle DockStyle { get; }

        bool VisibleEx { get; }

        System.Drawing.Point DockPanelFloatFormLocation { get; }

        System.Drawing.Size DockPanelFloatFormSize { get; }

        int BasePanelSelectedIndex { get; }
    }
}

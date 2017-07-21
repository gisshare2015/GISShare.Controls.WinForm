using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IDockBarP_ : IPlugin, IPlugin2, ISubItem
    {
        string Text { get; }

        System.Drawing.Image Image { get; }

        bool Visible { get; }

        bool CanFloat { get; }

        int Row { get; }

        System.Windows.Forms.DockStyle DockStyle { get; }

        System.Drawing.Point Location { get; }

        System.Drawing.Point DockBarFloatFormLocation { get; }

        System.Drawing.Size DockBarFloatFormSize { get; }

        System.Windows.Forms.Padding GripMargin { get; }

        System.Windows.Forms.ToolStripGripStyle GripStyle { get; }

        GISShare.Controls.WinForm.DockBar.DockBarGripStyles eDockBarGripStyle { get; }

        bool CanOverflow { get; }
    }
}

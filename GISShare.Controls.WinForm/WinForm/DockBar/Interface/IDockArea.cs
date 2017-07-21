using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IDockArea
    {
        DockAreaStyle eDockAreaStyle { get;}

        DockBarManager DockBarManager { get; }
    }

    //

    public enum DockAreaStyle
    {
        eDockBarDockArea,
        eDockBarFloatForm,
        //eDockBarContainer,
        eNone
    }
}

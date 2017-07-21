using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockArea : IDockPanelContainer, IRootNode
    {
        Size Size { get; set;}

        Point  Location { get; set;}

        DockStyle Dock { get; }

        DockAreaStyle eDockAreaStyle { get; }

        Rectangle DockAreaRectangle { get; }//屏幕坐标

        IDockPanel GetIDockPanel();

        DockPanel GetDockPanel();
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanelFloatForm : WFNew.IBaseItem, IDockArea, IDock
    {
        bool bActive { get; }

        Rectangle FrameRectangle { get; }

        Rectangle CaptionRectangle { get; }

        Rectangle TitleRectangle { get; }

        Rectangle MoveRectangle { get; }

        void Show(IDockPanel pDockPanel, Point mousePoint);

        void Show(IDockPanel pDockPanel);

        void Close();
    }
}

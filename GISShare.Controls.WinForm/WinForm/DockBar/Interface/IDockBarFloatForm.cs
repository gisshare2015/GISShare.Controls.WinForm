using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IDockBarFloatForm : IDockArea//, WFNew.IRecordItem
    {
        Image Image { get; }

        IDockBar pDockBar { get; }

        Rectangle DisplayRectangle { get; }

        Rectangle FrameRectangle { get; }

        Rectangle CaptionRectangle { get; }

        Rectangle ImageRectangle { get; }

        Rectangle TitleRectangle { get; }

        Rectangle MoveRectangle { get; }

        Rectangle ResizeRectangle { get; }

        void Show(IDockBar dockBar);

        void Show(IDockBar dockBar, Point location);

        bool ResetSize();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IOwner
    {
        IOwner pOwner { get; }

        Rectangle DisplayRectangle { get; }

        void Refresh();

        void Invalidate(Rectangle rectangle);

        Point PointToClient(Point point);

        Point PointToScreen(Point point);
    }
}

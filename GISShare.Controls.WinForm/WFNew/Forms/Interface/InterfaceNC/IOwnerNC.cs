using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface IOwnerNC : WFNew.IOwner, IOffsetNC
    {
        Point PointToClientNC(Point point);

        Point PointToScreenNC(Point point);
    }
}

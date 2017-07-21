using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDesignHelper
    {
        Rectangle DesignMouseClickRectangle { get; }

        bool DesignMouseClickRectangleContainsEx(Point point);

        Rectangle DesignMouseSelectedRectangle { get; }
    }
}

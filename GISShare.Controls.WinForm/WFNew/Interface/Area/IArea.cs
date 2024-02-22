using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IArea
    {
        bool ShowOutLine { get; }

        Rectangle FrameRectangle { get; }

        bool ShowBackground { get; }

        Rectangle AreaRectangle { get; }
    }
}

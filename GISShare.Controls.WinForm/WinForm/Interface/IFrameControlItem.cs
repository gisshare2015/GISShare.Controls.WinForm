using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface IFrameControlItem : WFNew.IBaseItem
    {
        Rectangle FrameRectangle { get; }

        BorderStyle BorderStyle { get; }
    }
}

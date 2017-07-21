using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface ITextBoxItemDB : WFNew.IBaseItem
    {
        bool ContainsFocus { get;}

        Color BackColor { get; }

        BorderStyle BorderStyle { get; }

        Rectangle FrameRectangle { get; }
    }
}

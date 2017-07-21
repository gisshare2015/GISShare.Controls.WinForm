using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IDockBarFloatFormButton : WFNew.IBaseButtonItem
    {
        DockBarFloatFormButtonStyle eDockBarFloatFormButtonStyle { get;  }

        System.Windows.Forms.Form OperationForm { get; }

        Rectangle GlyphRectangle { get; }
    }

    public enum DockBarFloatFormButtonStyle
    {
        eCloseButton = 44,
        eContextButton
    }
}

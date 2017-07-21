using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IUpDownButtons : WFNew.IBaseItem
    {
        System.Windows.Forms.Orientation eOrientation { get; }

        WFNew.BaseItemState eUpButtonState { get; }

        WFNew.BaseItemState eDownButtonState { get; }

        Rectangle UpButtonRectangle { get; }

        Rectangle DownButtonRectangle { get; }

        Rectangle UpButtonArrowRectangle { get; }

        Rectangle DownButtonArrowRectangle { get; }
    }

}

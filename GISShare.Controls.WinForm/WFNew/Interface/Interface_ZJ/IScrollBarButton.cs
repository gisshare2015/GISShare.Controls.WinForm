using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IScrollBarButton : WFNew.IBaseButtonItem
    {
        ScrollBarButtonStyle eScrollBarButtonStyle { get; }

        System.Windows.Forms.Orientation eOrientation { get; }
    }
}

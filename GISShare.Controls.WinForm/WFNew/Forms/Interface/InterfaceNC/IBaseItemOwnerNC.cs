using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface IBaseItemOwnerNC : WFNew.IBaseItemOwner, IOwnerNC
    {
        Rectangle ItemsRectangleNC { get; }

        Rectangle ItemsViewRectangleNC { get; }

        IntPtr Handle { get; }

        void RefreshNC();

        bool RefreshExNC();
    }
}

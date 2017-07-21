using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabButtonItem : IBaseButtonItem
    {
        int OffsetValue { get; }

        bool ShowCloseButton { get; }

        bool HaveTabButtonContainer { get; }

        BaseItemState eCloseButtonState { get; }

        ITabPageItem pTabPageItem { get; }

        TabAlignment TabAlignment { get; }

        Rectangle CloseButtonRectangle { get; }

        bool IsSelected { get; }

        void Selected();
    }
}

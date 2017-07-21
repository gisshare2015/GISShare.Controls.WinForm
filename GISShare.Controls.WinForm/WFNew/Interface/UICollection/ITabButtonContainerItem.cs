using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabButtonContainerItem : IBaseItemStackExItem
    {
        bool AutoShowOverflowTabButton { get; set; }

        bool AutoVisible { get; set; }

        TabAlignment TabAlignment { get; set; }

        bool UsingCloseTabButton { get; set; }

        TabButtonContainerStyle eTabButtonContainerStyle { get; set; }

        int TabButtonItemSelectedIndex { get; set; }

        Rectangle CloseButtonRectangle { get;}

        Rectangle ContextButtonRectangle { get;}

        ITabButtonItem SelectTabButtonItem { get; }

        ITabControl TryGetTabControl();
    }

    
}

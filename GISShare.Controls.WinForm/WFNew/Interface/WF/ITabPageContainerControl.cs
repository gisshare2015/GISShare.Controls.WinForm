using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabPageContainerControl
    {
        System.Windows.Forms.Control.ControlCollection Controls { get; }

        ITabControl TryGetTabControl();
    }
}

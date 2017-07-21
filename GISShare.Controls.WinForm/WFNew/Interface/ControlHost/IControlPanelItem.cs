using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IControlPanelItem : IControlHostItem
    {
        System.Windows.Forms.Control.ControlCollection Controls { get;}
    }
}

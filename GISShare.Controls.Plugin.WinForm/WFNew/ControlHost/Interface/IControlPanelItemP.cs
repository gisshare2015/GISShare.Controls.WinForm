using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IControlPanelItemP : IBaseItemP_
    {
        System.Windows.Forms.Control[] ChildControls { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IControlHostItemP :  IBaseItemP_
    {
        System.Windows.Forms.Control ControlObject { get; }
    }
}

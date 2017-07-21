using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseButtonExItem : IBaseButtonItem
    {
        System.Windows.Forms.Orientation eOrientation { get; set; }
    }
}

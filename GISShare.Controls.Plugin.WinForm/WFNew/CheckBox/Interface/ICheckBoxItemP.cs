using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ICheckBoxItemP : IBaseItemP_
    {
        CheckState CheckState { get;  }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IFormButtonStack : IBaseItemStackItem
    {
        //bool ShowMinButton { get; set; }

        //bool ShowMaxButton { get; set; }

        //bool ShowHelpButton { get; set; }

        //bool ShowCloseButton { get; set; }

        System.Windows.Forms.Form OperationForm { get; }
    }
}

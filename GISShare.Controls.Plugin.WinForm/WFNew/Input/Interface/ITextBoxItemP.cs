using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ITextBoxItemP : IBaseItemP_
    {
        GISShare.Controls.WinForm.WFNew.BorderStyle eBorderStyle { get; }

        char PasswordChar { get;  }

        bool CanEdit { get; }
    }
}

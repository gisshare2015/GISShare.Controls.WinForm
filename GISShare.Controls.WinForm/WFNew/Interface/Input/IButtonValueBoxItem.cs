using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IButtonValueBoxItem : IButtonTextBoxItem
    {
        object ValueItem { get; set; }
    }
}

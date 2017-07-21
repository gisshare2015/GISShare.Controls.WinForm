using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ILabelExItem : ILabelItem
    {
        bool IsMultipleLine { get; set; }
    }
}

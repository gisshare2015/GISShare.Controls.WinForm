using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IIntegerInputBoxItem : ITextBoxItem
    {
        int Value { get; set; }

        int Step { get; set; }

        int Minimum { get; set; }

        int Maximum { get; set; }
    }
}

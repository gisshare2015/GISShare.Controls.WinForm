using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDoubleInputBoxItem : ITextBoxItem
    {
        double Value { get; set; }

        double Step { get; set; }

        double Minimum { get; set; }

        double Maximum { get; set; }

        int FloatLength { get; set; }

        int ShowFloatLength { get; set; }
    }
}

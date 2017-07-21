using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IDoubleInputBoxItemP : ITextBoxItemP
    {
        double Value { get; }

        double Step { get;}

        double Minimum { get;}

        double Maximum { get; }

        int FloatLength { get; }

        int ShowFloatLength { get; }
    }
}

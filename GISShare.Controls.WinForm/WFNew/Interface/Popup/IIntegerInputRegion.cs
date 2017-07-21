using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IIntegerInputRegion : IInputRegion
    {
        int Value { get; set; }

        int Minimum { get; set; }

        int Maximum { get; set; }
    }
}

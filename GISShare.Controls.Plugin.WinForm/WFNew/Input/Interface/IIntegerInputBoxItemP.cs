using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IIntegerInputBoxItemP : ITextBoxItemP
    {
        int Value { get; }

        int Step { get;  }

        int Minimum { get; }

        int Maximum { get;  }
    }
}

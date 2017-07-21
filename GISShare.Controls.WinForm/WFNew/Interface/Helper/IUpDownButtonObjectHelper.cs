using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IUpDownButtonObjectHelper
    {
        bool UpButtonEnabled { get; }

        bool DownButtonEnabled { get; }

        bool UpButtonVisible { get; }

        bool DownButtonVisible { get; }

        Rectangle UpButtonRectangle { get; }

        Rectangle DownButtonRectangle { get; }

        void UpButtonOperation();

        void DownButtonOperation();
    }
}

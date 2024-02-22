using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPlayProcessBarItem : IProcessBarItem
    {
        Rectangle DrawRectangle { get; }

        Rectangle SliderAreaRectangle { get; }

        Rectangle SliderValueAreaRectangle { get; }

        Rectangle SliderButtonRectangle { get; }
    }
}

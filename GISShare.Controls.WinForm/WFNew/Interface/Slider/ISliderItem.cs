using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISliderItem : IBaseItem
    {
        System.Windows.Forms.Orientation eOrientation { get; set; }

        double Step { get;set; }

        double Value { get;set; }

        double Maximum { get;set; }

        double Minimum { get; set;}

        double Percentage { get; }

        Rectangle DrawRectangle { get; }

        Rectangle MinusButtonRectangle { get; }

        Rectangle PlusButtonRectangle { get; }

        Rectangle SliderAreaRectangle { get; }

        Rectangle SliderValueAreaRectangle { get; }

        Rectangle SliderButtonRectangle { get; }
    }
}

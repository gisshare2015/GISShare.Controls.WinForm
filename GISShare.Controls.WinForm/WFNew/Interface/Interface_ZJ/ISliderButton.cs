using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISliderButton : WFNew.IBaseButtonItem
    {
        SliderButtonStyle eSliderButtonStyle { get; }

        System.Windows.Forms.Orientation eOrientation { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICustomizePopup : IBasePopup2
    {
        int MinHeight { get; set; }

        int MinWidth { get; set; }

        ModifySizeStyle eModifySizeStyle { get; set; }

        Rectangle ModifySizeRegionRectangle { get; }

        Rectangle FrameRectangle { get; }

        System.Windows.Forms.Control ControlObject { get; }

        void SetSize(Size size);
    }
}

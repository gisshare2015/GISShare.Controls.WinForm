using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IInputRegion : IBasePopup2, IInputRegionEvent
    {
        bool InputingFilterText { get; set; }

        char PasswordChar { get; set; }

        object Tag { get; set; }

        void ShowInputRegion();

        Size GetInputRegionSize();

        string TryGetInputingText();
    }
}

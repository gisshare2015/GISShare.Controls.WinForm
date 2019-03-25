using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPopupObjectDesignHelper : IObjectDesignHelper
    {
        void ShowPopup();

        void ClosePopup();
    }
}

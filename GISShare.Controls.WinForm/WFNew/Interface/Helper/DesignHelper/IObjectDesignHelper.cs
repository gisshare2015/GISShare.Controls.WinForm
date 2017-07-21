using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IObjectDesignHelper
    {
        string Name { get; set; }

        string Text { get; set; }

        void Refresh();
    }
}

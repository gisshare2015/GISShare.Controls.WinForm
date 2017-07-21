using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabPageItem : IBaseItem3
    {
        ITabButtonItem pTabButtonItem { get; }
    }
}

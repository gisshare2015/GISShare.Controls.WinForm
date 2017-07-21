using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem3 : IBaseItem2, IBaseItemProperty
    {
        bool IsBaseBarItem { get; }

        bool IsPopupItem { get; }

        bool IsBasePopupItem { get; }

        bool IsDependBasePopup { get; }

        BasePopup TryGetDependBasePopup();

        bool AutoGetFocus { get; set; }

        bool Focus();
    }
}

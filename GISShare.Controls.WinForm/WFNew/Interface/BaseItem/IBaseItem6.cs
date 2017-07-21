using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem6 : IBaseItem5, IDismissPopupObject, IViewDepend
    {
        Size MinimumSize { get; set; }

        bool UsingViewOverflow { get; set; }

        bool ViewOverflow { get; }

        Rectangle ViewRectangle { get; }

        bool TryGetViewRectangle(out Rectangle viewRectangle);

        bool GetOverflowState();

        bool Contains(Point point);
    }
}

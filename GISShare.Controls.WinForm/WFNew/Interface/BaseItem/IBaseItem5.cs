using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem5 : IBaseItem4
    {
        int TryGetIndex();

        bool RemoveSelf();

        bool MoveTo(int index);
    }
}

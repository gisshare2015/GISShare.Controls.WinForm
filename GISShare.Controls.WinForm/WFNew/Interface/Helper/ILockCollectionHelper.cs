using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    internal interface ILockCollectionHelper
    {
        bool Locked { get; }

        void SetLocked(bool locked);
    }
}

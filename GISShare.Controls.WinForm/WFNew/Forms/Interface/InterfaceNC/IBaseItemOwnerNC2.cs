using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface IBaseItemOwnerNC2 : IBaseItemOwnerNC
    {
        bool CancelItemsDrawEventNC { get; }

        bool CancelItemsEventNC { get; }
    }
}

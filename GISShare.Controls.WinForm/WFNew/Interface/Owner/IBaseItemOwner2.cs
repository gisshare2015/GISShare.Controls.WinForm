using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItemOwner2 : IBaseItemOwner
    {
        bool CancelItemsDrawEvent { get; }

        bool CancelItemsEvent { get; }
    }
}

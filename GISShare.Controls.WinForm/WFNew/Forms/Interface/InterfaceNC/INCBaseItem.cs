using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface INCBaseItem : WFNew.IBaseItem2, IOffsetNC
    {
        IBaseItemOwnerNC GetTopBaseItemOwnerNC();
    }
}

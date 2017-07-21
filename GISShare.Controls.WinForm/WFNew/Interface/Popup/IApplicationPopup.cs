using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IApplicationPopup : IBaseItem2, ISimplyPopup
    {
        BaseItemCollection MenuItems { get; }

        BaseItemCollection RecordItems { get; }

        BaseItemCollection OperationItems { get; }
    }
}

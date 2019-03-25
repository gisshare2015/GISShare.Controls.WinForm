using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonApplicationObjectDesignHelper : IObjectDesignHelper
    {
        BaseItemCollection MenuItems { get; }

        BaseItemCollection RecordItems { get; }

        BaseItemCollection OperationItems { get; }
    }
}

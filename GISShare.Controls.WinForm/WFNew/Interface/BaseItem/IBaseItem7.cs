using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem7 : IBaseItem6
    {
        System.Windows.Forms.Padding Margin { get; set; }

        HAlignmentStyle eHAlignmentStyle { get; set; }

        VAlignmentStyle eVAlignmentStyle { get; set; }
    }
}

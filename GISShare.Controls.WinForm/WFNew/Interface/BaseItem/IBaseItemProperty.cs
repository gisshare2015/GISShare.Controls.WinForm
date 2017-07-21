using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItemProperty
    {
        BaseItemStyle eBaseItemStyle { get; }

        IBaseItem3 DependObject { get; }
    }

    
}

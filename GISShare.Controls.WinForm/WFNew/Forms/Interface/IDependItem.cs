using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface IDependItem
    {
        System.Windows.Forms.Control DependObject { get; }

        IntPtr DependObjectHandle { get; }
    }
}

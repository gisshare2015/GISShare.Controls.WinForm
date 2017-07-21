using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem4 : IBaseItem3
    {
        void UIUpdate();

        IOwner GetTopOwner();

        Control TryGetDependControl();

        Form TryGetDependParentForm();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISetExpandableCaptionPanelHelper
    {
        void ResetSize();
        void SetIsExpand(bool bExpand);
        void SetDockStyle(DockStyle eDockStyle);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Popup
{
    public class BasePopupControlHost : ToolStripControlHost
    {
        public BasePopupControlHost(Control ctr)
            : base(ctr)
        {  }

        public BasePopupControlHost(Control ctr, string name)
            : base(ctr, name)
        { }
    }
}

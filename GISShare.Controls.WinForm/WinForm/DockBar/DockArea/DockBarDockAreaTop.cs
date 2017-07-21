using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarDockAreaTop : DockBarDockArea
    {
        public DockBarDockAreaTop()
            : base(DockStyle.Top)
        { }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Top;
            }
        }

    }
}

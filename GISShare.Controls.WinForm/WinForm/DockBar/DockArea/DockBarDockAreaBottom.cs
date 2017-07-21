using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarDockAreaBottom : DockBarDockArea
    {
        public DockBarDockAreaBottom()
            : base(DockStyle.Bottom)
        { }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Bottom;
            }
        }

    }
}

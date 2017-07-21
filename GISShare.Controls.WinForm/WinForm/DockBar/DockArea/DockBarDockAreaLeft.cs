using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarDockAreaLeft : DockBarDockArea
    {
        public DockBarDockAreaLeft()
            : base(DockStyle.Left)
        { }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Left;
            }
        }

    }
}


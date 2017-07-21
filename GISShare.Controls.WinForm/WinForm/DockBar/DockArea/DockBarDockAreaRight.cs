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
    public class DockBarDockAreaRight : DockBarDockArea
    {
        public DockBarDockAreaRight()
            : base(DockStyle.Right)
        { }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Right;
            }
        }
    }
}


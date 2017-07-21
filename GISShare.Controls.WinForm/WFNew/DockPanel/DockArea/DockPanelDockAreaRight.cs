using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class DockPanelDockAreaRight : DockPanelDockArea
    {
        public DockPanelDockAreaRight()
            : base()
        {
            base.Name = "DockPanelDockAreaRight";
            base.Dock = DockStyle.Right;
        }

        [Browsable(false), DefaultValue(DockStyle.Right), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = DockStyle.Right;
            }
        }
    }
}

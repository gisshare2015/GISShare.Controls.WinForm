using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class DockPanelDockAreaLeft : DockPanelDockArea
    {
        public DockPanelDockAreaLeft()
            : base()
        {
            base.Name = "DockPanelDockAreaLeft";
            base.Dock = DockStyle.Left;
        }

        [Browsable(false), DefaultValue(DockStyle.Left), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = DockStyle.Left;
            }
        }
    }
}

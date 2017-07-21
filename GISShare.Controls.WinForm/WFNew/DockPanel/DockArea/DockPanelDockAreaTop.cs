using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class DockPanelDockAreaTop : DockPanelDockArea
    {
        public DockPanelDockAreaTop()
            : base()
        {
            base.Name = "DockPanelDockAreaTop";
            base.Dock = DockStyle.Top;
        }

        [Browsable(false), DefaultValue(DockStyle.Top), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = DockStyle.Top;
            }
        }
    }
}
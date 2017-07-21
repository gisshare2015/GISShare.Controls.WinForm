using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class DockPanelDockAreaBottom : DockPanelDockArea
    {
        public DockPanelDockAreaBottom()
            : base()
        {
            base.Name = "DockPanelDockAreaBottom";
            base.Dock = DockStyle.Bottom;
        }

        [Browsable(false), DefaultValue(DockStyle.Bottom), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = DockStyle.Bottom;
            }
        }
    }
}

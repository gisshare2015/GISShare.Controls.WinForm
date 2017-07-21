using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class DockPanelContainerLeftRight : DockPanelContainer
    {
        public DockPanelContainerLeftRight() 
            : base()
        {
            base.Name = "DockPanelContainerLeftRight"; 
            base.Orientation = Orientation.Vertical;
        }

        [Browsable(false), DefaultValue(Orientation.Vertical)]
        public new Orientation Orientation
        {
            get { return base.Orientation; }
            set
            {
                base.Orientation = Orientation.Vertical;
            }
        }
    }
}

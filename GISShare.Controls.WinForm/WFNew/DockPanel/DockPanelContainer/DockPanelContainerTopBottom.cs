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
    public class DockPanelContainerTopBottom : DockPanelContainer
    {
        public DockPanelContainerTopBottom()
            : base()
        {
            base.Name = "DockPanelContainerTopBottom"; 
            base.Orientation = Orientation.Horizontal;
        }

        [Browsable(false), DefaultValue(Orientation.Horizontal)]
        public new Orientation Orientation
        {
            get { return base.Orientation; }
            set
            {
                base.Orientation = Orientation.Horizontal;
            }
        }
    }
}

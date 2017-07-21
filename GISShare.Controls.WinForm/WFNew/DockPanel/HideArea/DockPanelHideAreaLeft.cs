using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [ToolboxItem(false)]
    sealed class DockPanelHideAreaLeft : DockPanelHideArea
    {
        public DockPanelHideAreaLeft(DockPanelManager dockPanelManager)
            : base(DockStyle.Left, dockPanelManager)
        { }

        [Browsable(false), DefaultValue(DockStyle.Left), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public override Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(base.DisplayRectangle.X, base.DisplayRectangle.Y + 2, base.DisplayRectangle.Width, base.DisplayRectangle.Height - 4);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace GISShare.Controls.WinForm
{
    [ToolboxItem(false)]
    public class VsPropertyGrid : PropertyGrid
    {
        public VsPropertyGrid(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                IUIService service = serviceProvider.GetService(typeof(IUIService)) as IUIService;
                if (service != null)
                {
                    base.ToolStripRenderer = (ToolStripProfessionalRenderer)service.Styles["VsToolWindowRenderer"];
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ILinkLabelItem : ILabelItem
    {
        bool LinkVisited { get; set; }

        System.Windows.Forms.LinkBehavior LinkBehavior { get; set; }
    }
}

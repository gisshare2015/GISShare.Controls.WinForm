using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ILinkLabelItemP : ILabelItemP
    {
        bool LinkVisited { get; }

        System.Windows.Forms.LinkBehavior LinkBehavior { get; }
    }
}

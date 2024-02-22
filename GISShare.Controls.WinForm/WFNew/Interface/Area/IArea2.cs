using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IArea2 : IArea
    {
        bool AreaCustomize { get; }

        Color OutLineColor { get; }

        Color BackgroundColor { get; }

        Image BackgroundImage { get; }

        ImageLayout BackgroundImageLayout { get; }
    }
}

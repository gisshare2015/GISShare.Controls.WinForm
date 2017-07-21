using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IViewItemRenderable : IRenderable
    {
        System.Drawing.Color TextColor { get; set; }
        System.Drawing.Color BackgroundColor { get; set; }
    }
}

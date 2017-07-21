using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm
{
    public interface IItem
    {
        string Name { get;set; }

        string Text { get;set; }

        bool ShowBackColor { get; set; }

        Color BackColor { get; set; }

        object Tag { get; set; }
    }
}

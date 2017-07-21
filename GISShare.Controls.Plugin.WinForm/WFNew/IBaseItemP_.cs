using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IBaseItemP_ : IPlugin, ISetEntityObject, IEventChain, IPlugin2
    {
        string Text { get; }

        bool Visible { get; }

        bool Enabled { get; }

        Padding Padding { get; }

        Font Font { get; }

        Color ForeColor { get; }

        Size Size { get; }
        
        bool Checked { get; }

        bool LockHeight { get; }

        bool LockWith { get; }

        string Category { get; }

        Size MinimumSize { get; }

        bool UsingViewOverflow { get; }
    }
}

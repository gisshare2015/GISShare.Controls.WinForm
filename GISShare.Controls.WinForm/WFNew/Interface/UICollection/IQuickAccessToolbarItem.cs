using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IQuickAccessToolbarItem : IBaseBarItem
    {
        int RoundWidth { get; set;}

        //bool ShowBackground { get; set;}

        QuickAccessToolbarStyle eQuickAccessToolbarStyle { get; set; }

        Rectangle CustomizeButtonRectangle { get;}

        //Rectangle FrameRectangle { get; }
    }
}

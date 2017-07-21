using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface ITabControlX : IFrameControlItem, WinForm.IItemOwner
    {
        int SelectedIndex { get; }
        ImageList ImageList { get; }
        TabAlignment Alignment { get; }
        TabAppearance Appearance { get; }
        Rectangle GetTabRect(int index);

        //

        int ImageSpace { get; set; }

        bool AutoMouseMoveSeleced { get; }

        GraphicsPath GetOutLineRegionPath();
    }
}

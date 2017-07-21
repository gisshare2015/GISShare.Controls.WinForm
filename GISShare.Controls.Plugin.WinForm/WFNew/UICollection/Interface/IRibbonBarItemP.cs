using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IRibbonBarItemP : IBaseItemStackItemP, ISubItem
    {
        Image Image { get; }

        bool ShowNomalState { get; }

        bool GlyphEnabled { get; }

        bool GlyphVisible { get; }

        int LeftTopRadius { get; }

        int RightTopRadius { get; }

        int LeftBottomRadius { get; }

        int RightBottomRadius { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonBarItem : IBaseItem2, IBaseItemStackItem
    {
        Image Image { get; set; }

        bool ShowNomalState { get; set; }

        bool GlyphEnabled { get; set; }

        bool GlyphVisible { get; set; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        bool IsMinState { get; }

        BaseItemState eGlyphState { get; }

        Rectangle DrawRectangle { get;  }

        Rectangle TitleRectangle { get; }

        Rectangle GlyphRectangle { get;}
    }

    
}

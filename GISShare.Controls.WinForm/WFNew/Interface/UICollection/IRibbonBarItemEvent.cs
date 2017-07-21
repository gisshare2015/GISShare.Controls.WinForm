using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonBarItemEvent
    {
        event MouseEventHandler GlyphMouseDown;
        event MouseEventHandler GlyphMouseMove;
        event MouseEventHandler GlyphMouseUp;
        event MouseEventHandler GlyphMouseClick;
        event MouseEventHandler GlyphMouseDoubleClick;

    }
}

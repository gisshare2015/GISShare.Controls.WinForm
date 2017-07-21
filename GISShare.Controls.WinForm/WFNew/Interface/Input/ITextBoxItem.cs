using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITextBoxItem : IBaseItem2
    {
        bool CanEdit { get; set; }

        BorderStyle eBorderStyle { get; set; }

        Rectangle FrameRectangle { get; }

        Rectangle TextRectangle { get; }
    }
}

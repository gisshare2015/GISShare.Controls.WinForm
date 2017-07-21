using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ILabelItem : IBaseItem2
    {
        ContentAlignment TextAlign { get; set; }

        Rectangle TextRectangle { get;}
    }
}

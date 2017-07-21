using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface IRadioButtonX : WFNew.IBaseItem
    {
        int VOffset { get; }

        bool Checked { get; }

        Rectangle CheckRectangle { get; }

        Rectangle TextRectangle { get; }
    }
}

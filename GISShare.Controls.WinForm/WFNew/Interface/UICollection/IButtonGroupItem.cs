using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IButtonGroupItem : IBaseItem2, IBaseItemStackItem
    {
        bool ShowNomalState { get; set; }

        bool UseButtonGroupRadius { get; set; }

        int LeftTopRadius { get; set; }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get; set; }

        int RightBottomRadius { get; set; }

        int FristDrawBaseItemIndex { get; }

        int LastDrawBaseItemIndex { get; }
    }
}

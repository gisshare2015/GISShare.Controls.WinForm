using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItemStackExItem : IBaseItemStackItem
    {
        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        int TopViewItemIndex { get; set; }

        bool PreButtonIncreaseIndex { get; set;}

        bool PreButtonVisible { get; }

        bool NextButtonVisible { get; }

        Rectangle DrawRectangle { get; }

        Rectangle PreButtonRectangle { get;}

        Rectangle NextButtonRectangle { get;}
    }
}

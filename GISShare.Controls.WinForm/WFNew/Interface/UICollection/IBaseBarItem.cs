using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseBarItem : IBaseItemStackItem
    {
        bool ShowNomalState { get; set; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        bool ShowOverflowButton { get; set;}

        Rectangle OverflowButtonRectangle { get;}
    }
    
}

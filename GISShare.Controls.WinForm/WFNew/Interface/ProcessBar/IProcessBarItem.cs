using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IProcessBarItem : IBaseItem
    {
        System.Windows.Forms.Orientation eOrientation { get; set; }

        int Value { get;set; }

        int Maximum { get;set; }

        int Minimum { get; set;}

        int Percentage { get; }

        Rectangle FrameRectangle { get; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IGalleryItem : IBaseItem2, IBaseItemStackItem, IPopupOwner
    {
        int TopViewItemIndex { get; set; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get;set;  }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get; set; }

        bool UpButtonIncreaseIndex { get;set;}

        Rectangle DrawRectangle { get; }

        Rectangle GalleryRectangle { get; }

        Rectangle ScrollRectangle { get; }

        Rectangle ScrollUpButtonRectangle { get; }

        Rectangle ScrollDownButtonRectangle { get; }

        Rectangle ScrollDropDownButtonRectangle { get; }
    }
}

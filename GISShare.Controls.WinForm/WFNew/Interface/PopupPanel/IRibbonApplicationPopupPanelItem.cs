using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonApplicationPopupPanelItem : IBaseItem3, IPopupPanel
    {
        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        int OperationItemsSpace { get; set; }

        int ToHeight { get; }

        int BottomHeight { get; }

        int MenuItemHeight { get; }

        int RecordItemHeight { get; }

        int MaxMenuItemsCount { get; }

        int MinMRHeight { get; }

        int MaxMRHeight { get; }

        int MROMiddleSpace { get; }

        int MenuItemsWidth { get; }

        double RecordItemsWidthFactor { get; }

        Rectangle FrameRectangle { get; }
        Rectangle TopRectangle { get; }
        Rectangle MiddleRectangle { get; }
        Rectangle BottomRectangle { get; }
        Rectangle MenuItemsRectangle { get; }
        Rectangle RecordItemsRectangle { get; }
        Rectangle OperationItemsRectangle { get; }

        Rectangle ItemsRectangle { get; }

        Point AnchorPoint { get; }

        Rectangle AnchorRectangle { get; }

        Rectangle MRRectangle { get; }

        BaseItemCollection MenuItems { get; }

        BaseItemCollection RecordItems { get; }

        BaseItemCollection OperationItems { get; }

        //Size GetIdealSize();

        int OperationItemsColumnDistance { get;set;  }
    }
}

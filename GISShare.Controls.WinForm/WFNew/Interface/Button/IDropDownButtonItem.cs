using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDropDownButtonItem : IBaseButtonItem, ICollectionItem, ICollectionItem2, ICollectionItem3, IPopupOwner2
    {
        int DropDownDistance { get;set; }

        ArrowStyle eArrowStyle { get;set; }

        ArrowDock eArrowDock { get; set; }

        ContextPopupStyle eContextPopupStyle { get; set; }

        Size ArrowSize { get;set; }

        Rectangle ArrowRectangle { get; }

        Rectangle VirtualPopupRectangle { get; }

        Rectangle ButtonTriggerRectangle { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IContextPopupPanelItem : IBaseItemStackExItem, IPopupPanel
    {
        int CheckRegionWidth { get; set; }

        int ImageRegionWidth { get; set; }

        ContextPopupStyle eContextPopupStyle { get; set;}

        Rectangle CIEDrawRectangle { get;}

        Rectangle CheckRectangle { get;}

        Rectangle ImageRectangle { get;}

        Rectangle EntityRectangle { get;}
    }
}

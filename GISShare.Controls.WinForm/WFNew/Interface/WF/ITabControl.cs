using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITabControl : IBaseItem, IArea
    {
        bool CanExchangeItem { get; set;}

        bool UsingCloseTabButton { get; set;}

        bool AutoVisibleTabButton { get; set;}

        bool AutoShowOverflowTabButton { get; set; }

        TabAlignment TabAlignment { get; set;}

        WFNew.TabButtonContainerStyle eTabButtonContainerStyle { get; set;}

        WFNew.PNLayoutStyle ePNLayoutStyle { get; set;}

        int TabPageSelectedIndex { get; set;}

        Rectangle TabButtonContainerRectangle { get; }

        WFNew.ITabPageItem SelectedTabPage { get; }

        WFNew.TabPageCollection TabPages { get; }

        void AddTabPage(WFNew.ITabPageItem pTabPageItem);

        void RemoveTabPage(WFNew.ITabPageItem pTabPageItem);

        bool SetSelectTabPage(WFNew.ITabPageItem pTabPageItem);
    }

    internal interface ITabControlHelper
    {
        IList TabPageList { get; }

        WFNew.BaseItemCollection TabButtonItemCollection { get; }
    }

}

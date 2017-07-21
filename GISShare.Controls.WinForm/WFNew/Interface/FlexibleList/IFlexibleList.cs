using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IFlexibleList : IList
    {
        bool ExchangeItem(int index1, int index2);

        bool ExchangeItem(object item1, object item2);
    }
}

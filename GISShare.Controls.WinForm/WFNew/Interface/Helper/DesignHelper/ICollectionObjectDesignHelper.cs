using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICollectionObjectDesignHelper : IObjectDesignHelper
    {
        IList List { get; }

        bool ExchangeItem(object item1, object item2);
    }
}

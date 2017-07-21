using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IStartButtonItem : IBaseButtonItem, IPopupOwner2, ICollectionItem
    {
        //BaseItemCollection BaseItems { get; }

        BaseItemCollection MenuItems { get; }

        BaseItemCollection RecordItems { get; }

        BaseItemCollection OperationItems { get; }
    }
}

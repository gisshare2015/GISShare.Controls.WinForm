using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class RibbonBarItemCollection : BaseItemCollection
    {
        internal RibbonBarItemCollection(IBaseItemOwner pBaseItemOwner)
            : base(pBaseItemOwner)
        { }

        protected override bool Filtration(BaseItem value)
        {
            if (value is RibbonBarItem) return false;
            return true;
        }
    }        
}

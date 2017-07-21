using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ButtonGroupItemCollection : BaseItemCollection
    {
        internal ButtonGroupItemCollection(IOwner pOwner)
            : base(pOwner)
        { }

        protected override bool Filtration(BaseItem value)
        {
            return value is RibbonGalleryRowItem;
        }
    }        
}

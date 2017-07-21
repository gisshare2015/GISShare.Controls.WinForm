using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class BaseItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public BaseItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type CreateCollectionItemType()
        {
            return typeof(BaseButtonItem);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class ColumnViewItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public ColumnViewItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(ColumnViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ColumnViewItem);
        }
    }
}

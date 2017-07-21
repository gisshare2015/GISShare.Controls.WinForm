using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class TitleViewItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public TitleViewItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(TitleViewItem),
                typeof(ColumnTitleViewItem),
                typeof(RowColumnTitleViewItem),
                typeof(VRowColumnTitleViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ColumnTitleViewItem);
        }
    }
}

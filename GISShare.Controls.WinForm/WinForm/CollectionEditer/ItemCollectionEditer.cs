using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public class ItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public ItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(FontItem), typeof(ImageItem), typeof(ColorItem) };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ImageItem);
        }
    }
}

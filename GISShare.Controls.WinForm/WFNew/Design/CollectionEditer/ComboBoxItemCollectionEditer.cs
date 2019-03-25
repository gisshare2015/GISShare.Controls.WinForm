using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ComboBoxItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public ComboBoxItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(View.ViewItem),
                typeof(View.TextViewItem), 
                typeof(View.ImageViewItem), 
                typeof(View.ColorViewItem), 
                typeof(View.SuperViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(View.ViewItem);
        }
    }
}

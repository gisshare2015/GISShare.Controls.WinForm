using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class ViewItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public ViewItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(ViewItem),
                typeof(SizeViewItem),
                typeof(TextViewItem), 
                typeof(ImageViewItem), 
                typeof(ColorViewItem), 
                typeof(TextEditViewItem), 
                typeof(ResizeViewItem), 
                typeof(RowViewItem),  
                typeof(VRowViewItem),  
                typeof(FlexibleRowViewItem), 
                typeof(FlexibleVRowViewItem),
                //
                typeof(SuperViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ViewItem);
        }
    }
}

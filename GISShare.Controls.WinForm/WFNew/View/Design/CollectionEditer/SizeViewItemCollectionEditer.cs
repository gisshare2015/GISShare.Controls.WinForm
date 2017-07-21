using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class SizeViewItemCollectionEditer : System.ComponentModel.Design.CollectionEditor
    {
        public SizeViewItemCollectionEditer(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
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
                typeof(SizeViewItemEx),
                typeof(TextViewItemEx), 
                typeof(ImageViewItemEx), 
                typeof(ColorViewItemEx),
                typeof(TextEditViewItemEx), 
                typeof(ResizeViewItemEx), 
                typeof(RowViewItemEx), 
                typeof(VRowViewItemEx), 
                typeof(FlexibleRowViewItemEx),
                typeof(FlexibleVRowViewItemEx),
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

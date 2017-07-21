using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class NodeViewItemCollectionEditor2 : System.ComponentModel.Design.CollectionEditor
    {
        public NodeViewItemCollectionEditor2(Type type)
            : base(type) { }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {
                typeof(NodeViewItem),
                typeof(ImageNodeViewItem),
                typeof(ResizeNodeViewItem),
                typeof(ResizeImageNodeViewItem),
                //
                typeof(RowNodeViewItem),
                typeof(VRowNodeViewItem),
                typeof(RowImageNodeViewItem),
                typeof(VRowImageNodeViewItem),
                typeof(FlexibleRowNodeViewItem),
                typeof(FlexibleVRowNodeViewItem),
                typeof(FlexibleRowImageNodeViewItem),
                typeof(FlexibleVRowImageNodeViewItem)
            };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(NodeViewItem);
        }
    }
}

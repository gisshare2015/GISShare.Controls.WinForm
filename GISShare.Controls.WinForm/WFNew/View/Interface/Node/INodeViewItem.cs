using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface INodeViewItem : IVisibleViewItem, ITextViewItem, ITextEditViewItem, INodeViewList
    {
        bool Enabled { get; set; }        

        bool IsExpanded { get; set; }

        bool ShowLines { get; set; }

        bool ShowPlusMinus { get; set; }

        bool ShowNomalState { get; set; }

        bool ShowBaseItemState { get; set; }

        int NodeOffset { get; }

        int NodeTextOffset { get; }

        int NodeDepth { get; }

        int OffsetX { get; }

        bool IsRootNode { get; }

        bool HaveVisibleNodeView { get; }

        NodeViewItem ParentNode { get; }

        INodeViewList NodeViewList { get; }

        Rectangle PlusMinusRectangle { get; }

        Rectangle TextRectangle { get; }
        
        bool SetIsExpand(bool value);

        INodeViewItemTree TryGetNodeViewItemTree();
    }
}

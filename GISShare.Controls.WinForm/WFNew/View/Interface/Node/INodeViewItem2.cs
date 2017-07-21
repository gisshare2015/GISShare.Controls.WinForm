using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface INodeViewItem2 : INodeViewItem
    {
        NodeViewStyle eNodeViewStyle { get; set; }
        bool SystemColor { get; set; }
        Color TitleBorder { get; set; }
        Color TitleBackgroundBegin { get; set; }
        Color TitleBackgroundEnd { get; set; }
    }
}

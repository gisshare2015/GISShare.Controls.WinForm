using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IViewItem : IRenderable
    {
        string Text { get; set; }
        
        ViewParameterStyle eViewParameterStyle { get; }

        BaseItemState eBaseItemState { get; }

        Rectangle DisplayRectangle { get; }

        Size MeasureSize(Graphics g);
    }
}

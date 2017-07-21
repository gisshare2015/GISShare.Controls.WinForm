using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IRowHeaderItem 
    {
        int RowIndex { get; }

        ViewParameterStyle eViewParameterStyle { get; }

        BaseItemState eBaseItemState { get; }

        Rectangle RowHeaderRectangle { get; }
    }
}

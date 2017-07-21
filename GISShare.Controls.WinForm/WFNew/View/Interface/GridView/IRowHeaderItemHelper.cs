using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    internal interface IRowHeaderItemHelper
    {
        void SetIndex(int index);

        void SetRowHeaderRectangle(Rectangle rectangle);

        void DrawRowHeader(System.Windows.Forms.PaintEventArgs e, bool ShowRowIndex, int iRowHeaderStartID);
    }
}

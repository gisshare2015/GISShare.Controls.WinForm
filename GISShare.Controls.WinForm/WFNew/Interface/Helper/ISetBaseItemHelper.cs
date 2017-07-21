using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    internal interface ISetBaseItemHelper
    {
        bool SetLocation(Point point);

        bool SetLocation(int x, int y);

        bool SetDisplayRectangle(Rectangle rectangle);

        bool SetDisplayRectangle(int x, int y, int width, int height);
    }
}

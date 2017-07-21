using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ITranslation
    {
        void Translation(int iX, int iY);
        void Translation(Point fromPoint, Point toPoint);
    }
}

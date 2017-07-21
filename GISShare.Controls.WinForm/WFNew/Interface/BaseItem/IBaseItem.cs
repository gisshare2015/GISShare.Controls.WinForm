using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem : IRenderable
    {
        string Name { get;set; }

        string Text { get;set; }

        bool Visible { get; set; }

        bool Enabled { get; set; }

        Padding Padding { get; set; }

        Font Font { get; set; }

        Color ForeColor { get; set; }

        Point Location { get; }//set; 

        Size Size { get; }//set; 

        int Height { get; }

        int Width { get; }

        Rectangle DisplayRectangle { get; }

        BaseItemState eBaseItemState { get; }

        Point PointToClient(Point point);

        Point PointToScreen(Point point);

        void Refresh();

        void Invalidate(Rectangle rectangle);
    }
}

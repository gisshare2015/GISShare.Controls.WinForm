using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBasePopup : IBaseItem, IBasePopupEvent
    {
        bool AutoClose { get; set; }
        bool DropShadowEnabled { get; set; }
        double Opacity { get; set; }

        //string Name { get; }
        //string Text { get; }
        //int Height { get; }
        //int Width { get; }
        //bool Enabled { get; }
        //bool Visible { get; }
        //Size Size { get; }
        //Point Location { get; }
        //Rectangle DisplayRectangle { get; }
        //void Refresh();
        //void Invalidate(Rectangle rectangle);
        //Point PointToClient(Point point);
        //Point PointToScreen(Point point);

        void Close();
        void Show();
        void Show(Point screenLocation);
        void Show(Control control, Point position);
        void Show(int x, int y);
        void Show(Control control, int x, int y);
    }
}

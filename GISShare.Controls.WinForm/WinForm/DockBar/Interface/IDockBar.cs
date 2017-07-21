using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IDockBar : IDock, WFNew.IRecordItem, ICollectionItemDB
    {
        event EventHandler Resize;
        event BoolValueChangedEventHandler BeforeVisibleExValueSeted;
        event BoolValueChangedEventHandler AfterVisibleExValueSeted;

        //int RecordID { get; }

        string Name { get; }

        string Text { get; set; }

        int MaxWidth { get; }

        int LineMaxWidth { get; }

        Image Image { get; set; }

        bool Visible { get; set; }

        bool VisibleEx { get; set; }

        bool CanFloat { get; set; }

        bool DrawMoreArrow { get; }

        DockStyle Dock { get; set; }

        ToolStripLayoutStyle LayoutStyle { get; set; }

        Point DockBarFloatFormLocation { get; set; }

        Size DockBarFloatFormSize { get; set; }

        bool ShowItemToolTips { get;  }

        Size ImageScalingSize { get;  }

        Size Size { get; }

        Point Location { get; set; }
        
        int Height { get; set; }

        int Width { get; set; }

        IntPtr Handle { get; }

        //string Describe { get; }

        DockBarStyle eDockBarStyle { get;}

        DockAreaStyle eDockAreaStyle { get;}

        DockBarDockArea DockBarDockArea { get;}

        DockBarManager DockBarManager { get; }

        ToolStripItemCollection Items { get; }

        ToolStripGripDisplayStyle GripDisplayStyle { get; }

        Padding GripMargin { get; set; }

        Rectangle GripRectangle { get; }

        ToolStripGripStyle GripStyle { get; set; }

        DockBarGripStyles eDockBarGripStyle { get; set; }

        bool CanOverflow { get; set; }

        Control Parent { get; }

        void Refresh();

        void Reset();

        void RemoveFromParent();

        bool ToDockBarFloatForm();

        bool ToDockBarFloatForm(Point location);
    }
}
